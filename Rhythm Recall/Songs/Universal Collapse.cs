﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal class UniversalCollapse : WaveConstructor, IWaveSet
    {

        //public UniversalCollapse() : base(62.5f / (560/*bpm*/ / 60f)) { }
        public UniversalCollapse() : base(9.375f) { }
        private class ThisInformation : SongInformation
        {
            public override string BarrageAuthor => "Tlottgodinf";
            public override string SongAuthor => "DM DOKURO";
            public override string PaintAuthor => "DM DOKURO";

            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Normal, 11.0f),
                            new(Difficulty.Hard, 14.8f),
                            new(Difficulty.Extreme, 17.9f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Normal, 11.0f),
                            new(Difficulty.Hard, 14.8f),
                            new(Difficulty.Extreme, 18.2f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 6.0f),
                            new(Difficulty.Easy, 11.0f),
                            new(Difficulty.Normal, 16.0f),
                            new(Difficulty.Hard, 19.0f),
                            new(Difficulty.Extreme, 21.2f),
                    }
                );
        }
        public SongInformation Attributes => new ThisInformation();
        public string Music => "Universal Collapse";

        public string FightName => "Universal Collapse";

        public static float bpm = 9.375f;

        public int zero = 0;


        private class Line : Entity
        {
            public int duration = 0;
            public float x1 = 0;
            public float y1 = 0;
            public float x2 = 0;
            public float y2 = 0;
            public Line(float x1, float y1, float x2, float y2, int duration, float alpha)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
                this.duration = duration;
                this.alpha = alpha;
            }
            public float alpha = 0.5f;
            public int time = 0;
            public float speed = 1;
            public override void Draw()
            {
                DrawingLab.DrawLine(new(x1, y1), new(x2, y2), 4, Color.Cyan * alpha, 0.5f);
                Depth = 0.01f;
            }

            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }

            }
        }

        private class LineR : Entity
        {
            public int duration = 0;
            public float xCentre = 0;
            public float yCentre = 0;
            public int rotate = 0;
            public LineR(float xCentre, float yCentre, int rotate, int duration, float alpha)
            {
                this.xCentre = xCentre;
                this.yCentre = yCentre;
                this.rotate = rotate;
                this.duration = duration;
                this.alpha = alpha;
            }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public override void Draw()
            {
                DrawingLab.DrawLine(new(xCentre - Tan(rotate) * yCentre, 0), new(xCentre + Tan(rotate) * (480 - yCentre), 480), 4, Color.White * alpha, 0.99f);
                Depth = 0.99f;
            }

            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }

            }
        }
        public void Effects()
        {
            //屏幕变灰变黑变灰变黑
            if (GametimeF >= 0 && GametimeF <= BeatTime(44))
            {
                var colrgb = MathF.Abs(MathF.Sin(Gametime / BeatTime(4))) / 2;
                ScreenDrawing.BackGroundColor = new(colrgb, colrgb, colrgb);
            }

            //扣字
            if (InBeat(43))
            {
                CreateEntity(new TextPrinter(160, "$It's not over yet kid!", new Vector2(150, 280), new TextColorAttribute(Color.Cyan)));
            }

            Line line1 = new(0, 0, 0, 480, 500, 0.9f),
            line2 = new(640, 0, 640, 480, 500, 0.9f),
            line3 = new(320, 0, 320, 480, 500, 0.9f),
            line4 = new(320, 0, 320, 480, 500, 0.9f),
            linedown = new(0, 480, 640, 480, 500, 0.5f),
            lineup = new(0, 0, 640, 0, 500, 0.5f),
            lineRand = new(80 * Rand(1, 7), 0, 80 * LastRand, 480, 600, 0.9f);
            if (GametimeF == 1)
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 6))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 8))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 14))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 16))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 22))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 23))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 24))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 26))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 28))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 30))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 32))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 36))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 38))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 40))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 44))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 46))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 48))
            {
                CreateEntity(lineRand);
                CreateEntity(lineRand);
            }
            if (GametimeF == (int)(bpm * 52))
            {
                CreateEntity(line1);
                CreateEntity(line2);
                int beattime = 0;
                AddInstance(new TimeRangedEvent(1, 500, () =>
                {
                    line1.x1 += 2f + beattime;
                    line1.x2 += 2f + beattime;
                    line2.x1 -= 2f + beattime;
                    line2.x2 -= 2f + beattime;
                    beattime += 4;
                }));
            }
            if (GametimeF == (int)(bpm * 56))
            {
                CreateEntity(line3);
                CreateEntity(line4);
                int beattime = 0;
                AddInstance(new TimeRangedEvent(1, 500, () =>
                {
                    line3.x1 += 2f + beattime;
                    line3.x2 += 2f + beattime;
                    line4.x1 -= 2f + beattime;
                    line4.x2 -= 2f + beattime;
                    beattime += 4;
                }));
            }
            if (GametimeF == (int)(bpm * 60))
            {
                CreateEntity(line1);
                CreateEntity(line2);
                int beattime = 0;
                AddInstance(new TimeRangedEvent(1, 500, () =>
                {
                    line1.x1 += 2f + beattime;
                    line1.x2 += 2f + beattime;
                    line2.x1 -= 2f + beattime;
                    line2.x2 -= 2f + beattime;
                    beattime += 4;
                }));
            }
            if (GametimeF == (int)(bpm * 70))
            {
                line1.Dispose();
                line2.Dispose();
                line3.Dispose();
                line4.Dispose();
                lineRand.Dispose();
            }
            for (int a = 0; a < 6; a++)
            {
                if (Gametime == (int)(bpm * 16 * 8 + bpm * a - 7 * bpm))
                {
                    CreateEntity(line1);
                    int beattime = 0;
                    AddInstance(new TimeRangedEvent(1, 50, () =>
                    {
                        line1.x1 += 2f + beattime;
                        line1.x2 += 2f + beattime;
                        beattime += 3;
                    }));
                }
            }
            for (int a = 0; a < 6; a++)
            {
                if (Gametime == (int)(bpm * 16 * 16 + a * bpm - 7 * bpm))
                {
                    CreateEntity(line2);
                    int beattime = 0;
                    AddInstance(new TimeRangedEvent(1, 50, () =>
                    {
                        line2.x1 += 2f + beattime;
                        line2.x2 += 2f + beattime;
                        beattime -= 3;
                    }));
                }
            }
            for (int a = 0; a < 6; a++)
            {
                if (Gametime == (int)(bpm * 16 * 56 + bpm * a - 7 * bpm))
                {
                    CreateEntity(line1);
                    int beattime = 0;
                    AddInstance(new TimeRangedEvent(1, 50, () =>
                    {
                        line1.x1 += 2f + beattime;
                        line1.x2 += 2f + beattime;
                        beattime += 3;
                    }));
                }
            }
            for (int a = 0; a < 6; a++)
            {
                if (Gametime == (int)(bpm * 16 * 64 + bpm * a - 7 * bpm))
                {
                    CreateEntity(line2);
                    int beattime = 0;
                    AddInstance(new TimeRangedEvent(1, 50, () =>
                    {
                        line2.x1 += 2f + beattime;
                        line2.x2 += 2f + beattime;
                        beattime -= 3;
                    }));
                }
            }
            for (int a = 0; a < 100; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 4 + a * bpm * 16))
                {
                    CreateEntity(linedown);
                    float beattime = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                      {
                          linedown.y1 -= beattime * beattime;
                          linedown.y2 -= beattime * beattime;
                          beattime += 0.08f;
                      }));
                    AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                      {
                          linedown.y1 += beattime * beattime;
                          linedown.y2 += beattime * beattime;
                          beattime -= 0.08f;
                      }));
                };
                if (GametimeF == (int)(bpm * 16 * 4 + bpm * 8 + a * bpm * 16))
                {
                    CreateEntity(lineup);
                    float beattime = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        lineup.y1 += beattime * beattime;
                        lineup.y2 += beattime * beattime;
                        beattime += 0.08f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                    {
                        lineup.y1 -= beattime * beattime;
                        lineup.y2 -= beattime * beattime;
                        beattime += 0.08f;
                    }));
                };
            }
            AddInstance(new TimeRangedEvent(1, 120, () =>
            {
                lineRand.alpha -= 0.0175f;
            }));

        }
        public void Easy()
        {
            Effects();

            if (GametimeF == (int)(bpm * 16 * 3 - 5))
            {

                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(160, "$It's not over yet kid!", new Vector2(150, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));

                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 3))
            {
                ScreenDrawing.BackGroundColor = new(0, 0, 0);
                //138,43,226
                SetSoul(1);
                PlaySound(Sounds.Ding);
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 - 80))
            {
                CreateArrow(80, Rand(0, 3), 6, 0, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 2 - 80))
            {
                CreateArrow(80, Rand(0, 3), 6, 0, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 3 - 80))
            {
                CreateArrow(80, Rand(0, 3), 6, 0, 0);
            }



            if (GametimeF == (int)(bpm * 16 * 4 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Partd + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parte + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partf + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partg + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4+bpm*4)

                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 0, 0);
                }
            }



            if (Gametime == (int)(bpm * 16 * 8 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 11, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 11, 0, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 16 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 11, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 11, 0, 0);
                }
            }
            //纵连



            if (GametimeF == (int)(bpm * 16 * 20))
            {
                SetBox(0, 641, 240 - 42, 240 + 42);
                SetSoul(0);
                PlaySound(Sounds.Ding);
                Heart.EnabledRedShield = true;
            }
            for (int a = 1; a < 65; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 + a * 4 * bpm))
                {
                    CreateBone(new UpBone(true, 2.47f, 38) { ColorType = Rand(1, 2) });
                    CreateBone(new DownBone(false, 2.47f, 38) { ColorType = Rand(1, 2) });
                    PlaySound(Sounds.pierce);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 2 * bpm))
            {
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                SetSoul(1);
                TP();
                Heart.EnabledRedShield = false;
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 80))
            {
                int Part1a = (int)(bpm * 14 + bpm * 2 + bpm * 12 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part1b = (int)(Part1a + bpm * 4 + bpm * 3 + bpm + bpm * 4 + bpm * 4 + bpm * 7 + bpm * 0.5f + bpm * 0.5f + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1c = (int)(Part1b + bpm * 4 + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int Part1d = (int)(Part1c + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1e = (int)(Part1d + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part1f = (int)(Part1e + bpm * 8 + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 4);
                int Part1g = (int)(Part1f + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int[] Arrow1 ={

                zero,
                (int)(bpm*14),
                (int)(bpm*14 + bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),

                (int)(Part1a+bpm*4),
                (int)(Part1a+bpm*4+bpm*3),
                (int)(Part1a+bpm*4+bpm*3+bpm),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2+bpm*2),

                (int)(Part1b+bpm*4),
                (int)(Part1b+bpm*4+bpm),
                (int)(Part1b+bpm*4+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),

                (int)(Part1c+bpm*2),
                (int)(Part1c+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2+bpm*2),

                (int)(Part1d+bpm),
                (int)(Part1d+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4+bpm*4),

                (int)(Part1e+bpm*8),
                (int)(Part1e+bpm*8+bpm),
                (int)(Part1e+bpm*8+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4+bpm*4),

                (int)(Part1f+bpm*4),
                (int)(Part1f+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
            };
                for (int a = 0; a < Arrow1.Length; a++)
                {
                    CreateArrow(80 + Arrow1[a], Rand(0, 3), 5, 1, 0);
                }
            }
            //旋转红矛双押

            if (GametimeF == (int)(bpm * 16 * 52 - 80))
            {
                int Part2a = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2b = (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2c = (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2d = (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part2e = (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2f = (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2g = (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow2 =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2a + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2b + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2c + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Part2d + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2e + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2f + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2g + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    };
                for (int aa = 0; aa < Arrow2.Length; aa++)
                {
                    CreateArrow(Arrow2[aa] + 80, Rand(0, 3), 6.2f, 0, 0);
                };
            }
            //红矛
            if (Gametime == (int)(bpm * 16 * 56 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 13, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 13, 0, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 64 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 13, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 13, 0, 0);
                }
            }
            //纵连2
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2))
            {
                SetSoul(0);
                SetBox(320 - 84, 320 + 84, 240 - 84, 240 + 84);
                TP(320 + 24, 240 - 24);
            }
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2 + 5))
            {
                CreateBone(new CentreCircleBone(300, 3.5f, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm));
            }
            //设置状态
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17))
            {
                CreateBone(new CentreCircleBone(210, 3.5f, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm) { ColorType = 2 });
            }
            //旋转橙骨
            //尾杀开始

            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                ScreenDrawing.BackGroundColor = new(118, 25, 111);
            }
            //上色
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(10000, "$A GOD DOES NOT FEAR DEATH!!!", new Vector2(90, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));
                SetSoul(1);
                TP();
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);

                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32 - 80))
            {
                for (int a = 0; a < 128; a++)
                {
                    CreateArrow(80 + (int)(a * 2 * bpm), Rand(0, 3), 5, 0, 0);
                }
            }
            //蓝矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 48 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),
                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 0, 0);
                }
            }



        }
        public void ExtremePlus()
        {


        }
        public void Hard()
        {
            Effects();

            if (GametimeF == (int)(bpm * 16 * 3 - 5))
            {

                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(160, "$It's not over yet kid!", new Vector2(150, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));

                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 3))
            {
                ScreenDrawing.BackGroundColor = new(0, 0, 0);
                //138,43,226
                SetSoul(1);
                PlaySound(Sounds.Ding);
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, LastRand + 2, 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 2 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, LastRand + 2, 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 3 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, LastRand + 2, 8, 1, 0);
            }



            if (GametimeF == (int)(bpm * 16 * 4 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Partd + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parte + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partf + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partg + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4+bpm*4)

                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 0, 0);
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 1, 0);
                }
            }



            if (Gametime == (int)(bpm * 16 * 8 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 11, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 11, 0, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 16 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 11, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 11, 0, 0);
                }
            }
            //纵连



            if (GametimeF == (int)(bpm * 16 * 20))
            {
                SetBox(0, 641, 240 - 42, 240 + 42);
                SetSoul(0);
                PlaySound(Sounds.Ding);
                Heart.EnabledRedShield = true;
            }
            for (int a = 1; a < 129; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 + a * 2 * bpm))
                {
                    CreateBone(new UpBone(true, 2.47f, 38) { ColorType = Rand(1, 2) });
                    CreateBone(new DownBone(false, 2.47f, 38) { ColorType = Rand(1, 2) });
                    PlaySound(Sounds.pierce);
                }
            }
            for (int a = 0; a < 16; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 - 80 + a * 16 * bpm))
                {
                    CreateArrow(80, 0, 5, 1, 1);
                    CreateArrow(80 + bpm * 8, 2, 5, 1, 1);
                };
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 2 * bpm))
            {
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                SetSoul(1);
                TP();
                Heart.EnabledRedShield = false;
            }
            for (int a = 0; a < 16 * 4 - 1; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 36 - 80 + a * 4 * bpm))
                {
                    CreateArrow(80, 0, 8, 0, 1, ArrowAttribute.RotateR);
                    CreateArrow(80 + (int)(bpm), 1, 8, 0, 1, ArrowAttribute.RotateR);
                    CreateArrow(80 + (int)(2 * bpm), 2, 8, 0, 1, ArrowAttribute.RotateR);
                    CreateArrow(80 + (int)(3 * bpm), 3, 8, 0, 1, ArrowAttribute.RotateR);
                }
            }
            int[] GB =
                {
                    zero,
                    (int)(bpm*8),
                    (int)(bpm*8 *2),
                    (int)(bpm * 8*3),
                    (int)(bpm * 8*4),
                    (int)(bpm * 8*5),
                    (int)(bpm * 8*6),
                    (int)(bpm * 8*7),
                    (int)(bpm * 8*8),
                    (int)(bpm * 8*9),
                    (int)(bpm * 8*10),
                    (int)(bpm * 8*11),
                    (int)(bpm * 8*12),
                    (int)(bpm * 8*12+bpm*12),
                    (int)(bpm * 8*12+bpm*14),
                    (int)(bpm * 8*12+bpm*16),
                    (int)(bpm * 8*16),
                    (int)(bpm * 8*17),
                    (int)(bpm * 8*18),
                    (int)(bpm * 8*19),
                    (int)(bpm * 8*20),
                    (int)(bpm * 8*21),
                    (int)(bpm * 8*22),
                    (int)(bpm * 8*23),
                    (int)(bpm * 8*24),

                    (int)(bpm * 8*25),
                    (int)(bpm * 8*26),
                    (int)(bpm * 8*27),
                    (int)(bpm * 8*28),
                    (int)(bpm * 8*29),
                    (int)(bpm * 8*30),
                    (int)(bpm * 8*31),

                };
            if (GametimeF == (int)(bpm * 16 * 52 - 80))
            {
                int Part2a = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2b = (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2c = (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2d = (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part2e = (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2f = (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2g = (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow2 =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2a + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2b + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2c + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Part2d + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2e + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2f + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2g + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    };
                for (int aa = 0; aa < Arrow2.Length; aa++)
                {
                    CreateArrow(Arrow2[aa] + 80, Rand(0, 3), 6.2f, 0, 0);
                    CreateArrow(Arrow2[aa] + 80, Rand(0, 3), 6.2f, 1, 0);
                };
            }
            //红矛
            if (Gametime == (int)(bpm * 16 * 56 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 13, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 13, 0, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 64 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 13, 0, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 13, 0, 0);
                }
            }
            //纵连2
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2))
            {
                SetSoul(0);
                SetBox(320 - 84, 320 + 84, 240 - 84, 240 + 84);
                TP();
            }
            //设置状态
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17))
            {
                CreateBone(new CentreCircleBone(300, -3.5f, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm) { ColorType = 2 });
            }
            //旋转橙骨

            for (int a = 0; a < 32; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 + GB[a]))
                {
                    if (Rand(1, 2) == 1)
                    {
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84), new(320 + 84, 240 - 84), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42), new(320 + 84, 240 - 84 + 42), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 2), new(320 + 84, 240 - 84 + 42 * 2), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 3), new(320 + 84, 240 - 84 + 42 * 3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 4), new(320 + 84, 240 - 84 + 42 * 4), new(1, 0.2f), 180, 60, 15));

                        CreateGB(new NormalGB(new(320 - 84, 240 - 84), new(320 - 84, 240 - 84), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42), new(320 - 84, 240 - 84 + 42), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 2), new(320 - 84, 240 - 84 + 42 * 2), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 3), new(320 - 84, 240 - 84 + 42 * 3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 4), new(320 - 84, 240 - 84 + 42 * 4), new(1, 0.2f), 0, 60, 15));
                        //横向gb墙
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84), new(320 + 84, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42, 240 - 84), new(320 + 84 - 42, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 2, 240 - 84), new(320 + 84 - 42 * 2, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 3, 240 - 84), new(320 + 84 - 42 * 3, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 4, 240 - 84), new(320 + 84 - 42 * 4, 240 - 84), new(1, 0.2f), 90, 60, 15));

                        CreateGB(new NormalGB(new(320 - 84, 240 + 84), new(320 - 84, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42, 240 + 84), new(320 - 84 - 42, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 2, 240 + 84), new(320 - 84 - 42 * 2, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 3, 240 + 84), new(320 - 84 - 42 * 3, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 4, 240 + 84), new(320 - 84 - 42 * 4, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        //纵向gb墙
                    }
                    else
                    {
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * -1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * -3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));

                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * -1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * -3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        //横向gb墙
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 + 21 * 3), new(320 - 21 * 3, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 1, 240 + 21 * 3), new(320 - 21 * 1, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * -1, 240 + 21 * 3), new(320 - 21 * -1, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * -3, 240 + 21 * 3), new(320 - 21 * -3, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));

                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 1, 240 - 21 * 3), new(320 - 21 * 1, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * -1, 240 - 21 * 3), new(320 - 21 * -1, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * -3, 240 - 21 * 3), new(320 - 21 * -3, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));

                        //纵向gb墙
                    }
                }
            }
            //尾杀开始

            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                ScreenDrawing.BackGroundColor = new(118, 25, 111);
            }
            //上色
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(10000, "$A GOD DOES NOT FEAR DEATH!!!", new Vector2(90, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));
                SetSoul(1);
                TP();
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                //扣字
            }
            //蓝矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32 - 80))
            {
                int Part1a = (int)(bpm * 14 + bpm * 2 + bpm * 12 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part1b = (int)(Part1a + bpm * 4 + bpm * 3 + bpm + bpm * 4 + bpm * 4 + bpm * 7 + bpm * 0.5f + bpm * 0.5f + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1c = (int)(Part1b + bpm * 4 + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int Part1d = (int)(Part1c + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1e = (int)(Part1d + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part1f = (int)(Part1e + bpm * 8 + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 4);
                int Part1g = (int)(Part1f + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int[] Arrow1 ={
                zero,
                (int)(bpm*14),
                (int)(bpm*14 + bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1a+bpm*4),
                (int)(Part1a+bpm*4+bpm*3),
                (int)(Part1a+bpm*4+bpm*3+bpm),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2+bpm*2),
                (int)(Part1b+bpm*4),
                (int)(Part1b+bpm*4+bpm),
                (int)(Part1b+bpm*4+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1c+bpm*2),
                (int)(Part1c+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2+bpm*2),
                (int)(Part1d+bpm),
                (int)(Part1d+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4+bpm*4),
                (int)(Part1e+bpm*8),
                (int)(Part1e+bpm*8+bpm),
                (int)(Part1e+bpm*8+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4+bpm*4),
                (int)(Part1f+bpm*4),
                (int)(Part1f+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
            };
                for (int a = 0; a < Arrow1.Length; a++)
                {
                    CreateArrow(80 + Arrow1[a], Rand(0, 3), 6.5f, 1, 0);
                    if (a % 4 == 0)
                    {
                        CreateArrow(80 + Arrow1[a], Rand(0, 3), 6.5f, 0, 0);
                    }
                }
            }
            //双押红矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 48 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),
                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 0, 0);
                    if (a % 4 == 0)
                    {
                        CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 1, 0);
                    }
                }
            }
        }
        public void Noob()
        {
            Effects();

            if (GametimeF == (int)(bpm * 16 * 3 - 5))
            {

                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(160, "$It's not over yet kid!", new Vector2(150, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));

                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 3))
            {
                ScreenDrawing.BackGroundColor = new(0, 0, 0);
                //138,43,226
                SetSoul(1);
                PlaySound(Sounds.Ding);
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 - 80))
            {
                CreateArrow(80, Rand(0, 3), 6.5f, 0, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 2 - 80))
            {
                CreateArrow(80, Rand(0, 3), 6.5f, 0, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 3 - 80))
            {
                CreateArrow(80, Rand(0, 3), 6.5f, 0, 0);
            }



            if (GametimeF == (int)(bpm * 16 * 4 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Partd + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parte + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partf + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partg + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4+bpm*4)

                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 0, 0);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 20))
            {
                SetBox(0, 641, 240 - 42, 240 + 42);
                SetSoul(0);
                PlaySound(Sounds.Ding);
                Heart.EnabledRedShield = true;
            }
            for (int a = 1; a < 65; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 + a * 4 * bpm))
                {
                    CreateBone(new UpBone(true, 2.47f, 38) { ColorType = Rand(1, 2) });
                    CreateBone(new DownBone(false, 2.47f, 38) { ColorType = Rand(1, 2) });
                    PlaySound(Sounds.pierce);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 2 * bpm))
            {
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                SetSoul(1);
                TP();
                Heart.EnabledRedShield = false;
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 80))
            {
                int Part1a = (int)(bpm * 14 + bpm * 2 + bpm * 12 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part1b = (int)(Part1a + bpm * 4 + bpm * 3 + bpm + bpm * 4 + bpm * 4 + bpm * 7 + bpm * 0.5f + bpm * 0.5f + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1c = (int)(Part1b + bpm * 4 + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int Part1d = (int)(Part1c + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1e = (int)(Part1d + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part1f = (int)(Part1e + bpm * 8 + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 4);
                int Part1g = (int)(Part1f + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int[] Arrow1 ={

                zero,
                (int)(bpm*14),
                (int)(bpm*14 + bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),

                (int)(Part1a+bpm*4),
                (int)(Part1a+bpm*4+bpm*3),
                (int)(Part1a+bpm*4+bpm*3+bpm),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2+bpm*2),

                (int)(Part1b+bpm*4),
                (int)(Part1b+bpm*4+bpm),
                (int)(Part1b+bpm*4+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),

                (int)(Part1c+bpm*2),
                (int)(Part1c+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2+bpm*2),

                (int)(Part1d+bpm),
                (int)(Part1d+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4+bpm*4),

                (int)(Part1e+bpm*8),
                (int)(Part1e+bpm*8+bpm),
                (int)(Part1e+bpm*8+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4+bpm*4),

                (int)(Part1f+bpm*4),
                (int)(Part1f+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
            };
                for (int a = 0; a < Arrow1.Length; a++)
                {
                    CreateArrow(80 + Arrow1[a], Rand(0, 3), 5, 0, 0);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 52 - 80))
            {
                int Part2a = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2b = (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2c = (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2d = (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part2e = (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2f = (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2g = (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow2 =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2a + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2b + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2c + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Part2d + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2e + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2f + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2g + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    };
                for (int aa = 0; aa < Arrow2.Length; aa++)
                {
                    CreateArrow(Arrow2[aa] + 80, Rand(0, 3), 5.4f, 0, 0);
                };
            }

            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2))
            {
                SetSoul(0);
                SetBox(320 - 84, 320 + 84, 240 - 84, 240 + 84);
                TP(320 + 24, 240 - 24);
            }
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2 + 5))
            {
                CreateBone(new CentreCircleBone(300, 2.7f, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm));
            }
            //设置状态
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17))
            {
                CreateBone(new CentreCircleBone(210, 2.7f, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm) { ColorType = 2 });
            }
            //旋转橙骨
            //尾杀开始

            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                ScreenDrawing.BackGroundColor = new(118, 25, 111);
            }
            //上色
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(10000, "$A GOD DOES NOT FEAR DEATH!!!", new Vector2(90, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));
                SetSoul(1);
                TP();
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32 - 80))
            {
                for (int a = 0; a < 128; a++)
                {
                    CreateArrow(80 + (int)(a * bpm * 2), Rand(0, 3), 5.2f, 0, 0);
                }
            }
            //蓝矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 48 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),
                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 6, 0, 0);
                }
            }

        }
        public void Normal()
        {
            Effects();

            if (GametimeF == (int)(bpm * 16 * 3 - 5))
            {

                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(160, "$It's not over yet kid!", new Vector2(150, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));

                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 3))
            {
                ScreenDrawing.BackGroundColor = new(0, 0, 0);
                //138,43,226
                SetSoul(1);
                PlaySound(Sounds.Ding);
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, LastRand + 2, 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 2 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, LastRand + 2, 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 3 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, LastRand + 2, 8, 1, 0);
            }

            if (GametimeF == (int)(bpm * 16 * 4 - 80))
            {
                for (int a = 0; a < 256; a++)
                {
                    CreateArrow(80 + (int)(a * bpm), Rand(0, 3), 5f, 0, 0);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 20))
            {
                SetBox(0, 641, 240 - 42, 240 + 42);
                SetSoul(0);
                PlaySound(Sounds.Ding);
                Heart.EnabledRedShield = true;
            }
            for (int a = 1; a < 129; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 + a * 2 * bpm))
                {
                    CreateBone(new UpBone(true, 2.47f, 38) { ColorType = Rand(1, 2) });
                    CreateBone(new DownBone(false, 2.47f, 38) { ColorType = Rand(1, 2) });
                    PlaySound(Sounds.pierce);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 2 * bpm))
            {
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                SetSoul(1);
                TP();
                Heart.EnabledRedShield = false;
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 80))
            {
                for (int a = 0; a < 256; a++)
                {
                    CreateArrow(80 + (int)(a * bpm), Rand(0, 3), 5.8f, 0, 0);
                }
            }
            if (GametimeF == (int)(bpm * 16 * 52 - 80))
            {
                int Part2a = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2b = (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2c = (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2d = (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part2e = (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part2f = (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Part2g = (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow2 =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2a + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2a + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2b + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2b + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2c + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Part2c + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Part2d + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2d + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2e + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2e + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Part2f + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2f + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Part2g + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Part2g + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    };
                for (int aa = 0; aa < Arrow2.Length; aa++)
                {
                    CreateArrow(Arrow2[aa] + 80, Rand(0, 3), 6.2f, 0, 0);
                    if (aa % 4 == 0)
                        CreateArrow(Arrow2[aa] + 80, Rand(0, 3), 6.2f, 1, 0);
                };
            }



            int[] GB =
                {
                    zero,
                    (int)(bpm*8),
                    (int)(bpm*8 *2),
                    (int)(bpm * 8*3),
                    (int)(bpm * 8*4),
                    (int)(bpm * 8*5),
                    (int)(bpm * 8*6),
                    (int)(bpm * 8*7),
                    (int)(bpm * 8*8),
                    (int)(bpm * 8*9),
                    (int)(bpm * 8*10),
                    (int)(bpm * 8*11),
                    (int)(bpm * 8*12),
                    (int)(bpm * 8*12+bpm*12),
                    (int)(bpm * 8*12+bpm*14),
                    (int)(bpm * 8*12+bpm*16),
                    (int)(bpm * 8*16),
                    (int)(bpm * 8*17),
                    (int)(bpm * 8*18),
                    (int)(bpm * 8*19),
                    (int)(bpm * 8*20),
                    (int)(bpm * 8*21),
                    (int)(bpm * 8*22),
                    (int)(bpm * 8*23),
                    (int)(bpm * 8*24),

                    (int)(bpm * 8*25),
                    (int)(bpm * 8*26),
                    (int)(bpm * 8*27),
                    (int)(bpm * 8*28),
                    (int)(bpm * 8*29),
                    (int)(bpm * 8*30),
                    (int)(bpm * 8*31),

                };
            //GB

            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2))
            {
                SetSoul(0);
                SetBox(320 - 84, 320 + 84, 240 - 84, 240 + 84);
                TP();
            }


            for (int a = 0; a < 32; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 + GB[a]))
                {
                    if (Rand(1, 2) == 1)
                    {
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84), new(320 + 84, 240 - 84), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42), new(320 + 84, 240 - 84 + 42), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 2), new(320 + 84, 240 - 84 + 42 * 2), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 3), new(320 + 84, 240 - 84 + 42 * 3), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 4), new(320 + 84, 240 - 84 + 42 * 4), new(1, 0.3f), 180, 60, 15));

                        CreateGB(new NormalGB(new(320 - 84, 240 - 84), new(320 - 84, 240 - 84), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42), new(320 - 84, 240 - 84 + 42), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 2), new(320 - 84, 240 - 84 + 42 * 2), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 3), new(320 - 84, 240 - 84 + 42 * 3), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 4), new(320 - 84, 240 - 84 + 42 * 4), new(1, 0.3f), 0, 60, 15));
                        //横向gb墙
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84), new(320 + 84, 240 - 84), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42, 240 - 84), new(320 + 84 - 42, 240 - 84), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 2, 240 - 84), new(320 + 84 - 42 * 2, 240 - 84), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 3, 240 - 84), new(320 + 84 - 42 * 3, 240 - 84), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 4, 240 - 84), new(320 + 84 - 42 * 4, 240 - 84), new(1, 0.3f), 90, 60, 15));

                        CreateGB(new NormalGB(new(320 - 84, 240 + 84), new(320 - 84, 240 + 84), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42, 240 + 84), new(320 - 84 - 42, 240 + 84), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 2, 240 + 84), new(320 - 84 - 42 * 2, 240 + 84), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 3, 240 + 84), new(320 - 84 - 42 * 3, 240 + 84), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 4, 240 + 84), new(320 - 84 - 42 * 4, 240 + 84), new(1, 0.3f), 270, 60, 15));
                        //纵向gb墙
                    }
                    else
                    {
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * -1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * -3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 180, 60, 15));

                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * -1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * -3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.3f), 0, 60, 15));
                        //横向gb墙
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 + 21 * 3), new(320 - 21 * 3, 240 - 21 * 3), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 1, 240 + 21 * 3), new(320 - 21 * 1, 240 - 21 * 3), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * -1, 240 + 21 * 3), new(320 - 21 * -1, 240 - 21 * 3), new(1, 0.3f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * -3, 240 + 21 * 3), new(320 - 21 * -3, 240 - 21 * 3), new(1, 0.3f), 270, 60, 15));

                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * 3), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 1, 240 - 21 * 3), new(320 - 21 * 1, 240 - 21 * 3), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * -1, 240 - 21 * 3), new(320 - 21 * -1, 240 - 21 * 3), new(1, 0.3f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * -3, 240 - 21 * 3), new(320 - 21 * -3, 240 - 21 * 3), new(1, 0.3f), 90, 60, 15));

                        //纵向gb墙
                    }
                }
            }
            //尾杀开始

            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                ScreenDrawing.BackGroundColor = new(118, 25, 111);
            }
            //上色
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(10000, "$A GOD DOES NOT FEAR DEATH!!!", new Vector2(90, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));
                SetSoul(1);
                TP();
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32 - 80))
            {
                int Part1a = (int)(bpm * 14 + bpm * 2 + bpm * 12 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part1b = (int)(Part1a + bpm * 4 + bpm * 3 + bpm + bpm * 4 + bpm * 4 + bpm * 7 + bpm * 0.5f + bpm * 0.5f + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1c = (int)(Part1b + bpm * 4 + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int Part1d = (int)(Part1c + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1e = (int)(Part1d + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part1f = (int)(Part1e + bpm * 8 + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 4);
                int Part1g = (int)(Part1f + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int[] Arrow1 ={
                zero,
                (int)(bpm*14),
                (int)(bpm*14 + bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1a+bpm*4),
                (int)(Part1a+bpm*4+bpm*3),
                (int)(Part1a+bpm*4+bpm*3+bpm),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2+bpm*2),
                (int)(Part1b+bpm*4),
                (int)(Part1b+bpm*4+bpm),
                (int)(Part1b+bpm*4+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1c+bpm*2),
                (int)(Part1c+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2+bpm*2),
                (int)(Part1d+bpm),
                (int)(Part1d+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4+bpm*4),
                (int)(Part1e+bpm*8),
                (int)(Part1e+bpm*8+bpm),
                (int)(Part1e+bpm*8+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4+bpm*4),
                (int)(Part1f+bpm*4),
                (int)(Part1f+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
            };
                for (int a = 0; a < Arrow1.Length; a++)
                {
                    CreateArrow(80 + Arrow1[a], Rand(0, 3), 7f, 1, 0);
                    CreateArrow(80 + Arrow1[a], LastRand, 6, 0, 0);
                }
            }
            //双押红矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 48 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),
                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 6, 0, 0);
                    CreateArrow(Arrow[a] + 80, LastRand, 6, 1, 0);
                }
            }



        }
        public void Extreme()
        {
            Effects();

            if (GametimeF == (int)(bpm * 16 * 3))
            {
                ScreenDrawing.BackGroundColor = new(0, 0, 0);
                //138,43,226
                SetSoul(1);
                PlaySound(Sounds.Ding);
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
            }
            if (GametimeF == BeatTime(52) - 80)
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, Rand(0, 3), 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 2 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, Rand(0, 3), 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 3 + bpm * 4 * 3 - 80))
            {
                CreateArrow(80, Rand(0, 3), 8, 0, 0);
                CreateArrow(80, Rand(0, 3), 8, 1, 0);
            }
            if (GametimeF == (int)(bpm * 16 * 4 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Partd + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parte + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partf + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partg + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4+bpm*4)

                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 10, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 7, 0, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 8 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 11, 1, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 11, 1, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 16 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 11, 1, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 11, 1, 0);
                }
            }
            //纵连
            if (GametimeF == (int)(bpm * 16 * 20))
            {
                SetBox(0, 641, 240 - 42, 240 + 42);
                SetSoul(3);
                PlaySound(Sounds.Ding);
                Heart.EnabledRedShield = true;
            }
            for (int a = 1; a < 129; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 + a * 2 * bpm))
                {
                    CreateBone(new UpBone(true, 2.47f, 38) { ColorType = Rand(1, 2) });
                    CreateBone(new DownBone(false, 2.47f, 38) { ColorType = Rand(1, 2) });
                    PlaySound(Sounds.pierce);
                }
            }
            for (int a = 0; a < 32; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 - 80 + a * 8 * bpm))
                {
                    CreateArrow(80, 0, 5, 1, 1);
                    CreateArrow(80 + bpm * 4, 2, 5, 1, 1);
                };
            }
            if (GametimeF == (int)(bpm * 16 * 36 - 2 * bpm))
            {
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                SetSoul(1);
                TP();
                Heart.EnabledRedShield = false;
            }
            for (int a = 0; a < 16 * 4 - 1; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 36 - 80 + a * 4 * bpm))
                {
                    CreateArrow(80, 0, 8, 0, 1, ArrowAttribute.RotateL);
                    CreateArrow(80 + (int)(bpm), 1, 8, 0, 1, ArrowAttribute.RotateL);
                    CreateArrow(80 + (int)(2 * bpm), 2, 8, 0, 1, ArrowAttribute.RotateL);
                    CreateArrow(80 + (int)(3 * bpm), 3, 8, 0, 1, ArrowAttribute.RotateL);
                }
            }
            int[] GB =
                {
                    zero,
                    (int)(bpm*8),
                    (int)(bpm*8 *2),
                    (int)(bpm * 8*3),
                    (int)(bpm * 8*4),
                    (int)(bpm * 8*5),
                    (int)(bpm * 8*6),
                    (int)(bpm * 8*7),
                    (int)(bpm * 8*8),
                    (int)(bpm * 8*9),
                    (int)(bpm * 8*10),
                    (int)(bpm * 8*11),
                    (int)(bpm * 8*12),
                    (int)(bpm * 8*12+bpm*12),
                    (int)(bpm * 8*12+bpm*14),
                    (int)(bpm * 8*12+bpm*16),
                    (int)(bpm * 8*16),
                    (int)(bpm * 8*17),
                    (int)(bpm * 8*18),
                    (int)(bpm * 8*19),
                    (int)(bpm * 8*20),
                    (int)(bpm * 8*21),
                    (int)(bpm * 8*22),
                    (int)(bpm * 8*23),
                    (int)(bpm * 8*24),

                    (int)(bpm * 8*25),
                    (int)(bpm * 8*26),
                    (int)(bpm * 8*27),
                    (int)(bpm * 8*28),
                    (int)(bpm * 8*29),
                    (int)(bpm * 8*30),
                    (int)(bpm * 8*31),

                };
            for (int a = 0; a < GB.Length; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 20 + GB[a]))
                {
                    if (Rand(0, 1) == 0)
                    {
                        CreateGB(new NormalGB(new Vector2(Rand(0, 640), 240 + 128), new Vector2(Rand(0, 640), 240 + 128), new Vector2(1, 0.4f), bpm * 4, bpm * 2));
                    }
                    else
                    {
                        CreateGB(new NormalGB(new Vector2(Rand(0, 640), 240 - 128), new Vector2(Rand(0, 640), 240 - 128), new Vector2(1, 0.4f), bpm * 4, bpm * 2));
                    }

                }
            }
            //GB
            if (GametimeF == (int)(bpm * 16 * 36 - 80))
            {
                int Part1a = (int)(bpm * 14 + bpm * 2 + bpm * 12 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part1b = (int)(Part1a + bpm * 4 + bpm * 3 + bpm + bpm * 4 + bpm * 4 + bpm * 7 + bpm * 0.5f + bpm * 0.5f + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1c = (int)(Part1b + bpm * 4 + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int Part1d = (int)(Part1c + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1e = (int)(Part1d + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part1f = (int)(Part1e + bpm * 8 + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 4);
                int Part1g = (int)(Part1f + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int[] Arrow1 ={

                zero,
                (int)(bpm*14),
                (int)(bpm*14 + bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),

                (int)(Part1a+bpm*4),
                (int)(Part1a+bpm*4+bpm*3),
                (int)(Part1a+bpm*4+bpm*3+bpm),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2+bpm*2),

                (int)(Part1b+bpm*4),
                (int)(Part1b+bpm*4+bpm),
                (int)(Part1b+bpm*4+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),

                (int)(Part1c+bpm*2),
                (int)(Part1c+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2+bpm*2),

                (int)(Part1d+bpm),
                (int)(Part1d+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4+bpm*4),

                (int)(Part1e+bpm*8),
                (int)(Part1e+bpm*8+bpm),
                (int)(Part1e+bpm*8+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4+bpm*4),

                (int)(Part1f+bpm*4),
                (int)(Part1f+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
            };
                for (int a = 0; a < Arrow1.Length; a++)
                {
                    CreateArrow(80 + Arrow1[a], Rand(0, 3), 5, 1, 0);
                }
            }
            //旋转红矛双押

            if (GametimeF == (int)(bpm * 16 * 52 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);

                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),

                    (int)(Partd + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Parte + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),

                    (int)(Partf + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                    (int)(Partg + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partg + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),

                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 10, 1, 0, ArrowAttribute.SpeedUp);
                }
                for (int a = 0; a < 256; a++)
                {
                    CreateArrow(80 + (int)(a * bpm), Rand(0, 3), 6, 0, 0);
                }
                //无脑乱蓝箭头+红矛杀
            }
            if (Gametime == (int)(bpm * 16 * 56 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 13, 1, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 13, 1, 0);
                }
            }
            if (Gametime == (int)(bpm * 16 * 64 - 80 - 7 * bpm))
            {
                for (int a = 0; a < 3; a++)
                {
                    CreateArrow(80 + a * 2 * bpm, 0, 13, 1, 0);
                    CreateArrow(80 + bpm + a * 2 * bpm, 2, 13, 1, 0);
                }
            }
            //纵连2
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2))
            {
                SetSoul(0);
                SetBox(320 - 84, 320 + 84, 240 - 84, 240 + 84);
                TP();
            }
            if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 - bpm * 2 + 5))
            {
                CreateBone(new CentreCircleBone(300, 1, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm));
                CreateBone(new CentreCircleBone(210, 1, 224, bpm * 16 * 1.5f + bpm * 16 * 15 + 4 * bpm) { ColorType = 2 });
            }
            //设置状态
            //设置状态

            //旋转橙骨

            for (int a = 0; a < 32; a++)
            {
                if (GametimeF == (int)(bpm * 16 * 51 + bpm * 16 * 17 + GB[a]))
                {
                    if (Rand(1, 2) == 1)
                    {
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84), new(320 + 84, 240 - 84), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42), new(320 + 84, 240 - 84 + 42), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 2), new(320 + 84, 240 - 84 + 42 * 2), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 3), new(320 + 84, 240 - 84 + 42 * 3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * 4), new(320 + 84, 240 - 84 + 42 * 4), new(1, 0.2f), 180, 60, 15));

                        CreateGB(new NormalGB(new(320 - 84, 240 - 84), new(320 - 84, 240 - 84), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42), new(320 - 84, 240 - 84 + 42), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 2), new(320 - 84, 240 - 84 + 42 * 2), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 3), new(320 - 84, 240 - 84 + 42 * 3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * 4), new(320 - 84, 240 - 84 + 42 * 4), new(1, 0.2f), 0, 60, 15));
                        //横向gb墙
                        CreateGB(new NormalGB(new(320 + 84, 240 - 84), new(320 + 84, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42, 240 - 84), new(320 + 84 - 42, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 2, 240 - 84), new(320 + 84 - 42 * 2, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 3, 240 - 84), new(320 + 84 - 42 * 3, 240 - 84), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 84 - 42 * 4, 240 - 84), new(320 + 84 - 42 * 4, 240 - 84), new(1, 0.2f), 90, 60, 15));

                        CreateGB(new NormalGB(new(320 - 84, 240 + 84), new(320 - 84, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42, 240 + 84), new(320 - 84 - 42, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 2, 240 + 84), new(320 - 84 - 42 * 2, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 3, 240 + 84), new(320 - 84 - 42 * 3, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 84 + 42 * 4, 240 + 84), new(320 - 84 - 42 * 4, 240 + 84), new(1, 0.2f), 270, 60, 15));
                        //纵向gb墙
                    }
                    else
                    {
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * -1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * -3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));

                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * -1), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * -3), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                        //横向gb墙
                        CreateGB(new NormalGB(new(320 - 21 * 3, 240 + 21 * 3), new(320 - 21 * 3, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * 1, 240 + 21 * 3), new(320 - 21 * 1, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * -1, 240 + 21 * 3), new(320 - 21 * -1, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                        CreateGB(new NormalGB(new(320 - 21 * -3, 240 + 21 * 3), new(320 - 21 * -3, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));

                        CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3), new(320 - 21 * 3, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * 1, 240 - 21 * 3), new(320 - 21 * 1, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * -1, 240 - 21 * 3), new(320 - 21 * -1, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                        CreateGB(new NormalGB(new(320 + 21 * -3, 240 - 21 * 3), new(320 - 21 * -3, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));

                        //纵向gb墙
                    }
                }
            }
            //尾杀开始

            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                ScreenDrawing.BackGroundColor = new(118, 25, 111);
            }
            //上色
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32))
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(10000, "$A GOD DOES NOT FEAR DEATH!!!", new Vector2(150, 280), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan)));
                SetSoul(1);
                TP();
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                //扣字
            }
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32 - 80))
            {
                for (int a = 0; a < 256; a++)
                {
                    CreateArrow(80 + (int)(a * bpm), Rand(0, 3), 6.5f, 0, 0);
                }
            }
            //蓝矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 32 - 80))
            {
                int Part1a = (int)(bpm * 14 + bpm * 2 + bpm * 12 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Part1b = (int)(Part1a + bpm * 4 + bpm * 3 + bpm + bpm * 4 + bpm * 4 + bpm * 7 + bpm * 0.5f + bpm * 0.5f + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1c = (int)(Part1b + bpm * 4 + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int Part1d = (int)(Part1c + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2);
                int Part1e = (int)(Part1d + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 4 + bpm * 4 + bpm * 8 + bpm * 4 + bpm * 4);
                int Part1f = (int)(Part1e + bpm * 8 + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 4);
                int Part1g = (int)(Part1f + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4 + bpm * 4);
                int[] Arrow1 ={
                zero,
                (int)(bpm*14),
                (int)(bpm*14 + bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2),
                (int)(bpm*14 + bpm*2+bpm*12+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2+bpm*8+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1a+bpm*4),
                (int)(Part1a+bpm*4+bpm*3),
                (int)(Part1a+bpm*4+bpm*3+bpm),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2),
                (int)(Part1a+bpm*4+bpm*3+bpm+bpm*4+bpm*4+bpm*7+bpm*0.5f+bpm*0.5f+bpm*4+bpm*2+bpm*2),
                (int)(Part1b+bpm*4),
                (int)(Part1b+bpm*4+bpm),
                (int)(Part1b+bpm*4+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1b+bpm*4+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1c+bpm*2),
                (int)(Part1c+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2),
                (int)(Part1c+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*2+bpm*6+bpm*2+bpm*4+bpm*2+bpm*2),
                (int)(Part1d+bpm),
                (int)(Part1d+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4),
                (int)(Part1d+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*4+bpm*4+bpm*8+bpm*4+bpm*4),
                (int)(Part1e+bpm*8),
                (int)(Part1e+bpm*8+bpm),
                (int)(Part1e+bpm*8+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4),
                (int)(Part1e+bpm*8+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm+bpm*2+bpm*2+bpm*2+bpm*2+bpm*4+bpm*4),
                (int)(Part1f+bpm*4),
                (int)(Part1f+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
                (int)(Part1f+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4+bpm*4),
            };
                for (int a = 0; a < Arrow1.Length; a++)
                {
                    CreateArrow(80 + Arrow1[a], Rand(0, 3), 6.5f, 1, 0);
                }
            }
            //双押红矛
            if (GametimeF == (int)(bpm * 16 * 52 + bpm * 16 * 48 - 80))
            {
                int Parta = (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partb = (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partc = (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partd = (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4);
                int Parte = (int)(Partd + bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int Partf = (int)(Parte + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8);
                int Partg = (int)(Partf + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2);
                int[] Arrow =
                {
                    zero,
                    (int)(bpm * 6),
                    (int)(bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(bpm * 6 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 6 + bpm + bpm + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Parta + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 4 + bpm * 2 + bpm * 2 + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partb + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 6 + bpm * 2 + bpm * 8 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4),
                    (int)(Partc + bpm * 3 + bpm * 3 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 2 + bpm * 8 + bpm * 4 + bpm * 4),
                };
                for (int a = 0; a < Arrow.Length; a++)
                {
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 0, 0);
                    CreateArrow(Arrow[a] + 80, Rand(0, 3), 5, 1, 0);
                }
            }


        }
        public void Start()
        {
            SetBox(320 - 8, 320 + 8, 240 - 8, 240 + 8);
            SetSoul(0);
            HeartAttribute.DamageTaken = 1;
            HeartAttribute.MaxHP = 8;
            TP();
        }
    }



}