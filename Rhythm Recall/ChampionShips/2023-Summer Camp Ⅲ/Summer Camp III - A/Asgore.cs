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
            public void Hard()
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(114), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { sound = false });
                if (InBeat(0))
                {
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
                        "(*^$0)(*^+21)Stop","","","",   "","","","",
                        "(*^$0)(*^+21)","","","",   "","","","",
                        "Line(*^$0)(*^+21)","","","Kick",   "","","","",
                        "","","","Kick",   "(*R)(*D1)","","","",

                        "(Line)(*R)(*D1)","","","Kick",   "","","(*R)(*D1)","",
                        "","","","Kick",   "(*R)(*D1)","","","",
                        "Line(*R)(*D1)","","","Kick",   "","","","",
                        "","","","Kick",   "(*R)(*D1)","","","",

                        "(Line)(*R)(*D1)","","","Kick",   "","","(*R)(*D1)","",
                        "","","","Kick",   "(*R)(*D1)","","","",
                        "Line(*R)(*D1)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",

                        "Line","","","Kick",   "Kick","","","",
                        "","","","Kick",   "(*R'1.1)(*+21'1.1)Kick","","(*+2'1.3)(*+21'1.3)","",
                        "(*+2'1.5)(*+21'1.5)","","","",   "","","","",
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
                    BarrageCreate(BeatTime(5), BeatTime(1), 6, new string[]
                    {
                        "CloneA","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "(R1)(D{CloneA})","+0{CloneA}","+0{CloneA}(CloneB)","+0{CloneA}",   "+0{CloneA}","+0{CloneA}","+0{CloneA}","+0{CloneA}",

                        "(+0{CloneA})(D1)","","","",   "R","","(+0)(D1)","", 
                        "","","(R1)(D{CloneB})","+0{CloneB}",   "+0{CloneB}","+0{CloneB}","(+0{CloneB})(D1)","",                  
                        "","","R","",   "(+0)(D1)","","","",
                        "(R)(D1)","","","",  "(R)(D1)","","","",

                        "(R)(D1)","","","",   "R","","(+0)(D1)","",
                        "","","R","",   "+0(CloneB)","","(+0)(D1)","",                       
                        "","","R","",   "(+0)(D1)","","","",   
                        "(R)(D1)","","","",   "(R1)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(D1)","","","",   "R","","(+0)(D1)","",
                        "","","R","",   "(+0)(D1)(CloneB)","","(R)(D1)","",                        
                        "","","R","",   "(+0)(D1)","","","",
                        "R","","","",   "(R1)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(D1)","","R","",   "+0","","(+0)(D1)","",
                        "","","R","",   "(+0)(D1)","","","",
                        "(R)(D1)","","","",   "R","+11","-1","",
                        "","","R(CloneB)","-11",   "+1","","","",
                        //
                        "*^R1'1.5","","","",   "","","*^R1'1.5","",
                        "","","R{CloneB}","+0{CloneB}",   "+0{CloneB}","+0{CloneB}","(+0{CloneB})(*^R1'1.5)","",
                        "","","","",   "*^R1'1.5","","","",
                        "*^R1'1.5","","","",   "*^R1'1.5","","","",

                        "*^R1'1.5","","","",   "","","*^R1'1.5","",
                        "","","","",   "(CloneB)","","*^R1'1.5","",
                        "","","","",   "*^R1'1.5","","","",
                        "*^R1'1.5","","","",   "(*^R1'1.5)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(*^D1'1.5)","","","",   "","","*^R1'1.5","",
                        "","","","",   "(CloneB)","","*^R1'1.5","",
                        "","","","",   "*^R1'1.5","","","",
                        "*^R1'1.5","","","",   "(*^R1'1.5)(D{CloneB})","+0{CloneB}","+0{CloneB}","+0{CloneB}",

                        "(+0{CloneB})(*^D1'1.5)","","","",   "","","R","",
                        "","","","",   "R1","","","",
                        "R","","+11","",   "","","R","",
                        "","","R'0.85","+1'0.85",   "+1'0.85","","","",

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
                    GametimeDelta = -5.4f+ BeatTime(58);
                    PlayOffset = BeatTime(58);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}