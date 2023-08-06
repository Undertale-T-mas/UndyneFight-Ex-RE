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
                                    EaseOut(BeatTime(1f), new Vector2(0, -250), EaseState.Quad)
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
                                    EaseOut(BeatTime(2f), new Vector2(330, 0), EaseState.Quad)
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
                                    EaseOut(BeatTime(2f), new Vector2(0, 250), EaseState.Quad)
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
                                    EaseOut(BeatTime(2f), new Vector2(-330, 0), EaseState.Quad)
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
                                    EaseOut(BeatTime(2f), new Vector2(0, -250), EaseState.Quad)
                                    ).Easing
                        });
                        DelayBeat(3.25f, mett1.Dispose);
                    });
                    RegisterFunctionOnce("Mark", () => { });
                    RegisterFunctionOnce("ChordLine1", () =>
                    {
                        var c1 = LinkEase(Stable(0, new Vector2(320, 0)), Linear(BeatTime(4), new Vector2(240, 0)), Linear(BeatTime(4), new Vector2(-240, 0)), Linear(BeatTime(4), new Vector2(240, 0)), Linear(BeatTime(4), new Vector2(-240, 0)));
                        Line l = new(c1.Easing, Stable(0, 90).Easing) { Alpha=0.01f};
                        CreateEntity(l);
                        l.DelayAlphaIncrease(0, BeatTime(4), 0.99f);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(4), 0.95f);
                        l.DelayAlphaIncrease(BeatTime(8), BeatTime(4), 0.95f);
                        l.DelayAlphaDecrease(BeatTime(12), BeatTime(4));
                        l.TransverseMirror = true;
                        l.AddShadow(6, 0.66f);
                        l.AddShadow(12, 0.33f);
                    });
                    RegisterFunctionOnce("ChordLine2", () =>
                    {
                        var c1 = LinkEase(Stable(0, new Vector2(80, 0)), Linear(BeatTime(4), new Vector2(240, 0)), Linear(BeatTime(4), new Vector2(-240, 0)), Linear(BeatTime(4), new Vector2(240, 0)), Linear(BeatTime(4), new Vector2(-240, 0)));
                        Line l = new(c1.Easing, Stable(0, 90).Easing) { Alpha = 0.01f };
                        CreateEntity(l);
                        l.DelayAlphaIncrease(0, BeatTime(4), 0.99f);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(4), 0.95f);
                        l.DelayAlphaIncrease(BeatTime(8), BeatTime(4), 0.95f);
                        l.DelayAlphaDecrease(BeatTime(12), BeatTime(4));
                        l.TransverseMirror = true;
                        l.AddShadow(6, 0.66f);
                        l.AddShadow(12, 0.33f);
                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "Stable(Start)(ChordLine1)","","<0>Angle","",   "Mark(Block2)","","","<90>Angle",
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

                        "(ChordLine2)","","<360>Angle","",   "Mark(Block2)","","","<90>Angle",
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
                        "","","<270>Angle","",   "Mark","","","",
                    });
                }
                if(InBeat(28))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        Heart.RotateTo(360);
                        SetSoul(1);
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(2), true);
                    });

                    BarrageCreate(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","+1","",   "+1","","+1","",
                        "R1","","","",   "R","","","",

                        "R","","","",   "R","","D1","",
                        "","","R","",   "D1","","","",
                        "R","","+1","",   "+1","","+1","",
                        "R1","","","",   "R","","","",

                        "R","","","",   "R","","D1","",
                        "","","R","",   "D1","","","",
                        "R","","+1","",   "+1","","+1","",
                        "R1","","","",   "R","","","",

                        "R","","","",   "R","","D1","",
                        "","","R","",   "D1","","","",
                        "(*R1)(+1)(*+11)","~_-1","~_+0","~_+0",   "~_+0","","","",
                        "(*R1)(-1)(*-11)","~_+1","~_+0","~_+0",   "~_+0","","","",
                        //
                        "(*$01)(*$21)","","","",   "$1","","","",
                        "(*$01)(*$21)","","","",   "$3","","","",
                        "(*$01)(*$21)","","","",   "$1","","","",
                        "(*$01)(*$21)","","","",   "$3","","","",

                        "(*$0)(*$2)","","","",   "$11","","","",
                        "(*$0)(*$2)","","","",   "$31","","","",
                        "(*$0)(*$2)","","","",   "$11","","","",
                        "(*$0)(*$2)","","","",   "$31","","","",

                        "(R)(+21)","","","",   "(R)(+21)","","(-1)(+21)","",
                        "","","(R)(+21)","",   "(-1)(+21)","","","",
                        "($0)(+21)","","(-1)(+21)","",   "(-1)(+21)","","(-1)(+21)","",
                        "(-1)(+21)","","","",   "(R)(+21)","","","",

                        "R","","","",   "R","","+1","",
                        "+1","","","",   "R","","","",
                        "R","","","",   "R","","-1","",
                        "-1","","","",   "R","","","",
                        //
                    });
                }
                if (InBeat(60))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(320-120, 650, 240 - 60, 240 + 60);
                        Heart.RotateTo(270);
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(-10, 320+120, 240 - 60, 240 + 60);
                        Heart.RotateTo(90);
                    });
                    RegisterFunctionOnce("Over", () =>
                    {
                        SetSoul(1);
                        TP();
                        SetGreenBox();
                        Heart.RotateTo(0);
                    });
                    var val = EaseIn(BeatTime(1), new Vector2(-80, 0), EaseState.Quad);
                    float spd = -5;
                    RegisterFunctionOnce("Block1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd , 0)))));
                    });
                    RegisterFunctionOnce("Block2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
                    });
                    RegisterFunctionOnce("Bomb2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new Bomb(20,LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new    ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 + 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240)), val, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 + 48)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
                    });
                    var vals = EaseIn(BeatTime(1), new Vector2(80, 0), EaseState.Quad);
                    float spds = 5;
                    RegisterFunctionOnce("sBlock1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spd, 0)))));
                    });
                    RegisterFunctionOnce("sBomb1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))) { AbleLink = false });
                    });
                    RegisterFunctionOnce("sBomb2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
                    });
                    Vector3 blockpos() 
                    {
                        int rand = Rand(1, 5);
                        int nextrand = 0;
                        if (Rand(0, 1) == 0)
                        {
                            if (rand != 1) nextrand = rand - 1;
                            else if (rand != 5) nextrand = rand + 1;
                        }
                        else
                        {
                            if (rand != 5) nextrand = rand + 1;
                            else if (rand != 1) nextrand = rand - 1;
                        }
                        int Lastrand = 0;
                        if (Rand(0, 1) == 0)
                        {
                            if (nextrand != 1) Lastrand = nextrand - 1;
                            else if (nextrand != 5) Lastrand = nextrand + 1;
                        }
                        else
                        {
                            if (nextrand != 5) Lastrand = nextrand + 1;
                            else if (nextrand != 1) Lastrand = nextrand - 1;
                        }
                        return new Vector3(rand, nextrand, Lastrand);
                    }
                    Vector3 rand1 = blockpos();
                    Vector3 rand2 = blockpos();
                    Vector3 rand3 = blockpos();
                    Vector3 rand4 = blockpos();
                    Vector3 rand5 = blockpos();
                    Vector3 rand6 = blockpos();
                    Vector3 rand7 = blockpos();
                    Vector3 rand8 = blockpos();
                    BarrageCreate(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul","","","",   $"(Block{rand1.X})","","","",
                        "","",$"(Block{rand1.X})","",   "","","","",
                        $"(Block{rand1.Y})","","","",   "","",$"(Block{rand1.Y})","",
                        "","","","",   $"(Block{rand1.Z})","","","",

                        "","","","",   $"(Bomb{rand2.X})","","","",
                        "","",$"(Bomb{rand2.X})","",   "","","","",
                        $"(Bomb{rand2.Y})","","","",   "","",$"(Bomb{rand2.Y})","",
                        "","","","",   $"(Bomb{rand2.Z})","","","",

                        "","","","",   $"(Block{rand3.X})","","","",
                        "","",$"(Block{rand3.X})","",   "","","","",
                        $"(Block{rand3.Y})","","","",   "","",$"(Block{rand3.Y})","",
                        "","","","",   $"(Block{rand3.Z})","","","",

                        "","","","",   $"(Bomb{rand4.X})","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "Change","","","",   $"(sBlock{rand5.X})","","","",
                        "","",$"(sBlock{rand5.X})","",   "","","","",
                        $"(sBlock{rand5.Y})","","","",   "","",$"(sBlock{rand5.Y})","",
                        "","","","",   $"(sBlock{rand5.Z})","","","",

                        "","","","",   $"(sBomb{rand6.X})","","","",
                        "","",$"(sBomb{rand6.X})","",   "","","","",
                        $"(sBomb{rand6.Y})","","","",   "","",$"(sBomb{rand6.Y})","",
                        "","","","",   $"(sBomb{rand6.Z})","","","",

                        "","","","",   $"(sBlock{rand7.X})","","","",
                        "","",$"(sBlock{rand7.X})","",   "","","","",
                        $"(sBlock{rand7.Y})","","","",   "","",$"(sBlock{rand7.Y})","",
                        "","","","",   $"(sBlock{rand7.Z})","","","",

                        "","","","",   $"(sBomb{rand8.X})","","","",
                        "","",$"(sBomb{rand8.X})","",   "","","","",
                        $"(sBomb{rand8.Y})","","","",   "","",$"(sBomb{rand8.Y})","",
                        "","","","",   $"(sBomb{rand8.Z})","","","",
                        //
                        "Over"
                    });
                }
                if(InBeat(92))
                {
                    RegisterFunctionOnce("LKickLine", () =>
                    {
                        var cr = LinkEase(EaseOut(BeatTime(1), new Vector2(650, 0), EaseState.Quart));
                        Line l = new(cr, Stable(0,90));
                        CreateEntity(l);
                        l.DelayDispose(BeatTime(1)+1);
                        l.AddShadow(4, 0.6f);
                        l.AddShadow(8, 0.3f);
                    });
                    RegisterFunctionOnce("RKickLine", () =>
                    {
                        var cr = LinkEase(Stable(0,new Vector2(640,0)),EaseOut(BeatTime(1), new Vector2(-650, 0), EaseState.Quart));
                        Line l = new(cr, Stable(0, 90));
                        CreateEntity(l);
                        l.DelayDispose(BeatTime(1)+1);
                        l.AddShadow(4, 0.6f);
                        l.AddShadow(8, 0.3f);
                    });
                    RegisterFunctionOnce("RSA", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-4.0f, 4.0f);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "+01","","","",
                        "+0","","","",   "","","","",

                        "R(RSA)","","+01(RSA)","",   "D(RSA)","","+01(RSA)","",
                        "D(RSA)","","+01(RSA)","",   "D(RSA)","","+01(RSA)","",
                        "*D(RSA)","","*+01(RSA)","",   "*+0(RSA)","","*+01(RSA)","",
                        "*+0(<0>Scrangle)","","","",   "+0","","","",

                        "(*$1)(*$2)(<0.5,3>Drum)(LKickLine)","","$01","",   "","","(*$1)(*$2)(<0.75,-3>Drum)(LKickLine)","",
                        "$31","","","",   "","","","",
                        "(*$1)(*$2)(<0.5,3>Drum)(LKickLine)","","$01","",   "","","(*$1)(*$2)(<0.75,-3>Drum)(LKickLine)","",
                        "$31","","","",   "","","","",

                        "(*$1)(*$0)(<0.5,3>Drum)(LKickLine)","","$21","",   "","","(*$1)(*$0)(<0.5,-3>Drum)(RKickLine)","",
                        "$31","","","",   "(*$1)(*$0)(<0.5,3>Drum)(LKickLine)","","$21","",
                        "(*$1)(*$0)(<0.75,-3>Drum)(RKickLine)","","$31","",   "","","","",
                        "(*$1)(*$0)(<0.75,-3>Drum)(RKickLine)","","","",   "","","","",
                        //
                        "R1","","","",   "+0","","","",
                        "+01","","","",   "","","","",
                        "D","","","",   "+01","","","",
                        "+0","","","",   "","","","",

                        "R","","+01","",   "D","","+01","",
                        "D","","+01","",   "D","","+01","",
                        "*D","","*+01","",   "*+0","","*+01","",
                        "*+0","","","",   "+0","","","",

                        "(*$11)(*$21)","","$0","",   "","","(*$11)(*$21)","",
                        "$3","","","",   "","","","",
                        "(*$11)(*$21)","","$0","",   "","","(*$11)(*$21)","",
                        "$3","","","",   "","","","",

                        "(*$11)(*$01)","","$2","",   "","","(*$11)(*$01)","",
                        "$3","","","",   "(*$11)(*$01)","","$2","",
                        "(*$11)(*$01)","","$3","",   "","","","",
                        "(*$11)(*$01)","","","",   "","","","",
                    });
                }
                if (InBeat(124))
                {
                    RegisterFunctionOnce("Bomb", () =>
                    {
                        CreateEntity(new Bomb(20, LinkEase(Stable(0,new Vector2(0,200)),InfLinear(new Vector2(2,0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0,new Vector2(0,180)),InfLinear(new Vector2(2,0)))));
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "Bomb","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
            }
            public void Easy()
            {

            }
            public void Start()
            {
                Settings.GreenTap = true;
                RegisterFunction("Drum", () =>
                {
                    ScreenDrawing.ScreenAngle = 0;
                    float time = Arguments[0];
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    },
                    EaseOut(BeatTime(time) / 4f, Arguments[1], EaseState.Quad),
                    EaseIn(BeatTime(time) / 4f * 3, -Arguments[1], EaseState.Quad)
                    );
                    //Delay(time+2, () => { ScreenDrawing.ScreenAngle = 0; });
                });
                RegisterFunction("Scrangle", () =>
                {
                    ScreenDrawing.ScreenAngle = Arguments[0];
                });
                RegisterFunction("Angle", () =>
                {
                    Heart.RotateTo(Arguments[0]);
                });
                SetSoul(Souls.YellowSoul);
                GametimeDelta = -2.5f;
                InstantSetBox(240, 84, 84);
                InstantTP(320, 240);
                bool jump = true;
                int jumpbeat=60;
                if (jump)
                {
                    GametimeDelta = -4.5f + BeatTime(jumpbeat);
                    PlayOffset=BeatTime(jumpbeat);
                    SetSoul(Souls.YellowSoul);
                    //Heart.RotateTo(0);
                }
            }
        }
    }
}