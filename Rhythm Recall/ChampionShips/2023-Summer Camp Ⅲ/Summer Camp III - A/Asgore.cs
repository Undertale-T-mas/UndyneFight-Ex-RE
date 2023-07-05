using Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class Asgore : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Asgore()
        {
            dif.Add("Div.2", Difficulty.Noob);
            dif.Add("Div.1", Difficulty.Hard);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f / (114.85f/ 60f)) { }
            public string Music => "Asgore";

            public string FightName => "Asgore";

            public SongImformation Attributes => new Information();
            class Information : SongImformation
            {
                public override string SongAuthor => "Toby Fox";
                public override string BarrageAuthor => "Tlottgodinf";
                public override string AttributeAuthor => "Unknown";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Noob,3.0f),
                    new(Difficulty.Hard,14.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,3.0f),
                    new(Difficulty.Hard,14.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,7.0f),
                    new(Difficulty.Hard,18.0f)
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
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }
            #endregion
            static Arrow.UnitEasing easeA = null, easeB = null, easeC = null;
            static Arrow.EnsembleEasing easeX = null, easeY = null, easeZ = null;
            public void Hard()
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(114), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { sound = false });
                if (InBeat(0))
                {
                    easeX = new();
                    easeX.RevolutionEase(Stable(1, -45));
                    AddInstance(easeX);
                    RegisterFunctionOnce("MoveBox", () =>
                    {
                        RunEase((s) => { InstantSetBox(240, s.X, s.Y); },
                                    Stable(0, new Vector2(80, 80)),
                                    EaseOut(BeatTime(1f), new Vector2(0, 40), EaseState.Quart),
                                    EaseOut(BeatTime(1f), new Vector2(20, 80), EaseState.Quart),
                                    EaseOut(BeatTime(1f), new Vector2(40, 120), EaseState.Quart)
                                    );
                    });
                    RegisterFunctionOnce("Line", () =>
                    {
                        for (int a = 0; a < 8; a++)
                        {
                            int x = a;
                            DelayBeat(0.25f * a, () =>
                            {
                                Line l = new(640 * ((x + 1) / 9f), 90) { Alpha = 0.3f };
                                l.AlphaDecrease(BeatTime(2f));
                                CreateEntity(l);
                            });
                        }
                    });
                    RegisterFunctionOnce("Kick", () =>
                    {
                        var v = LinkEase(
                             Stable(0, new Vector2(0, 480)),
                             EaseOut(BeatTime(0.75f), new Vector2(0, BoxStates.Down - 480), EaseState.Quad)
                             );
                        var r = Stable(0, 0);
                        Line l = new(v.Easing, r.Easing);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                        l.VerticalMirror = true;
                    });
                    RegisterFunctionOnce("Stop", () =>
                    {
                        var c1 = LinkEase(
                            EaseOut(BeatTime(0.75f), new Vector2(160, 0), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(160, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(160, 0), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(320, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(-480, 0), EaseState.Cubic)
                            );
                        var c2 = LinkEase(
                            EaseOut(BeatTime(0.75f), new Vector2(0, 120), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(0, 120)),
                            EaseOut(BeatTime(0.75f), new Vector2(0, 120), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(0, 240)),
                            EaseOut(BeatTime(0.75f), new Vector2(0, -360), EaseState.Cubic)
                            );
                        Line l = new(c1.Easing, c2.Easing);
                        CreateEntity(l);
                        ShadowLibrary.LineShadow(BeatTime(0.125f), 0.8f, 8, l);
                        l.ObliqueMirror = true;
                        l.TransverseMirror = true;
                        l.VerticalMirror = true;
                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "MoveBox","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        //
                        "Stop","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                    });
                }
                if (InBeat(28))
                {
                    RegisterFunctionOnce("MoveBox", () =>
                    {
                        RunEase((s) => { InstantSetBox(240, s.X, s.Y); },
                                    Stable(0, new Vector2(140, 320)),
                                    EaseOut(BeatTime(1f), -new Vector2(40, 120), EaseState.Quart),
                                    EaseOut(BeatTime(1f), -new Vector2(20, 80), EaseState.Quart),
                                    EaseOut(BeatTime(1f), -new Vector2(-4, 40 - 4), EaseState.Quart)
                                    );
                        DelayBeat(2, () =>
                        {
                            SetSoul(1);
                            TP();
                            SetGreenBox();
                        });
                    });
                    RegisterFunctionOnce("Line", () =>
                    {

                        for (int a = 0; a < 8; a++)
                        {
                            int x = a;
                            DelayBeat(0.25f * a, () =>
                            {
                                Line l = new(new Vector2(0, 480 * ((x + 1) / 9f)), 0) { Alpha = 0.3f };
                                l.AlphaDecrease(BeatTime(2f));
                                CreateEntity(l);
                            });
                        }
                    });
                    RegisterFunctionOnce("Kick", () =>
                    {
                        SetSoul(1);
                        RunEase((s) =>
                        {
                            InstantSetBox(240, s, s);
                        },
                        Stable(0, 84),
                        EaseOut(BeatTime(0.125f), 10, EaseState.Sine),
                        EaseOut(BeatTime(0.125f), -10, EaseState.Sine)
                        );
                    });
                    RegisterFunctionOnce("Stop", () =>
                    {
                        var c1 = LinkEase(
                            EaseOut(BeatTime(0.75f), new Vector2(160, 0), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(160, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(160, 0), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(320, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(-480, 0), EaseState.Cubic)
                            );
                        var c2 = LinkEase(
                            EaseOut(BeatTime(0.75f), new Vector2(0, 120), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(0, 120)),
                            EaseOut(BeatTime(0.75f), new Vector2(0, 120), EaseState.Cubic),
                            Stable(BeatTime(0.25f), new Vector2(0, 240)),
                            EaseOut(BeatTime(0.75f), new Vector2(0, -360), EaseState.Cubic)
                            );
                        Line l = new(c1.Easing, c2.Easing);
                        CreateEntity(l);
                        ShadowLibrary.LineShadow(BeatTime(0.125f), 0.8f, 8, l);
                        l.ObliqueMirror = true;
                        l.TransverseMirror = true;
                        l.VerticalMirror = true;
                    });
                    easeX.TagApply("X");
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {

                        "MoveBox","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line","","","Kick",   "","","","",
                        "","","","Kick",   "(R)(D1)","","","",

                        "(Line)(R)(D1)","","","Kick",   "","","(R)(D1)","",
                        "","","","Kick",   "(R)(D1)","","","",
                        "Line(R)(D1)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",

                        "(Line)(R)(D1)","","","Kick",   "","","","",
                        "(R)(D1)","","","Kick",   "","","","",
                        "Line(R)(D1)","","","Kick",   "","","","",
                        "","","","Kick",   "(R)(D1)","","","",

                        "(Line)(R)(D1)","","","Kick",   "","","(R)(D1)","",
                        "","","","Kick",   "(R)(D1)","","","",
                        "Line(R)(D1)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",
                        //
                        "(*^$002@X)(*^+212@X)Stop","","","",   "","","","",
                        "(*^$002@X)(*^+212@X)","","","",   "","","","",
                        "Line(*^$002@X)(*^+212@X)","","","Kick",   "","","","",
                        "","","","Kick",   "(*R02@X)(*D12@X)","","","",

                        "(Line)(*R02@X)(*D12@X)","","","Kick",   "","","(*R02@X)(*D12@X)","",
                        "","","","Kick",   "(*R02@X)(*D12@X)","","","",
                        "Line(*R02@X)(*D12@X)","","","Kick",   "","","","",
                        "","","","Kick",   "(*R02@X)(*D12@X)","","","",

                        "(Line)(*R02@X)(*D12@X)","","","Kick",   "","","(*R02@X)(*D12@X)","",
                        "","","","Kick",   "(*R02@X)(*D12@X)","","","",
                        "Line(*R02@X)(*D12@X)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",

                        "Line","","","Kick",   "Kick","","","",
                        "","","","Kick",   "(*R02@X'1.1)(*+212@X'1.1)Kick","","(*+202@X'1.3)(*+212@X'1.3)","",
                        "(*+202@X'1.5)(*+212@X'1.5)","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(58))
                {
                    RegisterFunctionOnce("CloneA", () =>
                    {
                        Arrow[] Ar = GetAll<Arrow>("CloneA");
                        for (int a = 0; a < Ar.Length; a++) 
                        {
                            int x = a;
                            if (x % 2 == 0)
                            {
                                ShadowLibrary.SetOffset(Ar[x], 400);
                                DelayBeat(0.5f+x * 0.125f, () =>
                                {
                                    RunEase((s) =>
                                    {
                                        ShadowLibrary.SetOffset(Ar[x], s);
                                    },
                                    EaseOut(BeatTime(1.5f), 400, 0, EaseState.Sine));
                                });
                            }
                            else
                            {
                                ShadowLibrary.SetOffset(Ar[x], -400);
                                DelayBeat(0.5f+x * 0.125f, () =>
                                {
                                    RunEase((s) =>
                                    {
                                        ShadowLibrary.SetOffset(Ar[x], s);
                                    },
                                    EaseOut(BeatTime(1.5f), -400, 0, EaseState.Sine));
                                });
                            }
                        }
                    });
                    RegisterFunctionOnce("CloneB", () =>
                    {
                        Arrow[] Ar = GetAll<Arrow>("CloneB");
                        for (int a = 0; a < Ar.Length; a++)
                        {
                            int x = a;
                            if (x % 2 == 0)
                            {
                                ShadowLibrary.SetOffset(Ar[x], 400);
                                DelayBeat(0.5f + x * 0.125f, () =>
                                {
                                    RunEase((s) =>
                                    {
                                        ShadowLibrary.SetOffset(Ar[x], s);
                                    },
                                    EaseOut(BeatTime(1.5f), 400, 0, EaseState.Sine));
                                });
                            }
                            else
                            {
                                ShadowLibrary.SetOffset(Ar[x], -400);
                                DelayBeat(0.5f + x * 0.125f, () =>
                                {
                                    RunEase((s) =>
                                    {
                                        ShadowLibrary.SetOffset(Ar[x], s);
                                    },
                                    EaseOut(BeatTime(1.5f), -400, 0, EaseState.Sine));
                                });
                            }
                        }
                    });
                    RegisterFunctionOnce("Reset", () =>
                    {

                    });
                    RegisterFunctionOnce("Lead1", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 1 / 5, 480 * 1 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 2 / 5, 480 * 1 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 3 / 5, 480 * 1 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 4 / 5, 480 * 1 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2=new(c2, r2);
                        Line l3=new(c3, r3);
                        Line l4=new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.25f,()=> { CreateEntity(l2); });
                        DelayBeat(0.5f, () => { CreateEntity(l3); });
                        DelayBeat(0.75f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead2", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 4 / 5, 480 * 4 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 3 / 5, 480 * 4 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 2 / 5, 480 * 4 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 1 / 5, 480 * 4 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.5f, () => { CreateEntity(l2); });
                        DelayBeat(0.75f, () => { CreateEntity(l3); });
                        DelayBeat(1f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead3", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 1 / 5, 480 * 1 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 1 / 5, 480 * 2 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 1 / 5, 480 * 3 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 1 / 5, 480 * 4 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.25f, () => { CreateEntity(l2); });
                        DelayBeat(0.5f, () => { CreateEntity(l3); });
                        DelayBeat(0.75f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead4", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 4 / 5, 480 * 4 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 4 / 5, 480 * 3 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 4 / 5, 480 * 2 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 4 / 5, 480 * 1 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.5f, () => { CreateEntity(l2); });
                        DelayBeat(0.75f, () => { CreateEntity(l3); });
                        DelayBeat(1f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("LeadEnd", () =>
                    {
                        var c = LinkEase(
                            Stable(0,new Vector2(320,240+42)),
                            EaseOut(BeatTime(1),-new Vector2(0,84),EaseState.Sine),
                            EaseIn(BeatTime(1),new Vector2(0,240 + 84), EaseState.Sine),
                            EaseOut(BeatTime(1), new Vector2(0, 240 + 84), EaseState.Linear)
                            ).Easing;
                        var r = Stable(0, 0).Easing;
                        Line l = new(c, r) {VerticalMirror=true,Alpha=0.7f};
                        var c1 = LinkEase(
                            Stable(0, new Vector2(320-42, 240)),
                            EaseOut(BeatTime(1), new Vector2(84,0), EaseState.Sine),
                            EaseIn(BeatTime(1), -new Vector2(320 + 84,0), EaseState.Sine),
                            EaseOut(BeatTime(1), -new Vector2(320 + 84,0), EaseState.Linear)
                            ).Easing;
                        var r1 = Stable(0, 90).Easing;
                        Line l1 = new(c1, r1) { TransverseMirror = true,Alpha=0.7f };
                        CreateEntity(l,l1);
                        DelayBeat(8, () => { l.Dispose();l1.Dispose(); });
                        ScreenDrawing.CameraEffect.SizeExpand(9f, BeatTime(2.5f));
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(2f), false);
                        DelayBeat(2, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), true); });
                    });
                    RegisterFunctionOnce("Shake", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        if (RandBool()==true)
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), 3, EaseState.Quad),
                            EaseOut(BeatTime(0.875f), -3, EaseState.Sine));
                            Line l = new(Rand(80, 480), 90);
                            Line l2 = new(LastRand + 80, 90);
                            CreateEntity(l, l2);
                            l.AlphaDecrease(BeatTime(0.5f));
                            l2.AlphaDecrease(BeatTime(0.5f));
                        }
                        else
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), -3, EaseState.Quad),
                            EaseOut(BeatTime(0.325f), 3, EaseState.Sine));
                            Line l = new(Rand(80, 400), 90);
                            Line l2 = new(LastRand + 160, 90);
                            CreateEntity(l, l2);
                            l.AlphaDecrease(BeatTime(0.5f));
                            l2.AlphaDecrease(BeatTime(0.5f));
                        }

                    });
                    easeX.TagApply("X");
                    BarrageCreate(BeatTime(5), BeatTime(1), 6, new string[]
                    {
                        "CloneA","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "(R1)(D{CloneA})","+0{CloneA}","+0{CloneA}(CloneB)","+0{CloneA}",   "+0{CloneA}","+0{CloneA}","+0{CloneA}","+0{CloneA}",

                        "(+0{CloneA})(D1)(Lead1)","","","",   "R","","(+0)(D1)","", 
                        "","","(R1)(D{CloneB})","+0{CloneB}",   "+0{CloneB}","+0{CloneB}","(+0{CloneB})(D1)(Lead2)","",
                        "","","R","",   "(+0)(D1)","","","",
                        "(R)(D1)","","","",  "(R)(D1)","","","",

                        "(R)(D1)(Lead3)","","","",   "R","","(+0)(D1)","",
                        "","","R","",   "+0(CloneB)","","(+0)(D1)(Lead4)","",
                        "","","R","",   "(+0)(D1)","","","",   
                        "(R)(D1)","","","",   "(R1)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(D1)(Lead1)","","","",   "R","","(+0)(D1)","",
                        "","","R","",   "(+0)(D1)(CloneB)","","(R)(D1)(Lead4)","",                        
                        "","","R","",   "(+0)(D1)","","","",
                        "R","","","",   "(R1)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(D1)(LeadEnd)","","R","",   "+0","","(+0)(D1)","",
                        "","","R","",   "(+0)(D1)","","","",
                        "(R)(D1)","","","",   "R","+11","-1","",
                        "","","R(CloneB)","-11",   "+1","","","(Shake)",
                        //
                        "*^R12@X'1.5","","","",   "","(Shake)","*^R12@X'1.5","",
                        "","","R{CloneB}","+0{CloneB}",   "+0{CloneB}","+0{CloneB}(Shake)","(+0{CloneB})(*^R12@X'1.5)","",
                        "","","","(Shake)",   "*^R12@X'1.5","","","(Shake)",
                        "*^R12@X'1.5","","","(Shake)",   "*^R12@X'1.5","","","(Shake)",

                        "*^R12@X'1.5","","","",   "","(Shake)","*^R12@X'1.5","",
                        "","","","",   "(CloneB)","(Shake)","*^R12@X'1.5","",
                        "","","","(Shake)",   "*^R12@X'1.5","","","(Shake)",
                        "*^R12@X'1.5","","","(Shake)",   "(*^R12@X'1.5)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}(Shake)",

                        "(+0{CloneB})(*^D12@X'1.5)","","","",   "","(Shake)","*^R12@X'1.5","",
                        "","","","",   "(CloneB)","(Shake)","*^R12@X'1.5","",
                        "","","","(Shake)",   "*^R12@X'1.5","","","(Shake)",
                        "*^R12@X'1.5","","","(Shake)",   "(*^R12@X'1.5)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(*^D1'1.5)(LeadEnd)","","","",   "","","R","",
                        "","","","",   "R1","","","",
                        "R","","+11","",   "","","R","",
                        "","","R'0.85","+1'0.85",   "+1'0.85","","","",

                    });
                }
                if (InBeat(98))
                {
                    RegisterFunctionOnce("Lead1", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 4 / 5, 480 * 4 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 3 / 5, 480 * 4 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 2 / 5, 480 * 4 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 1 / 5, 480 * 4 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.25f, () => { CreateEntity(l2); });
                        DelayBeat(0.5f, () => { CreateEntity(l3); });
                        DelayBeat(0.75f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead2", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 1 / 5, 480 * 1 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 2 / 5, 480 * 1 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 3 / 5, 480 * 1 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 4 / 5, 480 * 1 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.5f, () => { CreateEntity(l2); });
                        DelayBeat(0.75f, () => { CreateEntity(l3); });
                        DelayBeat(1f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead3", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 4 / 5, 480 * 4 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 4 / 5, 480 * 3 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 4 / 5, 480 * 2 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 4 / 5, 480 * 1 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.25f, () => { CreateEntity(l2); });
                        DelayBeat(0.5f, () => { CreateEntity(l3); });
                        DelayBeat(0.75f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead4", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 1 / 5, 480 * 1 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 1 / 5, 480 * 2 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 1 / 5, 480 * 3 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 1 / 5, 480 * 4 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.5f, () => { CreateEntity(l2); });
                        DelayBeat(0.75f, () => { CreateEntity(l3); });
                        DelayBeat(1f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("LeadEnd", () =>
                    {
                        var c = LinkEase(
                            Stable(0, new Vector2(320, 240 +BoxStates.Height/2)),
                            EaseOut(BeatTime(1), -new Vector2(0, BoxStates.Height), EaseState.Sine),
                            EaseIn(BeatTime(1), new Vector2(0, 240 + BoxStates.Height), EaseState.Sine),
                            EaseOut(BeatTime(1), new Vector2(0, 240 + BoxStates.Height), EaseState.Linear)
                            ).Easing;
                        var r = Stable(0, 0).Easing;
                        Line l = new(c, r) { VerticalMirror = true, Alpha = 0.7f };
                        var c1 = LinkEase(
                            Stable(0, new Vector2(320 - BoxStates.Width/2, 240)),
                            EaseOut(BeatTime(1), new Vector2(BoxStates.Width, 0), EaseState.Sine),
                            EaseIn(BeatTime(1), -new Vector2(320 + BoxStates.Width, 0), EaseState.Sine),
                            EaseOut(BeatTime(1), -new Vector2(320 + BoxStates.Width, 0), EaseState.Linear)
                            ).Easing;
                        var r1 = Stable(0, 90).Easing;
                        Line l1 = new(c1, r1) { TransverseMirror = true, Alpha = 0.7f };
                        CreateEntity(l, l1);
                        DelayBeat(8, () => { l.Dispose(); l1.Dispose(); });
                        ScreenDrawing.CameraEffect.SizeExpand(9f, BeatTime(2.5f));
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(2f), true);
                        DelayBeat(2, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), false); });
                    });
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84 * 2f, 84 * 2f);
                        SetSoul(0);
                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "SetBox(Lead1)","","","",   "","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "LeadEnd","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(Lead1)","","","",   "","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "LeadEnd","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(130))
                {
                    RegisterFunctionOnce("Lead1", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 4 / 5, 480 * 4 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 3 / 5, 480 * 4 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 2 / 5, 480 * 4 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 1 / 5, 480 * 4 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.25f, () => { CreateEntity(l2); });
                        DelayBeat(0.5f, () => { CreateEntity(l3); });
                        DelayBeat(0.75f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead2", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 1 / 5, 480 * 1 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 2 / 5, 480 * 1 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 3 / 5, 480 * 1 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 4 / 5, 480 * 1 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), 80),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.5f, () => { CreateEntity(l2); });
                        DelayBeat(0.75f, () => { CreateEntity(l3); });
                        DelayBeat(1f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead3", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 4 / 5, 480 * 4 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 4 / 5, 480 * 3 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 4 / 5, 480 * 2 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 4 / 5, 480 * 1 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.25f, () => { CreateEntity(l2); });
                        DelayBeat(0.5f, () => { CreateEntity(l3); });
                        DelayBeat(0.75f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("Lead4", () =>
                    {
                        var c1 = Stable(0, new Vector2(640 * 1 / 5, 480 * 1 / 5)).Easing;
                        var c2 = Stable(0, new Vector2(640 * 1 / 5, 480 * 2 / 5)).Easing;
                        var c3 = Stable(0, new Vector2(640 * 1 / 5, 480 * 3 / 5)).Easing;
                        var c4 = Stable(0, new Vector2(640 * 1 / 5, 480 * 4 / 5)).Easing;
                        var r1 = LinkEase(
                            Stable(0, -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r2 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r3 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        var r4 = LinkEase(
                            Stable(BeatTime(0), -10),
                            EaseOut(BeatTime(0.5f), 10, EaseState.Sine)
                            ).Easing;
                        Line l1 = new(c1, r1);
                        Line l2 = new(c2, r2);
                        Line l3 = new(c3, r3);
                        Line l4 = new(c4, r4);
                        l1.AlphaDecrease(BeatTime(1));
                        l2.AlphaDecrease(BeatTime(1.25f));
                        l3.AlphaDecrease(BeatTime(1.5f));
                        l4.AlphaDecrease(BeatTime(1.75f));
                        CreateEntity(l1);
                        DelayBeat(0.5f, () => { CreateEntity(l2); });
                        DelayBeat(0.75f, () => { CreateEntity(l3); });
                        DelayBeat(1f, () => { CreateEntity(l4); });
                    });
                    RegisterFunctionOnce("LeadEnd", () =>
                    {
                        var c = LinkEase(
                            Stable(0, new Vector2(320, 240 + BoxStates.Height / 2)),
                            EaseOut(BeatTime(1), -new Vector2(0, BoxStates.Height), EaseState.Sine),
                            EaseIn(BeatTime(1), new Vector2(0, 240 + BoxStates.Height), EaseState.Sine),
                            EaseOut(BeatTime(1), new Vector2(0, 240 + BoxStates.Height), EaseState.Linear)
                            ).Easing;
                        var r = Stable(0, 0).Easing;
                        Line l = new(c, r) { VerticalMirror = true, Alpha = 0.7f };
                        var c1 = LinkEase(
                            Stable(0, new Vector2(320 - BoxStates.Width / 2, 240)),
                            EaseOut(BeatTime(1), new Vector2(BoxStates.Width, 0), EaseState.Sine),
                            EaseIn(BeatTime(1), -new Vector2(320 + BoxStates.Width, 0), EaseState.Sine),
                            EaseOut(BeatTime(1), -new Vector2(320 + BoxStates.Width, 0), EaseState.Linear)
                            ).Easing;
                        var r1 = Stable(0, 90).Easing;
                        Line l1 = new(c1, r1) { TransverseMirror = true, Alpha = 0.7f };
                        CreateEntity(l, l1);
                        DelayBeat(8, () => { l.Dispose(); l1.Dispose(); });
                        ScreenDrawing.CameraEffect.SizeExpand(9f, BeatTime(2.5f));
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(2f), true);
                        DelayBeat(2, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), false); });
                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "(Lead1)","","","",   "","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "LeadEnd","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(Lead1)","","","",   "","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "LeadEnd","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if(InBeat(158))
                {
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {

                    });
                }
            }
            public void Noob()
            {

            }
            public void Start()
            {
                GametimeDelta = -2.125f;
                PlayOffset = 0;
                InstantSetBox(240, 80, 80);
                SetSoul(0);
                InstantTP(320,240);
                bool jump = false;
                if (jump==true)
                {
                    SetSoul(1);
                    GametimeDelta = -5.4f+ BeatTime(130);
                    PlayOffset = BeatTime(130);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}