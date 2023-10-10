using Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static Extends.Someway;
using static Extends.LineMoveLibrary;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.CameraEffect;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.MathUtil;


namespace Rhythm_Recall.Waves
{
    public class Letsgonow : IChampionShip
    {
        public Letsgonow()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new()
            {
                { "div.3", Difficulty.Noob },
                { "div.2", Difficulty.Normal },
                { "div.1", Difficulty.Extreme },
                { "DIV.0", Difficulty.ExtremePlus }
            };
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
                public override string SongAuthor => "U-ske";
                public override string PaintAuthor => "Mikaze-maruto";
                public override string AttributeAuthor => "Tlottgodinf";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 19.3f),//Unrating, 19.3
                            new(Difficulty.Extreme, 17.0f),
                            new(Difficulty.Normal, 12.0f),
                            new(Difficulty.Noob, 2.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 19.6f),//Unrating, 19.6
                            new(Difficulty.Extreme, 17.2f),
                            new(Difficulty.Normal, 12.0f),
                            new(Difficulty.Noob, 2.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21.0f),
                            new(Difficulty.Extreme, 20.0f),
                            new(Difficulty.Normal, 15.0f),
                            new(Difficulty.Noob, 5.0f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();
            public Game() : base(62.5f / (188 / 60f)) { }
            public static Game instance;

            public string Music => "Let's go now";
            public string FightName => "Let's go now!";
            public static float bpm = 62.5f / (188 / 60f);

            #region Non
            public void ExtremePlus()
            {

                //CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { Sound = false });
                //CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Arrows:" + "$" + (GetAll<Arrow>().Length).ToString(), new(0, 240-20), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { Sound = false });
                if (InBeat(-2, 4)) InstantSetBox(240, BoxStates.Width * 0.98f + 84 * 3 * 0.02f, BoxStates.Height * 0.98f + 84 * 2 * 0.02f);
                if (InBeat(0)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 2,
                     new string[]
                     {
                    #region intro
                    "Bone1","/","/","Bone2","/","/",   "Bone1","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Box","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "Bone3","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Back","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",

                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",
                    "ready1","R1","/","/","/",   "R","/","+0","/",
                    "R","/","+0","/",   "R1","/","+01","/",

                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",

                    "ready2","R1","/","/","/",   "R1","/","+01","/",
                    "R","/","+0","/",   "R1","/","+01","/",
                    "ready1","R1","/","/","/",   "R","/","+0","/",
                    "R","/","+0","/",   "R1","/","+01","/",

                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",

                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",
                    "ready2","R1","/","/","/",   "R1","/","+01","/",
                    "R","/","+0","/",   "R1","/","+01","/",

                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",

                    "ready1","(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "Black","R","+0","+0","+0",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "R","/","+0","/",
                    "D1","/","+01","/",   "(R)(R1)","/","/","/",

                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)(R1)","/","/","/",

                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)","/","/","/",   "(R)(R1)","/","R","/",
                    "+0","/","/","/",   "(R)(R1)","/","+01","/",
                    "(R1)(R)","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "b1","/","/","/","/",
                    "b2","/","/","b2","/","/",   "b2","/","/","b2","/","/",
                    "b3","/","/","/","/",   "b3","/","/","/","/",
                    "b4","/","/","/","/",   "soul2","b4","/","/","/","/",

                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "R","/","+0","/",
                    "D1","/","+01","/",   "(R)(R1)","/","/","/",

                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)(R1)","/","/","/",

                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)","/","/","/",   "(R)(R1)","/","R","/",
                    "+0","/","/","/",   "(R)(R1)","/","+01","/",
                    "(R1)(R)","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "b5","/","/","/","/",
                    "b5","/","/","b5","/","/",   "b5","/","/","b5","/","/",
                    "soul2","/","/","/","/",   "?","($01)($01)","/","/","/",
                    "($01)($01)","/","/","/",   "($01)($01)","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "back","(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",   "+01","/","/","/",

                    "(R1)","/","/","/",   "+01","/","/","/",
                    "(R1)","/","/","/",   "+01","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt1","(R1)","/","/","/",
                    "+01","/","/","/",   "","/","/","/",
                    "R1","/","/","/",   "+01","/","/","/",
                    "+01","/","/","/",   "+01","/","/","/",

                    "+01","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",  "+01","/","/","/",
                    "(R1)","/","/","/",  "+01","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt2","/","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "+01","/","/","/",   "/","/","/","/",

                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "/","/","/","/",   "rt1","(R1)","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "+01","/","/","/",   "+01","/","/","/",

                     "/","/","(R1)(+01)","/",  "/","/","(R1)(+01)","/",
                     "/","/","(R1)(+01)","/",   "/","/","/","/",
                     "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",
                     "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",

                     "sc","(R1)(+01)","/","/","/",   "($01)","/","/","/",
                     "($01)","/","/","/",   "($01)","/","/","/",
                     "($01)","/","/","/",    "3<($01)","/","/","3<($01)","/","/","3<($01)","/","/",
                     "($01)","/","/","/",

                     "sc2","/","/","/","/",


                         #endregion
                     },
                     new string[] {
                    "Box","Bone1", "Bone2", "Bone3","Back","shrink1", "shrink2","Black","ready1", "ready2","soul","b1","b2","b3","b4", "soul2", "b5","?","back","sc", "sc2" ,
                    "rt1","rt2"
                     },
                     new Action[]
                     {
                    #region intro
                    ()=>{ ForBeat(4,()=>{InstantSetBox(240,BoxStates.Width*0.975f+84*2f*0.025f,BoxStates.Height*0.975f+97*1.5f*0.025f); }); },
                    ()=>{PlaySound(pierce); DownBone b;CreateBone(b = new(true,0,20){ ColorType=1});UpBone c;CreateBone(c = new(false,0,20){ ColorType=1}); ForBeat(5,()=>{b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f; c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);DownBone b; CreateBone(b = new(true, 0, 20) { ColorType = 2 }); UpBone c; CreateBone(c = new(false, 0, 20) { ColorType = 2 }); ForBeat(5, () => {b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f;c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);LeftBone b; CreateBone(b = new(true, 0, 84*1.5f-19)); RightBone c; CreateBone(c = new(false, 0, 84*1.5f-19)); ForBeat(5, () => {c.Speed=c.Speed*0.96f+5.5f*0.04f; b.Speed=b.Speed*0.96f+5.5f*0.04f; }); },
                    ()=>{SetSoul(1); TP();SetGreenBox();SizeShrink(7,bpm*4);RotateSymmetricBack(bpm*4,12); },
                    ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{MaskSquare maskSquare=new(0,0,640,480,(int)(bpm*0.5f+1),Color.Black,1);CreateEntity(maskSquare);float sin=90; ForBeat(0.5f,()=>{maskSquare.alpha=Sin(sin);sin+=180; }); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{ SetSoul(0);PlaySound(Ding); },
                    ()=>{ CreateBone(new LeftBone(true,5,84-36)); PlaySound(pierce);},
                    ()=>{ CreateBone(new RightBone(false,5,84-36));PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(false,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(true,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ SetSoul(1);TP(); PlaySound(Ding); },
                    ()=>{ CreateBone(new DownBone(true,4,84/2-16));CreateBone(new LeftBone(true,4,84/2-16));CreateBone(new UpBone(false,4,84/2-16));CreateBone(new RightBone(false,4,84/2-16));PlaySound(pierce); },
                    ()=>{if(Rand(0,1)==0)Rotate(15,bpm*1.5f);else Rotate(-15,bpm*1.5f);},
                    ()=>{RotateTo(0,bpm*2.5f); },
                    ()=>{LerpScreenScale(bpm*4.5f,2f,0.015f); },
                    ()=>{LerpScreenScale(bpm*4.5f,1f,0.075f); },
#endregion
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,false); },
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,true); },
                     }
                     );
                if (InBeat(0)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 2,
                     new string[]
                     {
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","R","/",   "D","/","+0","/",

                    "+0","/","+0","/",  "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","+0","/",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","+0","/",   "+0","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "D","/","","/",

                         #endregion
                     },
                     new string[] { },
                     new Action[]
                     {

         }
                     );
                if (InBeat(8 * 4 * 3 + 16 + 4)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 4,
                            new string[]
                            {
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "dl","/","/","/","/",   "R(R1)","/","/","/",

                    "/","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",    "/","/","/","/",
                    "R(R1)","/","/","/",    "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "($0)($2)($11)($31)","/","/","/",

                    "/","/","/","/",   "rok","R(R1)","/","/","/",
                    "/","/","/","/",   "($0)($2)($11)($31)","/","/","/",
                    "/","/","/","/",   "rok2","R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "back","R(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "/","/","/","/",
                    "($0)($21)","/","/","/",   "($0)($21)","/","/","/",
#endregion
                    #region intro
                    "Ef1","($0)($21)","/","/","/",   "Ef2","($0)($21)","/","/","/",
                    "Ef3","/","/","/","/",   "Ef4","/","/","/","/",
                    "/","/","/","/",   "~R(R1)","/","/","/",
                    "dl","/","/","/","/",   "R(R1)","/","/","/",

                    "/","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",    "/","/","/","/",
                    "R(R1)","/","/","/",    "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "($0)($2)($11)($31)","/","/","/",

                    "/","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "rok","($0)($2)($11)($31)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R(R1)","/","/","/",   "rok2","R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "back","/","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "G014","G304","/","/","$3","/",
                    "$3","/","$3","/",   "R(R1)","/","/","/",

                    "R(R1)","/","/","/",   "rok","G104","G014","/","/","$01","/",
                    "+01","/","+01","/",   "R(R1)","/","/","/",
                    "R(R1)","/","/","/",   "rok2","G2112","G0012","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "/","/","/","/",   "rot","back","/","/","/","/",
                    "/","/","/","/",   "~R(R1)","/","/","/",
                    "+0","/","/","/",   "~R(R1)","/","/","/",
                    "/","/","/","/",   "r1","/","/","/","/",
                                #endregion
                            },
                            new string[] { "dl", "soul", "rok", "rok2", "back", "rot", "r1", "Ef1", "Ef2", "Ef3", "Ef4", },
                            new Action[]
                            {
                    ()=>{
                        Arrow[] ar=GetAll<Arrow>();for(int a=0;a<ar.Length;a++){ int x=a;ar[x].Delay(bpm); };
                            GreenSoulGB[] gb=GetAll<GreenSoulGB>();for(int a=0;a<gb.Length;a++){int x=a;gb[x].Delay(bpm); }
                        },
                    () =>
                    {
                        SetSoul(2);SetBox(240,40,200);PlaySound(Ding);
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*2+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                            }
                                    );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, 0,
                                new string[]
                                {
                               #region intro
                    "soul1","b1","/","/","/","/",   "b2","/","/","/","/",
                    "b3","/","/","/","/",   "b4","/","/","/","/",
                    "/","/","/","/",   "b6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "b5","/","/","b5","/","/",   "b7","b5","/","/","/","/",
                    "b5","/","/","/","/",   "b5","/","/","/","/",
                    "b5","/","/","/","/",   "b5","/","/","/","/",
                     "/","/","/","/",

                    "soul2","a1","/","/","/","/",   "a2","/","/","/","/",
                    "a3","/","/","/","/",   "a4","/","/","/","/",
                    "/","/","/","/",   "a6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "a5","/","/","a5","/","/",   "a7","a5","/","/","/","/",
                    "a5","/","/","/","/",   "a5","/","/","/","/",
                    "a5","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "b6","/","/","/","/",   "/","/","/","/",
                                    #endregion

                                },
                                new string[] { "soul1", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "soul2", "a1", "a2", "a3", "a4", "a5", "a6", "a7", },
                                new Action[]
                                {
                    () => { SetSoul(2);SetBox(240,60,200);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,25)); CreateBone(new UpBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,50)); CreateBone(new UpBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,75)); CreateBone(new UpBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,100)); CreateBone(new UpBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,60)); CreateBone(new UpBone(false,3,60));CreateBone(new DownBone(true,3,60)); CreateBone(new DownBone(false,3,60)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre+new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,-5 }}); },
                    () => { SetSoul(2);Heart.GiveForce(180,0); SetBox(240,36,180);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,25)); CreateBone(new DownBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,50)); CreateBone(new DownBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,75)); CreateBone(new DownBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,100)); CreateBone(new DownBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,60)); CreateBone(new UpBone(false,3,60));CreateBone(new DownBone(true,3,60)); CreateBone(new DownBone(false,3,60)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre-new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,5 }}); },
                                }
                                        );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 20,
                               new string[]
                               {
                               #region intro

                               "soul","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "pos1","R(R1)","/","/","/",   "R(R1)","/","/","/",
                               "R","/","+0","/",   "R1","/","/","/",
                               "pos2","R(R1)","/","/","/",   "R(R1)","/","/","/",
                               "R","/","+0","/",   "R1","/","/","/",

                               "pos1","R(R1)","/","/","/",   "R(R1)","/","/","/",
                               "R","/","+0","/",   "R1","/","/","/",
                               "pos2","R(R1)","/","/","/",   "(R1)(R)","/","+0","/",
                               "R1","/","+01","/",   "R","/","+0","/",

                               "posr" ,"scr","R(R1)","/","/","/",   "/","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",

                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",

                               "(R)(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "soul1","/","/","/",

                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "b1","/","/","/","/",   "b1","/","/","/","/",
                               "b1","/","/","/","/",   "b1","/","/","/","/",
                               "b1","/","/","/","/","/","b1","/","/","/","/","/","b1","/","/","/","/",
                               "b1","soul2","/","/","/","/",

                               "/","/","/","/",   "Line","/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                                  "out","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "third","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "G3056",
                                   #endregion

                               },
                               new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "pos1", "pos2", "posr", "scr", "Line", "out", "third" },
                               new Action[]
                               {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); Vector2 vec1=Heart.Centre-new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec1,vec1,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); DownBone b=new(true,3,124){ ColorType=1};CreateBone(b);ForBeat(4,()=>{ b.Speed=b.Speed*0.95f+8*0.05f; }); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,-28),0.09f); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,28),0.09f); },
                    ()=>{LerpScreenPos(bpm*4f,new(0,0),0.075f); },
                    ()=>{float sin =0;ForBeat(12,()=>{sin+=360/bpm/12;Heart.InstantSetRotation(Sin(sin)*15); }); },
                               () =>
                               {
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"a");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"b");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"c");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"d");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"e");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"f");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"g");
                                   DelayBeat(1,()=>{
                                   LAlphaLerp("a",bpm*4,1,0.065f);
                                   LAlphaLerp("b",bpm*4,1,0.065f);
                                   LAlphaLerp("c",bpm*4,1,0.065f);
                                   LAlphaLerp("d",bpm*4,1,0.065f);
                                   LAlphaLerp("e",bpm*4,1,0.065f);
                                   LAlphaLerp("f",bpm*4,1,0.065f);
                                   LAlphaLerp("g",bpm*4,1,0.065f); });
                               },
                               () =>
                               {
                                   LerpScreenPos(bpm*4,new(0,115),0.086f);
                                   LRotLerp("a",bpm*8,180+90+50,0.05f);
                                   LRotLerp("b",bpm*8,180+90+25,0.05f);
                                   LRotLerp("c",bpm*8,180+90,0.05f);
                                   LRotLerp("d",bpm*8,180+90-25,0.05f);
                                   LRotLerp("e",bpm*8,180+90-50,0.05f);
                                   LRotLerp("f",bpm*8,180+90+75,0.05f);
                                   LRotLerp("g",bpm*8,180+90-75,0.05f);
                               },
                               () =>
                               {
                                   LVecLerp("a",bpm*8,new(320,660),0.02f);
                                   LVecLerp("b",bpm*8,new(320,660),0.02f);
                                   LVecLerp("c",bpm*8,new(320,660),0.02f);
                                   LVecLerp("d",bpm*8,new(320,660),0.02f);
                                   LVecLerp("e",bpm*8,new(320,660),0.02f);
                                   LVecLerp("f",bpm*8,new(320,660),0.02f);
                                   LVecLerp("g",bpm*8,new(320,660),0.02f);

                                   DelayBeat(4,()=>{
                                       LAlphaLerp("a",bpm*16,0,0.05f);
                                   LAlphaLerp("b",bpm*16,0,0.05f);
                                   LAlphaLerp("c",bpm*16,0,0.05f);
                                   LAlphaLerp("d",bpm*16,0,0.05f);
                                   LAlphaLerp("e",bpm*16,0,0.05f);
                                   LAlphaLerp("f",bpm*16,0,0.05f);
                                   LAlphaLerp("g",bpm*16,0,0.05f);
                                   Rotate(180*4,bpm*8); LerpScreenPos(bpm*8,new(0,0),0.05f);});
                                  DelayBeat(4,()=>{
                                   LRotLerp("a",bpm*8,180+90,0.09f);
                                   LRotLerp("b",bpm*8,180+90,0.09f);
                                   LRotLerp("c",bpm*8,180+90,0.09f);
                                   LRotLerp("d",bpm*8,180+90,0.09f);
                                   LRotLerp("e",bpm*8,180+90,0.09f);
                                   LRotLerp("f",bpm*8,180+90,0.09f);
                                   LRotLerp("g",bpm*8,180+90,0.09f);
                                   });
                               },
                               }
                                       );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 8 + 54)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 16,
                                 new string[]
                                 {
                               #region intro


                    "R1","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",

                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                    "(R)(R1)","/","/","/",   "/","/","/","/",
                    "Ef1","($0)($21)","/","/","/",  "Ef2","($0)($21)","/","/","/",
                    "Ef3","($0)($21)","/","/","/",   "Ef4","($0)($21)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R1","/","/","/",    "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "R1","/","/","/",

                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "rok","($0)($2)($11)($31)","/","/","/",
                    "/","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "rok2","($0)($2)($11)($31)","/","/","/",

                    "R(R1)","/","/","/",   "/","/","/","/",
                    "(R)(R1)","/","/","/",   "back","(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R1","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "(R)(R1)","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "rok","G214","G004","/","/","$0","/",    "+0","/","+0","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "rok2","G014","G204","/","/","$01","/",    "+01","/","+01","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "back","rot2","G0112","G3012","/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "rok","G114","G204","/","/","$2","/",    "+0","/","+0","/",
                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "rok2","G314","G004","/","/","$31","/",    "+01","/","+01","/",

                    "R(R1)","/","/","/",   "R(R1)","/","/","/",
                    "back","rot","G0016","G2116","!($0)($21)","/","/","/",    "sc1","@($0)($21)","/","/","/",
                    "sc2","!($0)($21)","/","/","/",   "sc3","@($0)($21)","/","/","/",
                    "sc4","~($0)($21)","/","/","/",   "/","/","/","/",

                    "sc5","/","/","/","/",

                                     #endregion
                                 },
                                 new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "rok", "rok2", "back", "rot", "rot2", "r1", "Ef1", "Ef2", "Ef3", "Ef4",
                           "sc1","sc2","sc3","sc4","sc5",
                                 },
                                 new Action[]
                                 {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-50,50),-Rand(140,180)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); CreateBone(new DownBone(true,6,124){ ColorType=1}); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },

                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*3+2); },
                    ()=>{ Rotate(-360,bpm*3+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                    ()=>{ ScreenScale=1.333f; },
                    ()=>{ ScreenScale=1.666f;},
                    ()=>{ ScreenScale=2f; },
                    ()=>{ ScreenScale=3f; },
                    ()=>{ LerpScreenScale(bpm*8,1,0.09f); },
                                 }
                                         );
            }
            #endregion

            public void Start()
            {
                GametimeDelta = -BeatTime(1.5f) - 2.0f;
                Heart.SoftFalling = true;
                InstantSetBox(240, 84, 84);
                SetSoul(0);
                InstantTP(320, 240);
                HeartAttribute.Speed = 3;
                HeartAttribute.MaxHP = 5;
                HeartAttribute.Gravity = 8.7f;
                UISettings.CreateUISurface();
            }
            int x = 0;
            public void Noob()
            {
                if (InBeat(-2, 4)) InstantSetBox(240, BoxStates.Width * 0.98f + 84 * 3 * 0.02f, BoxStates.Height * 0.98f + 84 * 2 * 0.02f);
                if (InBeat(0)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 2,
                     new string[]
                     {
                    #region intro
                    "Bone1","/","/","Bone2","/","/",   "Bone1","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Back","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "$0","/","/","/",   "shrink1","($0)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)","/","/","/",

                    "/","/","/","/",   "shrink1","($0)","/","/","/",
                    "/","/","/","/",   "($0)","/","/","/",
                    "ready1","D","/","/","/",   "+0","/","/","/",
                    "R","/","/","/",   "+0","/","/","/",

                    "+0","/","/","/",   "shrink2","($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($2)","/","/","/",
                    "/","/","/","/",   "($2)","/","/","/",

                    "ready2","R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                    "ready1","+0","/","/","/",   "R","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                    "+0","/","/","/",   "shrink2","($0)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)","/","/","/",

                    "/","/","/","/",   "shrink2","($0)","/","/","/",
                    "/","/","/","/",   "($0)","/","/","/",
                    "ready2","R","/","/","/",   "+0","/","/","/",
                    "R","/","/","/",   "+0","/","/","/",

                    "+0","/","/","/",   "shrink1","($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($2)","/","/","/",
                    "/","/","/","/",   "($2)","/","/","/",

                    "ready1","(R)","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "Black","R","+0","+0","+0",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "R","/","+0","/",
                    "+0","/","+0","/",   "/","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "R","/","+0","/",
                    "+0","/","+0","/",   "/","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","+0","/",
                    "+0","/","/","/",   "(+0)","/","+0","/",
                    "(+0)","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "soul2","/","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "R","/","+0","/",
                    "+0","/","+0","/",   "/","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "R","/","+0","/",
                    "+0","/","+0","/",   "/","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","+0","/",
                    "+0","/","/","/",   "(R)","/","+0","/",
                    "+0","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "soul2","/","/","/","/",   "?","($0)($0)","/","/","/",
                    "($0)($0)","/","/","/",   "($0)($0)","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "back","R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt1","(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",  "/","/","/","/",
                    "/","/","/","/",  "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt2","R","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "/","/","/","/",   "rt1","R","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "sc","/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",    "/","/","/","/",

                     "/","/","/","/",   "sc2","/","/","/","/",

                         #endregion
                     },
                     new string[] {
                    "Box","Bone1", "Bone2", "Bone3","Back","shrink1", "shrink2","Black","ready1", "ready2","soul","b1","b2","b3","b4", "soul2", "b5","?","back","sc", "sc2" ,
                    "rt1","rt2"
                     },
                     new Action[]
                     {

                    ()=>{ ForBeat(4,()=>{InstantSetBox(240,BoxStates.Width*0.975f+84*2f*0.025f,BoxStates.Height*0.975f+97*1.5f*0.025f); }); },
                    ()=>{PlaySound(pierce); DownBone b;CreateBone(b = new(true,0,20){ ColorType=1});UpBone c;CreateBone(c = new(false,0,20){ ColorType=1}); ForBeat(5,()=>{b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f; c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);DownBone b; CreateBone(b = new(true, 0, 20) { ColorType = 1 }); UpBone c; CreateBone(c = new(false, 0, 20) { ColorType = 1 }); ForBeat(5, () => {b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f;c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);LeftBone b; CreateBone(b = new(true, 0, 84*1.5f-30)); RightBone c; CreateBone(c = new(false, 0, 84*1.5f-30)); ForBeat(5, () => {c.Speed=c.Speed*0.96f+5.5f*0.04f; b.Speed=b.Speed*0.96f+5.5f*0.04f; }); },
                    ()=>{SetSoul(1); TP();SetGreenBox();SizeShrink(7,bpm*4);RotateSymmetricBack(bpm*4,12); },
                    ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{MaskSquare maskSquare=new(0,0,640,480,(int)(bpm*0.5f+1),Color.Black,1);CreateEntity(maskSquare);float sin=90; ForBeat(0.5f,()=>{maskSquare.alpha=Sin(sin);sin+=180; }); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{ SetSoul(0);PlaySound(Ding); },
                    ()=>{ CreateBone(new LeftBone(true,5,84-36)); PlaySound(pierce);},
                    ()=>{ CreateBone(new RightBone(false,5,84-36));PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(false,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(true,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ SetSoul(1);TP(); PlaySound(Ding); },
                    ()=>{ CreateBone(new DownBone(true,4,84/2-16));CreateBone(new LeftBone(true,4,84/2-16));CreateBone(new UpBone(false,4,84/2-16));CreateBone(new RightBone(false,4,84/2-16));PlaySound(pierce); },
                    ()=>{if(Rand(0,1)==0)Rotate(15,bpm*1.5f);else Rotate(-15,bpm*1.5f);},
                    ()=>{RotateTo(0,bpm*2.5f); },
                    ()=>{LerpScreenScale(bpm*4.5f,2f,0.015f); },
                    ()=>{LerpScreenScale(bpm*4.5f,1f,0.075f); },
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,false); },
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,true); },
                     }
                     );
                if (InBeat(8 * 4 * 3 + 16 + 4)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 4,
                            new string[]
                            {
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "dl","/","/","/","/",   "R","/","/","/",

                    "/","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",
                    "R","/","/","/",    "/","/","/","/",
                    "+0","/","/","/",    "+0","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",

                    "/","/","/","/",   "rok","R","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "rok2","R","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                    "+0","/","/","/",   "back","+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "/","/","/","/",
                    "($0)","/","/","/",   "($0)","/","/","/",
#endregion
                    #region intro
                    "Ef1","($0)","/","/","/",   "Ef2","($0)","/","/","/",
                    "Ef3","/","/","/","/",   "Ef4","/","/","/","/",
                    "/","/","/","/",   "~R","/","/","/",
                    "dl","/","/","/","/",   "+0","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "+0","/","/","/",    "/","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "rok","+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "rok2","+0","/","/","/",

                    "+0","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "back","/","/","/","/",
                    "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                     "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",
                     "R","/","/","/",   "+0","/","/","/",
                    "+0","/","/","/",   "+0","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",
                    "$3","/","/","/",   "$3","/","$3","/",
                    "$3","/","$3","/",   "$3","/","/","/",

                    "$1","/","/","/",   "rok","$1","/","$1","/",
                    "+0","/","+0","/",   "$1","/","/","/",
                    "R","/","/","/",   "rok2","R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "/","/","/","/",   "rot","back","/","/","/","/",
                    "/","/","/","/",   "~R","/","/","/",
                    "/","/","/","/",   "~+0","/","/","/",
                    "/","/","/","/",   "r1","/","/","/","/",
                                #endregion
                            },
                            new string[] { "dl", "soul", "rok", "rok2", "back", "rot", "r1", "Ef1", "Ef2", "Ef3", "Ef4", },
                            new Action[]
                            {
                    ()=>{
                        Arrow[] ar=GetAll<Arrow>();for(int a=0;a<ar.Length;a++){ int x=a;ar[x].Delay(bpm); };
                            GreenSoulGB[] gb=GetAll<GreenSoulGB>();for(int a=0;a<gb.Length;a++){int x=a;gb[x].Delay(bpm); }
                        },
                    () =>
                    {
                        SetSoul(2);SetBox(240,40,200);PlaySound(Ding);
                    },
                   ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f); },
                    ()=>{ Rotate(360,bpm*2+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                            }
                                    );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, 0,
                                new string[]
                                {
                               #region intro
                    "soul1","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "b6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",

                    "soul2","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "a6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "b6","/","/","/","/",   "/","/","/","/",
                                    #endregion

                                },
                                new string[] { "soul1", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "soul2", "a1", "a2", "a3", "a4", "a5", "a6", "a7", },
                                new Action[]
                                {
                    () => { SetSoul(2);SetBox(240,60,200);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,25)); CreateBone(new UpBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,50)); CreateBone(new UpBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,75)); CreateBone(new UpBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,100)); CreateBone(new UpBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,40)); CreateBone(new UpBone(false,3,40));CreateBone(new DownBone(true,3,40)); CreateBone(new DownBone(false,3,40)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre+new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,-5 }}); },
                    () => { SetSoul(2);Heart.GiveForce(180,0); SetBox(240,36,180);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,25)); CreateBone(new DownBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,50)); CreateBone(new DownBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,75)); CreateBone(new DownBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,100)); CreateBone(new DownBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,40)); CreateBone(new UpBone(false,3,40));CreateBone(new DownBone(true,3,40)); CreateBone(new DownBone(false,3,40)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre-new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,5 }}); },
                                }
                                        );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 20,
                               new string[]
                               {
                               #region intro

                               "soul","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "pos1","R","/","/","/",   "+0","/","/","/",
                               "+0","/","/","/",   "+0","/","/","/",
                               "pos2","R","/","/","/",   "+0","/","/","/",
                               "+0","/","/","/",   "+0","/","/","/",

                               "pos1","R","/","/","/",   "+0","/","/","/",
                               "+0","/","/","/",   "+0","/","/","/",
                               "pos2","R","/","/","/",   "(+0)","/","/","/",
                               "+0","/","/","/",   "+0","/","/","/",

                               "posr" ,"scr","R","/","/","/",   "/","/","/","/",
                               "(R)","/","/","/",   "(+0)","/","/","/",
                               "(+0)","/","/","/",   "(R)","/","/","/",
                               "(+0)","/","/","/",   "(+0)","/","/","/",

                               "(R)","/","/","/",   "(+0)","/","/","/",
                               "(+0)","/","/","/",   "(+0)","/","/","/",
                               "(R)","/","/","/",   "(+0)","/","/","/",
                               "(+0)","/","/","/",   "(+0)","/","/","/",

                               "(R)","/","/","/",   "(+0)","/","/","/",
                               "(+0)","/","/","/",   "(+0)","/","/","/",
                               "(R)","/","/","/",   "(+0)","/","/","/",
                               "(+0)","/","/","/",   "soul1","/","/","/",

                               "gb","/","/","/","/",   "gb","/","/","/","/",
                               "gb","/","/","/","/",   "gb","/","/","/","/",
                               "gb","/","/","/","/",   "gb","/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/", "/","/",
                               "/","/","/","/",   "soul2","/","/","/","/",

                               "/","/","b1","/","/",   "Line","/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                                  "out","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "third","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                              
                                   #endregion

                               },
                               new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "pos1", "pos2", "posr", "scr", "Line", "out", "third" },
                               new Action[]
                               {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); Vector2 vec1=Heart.Centre-new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec1,vec1,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); DownBone b=new(true,3,124){ ColorType=1};CreateBone(b);ForBeat(4,()=>{ b.Speed=b.Speed*0.95f+8*0.05f; }); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,-28),0.09f); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,28),0.09f); },
                    ()=>{LerpScreenPos(bpm*4f,new(0,0),0.075f); },
                    ()=>{float sin =0;ForBeat(12,()=>{sin+=360/bpm/12;Heart.InstantSetRotation(Sin(sin)*15); }); },
                               () =>
                               {
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"a");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"b");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"c");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"d");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"e");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"f");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"g");
                                   DelayBeat(1,()=>{
                                   LAlphaLerp("a",bpm*4,1,0.065f);
                                   LAlphaLerp("b",bpm*4,1,0.065f);
                                   LAlphaLerp("c",bpm*4,1,0.065f);
                                   LAlphaLerp("d",bpm*4,1,0.065f);
                                   LAlphaLerp("e",bpm*4,1,0.065f);
                                   LAlphaLerp("f",bpm*4,1,0.065f);
                                   LAlphaLerp("g",bpm*4,1,0.065f); });
                               },
                               () =>
                               {
                                   LerpScreenPos(bpm*4,new(0,115),0.086f);
                                   LRotLerp("a",bpm*8,180+90+50,0.05f);
                                   LRotLerp("b",bpm*8,180+90+25,0.05f);
                                   LRotLerp("c",bpm*8,180+90,0.05f);
                                   LRotLerp("d",bpm*8,180+90-25,0.05f);
                                   LRotLerp("e",bpm*8,180+90-50,0.05f);
                                   LRotLerp("f",bpm*8,180+90+75,0.05f);
                                   LRotLerp("g",bpm*8,180+90-75,0.05f);
                               },
                               () =>
                               {
                                   LVecLerp("a",bpm*8,new(320,660),0.02f);
                                   LVecLerp("b",bpm*8,new(320,660),0.02f);
                                   LVecLerp("c",bpm*8,new(320,660),0.02f);
                                   LVecLerp("d",bpm*8,new(320,660),0.02f);
                                   LVecLerp("e",bpm*8,new(320,660),0.02f);
                                   LVecLerp("f",bpm*8,new(320,660),0.02f);
                                   LVecLerp("g",bpm*8,new(320,660),0.02f);

                                   DelayBeat(4,()=>{
                                       LAlphaLerp("a",bpm*16,0,0.05f);
                                   LAlphaLerp("b",bpm*16,0,0.05f);
                                   LAlphaLerp("c",bpm*16,0,0.05f);
                                   LAlphaLerp("d",bpm*16,0,0.05f);
                                   LAlphaLerp("e",bpm*16,0,0.05f);
                                   LAlphaLerp("f",bpm*16,0,0.05f);
                                   LAlphaLerp("g",bpm*16,0,0.05f);
                                   Rotate(180*4,bpm*8); LerpScreenPos(bpm*8,new(0,0),0.05f);});
                                  DelayBeat(4,()=>{
                                   LRotLerp("a",bpm*8,180+90,0.09f);
                                   LRotLerp("b",bpm*8,180+90,0.09f);
                                   LRotLerp("c",bpm*8,180+90,0.09f);
                                   LRotLerp("d",bpm*8,180+90,0.09f);
                                   LRotLerp("e",bpm*8,180+90,0.09f);
                                   LRotLerp("f",bpm*8,180+90,0.09f);
                                   LRotLerp("g",bpm*8,180+90,0.09f);
                                   });
                               },
                               }
                                       );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 8 + 54)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 16,
                                 new string[]
                                 {
                               #region intro


                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",
                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",

                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",
                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "/","/","/","/",   "(R)","/","/","/",

                    "(+0)","/","/","/",   "(+0)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "(R)","/","/","/",   "(+0)","/","/","/",
                    "(R)","/","/","/",   "(+0)","/","/","/",

                    "(+0)","/","/","/",   "/","/","/","/",
                    "Ef1","($0)($21)","/","/","/",  "Ef2","($0)($21)","/","/","/",
                    "Ef3","($0)($21)","/","/","/",   "Ef4","($0)($21)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",    "(+0)","/","/","/",
                    "/","/","/","/",   "(+0)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "+0","/","/","/",

                    "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",
                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "/","/","/","/",   "rok","+0","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "rok2","+0","/","/","/",

                    "+0","/","/","/",   "/","/","/","/",
                    "(R)","/","/","/",   "back","(+0)","/","/","/",
                    "(+0)","/","/","/",   "(+0)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",
                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",

                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",   "(+0)","/","/","/",
                   "(R)","/","/","/",   "(+0)","/","/","/",
                   "(+0)","/","/","/",

                    "$0","/","/","/",   "$0","/","/","/",
                    "rok","R","/","/","/",    "/","/","/","/",
                    "$2","/","/","/",   "$2","/","/","/",
                    "rok2","R","/","/","/",    "/","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "back","rot2","+0","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",

                    "$0","/","/","/",   "$0","/","/","/",
                    "rok","R","/","/","/",    "/","/","/","/",
                    "$2","/","/","/",   "$2","/","/","/",
                    "rok2","R","/","/","/",    "/","/","/","/",

                    "R","/","/","/",   "+0","/","/","/",
                    "back","rot","G3016","/","/","/","/",    "sc1","/","/","/","/",
                    "sc2","/","/","/","/",   "sc3","/","/","/","/",
                    "sc4","/","/","/","/",   "/","/","/","/",

                    "sc5","/","/","/","/",

                                     #endregion
                                 },
                                 new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "rok", "rok2", "back", "rot", "rot2", "r1", "Ef1", "Ef2", "Ef3", "Ef4",
                           "sc1","sc2","sc3","sc4","sc5",
                                 },
                                 new Action[]
                                 {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-50,50),-Rand(140,180)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); CreateBone(new DownBone(true,6,124){ ColorType=1}); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },

                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*3+2); },
                    ()=>{ Rotate(-360,bpm*3+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                    ()=>{ ScreenScale=1.333f; },
                    ()=>{ ScreenScale=1.666f;},
                    ()=>{ ScreenScale=2f; },
                    ()=>{ ScreenScale=3f; },
                    ()=>{ LerpScreenScale(bpm*8,1,0.09f); },
                                 }
                                         );
            }

            public void Easy()
            {

            }

            public void Normal()
            {
                if (InBeat(-2, 4)) InstantSetBox(240, BoxStates.Width * 0.98f + 84 * 3 * 0.02f, BoxStates.Height * 0.98f + 84 * 2 * 0.02f);
                if (InBeat(0)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 2,
                     new string[]
                     {
                    #region intro
                    "Bone1","/","/","Bone2","/","/",   "Bone1","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Box","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "Bone3","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Back","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "R","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)($2)","/","/","/",

                    "/","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",
                    "ready1","R","/","/","/",   "R","/","+0","/",
                    "R","/","+0","/",   "R","/","+0","/",

                    "R","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",

                    "ready2","R","/","/","/",   "R","/","+0","/",
                    "+0","/","/","/",   "R","/","+0","/",
                    "ready1","+0","/","/","/",   "R","/","+0","/",
                    "+0","/","/","/",   "R","/","+0","/",

                    "+0","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink2","($0)($2)","/","/","/",

                    "/","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",
                    "ready2","R","/","/","/",   "R","/","+0","/",
                    "R","/","+0","/",   "R","/","+0","/",

                    "R","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",

                    "ready1","(R)","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "Black","R","+0","+0","+0",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","+0","/",
                    "+0","/","/","/",   "(R)","/","+0","/",
                    "(R)","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "b2","/","/","/","/",
                    "b2","/","/","b2","/","/",   "b2","/","/","b2","/","/",
                    "b3","/","/","/","/",   "b3","/","/","/","/",
                    "b4","/","/","/","/",   "soul2","b4","/","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","+0","/",
                    "+0","/","/","/",   "(R)","/","+0","/",
                    "R","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "b5","/","/","/","/",
                    "b5","/","/","b5","/","/",   "b5","/","/","b5","/","/",
                    "soul2","/","/","/","/",   "?","($01)($01)","/","/","/",
                    "($01)($01)","/","/","/",   "($01)($01)","/","/","/",
                    #endregion
                                        "/","/","/","/",   "back","(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",   "+01","/","/","/",

                    "(R1)","/","/","/",   "+01","/","/","/",
                    "(R1)","/","/","/",   "+01","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt1","(R1)","/","/","/",
                    "+01","/","/","/",   "","/","/","/",
                    "R1","/","/","/",   "+01","/","/","/",
                    "+01","/","/","/",   "+01","/","/","/",

                    "+01","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",  "+01","/","/","/",
                    "(R1)","/","/","/",  "+01","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt2","/","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "+01","/","/","/",   "/","/","/","/",

                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "/","/","/","/",   "rt1","(R1)","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "+01","/","/","/",   "+01","/","/","/",

                     "/","/","(R1)(+01)","/",  "/","/","(R1)(+01)","/",
                     "/","/","(R1)(+01)","/",   "/","/","/","/",
                     "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",
                     "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",

                     "sc","(R1)(+01)","/","/","/",   "($01)","/","/","/",
                     "($01)","/","/","/",   "($01)","/","/","/",
                     "($01)","/","/","/",    "3<($01)","/","/","3<($01)","/","/","3<($01)","/","/",
                     "($01)","/","/","/",    "/","/","/","/",

                     "sc2","/","/","/",
                     },
                     new string[] {
                    "Box","Bone1", "Bone2", "Bone3","Back","shrink1", "shrink2","Black","ready1", "ready2","soul","b1","b2","b3","b4", "soul2", "b5","?","back","sc", "sc2" ,
                    "rt1","rt2"
                     },
                     new Action[]
                     {
                    #region intro
                    ()=>{ ForBeat(4,()=>{InstantSetBox(240,BoxStates.Width*0.975f+84*2f*0.025f,BoxStates.Height*0.975f+97*1.5f*0.025f); }); },
                    ()=>{PlaySound(pierce); DownBone b;CreateBone(b = new(true,0,20){ ColorType=1});UpBone c;CreateBone(c = new(false,0,20){ ColorType=1}); ForBeat(5,()=>{b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f; c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);DownBone b; CreateBone(b = new(true, 0, 20) { ColorType = 1 }); UpBone c; CreateBone(c = new(false, 0, 20) { ColorType = 1 }); ForBeat(5, () => {b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f;c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);LeftBone b; CreateBone(b = new(true, 0, 84*1.5f-30)); RightBone c; CreateBone(c = new(false, 0, 84*1.5f-30)); ForBeat(5, () => {c.Speed=c.Speed*0.96f+5.5f*0.04f; b.Speed=b.Speed*0.96f+5.5f*0.04f; }); },
                    ()=>{SetSoul(1); TP();SetGreenBox();SizeShrink(7,bpm*4);RotateSymmetricBack(bpm*4,12); },
                    ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{MaskSquare maskSquare=new(0,0,640,480,(int)(bpm*0.5f+1),Color.Black,1);CreateEntity(maskSquare);float sin=90; ForBeat(0.5f,()=>{maskSquare.alpha=Sin(sin);sin+=180; }); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{ SetSoul(0);PlaySound(Ding); },
                    ()=>{ CreateBone(new LeftBone(true,5,84-36)); PlaySound(pierce);},
                    ()=>{ CreateBone(new RightBone(false,5,84-36));PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(false,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(true,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ SetSoul(1);TP(); PlaySound(Ding); },
                    ()=>{ CreateBone(new DownBone(true,4,84/2-16));CreateBone(new LeftBone(true,4,84/2-16));CreateBone(new UpBone(false,4,84/2-16));CreateBone(new RightBone(false,4,84/2-16));PlaySound(pierce); },
                    ()=>{if(Rand(0,1)==0)Rotate(15,bpm*1.5f);else Rotate(-15,bpm*1.5f);},
                    ()=>{RotateTo(0,bpm*2.5f); },
                    ()=>{LerpScreenScale(bpm*4.5f,2f,0.015f); },
                    ()=>{LerpScreenScale(bpm*4.5f,1f,0.075f); },
#endregion
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,false); },
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,true); },
                     }
                     );
                if (InBeat(8 * 4 * 3 + 16 + 4)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 4,
                            new string[]
                            {
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "dl","/","/","/","/",   "R","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",    "/","/","/","/",
                    "R","/","/","/",    "R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",

                    "/","/","/","/",   "rok","R","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",
                    "/","/","/","/",   "rok2","R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "R","/","/","/",   "back","R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "/","/","/","/",
                    "($0)($21)","/","/","/",   "($0)($21)","/","/","/",
#endregion
                    #region intro
                    "Ef1","($0)($21)","/","/","/",   "Ef2","($0)($21)","/","/","/",
                    "Ef3","/","/","/","/",   "Ef4","/","/","/","/",
                    "/","/","/","/",   "~R","/","/","/",
                    "dl","/","/","/","/",   "R","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",    "/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "rok","($0)($2)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "rok2","R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "back","/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "G304","/","/","$3","/",
                    "$3","/","$3","/",   "R","/","/","/",

                    "R","/","/","/",   "rok","G104","/","/","$1","/",
                    "+0","/","+0","/",   "R","/","/","/",
                    "R","/","/","/",   "rok2","G3012","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "/","/","/","/",   "rot","back","/","/","/","/",
                    "/","/","/","/",   "~R","/","/","/",
                    "+0","/","/","/",   "~R","/","/","/",
                    "/","/","/","/",   "r1","/","/","/","/",
                                #endregion
                            },
                            new string[] { "dl", "soul", "rok", "rok2", "back", "rot", "r1", "Ef1", "Ef2", "Ef3", "Ef4", },
                            new Action[]
                            {
                    ()=>{
                        Arrow[] ar=GetAll<Arrow>();for(int a=0;a<ar.Length;a++){ int x=a;ar[x].Delay(bpm); };
                            GreenSoulGB[] gb=GetAll<GreenSoulGB>();for(int a=0;a<gb.Length;a++){int x=a;gb[x].Delay(bpm); }
                        },
                    () =>
                    {
                        SetSoul(2);SetBox(240,40,200);PlaySound(Ding);
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*2+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                            }
                                    );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, 0,
                                new string[]
                                {
                               #region intro
                    "soul1","b1","/","/","/","/",   "b2","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "b6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "b5","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "b5","/","/","/","/",   "/","/","/","/",
                     "/","/","/","/",

                    "soul2","a1","/","/","/","/",   "a2","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "a6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "a5","/","/","/","/",   "a5","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "a5","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "b6","/","/","/","/",   "/","/","/","/",
                                    #endregion

                                },
                                new string[] { "soul1", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "soul2", "a1", "a2", "a3", "a4", "a5", "a6", "a7", },
                                new Action[]
                                {
                    () => { SetSoul(2);SetBox(240,60,200);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,25)); CreateBone(new UpBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,50)); CreateBone(new UpBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,75)); CreateBone(new UpBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,100)); CreateBone(new UpBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,50)); CreateBone(new UpBone(false,3,50));CreateBone(new DownBone(true,3,50)); CreateBone(new DownBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre+new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,-5 }}); },
                    () => { SetSoul(2);Heart.GiveForce(180,0); SetBox(240,36,180);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,25)); CreateBone(new DownBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,50)); CreateBone(new DownBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,75)); CreateBone(new DownBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,100)); CreateBone(new DownBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,50)); CreateBone(new UpBone(false,3,50));CreateBone(new DownBone(true,3,50)); CreateBone(new DownBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre-new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,5 }}); },
                                }
                                        );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 20,
                               new string[]
                               {
                               #region intro

                               "soul","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "pos1","R","/","/","/",   "R","/","/","/",
                               "R","/","+0","/",   "+0","/","/","/",
                               "pos2","R","/","/","/",   "R","/","/","/",
                               "R","/","+0","/",   "+0","/","/","/",

                               "pos1","R","/","/","/",   "R","/","/","/",
                               "R","/","+0","/",   "+0","/","/","/",
                               "pos2","R","/","/","/",   "(R)","/","+0","/",
                               "R","/","+0","/",   "R","/","+0","/",

                               "posr" ,"scr","R","/","/","/",   "/","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",

                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",

                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "(R)","/","/","/",
                               "(R)","/","/","/",   "soul1","/","/","/",

                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "b1","/","/","/","/",   "b1","/","/","/","/",
                               "b1","/","/","/","/",   "b1","/","/","/","/",
                               "b1","/","/","/","/",   "/","/", "b1","/","/",
                               "/","/","/","/",   "b1","soul2","/","/","/","/",

                               "/","/","b1","/","/",   "Line","/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                                  "out","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "third","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "G3056",
                                   #endregion

                               },
                               new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "pos1", "pos2", "posr", "scr", "Line", "out", "third" },
                               new Action[]
                               {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); Vector2 vec1=Heart.Centre-new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec1,vec1,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); DownBone b=new(true,3,124){ ColorType=1};CreateBone(b);ForBeat(4,()=>{ b.Speed=b.Speed*0.95f+8*0.05f; }); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,-28),0.09f); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,28),0.09f); },
                    ()=>{LerpScreenPos(bpm*4f,new(0,0),0.075f); },
                    ()=>{float sin =0;ForBeat(12,()=>{sin+=360/bpm/12;Heart.InstantSetRotation(Sin(sin)*15); }); },
                               () =>
                               {
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"a");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"b");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"c");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"d");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"e");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"f");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"g");
                                   DelayBeat(1,()=>{
                                   LAlphaLerp("a",bpm*4,1,0.065f);
                                   LAlphaLerp("b",bpm*4,1,0.065f);
                                   LAlphaLerp("c",bpm*4,1,0.065f);
                                   LAlphaLerp("d",bpm*4,1,0.065f);
                                   LAlphaLerp("e",bpm*4,1,0.065f);
                                   LAlphaLerp("f",bpm*4,1,0.065f);
                                   LAlphaLerp("g",bpm*4,1,0.065f); });
                               },
                               () =>
                               {
                                   LerpScreenPos(bpm*4,new(0,115),0.086f);
                                   LRotLerp("a",bpm*8,180+90+50,0.05f);
                                   LRotLerp("b",bpm*8,180+90+25,0.05f);
                                   LRotLerp("c",bpm*8,180+90,0.05f);
                                   LRotLerp("d",bpm*8,180+90-25,0.05f);
                                   LRotLerp("e",bpm*8,180+90-50,0.05f);
                                   LRotLerp("f",bpm*8,180+90+75,0.05f);
                                   LRotLerp("g",bpm*8,180+90-75,0.05f);
                               },
                               () =>
                               {
                                   LVecLerp("a",bpm*8,new(320,660),0.02f);
                                   LVecLerp("b",bpm*8,new(320,660),0.02f);
                                   LVecLerp("c",bpm*8,new(320,660),0.02f);
                                   LVecLerp("d",bpm*8,new(320,660),0.02f);
                                   LVecLerp("e",bpm*8,new(320,660),0.02f);
                                   LVecLerp("f",bpm*8,new(320,660),0.02f);
                                   LVecLerp("g",bpm*8,new(320,660),0.02f);

                                   DelayBeat(4,()=>{
                                       LAlphaLerp("a",bpm*16,0,0.05f);
                                   LAlphaLerp("b",bpm*16,0,0.05f);
                                   LAlphaLerp("c",bpm*16,0,0.05f);
                                   LAlphaLerp("d",bpm*16,0,0.05f);
                                   LAlphaLerp("e",bpm*16,0,0.05f);
                                   LAlphaLerp("f",bpm*16,0,0.05f);
                                   LAlphaLerp("g",bpm*16,0,0.05f);
                                   Rotate(180*4,bpm*8); LerpScreenPos(bpm*8,new(0,0),0.05f);});
                                  DelayBeat(4,()=>{
                                   LRotLerp("a",bpm*8,180+90,0.09f);
                                   LRotLerp("b",bpm*8,180+90,0.09f);
                                   LRotLerp("c",bpm*8,180+90,0.09f);
                                   LRotLerp("d",bpm*8,180+90,0.09f);
                                   LRotLerp("e",bpm*8,180+90,0.09f);
                                   LRotLerp("f",bpm*8,180+90,0.09f);
                                   LRotLerp("g",bpm*8,180+90,0.09f);
                                   });
                               },
                               }
                                       );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 8 + 54)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 16,
                                 new string[]
                                 {
                               #region intro


                    "R","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",

                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",

                    "(R)","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",

                    "(R)","/","/","/",   "/","/","/","/",
                    "Ef1","($0)($21)","/","/","/",  "Ef2","($0)($21)","/","/","/",
                    "Ef3","($0)($21)","/","/","/",   "Ef4","($0)($21)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",    "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "R","/","/","/",

                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "rok","($0)($2)","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "rok2","($0)($2)","/","/","/",

                    "R","/","/","/",   "/","/","/","/",
                    "(R)","/","/","/",   "back","(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",

                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "rok","G004","/","/","$0","/",    "+0","/","+0","/",
                    "R","/","/","/",   "R","/","/","/",
                    "rok2","G204","/","/","$2","/",    "+0","/","+0","/",

                    "R","/","/","/",   "R","/","/","/",
                    "back","rot2","G3012","/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "rok","G204","/","/","$2","/",    "+0","/","+0","/",
                    "R","/","/","/",   "R","/","/","/",
                    "rok2","G004","/","/","$0","/",    "+0","/","+0","/",

                    "R","/","/","/",   "R","/","/","/",
                    "back","rot","G0016","G2116","!($0)($21)","/","/","/",    "sc1","@($0)($21)","/","/","/",
                    "sc2","!($0)($21)","/","/","/",   "sc3","@($0)($21)","/","/","/",
                    "sc4","~($0)($21)","/","/","/",   "/","/","/","/",

                    "sc5","/","/","/","/",

                                     #endregion
                                 },
                                 new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "rok", "rok2", "back", "rot", "rot2", "r1", "Ef1", "Ef2", "Ef3", "Ef4",
                           "sc1","sc2","sc3","sc4","sc5",
                                 },
                                 new Action[]
                                 {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-50,50),-Rand(140,180)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); CreateBone(new DownBone(true,6,124){ ColorType=1}); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },

                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*3+2); },
                    ()=>{ Rotate(-360,bpm*3+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                    ()=>{ ScreenScale=1.333f; },
                    ()=>{ ScreenScale=1.666f;},
                    ()=>{ ScreenScale=2f; },
                    ()=>{ ScreenScale=3f; },
                    ()=>{ LerpScreenScale(bpm*8,1,0.09f); },
                                 }
                                         );
            }

            public void Hard()
            {

            }

            public void Extreme()
            {

                //CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { Sound = false });
                //CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Arrows:" + "$" + (GetAll<Arrow>().Length).ToString(), new(0, 240-20), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { Sound = false });
                if (InBeat(-2, 4)) InstantSetBox(240, BoxStates.Width * 0.98f + 84 * 3 * 0.02f, BoxStates.Height * 0.98f + 84 * 2 * 0.02f);
                if (InBeat(0)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 2,
                     new string[]
                     {
                    #region intro
                    "Bone1","/","/","Bone1","/","/",   "Bone1","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Box","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "Bone3","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "Back","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",

                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",
                    "ready1","R1","/","/","/",   "R","/","+0","/",
                    "R","/","+0","/",   "R1","/","+01","/",

                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",

                    "ready2","R1","/","/","/",   "R1","/","+01","/",
                    "R","/","+0","/",   "R1","/","+01","/",
                    "ready1","R1","/","/","/",   "R","/","+0","/",
                    "R","/","+0","/",   "R1","/","+01","/",

                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",

                    "R1","/","/","/",   "shrink2","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",
                    "ready2","R1","/","/","/",   "R1","/","+01","/",
                    "R","/","+0","/",   "R1","/","+01","/",

                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "shrink1","($0)($2)","/","/","/",
                    "R1","/","/","/",   "($0)($2)","/","/","/",

                    "ready1","(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "Black","R","+0","+0","+0",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "R","/","+0","/",
                    "D1","/","+01","/",   "(R)(R1)","/","/","/",

                    "/","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R1)","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","R","/",
                    "+0","/","/","/",   "(R)","/","R1","/",
                    "+01","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "b1","/","/","/","/",
                    "b2","/","/","b2","/","/",   "b2","/","/","b2","/","/",
                    "b3","/","/","/","/",   "b3","/","/","/","/",
                    "b4","/","/","/","/",   "soul2","b4","/","/","/","/",

                    "/","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "(R)(R1)","/","/","/",   "R","/","+0","/",
                    "D1","/","+01","/",   "(R)","/","/","/",

                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","+0","/",
                    "D","/","+0","/",   "(R)","/","/","/",

                    "/","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",   "(R1)","/","R","/",
                    "+0","/","/","/",   "(R1)","/","+01","/",
                    "R","/","+0","/",   "soul","/","/","/","/",

                    "/","/","/","/",   "b5","/","/","/","/",
                    "b5","/","/","b5","/","/",   "b5","/","/","b5","/","/",
                    "soul2","/","/","/","/",   "?","($01)($01)","/","/","/",
                    "($01)($01)","/","/","/",   "($01)($01)","/","/","/",
                    #endregion
                    #region intro
                                        "/","/","/","/",   "back","(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",   "+01","/","/","/",

                    "(R1)","/","/","/",   "+01","/","/","/",
                    "(R1)","/","/","/",   "+01","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt1","(R1)","/","/","/",
                    "+01","/","/","/",   "","/","/","/",
                    "R1","/","/","/",   "+01","/","/","/",
                    "+01","/","/","/",   "+01","/","/","/",

                    "+01","/","/","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",  "+01","/","/","/",
                    "(R1)","/","/","/",  "+01","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "rt2","/","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "+01","/","/","/",   "/","/","/","/",

                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "/","/","/","/",   "/","/","/","/",

                     "/","/","/","/",   "rt1","(R1)","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "(R1)","/","/","/",   "+01","/","/","/",
                     "+01","/","/","/",   "+01","/","/","/",

                     "/","/","(R1)(+01)","/",  "/","/","(R1)(+01)","/",
                     "/","/","(R1)(+01)","/",   "/","/","/","/",
                     "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",
                     "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",

                     "sc","(R1)(+01)","/","/","/",   "($01)","/","/","/",
                     "($01)","/","/","/",   "($01)","/","/","/",
                     "($01)","/","/","/",    "3<($01)","/","/","3<($01)","/","/","3<($01)","/","/",
                     "($01)","/","/","/","/","/","/","/", 
                     "sc2","/","/","/",

                         #endregion
                     },
                     new string[] {
                    "Box","Bone1", "Bone2", "Bone3","Back","shrink1", "shrink2","Black","ready1", "ready2","soul","b1","b2","b3","b4", "soul2", "b5","?","back","sc", "sc2" ,
                    "rt1","rt2"
                     },
                     new Action[]
                     {
                    #region intro
                    ()=>{ ForBeat(4,()=>{InstantSetBox(240,BoxStates.Width*0.975f+84*2f*0.025f,BoxStates.Height*0.975f+97*1.5f*0.025f); }); },
                    ()=>{PlaySound(pierce); DownBone b;CreateBone(b = new(true,0,20){ ColorType=1});UpBone c;CreateBone(c = new(false,0,20){ ColorType=1}); ForBeat(5,()=>{b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f; c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);DownBone b; CreateBone(b = new(true, 0, 20) { ColorType = 2 }); UpBone c; CreateBone(c = new(false, 0, 20) { ColorType = 2 }); ForBeat(5, () => {b.MissionLength=b.MissionLength*0.96f+84*2*0.04f; c.MissionLength=c.MissionLength*0.96f+84*2*0.04f;c.Speed=c.Speed*0.96f+7f*0.04f; b.Speed=b.Speed*0.96f+7f*0.04f; }); },
                    ()=>{PlaySound(pierce);LeftBone b; CreateBone(b = new(true, 0, 84*1.5f-19)); RightBone c; CreateBone(c = new(false, 0, 84*1.5f-19)); ForBeat(5, () => {c.Speed=c.Speed*0.96f+5.5f*0.04f; b.Speed=b.Speed*0.96f+5.5f*0.04f; }); },
                    ()=>{SetSoul(1); TP();SetGreenBox();SizeShrink(7,bpm*4);RotateSymmetricBack(bpm*4,12); },
                    ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{MaskSquare maskSquare=new(0,0,640,480,(int)(bpm*0.5f+1),Color.Black,1);CreateEntity(maskSquare);float sin=90; ForBeat(0.5f,()=>{maskSquare.alpha=Sin(sin);sin+=180; }); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{ SetSoul(0);PlaySound(Ding); },
                    ()=>{ CreateBone(new LeftBone(true,5,84-36)); PlaySound(pierce);},
                    ()=>{ CreateBone(new RightBone(false,5,84-36));PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(false,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ CreateBone(new DownBone(true,4,84-5){ ColorType=1});PlaySound(pierce); },
                    ()=>{ SetSoul(1);TP(); PlaySound(Ding); },
                    ()=>{ CreateBone(new DownBone(true,4,84/2-16));CreateBone(new LeftBone(true,4,84/2-16));CreateBone(new UpBone(false,4,84/2-16));CreateBone(new RightBone(false,4,84/2-16));PlaySound(pierce); },
                    ()=>{if(Rand(0,1)==0)Rotate(15,bpm*1.5f);else Rotate(-15,bpm*1.5f);},
                    ()=>{RotateTo(0,bpm*2.5f); },
                    ()=>{LerpScreenScale(bpm*4.5f,2f,0.015f); },
                    ()=>{LerpScreenScale(bpm*4.5f,1f,0.075f); },
#endregion
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,false); },
                    ()=>{ SizeShrink(6,bpm*4);Convulse(13,bpm*6,true); },
                     }
                     );
                if (InBeat(0)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 2,
                     new string[]
                     {
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    #endregion
                    #region intro
                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","R","/",   "D","/","+0","/",

                    "+0","/","+0","/",  "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","+0","/",   "+0","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R","/","+0","/",   "+0","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "D","/","","/",

                         #endregion
                     },
                     new string[] { },
                     new Action[]
                     {

         }
                     );
                if (InBeat(8 * 4 * 3 + 16 + 4)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 4,
                            new string[]
                            {
                    #region intro
                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "dl","/","/","/","/",   "R(R1)","/","/","/",

                    "/","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",    "/","/","/","/",
                    "(R1)","/","/","/",    "R","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "($0)($2)","/","/","/",

                    "/","/","/","/",   "rok","R","/","/","/",
                    "/","/","/","/",   "($11)($31)","/","/","/",
                    "/","/","/","/",   "rok2","(R1)","/","/","/",
                    "R(R1)","/","/","/",   "R","/","/","/",

                    "R(R1)","/","/","/",   "back","R","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",

                    "R","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",

                    "R","/","/","/",   "R1","/","/","/",
                    "/","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "/","/","/","/",
                    "($0)($21)","/","/","/",   "($0)($21)","/","/","/",
#endregion
                    #region intro
                    "Ef1","($0)($21)","/","/","/",   "Ef2","($0)($21)","/","/","/",
                    "Ef3","/","/","/","/",   "Ef4","/","/","/","/",
                    "/","/","/","/",   "~R(R1)","/","/","/",
                    "dl","/","/","/","/",   "(R1)","/","/","/",

                    "/","/","/","/",   "(R1)","/","/","/",
                    "R(R1)","/","/","/",    "/","/","/","/",
                    "R","/","/","/",    "R1","/","/","/",

                    "R","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",
                    "R","/","/","/",   "R1","/","/","/",
                    "/","/","/","/",   "($1)($3)","/","/","/",

                    "/","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "rok","($01)($21)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "(R1)","/","/","/",   "rok2","R","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "back","/","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "(R1)","/","/","/",   "R","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "/","/","/","/",   "R","/","/","/",
                    "R","/","/","/",   "G014","G304","/","/","$3","/",
                    "$3","/","$3","/",   "R","/","/","/",

                    "(R1)","/","/","/",   "rok","G104","G014","/","/","$01","/",
                    "+01","/","+01","/",   "(R1)","/","/","/",
                    "(R1)","/","/","/",   "rok2","G2112","G0012","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
#endregion
                    #region intro
                    "/","/","/","/",   "rot","back","/","/","/","/",
                    "/","/","/","/",   "~R(R1)","/","/","/",
                    "+0","/","/","/",   "~R(R1)","/","/","/",
                    "/","/","/","/",   "r1","/","/","/","/",
                                #endregion
                            },
                            new string[] { "dl", "soul", "rok", "rok2", "back", "rot", "r1", "Ef1", "Ef2", "Ef3", "Ef4", },
                            new Action[]
                            {
                    ()=>{
                        Arrow[] ar=GetAll<Arrow>();for(int a=0;a<ar.Length;a++){ int x=a;ar[x].Delay(bpm); };
                            GreenSoulGB[] gb=GetAll<GreenSoulGB>();for(int a=0;a<gb.Length;a++){int x=a;gb[x].Delay(bpm); }
                        },
                    () =>
                    {
                        SetSoul(2);SetBox(240,40,200);PlaySound(Ding);
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*2+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                            }
                                    );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, 0,
                                new string[]
                                {
                               #region intro
                    "soul1","b1","/","/","/","/",   "b2","/","/","/","/",
                    "b3","/","/","/","/",   "b4","/","/","/","/",
                    "/","/","/","/",   "b6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "b5","/","/","b5","/","/",   "b7","b5","/","/","/","/",
                    "b5","/","/","/","/",   "b5","/","/","/","/",
                    "b5","/","/","/","/",   "b5","/","/","/","/",
                     "/","/","/","/",

                    "soul2","a1","/","/","/","/",   "a2","/","/","/","/",
                    "a3","/","/","/","/",   "a4","/","/","/","/",
                    "/","/","/","/",   "a6","/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "a5","/","/","a5","/","/",   "a7","a5","/","/","/","/",
                    "a5","/","/","/","/",   "a5","/","/","/","/",
                    "a5","/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "/","/","/","/",   "/","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "b6","/","/","/","/",   "/","/","/","/",
                                    #endregion

                                },
                                new string[] { "soul1", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "soul2", "a1", "a2", "a3", "a4", "a5", "a6", "a7", },
                                new Action[]
                                {
                    () => { SetSoul(2);SetBox(240,60,200);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,25)); CreateBone(new UpBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,50)); CreateBone(new UpBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,75)); CreateBone(new UpBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,100)); CreateBone(new UpBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,60)); CreateBone(new UpBone(false,3,60));CreateBone(new DownBone(true,3,60)); CreateBone(new DownBone(false,3,60)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre+new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,-5 }}); },
                    () => { SetSoul(2);Heart.GiveForce(180,0); SetBox(240,36,180);PlaySound(Ding); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,25)); CreateBone(new DownBone(false,3,25)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,50)); CreateBone(new DownBone(false,3,50)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,75)); CreateBone(new DownBone(false,3,75)); },
                    ()=>{PlaySound(pierce);CreateBone(new DownBone(true,3,100)); CreateBone(new DownBone(false,3,100)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,20)); CreateBone(new UpBone(false,3,20));CreateBone(new DownBone(true,3,20)); CreateBone(new DownBone(false,3,20)); },
                    ()=>{PlaySound(pierce);CreateBone(new UpBone(true,3,60)); CreateBone(new UpBone(false,3,60));CreateBone(new DownBone(true,3,60)); CreateBone(new DownBone(false,3,60)); },
                    ()=>{PlaySound(pierce);CreateBone(new CustomBone(BoxStates.Centre-new Vector2(0,120),Motions.PositionRoute.linear,0,40){ PositionRouteParam=new float[]{0,5 }}); },
                                }
                                        );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 7.5f)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 20,
                               new string[]
                               {
                               #region intro

                               "soul","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "pos1","R(R1)","/","/","/",   "(R1)","/","/","/",
                               "R","/","+0","/",   "R1","/","/","/",
                               "pos2","R(R1)","/","/","/",   "(R1)","/","/","/",
                               "R","/","+0","/",   "R1","/","/","/",

                               "pos1","R(R1)","/","/","/",   "(R1)","/","/","/",
                               "R","/","+0","/",   "R1","/","/","/",
                               "pos2","R","/","/","/",   "(R)","/","+0","/",
                               "R1","/","+01","/",   "R","/","+0","/",

                               "posr" ,"scr","R(R1)","/","/","/",   "/","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",

                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",

                               "(R)(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "(R1)","/","/","/",
                               "(R1)","/","/","/",   "soul1","/","/","/",

                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "gb","/","/","gb","/","/",   "gb","/","/","gb","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "b1","/","/","/","/",   "b1","/","/","/","/",
                               "b1","/","/","/","/",   "b1","/","/","/","/",
                               "b1","/","/","/","/",   "/","/","b1","/","/",
                               "/","/","/","/",   "b1","soul2","/","/","/","/",

                               "/","/","b1","/","/",   "Line","/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                                  "out","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "third","/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",
                               "/","/","/","/",   "/","/","/","/",

                               "G3056",
                                   #endregion

                               },
                               new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "pos1", "pos2", "posr", "scr", "Line", "out", "third" },
                               new Action[]
                               {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); Vector2 vec1=Heart.Centre-new Vector2(Rand(-100,100),-Rand(120,200)); CreateGB(new NormalGB(vec1,vec1,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); DownBone b=new(true,3,124){ ColorType=1};CreateBone(b);ForBeat(4,()=>{ b.Speed=b.Speed*0.95f+8*0.05f; }); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,-28),0.09f); },
                    ()=>{LerpScreenPos(bpm*1.5f,new(0,28),0.09f); },
                    ()=>{LerpScreenPos(bpm*4f,new(0,0),0.075f); },
                    ()=>{float sin =0;ForBeat(12,()=>{sin+=360/bpm/12;Heart.InstantSetRotation(Sin(sin)*15); }); },
                               () =>
                               {
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"a");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"b");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"c");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"d");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"e");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"f");
                                   CreateTagLine(new Linerotatelong(320,360,180+90,3000,0,1145,new(243,157,179)),"g");
                                   DelayBeat(1,()=>{
                                   LAlphaLerp("a",bpm*4,1,0.065f);
                                   LAlphaLerp("b",bpm*4,1,0.065f);
                                   LAlphaLerp("c",bpm*4,1,0.065f);
                                   LAlphaLerp("d",bpm*4,1,0.065f);
                                   LAlphaLerp("e",bpm*4,1,0.065f);
                                   LAlphaLerp("f",bpm*4,1,0.065f);
                                   LAlphaLerp("g",bpm*4,1,0.065f); });
                               },
                               () =>
                               {
                                   LerpScreenPos(bpm*4,new(0,115),0.086f);
                                   LRotLerp("a",bpm*8,180+90+50,0.05f);
                                   LRotLerp("b",bpm*8,180+90+25,0.05f);
                                   LRotLerp("c",bpm*8,180+90,0.05f);
                                   LRotLerp("d",bpm*8,180+90-25,0.05f);
                                   LRotLerp("e",bpm*8,180+90-50,0.05f);
                                   LRotLerp("f",bpm*8,180+90+75,0.05f);
                                   LRotLerp("g",bpm*8,180+90-75,0.05f);
                               },
                               () =>
                               {
                                   LVecLerp("a",bpm*8,new(320,660),0.02f);
                                   LVecLerp("b",bpm*8,new(320,660),0.02f);
                                   LVecLerp("c",bpm*8,new(320,660),0.02f);
                                   LVecLerp("d",bpm*8,new(320,660),0.02f);
                                   LVecLerp("e",bpm*8,new(320,660),0.02f);
                                   LVecLerp("f",bpm*8,new(320,660),0.02f);
                                   LVecLerp("g",bpm*8,new(320,660),0.02f);

                                   DelayBeat(4,()=>{
                                       LAlphaLerp("a",bpm*16,0,0.05f);
                                   LAlphaLerp("b",bpm*16,0,0.05f);
                                   LAlphaLerp("c",bpm*16,0,0.05f);
                                   LAlphaLerp("d",bpm*16,0,0.05f);
                                   LAlphaLerp("e",bpm*16,0,0.05f);
                                   LAlphaLerp("f",bpm*16,0,0.05f);
                                   LAlphaLerp("g",bpm*16,0,0.05f);
                                   Rotate(180*4,bpm*8); LerpScreenPos(bpm*8,new(0,0),0.05f);});
                                  DelayBeat(4,()=>{
                                   LRotLerp("a",bpm*8,180+90,0.09f);
                                   LRotLerp("b",bpm*8,180+90,0.09f);
                                   LRotLerp("c",bpm*8,180+90,0.09f);
                                   LRotLerp("d",bpm*8,180+90,0.09f);
                                   LRotLerp("e",bpm*8,180+90,0.09f);
                                   LRotLerp("f",bpm*8,180+90,0.09f);
                                   LRotLerp("g",bpm*8,180+90,0.09f);
                                   });
                               },
                               }
                                       );
                if (InBeat(8 * 4 * 3 + 16 + 4 + 64 + 8 + 54)) SpecialRhythmCreate(0.125f * bpm, 6.5f, bpm * 16,
                                 new string[]
                                 {
                               #region intro


                    "R1","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",

                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",

                    "(R)","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","/","/",

                    "(R)","/","/","/",   "/","/","/","/",
                    "Ef1","($0)($21)","/","/","/",  "Ef2","($0)($21)","/","/","/",
                    "Ef3","($0)($21)","/","/","/",   "Ef4","($0)($21)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R1","/","/","/",    "(R)","/","/","/",
                    "/","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "(R)","/","/","/",
                    "/","/","/","/",   "R1","/","/","/",

                    "(R)","/","/","/",   "(R1)","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","/","/",

                    "R","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "rok","($0)($2)","/","/","/",
                    "/","/","/","/",   "R(R1)","/","/","/",
                    "/","/","/","/",   "rok2","($11)($31)","/","/","/",

                    "R","/","/","/",   "/","/","/","/",
                    "(R)","/","/","/",   "back","(R1)","/","/","/",
                    "(R)","/","/","/",   "(R1)","/","/","/",
                    "/","/","/","/",   "/","/","/","/",

                    "R1","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",

                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",   "(R)","/","/","/",
                    "(R1)","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "rok","G214","G004","/","/","$0","/",    "+0","/","+0","/",
                    "R","/","/","/",   "R","/","/","/",
                    "rok2","G014","G204","/","/","$01","/",    "+01","/","+01","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "back","rot2","G0112","G3012","/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",
                    "/","/","/","/",    "/","/","/","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "rok","G114","G204","/","/","$2","/",    "+0","/","+0","/",
                    "(R1)","/","/","/",   "R","/","/","/",
                    "rok2","G314","G004","/","/","$31","/",    "+01","/","+01","/",

                    "(R1)","/","/","/",   "R","/","/","/",
                    "back","rot","G0016","G2116","!($0)($21)","/","/","/",    "sc1","@($0)($21)","/","/","/",
                    "sc2","!($0)($21)","/","/","/",   "sc3","@($0)($21)","/","/","/",
                    "sc4","~($0)($21)","/","/","/",   "/","/","/","/",

                    "sc5","/","/","/","/",

                                     #endregion
                                 },
                                 new string[] { "soul", "soul1", "gb", "b1", "soul2", "shrink1", "shrink2", "ready1", "ready2", "rok", "rok2", "back", "rot", "rot2", "r1", "Ef1", "Ef2", "Ef3", "Ef4",
                           "sc1","sc2","sc3","sc4","sc5",
                                 },
                                 new Action[]
                                 {
                               ()=>{SetSoul(1);SetBox(240,84,84); TP();Heart.GiveForce(0,0);},
                               ()=>{SetSoul(0);SetBox(240,300,128); },
                               ()=>{Vector2 vec=Heart.Centre+new Vector2(Rand(-50,50),-Rand(140,180)); CreateGB(new NormalGB(vec,vec,new(1,0.5f),bpm*1,bpm*0.5f)); },
                               ()=>{PlaySound(pierce); CreateBone(new DownBone(true,6,124){ ColorType=1}); },
                               ()=>{ SetBox(240,84,84);TP();SetSoul(1); },
                               ()=>{Convulse(120,bpm*1.8f,false); },
                    ()=>{Convulse(120,bpm*1.8f,true); },

                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,8); },
                    ()=>{SizeShrink(7,bpm*2);RotateWithBack((int)bpm*2,-8); },
                    ()=>{
                        LerpScreenPos(bpm,new(-26,0),0.09f);Convulse(40,bpm,false);
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","A" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","B" });
                        CreateTagLine(new Linerotate(0,240,90,bpm*6,0,new(243,157,179)),new string[]{"a","C" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("a",bpm*4,1,0,bpm*6f,0);
                            VecLerp("A",bpm*4,new(120,0),0.08f);
                            VecLerp("B",bpm*4,new(180,0),0.08f);
                            VecLerp("C",bpm*4,new(170+75,0),0.08f);
                        }));
                    },
                    ()=>{
                        LerpScreenPos(bpm,new(26,0),0.09f);Convulse(40,bpm,true);
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","X" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Y" });
                        CreateTagLine(new Linerotate(640,240,90,bpm*6,0,new(243,157,179)),new string[]{"b","Z" });
                        AddInstance(new InstantEvent(2,()=>
                        {
                            AlphaSin("b",bpm*4,1,0,bpm*6f,0);
                            VecLerp("X",bpm*4,new(640-120,0),0.08f);
                            VecLerp("Y",bpm*4,new(640-180,0),0.08f);
                            VecLerp("Z",bpm*4,new(640-170-75,0),0.08f);
                        }));
                    },
                    ()=>{ LerpScreenPos(bpm,new(0,0),0.09f);},
                    ()=>{ Rotate(360,bpm*3+2); },
                    ()=>{ Rotate(-360,bpm*3+2); },
                    ()=>{RotateSymmetricBack(bpm*2,20); },
                    ()=>{ ScreenDrawing.ScreenAngle=90;Heart.InstantSetRotation(90); },
                    ()=>{ ScreenDrawing.ScreenAngle=180;Heart.InstantSetRotation(180); },
                    ()=>{ ScreenDrawing.ScreenAngle=270;Heart.InstantSetRotation(270); },
                    ()=>{ ScreenDrawing.ScreenAngle=0;Heart.InstantSetRotation(0); },
                    ()=>{ ScreenScale=1.333f; },
                    ()=>{ ScreenScale=1.666f;},
                    ()=>{ ScreenScale=2f; },
                    ()=>{ ScreenScale=3f; },
                    ()=>{ LerpScreenScale(bpm*8,1,0.09f); },
                                 }
                                         );
            }
        }
    }
}
