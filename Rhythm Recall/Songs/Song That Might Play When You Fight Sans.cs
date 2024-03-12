using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using V = Microsoft.Xna.Framework.Vector2;

namespace Rhythm_Recall.Waves
{
    internal class Song_That_Might_Play_When_You_Fight_Sans : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Song_That_Might_Play_When_You_Fight_Sans()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Hard);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (120f / 60f)) { }

            public string Music => "Song That Might Play When You Fight Sans";
            public string FightName => "Song That Might Play When You Fight Sans";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "TobyFox";
                public override string BarrageAuthor => "Tlott!Sans";
                public override string AttributeAuthor => "Papyrus's SOUL";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Hard, 14f),
                        new(Difficulty.Easy, 6f),
                    }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Hard, 14.5f),
                        new(Difficulty.Easy, 7f),
                    }
                );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Hard, 16.5f),
                        new(Difficulty.Easy, 10f),
                    }
                    );
                public Information() { this.MusicOptimized = true; }
            }
            bool jump = false;
            float T(float beat)
            {
                return BeatTime(beat);
            }
            public void Start()
            {
                HeartAttribute.MaxHP = 5;
                RegisterFunction("Con", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(Arguments[0], T(Arguments[1]), Arguments[2] == 0);
                });
                InstantSetBox(240, 650, 490);
                GametimeDelta = -0.5f;
                TP(320, 240);
                SetSoul(1);
                Settings.GreenTap = true;
                ScreenDrawing.ScreenAngle = 180;
                if (jump)
                {
                    float beat = 16f;
                    GametimeDelta = T(beat) - 3.5f;
                    PlayOffset = T(beat);
                    ScreenDrawing.ScreenAngle = 0;
                    InstantSetBox(240, 84, 84);

                }
            }
            public void Easy()
            {
                if (InBeat(0))
                {
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(4.75f);
                    l.RotationEase = LinkEase(Stable(T(4), 45), EaseOut(T(0.75f), -45, EaseState.Cubic));

                    AddInstance(l);
                    RegisterFunctionOnce("Start", () =>
                    {
                        var r = LinkEase(
                            Stable(0, 180),
                            EaseInOut(T(3.5f), 180, 180 + 720 - 180, EaseState.Cubic));
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        }, r);
                        var r1 = LinkEase(
                            Stable(0, new V(650, 490)),
                            EaseInOut(T(3.5f), new V(650, 490), new V(84, 84), EaseState.Quad));
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s.X, s.Y);
                        }, r1);
                    });
                    RegisterFunctionOnce("Drum", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, LinkEase(EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(-2 * Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad)));
                    });
                    CreateChart(0, T(1), 6.5f, new string[]
                    {
                        "Start","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",

                        "","","","",   "(R)","","","",
                        "(R)","","","",   "(R)","","","",
                        "R","","+0","",   "R","","+0","",
                        "R","","+0","",   "","","R","",

                        "+1","","+1","",   "","","","",
                        "R","","+1","",   "+1","","","",
                        "R","","+0","",   "(D)(<-25>Drum)(<7,0.7,0>Con)","","D","",
                        "D","","(D)(<7,0.7,1>Con)","",   "D","","D","",

                        "D(<7,0.7,0>Con)","","D","",   "","","","",
                        "R","","+1","",   "+1","","","",
                        "R","","-1","",   "-1","","-1","",
                        "+1","","+1","",   "","","R","",
                        //
                        "+1","","+1","",   "","","","",
                        "R","","-1","",   "-1","","","",
                        "R","","+0","",   "(D)(<25>Drum)(<7,0.7,1>Con)","","D","",
                        "D","","(D)(<7,0.7,0>Con)","",   "D","","D","",

                        "D(<7,0.7,1>Con)","","D","",   "","","","",
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
                if (InBeat(16))
                {
                    RegisterFunctionOnce("SL1", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 0));
                        var c = LinkEase(Stable(0, 0), Linear(T(2), 0, 90 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL2", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 0));
                        var c = LinkEase(Stable(0, 90), Linear(T(2), 90, 180 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL3", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 480));
                        var c = LinkEase(Stable(0, 180), Linear(T(2), 180, 270 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL4", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 480));
                        var c = LinkEase(Stable(0, 270), Linear(T(2), 270, 360 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("CL", () =>
                    {
                        var c = LinkEase(
                            Stable(0, 640, 480),
                            Combine(
                                LinkEase(Stable(0, 640),
                                EaseOut(T(6), 640, 320, EaseState.Quad)),
                                LinkEase(Stable(0, 480),
                                EaseOut(T(1), 480, 280, EaseState.Quad),
                                EaseIn(T(1), 280, 480, EaseState.Quad),
                                EaseOut(T(1), 480, 480 + 280, EaseState.Quad)
                                )));
                        var r1 = LinkEase(Stable(0, 0), EaseOut(T(3), 0, 45, EaseState.Sine));
                        var r2 = LinkEase(Stable(0, 90), EaseOut(T(3), 90, 90 + 45, EaseState.Sine));
                        Line l = new(c, r1);
                        Line l2 = new(c, r2);
                        CreateEntity(l, l2);
                        RunEase((s) => { l.Alpha = s; l2.Alpha = s; }, Stable(0, 1), EaseIn(T(3), -1.1f, EaseState.Sine));
                        DelayBeat(8, () => { l.Dispose(); l2.Dispose(); });
                    });
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(5);
                    l.RotationEase = LinkEase(Stable(T(4), -15), EaseOut(T(1), -15, 15, EaseState.Sine));
                    l.TagApply("L");
                    Arrow.UnitEasing l2 = new();
                    l2.ApplyTime = T(5);
                    l2.RotationEase = LinkEase(Stable(T(4), 15), EaseOut(T(1), 15, -15, EaseState.Sine));
                    l2.TagApply("R");
                    AddInstance(l);
                    AddInstance(l2);
                    CreateChart(T(4.5f), T(1), 6.5f, new string[]
                    {
                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

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
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",

                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(CL)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                }
                if (InBeat(32))
                {
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(4.75f);
                    l.RotationEase = LinkEase(Stable(T(4), 45), EaseOut(T(0.75f), -45, EaseState.Cubic));

                    AddInstance(l);
                    RegisterFunctionOnce("Drum", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, LinkEase(EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(-2 * Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad)));
                    });
                    RegisterFunctionOnce("Start", () =>
                    {
                        var r = LinkEase(
                            Stable(0, 180),
                            EaseInOut(T(3.5f), 180, 180 + 720 - 180, EaseState.Quad));
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        }, r);
                        var r1 = LinkEase(
                            Stable(0, new V(650, 490)),
                            EaseInOut(T(3.5f), new V(650, 490), new V(84, 84), EaseState.Quad));
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s.X, s.Y);
                        }, r1);
                    });
                    CreateChart(T(3.5f), T(1), 6.5f, new string[]
                    {
                        "","","","",   "(R)","","","",
                        "(R)","","","",   "(R)","","","",
                        "R","","+0","",   "R","","+0","",
                        "R","","+0","",   "","","R","",

                        "+1","","+1","",   "","","","",
                        "R","","+1","",   "+1","","","",
                        "R","","+0","",   "(D)(<-25>Drum)(<7,0.7,0>Con)","","D","",
                        "D","","(D)(<7,0.7,1>Con)","",   "D","","D","",

                        "D(<7,0.7,0>Con)","","D","",   "","","","",
                        "R","","+1","",   "+1","","","",
                        "R","","-1","",   "-1","","-1","",
                        "+1","","+1","",   "","","R","",
                        //
                        "+1","","+1","",   "","","","",
                        "R","","-1","",   "-1","","","",
                        "R","","+0","",   "(D)(<25>Drum)(<7,0.7,1>Con)","","D","",
                        "D","","(D)(<7,0.7,0>Con)","",   "D","","D","",

                        "D(<7,0.7,1>Con)","","D","",   "","","","",
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
                if (InBeat(48))
                {
                    RegisterFunctionOnce("SL1", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 0));
                        var c = LinkEase(Stable(0, 0), Linear(T(2), 0, 90 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL2", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 0));
                        var c = LinkEase(Stable(0, 90), Linear(T(2), 90, 180 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL3", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 480));
                        var c = LinkEase(Stable(0, 180), Linear(T(2), 180, 270 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL4", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 480));
                        var c = LinkEase(Stable(0, 270), Linear(T(2), 270, 360 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("CL", () =>
                    {
                        var c = LinkEase(
                            Stable(0, 0, 480),
                            Combine(
                                LinkEase(Stable(0, 0),
                                EaseOut(T(6), 0, 320, EaseState.Quad)),
                                LinkEase(Stable(0, 480),
                                EaseOut(T(1), 480, 280, EaseState.Quad),
                                EaseIn(T(1), 280, 480, EaseState.Quad),
                                EaseOut(T(1), 480, 480 + 280, EaseState.Quad)
                                )));
                        var r1 = LinkEase(Stable(0, 0), EaseOut(T(3), 0, -45, EaseState.Sine));
                        var r2 = LinkEase(Stable(0, 90), EaseOut(T(3), -90, -90 - 45, EaseState.Sine));
                        Line l = new(c, r1);
                        Line l2 = new(c, r2);
                        CreateEntity(l, l2);
                        RunEase((s) => { l.Alpha = s; l2.Alpha = s; }, Stable(0, 1), EaseIn(T(3), -1.1f, EaseState.Sine));
                        DelayBeat(8, () => { l.Dispose(); l2.Dispose(); });
                    });
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(5);
                    l.RotationEase = LinkEase(Stable(T(4), -15), EaseOut(T(1), -15, 15, EaseState.Sine));
                    l.TagApply("L");
                    Arrow.UnitEasing l2 = new();
                    l2.ApplyTime = T(5);
                    l2.RotationEase = LinkEase(Stable(T(4), 15), EaseOut(T(1), 15, -15, EaseState.Sine));
                    l2.TagApply("R");
                    AddInstance(l);
                    AddInstance(l2);
                    CreateChart(T(4.5f), T(1), 6.5f, new string[]
                    {
                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

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
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",

                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(CL)","","","",   "","","","",
                        "","","","",   "","","","",
                        //                
                    });
                }
                if (InBeat(64))
                {
                    RegisterFunctionOnce("SCS", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, Stable(0, 1), EaseOut(T(1), Arguments[0], EaseState.Sine), EaseOut(T(1), -Arguments[0], EaseState.Sine));

                    });
                    RegisterFunctionOnce("SCC", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, Stable(0, 1), EaseOut(T(1), -Arguments[0], EaseState.Sine), EaseOut(T(1), Arguments[0], EaseState.Sine));

                    });
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",   "","","","",
                        
                        //
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",

                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "R","","R","",   "R","","R","",
                        "R","","R","",   "R","","R","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","R","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",

                        "R(<20,4,0>Con)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "R(<20,4,1>Con)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(116))
                {
                    var r = LinkEase(
                        Stable(0, 0),
                        EaseIn(T(4f), 0, 720, EaseState.Cubic));
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    }, r);
                    var r1 = LinkEase(
                        Stable(0, new V(84, 84)),
                        EaseIn(T(4f), new V(84, 84), new V(650, 490), EaseState.Quad));
                    RunEase((s) =>
                    {
                        InstantSetBox(240, s.X, s.Y);
                    }, r1);
                }
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }

            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }

            public void Hard()
            {
                if (InBeat(0))
                {
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(4.75f);
                    l.RotationEase = LinkEase(Stable(T(4), 45), EaseOut(T(0.75f), -45, EaseState.Cubic));
                    l.TagApply("L");
                    AddInstance(l);
                    RegisterFunctionOnce("Start", () =>
                    {
                        var r = LinkEase(
                            Stable(0, 180),
                            EaseInOut(T(3.5f), 180, 180 + 720 - 180, EaseState.Cubic));
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        }, r);
                        var r1 = LinkEase(
                            Stable(0, new V(650, 490)),
                            EaseInOut(T(3.5f), new V(650, 490), new V(84, 84), EaseState.Quad));
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s.X, s.Y);
                        }, r1);
                    });
                    RegisterFunctionOnce("Drum", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, LinkEase(EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(-2 * Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad)));
                    });
                    CreateChart(0, T(1), 6.5f, new string[]
                    {
                        "Start","","","",   "","","","",
                        "","","","",   "","","","",
                        "$0","","","",   "$0","","","",
                        "$0","","$0","",

                        "$0","$0","$0","",   "(R)(R1)","","","",
                        "(R)(R1)","","","",   "(R)(R1)","","","",
                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "","","R1","",

                        "+21","","+21","",   "","","","",
                        "R","","+2","",   "+2","","","",
                        "$31","","+01","",   "($11)(<-25>Drum)(<7,0.7,0>Con)","","$11","",
                        "$11","","($3)(<7,0.7,1>Con)","",   "$3","","$3","",

                        "$1(<7,0.7,0>Con)","","$1","",   "","","","",
                        "R","","+1","",   "-1","","","",
                        "R1","","+11","",   "-11","","+1","",
                        "+1","","+1","",   "","","R1","",
                        //
                        "+11","","-11","",   "","","","",
                        "R","","-1","",   "+1","","","",
                        "$31","","+01","",   "($11)(<25>Drum)(<7,0.7,1>Con)","","$11","",
                        "$11","","($3)(<7,0.7,0>Con)","",   "$3","","$3","",

                        "$1(<7,0.7,1>Con)","","$3","",   "","","","",
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
                    CreateChart(0, T(1), 6.5f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "(*$2@L)","","","",
                        "","","(*$01@L)","",   "","","","",

                        "(*$21@L)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "(*$2@L)","","","",
                        "","","(*$01@L)","",   "","","","",

                        "(*$21@L)","","","",   "","","","",
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
                if (InBeat(16))
                {
                    RegisterFunctionOnce("SL1", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 0));
                        var c = LinkEase(Stable(0, 0), Linear(T(2), 0, 90 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL2", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 0));
                        var c = LinkEase(Stable(0, 90), Linear(T(2), 90, 180 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL3", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 480));
                        var c = LinkEase(Stable(0, 180), Linear(T(2), 180, 270 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL4", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 480));
                        var c = LinkEase(Stable(0, 270), Linear(T(2), 270, 360 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("CL", () =>
                    {
                        var c = LinkEase(
                            Stable(0, 640, 480),
                            Combine(
                                LinkEase(Stable(0, 640),
                                EaseOut(T(6), 640, 320, EaseState.Quad)),
                                LinkEase(Stable(0, 480),
                                EaseOut(T(1), 480, 280, EaseState.Quad),
                                EaseIn(T(1), 280, 480, EaseState.Quad),
                                EaseOut(T(1), 480, 480 + 280, EaseState.Quad)
                                )));
                        var r1 = LinkEase(Stable(0, 0), EaseOut(T(3), 0, 45, EaseState.Sine));
                        var r2 = LinkEase(Stable(0, 90), EaseOut(T(3), 90, 90 + 45, EaseState.Sine));
                        Line l = new(c, r1);
                        Line l2 = new(c, r2);
                        CreateEntity(l, l2);
                        RunEase((s) => { l.Alpha = s; l2.Alpha = s; }, Stable(0, 1), EaseIn(T(3), -1.1f, EaseState.Sine));
                        DelayBeat(8, () => { l.Dispose(); l2.Dispose(); });
                    });
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(5);
                    l.RotationEase = LinkEase(Stable(T(4), -15), EaseOut(T(1), -15, 15, EaseState.Sine));
                    l.TagApply("L");
                    Arrow.UnitEasing l2 = new();
                    l2.ApplyTime = T(5);
                    l2.RotationEase = LinkEase(Stable(T(4), 15), EaseOut(T(1), 15, -15, EaseState.Sine));
                    l2.TagApply("R");
                    AddInstance(l);
                    AddInstance(l2);
                    CreateChart(T(4.5f), T(1), 6.5f, new string[]
                    {
                        "(*N11)","","*N11","",   "*N11","","","",
                        "(*N11)","","*N11","",   "*N11","","*N11","",
                        "(*N11)","","*N11","",   "","","*N11","",
                        "(*N11)","","","",   "","","","",

                        "(*N3)","","*N3","",   "*N3","","","",
                        "(*N3)","","*N3","",   "*N3","","*N3","",
                        "(*N3)","","*N3","",   "","","*N3","",
                        "(*N3)","","","",   "","","","",

                        "(*N11)","","*N11","",   "*N11","","","",
                        "(*N11)","","*N11","",   "*N11","","*N11","",
                        "(*N11)","","*N11","",   "","","*N11","",
                        "(*N11)","","","",   "","","","",

                        "(*N3)","","*N3","",   "*N3","","","",
                        "(*N3)","","*N3","",   "*N3","","*N3","",
                        "(*N3)","","*N3","",   "","","*N3","",
                        "(*N3)","","","",   "","","","",

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
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(~$1@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$1@R)(SL1)","","","",
                        "(~$1@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$1@R)(SL1)","","","",
                        "(~$1@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$1@R)(SL2)","","","",
                        "(~$1@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$1@R)(SL2)","","","",

                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@R)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@R)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@R)(SL4)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@R)(SL4)","","","",

                        "(~$1@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$1@R)(SL1)","","","",
                        "(~$1@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$1@R)(SL1)","","","",
                        "(~$1@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$1@R)(SL2)","","","",
                        "(~$1@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$1@R)(SL2)","","","",

                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@R)(SL3)","","","",
                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@R)(SL3)","","","",
                        "(~$31@L)(CL)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                }
                if (InBeat(32))
                {
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(4.75f);
                    l.RotationEase = LinkEase(Stable(T(4), 45), EaseOut(T(0.75f), -45, EaseState.Cubic));
                    l.TagApply("G");
                    RegisterFunctionOnce("Drum", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, LinkEase(EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(-2 * Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad)));
                    });
                    RegisterFunctionOnce("Start", () =>
                    {
                        var r = LinkEase(
                            Stable(0, 180),
                            EaseInOut(T(3.5f), 180, 180 + 720 - 180, EaseState.Quad));
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        }, r);
                        var r1 = LinkEase(
                            Stable(0, new V(650, 490)),
                            EaseInOut(T(3.5f), new V(650, 490), new V(84, 84), EaseState.Quad));
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s.X, s.Y);
                        }, r1);
                    });
                    CreateChart(T(3.5f), T(1), 6.5f, new string[]
                    {
                        "","","","",   "(R)(R1)","","","",
                        "(R)(R1)","","","",   "(R)(R1)","","","",
                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "","","R1","",

                        "+21","","+21","",   "","","","",
                        "R","","+2","",   "+2","","","",
                        "$31","","+01","",   "($11)(<-25>Drum)(<7,0.7,0>Con)","","$11","",
                        "$11","","($3)(<7,0.7,1>Con)","",   "$3","","$3","",

                        "$1(<7,0.7,0>Con)","","$1","",   "","","","",
                        "R","","+1","",   "-1","","","",
                        "R1","","+11","",   "-11","","+1","",
                        "+1","","+1","",   "","","R1","",
                        //
                        "+11","","-11","",   "","","","",
                        "R","","-1","",   "+1","","","",
                        "$31","","+01","",   "($11)(<25>Drum)(<7,0.7,1>Con)","","$11","",
                        "$11","","($3)(<7,0.7,0>Con)","",   "$3","","$3","",

                        "$1(<7,0.7,1>Con)","","$3","",   "","","","",
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
                    CreateChart(T(3.5f), T(1), 6.5f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "(*$2@G)","","","",
                        "","","(*$01@G)","",   "","","","",

                        "(*$21@G)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "(*$2@G)","","","",
                        "","","(*$01@G)","",   "","","","",

                        "(*$21@G)","","","",   "","","","",
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
                if (InBeat(48))
                {
                    RegisterFunctionOnce("SL1", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 0));
                        var c = LinkEase(Stable(0, 0), Linear(T(2), 0, 90 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL2", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 0));
                        var c = LinkEase(Stable(0, 90), Linear(T(2), 90, 180 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL3", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 480));
                        var c = LinkEase(Stable(0, 180), Linear(T(2), 180, 270 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL4", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 480));
                        var c = LinkEase(Stable(0, 270), Linear(T(2), 270, 360 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("CL", () =>
                    {
                        var c = LinkEase(
                            Stable(0, 0, 480),
                            Combine(
                                LinkEase(Stable(0, 0),
                                EaseOut(T(6), 0, 320, EaseState.Quad)),
                                LinkEase(Stable(0, 480),
                                EaseOut(T(1), 480, 280, EaseState.Quad),
                                EaseIn(T(1), 280, 480, EaseState.Quad),
                                EaseOut(T(1), 480, 480 + 280, EaseState.Quad)
                                )));
                        var r1 = LinkEase(Stable(0, 0), EaseOut(T(3), 0, -45, EaseState.Sine));
                        var r2 = LinkEase(Stable(0, 90), EaseOut(T(3), -90, -90 - 45, EaseState.Sine));
                        Line l = new(c, r1);
                        Line l2 = new(c, r2);
                        CreateEntity(l, l2);
                        RunEase((s) => { l.Alpha = s; l2.Alpha = s; }, Stable(0, 1), EaseIn(T(3), -1.1f, EaseState.Sine));
                        DelayBeat(8, () => { l.Dispose(); l2.Dispose(); });
                    });
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(5);
                    l.RotationEase = LinkEase(Stable(T(4), -15), EaseOut(T(1), -15, 15, EaseState.Sine));
                    l.TagApply("L");
                    Arrow.UnitEasing l2 = new();
                    l2.ApplyTime = T(5);
                    l2.RotationEase = LinkEase(Stable(T(4), 15), EaseOut(T(1), 15, -15, EaseState.Sine));
                    l2.TagApply("R");
                    AddInstance(l);
                    AddInstance(l2);
                    CreateChart(T(4.5f), T(1), 6.5f, new string[]
                    {
                        "*N11","","*N11","",   "*N11","","","",
                        "*N11","","*N11","",   "*N11","","*N11","",
                        "*N11","","*N11","",   "","","*N11","",
                        "*N11","","","",   "","","","",

                        "*N3","","*N3","",   "*N3","","","",
                        "*N3","","*N3","",   "*N3","","*N3","",
                        "*N3","","*N3","",   "","","*N3","",
                        "*N3","","","",   "","","","",

                        "*N11","","*N11","",   "*N11","","","",
                        "*N11","","*N11","",   "*N11","","*N11","",
                        "*N11","","*N11","",   "","","*N11","",
                        "*N11","","","",   "","","","",

                        "*N3","","*N3","",   "*N3","","","",
                        "*N3","","*N3","",   "*N3","","*N3","",
                        "*N3","","*N3","",   "","","*N3","",
                        "*N3","","","",   "","","","",

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
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(~$1@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$1@L)(SL1)","","","",
                        "(~$1@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$1@L)(SL1)","","","",
                        "(~$1@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$1@L)(SL2)","","","",
                        "(~$1@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$1@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",

                        "(~$1@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$1@L)(SL1)","","","",
                        "(~$1@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$1@L)(SL1)","","","",
                        "(~$1@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$1@L)(SL2)","","","",
                        "(~$1@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$1@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(CL)","","","",   "","","","",
                        "","","","",   "","","","",
                        //                
                    });
                }
                if (InBeat(64))
                {
                    RegisterFunctionOnce("SCS", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, Stable(0, 1), EaseOut(T(1), Arguments[0], EaseState.Sine), EaseOut(T(1), -Arguments[0], EaseState.Sine));

                    });
                    RegisterFunctionOnce("SCC", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, Stable(0, 1), EaseOut(T(1), -Arguments[0], EaseState.Sine), EaseOut(T(1), Arguments[0], EaseState.Sine));

                    });
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",   "","","","",
                        
                        //
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",

                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","R1","",

                        "R","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "R","","","",   "R1","","","",

                        "R(<20,4,0>Con)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "R(<20,4,1>Con)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "R","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",

                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",

                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",

                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",

                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                        "D","","","",   "D","","","",
                    });
                }
                if (InBeat(116))
                {
                    var r = LinkEase(
                        Stable(0, 0),
                        EaseIn(T(4f), 0, 720, EaseState.Cubic));
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    }, r);
                    var r1 = LinkEase(
                        Stable(0, new V(84, 84)),
                        EaseIn(T(4f), new V(84, 84), new V(650, 490), EaseState.Quad));
                    RunEase((s) =>
                    {
                        InstantSetBox(240, s.X, s.Y);
                    }, r1);
                }
            }

            public void Noob()
            {
                return;
            }

            public void Normal()
            {
                if (InBeat(0))
                {
                    SetSoul(0);
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(4.75f);
                    l.RotationEase = LinkEase(Stable(T(4), 45), EaseOut(T(0.75f), -45, EaseState.Cubic));

                    AddInstance(l);
                    RegisterFunctionOnce("Start", () =>
                    {
                        var r = LinkEase(
                            Stable(0, 180),
                            EaseInOut(T(3.5f), 180, 180 + 720 - 180, EaseState.Cubic));
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        }, r);
                        var r1 = LinkEase(
                            Stable(0, new V(650, 490)),
                            EaseInOut(T(2.5f), new V(650, 490), new V(100, 100), EaseState.Quad));
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s.X, s.Y);
                        }, r1);
                    });
                    RegisterFunctionOnce("Drum", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, LinkEase(EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(-2 * Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad)));
                    });
                    RegisterFunctionOnce("B1", () =>
                    {
                        CreateBone(new DownBone(true, 4, 45) { ColorType = 1 });
                        CreateBone(new UpBone(false, 4, 45) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    });//Bone1
                    RegisterFunctionOnce("RDB", () =>
                    {
                        CreateBone(new UpBone(false, 7f, 27 + 20));
                        CreateBone(new LeftBone(false, 7f, 27 + 20));
                        CreateBone(new RightBone(true, 7f, 27 - 20));
                        CreateBone(new DownBone(true, 7f, 27 - 20));
                        PlaySound(Sounds.pierce);
                    });//RightDownBone
                    RegisterFunctionOnce("MB", () =>
                    {
                        CreateBone(new UpBone(false, 7f, 27));
                        CreateBone(new LeftBone(false, 7f, 27));
                        CreateBone(new RightBone(true, 7f, 27));
                        CreateBone(new DownBone(true, 7f, 27));
                        PlaySound(Sounds.pierce);
                    });//MiddleBone
                    RegisterFunctionOnce("LUB", () =>
                    {
                        CreateBone(new UpBone(false, 7f, 27 - 20));
                        CreateBone(new LeftBone(false, 7f, 27 - 20));
                        CreateBone(new RightBone(true, 7f, 27 + 20));
                        CreateBone(new DownBone(true, 7f, 27 + 20));
                        PlaySound(Sounds.pierce);
                    });//LeftUpBone
                    RegisterFunctionOnce("CB", () =>
                    {
                        CreateBone(new CentreCircleBone(0, 3, 120, T(0.7f)));
                        DelayBeat(0.25f, () => { CreateBone(new CentreCircleBone(T(0.25f) * 3 + 90, 3, 120, T(0.7f - 0.25f)) { IsMasked = true }); PlaySound(Sounds.pierce); });
                        PlaySound(Sounds.pierce);
                    });//CentreBone
                    RegisterFunctionOnce("RGB", () =>
                    {
                        CreateGB(new NormalGB(new(320 - 50, 240 - 100), new(320 - 50, 0), new(1, 1), 90, BeatTime(1.25f), T(1.5f)));
                        DelayBeat(0.25f, () => { CreateGB(new NormalGB(new(320 + 50, 240 - 50), new(640, 240 - 50), new(1, 1), 180, BeatTime(1f), T(1.5f))); });
                        DelayBeat(0.5f, () => { CreateGB(new NormalGB(new(320 + 50, 240 + 50), new(320 + 50, 480), new(1, 1), 270, BeatTime(0.75f), T(1.5f))); });
                        DelayBeat(0.75f, () => { CreateGB(new NormalGB(new(320 - 100, 240 + 50), new(0, 240 + 50), new(1, 1), 0, BeatTime(0.5f), T(0.25f))); });
                    });//ReadyGB
                    RegisterFunctionOnce("OB", () =>
                    {
                        CreateBone(new LeftBone(true, 7, 84) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    });//OrangeBone
                    RegisterFunctionOnce("OB1", () =>
                    {
                        CreateBone(new LeftBone(false, 7, 84) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    });//OrangeBone2
                    RegisterFunctionOnce("Blue", () =>
                    {
                        SetSoul(2);
                        PlaySound(Sounds.Ding);
                        PlaySound(Sounds.Ding);
                        SetBox(320 - 42, 320 + 42, 240 - 120, 240 + 120);
                    });//BlueSoul
                    RegisterFunctionOnce("HBU", () =>
                    {
                        CreateBone(new LeftBone(true, 7, Arguments[0]));
                        CreateBone(new RightBone(true, 7, Arguments[0]));
                        PlaySound(Sounds.pierce);
                    });//HideBoneUp
                    RegisterFunctionOnce("HBD", () =>
                    {
                        CreateBone(new LeftBone(false, 7, Arguments[0]));
                        CreateBone(new RightBone(false, 7, Arguments[0]));
                        PlaySound(Sounds.pierce);
                    });//HideBoneUp
                    RegisterFunctionOnce("BWR", () =>
                    {
                        CreateEntity(new Boneslab(0, 40, 0, T(0.5f)));
                        CreateEntity(new Boneslab(180, 40, 0, T(0.5f)));
                        CreateEntity(new Boneslab(90, 40, 0, T(0.5f)));
                        CreateEntity(new Boneslab(270, 40, 0, T(0.5f)) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("BWL", () =>
                    {
                        CreateEntity(new Boneslab(0, 40, 0, T(0.5f)));
                        CreateEntity(new Boneslab(180, 40, 0, T(0.5f)));
                        CreateEntity(new Boneslab(90, 40, 0, T(0.5f)) { ColorType = 2 });
                        CreateEntity(new Boneslab(270, 40, 0, T(0.5f)));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("Pow", () =>
                    {
                        Heart.GiveForce(180, 0);
                        PlaySound(Sounds.Ding);
                        PlaySound(Sounds.Ding);
                    });
                    RegisterFunctionOnce("Plank", () =>
                    {
                        CreateEntity(new Boneslab(180, 30, T(1), T(4f)));
                        var a = LinkEase(Stable(0, new V(380, 240 - 80)),
                            EaseOut(T(1f), new V(380, 240 - 80), new V(350 - 90, 240 - 80), EaseState.Sine),
                            EaseOut(T(1f), new V(350 - 90, 240 - 80), new V(380, 240 - 80), EaseState.Sine),
                            EaseOut(T(1f), new V(380, 240 - 80), new V(350 - 90, 240 - 80), EaseState.Sine),
                            EaseOut(T(1f), new V(350 - 90, 240 - 80), new V(380, 240 - 80), EaseState.Sine),
                            EaseOut(T(1f), new V(380, 240 - 80), new V(350 - 90, 240 - 80), EaseState.Sine));
                        CreatePlatform(new Platform(0, new V(0), a, 180, 30));
                    });
                    CreateChart(0, T(1), 6.5f, new string[]
                    {
                        "Start","","","",   "","","","",
                        "","","","",   "","","","",
                        "B1","","","",   "B1","","","",
                        "B1","","B1","",   "B1","B1","B1","",

                        "RDB","","","",   "MB","","","",
                        "LUB","","","",   "CB","","","",
                        "RGB","","","",   "","","","",
                        "","","","",   "Blue(OB)","","OB","",

                        "","","","",   "<20>HBU","","<20>HBU","",
                        "<20>HBD","","","",   "<20>HBD","","<20>HBD","",
                        "(BWR)(<-25>Drum)(<7,0.7,0>Con)","","","",   "","","(BWL)(<7,0.7,1>Con)","",
                        "","","","",   "(BWR)(<7,0.7,0>Con)","","","",

                        "","","","",   "Pow","","OB1","",
                        "OB1","","","",   "Plank","","","",
                        "","","<20>HBU","",   "<20>HBU","","<20>HBU","",
                        "","","","",   "<20>HBU","","<20>HBU","",

                        "<20>HBU","","","",   "<20>HBU","","<20>HBU","",
                        "<20>HBU","","","",   "<20>HBU","","<20>HBU","",
                        "(<25>Drum)(BWR)(<7,0.7,0>Con)","","","",   "","","(BWL)(<7,0.7,1>Con)","",
                        "","","","",   "(BWR)(<7,0.7,0>Con)","","","",

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
                if (InBeat(16))
                {
                    RegisterFunctionOnce("SL1", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 0));
                        var c = LinkEase(Stable(0, 0), Linear(T(2), 0, 90 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL2", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 0));
                        var c = LinkEase(Stable(0, 90), Linear(T(2), 90, 180 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL3", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 480));
                        var c = LinkEase(Stable(0, 180), Linear(T(2), 180, 270 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL4", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 480));
                        var c = LinkEase(Stable(0, 270), Linear(T(2), 270, 360 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("CL", () =>
                    {
                        var c = LinkEase(
                            Stable(0, 640, 480),
                            Combine(
                                LinkEase(Stable(0, 640),
                                EaseOut(T(6), 640, 320, EaseState.Quad)),
                                LinkEase(Stable(0, 480),
                                EaseOut(T(1), 480, 280, EaseState.Quad),
                                EaseIn(T(1), 280, 480, EaseState.Quad),
                                EaseOut(T(1), 480, 480 + 280, EaseState.Quad)
                                )));
                        var r1 = LinkEase(Stable(0, 0), EaseOut(T(3), 0, 45, EaseState.Sine));
                        var r2 = LinkEase(Stable(0, 90), EaseOut(T(3), 90, 90 + 45, EaseState.Sine));
                        Line l = new(c, r1);
                        Line l2 = new(c, r2);
                        CreateEntity(l, l2);
                        RunEase((s) => { l.Alpha = s; l2.Alpha = s; }, Stable(0, 1), EaseIn(T(3), -1.1f, EaseState.Sine));
                        DelayBeat(8, () => { l.Dispose(); l2.Dispose(); });
                    });
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(5);
                    l.RotationEase = LinkEase(Stable(T(4), -15), EaseOut(T(1), -15, 15, EaseState.Sine));
                    l.TagApply("L");
                    Arrow.UnitEasing l2 = new();
                    l2.ApplyTime = T(5);
                    l2.RotationEase = LinkEase(Stable(T(4), 15), EaseOut(T(1), 15, -15, EaseState.Sine));
                    l2.TagApply("R");
                    AddInstance(l);
                    AddInstance(l2);
                    CreateChart(T(4.5f), T(1), 6.5f, new string[]
                    {
                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

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
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",

                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(CL)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                }
                if (InBeat(32))
                {
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(4.75f);
                    l.RotationEase = LinkEase(Stable(T(4), 45), EaseOut(T(0.75f), -45, EaseState.Cubic));

                    AddInstance(l);
                    RegisterFunctionOnce("Drum", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, LinkEase(EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(-2 * Arguments[0], 0), EaseState.Quad), EaseOut(T(0.75f), new V(Arguments[0], 0), EaseState.Quad)));
                    });
                    RegisterFunctionOnce("Start", () =>
                    {
                        var r = LinkEase(
                            Stable(0, 180),
                            EaseInOut(T(3.5f), 180, 180 + 720 - 180, EaseState.Quad));
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        }, r);
                        var r1 = LinkEase(
                            Stable(0, new V(650, 490)),
                            EaseInOut(T(3.5f), new V(650, 490), new V(84, 84), EaseState.Quad));
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s.X, s.Y);
                        }, r1);
                    });
                    CreateChart(T(3.5f), T(1), 6.5f, new string[]
                    {
                        "","","","",   "(R)","","","",
                        "(R)","","","",   "(R)","","","",
                        "R","","+0","",   "R","","+0","",
                        "R","","+0","",   "","","R","",

                        "+1","","+1","",   "","","","",
                        "R","","+1","",   "+1","","","",
                        "R","","+0","",   "(D)(<-25>Drum)(<7,0.7,0>Con)","","D","",
                        "D","","(D)(<7,0.7,1>Con)","",   "D","","D","",

                        "D(<7,0.7,1>Con)","","D","",   "","","","",
                        "R","","+1","",   "+1","","","",
                        "R","","-1","",   "-1","","-1","",
                        "+1","","+1","",   "","","R","",
                        //
                        "+1","","+1","",   "","","","",
                        "R","","-1","",   "-1","","","",
                        "R","","+0","",   "(D)(<25>Drum)(<7,0.7,1>Con)","","D","",
                        "D","","(D)(<7,0.7,0>Con)","",   "D","","D","",

                        "D(<7,0.7,1>Con)","","D","",   "","","","",
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
                if (InBeat(48))
                {
                    RegisterFunctionOnce("SL1", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 0));
                        var c = LinkEase(Stable(0, 0), Linear(T(2), 0, 90 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL2", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 0));
                        var c = LinkEase(Stable(0, 90), Linear(T(2), 90, 180 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL3", () =>
                    {
                        var l = LinkEase(Stable(0, 640, 480));
                        var c = LinkEase(Stable(0, 180), Linear(T(2), 180, 270 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("SL4", () =>
                    {
                        var l = LinkEase(Stable(0, 0, 480));
                        var c = LinkEase(Stable(0, 270), Linear(T(2), 270, 360 + 30));
                        Line r = new(l, c);
                        CreateEntity(r);
                        RunEase((s) => { r.Alpha = s; }, Stable(0, 1), Linear(T(1.5f), -1.2f));
                    });
                    RegisterFunctionOnce("CL", () =>
                    {
                        var c = LinkEase(
                            Stable(0, 0, 480),
                            Combine(
                                LinkEase(Stable(0, 0),
                                EaseOut(T(6), 0, 320, EaseState.Quad)),
                                LinkEase(Stable(0, 480),
                                EaseOut(T(1), 480, 280, EaseState.Quad),
                                EaseIn(T(1), 280, 480, EaseState.Quad),
                                EaseOut(T(1), 480, 480 + 280, EaseState.Quad)
                                )));
                        var r1 = LinkEase(Stable(0, 0), EaseOut(T(3), 0, -45, EaseState.Sine));
                        var r2 = LinkEase(Stable(0, 90), EaseOut(T(3), -90, -90 - 45, EaseState.Sine));
                        Line l = new(c, r1);
                        Line l2 = new(c, r2);
                        CreateEntity(l, l2);
                        RunEase((s) => { l.Alpha = s; l2.Alpha = s; }, Stable(0, 1), EaseIn(T(3), -1.1f, EaseState.Sine));
                        DelayBeat(8, () => { l.Dispose(); l2.Dispose(); });
                    });
                    Arrow.UnitEasing l = new();
                    l.ApplyTime = T(5);
                    l.RotationEase = LinkEase(Stable(T(4), -15), EaseOut(T(1), -15, 15, EaseState.Sine));
                    l.TagApply("L");
                    Arrow.UnitEasing l2 = new();
                    l2.ApplyTime = T(5);
                    l2.RotationEase = LinkEase(Stable(T(4), 15), EaseOut(T(1), 15, -15, EaseState.Sine));
                    l2.TagApply("R");
                    AddInstance(l);
                    AddInstance(l2);
                    CreateChart(T(4.5f), T(1), 6.5f, new string[]
                    {
                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

                        "N1","","N1","",   "N1","","","",
                        "N1","","N1","",   "N1","","N1","",
                        "N1","","N1","",   "","","N1","",
                        "N1","","","",   "","","","",

                        "N3","","N3","",   "N3","","","",
                        "N3","","N3","",   "N3","","N3","",
                        "N3","","N3","",   "","","N3","",
                        "N3","","","",   "","","","",

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
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,1>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",
                        "(~$31@L)(<17,1.25,1>Con)(SL4)","","","",   "(~$31@L)(SL4)","","","",

                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL1)","","","",   "(~$11@L)(SL1)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",
                        "(~$11@L)(<17,1.25,0>Con)(SL2)","","","",   "(~$11@L)(SL2)","","","",

                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(<17,1.25,0>Con)(SL3)","","","",   "(~$31@L)(SL3)","","","",
                        "(~$31@L)(CL)","","","",   "","","","",
                        "","","","",   "","","","",
                        //                
                    });
                }
                if (InBeat(64))
                {
                    RegisterFunctionOnce("SCS", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, Stable(0, 1), EaseOut(T(1), Arguments[0], EaseState.Sine), EaseOut(T(1), -Arguments[0], EaseState.Sine));

                    });
                    RegisterFunctionOnce("SCC", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, Stable(0, 1), EaseOut(T(1), -Arguments[0], EaseState.Sine), EaseOut(T(1), Arguments[0], EaseState.Sine));

                    });
                    CreateChart(T(4f), T(1), 6.5f, new string[]
                    {
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",   "","","","",
                        
                        //
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",

                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCC)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",
                        "(*R1)(~_+01)(<0.1>SCS)","~_+01","~_+01","~_+01",   "~_+01","~_+01","~_+01","~_+01",
                        "(+01)(+01)","","","",   "","","","",

                        "R","","R","",   "R","","R","",
                        "R","","R","",   "R","","R","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","R","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",

                        "R(<20,4,0>Con)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "R(<20,4,1>Con)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });



                }
                if (InBeat(116))
                {

                    var r = LinkEase(
                        Stable(0, 0),
                        EaseIn(T(4f), 0, 720, EaseState.Cubic));
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    }, r);
                    var r1 = LinkEase(
                        Stable(0, new V(84, 84)),
                        EaseIn(T(4f), new V(84, 84), new V(650, 490), EaseState.Quad));
                    RunEase((s) =>
                    {
                        InstantSetBox(240, s.X, s.Y);
                    }, r1);


                }
            }

        }
    }
}
