using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
namespace Rhythm_Recall.Waves
{
    internal class BIG_SHOT : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public BIG_SHOT()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
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
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,4.0f),
                    new(Difficulty.Extreme,17.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,4.0f),
                    new(Difficulty.Extreme,17.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
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
                if (InBeat(0))
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
                                    EaseOut(BeatTime(1.75f), new Vector2(330, 0), EaseState.Quad)
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
                                    EaseIn(BeatTime(2f), new Vector2(330, 0), EaseState.Quad),
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
                    CreateChart(0, BeatTime(1), 6, new string[]
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
                if (InBeat(28))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        Heart.RotateTo(360);
                        SetSoul(1);
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(2), true);
                    });

                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
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
                        SetBox(320 - 120, 650, 240 - 60, 240 + 60);
                        Heart.RotateTo(270);
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(-10, 320 + 120, 240 - 60, 240 + 60);
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
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
                    RegisterFunctionOnce("PlaySound", () =>
                    {
                        PlaySound(Sounds.pierce);
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
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
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
                        "","","PlaySound","",   "","","","",
                        "PlaySound","","","",   "","","PlaySound","",
                        "","","","",   "PlaySound","","","",
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
                        "","",$"PlaySound","",   "","","","",
                        $"PlaySound","","","",   "","",$"PlaySound","",
                        "","","","",   $"PlaySound","","","",
                        //
                        "Over"
                    });
                }
                if (InBeat(92))
                {
                    RegisterFunctionOnce("LKickLine", () =>
                    {
                        var cr = LinkEase(EaseOut(BeatTime(1), new Vector2(650, 0), EaseState.Quart));
                        Line l = new(cr, Stable(0, 90));
                        CreateEntity(l);
                        l.DelayDispose(BeatTime(1) + 1);
                        l.AddShadow(4, 0.6f);
                        l.AddShadow(8, 0.3f);
                    });
                    RegisterFunctionOnce("RKickLine", () =>
                    {
                        var cr = LinkEase(Stable(0, new Vector2(640, 0)), EaseOut(BeatTime(1), new Vector2(-650, 0), EaseState.Quart));
                        Line l = new(cr, Stable(0, 90));
                        CreateEntity(l);
                        l.DelayDispose(BeatTime(1) + 1);
                        l.AddShadow(4, 0.6f);
                        l.AddShadow(8, 0.3f);
                    });
                    RegisterFunctionOnce("RSA", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-4.0f, 4.0f);
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "+01","","","",
                        "+0","","","",   "","","","",

                        "(R)(+0)(RSA)","","+01(RSA)","",   "(D)(+0)(RSA)","","+01(RSA)","",
                        "(D)(+0)(RSA)","","+01(RSA)","",   "(D)(+0)(RSA)","","+01(RSA)","",
                        "*D(RSA)","","*+01(RSA)","",   "*+0(RSA)","","*+01(RSA)","",
                        "*+0(<0>Scrangle)","","","",   "+0","","","",

                        "(*$1)(*$2)(<0.5,3>Drum)(LKickLine)","","($01)($01)","",   "","","(*$1)(*$2)(<0.75,-3>Drum)(LKickLine)","",
                        "$31($31)","","","",   "","","","",
                        "(*$1)(*$2)(<0.5,3>Drum)(LKickLine)","","$01($01)","",   "","","(*$1)(*$2)(<0.75,-3>Drum)(LKickLine)","",
                        "$31($31)","","","",   "","","","",

                        "(*$1)(*$0)(<0.5,3>Drum)(LKickLine)","","$21($21)","",   "","","(*$1)(*$0)(<0.5,-3>Drum)(RKickLine)","",
                        "$31($31)","","","",   "(*$1)(*$0)(<0.5,3>Drum)(LKickLine)","","$21($21)","",
                        "(*$1)(*$0)(<0.75,-3>Drum)(RKickLine)","","$31($31)","",   "","","","",
                        "(*$1)(*$0)(<0.75,-3>Drum)(RKickLine)","","","",   "","","","",
                        //
                        "R1","","","",   "+0","","","",
                        "+01","","","",   "","","","",
                        "D","","","",   "+01","","","",
                        "+0","","","",   "","","","",

                        "(R)(+0)(RSA)","","+01(RSA)","",   "(D)(+0)(RSA)","","+01(RSA)","",
                        "(D)(+0)(RSA)","","+01(RSA)","",   "(D)(+0)(RSA)","","+01(RSA)","",
                        "*D(RSA)","","*+01(RSA)","",   "*+0(RSA)","","*+01(RSA)","",
                        "*+0(<0>Scrangle)","","","",   "+0","","","",

                        "(*$11)(*$21)(<0.5,-3>Drum)(RKickLine)","","$0($0)","",   "","","(*$11)(*$21)(<0.75,-3>Drum)(RKickLine)","",
                        "$3($3)","","","",   "","","","",
                        "(*$11)(*$21)(<0.5,-3>Drum)(RKickLine)","","$0($0)","",   "","","(*$11)(*$21)(<0.75,-3>Drum)(RKickLine)","",
                        "$3($3)","","","",   "","","","",

                        "(*$11)(*$01)(<0.5,-3>Drum)(RKickLine)","","$2($2)","",   "","","(*$11)(*$01)(<0.5,3>Drum)(LKickLine)","",
                        "$3($3)","","","",   "(*$11)(*$01)(<0.5,-3>Drum)(RKickLine)","","$2($2)","",
                        "(*$11)(*$01)(<0.75,3>Drum)(LKickLine)","","$3($3)","",   "","","","",
                        "(*$11)(*$01)(<0.75,3>Drum)(LKickLine)","","","",   "","","","",
                    });
                }
                if (InBeat(124))
                {
                    RegisterFunctionOnce("Mtt", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(-10, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.InsertShot(4, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Mtt2", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(650, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(3)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        //mt.BulletDepth = 0.49f;
                        mt.InsertShot(4, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Bomb", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(-10, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    RegisterFunctionOnce("Bomb2", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(-10, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(2)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    int rd1 = Rand(0, 3);
                    int rd2 = Rand(0, 3);
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        $"Mtt(#4#${rd1})","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bomb","","","",   "","","","",
                        "","","","",   "","","","",

                        "Mtt","","","",   $"(N{rd1}1)","","","",
                        $"(N{rd1}1)","","","",   $"(N{rd1}1)","","","",
                        $"Bomb(#1#${rd2})","","","",   "","","","",
                        $"N{rd2}1","","","",   $"(#4#R)","","","",

                        "Mtt","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bomb","","","",   "","","","",
                        "","","","",   "","","","",

                        "Mtt","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "Bomb(#0.5#D)","","","",   "","","","",
                        "(#0.5#D)","","","",   "","","","",
                        //
                        "Mtt(R)(+21)","","","",   "(D)(+21)","","","",
                        "(D)(+21)","","","",   "(#1#D)(+21)","","","",
                        "Bomb","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "Mtt(#1#D)","","","",   "","","","",
                        "(#1#D1)","","","",  "","","","",
                        "Bomb(#1#D)","","","",   "","","+01","",
                        "","","","",   "$0","$1","#3#$2","",

                        "Mtt","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bomb","","","",   "","","","",
                        "(#1#R)","","","",   "","","","",

                        "Mtt(#1#D1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bomb(D)","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(156))
                {
                    RegisterFunctionOnce("Mtt", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(-10, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.InsertShot(8, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Mtt2", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(650, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(3)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.InsertShot(8, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Bomb", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(-10, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    RegisterFunctionOnce("Bomb2", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(650, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(2)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    Arrow.UnitEasing ease;
                    AddInstance(ease = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease.RotationEase = LinkEase(Stable(BeatTime(1), 30), EaseOut(BeatTime(1), -30, EaseState.Back));
                    ease.TagApply("s");
                    Arrow.UnitEasing ease2;
                    AddInstance(ease2 = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease2.RotationEase = LinkEase(Stable(BeatTime(1), 60), EaseOut(BeatTime(1), -60, EaseState.Elastic));
                    ease2.TagApply("ss");
                    Arrow.UnitEasing ease3;
                    AddInstance(ease3 = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease3.RotationEase = LinkEase(Stable(BeatTime(1), -30), EaseOut(BeatTime(1), 30, EaseState.Back));
                    ease3.TagApply("w");
                    Arrow.UnitEasing ease4;
                    AddInstance(ease4 = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease4.RotationEase = LinkEase(Stable(BeatTime(1), -60), EaseOut(BeatTime(1), 60, EaseState.Elastic));
                    ease4.TagApply("ww");
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "*$3@s(Mtt)","","","",   "","","*$3@ss","",
                        "","","","",   "*$3@s","","","",
                        "Bomb","","*$3@ss","",   "","","","",
                        "*$3@s","","","",   "*$3@ss","","","",

                        "*$31@w(Mtt2)","","","",   "","","*$31@ww","",
                        "","","","",   "*$31@w","","","",
                        "Bomb2","","*$31@ww","",   "","","","",
                        "*$31@w","","","",   "*$31@ww","","","",

                        "*$1@s(Mtt)","","","",   "","","*$1@ss","",
                        "","","","",   "*$1@s","","","",
                        "Bomb","","*$1@ss","",   "","","","",
                        "*$1@s","","","",   "*$1@ss","","","",

                        "*$11@w(Mtt2)","","","",   "","","*$11@ww","",
                        "","","","",   "*$11@w","","","",
                        "Bomb2","","*$11@ww","",   "","","","",
                        "*$11@w","","","",   "*$11@ww","","","",
                        //
                        "*$3@s(Mtt)","","","",   "","","*$11@ss","",
                        "","","","",   "*$3@s","","","",
                        "Bomb","","*$11@ss","",   "","","","",
                        "*$3@s","","","",   "*$11@ss","","","",

                        "*$3@w(Mtt2)","","","",   "","","*$11@ww","",
                        "","","","",   "*$3@w","","","",
                        "Bomb2","","*$11@ww","",   "","","","",
                        "*$3@w","","","",   "*$11@ww","","","",

                        "*$1@s(Mtt)","","","",   "","","*$31@ss","",
                        "","","","",   "*$1@s","","","",
                        "Bomb","","*$31@ss","",   "","","","",
                        "*$1@s","","","",   "*$31@ss","","","",

                        "*$1@w(Mtt2)","","","",   "","","*$31@ww","",
                        "","","","",   "*$1@w","","","",
                        "Bomb2","","*$31@ww","",   "","","","",
                        "*$1@w","","","",   "*$31@ww","","","",
                    });
                }
                if (InBeat(188))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        Heart.RotateTo(360);
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("BaseLine", () =>
                    {
                        var ec = LinkEase(
                            Stable(0, new Vector2(0, 240)),
                            EaseOut(BeatTime(0.75f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(-180, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(-180, 0), EaseState.Quart)
                            );
                        Line l = new(ec.Easing, Stable(0, 90));
                        CreateEntity(l);
                        l.AddShadow(2, 0);
                        l.AddShadow(4, 0);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                        var sc = LinkEase(
                            Stable(0, 0),
                            EaseOut(BeatTime(0.75f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), 3f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), 3f, EaseState.Quart)
                            );
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, sc);
                    });
                    RegisterFunctionOnce("BaseLine2", () =>
                    {
                        var ec = LinkEase(
                            Stable(0, new Vector2(640, 240)),
                            EaseOut(BeatTime(0.75f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(180, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(180, 0), EaseState.Quart)
                            );
                        Line l = new(ec.Easing, Stable(0, 90));
                        CreateEntity(l);
                        l.AddShadow(4, 0);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                        var sc = LinkEase(
                            Stable(0, 0),
                            EaseOut(BeatTime(0.75f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), -3f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), -3f, EaseState.Quart)
                            );
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, sc);
                    });
                    RegisterFunctionOnce("aLine", () =>
                    {
                        Line l = new(Stable(0, new Vector2(Arguments[0], 240)).Easing, LinkEase(Stable(0, 90), EaseOut(BeatTime(0.5f), Arguments[1], EaseState.Cubic)));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.8f));
                    });
                    RegisterFunctionOnce("aLine2", () =>
                    {
                        Line l = new(LinkEase(Stable(0, new Vector2(Arguments[0], 240)), EaseOut(BeatTime(0.8f), new Vector2(Arguments[2], 0), EaseState.Cubic)).Easing, LinkEase(Stable(0, 90), EaseOut(BeatTime(0.5f), Arguments[1], EaseState.Cubic)));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.8f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul(BaseLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","+1","",   "+1","","+1","",
                        "R1","","","",   "R","","","",

                        "R(BaseLine2)","","","",   "R","","D1","",
                        "","","R","",   "D1","","","",
                        "R","","+1","",   "+1","","+1","",
                        "R1","","","",   "R","","","",

                        "R(<200,-15>aLine)","","","",   "R","","D1(<300,-15>aLine)","",
                        "","","R","",   "D1(<400,-15>aLine)","","","",
                        "R","","+1(<300,15>aLine)","",   "+1","","+1","",
                        "R1(<400,-15>aLine)","","","",   "R(<200,15>aLine)","","","",

                        "R(<440,15>aLine)","","","",   "R","","D1(<340,15>aLine)","",
                        "","","R","",   "D1(<240,15>aLine)","","","",
                        "(*R1)(+1)(*+11)(<380,10,60>aLine2)","~_-1(<350,10,30>aLine2)","~_+0(<320,10,0>aLine2)","~_+0(<290,10,-30>aLine2)",   "~_+0(<260,10,-60>aLine2)","","","",
                        "(*R1)(-1)(*-11)(<260,-10,-60>aLine2)","~_+1(<290,-10,-30>aLine2)","~_+0(<320,-10,0>aLine2)","~_+0(<350,-10,30>aLine2)",   "~_+0(<380,-10,60>aLine2)","","","",
                        //
                        "(*$01)(*$21)(<0.8,2>Drum)","","","",   "$1","","","",
                        "(*$01)(*$21)(<0.8,-2>Drum)","","","",   "$3","","","",
                        "(*$01)(*$21)(<0.8,2>Drum)","","","",   "$1","","","",
                        "(*$01)(*$21)(<0.8,-2>Drum)","","","",   "$3","","","",

                        "(*$0)(*$2)(<0.8,2>Drum)","","","",   "$11","","","",
                        "(*$0)(*$2)(<0.8,-2>Drum)","","","",   "$31","","","",
                        "(*$0)(*$2)(<0.8,2>Drum)","","","",   "$11","","","",
                        "(*$0)(*$2)(<0.8,-2>Drum)","","","",   "$31","","","",

                        "(R)(+21)(<0.8,3>Drum2)","","","",   "(R)(+21)","","(-1)(+21)","",
                        "(<0.8,-3>Drum2)","","(R)(+21)","",   "(-1)(+21)","","","",
                        "($0)(+21)(<0.8,3>Drum2)","","(-1)(+21)","",   "(-1)(+21)","","(-1)(+21)","",
                        "(-1)(+21)(<0.8,-3>Drum2)","","","",   "(R)(+21)","","","",

                        "R(<0>Scrangle)","","","",   "R","","+1","",
                        "+1","","","",   "R","","","",
                        "R","","","",   "R","","-1","",
                        "-1","","","",   "R","","","",
                        });
                }
                if (InBeat(220))
                {
                    Arrow.UnitEasing ar = new();
                    AddInstance(ar);
                    float RunTime = BeatTime(4);
                    ar.ApplyTime = RunTime;
                    ar.PositionEase = LinkEase(Stable(BeatTime(3), new Vector2(0, -260)), EaseOut(BeatTime(1), new Vector2(0, 260), EaseState.Sine));
                    ar.TagApply("X");
                    Arrow.UnitEasing ar2 = new();
                    AddInstance(ar2);
                    float RunTime2 = BeatTime(4);
                    ar2.ApplyTime = RunTime2;
                    ar2.PositionEase = LinkEase(Stable(BeatTime(3), new Vector2(0, 260)), EaseOut(BeatTime(1), new Vector2(0, -260), EaseState.Sine));
                    ar2.TagApply("Y");
                    RegisterFunctionOnce("ThreeLine1", () =>
                    {
                        var ce = LinkEase(Stable(0, new Vector2(480 - Arguments[0], 240)));
                        var re = LinkEase(Stable(0, 90));
                        Line l = new(ce, re);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ThreeLine2", () =>
                    {
                        var ce = LinkEase(Stable(0, new Vector2(160 + Arguments[0], 240)));
                        var re = LinkEase(Stable(0, 90));
                        Line l = new(ce, re);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("SCR", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, EaseOut(BeatTime(Arguments[0]), Arguments[1], EaseState.Sine), EaseOut(BeatTime(Arguments[0]), -Arguments[1], EaseState.Sine));
                    });
                    RegisterFunctionOnce("RSA", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-4.0f, 4.0f);
                    });
                    RegisterFunctionOnce("ArrowLine1", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 3.1f, 560)),
                            Linear(BeatTime(1), new Vector2(-BeatTime(1) * 3.1f, 0)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 3.1f, 560)),
                            Combine(Linear(BeatTime(1), -BeatTime(1) * 3.1f), EaseOut(BeatTime(1), -640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ArrowLine2", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f, 560)),
                            Linear(BeatTime(1), new Vector2(BeatTime(1) * 3.1f, 0)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f, 560)),
                            Combine(Linear(BeatTime(1), BeatTime(1) * 3.1f), EaseOut(BeatTime(1), -640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ArrowLine3", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 3.1f, -80)),
                            Linear(BeatTime(1), new Vector2(-BeatTime(1) * 3.1f, -80)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 3.1f, -80)),
                            Combine(Linear(BeatTime(1), -BeatTime(1) * 3.1f), EaseOut(BeatTime(1), 640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ArrowLine4", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f, -80)),
                            Linear(BeatTime(1), new Vector2(BeatTime(1) * 3.1f, -80)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f, -80)),
                            Combine(Linear(BeatTime(1), BeatTime(1) * 3.1f), EaseOut(BeatTime(1), 640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                        {
                        "(<1,-5>SCR)(ArrowLine1)","","","",   "","","","",
                        "$0'0.5@X","","","",   "","","","",
                        "R","","+1","",   "","","R1","",
                        "","","R","",   "+1","","+1","",

                        "(<1,5>SCR)(ArrowLine2)","","","",   "","","","",
                        "$2'0.5@X","","","",   "","","","",
                        "R","","-1","",   "","","R1","",
                        "","","R","",   "-1","","-1","",

                        "(<1,-5>SCR)(ArrowLine3)","","","",   "","","","",
                        "$01'0.5@Y","","","",   "","","","",
                        "R1","","+11","",   "","","R","",
                        "","","R1","",   "+01","","+01","",

                        "($0)($0)(<-3>Scrangle)(<0>ThreeLine1)","$1(<-2>Scrangle)(<30>ThreeLine1)","$2(<-1>Scrangle)(<60>ThreeLine1)","",   "$21($21)(<3>Scrangle)(<0>ThreeLine2)","$31(<2>Scrangle)(<30>ThreeLine2)","$01(<1>Scrangle)(<60>ThreeLine2)","",
                        "$0($0)(<-3>Scrangle)(<0>ThreeLine1)","$1(<-2>Scrangle)(<30>ThreeLine1)","$2(<-1>Scrangle)(<60>ThreeLine1)","",   "$21($21)(<3>Scrangle)(<0>ThreeLine2)","$31(<2>Scrangle)(<30>ThreeLine2)","$01(<1>Scrangle)(<60>ThreeLine2)","",
                        "$0($0)(<-3>Scrangle)(<0>ThreeLine1)","$1(<-2>Scrangle)(<30>ThreeLine1)","$2(<-1>Scrangle)(<60>ThreeLine1)","",   "$21($21)(<3>Scrangle)(<0>ThreeLine2)","$31(<2>Scrangle)(<30>ThreeLine2)","$01(<1>Scrangle)(<60>ThreeLine2)","",
                        "$0($0)(<-3>Scrangle)(<0>ThreeLine1)","$1(<-2>Scrangle)(<30>ThreeLine1)","$2(<-1>Scrangle)(<60>ThreeLine1)","",   "$21($21)(<3>Scrangle)(<0>ThreeLine2)","$31(<2>Scrangle)(<30>ThreeLine2)","$01(<1>Scrangle)(<60>ThreeLine2)","",
                        //
                        "(ArrowLine4)(<0>Scrangle)(<1,5>SCR)","","","",   "","","","",
                        "$21'0.5@Y","","","",   "","","","",
                        "R1","","-11","",   "","","R","",
                        "","","R1","",   "-11","","-11","",

                        "(ArrowLine1)(ArrowLine4)(<1,-5>SCR)","","","",   "","","","",
                        "$21'0.5@Y($0'0.5@X)","","","",   "","","","",
                        "(R1)(R)","","+1","",   "","","(R1)(R)","",
                        "","","R","",   "+1","","+1","",

                        "(ArrowLine2)(ArrowLine3)(<1,5>SCR)","","","",   "","","","",
                        "$2'0.5@X($01'0.5@Y)","","","",   "","","","",
                        "(R)(R1)","","+11","",   "","","(R1)(R)","",
                        "","","R1","",   "+11","","+11","",

                        "R","","","",   "+1","","","",
                        "+1","","","",   "+1","","$0'0.5@X(<140>ThreeLine1)","$0'0.5@X(<110>ThreeLine1)",
                        "$0'0.5@X(<80>ThreeLine1)","","","",   "$21'0.5@Y(<100>ThreeLine2)","$21'0.5@Y(<40>ThreeLine2)","$21'0.5@Y(<70>ThreeLine2)","",
                        "","","","",   "R","","","",
                       });
                }
                if (InBeat(252))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(320 - 120, 650, 240 - 60, 240 + 60);
                        Heart.RotateTo(270);
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(-10, 320 + 120, 240 - 60, 240 + 60);
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
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
                    RegisterFunctionOnce("PlaySound", () =>
                    {
                        PlaySound(Sounds.pierce);
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
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
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
                        "","","PlaySound","",   "","","","",
                        "PlaySound","","","",   "","","PlaySound","",
                        "","","","",   "PlaySound","","","",
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
                        "","",$"PlaySound","",   "","","","",
                        $"PlaySound","","","",   "","",$"PlaySound(Over)","",
                        "","","","",   $"PlaySound","","","",
                        //
                       
                    });
                }
                if (InBeat(284))
                {
                    Arrow.UnitEasing ar = new();
                    AddInstance(ar);
                    float RunTime = BeatTime(8);
                    ar.ApplyTime = RunTime;
                    ar.RotationEase = LinkEase(Stable(BeatTime(7.3f), 90), EaseOut(BeatTime(0.7f), -70, EaseState.Cubic));
                    ar.TagApply("L");
                    Arrow.UnitEasing ar2 = new();
                    AddInstance(ar2);
                    float RunTime2 = BeatTime(8);
                    ar2.ApplyTime = RunTime2;
                    ar2.RotationEase = LinkEase(Stable(BeatTime(7.3f), -90), EaseOut(BeatTime(0.7f), 70, EaseState.Cubic));
                    ar2.TagApply("R");
                    RegisterFunctionOnce("DAL", () =>
                    {
                        Line l = new(Arguments[0], 90);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("TP", () =>
                    {
                        InstantSetBox(10086, 114, 514);
                        InstantTP(new Vector2(1919, 810));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(0.5f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7f, new string[]
                        {
                        "$1","","","",   "(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","","(N11)(+01)","",
                        "","","(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","",   "(N11)(+01)","","","",
                        "(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","","(N11)(+01)","",   "","","(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","",
                        "(N11)(+01)","","","",   "(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","","(N11)(+01)","",

                        "(+01)(+01)","","","",   "(<0.5,-2>Drum2)(*$0'1.5@R)(*!$3)(*$2'1.5@L)","","(N31)(+01)","",
                        "","","(<0.5,-2>Drum2)(*$0'1.5@R)(*!$3)(*$2'1.5@L)","",   "(N31)(+01)","","","",
                        "(<0.5,-2>Drum2)(*$0'1.5@R)(*!$3)(*$2'1.5@L)","","(N31)(+01)","",   "","","(<0.5,-2>Drum2)(*$0'1.5@R)(*!$3)(*$2'1.5@L)","",
                        "(N31)(+01)","","","",   "(<0.5,-2>Drum2)(*$0'1.5@R)(*!$3)(*$2'1.5@L)","","(N31)(+01)","",

                        "(+01)(+01)","","","",   "(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","","(N11)(+01)","",
                        "","","(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","",   "(N11)(+01)","","","",
                        "(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","","(N11)(+01)","",   "","","(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","",
                        "(N11)(+01)","","","",   "(<0.5,2>Drum2)(*$0'1.5@L)(*!$1)(*$2'1.5@R)","","(N11)(+01)","",

                        "(+01)(+01)","","","",   "$2","","$01","",
                        "","","","",   "($21)(+01)","","","",
                        "$0","","","",   "($0@L)($21@L)(<15>Scrangle)","($0@R)($21@R)(<10>Scrangle)","($0@L)($21@L)(<5>Scrangle)","($0@R)($21@R)(<3>Scrangle)",
                        "($0@L)($21@L)($0@R)($21@R)(<-3>Scrangle)","","","",   "($21)($21)(<0>Scrangle)","","","",
                        //
                        "$11","","","",   "(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","","(N1)(+0)","",
                        "","","(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","",   "(N1)(+0)","","","",
                        "(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","","(N1)(+0)","",   "","","(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","",
                        "(N1)(+0)","","","",   "(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","","(N1)(+0)","",

                        "(+0)(+0)","","","",   "(<0.5,-2>Drum2)(*$01'1.5@R)(*!$31)(*$21'1.5@L)","","(N3)(+0)","",
                        "","","(<0.5,-2>Drum2)(*$01'1.5@R)(*!$31)(*$21'1.5@L)","",   "(N3)(+0)","","","",
                        "(<0.5,-2>Drum2)(*$01'1.5@R)(*!$31)(*$21'1.5@L)","","(N3)(+0)","",   "","","(<0.5,-2>Drum2)(*$01'1.5@R)(*!$31)(*$21'1.5@L)","",
                        "(N3)(+0)","","","",   "(<0.5,-2>Drum2)(*$01'1.5@R)(*!$31)(*$21'1.5@L)","","(N3)(+0)","",

                        "(+0)(+0)","","","",   "(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","","(N1)(+0)","",
                        "","","(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","",   "(N1)(+0)","","","",
                        "(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","","(N1)(+0)","",   "","","(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","",
                        "(N1)(+0)","","","",   "(<0.5,2>Drum2)(*$01'1.5@L)(*!$11)(*$21'1.5@R)","","(N1)(+0)","",

                        "Expand(*$01)(*+21)","<0>Scrangle","","",   "Expand(*$01)(*+21)","","","",
                        "Expand(*$01)(*+21)","","","",   "Expand(*$01)(*+21)","","","",
                        "Expand(*$01)(*+21)","","","",   "Expand(*$01)(*+21)","","","",
                        "Expand(*$01)(*+21)","","","",   "Expand(*$01)(*+21)","","","",

                        "<80>DAL","","","",   "<160>DAL","","","",
                        "<240>DAL","","","",   "<320>DAL","","<270>DAL","<290>DAL",
                        "<310>DAL","","","",   "<470>DAL","<490>DAL","<510>DAL","",
                        "","","","",   "<360>DAL","","","",

                        "<300>DAL","","<380>DAL","",   "<220>DAL","","","",
                        "(<75>DAL)","","","",   "<130>DAL","","","",
                        "<600>DAL","<580>DAL","<560>DAL","",   "","","<380>DAL","<360>DAL",
                        "<340>DAL","","","",   "<140>DAL(TP)","","","",
                       });
                }
            }
            public void Easy()
            {
                if (InBeat(0))
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
                                    EaseIn(BeatTime(2f), new Vector2(330, 0), EaseState.Quad),
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
                    CreateChart(0, BeatTime(1), 6, new string[]
                    {
                        "Stable(ChordLine1)","","<180>Angle","",   "Mark","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "(ChordLine2)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Block2)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(28))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        Heart.RotateTo(360);
                        SetSoul(1);
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(2), true);
                    });
                    Arrow.UnitEasing ease;
                    AddInstance(ease = new Arrow.UnitEasing() { ApplyTime = BeatTime(2f) });
                    ease.RotationEase = LinkEase(Stable(0, 15), EaseOut(BeatTime(2f), -15, EaseState.Sine));
                    ease.TagApply("L");
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","+0","",   "+0","","+0","",
                        "+0","","","",   "R","","","",

                        "R","","","",   "R","","+0@L","",
                        "","","R","",   "+0@L","","","",
                        "R","","+0","",   "+0","","+0","",
                        "+0","","","",   "R","","","",

                        "R","","","",   "R","","+0@L","",
                        "","","R","",   "+0@L","","","",
                        "R","","+0","",   "+0","","+0","",
                        "R","","","",   "R","","","",

                        "R","","","",   "R","","+0@L","",
                        "","","R","",   "+0@L","","","",
                        "R","~_+0","~_+0","~_+0",   "~_+0","","","",
                        "R","~_+0","~_+0","~_+0",   "~_+0","","","",
                        //
                        "$0@L","","","",   "$1","","","",
                        "$2@L","","","",   "$3","","","",
                        "$0@L","","","",   "$1","","","",
                        "$2@L","","","",   "$3","","","",

                        "$0@L","","","",   "$3","","","",
                        "$2@L","","","",   "$1","","","",
                        "$0@L","","","",   "$3","","","",
                        "$2@L","","","",   "$1","","","",

                        "R","","","",   "R","","+0@L","",
                        "","","R","",   "+0@L","","","",
                        "$0","","+0","",   "+0","","+0","",
                        "+2","","","",   "+2","","","",

                        "R","","","",   "R","","+0@L","",
                        "+0@L","","","",   "R","","","",
                        "R","","","",   "R","","+0@L","",
                        "+0@L","","","",   "R","","","",
                        //
                    });
                }
                if (InBeat(60))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(320 - 120, 650, 240 - 60, 240 + 60);
                        Heart.RotateTo(270);
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(-10, 320 + 120, 240 - 60, 240 + 60);
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
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
                    RegisterFunctionOnce("PlaySound", () =>
                    {
                        PlaySound(Sounds.pierce);
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
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul","","","",   $"(Block{rand1.X})","","","",
                        "","",$"(Block{rand1.X})","",   "","","","",
                        $"(Block{rand1.X})","","","",   "","",$"(Block{rand1.X})","",
                        "","","","",   $"(Block{rand1.X})","","","",

                        "","","","",   $"(Block{rand1.Y})","","","",
                        "","",$"(Block{rand1.Y})","",   "","","","",
                        $"(Block{rand1.Y})","","","",   "","",$"(Block{rand1.Y})","",
                        "","","","",   $"(Block{rand1.Y})","","","",

                        "","","","",   $"(Block{rand1.Z})","","","",
                        "","",$"(Block{rand1.Z})","",   "","","","",
                        $"(Block{rand1.Z})","","","",   "","",$"(Block{rand1.Z})","",
                        "","","","",   $"(Block{rand1.Z})","","","",

                        "","","","",   $"(Block{rand1.Y})","","","",
                        "","","PlaySound","",   "","","","",
                        "PlaySound","","","",   "","","PlaySound","",
                        "","","","",   "PlaySound","","","",
                        //
                        "Change","","","",   $"(sBlock{rand5.X})","","","",
                        "","",$"(sBlock{rand5.X})","",   "","","","",
                        $"(sBlock{rand5.X})","","","",   "","",$"(sBlock{rand5.X})","",
                        "","","","",   $"(sBlock{rand5.X})","","","",

                        "","","","",   $"(sBlock{rand5.Y})","","","",
                        "","",$"(sBlock{rand5.Y})","",   "","","","",
                        $"(sBlock{rand5.Y})","","","",   "","",$"(sBlock{rand5.Y})","",
                        "","","","",   $"(sBlock{rand5.Y})","","","",

                        "","","","",   $"(sBlock{rand5.Z})","","","",
                        "","",$"(sBlock{rand5.Z})","",   "","","","",
                        $"(sBlock{rand5.Z})","","","",   "","",$"(sBlock{rand5.Z})","",
                        "","","","",   $"(sBlock{rand5.Z})","","","",

                        "","","","",   $"(sBlock{rand5.Y})","","","",
                        "","",$"PlaySound","",   "","","","",
                        $"PlaySound","","","",   "","",$"PlaySound","",
                        "","","","",   $"PlaySound","","","",
                        //
                        "Over"
                    });
                }
                if (InBeat(92))
                {
                    RegisterFunctionOnce("LKickLine", () =>
                    {
                        var cr = LinkEase(EaseOut(BeatTime(1), new Vector2(650, 0), EaseState.Quart));
                        Line l = new(cr, Stable(0, 90));
                        CreateEntity(l);
                        l.DelayDispose(BeatTime(1) + 1);
                        l.AddShadow(4, 0.6f);
                        l.AddShadow(8, 0.3f);
                    });
                    RegisterFunctionOnce("RKickLine", () =>
                    {
                        var cr = LinkEase(Stable(0, new Vector2(640, 0)), EaseOut(BeatTime(1), new Vector2(-650, 0), EaseState.Quart));
                        Line l = new(cr, Stable(0, 90));
                        CreateEntity(l);
                        l.DelayDispose(BeatTime(1) + 1);
                        l.AddShadow(4, 0.6f);
                        l.AddShadow(8, 0.3f);
                    });
                    RegisterFunctionOnce("RSA", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-4.0f, 4.0f);
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "+0","","","",
                        "+0","","","",   "","","","",

                        "(R)(RSA)","","","",   "(D)(RSA)","","","",
                        "(D)(RSA)","","","",   "(D)(RSA)","","","",
                        "D(RSA)","","","",   "+0(RSA)","","","",
                        "+0(<0>Scrangle)","","","",   "+0","","","",

                        "(D)(+01)(<0.5,3>Drum)(LKickLine)","","","",   "","","(D)(+01)(<0.75,-3>Drum)(LKickLine)","",
                        "","","","",   "","","","",
                        "(D)(+01)(<0.5,3>Drum)(LKickLine)","","","",   "","","(D)(+01)(<0.75,-3>Drum)(LKickLine)","",
                        "","","","",   "","","","",

                        "(D)(+01)(<0.5,3>Drum)(LKickLine)","","","",   "","","(D)(+01)(<0.5,-3>Drum)(RKickLine)","",
                        "","","","",   "(D)(+01)(<0.5,3>Drum)(LKickLine)","","","",
                        "(D)(+01)(<0.75,-3>Drum)(RKickLine)","","","",   "","","","",
                        "(D)(+01)(<0.75,-3>Drum)(RKickLine)","","","",   "","","","",
                        //
                        "R","","","",   "+0","","","",
                        "+0","","","",   "","","","",
                        "D","","","",   "+0","","","",
                        "+0","","","",   "","","","",

                        "(R)(RSA)","","","",   "(D)(RSA)","","","",
                        "(D)(RSA)","","","",   "(D)(RSA)","","","",
                        "D(RSA)","","","",   "+0(RSA)","","","",
                        "+0(<0>Scrangle)","","","",   "+0","","","",

                        "(D)(+01)(<0.5,-3>Drum)(RKickLine)","","","",   "","","(D)(+01)(<0.75,-3>Drum)(RKickLine)","",
                        "","","","",   "","","","",
                        "(D)(+01)(<0.5,-3>Drum)(RKickLine)","","","",   "","","(D)(+01)(<0.75,-3>Drum)(RKickLine)","",
                        "","","","",   "","","","",

                        "(D)(+01)(<0.5,-3>Drum)(RKickLine)","","","",   "","","(D)(+01)(<0.5,3>Drum)(LKickLine)","",
                        "","","","",   "(D)(+01)(<0.5,-3>Drum)(RKickLine)","","","",
                        "(D)(+01)(<0.75,3>Drum)(LKickLine)","","","",   "","","","",
                        "(D)(+01)(<0.75,3>Drum)(LKickLine)","","","",   "","","","",
                    });
                }
                if (InBeat(124))
                {
                    RegisterFunctionOnce("Mtt", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(-10, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.InsertShot(8, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Mtt2", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(650, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(3)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        //mt.BulletDepth = 0.49f;
                        mt.InsertShot(8, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Bomb", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(-10, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    RegisterFunctionOnce("Bomb2", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(-10, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(2)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "(#4#R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "(+0)","","","",
                        "(+1)","","","",   "(+1)","","","",
                        "(#1#+1)","","","",   "","","","",
                        "+0","","","",   "(#4#+1)","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "+1","","","",
                        "+1","","","",   "+1","","","",
                        "(#1#-1)","","","",   "","","","",
                        "(#1#-1)","","","",   "","","","",
                        //
                        "(R)(+01)","","","",   "(+1)(+01)","","","",
                        "(+1)(+01)","","","",   "(+1)(+01)","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "R","","","",

                        "-1","","","",   "","","","",
                        "+2","","","",  "","","","",
                        "+1","","","",   "","","+1","",
                        "","","","",   "R","+0","#3#+0","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(#1#+2)","","","",   "","","","",

                        "(#1#+0)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(+2)","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(156))
                {
                    RegisterFunctionOnce("Mtt", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(-10, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.InsertShot(8, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Mtt2", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new Vector2(650, 240 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(3)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.InsertShot(8, BeatTime(2f));
                    });
                    RegisterFunctionOnce("Bomb", () =>
                    {
                        float speed = Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(-10, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    RegisterFunctionOnce("Bomb2", () =>
                    {
                        float speed = -Rand(2.50f, 3.50f);
                        float offset = Rand(-80, 80);
                        var ce2 = LinkEase(Stable(0, new Vector2(650, 200 + offset)),
                          Combine(InfLinear(0, speed), SineWave(80, BeatTime(6), 99999, BeatTime(2)))
                            );
                        CreateEntity(new Bomb(20, ce2) { Depth = 0.39f });
                    });
                    Arrow.UnitEasing ease;
                    AddInstance(ease = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease.RotationEase = LinkEase(Stable(BeatTime(1), 30), EaseOut(BeatTime(1), -30, EaseState.Back));
                    ease.TagApply("s");
                    Arrow.UnitEasing ease2;
                    AddInstance(ease2 = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease2.RotationEase = LinkEase(Stable(BeatTime(1), 60), EaseOut(BeatTime(1), -60, EaseState.Elastic));
                    //ease2.TagApply("ss");
                    Arrow.UnitEasing ease3;
                    AddInstance(ease3 = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease3.RotationEase = LinkEase(Stable(BeatTime(1), -30), EaseOut(BeatTime(1), 30, EaseState.Back));
                    //ease3.TagApply("w");
                    Arrow.UnitEasing ease4;
                    AddInstance(ease4 = new Arrow.UnitEasing() { ApplyTime = BeatTime(2) });

                    ease4.RotationEase = LinkEase(Stable(BeatTime(1), -60), EaseOut(BeatTime(1), 60, EaseState.Elastic));
                    //ease4.TagApply("ww");
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "$3@s","","","",   "","","$1@s","",
                        "","","","",   "$3@s","","","",
                        "","","$1@s","",   "","","","",
                        "$3@s","","","",   "$1@s","","","",

                        "$3@s","","","",   "","","$1@s","",
                        "","","","",   "$3@s","","","",
                        "","","$1@s","",   "","","","",
                        "$3@s","","","",   "$1@s","","","",

                        "$3@s","","","",   "","","$1@s","",
                        "","","","",   "$3@s","","","",
                        "","","$1@s","",   "","","","",
                        "$3@s","","","",   "$1@s","","","",

                        "$3@s","","","",   "","","$1@s","",
                        "","","","",   "$3@s","","","",
                        "","","$1@s","",   "","","","",
                        "$3@s","","","",   "$1@s","","","",
                        //
                        "$11@s","","","",   "","","$31@s","",
                        "","","","",   "$11@s","","","",
                        "","","$31@s","",   "","","","",
                        "$11@s","","","",   "$31@s","","","",

                        "$11@s","","","",   "","","$31@s","",
                        "","","","",   "$11@s","","","",
                        "","","$31@s","",   "","","","",
                        "$11@s","","","",   "$31@s","","","",

                        "$11@s","","","",   "","","$31@s","",
                        "","","","",   "$11@s","","","",
                        "","","$31@s","",   "","","","",
                        "$11@s","","","",   "$31@s","","","",

                        "$11@s","","","",   "","","$31@s","",
                        "","","","",   "$11@s","","","",
                        "","","$31@s","",   "","","","",
                        "$11@s","","","",   "$31@s","","","",
                    });
                }
                if (InBeat(188))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        Heart.RotateTo(360);
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("BaseLine", () =>
                    {
                        var ec = LinkEase(
                            Stable(0, new Vector2(0, 240)),
                            EaseOut(BeatTime(0.75f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(-180, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(-180, 0), EaseState.Quart)
                            );
                        Line l = new(ec.Easing, Stable(0, 90));
                        CreateEntity(l);
                        l.AddShadow(2, 0);
                        l.AddShadow(4, 0);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                        var sc = LinkEase(
                            Stable(0, 0),
                            EaseOut(BeatTime(0.75f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), 3f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), -1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), 3f, EaseState.Quart)
                            );
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, sc);
                    });
                    RegisterFunctionOnce("BaseLine2", () =>
                    {
                        var ec = LinkEase(
                            Stable(0, new Vector2(640, 240)),
                            EaseOut(BeatTime(0.75f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.75f), new Vector2(180, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(-90, 0), EaseState.Quart),
                            EaseOut(BeatTime(0.5f), new Vector2(180, 0), EaseState.Quart)
                            );
                        Line l = new(ec.Easing, Stable(0, 90));
                        CreateEntity(l);
                        l.AddShadow(4, 0);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                        var sc = LinkEase(
                            Stable(0, 0),
                            EaseOut(BeatTime(0.75f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), -3f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), 1.5f, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), -3f, EaseState.Quart)
                            );
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, sc);
                    });
                    RegisterFunctionOnce("aLine", () =>
                    {
                        Line l = new(Stable(0, new Vector2(Arguments[0], 240)).Easing, LinkEase(Stable(0, 90), EaseOut(BeatTime(0.5f), Arguments[1], EaseState.Cubic)));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.8f));
                    });
                    RegisterFunctionOnce("aLine2", () =>
                    {
                        Line l = new(LinkEase(Stable(0, new Vector2(Arguments[0], 240)), EaseOut(BeatTime(0.8f), new Vector2(Arguments[2], 0), EaseState.Cubic)).Easing, LinkEase(Stable(0, 90), EaseOut(BeatTime(0.5f), Arguments[1], EaseState.Cubic)));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.8f));
                    });
                    Arrow.UnitEasing ease;
                    AddInstance(ease = new Arrow.UnitEasing() { ApplyTime = BeatTime(2f) });
                    ease.RotationEase = LinkEase(Stable(0, 15), EaseOut(BeatTime(2f), -15, EaseState.Sine));
                    ease.TagApply("L");
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul(BaseLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","+0","",   "+0","","+0","",
                        "+2","","","",   "R","","","",

                        "R(BaseLine2)","","","",   "R","","+0@L","",
                        "","","R","",   "+0@L","","","",
                        "R","","+0","",   "+0","","+0","",
                        "+2","","","",   "R","","","",

                        "R(<200,-15>aLine)","","","",   "R","","+0@L(<300,-15>aLine)","",
                        "","","R","",   "+0@L(<400,-15>aLine)","","","",
                        "R","","+0(<300,15>aLine)","",   "+0","","+0","",
                        "+2(<400,-15>aLine)","","","",   "R(<200,15>aLine)","","","",

                        "R(<440,15>aLine)","","","",   "R","","+0@L(<340,15>aLine)","",
                        "","","R","",   "+0@L(<240,15>aLine)","","","",
                        "(R)(<380,10,60>aLine2)","~_-0(<350,10,30>aLine2)","~_+0(<320,10,0>aLine2)","~_+0(<290,10,-30>aLine2)",   "~_+0(<260,10,-60>aLine2)","","","",
                        "(R)(<260,-10,-60>aLine2)","~_+0(<290,-10,-30>aLine2)","~_+0(<320,-10,0>aLine2)","~_+0(<350,-10,30>aLine2)",   "~_+0(<380,-10,60>aLine2)","","","",
                        //
                        "$0@L(<0.8,2>Drum)","","","",   "$1","","","",
                        "$2@L(<0.8,-2>Drum)","","","",   "$3","","","",
                        "$0@L(<0.8,2>Drum)","","","",   "$1","","","",
                        "$2@L(<0.8,-2>Drum)","","","",   "$3","","","",

                        "$0@L(<0.8,2>Drum)","","","",   "$3","","","",
                        "$2@L(<0.8,-2>Drum)","","","",   "$1","","","",
                        "$0@L(<0.8,2>Drum)","","","",   "$3","","","",
                        "$2@L(<0.8,-2>Drum)","","","",   "$1","","","",

                        "(R)(<0.8,3>Drum2)","","","",   "(R)","","+1","",
                        "(<0.8,-3>Drum2)","","(R)","",   "-1","","","",
                        "R(<0.8,3>Drum2)","","(+0)","",   "(+0)","","(+0)","",
                        "(+2)(<0.8,-3>Drum2)","","","",   "(R)","","","",

                        "R(<0>Scrangle)","","","",   "R","","+0","",
                        "+1","","","",   "R","","","",
                        "R","","","",   "R","","+0","",
                        "-1","","","",   "R","","","",
                        });
                }
                if (InBeat(220))
                {
                    Arrow.UnitEasing ar = new();
                    AddInstance(ar);
                    float RunTime = BeatTime(4);
                    ar.ApplyTime = RunTime;
                    ar.PositionEase = LinkEase(Stable(BeatTime(3), new Vector2(0, -260)), EaseOut(BeatTime(1), new Vector2(0, 260), EaseState.Sine));
                    ar.TagApply("X");
                    Arrow.UnitEasing ar2 = new();
                    AddInstance(ar2);
                    float RunTime2 = BeatTime(4);
                    ar2.ApplyTime = RunTime2;
                    ar2.PositionEase = LinkEase(Stable(BeatTime(3), new Vector2(0, 260)), EaseOut(BeatTime(1), new Vector2(0, -260), EaseState.Sine));
                    ar2.TagApply("Y");
                    RegisterFunctionOnce("ThreeLine1", () =>
                    {
                        var ce = LinkEase(Stable(0, new Vector2(480 - Arguments[0], 240)));
                        var re = LinkEase(Stable(0, 90));
                        Line l = new(ce, re);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ThreeLine2", () =>
                    {
                        var ce = LinkEase(Stable(0, new Vector2(160 + Arguments[0], 240)));
                        var re = LinkEase(Stable(0, 90));
                        Line l = new(ce, re);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("SCR", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, EaseOut(BeatTime(Arguments[0]), Arguments[1], EaseState.Sine), EaseOut(BeatTime(Arguments[0]), -Arguments[1], EaseState.Sine));
                    });
                    RegisterFunctionOnce("RSA", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-4.0f, 4.0f);
                    });
                    RegisterFunctionOnce("ArrowLine1", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 6.2f, 560)),
                            Linear(BeatTime(1), new Vector2(-BeatTime(1) * 6.2f, 0)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 6.2f, 560)),
                            Combine(Linear(BeatTime(1), -BeatTime(1) * 6.2f), EaseOut(BeatTime(1), -640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ArrowLine2", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f * 2, 560)),
                            Linear(BeatTime(1), new Vector2(BeatTime(1) * 3.1f * 2, 0)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f * 2, 560)),
                            Combine(Linear(BeatTime(1), BeatTime(1) * 3.1f * 2), EaseOut(BeatTime(1), -640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ArrowLine3", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 3.1f * 2, -80)),
                            Linear(BeatTime(1), new Vector2(-BeatTime(1) * 3.1f * 2, -80)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 + 42 - 3 + BeatTime(1) * 3.1f * 2, -80)),
                            Combine(Linear(BeatTime(1), -BeatTime(1) * 3.1f * 2), EaseOut(BeatTime(1), 640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(-150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(-42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ArrowLine4", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f * 2, -80)),
                            Linear(BeatTime(1), new Vector2(BeatTime(1) * 3.1f * 2, -80)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        var ce2 = LinkEase(
                            Stable(0, new Vector2(320 - 42 + 3 - BeatTime(1) * 3.1f * 2, -80)),
                            Combine(Linear(BeatTime(1), BeatTime(1) * 3.1f * 2), EaseOut(BeatTime(1), 640, EaseState.Quad)),
                            EaseOut(BeatTime(1), new Vector2(150, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(45, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.5f), new Vector2(42, 0), EaseState.Quad),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine),
                            EaseOut(BeatTime(0.25f), new Vector2(-470 / 3f, 0), EaseState.Sine)
                            );
                        Line l = new(ce, ce2);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(0.5f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                        {
                        "(<1,-5>SCR)(ArrowLine1)","","","",   "","","","",
                        "$0@X","","","",   "","","","",
                        "R","","+0","",   "","","R","",
                        "","","R","",   "+0","","+0","",

                        "(<1,5>SCR)(ArrowLine2)","","","",   "","","","",
                        "$2@X","","","",   "","","","",
                        "R","","+0","",   "","","R","",
                        "","","R","",   "-0","","-0","",

                        "(<1,-5>SCR)(ArrowLine3)","","","",   "","","","",
                        "$0@Y","","","",   "","","","",
                        "R","","+0","",   "","","R","",
                        "","","R","",   "+0","","+0","",

                        "$0(<-1>Scrangle)(<0>ThreeLine1)","","","",   "$21(<1>Scrangle)(<0>ThreeLine2)","","","",
                        "$0(<-1>Scrangle)(<0>ThreeLine1)","","","",   "$21(<1>Scrangle)(<0>ThreeLine2)","","","",
                        "$0(<-1>Scrangle)(<0>ThreeLine1)","","","",   "$21(<1>Scrangle)(<0>ThreeLine2)","","","",
                        "$0(<-1>Scrangle)(<0>ThreeLine1)","","","",   "$21(<1>Scrangle)(<0>ThreeLine2)","","","",
                        //
                        "(ArrowLine4)(<0>Scrangle)(<1,5>SCR)","","","",   "","","","",
                        "$2@Y","","","",   "","","","",
                        "R","","-1","",   "","","R","",
                        "","","R","",   "+0","","-1","",

                        "(ArrowLine1)(<1,-5>SCR)","","","",   "","","","",
                        "$0@X","","","",   "","","","",
                        "(R)","","+1","",   "","","(R)","",
                        "","","R","",   "+0","","+1","",

                        "(ArrowLine2)(<1,5>SCR)","","","",   "","","","",
                        "$2@X","","","",   "","","","",
                        "(R)","","+1","",   "","","(R)","",
                        "","","R","",   "+1","","+1","",

                        "R","","","",   "+1","","","",
                        "+1","","","",   "+1","","$0@X(<140>ThreeLine1)","$0@X(<110>ThreeLine1)",
                        "$0@X(<80>ThreeLine1)","","","",   "$2@Y(<100>ThreeLine2)","$2@Y(<40>ThreeLine2)","$2@Y(<70>ThreeLine2)","",
                        "","","","",   "R","","","",
                       });
                }
                if (InBeat(252))
                {
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(320 - 120, 650, 240 - 60, 240 + 60);
                        Heart.RotateTo(270);
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        SetSoul(Souls.YellowSoul);
                        SetBox(-10, 320 + 120, 240 - 60, 240 + 60);
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new Vector2(650, 240 - 24)), val, InfLinear(new Vector2(spd, 0)))) { AbleLink = false });
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
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(650, 240 - 48)), val, InfLinear(new Vector2(spd, 0)))));
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
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 24)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new Vector2(-10, 240 + 48)), vals, InfLinear(new Vector2(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new Vector2(-10, 240 - 48)), vals, InfLinear(new Vector2(spds, 0)))));
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
                    RegisterFunctionOnce("PlaySound", () =>
                    {
                        PlaySound(Sounds.pierce);
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
                    CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                        "SetSoul","","","",   $"(Block{rand1.X})","","","",
                        "","",$"(Block{rand1.X})","",   "","","","",
                        $"(Block{rand1.X})","","","",   "","",$"(Block{rand1.X})","",
                        "","","","",   $"(Block{rand1.X})","","","",

                        "","","","",   $"(Block{rand1.Y})","","","",
                        "","",$"(Block{rand1.Y})","",   "","","","",
                        $"(Block{rand1.Y})","","","",   "","",$"(Block{rand1.Y})","",
                        "","","","",   $"(Block{rand1.Y})","","","",

                        "","","","",   $"(Block{rand1.Z})","","","",
                        "","",$"(Block{rand1.Z})","",   "","","","",
                        $"(Block{rand1.Z})","","","",   "","",$"(Block{rand1.Z})","",
                        "","","","",   $"(Block{rand1.Z})","","","",

                        "","","","",   $"(Block{rand1.Y})","","","",
                        "","","PlaySound","",   "","","","",
                        "PlaySound","","","",   "","","PlaySound","",
                        "","","","",   "PlaySound","","","",
                        //
                        "Change","","","",   $"(sBlock{rand5.X})","","","",
                        "","",$"(sBlock{rand5.X})","",   "","","","",
                        $"(sBlock{rand5.X})","","","",   "","",$"(sBlock{rand5.X})","",
                        "","","","",   $"(sBlock{rand5.X})","","","",

                        "","","","",   $"(sBlock{rand5.Y})","","","",
                        "","",$"(sBlock{rand5.Y})","",   "","","","",
                        $"(sBlock{rand5.Y})","","","",   "","",$"(sBlock{rand5.Y})","",
                        "","","","",   $"(sBlock{rand5.Y})","","","",

                        "","","","",   $"(sBlock{rand5.Z})","","","",
                        "","",$"(sBlock{rand5.Z})","",   "","","","",
                        $"(sBlock{rand5.Z})","","","",   "","",$"(sBlock{rand5.Z})","",
                        "","","","",   $"(sBlock{rand5.Z})","","","",

                        "","","","",   $"(sBlock{rand5.Y})","","","",
                        "","",$"PlaySound","",   "","","","",
                        $"PlaySound","","","",   "","",$"PlaySound(Over)","",
                        "","","","",   $"PlaySound","","","",
                        //
                       
                    });
                }
                if (InBeat(284))
                {
                    Arrow.UnitEasing ar = new();
                    AddInstance(ar);
                    float RunTime = BeatTime(8);
                    ar.ApplyTime = RunTime;
                    ar.RotationEase = LinkEase(Stable(BeatTime(7.3f), 90), EaseOut(BeatTime(0.7f), -70, EaseState.Cubic));
                    ar.TagApply("L");
                    Arrow.UnitEasing ar2 = new();
                    AddInstance(ar2);
                    float RunTime2 = BeatTime(8);
                    ar2.ApplyTime = RunTime2;
                    ar2.RotationEase = LinkEase(Stable(BeatTime(7.3f), -90), EaseOut(BeatTime(0.7f), 70, EaseState.Cubic));
                    ar2.TagApply("R");
                    RegisterFunctionOnce("DAL", () =>
                    {
                        Line l = new(Arguments[0], 90);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("TP", () =>
                    {
                        InstantSetBox(10086, 114, 514);
                        InstantTP(new Vector2(1919, 810));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(0.5f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7f, new string[]
                        {
                        "$1","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",
                        "","","(<0.5,2>Drum2)(D)(+01)","",   "(+0)","","","",
                        "(<0.5,2>Drum2)(D)(+01)","","(+0)","",   "","","(<0.5,2>Drum2)(D)(+01)","",
                        "(+0)","","","",   "(<0.5,2>Drum2)(D)(+0)","","(+0)","",

                        "(+0)","","","",   "(<0.5,-2>Drum2)(D)(+01)","","(+01)","",
                        "","","(<0.5,-2>Drum2)(D)(+01)","",   "(+01)","","","",
                        "(<0.5,-2>Drum2)(D)(+01)","","(+01)","",   "","","(<0.5,-2>Drum2)(D)(+01)","",
                        "(+01)","","","",   "(<0.5,-2>Drum2)(D)(+01)","","(+01)","",

                        "(+01)","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",
                        "","","(<0.5,2>Drum2)(D)(+01)","",   "(+0)","","","",
                        "(<0.5,2>Drum2)(D)(+01)","","(+0)","",   "","","(<0.5,2>Drum2)(D)(+01)","",
                        "(+0)","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",

                        "(+0)","","","",   "+2","","+0","",
                        "","","","",   "+0","","","",
                        "($0)($21)","","","",   "($0@L)($21@L)(<15>Scrangle)","($0@R)($21@R)(<10>Scrangle)","($0@L)($21@L)(<5>Scrangle)","($0@R)($21@R)(<3>Scrangle)",
                        "($0@L)($21@L)($0@R)($21@R)(<-3>Scrangle)","","","",   "($21)($21)(<0>Scrangle)","","","",
                        //
                        "$21","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",
                        "","","(<0.5,2>Drum2)(D)(+01)","",   "(+0)","","","",
                        "(<0.5,2>Drum2)(D)(+01)","","(+0)","",   "","","(<0.5,2>Drum2)(D)(+01)","",
                        "(+0)","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",

                        "(+0)","","","",   "(<0.5,-2>Drum2)(D)(+01)","","(+01)","",
                        "","","(<0.5,-2>Drum2)(D)(+01)","",   "(+01)","","","",
                        "(<0.5,-2>Drum2)(D)(+01)","","(+01)","",   "","","(<0.5,-2>Drum2)(D)(+01)","",
                         "(+01)","","","",   "(<0.5,-2>Drum2)(D)(+01)","","(+01)","",

                        "(+0)","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",
                        "","","(<0.5,2>Drum2)(D)(+01)","",   "(+0)","","","",
                        "(<0.5,2>Drum2)(D)(+01)","","(+0)","",   "","","(<0.5,2>Drum2)(D)(+01)","",
                        "(+0)","","","",   "(<0.5,2>Drum2)(D)(+01)","","(+0)","",

                        "Expand($0)","<0>Scrangle","","",   "Expand($21)","","","",
                        "Expand($0)","","","",   "Expand($21)","","","",
                        "Expand($0)","","","",   "Expand($21)","","","",
                        "Expand($0)","","","",   "Expand($21)","","","",

                        "<80>DAL","","","",   "<160>DAL","","","",
                        "<240>DAL","","","",   "<320>DAL","","<270>DAL","<290>DAL",
                        "<310>DAL","","","",   "<470>DAL","<490>DAL","<510>DAL","",
                        "","","","",   "<360>DAL","","","",

                        "<300>DAL","","<380>DAL","",   "<220>DAL","","","",
                        "(<75>DAL)","","","",   "<130>DAL","","","",
                        "<600>DAL","<580>DAL","<560>DAL","",   "","","<380>DAL","<360>DAL",
                        "<340>DAL","","","",   "<140>DAL(TP)","","","",
                       });
                }
            }
            public void Start()
            {
                //黄魂射击十字炸弹那一段，当向左射击时弹幕会有概率不明所以地突然向左移动
                //(已修复)
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
                RegisterFunction("Drum2", () =>
                {
                    ScreenDrawing.ScreenAngle = 0;
                    float time = Arguments[0];
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    },
                    EaseOut(BeatTime(time) / 8f, Arguments[1], EaseState.Quad),
                    EaseIn(BeatTime(time) / 8f * 7, -Arguments[1], EaseState.Quad)
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
                bool jump = false;
                int jumpbeat = 284;
                HeartAttribute.MaxHP = 54;
                HeartAttribute.DamageTaken = 8;
                HeartAttribute.ArrowFixed = true;
                if (jump)
                {
                    GametimeDelta = -4.5f + BeatTime(jumpbeat);
                    PlayOffset = BeatTime(jumpbeat);
                    SetSoul(1);
                    //Heart.RotateTo(0);for(int i = 0; i < 5;i++) 
                }
            }
        }
    }
}