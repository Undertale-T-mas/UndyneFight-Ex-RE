using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
namespace Rhythm_Recall.Waves
{
    public class BIG_SHOT : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public BIG_SHOT()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f / (140f / 60f)) { }
            public string Music => "BIG SHOT";

            public string FightName => "BIG SHOT";

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "Toby Fox";
                public override string BarrageAuthor => "Big Tlott";
                public override string AttributeAuthor => "Big Tlott";
                public override string PaintAuthor => "UK";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,4.0f),
                    new(Difficulty.Extreme,16.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,4.0f),
                    new(Difficulty.Extreme,16.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,8.0f),
                    new(Difficulty.Extreme,20.0f)
                    }
                    );
            }
            #region disused
            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                throw new NotImplementedException();
            }
            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Extreme()
            {
                if(InBeat(0))
                {
                    ForBeat120(32, () =>
                    {
                        //if (GameStates.IsKeyDown(InputIdentity.Confirm)) Heart.RotateTo(MathF.PI*Heart.Rotation + 90);
                    });
                    RegisterFunctionOnce("Stable", () =>
                    {
                        RunEase((s) => { InstantTP(s); }, Stable(BeatTime(32), new Vector2(320, 240)));
                    });
                    RegisterFunctionOnce("Start", () =>
                    {
                        //Heart.RotateTo(90);
                        ShotableBlock mett1;
                        GameStates.InstanceCreate(mett1 = new ShotableBlock()
                        {
                            PositionRoute =
                                LinkEase(
                                    Stable(0, new Vector2(320, 490)),
                                    EaseIn(BeatTime(1f), new Vector2(0, -250), EaseState.Quad),
                                    Stable(BeatTime(1), Vector2.Zero)
                                    ).Easing
                        });
                        DelayBeat(2.25f, mett1.Dispose);
                        ShotableBlock mett2;
                        GameStates.InstanceCreate(mett2 = new ShotableBlock()
                        {
                            PositionRoute =
                                LinkEase(
                                    Stable(0, new Vector2(-10, 240)),
                                    EaseIn(BeatTime(1.75f), new Vector2(330, 0), EaseState.Quad),
                                    Stable(BeatTime(1), Vector2.Zero)
                                    ).Easing
                        });
                        DelayBeat(3, mett2.Dispose);
                    });
                    RegisterFunctionOnce("Block1", () =>
                    {
                        //Heart.RotateTo(90);
                        ShotableBlock mett1;
                        GameStates.InstanceCreate(mett1 = new ShotableBlock()
                        {
                            PositionRoute =
                                LinkEase(
                                    Stable(0, new Vector2(-10, 240)),
                                    EaseIn(BeatTime(2f), new Vector2(330, 0),EaseState.Quad),
                                    Stable(BeatTime(1), Vector2.Zero)
                                    ).Easing
                        });
                        DelayBeat(3.25f, mett1.Dispose);
                    });
                    RegisterFunctionOnce("Block2", () =>
                    {
                        //Heart.RotateTo(90);
                        ShotableBlock mett1;
                        GameStates.InstanceCreate(mett1 = new ShotableBlock()
                        {
                            PositionRoute =
                                LinkEase(
                                    Stable(0, new Vector2(320, -10)),
                                    EaseIn(BeatTime(2f), new Vector2(0, 250), EaseState.Quad),
                                    Stable(BeatTime(1), Vector2.Zero)
                                    ).Easing
                        });
                        DelayBeat(3.25f, mett1.Dispose);
                    });
                    RegisterFunctionOnce("Block3", () =>
                    {
                        //Heart.RotateTo(90);
                        ShotableBlock mett1;
                        GameStates.InstanceCreate(mett1 = new ShotableBlock()
                        {
                            PositionRoute =
                                LinkEase(
                                    Stable(0, new Vector2(650, 240)),
                                    EaseIn(BeatTime(2f), new Vector2(-330, 0), EaseState.Quad),
                                    Stable(BeatTime(1), Vector2.Zero)
                                    ).Easing
                        });
                        DelayBeat(3.25f, mett1.Dispose);
                    });
                    RegisterFunctionOnce("Block4", () =>
                    {
                        //Heart.RotateTo(90);
                        ShotableBlock mett1;
                        GameStates.InstanceCreate(mett1 = new ShotableBlock()
                        {
                            PositionRoute =
                                LinkEase(
                                    Stable(0, new Vector2(320, 490)),
                                    EaseIn(BeatTime(2f), new Vector2(0, -250), EaseState.Quad),
                                    Stable(BeatTime(1), Vector2.Zero)
                                    ).Easing
                        });
                        DelayBeat(3.25f, mett1.Dispose);
                    });
                    RegisterFunctionOnce("Mark", () => { });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "Stable(Start)","","<0>Angle","",   "Mark(Block2)","","","<90>Angle",
                        "","(Mark)","(Block3)","",   "","","<180>Angle","",
                        "Mark(Block4)","","","",   "<270>Angle","","Mark","",
                        "(Block1)","","<360>Angle","",   "Mark","","(Block2)","",

                        "","","<90>Angle","",   "Mark(Block3)","","","<180>Angle",
                        "","(Mark)","(Block4)","",   "","","<270>Angle","",
                        "Mark(Block1)","","","",   "<360>Angle","","Mark","",
                        "(Block2)","","<90>Angle","",   "Mark","","(Block3)","",

                        "","","<180>Angle","",   "Mark(Block4)","","","<270>Angle",
                        "","(Mark)","(Block1)","",   "","","<360>Angle","",
                        "Mark(Block2)","","","",   "<90>Angle","","Mark","",
                        "(Block3)","","<180>Angle","",   "Mark","","(Block4)","",

                        "","","<270>Angle","",   "Mark(Block1)","","","<360>Angle",
                        "","(Mark)","(Block2)","",   "","","<90>Angle","",
                        "Mark(Block3)","","","",   "<180>Angle","","Mark","",
                        "(Block4)","","<270>Angle","",   "Mark","","(Block1)","",

                        "","","<360>Angle","",   "Mark(Block4)","","","<-90>Angle",
                        "","(Mark)","(Block3)","",   "","","<-180>Angle","",
                        "Mark(Block2)","","","",   "<-270>Angle","","Mark","",
                        "(Block1)","","<-360>Angle","",   "Mark","","(Block4)","",

                        "","","<-90>Angle","",   "Mark(Block3)","","","<-180>Angle",
                        "","(Mark)","(Block2)","",   "","","<-270>Angle","",
                        "Mark(Block1)","","","",   "<-360>Angle","","Mark","",
                        "(Block4)","","<-90>Angle","",   "Mark","","(Block3)","",

                        "","","<-180>Angle","",   "Mark(Block2)","","","<-270>Angle",
                        "","(Mark)","(Block1)","",   "","","<-360>Angle","",
                        "Mark(Block4)","","","",   "<-90>Angle","","Mark","",
                        "(Block3)","","<-180>Angle","",   "Mark","","(Block2)","",

                        "","","<-270>Angle","",   "Mark(Block1)","","","<-360>Angle",
                        "","(Mark)","(Block4)","",   "","","<-90>Angle","",
                        "Mark(Block3)","","","",   "<-180>Angle","","Mark","",
                        "","","<-270>Angle","",   "Mark","","","",
                    });
                }
            }
            public void Easy()
            {

            }
            public void Start()
            { 
                RegisterFunction("Angle", () =>
                {
                    Heart.RotateTo(Arguments[0]);
                });
                SetSoul(Souls.YellowSoul);
                GametimeDelta = -2.5f;
                InstantSetBox(240, 84, 84);
                InstantTP(320, 240);
                bool jump = false;
                int jumpbeat=0;
                if (jump)
                {
                    GametimeDelta = -3.5f + BeatTime(jumpbeat);
                    PlayOffset=BeatTime(jumpbeat);
                }
            }
        }
    }
}