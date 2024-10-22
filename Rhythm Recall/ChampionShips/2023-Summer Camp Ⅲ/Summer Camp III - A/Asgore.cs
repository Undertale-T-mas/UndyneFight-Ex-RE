﻿using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using Barrage = UndyneFight_Ex.Remake.Entities.Barrage;
using Color = Microsoft.Xna.Framework.Color;

namespace Rhythm_Recall.Waves
{
    internal class Asgore : IChampionShip
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

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "Toby Fox";
                public override string BarrageAuthor => "Woem";
                public override string AttributeAuthor => "Woem";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Noob,3.0f),
                    new(Difficulty.Hard,14.8f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,3.0f),
                    new(Difficulty.Hard,14.8f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
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
            public static Texture2D HandImage;
            public class Hand : Barrage
            {
                public Hand(Func<ICustomMotion, Vector2> ce, Func<ICustomMotion, float> re)
                {
                    this.Image = HandImage;
                    this.PositionRoute = ce;
                    this.RotationRoute = re;
                    this.Depth = 0.5f;
                    this.UpdateIn120 = true;
                    this.Alpha = 1.0f;
                }
                public Hand(EaseUnit<Vector2> ce, EaseUnit<float> re) : this(ce.Easing, re.Easing) { }
                protected override float GetDistance(Player.Heart heart)
                {
                    return 10000;
                }
            }
            public bool Judge0or1(int arg)
            {
                if(arg==0)return false;
                else return true;
            }
            public void Hard()
            {
                //IsKey();
                
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
                    RegisterFunctionOnce("FireBall1", () =>
                    {
                        PlaySound(Sounds.pierce,0.7f);
                        Vector2 rand = new(Rand(BoxStates.Left, BoxStates.Right), BoxStates.Up - 20);
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Up - 20 - 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine)
                                        ), 6);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, 600 - 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Up - 20 - 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine)
                                        ), 6);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, 600 - 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                    });
                    RegisterFunctionOnce("FireBall2", () =>
                    {
                        PlaySound(Sounds.pierce,0.7f);
                        Vector2 rand = new(Rand(BoxStates.Left, BoxStates.Right), BoxStates.Down + 20);
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Down + 20 + 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine)
                                        ), 3);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, -600 + 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Down + 20 + 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(0.75f), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(0.75f), new Vector2(-40, 0), EaseState.Sine)
                                        ), 3);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, -600 + 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                    });
                    RegisterFunctionOnce("CrossAtk", () =>
                    {
                        PlaySound(Sounds.Ding);
                        DelayBeat(1f, () => { PlaySound(Sounds.pierce,0.7f); });
                        int rd = Rand(0, 90);
                        for (int a = 0; a < 4; a++)
                        {
                            for (int b = 0; b < 4; b++)
                            {
                                int i = a;
                                int u = b;
                                Vector2 hc = Heart.Centre;
                                var ce = LinkEase(
                                    Stable(BeatTime(1f), hc + MathUtil.GetVector2(80+u*10, rd+i*90)),
                                    Linear(BeatTime(4), MathUtil.GetVector2(850 - u * 10, rd+180+i*90))
                                    );
                                FireBall f = new(ce);
                                CreateEntity(f);
                            }
                        }
                    });
                    RegisterFunctionOnce("PlaySound", () => { PlaySound(Sounds.pierce, 0.7f); });
                    CreateChart(0, BeatTime(1), 6, new string[]
                    {
                        "MoveBox","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall1)","","","",
                        "","","","",   "Kick","","","",

                        "Line(CrossAtk)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall1)","","","",
                        "","","","",   "Kick","","","",

                        "Line(CrossAtk)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall1)","","","",
                        "","","","",   "Kick","","","",

                        "Line(CrossAtk)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall1)","","","",
                        "","","","",   "Kick","","","",
                        //
                        "Stop","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall2)","","","",
                        "","","","",   "Kick","","","",

                        "Line(CrossAtk)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall2)","","","",
                        "","","","",   "Kick","","","",

                        "Line(CrossAtk)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(CrossAtk)","","","",   "Kick(FireBall2)","","","",
                        "","","","",   "Kick","","","",

                        "Line(CrossAtk)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(CrossAtk)","","","",   "Kick(PlaySound)","","","",
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
                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
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
                        DelayBeat(2.1f, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), true); });
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
                            EaseOut(BeatTime(0.375f), -3, EaseState.Sine));
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
                    CreateChart(BeatTime(5), BeatTime(1), 6, new string[]
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
                        DelayBeat(2.1f, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), false); });
                    });
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84 * 2f, 84 * 2f);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        SetBox(240, 84 * 2f, 84 * 2f);
                        SetSoul(0);
                    });
                    RegisterFunction("Rain1", () =>
                    {
                        float randx = Rand(-120, -100);
                        PlaySound(Sounds.Ding);
                        for(int a=0;a<16;a++)
                        {
                            var link = Add(LinkEase(EaseOut(BeatTime(1f), new Vector2(0, -60), EaseState.Quad),
                                EaseIn(BeatTime(3f), new Vector2(0, 540), EaseState.Quad)), Linear(BeatTime(2.5f), new Vector2(40,0)));
                            int i = a;
                            var ce = LinkEase(
                                Stable(0,new Vector2(randx+i*60,60)),
                                link
                                );
                            FireBall f = new(ce) { Scale=2};
                            CreateEntity(f);
                        }
                    });
                    RegisterFunction("Rain2", () =>
                    {
                        float randx = Rand(-120, -100);
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 16; a++)
                        {
                            var link = Add(LinkEase(EaseOut(BeatTime(1f), new Vector2(0, -60), EaseState.Quad),
                                EaseIn(BeatTime(3f), new Vector2(0, 540), EaseState.Quad)), Linear(BeatTime(2.5f), new Vector2(-40, 0)));
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(randx + i * 60, 60)),
                                link
                                );
                            FireBall f = new(ce) { Scale = 2 };
                            CreateEntity(f);
                        }
                    });
                    float dist = 18;
                    RegisterFunctionOnce("Kick1", () => 
                    {
                        PlaySound(Sounds.pierce,0.7f);
                        for(int a = 0; a < 6; a++)
                        {
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(BoxStates.Right + dist, BoxStates.Down -dist*i)),
                                Linear(BeatTime(6.2f) - i*BeatTime(0.4f), new Vector2(-440, 0))
                                );
                            FireBall f = new(ce) { Scale = 1.5f,Hidden=true };
                            CreateEntity(f);
                        }
                    });
                    RegisterFunctionOnce("Kick2", () =>
                    {
                        PlaySound(Sounds.pierce,0.7f);
                        for (int a = 0; a < 6; a++)
                        {
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(BoxStates.Right + dist, BoxStates.Up + dist * i)),
                                Linear(BeatTime(6.2f) - i * BeatTime(0.4f), new Vector2(-440, 0))
                                );
                            FireBall f = new(ce) { Scale = 1.5f, Hidden = true };
                            CreateEntity(f);
                        }
                    });
                    CreateChart(0, BeatTime(1), 6, new string[]
                    {
                        "SetBox(Lead1)","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","(Lead2)","",
                        "","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","","",

                        "(Lead3)","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","(Lead4)","",
                        "","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","","",

                        "Lead1","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","Lead4","",
                        "","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","","",

                        "LeadEnd(Rain1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(Lead1)","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","(Lead2)","",
                        "","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","","",

                        "(Lead3)","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","(Lead4)","",
                        "","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","","",

                        "Lead1","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","Lead4","",
                        "","","","",   "(Kick1)","","","",
                        "","","","",   "(Kick2)","","","",

                        "LeadEnd(Rain2)","","","",   "","","","",
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
                        DelayBeat(2.1f, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), false); });
                    });
                    RegisterFunctionOnce("HandAttackL", () =>
                    {
                        Vector2 v = new(BoxStates.Left, BoxStates.Down);
                        var ce = LinkEase(
                            Stable(BeatTime(0.5f), v),
                            Linear(BeatTime(0.5f), new Vector2(0, -BoxStates.Height)),
                            Linear(BeatTime(0.25f), new Vector2(0, -40))
                            );
                        Hand h = new(ce, Stable(0, 90));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(0.5f + i * 0.125f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 2 };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(1f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(0.25f), 1, 0)); });
                        DelayBeat(1.25f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackU", () =>
                    {
                        Vector2 v = new(BoxStates.Left, BoxStates.Up);
                        var ce = LinkEase(
                            Stable(BeatTime(0.5f), v),
                            Linear(BeatTime(0.5f), new Vector2(BoxStates.Width, 0)),
                            Linear(BeatTime(0.25f), new Vector2(40, 0))
                            );
                        Hand h = new(ce, Stable(0, 180));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(0.5f + i * 0.125f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 2 };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(1f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(0.25f), 1, 0)); });
                        DelayBeat(1.25f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackR", () =>
                    {
                        Vector2 v = new(BoxStates.Right, BoxStates.Up);
                        var ce = LinkEase(
                            Stable(BeatTime(0.5f), v),
                            Linear(BeatTime(0.5f), new Vector2(0, BoxStates.Height)),
                            Linear(BeatTime(0.25f), new Vector2(0, 40))
                            );
                        Hand h = new(ce, Stable(0, 270));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(0.5f + i * 0.125f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 2 };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(1f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(0.25f), 1, 0)); });
                        DelayBeat(1.25f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackD", () =>
                    {
                        Vector2 v = new(BoxStates.Right, BoxStates.Down);
                        var ce = LinkEase(
                            Stable(BeatTime(0.5f), v),
                            Linear(BeatTime(0.5f), new Vector2(-BoxStates.Width, 0)),
                            Linear(BeatTime(0.25f), new Vector2(-40, 0))
                            );
                        Hand h = new(ce, Stable(0, 90));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(0.5f + i * 0.125f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 2 };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(1f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(0.25f), 1, 0)); });
                        DelayBeat(1.25f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("PlaySound", () =>
                    {
                        
                    });
                    CreateChart(0, BeatTime(1), 6, new string[]
                    {
                        "(Lead1)","","","",   "(HandAttackL)","","","",
                        "","","","",   "(HandAttackR)","","(Lead2)","",
                        "","","","",   "(HandAttackL)","","","",
                        "","","","",   "(HandAttackR)","","","",

                        "(Lead3)","","","",   "(HandAttackL)","","","",
                        "","","","",   "(HandAttackR)","","(Lead4)","",
                        "","","","",   "(HandAttackL)","","","",
                        "","","","",   "(HandAttackR)","","","",

                        "Lead1","","","",   "(HandAttackU)","","","",
                        "","","","",   "(HandAttackD)","","Lead4","",
                        "","","","",   "(HandAttackU)","","","",
                        "","","","",   "(HandAttackD)","","","",

                        "LeadEnd","","","",   "Rain1","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(Lead1)","","","",   "(HandAttackL)","","","",
                        "","","","",   "(HandAttackL)","","(Lead2)","",
                        "","","","",   "(HandAttackL)","","","",
                        "","","","",   "(HandAttackL)","","","",

                        "(Lead3)","","","",   "(HandAttackU)","","","",
                        "","","","",   "(HandAttackU)","","(Lead4)","",
                        "","","","",   "(HandAttackU)","","","",
                        "","","","",   "(HandAttackU)","","","",

                        "Lead1","","","",   "(HandAttackR)","","","",
                        "","","","",   "(HandAttackR)","","Lead4","",
                        "","","","",   "(HandAttackR)","","","",
                        "","","","",   "(HandAttackR)","","","",

                        "LeadEnd","","","",   "Rain2","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if(InBeat(158))
                {
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84, 84);
                        TP();
                        SetSoul(1);

                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        RunEase((s) =>
                        {
                            InstantSetBox(s, 84, 84);
                            InstantTP(320, s);
                        },
                        Stable(0, 240),
                        EaseIn(BeatTime(1f), 320, EaseState.Sine)
                        );
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenScale = s;
                        },
                        Stable(0, 1.1f),
                        EaseOut(BeatTime(1f), -0.1f, EaseState.Sine));
                    });
                    RegisterFunctionOnce("Conv", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(Arguments[0]), Judge0or1((int)Arguments[1]));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(1.5f, BeatTime(Arguments[0]));
                    });
                    RegisterFunctionOnce("Scale+", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenScale = s;
                        },
                        Stable(0,1),
                        EaseOut(BeatTime(2.5f),0.1f,EaseState.Sine));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "SetBox(<3,0>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "*R","","","",   "*R","","","",

                        "#1.25#R(<1.5,1>Conv)","","","",   "","","","",
                        "","","","",   "*+0","","","",
                        "*R","","*+0","",   "","","*R","",
                        "","","","",   "*R","","","",

                        "#1.75#R(<2,0>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "*+0","","","",   "*R","","*+0","",
                        "","","*R","*+1",   "*-1","","","",

                        "*R","","","",   "","","","",
                        "","","","",   "","","","",
                        "(^*R)(^*D1)(<0.75>Expand)","","","",   "","","(<0.75>Expand)(*R)(*D1)","",
                        "","","","",   "(*R)(*D1)(<0.75>Expand)","","","",
                        //
                        "(#2.75#R1)(<3,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "*+01","","","",   "*R1","","","",

                        "(#1.25#R1)(<1.5,0>Conv)","","","",   "","","","",
                        "","","","",   "*+01","","","",
                        "*R1","","","",   "*R1","","*+01","",
                        "","","","",   "*R1","","","",

                        "(#2.75#R1)(<3,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "*+01","","","",   "*R1","","","",

                        "(#1.25#R1)(<1.5,0>Conv)","","","",   "","","","",
                        "","","","",   "*+01","","","",
                        "*R1","","","",   "*R1","","*+01","",
                        "","","","",   "*R1","","","",
                        //
                        "(#2.75#R1)(<3,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "*+0","","","",   "*R","","","",

                        "(#1.25#R1)(<1.5,0>Conv)","","","",   "","","","",
                        "","","","",   "*+0","","","",
                        "*R","","","",   "*R","","*+0","",
                        "","","","",   "*R","","","",

                        "(#3.75#$31)(<4,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(*$3)(#3#R1)(<3,0>Conv)(Scale+)","","*$0","",   "*$1","","*$2","",
                        "","","*$0","",   "*$0","","","",
                        "*$1","","*$1","",   "","","*$2","",
                        "(Return)","","*$0'0.7","*$1'0.7",   "*$2'0.7","","","",
                        //
                    });
                }
                if (InBeat(206))
                {
                    RegisterFunctionOnce("CrossLine1", () =>
                    {
                        var c = LinkEase(
                            Stable(0,new Vector2(Rand(80,560),Rand(90,180))),
                            EaseIn(BeatTime(2),new Vector2(0,480),EaseState.Quart)
                            );
                        var r = LinkEase(
                            Stable(0,60),
                            EaseOut(BeatTime(1),75,EaseState.Quad)
                            );
                        Line l = new(c.Easing, r.Easing);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                        var r1 = LinkEase(
                            Stable(0, 60+90),
                            EaseOut(BeatTime(1), 75, EaseState.Quad)
                            );
                        Line l1 = new(c.Easing, r1.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("CrossLine2", () =>
                    {
                        var c = LinkEase(
                            Stable(0, new Vector2(Rand(80, 560), Rand(90, 180))),
                            EaseIn(BeatTime(2), new Vector2(0, 480), EaseState.Quart)
                            );
                        var r = LinkEase(
                            Stable(0, 30),
                            EaseOut(BeatTime(1), -75, EaseState.Quad)
                            );
                        Line l = new(c.Easing, r.Easing);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                        var r1 = LinkEase(
                            Stable(0, 30 + 90),
                            EaseOut(BeatTime(1), -75, EaseState.Quad)
                            );
                        Line l1 = new(c.Easing, r1.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        RunEase((s) =>
                        {
                            InstantSetBox(s, 84, 84);
                            InstantTP(320, s);
                        },
                        Stable(0, -80),
                        EaseOut(BeatTime(4), 320, EaseState.Quart)
                        );
                    });
                    RegisterFunctionOnce("BackGround", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Purple*0.9f;
                        RunEase((s) =>
                        {
                            ScreenDrawing.DownBoundDistance = s;
                        },
                        EaseOut(BeatTime(4),80,EaseState.Back),
                        Stable(BeatTime(4)),
                        EaseOut(BeatTime(4), 80, EaseState.Back),
                        Stable(BeatTime(4))                  
                        );
                        ForBeat(18*4, () =>
                        {
                            if(GametimeF%6<1)
                            {
                                float rot = -145+Rand(10, 80);
                                float speed = Rand(7f, 14.1f);
                                CreateEntity(new Particle(
                                    Color.OrangeRed,
                                   new(
                                       Cos(rot) * speed,
                                       Sin(rot) * speed),
                                   Rand(11f, 20.5f),
                                   new(Rand(0, 640), 500), Sprites.square)
                                {
                                    AutoRotate = true,
                                    SlowLerp = Rand(0.03f, 0.04f),
                                    DarkingSpeed = Rand(4.3f, 5.5f),
                                    Depth = 0.2f
                                });
                            }
                        });
                    });
                    
                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "CrossLine1(Back)(BackGround)","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        
                    });
                }
                if(InBeat(238))
                {
                    RegisterFunctionOnce("SineBackGround", () =>
                    {
                        float scale = 40;
                        float time = BeatTime(2);
                        RunEase((s) =>
                        {
                            ScreenDrawing.DownBoundDistance = s;
                        },             
                        Stable(0,160),
                        EaseOut(time,-scale,EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        #region repeat
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        #endregion
                        EaseOut(BeatTime(1),-160,EaseState.Sine)
                        );
                    });
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 160,160);
                        TP();
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        SetBox(240, BoxStates.Width + 60, BoxStates.Height + 60);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Fireball", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce,0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype=0;
                            
                            int x = a;
                            if ((0 < x&&x < 9)||(18<x && x<27)) colortype = 2;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(BeatTime(4), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0),
                                Linear(BeatTime(24), 360)
                                );
                            var end = Add(Polar(e, i),new Vector2(320,240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose=false,ColorType=colortype};
                            //Hand h = new(end, Stable(0));
                            CreateEntity(f);
                            DelayBeat(4, () => { f.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Fireball2", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype = 2;

                            int x = a;
                            if ((0 <= x && x < 9) || (18 <= x && x < 27)) colortype = 2;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(BeatTime(4), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0),
                                Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(4, () => { f.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Fireball3", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 9; a++)
                        {
                            int colortype = 0;

                            int x = a;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 40);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(BeatTime(4), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0),
                                Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(4, () => { f.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Fireball4", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 9; a++)
                        {
                            int colortype = 0;

                            int x = a;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 40);
                            var e = LinkEase(
                                Stable(0, new Vector2(0,0)),
                                Linear(BeatTime(4), mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0),
                                Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(4, () => { f.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Fireball5", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype = 2;

                            int x = a;
                            if ((0 <= x && x < 9) || (18 <= x && x < 27)) colortype = 2;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, new Vector2(0,0)),
                                Linear(BeatTime(4), mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0),
                                Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(4, () => { f.Dispose(); });
                        }
                    });
                    
                    RegisterFunctionOnce("SoundPlay",() => { PlaySound(Sounds.pierce,0.7f); });
                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "SetBox(SineBackGround)","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",

                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",

                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",

                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball)","","","",
                        "","","","",   "(Fireball2)","","","",
                        
                        "Shine","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",

                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",

                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",

                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "SoundPlay","","","",
                        "","","","",   "SoundPlay","","","",
                        //
                        "Shine","","","",   "(Fireball5)","","","",
                        "","","","",   "(Fireball4)","","","",
                        "","","","",   "(Fireball5)","","","",
                        "","","","",   "(Fireball4)","","","",

                        "","","","",   "(Fireball5)","","","",
                        "","","","",   "(Fireball4)","","","",
                        "","","","",   "(Fireball5)","","","",
                        "","","","",   "(Fireball4)","","","",

                        "","","","",   "(Fireball5)","","","",
                        "","","","",   "(Fireball4)","","","",
                        "","","","",   "SoundPlay","","","",
                        "","","","",   "SoundPlay","","","",
                    });
                }
                if (InBeat(282))
                {
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84, 84);
                        TP();
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("SineWave", () =>
                    {
                        Vector2 scale = new(0,15);
                        Arrow[] ars = GetAll<Arrow>("s");
                        float y1 = 225, y2 = 255;
                        var ease = LinkEase(
                            EaseOut(BeatTime(0.75f), 240, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.4f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, 240, EaseState.Quart)
                        );
                        RunEase((s) =>
                        {
                            for (int i = 0; i < ars.Length; i++)
                            {
                                int x = i;
                                ShadowLibrary.SetOffset2(ars[x], s - 240);
                            }
                        },ease                    
                        );
                        Line l = new((s) => new Vector2(320, ease.Easing(s)), Stable(0,0).Easing) { Alpha = 0,Depth=0.01f };
                        CreateEntity(l);
                        DelayBeat(3, () => { l.AlphaIncrease(BeatTime(1), 0.4f); });
                        DelayBeat(12, () => { l.AlphaDecrease(BeatTime(1)); });
                    });
                    RegisterFunctionOnce("Over", () =>
                    {
                        BoxStates.BoxMovingScale = 0.07f;
                        Delay(10, () => { SetSoul(0); });
                        SetBox(240, 160, 160);
                    });
                    RegisterFunctionOnce("Shake1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        if (RandBool() == true)
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), 4, EaseState.Quad),
                            EaseOut(BeatTime(0.625f), -4, EaseState.Sine));                           
                        }
                        else
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), -4, EaseState.Quad),
                            EaseOut(BeatTime(0.625f), 4, EaseState.Sine));
                        }
                        float randx = Rand(90, 550);
                        Line l1 = new(randx, 90) {Alpha=0.9f };
                        var easex1 = LinkEase(
                            Stable(0, new Vector2(randx, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(80, 0), EaseState.Quad)
                            ).Easing;
                        var easex2 = LinkEase(
                            Stable(0, new Vector2(randx)),
                            EaseOut(BeatTime(0.75f), new Vector2(-80, 0), EaseState.Quad)
                            ).Easing;
                        Line l2 = new(easex1, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        Line l3 = new(easex2, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        CreateEntity(l1, l2, l3);
                        l1.AlphaDecrease(BeatTime(0.75f));
                        l2.AlphaDecrease(BeatTime(0.45f));
                        l3.AlphaDecrease(BeatTime(0.45f));
                    });
                    RegisterFunctionOnce("Shake2", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        if (RandBool() == true)
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), 3, EaseState.Quad),
                            EaseOut(BeatTime(0.375f), -3, EaseState.Sine));
                        }
                        else
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), -3, EaseState.Quad),
                            EaseOut(BeatTime(0.375f), 3, EaseState.Sine));
                        }
                        float randx = Rand(90, 550);
                        Line l1 = new(randx, 90) { Alpha=0.9f};
                        var easex1 = LinkEase(
                            Stable(0, new Vector2(randx, 0)),
                            EaseOut(BeatTime(0.5f), new Vector2(80, 0), EaseState.Quad)
                            ).Easing;
                        var easex2 = LinkEase(
                            Stable(0, new Vector2(randx)),
                            EaseOut(BeatTime(0.5f), new Vector2(-80, 0), EaseState.Quad)
                            ).Easing;
                        Line l2 = new(easex1, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        Line l3 = new(easex2, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        CreateEntity(l1, l2, l3);
                        l1.AlphaDecrease(BeatTime(0.5f));
                        l2.AlphaDecrease(BeatTime(0.3f));
                        l3.AlphaDecrease(BeatTime(0.3f));
                    });

                    Arrow.UnitEasing ease;
                    AddInstance(ease = new Arrow.UnitEasing() { ApplyTime = BeatTime(4) });

                    ease.DistanceEase = EaseOut(BeatTime(4), 3060, 0, EaseState.Cubic);
                    ease.TagApply("s");

                    CreateChart(0, BeatTime(1), 6.5f, new string[]
                    {
                        "SineWave","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "SetBox","","","",   "","","","",

                        "(*$0{s})(*$2{s})(Shake1)","","R1","",   "R1","","(*$0{s})(*$2{s})(Shake1)","",
                        "R1","","R1","",   "(*$0{s})(*$2{s})(Shake2)","","R1","",
                        "(*$0{s})(*$2{s})(Shake1)","","R1","",   "R1","","(*$0{s})(*$2{s})(Shake1)","",
                        "R1","","R1","",   "(*$0{s})(*$2{s})(Shake2)","","R1","",

                        "(*$01{s})(*$21{s})(Shake1)","","R","",   "R","","(*$01{s})(*$21{s})(Shake1)","",
                        "R","","R","",   "(*$01{s})(*$21{s})(Shake2)","","R","",
                        "(*$01{s})(*$21{s})(Shake1)","","R","",   "R","","(*$01{s})(*$21{s})(Shake1)","",
                        "R","","R","",   "(*$01{s})(*$21{s})(Shake2)","","R","",

                        "(*$01{s})(*$21{s})(Shake1)(Over)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
            }
            public void Noob()
            {
                //CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(114), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { PlaySound = false });
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
                    RegisterFunctionOnce("FireBall1", () =>
                    {
                        PlaySound(Sounds.pierce, 0.7f);
                        Vector2 rand = new(Rand(BoxStates.Left, BoxStates.Right), BoxStates.Up - 20);
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Up - 20 - 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(1f), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(1f), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1f), new Vector2(40, 0), EaseState.Sine)
                                        ), 6);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, 600 - 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Up - 20 - 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(1), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1), new Vector2(40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(1), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1), new Vector2(-40, 0), EaseState.Sine)
                                        ), 6);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, 600 - 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                    });
                    RegisterFunctionOnce("FireBall2", () =>
                    {
                        PlaySound(Sounds.pierce, 0.7f);
                        Vector2 rand = new(Rand(BoxStates.Left, BoxStates.Right), BoxStates.Down + 20);
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Down + 20 + 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(1), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1), new Vector2(-40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(1), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1), new Vector2(40, 0), EaseState.Sine)
                                        ), 3);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, -600 + 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            int i = a;
                            Delay(i * 4, () =>
                            {

                                rand.Y = BoxStates.Down + 20 + 8 * i;
                                var sinw = Copy(
                                    LinkEase(
                                        EaseOut(BeatTime(1), new Vector2(-40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1), new Vector2(40, 0), EaseState.Sine),
                                        EaseOut(BeatTime(1), new Vector2(40, 0), EaseState.Sine),
                                        EaseIn(BeatTime(1), new Vector2(-40, 0), EaseState.Sine)
                                        ), 3);
                                var ce = LinkEase(
                                    Stable(0, rand),
                                    Add(Add(Linear(BeatTime(14), rand, new Vector2(rand.X, -600 + 8 * i)), sinw), Linear(BeatTime(8), new Vector2(0, BeatTime(8) * 0)))

                                    );
                                FireBall f = new(ce) { Hidden = true };
                                CreateEntity(f);
                            });
                        }
                    });
                    RegisterFunctionOnce("CrossAtk", () =>
                    {
                        PlaySound(Sounds.Ding);
                        DelayBeat(1f, () => { PlaySound(Sounds.pierce, 0.7f); });
                        int rd = Rand(0, 90);
                        for (int a = 0; a < 4; a++)
                        {
                            for (int b = 0; b < 4; b++)
                            {
                                int i = a;
                                int u = b;
                                Vector2 hc = Heart.Centre;
                                var ce = LinkEase(
                                    Stable(BeatTime(1f), hc + MathUtil.GetVector2(80 + u * 10, rd + i * 90)),
                                    Linear(BeatTime(4), MathUtil.GetVector2(850 - u * 10, rd + 180 + i * 90))
                                    );
                                FireBall f = new(ce);
                                CreateEntity(f);
                            }
                        }
                    });
                    RegisterFunctionOnce("PlaySound", () => { PlaySound(Sounds.pierce, 0.7f); });
                    CreateChart(0, BeatTime(1), 5, new string[]
                    {
                        "MoveBox","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line(FireBall1)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(FireBall1)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(FireBall1)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(FireBall1)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        //
                        "Stop","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line(FireBall2)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(FireBall2)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(FireBall2)","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",

                        "Line","","","",   "Kick","","","",
                        "","","","",   "Kick","","","",
                        "Line(PlaySound)","","","",   "Kick","","","",
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
                    RegisterFunctionOnce("Lead1", () =>
                    {
                        var re = LinkEase(
                            Stable(0, 90),
                            EaseOut(BeatTime(1), 90, 90+33, EaseState.Quad)
                            );
                        Line l1 = new(Stable(0,new Vector2(320-120,240)).Easing,re.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("Lead2", () =>
                    {
                        var re = LinkEase(
                            Stable(0, 90),
                            EaseOut(BeatTime(1), 90, 90+33, EaseState.Quad)
                            );
                        Line l1 = new(Stable(0, new Vector2(320, 240)).Easing, re.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("Lead3", () =>
                    {
                        var re = LinkEase(
                            Stable(0, 90),
                            EaseOut(BeatTime(1), 90, 90+33, EaseState.Quad)
                            );
                        Line l1 = new(Stable(0, new Vector2(320+120, 240)).Easing, re.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1));
                    });
                    //easeX.TagApply("X");
                    CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                    {

                        "MoveBox","","","",   "","","","",
                        "","","","",   "","","","",
                        "Line","","","Kick",   "","","","",
                        "","","","Kick",   "(R)","","","",

                        "(Line)(+0)","","","Kick",   "","","(R)","",
                        "","","","Kick",   "(R)","","","",
                        "Line(+0)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",

                        "(Line)(R)","","","Kick",   "","","","",
                        "(R)","","","Kick",   "","","","",
                        "Line(R)","","","Kick",   "","","","",
                        "","","","Kick",   "(R)","","","",

                        "(Line)(+0)","","","Kick",   "","","(R)","",
                        "","","","Kick",   "(R)","","","",
                        "Line(R)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",
                        //
                        "(^$01)Stop","","","",   "","","","",
                        "(^$21)","","","",   "","","","",
                        "Line(^$01)","","","Kick",   "","","","",
                        "","","","Kick",   "(R)","","","",

                        "(Line)(R)","","","Kick",   "","","(R)","",
                        "","","","Kick",   "(R)","","","",
                        "Line(R)","","","Kick",   "","","","",
                        "","","","Kick",   "(R)","","","",

                        "(Line)(R)","","","Kick",   "","","(R)","",
                        "","","","Kick",   "(R)","","","",
                        "Line(R)","","","Kick",   "","","","",
                        "","","","Kick",   "","","","",

                        "Line","","","Kick",   "Kick","","","",
                        "","","","Kick",   "(Lead1)Kick","","(Lead2)","",
                        "(Lead3)","","","",   "","","","",
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
                            Stable(0, new Vector2(320, 240 + 42)),
                            EaseOut(BeatTime(1), -new Vector2(0, 84), EaseState.Sine),
                            EaseIn(BeatTime(1), new Vector2(0, 240 + 84), EaseState.Sine),
                            EaseOut(BeatTime(1), new Vector2(0, 240 + 84), EaseState.Linear)
                            ).Easing;
                        var r = Stable(0, 0).Easing;
                        Line l = new(c, r) { VerticalMirror = true, Alpha = 0.7f };
                        var c1 = LinkEase(
                            Stable(0, new Vector2(320 - 42, 240)),
                            EaseOut(BeatTime(1), new Vector2(84, 0), EaseState.Sine),
                            EaseIn(BeatTime(1), -new Vector2(320 + 84, 0), EaseState.Sine),
                            EaseOut(BeatTime(1), -new Vector2(320 + 84, 0), EaseState.Linear)
                            ).Easing;
                        var r1 = Stable(0, 90).Easing;
                        Line l1 = new(c1, r1) { TransverseMirror = true, Alpha = 0.7f };
                        CreateEntity(l, l1);
                        DelayBeat(8, () => { l.Dispose(); l1.Dispose(); });
                        ScreenDrawing.CameraEffect.SizeExpand(9f, BeatTime(2.5f));
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(2f), false);
                        DelayBeat(2.1f, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), true); });
                    });
                    RegisterFunctionOnce("Shake", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        if (RandBool() == true)
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), 3, EaseState.Quad),
                            EaseOut(BeatTime(0.375f), -3, EaseState.Sine));
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
                    //easeX.TagApply("X");
                    CreateChart(BeatTime(5), BeatTime(1), 5, new string[]
                    {
                        "CloneA","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "(D)","","","",   "","","","",

                        "(R)(Lead1)","","+1","",   "+1","","+1","",
                        "+1","","","",   "","","(-1)(Lead2)","",
                        "","","+1","",   "+1","","+1","",
                        "+1","","","",  "","","","",

                        "(R)(Lead3)","","+1","",   "+1","","+1","",
                        "+1","","","",   "","","(-1)(Lead4)","",
                        "","","+1","",   "+1","","+1","",
                        "+1","","","",  "","","","",

                        "(R)(Lead1)","","+1","",   "+1","","+1","",
                        "+1","","","",   "","","(-1)(Lead2)","",
                        "","","+1","",   "+1","","+1","",
                        "+1","","","",  "","","","",

                        "(R)(LeadEnd)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","(Shake)",
                        //
                        "^R","","","",   "","(Shake)","^R","",
                        "","","","",   "","(Shake)","^R","",
                        "","","","(Shake)",   "^+1","","","(Shake)",
                        "^+1","","","(Shake)",   "^+1","","","(Shake)",

                        "^+1","","","",   "","(Shake)","^R","",
                        "","","","",   "","(Shake)","^R","",
                        "","","","(Shake)",   "^+1","","","(Shake)",
                        "^+1","","","(Shake)",   "^+1","","","",

                        "^+1","","","",   "","(Shake)","^R","",
                        "","","","",   "(CloneB)","(Shake)","^R","",
                        "","","","(Shake)",   "^R","","","(Shake)",
                        "^+1","","","(Shake)",   "^+1","","","",

                        "(^+1)(LeadEnd)","","","",   "","","R","",
                        "","","","",   "R","","","",
                        "R","","+0","",   "","","R","",
                        "","","R'0.85","",   "","","","",

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
                        DelayBeat(2.1f, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), false); });
                    });
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84 * 2f, 84 * 2f);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        SetBox(240, 84 * 2f, 84 * 2f);
                        SetSoul(0);
                    });
                    RegisterFunction("Rain1", () =>
                    {
                        float randx = Rand(-120, -100);
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 16; a++)
                        {
                            var link = Add(LinkEase(EaseOut(BeatTime(1f), new Vector2(0, -60), EaseState.Quad),
                                EaseIn(BeatTime(3f), new Vector2(0, 540), EaseState.Quad)), Linear(BeatTime(2.5f), new Vector2(0)));
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(randx + i * 80, 60)),
                                link
                                );
                            FireBall f = new(ce) { Scale = 2 };
                            CreateEntity(f);
                        }
                    });
                    RegisterFunction("Rain2", () =>
                    {
                        float randx = Rand(-120, -100);
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 16; a++)
                        {
                            var link = Add(LinkEase(EaseOut(BeatTime(1f), new Vector2(0, -60), EaseState.Quad),
                                EaseIn(BeatTime(3f), new Vector2(0, 540), EaseState.Quad)), Linear(BeatTime(2.5f), new Vector2(0)));
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(randx + i * 80, 60)),
                                link
                                );
                            FireBall f = new(ce) { Scale = 2 };
                            CreateEntity(f);
                        }
                    });
                    float dist = 18;
                    RegisterFunctionOnce("Kick1", () =>
                    {
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 7; a++)
                        {
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(BoxStates.Right + dist + (6 - i) * 0, BoxStates.Down - dist * i)),
                                Linear(BeatTime(6.2f), new Vector2(-440, 0))
                                );
                            FireBall f = new(ce) { Scale = 1.5f, Hidden = true };
                            CreateEntity(f);
                        }
                    });
                    RegisterFunctionOnce("Kick2", () =>
                    {
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 7; a++)
                        {
                            int i = a;
                            var ce = LinkEase(
                                Stable(0, new Vector2(BoxStates.Right + dist + (6 - i) * 0, BoxStates.Up + dist * i)),
                                Linear(BeatTime(6.2f), new Vector2(-440, 0))
                                );
                            FireBall f = new(ce) { Scale = 1.5f, Hidden = true };
                            CreateEntity(f);
                        }
                    });
                    CreateChart(0, BeatTime(1), 5, new string[]
                    {
                        "SetBox(Lead1)","","","",   "(Kick1)","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "(Kick2)","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "(Kick1)","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "(Kick2)","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "(Kick1)","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "(Kick2)","","","",
                        "","","","",   "","","","",

                        "LeadEnd(Rain1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(Lead1)","","","",   "(Kick1)","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "(Kick2)","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "(Kick1)","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "(Kick2)","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "(Kick1)","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "(Kick2)","","","",
                        "","","","",   "","","","",

                        "LeadEnd(Rain2)","","","",   "","","","",
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
                        DelayBeat(2.1f, () => { ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1.5f), false); });
                    });
                    RegisterFunctionOnce("HandAttackL", () =>
                    {
                        Vector2 v = new(BoxStates.Left, BoxStates.Down);
                        var ce = LinkEase(
                            Stable(BeatTime(1f), v),
                            Linear(BeatTime(2f), new Vector2(0, -BoxStates.Height)),
                            Linear(BeatTime(1f), new Vector2(0, -40))
                            );
                        Hand h = new(ce, Stable(0, 90));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(1f + i * 0.5f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 1.5f };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(3f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(1f), 1, 0)); });
                        DelayBeat(4f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackU", () =>
                    {
                        Vector2 v = new(BoxStates.Left, BoxStates.Up);
                        var ce = LinkEase(
                            Stable(BeatTime(1f), v),
                            Linear(BeatTime(2f), new Vector2(BoxStates.Width, 0)),
                            Linear(BeatTime(1f), new Vector2(40, 0))
                            );
                        Hand h = new(ce, Stable(0, 180));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(1f + i * 0.5f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 1.5f };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(3f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(1f), 1, 0)); });
                        DelayBeat(4f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackR", () =>
                    {
                        Vector2 v = new(BoxStates.Right, BoxStates.Up);
                        var ce = LinkEase(
                            Stable(BeatTime(1f), v),
                            Linear(BeatTime(2f), new Vector2(0, BoxStates.Height)),
                            Linear(BeatTime(1f), new Vector2(0, 40))
                            );
                        Hand h = new(ce, Stable(0, 270));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(1f + i * 0.5f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 1.5f };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(3f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(1f), 1, 0)); });
                        DelayBeat(4f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackD", () =>
                    {
                        Vector2 v = new(BoxStates.Right, BoxStates.Down);
                        var ce = LinkEase(
                            Stable(BeatTime(1f), v),
                            Linear(BeatTime(2f), new Vector2(-BoxStates.Width, 0)),
                            Linear(BeatTime(1f), new Vector2(-40, 0))
                            );
                        Hand h = new(ce, Stable(0, 90));
                        CreateEntity(h);
                        h.CreateShinyEffect();
                        PlaySound(Sounds.Ding);
                        for (int a = 0; a < 8; a++)
                        {
                            int i = a;
                            DelayBeat(1f + i * 0.5f / 2f, () =>
                            {
                                float rot = MathUtil.Direction(h.Centre, Heart.Centre);
                                Vector2 vec = MathUtil.GetVector2(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(BeatTime(4f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 1.5f };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(3f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(BeatTime(1f), 1, 0)); });
                        DelayBeat(4f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("PlaySound", () =>
                    {

                    });
                    CreateChart(0, BeatTime(1), 5, new string[]
                    {
                        "(Lead1)","","","",   "(HandAttackL)","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "(HandAttackR)","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "(HandAttackL)","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "LeadEnd(Rain1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(Lead1)","","","",   "(HandAttackU)","","","",
                        "","","","",   "","","(Lead2)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Lead3)","","","",   "(HandAttackD)","","","",
                        "","","","",   "","","(Lead4)","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "Lead1","","","",   "(HandAttackU)","","","",
                        "","","","",   "","","Lead4","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "LeadEnd(Rain2)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(158))
                {
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84, 84);
                        TP();
                        SetSoul(1);

                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        RunEase((s) =>
                        {
                            InstantSetBox(s, 84, 84);
                            InstantTP(320, s);
                        },
                        Stable(0, 240),
                        EaseIn(BeatTime(1f), 320, EaseState.Sine)
                        );
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenScale = s;
                        },
                        Stable(0, 1.1f),
                        EaseOut(BeatTime(1f), -0.1f, EaseState.Sine));
                    });
                    RegisterFunctionOnce("Conv", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(Arguments[0]), Judge0or1((int)Arguments[1]));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(1.5f, BeatTime(Arguments[0]));
                    });
                    RegisterFunctionOnce("Scale+", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenScale = s;
                        },
                        Stable(0, 1),
                        EaseOut(BeatTime(2.5f), 0.1f, EaseState.Sine));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                    {
                        "SetBox(<3,0>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "R(<1.5,1>Conv)","","","",   "","","","",
                        "","","","",   "R","","","",
                        "R1","","+01","",   "","","R","",
                        "","","","",   "R","","","",

                        "R(<2,0>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "R1","","+01","",
                        "","","R","",   "","","","",

                        "R","","","",   "","","","",
                        "","","","",   "","","","",
                        "R(<0.75>Expand)","","","",   "","","(<0.75>Expand)R","",
                        "","","","",   "R(<0.75>Expand)","","","",
                        //
                        "R(<3,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "+0","","","",

                        "R(<1.5,0>Conv)","","","",   "","","","",
                        "","","","",   "+0","","","",
                        "+0","","","",   "R","","+0","",
                        "","","","",   "R","","","",

                        "R(<3,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "+0","","","",

                        "R(<1.5,0>Conv)","","","",   "","","","",
                        "","","","",   "+0","","","",
                        "+0","","","",   "R","","+0","",
                        "","","","",   "R","","","",
                        //
                        "R(<3,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "+0","","","",

                        "R(<1.5,0>Conv)","","","",   "","","","",
                        "","","","",   "+0","","","",
                        "+0","","","",   "R","","+0","",
                        "","","","",   "R","","","",

                        "$3(<4,1>Conv)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$3(<3,0>Conv)(Scale+)","","$3","",   "$3","","$3","",
                        "","","$0","",   "$0","","","",
                        "$1","","$1","",   "","","$2","",
                        "(Return)","","","",   "$0","","","",
                        //
                    });
                }
                if (InBeat(206))
                {
                    RegisterFunctionOnce("CrossLine1", () =>
                    {
                        var c = LinkEase(
                            Stable(0, new Vector2(Rand(80, 560), Rand(90, 180))),
                            EaseIn(BeatTime(2), new Vector2(0, 480), EaseState.Quart)
                            );
                        var r = LinkEase(
                            Stable(0, 60),
                            EaseOut(BeatTime(1), 75, EaseState.Quad)
                            );
                        Line l = new(c.Easing, r.Easing);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                        var r1 = LinkEase(
                            Stable(0, 60 + 90),
                            EaseOut(BeatTime(1), 75, EaseState.Quad)
                            );
                        Line l1 = new(c.Easing, r1.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("CrossLine2", () =>
                    {
                        var c = LinkEase(
                            Stable(0, new Vector2(Rand(80, 560), Rand(90, 180))),
                            EaseIn(BeatTime(2), new Vector2(0, 480), EaseState.Quart)
                            );
                        var r = LinkEase(
                            Stable(0, 30),
                            EaseOut(BeatTime(1), -75, EaseState.Quad)
                            );
                        Line l = new(c.Easing, r.Easing);
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                        var r1 = LinkEase(
                            Stable(0, 30 + 90),
                            EaseOut(BeatTime(1), -75, EaseState.Quad)
                            );
                        Line l1 = new(c.Easing, r1.Easing);
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        RunEase((s) =>
                        {
                            InstantSetBox(s, 84, 84);
                            InstantTP(320, s);
                        },
                        Stable(0, -80),
                        EaseOut(BeatTime(4), 320, EaseState.Quart)
                        );
                    });
                    RegisterFunctionOnce("BackGround", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Purple * 0.9f;
                        RunEase((s) =>
                        {
                            ScreenDrawing.DownBoundDistance = s;
                        },
                        EaseOut(BeatTime(4), 80, EaseState.Back),
                        Stable(BeatTime(4)),
                        EaseOut(BeatTime(4), 80, EaseState.Back),
                        Stable(BeatTime(4))
                        );
                        ForBeat(18 * 4, () =>
                        {
                            if (GametimeF % 6 < 1)
                            {
                                float rot = -145 + Rand(10, 80);
                                float speed = Rand(7f, 14.1f);
                                CreateEntity(new Particle(
                                    Color.OrangeRed,
                                   new(
                                       Cos(rot) * speed,
                                       Sin(rot) * speed),
                                   Rand(11f, 20.5f),
                                   new(Rand(0, 640), 500), Sprites.square)
                                {
                                    AutoRotate = true,
                                    SlowLerp = Rand(0.03f, 0.04f),
                                    DarkingSpeed = Rand(4.3f, 5.5f),
                                    Depth = 0.2f
                                });
                            }
                        });
                    });

                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "CrossLine1(Back)(BackGround)","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",
                        "CrossLine1","","","",   "","","","",
                        "","","","",   "","","","",

                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "CrossLine2","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        
                    });
                }
                if (InBeat(238))
                {
                    RegisterFunctionOnce("SineBackGround", () =>
                    {
                        float scale = 40;
                        float time = BeatTime(2);
                        RunEase((s) =>
                        {
                            ScreenDrawing.DownBoundDistance = s;
                        },
                        Stable(0, 160),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        #region repeat
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        EaseOut(time, scale, EaseState.Sine),
                        EaseIn(time, -scale, EaseState.Sine),
                        EaseOut(time, -scale, EaseState.Sine),
                        EaseIn(time, scale, EaseState.Sine),
                        #endregion
                        EaseOut(BeatTime(1), -160, EaseState.Sine)
                        );
                    });
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 160, 160);
                        TP();
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        SetBox(240, BoxStates.Width + 60, BoxStates.Height + 60);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Fireball", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype = 0;

                            int x = a;
                            
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(BeatTime(5), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0)
                                //Linear(BeatTime(24), 360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            if ((0 < x && x < 9+5) || (18 < x && x < 27+5))
                            {
                                FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                                //Hand h = new(end, Stable(0));
                                CreateEntity(f);

                                DelayBeat(5, () => { f.Dispose(); });
                            }
                        }
                    });
                    RegisterFunctionOnce("Fireball2", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype = 2;

                            int x = a;
                            if ((0 <= x && x < 9) || (18 <= x && x < 27)) colortype = 2;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(BeatTime(5), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0)
                                //Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                                FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                                CreateEntity(f);
                                DelayBeat(5, () => { f.Dispose(); });
                            
                        }
                    });
                    RegisterFunctionOnce("Fireball3", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 9; a++)
                        {
                            int colortype = 0;

                            int x = a;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 40);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(BeatTime(5), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0)
                                //Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(5, () => { f.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Fireball4", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 9; a++)
                        {
                            int colortype = 0;

                            int x = a;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 40);
                            var e = LinkEase(
                                Stable(0, new Vector2(0)),
                                Linear(BeatTime(6), mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0)
                                //Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(6, () => { f.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Fireball5", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype = 2;

                            int x = a;
                            if ((0 <= x && x < 9) || (18 <= x && x < 27)) colortype = 2;
                            Vector2 mt = MathUtil.GetVector2(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, new Vector2(0)),
                                Linear(BeatTime(6), mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0)
                                //Linear(BeatTime(24), -360)
                                );
                            var end = Add(Polar(e, i), new Vector2(320, 240));
                            //Line l = new(end.Easing, Stable(0, new Vector2(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(BeatTime(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            CreateEntity(f);
                            DelayBeat(6, () => { f.Dispose(); });
                        }
                    });

                    RegisterFunctionOnce("SoundPlay", () => { PlaySound(Sounds.pierce, 0.7f); });
                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "SetBox(SineBackGround)","","","",   "(Fireball)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "Shine","","","",   "(Fireball3)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball2)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball3)","","","",
                        "","","","",   "","","","",
                        "","","","",   "SoundPlay","","","",
                        "","","","",   "","","","",
                        //
                        "Shine","","","",   "(Fireball5)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball4)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball5)","","","",
                        "","","","",   "","","","",
                        "","","","",   "(Fireball4)","","","",
                        "","","","",   "","","","",

                        "","","","",   "(Fireball5)","","","",
                        "","","","",   "","","","",
                        "","","","",   "SoundPlay","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(282))
                {
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetBox(240, 84, 84);
                        TP();
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("SineWave", () =>
                    {
                        Vector2 scale = new(0, 15);
                        Arrow[] ars = GetAll<Arrow>("s");
                        float y1 = 225, y2 = 255;
                        var ease = LinkEase(
                            EaseOut(BeatTime(0.75f), 240, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.75f), y1, y2, EaseState.Quart),
                            EaseOut(BeatTime(0.4f), y2, y1, EaseState.Quart),
                            EaseOut(BeatTime(0.5f), y1, 240, EaseState.Quart)
                        );
                        RunEase((s) =>
                        {
                            for (int i = 0; i < ars.Length; i++)
                            {
                                int x = i;
                                ShadowLibrary.SetOffset2(ars[x], s - 240);
                            }
                        }, ease
                        );
                        Line l = new((s) => new Vector2(320, ease.Easing(s)), Stable(0, 0).Easing) { Alpha = 0, Depth = 0.01f };
                        CreateEntity(l);
                        DelayBeat(3, () => { l.AlphaIncrease(BeatTime(1), 0); });
                        DelayBeat(12, () => { l.AlphaDecrease(BeatTime(1)); });
                    });
                    RegisterFunctionOnce("Over", () =>
                    {
                        BoxStates.BoxMovingScale = 0.07f;
                        Delay(10, () => { SetSoul(0); });
                        SetBox(240, 160, 160);
                    });
                    RegisterFunctionOnce("Shake1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        if (RandBool() == true)
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), 4, EaseState.Quad),
                            EaseOut(BeatTime(0.625f), -4, EaseState.Sine));
                        }
                        else
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), -4, EaseState.Quad),
                            EaseOut(BeatTime(0.625f), 4, EaseState.Sine));
                        }
                        float randx = Rand(90, 550);
                        Line l1 = new(randx, 90) { Alpha = 0.9f };
                        var easex1 = LinkEase(
                            Stable(0, new Vector2(randx, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(80, 0), EaseState.Quad)
                            ).Easing;
                        var easex2 = LinkEase(
                            Stable(0, new Vector2(randx)),
                            EaseOut(BeatTime(0.75f), new Vector2(-80, 0), EaseState.Quad)
                            ).Easing;
                        Line l2 = new(easex1, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        Line l3 = new(easex2, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        CreateEntity(l1, l2, l3);
                        l1.AlphaDecrease(BeatTime(0.75f));
                        l2.AlphaDecrease(BeatTime(0.45f));
                        l3.AlphaDecrease(BeatTime(0.45f));
                    });
                    RegisterFunctionOnce("Shake2", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        if (RandBool() == true)
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), 3, EaseState.Quad),
                            EaseOut(BeatTime(0.375f), -3, EaseState.Sine));
                        }
                        else
                        {
                            RunEase((s) =>
                            {
                                ScreenDrawing.ScreenAngle = s;
                            },
                            Stable(0, 0),
                            EaseOut(BeatTime(0.125f), -3, EaseState.Quad),
                            EaseOut(BeatTime(0.375f), 3, EaseState.Sine));
                        }
                        float randx = Rand(90, 550);
                        Line l1 = new(randx, 90) { Alpha = 0.9f };
                        var easex1 = LinkEase(
                            Stable(0, new Vector2(randx, 0)),
                            EaseOut(BeatTime(0.5f), new Vector2(80, 0), EaseState.Quad)
                            ).Easing;
                        var easex2 = LinkEase(
                            Stable(0, new Vector2(randx)),
                            EaseOut(BeatTime(0.5f), new Vector2(-80, 0), EaseState.Quad)
                            ).Easing;
                        Line l2 = new(easex1, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        Line l3 = new(easex2, Stable(1145, 90).Easing) { Alpha = 0.4f };
                        CreateEntity(l1, l2, l3);
                        l1.AlphaDecrease(BeatTime(0.5f));
                        l2.AlphaDecrease(BeatTime(0.3f));
                        l3.AlphaDecrease(BeatTime(0.3f));
                    });

                    Arrow.UnitEasing ease;
                    AddInstance(ease = new Arrow.UnitEasing() { ApplyTime = BeatTime(4) });

                    ease.DistanceEase = EaseOut(BeatTime(4), 3060, 0, EaseState.Cubic);
                    //ease.TagApply("s");

                    CreateChart(0, BeatTime(1),5.8f, new string[]
                    {
                        "SineWave","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "SetBox","","","",   "","","","",

                        "(R)(+0)(Shake1)","","","",   "","","(R)(+0)(Shake1)","",
                        "","","","",   "(R)(+0)(Shake2)","","","",
                        "(R)(+0)(Shake1)","","","",   "","","(R)(+0)(Shake1)","",
                        "","","","",   "(R)(+0)(Shake2)","","","",

                        "(R1)(+01)(Shake1)","","","",   "","","(R1)(+01)(Shake1)","",
                        "","","","",   "(R1)(+01)(Shake2)","","","",
                        "(R1)(+01)(Shake1)","","","",   "","","(R1)(+01)(Shake1)","",
                        "","","","",   "(R1)(+01)(Shake2)","","","",

                        "(R1)(+01)(Shake1)(Over)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
            }
            public void IsKey()
            {
                if (GameStates.IsKeyDown(InputIdentity.MainUp)&& GameStates.IsKeyDown(InputIdentity.MainRight) ||
                    GameStates.IsKeyDown(InputIdentity.MainUp) && GameStates.IsKeyDown(InputIdentity.MainLeft) ||
                    GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight) ||
                    GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainLeft))
                    Heart.Speed = 3f * 1.414f;
            }
            public void Start()
            {
                this.Settings.GreenTap = true;
                HandImage = Loader.Load<Texture2D>("Musics\\Asgore\\Hand");
                Heart.Speed = 3f;
                GametimeDelta = -2.5f;
                PlayOffset = 0;
                HeartAttribute.MaxHP = 7;
                InstantSetBox(240, 80, 80);
                SetSoul(0);
                InstantTP(320, 240);
                bool jump = false;
                if (jump)
                {
                    SetSoul(0);
                    int beat = 58;
                    GametimeDelta = -5.4f+ BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}