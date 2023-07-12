﻿using Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static Extends.Someway;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.CameraEffect;

namespace Rhythm_Recall.Waves
{
    public class GOODWORLD : IChampionShip
    {
        public GOODWORLD()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Easy);
            difficulties.Add("div.1", Difficulty.Hard);

        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "Tlottgodinf";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "EBIMAYO";
                public override string AttributeAuthor => "Tlottgodinf";
                public override string Extra => "Fixed by TK";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Hard, 13.8f),
                            new(Difficulty.Easy, 7.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Hard, 14.3f),
                            new(Difficulty.Easy, 7.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Hard, 19.0f),
                            new(Difficulty.Easy, 12.0f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();
            public class Linerotatee : Entity
            {
                public float duration = 0;
                public float xCenter = 0;
                public float yCenter = 0;
                public float rotate = 0;
                public float width = 4;
                public Color color = Color.White;
                public Linerotatee(float xCenter, float yCenter, float rotate, float duration, float alpha, Color color)
                {
                    this.xCenter = xCenter;
                    this.yCenter = yCenter;
                    this.rotate = rotate;
                    this.duration = duration;
                    this.alpha = alpha;
                    this.color = color;
                }
                public Linerotatee(float xCenter, float yCenter, float rotate, float duration, float alpha) : this(xCenter, yCenter, rotate, duration, alpha, Color.White) { }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public float depth = 0.99f;
                public override void Draw()
                {
                    if (rotate % 180 != 0)
                        DrawingLab.DrawLine(new(xCenter - (1f / Tan(rotate)) * (yCenter), -50), new(xCenter + (1f / Tan(rotate)) * (480 - yCenter), 520), width, color * alpha, depth);
                    else
                        DrawingLab.DrawLine(new(0, yCenter), new(640, yCenter), width, color * alpha, depth);
                    Depth = 0.99f;
                }

                public override void Update()
                {
                    time++;
                    if (time >= duration)
                    {
                        Dispose();
                    }

                }
            }
            public Game() : base(62.5f / (165/*bpm*/ / 60f)) { }
            public static Game instance;
            public Color GroundColor = Color.Black;
            public static Player.Heart heart;
            public string Music => "GOODWORLD";
            public string FightName => "GOODWORLD";
            public static float bpm = 62.5f / (165/*bpm*/ / 60f);
            public void Hard()
            {
                ScreenDrawing.BoundColor = new(200, 18, 35);
                ScreenDrawing.BackGroundColor = GroundColor;

                if (InBeat(-2))
                {
                    heart = Heart.Split();
                    heart.InstantTP(new(-500, -500));
                    SetBoxMission(1);
                    InstantSetBox(-500, 84, 84);
                    SetBoxMission(0);
                    SetPlayerMission(0);
                    LerpGreenBox(bpm * 2, new(320, 240), 0.095f);
                    SpecialRhythmCreate(bpm * 0.125f, 6, bpm * 2,
                        new string[]
                        {
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",

                            "($01)(R)","/","/","/",   "R","/","/","/",
                            "($01)(R)","/","/","/",   "R","/","/","/",
                            "($01)(R)","/","/","/",   "R","/","/","/",
                            "($01)(R)","/","/","/",   "R","/","/","/",

                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",

                            "($21)","($21)","($21)","($21)",   "($21)","/","/","/",
                            "($0)","($0)","($0)","($0)",   "($0)","/","/","/",
                            "(R)","/","/","/",   "R","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                        },
                        new string[] { },
                        new Action[] { }
                        );
                }
                if (InBeat(8 * 4 - 1.5f))
                {
                    LerpGreenBox(bpm * 2, new(320, 240), 0.095f);
                    SpecialRhythmCreate(bpm * 0.125f, 6, bpm * 1.5f,
                        new string[]
                        {
                            #region intro
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",
                            "(R)(+0)","/","!+1","/",   "@-1","/","!+1","/",
                            "@-1","/","!+1","/",   "D1","/","/","/",

                            "D1","/","/","/",   "D1","/","/","/",
                            "D1","/","/","/",   "D1","/","/","/",
                            "D1","/","/","/",   "D1","/","/","/",
                            "D1","/","/","/",   "D1","/","/","/",
#endregion
                            #region intro
                            "move","~($3)($31)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","R1","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","R","/",
                            "/","/","/","/",   "WR18","/","/","/","/",

                            "/","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "WR08","/","/","/","/",   "/","/","/","/",
                            "WR18","/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","R1","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","R","/",
                            "R","/","R","/",   "R","/","R","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",

                           "WR08","/","/","/","/",   "/","/","/","/",
                           "/","/","/","/",   "WR18","/","/","/","/",
                           "/","/","/","/",   "/","/","/","/",
                           "WR08","/","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "WR18","/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "WR08","/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
#endregion
                            #region intro
                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "box","(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "box1","(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "box2","(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "box1","(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "box2","(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "box1","(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "box2","(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "R1","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "go","(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            #endregion
                            #region intro
                            "sc","/","/","/","/",   "/","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",
                            "/","/","($0)($2)","/",   "/","/","/","/",

                            "sc2","/","/","/","/",   "/","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","/","/",

                            "sc3","/","/","/","/",   "/","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",
                            "/","/","($0)($2)","/",   "/","/","/","/",

                            "sc4","/","/","/","/",   "/","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","/","/",

                            "($0)($2)(R1)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",
                            "($0)($2)(R1)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",

                             "($0)($2)(R1)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($2)","/","/","/",
                            "($0)($2)(R1)","/","/","/",   "($0)($2)","/","/","/",
                            "($0)($2)","/","/","/",   "/","/","/","/",

                            "($0)($2)(R1)","/","/","/",   "($0)($2)","/","/","/",
                            "($0)($2)","/","/","/",   "($0)($2)","/","/","/",
                            "arrow","angle","$0","/","+1","/",   "+1","/","+1","/",
                            "+1","/","+1","/",   "+1","/","+1","/",

                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
#endregion
                            #region intro
                            "First","biu","R1","/","/","/",   "biu","R1","/","/","/",
                            "biu","$0","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "/","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","R1","/","/","/",   "biu","R1","/","/","/",
                            "biu","$0","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","+2","/","biu","+2","/",   "/","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "R1","/","R1","/",   "R1","/","R1","/",
                             "/","/","/","/",   "/","/","/","/",

                            "Second","biu","R1","/","/","/",   "biu","R1","/","/","/",
                            "biu","$0","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "/","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","R1","/","/","/",   "biu","R1","/","/","/",
                            "biu","$0","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "ret","R","+0","+0","+0",   "+0","/","/","/",
                            "R1","+01","+01","+01",   "+01","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",


                            #endregion
                            #region intro
                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
#endregion
                            #region intro
                            "First","biu","R1","/","/","/",   "biu","R1","/","/","/",
                            "biu","$0","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu", "/","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "biu","R1","/","/","/",  "biu", "R1","/","/","/",
                            "biu","$0","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                           "biu", "+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",
                            "biu","+2","/","biu","+2","/",   "biu","+2","/","biu","+2","/",

                            "over","R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R(R1)","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                             "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "(R)(R1)","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R(R1)","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                             "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "(R)(R1)","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                             "R","/","/","/",   "R","/","/","/",
                            "R1","/","R1","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","R","/",
                            "R","/","R","/",   "R","/","R","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","R","/",   "R","/","/","/",

                            "WR08","/","/","/","/",   "/","/","/","/",
                           "/","/","/","/",   "WR18","/","/","/","/",
                           "/","/","/","/",   "/","/","/","/",
                           "WR08","/","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "WR18","/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "WR08","/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",


#endregion
                            #region intro
                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R1","/","/","/",   "/","/","R1","/",
                            "/","/","/","/",   "R1","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R1","/","/","/",   "/","/","/","/",
                            "(R)(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            #endregion
                            #region final
                            "zan","/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","R1","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","R","/",
                            "/","/","/","/",   "R1","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","R1","/",
                            "/","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","/","/",

                            "R(R1)","/","/","/",   "/","/","R(R1)","/",
                            "/","/","/","/",   "R(R1)","/","/","/",
                            "R(R1)","/","/","/",   "/","/","R(R1)","/",
                            "/","/","/","/",   "R(R1)","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","R1","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","R","/",
                            "/","/","/","/",   "R1","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","R1","/",
                            "/","/","/","/",   "R","/","/","/",
                            "R1","/","/","/",   "R1","/","/","/",

                            "R","+0","+0","+0",   "+0","/","/","/",
                            "R1","+01","+01","+01",   "+01","/","/","/",
                            "R(R1)","/","/","/",   "/","/","R(R1)","/",
                            "/","/","/","/",   "R(R1)","/","/","/",

                             "R","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",

                            "R(R1)","/","/","/",   "/","/","R(R1)","/",
                            "/","/","/","/",   "R(R1)","/","/","/",
                            "R(R1)","/","/","/",   "/","/","R(R1)","/",
                            "/","/","/","/",   "R(R1)","/","/","/",

                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "R(R1)","/","/","/",

                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "R(R1)","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",

                            "R(R1)","/","/","/",   "R","/","/","/",
                            "R(R1)","/","/","/",   "R","/","/","/",
                            "R(R1)","/","/","/",   "R","/","/","/",
                            "R(R1)","/","/","/",   "R","/","/","/",

                            "R(R1)","/","/","/",   "R(R1)","/","/","/",
                            "R(R1)","/","/","/",   "R(R1)","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",
                            "R(R1)","/","/","/",   "/","/","/","/",

                            "R(R1)","/","/","/",   "/","/","/","/",
#endregion
                        },
                        new string[] { "move", "sc", "sc2", "sc3", "sc4", "angle", "arrow", "box", "box1", "box2", "go", "First", "biu", "ret", "Second", "over", "zan" },
                        new Action[]
                        {
                            ()=>
                            {
                                ForBeat(2,()=>{
                                    DownBoundDistance= DownBoundDistance*0.95f+140*0.05f;
                        InstantSetBox((510+90) * 0.06f + BoxStates.Centre.Y * 0.94f, 84, 84);
                        InstantTP(320, (510+90) * 0.06f + Heart.Centre.Y * 0.94f);
                                });
                                DelayBeat(3,()=>{InstantSetBox(-90,84,84);InstantTP(320,-90); });
                                ForBeat(4,4,()=>{
                                    DownBoundDistance= DownBoundDistance*0.95f+280*0.05f;
                        InstantSetBox(240 * 0.06f + BoxStates.Centre.Y * 0.94f, 84, 84);
                        InstantTP(320, 240 * 0.06f + Heart.Centre.Y * 0.94f);
                                });
                                ForBeat(8, 4, () =>
                                {
                                    DownBoundDistance=DownBoundDistance*0.95f+0*0.05f;
                                });
                            },
                            ()=>
                            {
                                SizeExpand(400,bpm*2);
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Red,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.Red,0.1f);
                                });
                                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(bpm*4),"$$$////WARNING////",new(0,170),new UndyneFight_Ex.Fight.TextAttribute[]{new UndyneFight_Ex.Fight.TextColorAttribute(new(255,176,4)),new UndyneFight_Ex.Fight.TextSizeAttribute(3f),new UndyneFight_Ex.Fight.TextSpeedAttribute(114) }));
                                ForBeat(2,2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Black,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                            },
                            ()=>
                            {
                                SizeExpand(400,bpm*2);
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Red,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.Red,0.1f);
                                });
                                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(bpm*4),"$$$///ENEMYYY///",new(0,170),new UndyneFight_Ex.Fight.TextAttribute[]{new UndyneFight_Ex.Fight.TextColorAttribute(new(255,176,4)),new UndyneFight_Ex.Fight.TextSizeAttribute(3f),new UndyneFight_Ex.Fight.TextSpeedAttribute(114) }));
                                ForBeat(2,2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Black,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                            },
                            ()=>
                            {
                                SizeExpand(400,bpm*2);
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Red,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.Red,0.1f);
                                });
                                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(bpm*4),"$$$///PROTECT///",new(0,170),new UndyneFight_Ex.Fight.TextAttribute[]{new UndyneFight_Ex.Fight.TextColorAttribute(new(255, 171, 1)),new UndyneFight_Ex.Fight.TextSizeAttribute(3f),new UndyneFight_Ex.Fight.TextSpeedAttribute(114) }));
                                ForBeat(2,2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Black,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                            },
                            ()=>
                            {
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,new(3,20,4),0.1f);
                                    UIColor=Color.Lerp(UIColor,new(3,20,4),0.1f);
                                });
                                for(int a=0;a<13;a++)
                                {
                                    CreateTagLine(new Linerotate(40+a*50,240,90,bpm*16,0f,Color.Green){ width=2f,depth=0.01f},"A") ;
                                }
                                for(int a=0;a<9*2;a++)
                                {
                                    CreateTagLine(new Linerotate(320,480-15-a*50,00,bpm*16,0f,Color.Green){ width=2f,depth=0.01f},"B");
                                }
                                AddInstance(new TimeRangedEvent(2,1, () =>
                                {
                                    LineMoveLibrary.VecLinear("B",bpm*16,new(0,1));
                                    LineMoveLibrary.AlphaLerp("A",bpm*4,0.5f,0.08f);
                                    LineMoveLibrary.AlphaLerp("B",bpm*4,0.5f,0.08f);
                                }));
                            },
                            ()=>
                            {
                                LineMoveLibrary.AlphaLerp("A",bpm*8,0,0.1f);
                                LineMoveLibrary.AlphaLerp("B",bpm*8,0,0.1f);
                                ForBeat(2, () =>
                                {
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                                for(int a=0;a<8;a++){DelayBeat(a*0.25f,()=>{GroundColor=new(255/8*a,0,0); }); }


                            },
                            ()=>
                            {
                                Arrow a =MakeArrow(bpm*2+3f,0,5,0,0);
                                Arrow b =MakeArrow(bpm*2+3f,2,5,1,0);
                                a.Tags=new string[]{ "a"};
                                b.Tags=new string[]{ "a"};
                                CreateEntity(a);
                                CreateEntity(b);
                                ForBeat(4, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,new(0,0,0),0.1f);
                                    UIColor=Color.Lerp(UIColor,new(255,255,255),0.1f);
                                });
                                DelayBeat(2,()=>
                                {
                                    a.Delay(bpm*1+1);
                                    b.Delay(bpm*1+1);
                                });
                                DelayBeat(3,()=>
                                {
                                    a.Delay(bpm*1+1);
                                    b.Delay(bpm*1+1);
                                });
                                DelayBeat(4,()=>
                                {
                                    a.Delay(bpm*1-3);
                                    b.Delay(bpm*1-3);
                                });
                                DelayBeat(4.5f,()=>
                                {
                                    a.ResetColor(1);
                                    a.Speed=40;
                                    b.ResetColor(0);
                                    b.Speed=40;
                                });
                            },
                            ()=>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                InstantTP(-100,480-90);
                                InstantSetBox(new Vector2(-100,480-90),84,84);
                                LerpGreenBox(bpm*4,new(50,480-90),0.09f);
                            },
                            ()=>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                LerpGreenBox(bpm*4,new(640-50,480-90),0.09f);
                            },
                            ()=>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                LerpGreenBox(bpm*4,new(50,480-90),0.09f);
                            },
                            () =>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                LerpGreenBox(bpm*4,new(800,480-90),0.09f);
                                DelayBeat(8,()=>{SetPlayerMission(0);SetBoxMission(0); });
                            },
                            () =>
                            {
                            ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.RGBSplitting());
                            AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                DownBoundDistance=DownBoundDistance*0.91f+300*0.09f;
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*10);
                             }));
                            },
                            ()=>
                            {
                                Linerotatee line = new(Rand(50,640-50),240,90+Rand(-35,35),26,1){ width=0,depth=0.001f };
                                float sin=-1;
                                AddInstance(new TimeRangedEvent(35
                                   , () =>
                                {
                                    line.width=42*sin*sin*sin*sin;
                                    sin+=1f/35f;
                                    if(line.width<=1)line.width=0;
                                }));
                                CreateEntity(line);
                            },
                            () =>
                            {
                                AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*0);
                             }));
                            },
                            () =>
                            {
                            AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*-10);
                             }));
                            },
                            () =>
                            {
                                AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                DownBoundDistance=DownBoundDistance*0.91f+0*0.09f;
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*0);
                             }));
                            },
                            () =>
                            {
                                SizeExpand(40,bpm*4);
                            }
                        }
                        );
                }
            }
            public void Easy()
            {
                if (InBeat(-2))
                {
                    heart = Heart.Split();
                    heart.InstantTP(new(-500, -500));
                    SetBoxMission(1);
                    InstantSetBox(-500, 84, 84);
                    SetBoxMission(0);
                    SetPlayerMission(0);
                    LerpGreenBox(bpm * 2, new(320, 240), 0.095f);
                    SpecialRhythmCreate(bpm * 0.125f, 6, bpm * 2,
                        new string[]
                        {
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",

                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",

                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",
                            "($21)(R)","/","/","/",   "R","/","/","/",

                            "($2)","($2)","($2)","($2)",   "($2)","/","/","/",
                            "($0)","($0)","($0)","($0)",   "($0)","/","/","/",
                            "(R)","/","/","/",   "R","/","/","/",
                            "(R)","/","/","/",   "/","/","/","/",
                        },
                        new string[] { },
                        new Action[] { }
                        );
                }
                if (InBeat(8 * 4 - 1.5f))
                {
                    LerpGreenBox(bpm * 2, new(320, 240), 0.095f);
                    SpecialRhythmCreate(bpm * 0.125f, 6, bpm * 1.5f,
                        new string[]
                        {
                            #region intro
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",
                            "(R)(+0)","/","!+0","/",   "@-0","/","!+0","/",
                            "@-0","/","!+0","/",   "D","/","/","/",

                            "D","/","/","/",   "D","/","/","/",
                            "D","/","/","/",   "D","/","/","/",
                            "D","/","/","/",   "D","/","/","/",
                            "D","/","/","/",   "D","/","/","/",
#endregion
                            #region intro
                            "move","~($3)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "/","/","/","/",   "WR18","/","/","/","/",

                            "/","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "WR08","/","/","/","/",   "/","/","/","/",
                            "WR18","/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "+0","/","+0","/",   "+0","/","+0","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",

                           "WR08","/","/","/","/",   "/","/","/","/",
                           "/","/","/","/",   "WR08","/","/","/","/",
                           "/","/","/","/",   "/","/","/","/",
                           "WR08","/","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "WR08","/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "WR08","/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
#endregion
                            #region intro
                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "box","(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "box1","(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "box2","(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "box1","(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "box2","(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "box1","(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "box2","(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "go","(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            #endregion
                            #region intro
                            "sc","/","/","/","/",   "/","/","/","/",
                            "($0)($0)","/","/","/",   "/","/","($0)($0)","/",
                            "/","/","/","/",   "($0)($0)","/","/","/",
                            "/","/","($0)($0)","/",   "/","/","/","/",

                            "sc2","/","/","/","/",   "/","/","/","/",
                            "($2)($2)","/","/","/",   "/","/","($2)($2)","/",
                            "/","/","/","/",   "($2)($2)","/","/","/",
                            "($2)($2)","/","/","/",   "/","/","/","/",

                            "sc3","/","/","/","/",   "/","/","/","/",
                            "($0)($0)","/","/","/",   "/","/","($0)($0)","/",
                            "/","/","/","/",   "($0)($0)","/","/","/",
                            "/","/","($0)($0)","/",   "/","/","/","/",

                            "sc4","/","/","/","/",   "/","/","/","/",
                            "($2)($2)","/","/","/",   "/","/","($2)($2)","/",
                            "/","/","/","/",   "($2)($2)","/","/","/",
                            "($2)($2)","/","/","/",   "/","/","/","/",

                            "($0)($0)(R1)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($0)","/","/","/",
                            "($0)($0)(R1)","/","/","/",   "/","/","($0)($2)","/",
                            "/","/","/","/",   "($0)($0)","/","/","/",

                             "($0)($0)(R1)","/","/","/",   "/","/","($0)($0)","/",
                            "/","/","/","/",   "($0)($0)","/","/","/",
                            "($0)($0)(R1)","/","/","/",   "($0)($0)","/","/","/",
                            "($0)($0)","/","/","/",   "/","/","/","/",

                            "($0)($0)(R1)","/","/","/",   "($0)($0)","/","/","/",
                            "($0)($0)","/","/","/",   "($0)($0)","/","/","/",
                            "arrow","angle","$0","/","+0","/",   "+1","/","+0","/",
                            "+1","/","+0","/",   "+1","/","+0","/",

                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
#endregion
                            #region intro
                            "First","biu","R","/","/","/",   "biu","R","/","/","/",
                            "biu","$0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+2","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","+2","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+2","/","biu","+0","/",   "/","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","R","/","/","/",   "biu","R","/","/","/",
                            "biu","$0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+2","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","+2","/","biu","+0","/",   "/","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "R","/","R","/",   "R","/","R","/",
                             "/","/","/","/",   "/","/","/","/",

                            "Second","biu","R","/","/","/",   "biu","R","/","/","/",
                            "biu","$0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+2","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","+2","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+2","/","biu","+0","/",   "/","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","R","/","/","/",   "biu","R","/","/","/",
                            "biu","$0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+2","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "ret","R","+0","+0","+0",   "+0","/","/","/",
                            "R","+0","+0","+0",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "/","/","/","/",


                            #endregion
                            #region intro
                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
#endregion
                            #region intro
                            "First","biu","R","/","/","/",   "biu","R","/","/","/",
                            "biu","$0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+2","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","+2","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+2","/","biu","+0","/",   "/","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "biu","R","/","/","/",   "biu","R","/","/","/",
                            "biu","$0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+2","/","biu","+0","/",
                            "biu","+0","/","biu","+0","/",   "biu","+0","/","biu","+0","/",

                            "over","R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                             "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "(R)","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                             "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "(R)","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                             "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "R","/","+0","/",   "+0","/","+0","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",

                            "WR08","/","/","/","/",   "/","/","/","/",
                           "/","/","/","/",   "WR08","/","/","/","/",
                           "/","/","/","/",   "/","/","/","/",
                           "WR08","/","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "WR08","/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "WR08","/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",


#endregion
                            #region intro
                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                           "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","R","/",
                            "/","/","/","/",   "R","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "/","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            #endregion
                            #region final
                            "zan","/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",
                            "/","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "/","/","/","/",   "+0","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "/","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "(R)(+01)","/","/","/",   "/","/","(R)(+01)","/",
                            "/","/","/","/",   "(R)(+01)","/","/","/",
                            "(R)(+01)","/","/","/",   "/","/","(R)(+01)","/",
                            "/","/","/","/",   "(R)(+01)","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","+0","/",   "+0","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "/","/","/","/",   "+0","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","+0","/",
                            "/","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","+0","+0","+0",   "+0","/","/","/",
                            "R","+0","+0","+0",   "+0","/","/","/",
                            "(R)(+01)","/","/","/",   "/","/","(R)(+01)","/",
                            "/","/","/","/",   "(R)(+01)","/","/","/",

                             "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "(R1)","/","/","/",   "/","/","/","/",

                           "(R)(+01)","/","/","/",   "/","/","(R)(+01)","/",
                            "/","/","/","/",   "(R)(+01)","/","/","/",
                            "(R)(+01)","/","/","/",   "/","/","(R)(+01)","/",
                            "/","/","/","/",   "(R)(+01)","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",

                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "R","/","/","/",
                            "R","/","/","/",   "/","/","/","/",
                            "R","/","/","/",   "/","/","/","/",

                            "(R)(+21)","/","/","/",   "/","/","/","/",
#endregion
                        },
                        new string[] { "move", "sc", "sc2", "sc3", "sc4", "angle", "arrow", "box", "box1", "box2", "go", "First", "biu", "ret", "Second", "over", "zan" },
                        new Action[]
                        {
                            ()=>
                            {
                                ForBeat(2,()=>{
                                    DownBoundDistance= DownBoundDistance*0.95f+140*0.05f;
                        InstantSetBox((510+90) * 0.06f + BoxStates.Centre.Y * 0.94f, 84, 84);
                        InstantTP(320, (510+90) * 0.06f + Heart.Centre.Y * 0.94f);
                                });
                                DelayBeat(3,()=>{InstantSetBox(-90,84,84);InstantTP(320,-90); });
                                ForBeat(4,4,()=>{
                                    DownBoundDistance= DownBoundDistance*0.95f+280*0.05f;
                        InstantSetBox(240 * 0.06f + BoxStates.Centre.Y * 0.94f, 84, 84);
                        InstantTP(320, 240 * 0.06f + Heart.Centre.Y * 0.94f);
                                });
                                ForBeat(8, 4, () =>
                                {
                                    DownBoundDistance=DownBoundDistance*0.95f+0*0.05f;
                                });
                            },
                            ()=>
                            {
                                SizeExpand(400,bpm*2);
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Red,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.Red,0.1f);
                                });
                                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(bpm*4),"$$$////WARNING////",new(0,170),new UndyneFight_Ex.Fight.TextAttribute[]{new UndyneFight_Ex.Fight.TextColorAttribute(new(255,176,4)),new UndyneFight_Ex.Fight.TextSizeAttribute(3f),new UndyneFight_Ex.Fight.TextSpeedAttribute(114) }));
                                ForBeat(2,2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Black,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                            },
                            ()=>
                            {
                                SizeExpand(400,bpm*2);
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Red,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.Red,0.1f);
                                });
                                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(bpm*4),"$$$///ENEMYYY///",new(0,170),new UndyneFight_Ex.Fight.TextAttribute[]{new UndyneFight_Ex.Fight.TextColorAttribute(new(255,176,4)),new UndyneFight_Ex.Fight.TextSizeAttribute(3f),new UndyneFight_Ex.Fight.TextSpeedAttribute(114) }));
                                ForBeat(2,2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Black,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                            },
                            ()=>
                            {
                                SizeExpand(400,bpm*2);
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Red,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.Red,0.1f);
                                });
                                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(bpm*4),"$$$///PROTECT///",new(0,170),new UndyneFight_Ex.Fight.TextAttribute[]{new UndyneFight_Ex.Fight.TextColorAttribute(new(255, 171, 1)),new UndyneFight_Ex.Fight.TextSizeAttribute(3f),new UndyneFight_Ex.Fight.TextSpeedAttribute(114) }));
                                ForBeat(2,2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,Color.Black,0.1f);
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                            },
                            ()=>
                            {
                                ForBeat(2, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,new(3,20,4),0.1f);
                                    UIColor=Color.Lerp(UIColor,new(3,20,4),0.1f);
                                });
                                for(int a=0;a<13;a++)
                                {
                                    CreateTagLine(new Linerotate(40+a*50,240,90,bpm*16,0f,Color.Green){ width=2f,depth=0.01f},"A") ;
                                }
                                for(int a=0;a<9*2;a++)
                                {
                                    CreateTagLine(new Linerotate(320,480-15-a*50,00,bpm*16,0f,Color.Green){ width=2f,depth=0.01f},"B");
                                }
                                AddInstance(new TimeRangedEvent(2,1, () =>
                                {
                                    LineMoveLibrary.VecLinear("B",bpm*16,new(0,1));
                                    LineMoveLibrary.AlphaLerp("A",bpm*4,0.5f,0.08f);
                                    LineMoveLibrary.AlphaLerp("B",bpm*4,0.5f,0.08f);
                                }));
                            },
                            ()=>
                            {
                                LineMoveLibrary.AlphaLerp("A",bpm*8,0,0.1f);
                                LineMoveLibrary.AlphaLerp("B",bpm*8,0,0.1f);
                                ForBeat(2, () =>
                                {
                                    UIColor=Color.Lerp(UIColor,Color.White,0.1f);
                                });
                                for(int a=0;a<8;a++){DelayBeat(a*0.25f,()=>{GroundColor=new(255/8*a,0,0); }); }


                            },
                            ()=>
                            {
                                Arrow a =MakeArrow(bpm*2+3f,0,5,0,0);
                                Arrow b =MakeArrow(bpm*2+3f,2,5,1,0);
                                a.Tags=new string[]{ "a"};
                                b.Tags=new string[]{ "a"};
                                CreateEntity(a);
                                CreateEntity(b);
                                ForBeat(4, () =>
                                {
                                    GroundColor=Color.Lerp(GroundColor,new(0,0,0),0.1f);
                                    UIColor=Color.Lerp(UIColor,new(255,255,255),0.1f);
                                });
                                DelayBeat(2,()=>
                                {
                                    a.Delay(bpm*1+1);
                                    b.Delay(bpm*1+1);
                                });
                                DelayBeat(3,()=>
                                {
                                    a.Delay(bpm*1+1);
                                    b.Delay(bpm*1+1);
                                });
                                DelayBeat(4,()=>
                                {
                                    a.Delay(bpm*1-3);
                                    b.Delay(bpm*1-3);
                                });
                                DelayBeat(4.5f,()=>
                                {
                                    a.ResetColor(1);
                                    a.Speed=40;
                                    b.ResetColor(0);
                                    b.Speed=40;
                                });
                            },
                            ()=>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                InstantTP(-100,480-90);
                                InstantSetBox(new Vector2(-100,480-90),84,84);
                                LerpGreenBox(bpm*4,new(50,480-90),0.09f);
                            },
                            ()=>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                LerpGreenBox(bpm*4,new(640-50,480-90),0.09f);
                            },
                            ()=>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                LerpGreenBox(bpm*4,new(50,480-90),0.09f);
                            },
                            () =>
                            {
                                SetPlayerMission(1);SetBoxMission(1);
                                LerpGreenBox(bpm*4,new(800,480-90),0.09f);
                                DelayBeat(8,()=>{SetPlayerMission(0);SetBoxMission(0); });
                            },
                            () =>
                            {
                            ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.RGBSplitting());
                            AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                DownBoundDistance=DownBoundDistance*0.91f+300*0.09f;
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*10);
                             }));
                            },
                            ()=>
                            {
                                Linerotatee line = new(Rand(50,640-50),240,90+Rand(-35,35),26,1){ width=0,depth=0.001f };
                                float sin=-1;
                                AddInstance(new TimeRangedEvent(35
                                   , () =>
                                {
                                    line.width=42*sin*sin*sin*sin;
                                    sin+=1f/35f;
                                    if(line.width<=1)line.width=0;
                                }));
                                CreateEntity(line);
                            },
                            () =>
                            {
                                AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*0);
                             }));
                            },
                            () =>
                            {
                            AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*-10);
                             }));
                            },
                            () =>
                            {
                                AddInstance(new TimeRangedEvent(bpm*4, () =>
                            {
                                DownBoundDistance=DownBoundDistance*0.91f+0*0.09f;
                                Heart.InstantSetRotation(Heart.Rotation*0.9f+0.1f*0);
                             }));
                            },
                            ()=>
                            {
                                SizeExpand(40,bpm*4);
                            },

                        }
                        );
                }
            }


            public void Start()
            {
                //TP(160, 280); 
                //GametimeDelta = (int)(this.BeatTime(1320 + 16 - 8 + 7 * 16-10));
                GametimeDelta = -bpm * 4 - 3.5f;
                Heart.SoftFalling = true;
                SetBox(240 + 80, 84, 84);
                SetSoul(1);
                TP(320, 240 + 80);
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 1.5f;
                HeartAttribute.MaxHP = 32;
                GroundColor = Color.Black;
                //black.Parameters["acc"].SetValue(0f);
            }
            #region non
            public void Normal()
            {

            }

            public void ExtremePlus()
            {

            }

            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }
            #endregion
        }
    }
}