using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Entities.Player;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.BSet;
using C = Microsoft.Xna.Framework.Color;
using ES = UndyneFight_Ex.Entities.SimplifiedEasing.EaseState;
using V = Microsoft.Xna.Framework.Vector2;
using System.Text;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;
using static Rhythm_Recall.Resources.BadAppleRE;
using static Rhythm_Recall.Resources;

namespace Rhythm_Recall.Waves
{

    internal partial class BadApple_RE : IChampionShip
    {
        partial class Game : WaveConstructor, IWaveSet
        {
            private class ThisImformation : SongInformation
            {
                public override string BarrageAuthor => "mentosu";

                public override string SongAuthor => "Alstroemeria Records";

                public override string PaintAuthor => "mentosu";

                public override string AttributeAuthor => "mentosu";

                public override string Extra => "Thanks to all official chart creators.";

                public override Color ExtraColor => Color.Yellow;

                public override V ExtraPosition => new V(90, 250);
#if !DEBUG
                //public override bool Hidden 
                //{ 
                //    get 
                //    {

                //        return PlayerManager.CurrentUser == null || !PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reBadApple");
                //    } 
                //}
#endif

                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 22f),
                        }
                    );

            }

            public SongInformation Attributes => (SongInformation)(object)new ThisImformation();

            public string Music => "BadAppleRE";

            public string FightName => "BadApple!!";

            public Game() : base(62.5f / (138f / 60) / 1) { }
            public void ExtendEffect()
            {
                
                #region SpiderDance Effect
                if (InBeat(72))
                {
                    BoundColor = C.MediumPurple;
                    RunEase((s) =>
                    {
                        LeftBoundDistance = RightBoundDistance = s;
                    },
                        LinkEase(Stable(0, 0), EaseOut(T(2f), 200, ES.Cubic)));

                }
                if (InBeat(72))
                {
                    rgb = new(0.8633f);
                    SceneRendering.InsertProduction(rgb);

                }
                if (InBeat(72, 104))
                {
                    LeftBoundDistance = RightBoundDistance = 200 + Sin((Gametime - T(72))) * 100;
                }
                if (InBeat(104))
                {
                    BoundColor = CW;
                    LeftBoundDistance = RightBoundDistance = 0;
                }
                if (InBeat(72, 99) && At0thBeat(2))
                {
                    RunEase((s) => { rgb.intensity = s; }, LinkEase(Stable(0, 20), EaseOut(T(2f), -18, ES.Cubic)));
                    float intensity = 20;
                    ForBeat(2, () =>
                    {
                        ScreenPositionDelta = GV(intensity *= -0.94f, Rand(0, 360f));
                    });

                    V rand = new V(Rand(150, 490f), 480);
                    for (int i = 0; i < 20; i++)
                    {
                        EfEn ef = new(rand, Sprites.Croissant) { rot = Rand(0f, 360f), Size = new V(1.2f) };
                        float xspeed = Rand(0.5f, 5f) * RandSignal();
                        float yspeed = -Rand(3, 30f);
                        CreateEntity(ef);
                        ef.ColorEase(false, C.Red, CW, T(0.5f), Cu);
                        ForBeat(10, () =>
                        {
                            ef.centre += new V(xspeed, yspeed);
                            ef.rot += yspeed;
                            yspeed += 0.4f;
                        });
                        ef.AutoDis(T(10));
                    }
                    EfEn spear = new(new V(rand.X, 720), Sprites.spear) { rot = -90, Size = new V(5) };
                    spear.vec2Ease(false, spear.centre, new V(0, -350), T(0.5f), Cu);
                    spear.AutoDis(T(4));
                    spear.vec2Ease(true, new V(rand.X, 720 - 350), new V(0, 480), T(0.5f), T(1.7f), Ex);
                    CreateEntity(spear);

                }
                if (InBeat(72, 103) && At0thBeat(0.05f))
                {
                    float size = Rand(0.25f, 2f);
                    float rot = Rand(0, 360f);
                    V vec = new V(Rand(140, 500), Rand(50, 430));
                    EfEn ef = new(WCentre, UndyneFight_Ex.Remake.Resources.FightSprites.Spider) { Size = new V(size), rot = rot, Alpha = 0.8f };
                    ef.vecEase(false, vec + GV(1000, rot), vec + GV(-1000, rot), T(16), Cu);
                    ef.AutoDis(T(16));
                    Line l = new(Stable(0, vec + GV(1000, rot)),
                                 LinkEase(Stable(0, vec + GV(1000, rot)), EaseOut(T(16), GV(-2000, rot), Cu)))
                    { Width = size * 2, Alpha = 0.2f };
                    CreateEntity(l);
                    CreateEntity(ef);

                }
                #endregion
                #region Lostmemory Effect
                if (InBeat(136, 147.75f) && At0thBeat(0.6f))
                {
                    Line l = new(LinkEase(Stable(0, new V(320, -20)), EaseOut(T(4), new V(0, 150), Li)),
                                 EaseOut(T(4),0,-30,ES.Quad))
                    { VerticalMirror = true };
                    CreateEntity(l);
                    l.AlphaDecrease(T(4));
                    l.DelayDispose(T(4));
                    DelayBeat(0, () => 
                    {
                        Line l = new(LinkEase(Stable(0, new V(320, -20)), EaseOut(T(4), new V(0, 150), Li)),
                                 EaseOut(T(4), 0, 30, ES.Quad))
                        { VerticalMirror = true };
                        CreateEntity(l);
                        l.AlphaDecrease(T(4));
                        l.DelayDispose(T(4));
                    });
                }
                if (InBeat(136, 147.75f) && At0thBeat(2f))
                {
                    Line l = new(EaseOut(T(4),new V(0,485),new V(0,-200),Li),Stable(0,15));
                    l.DrawingColor = C.Lerp(CW, C.Black, 0.6f);
                    CreateEntity(l);
                    l.Width = 1;
                    l.Depth = 0;
                    l.DelayDispose(T(6));
                    DelayBeat(1, () => 
                    {
                        Line l = new(EaseOut(T(4), new V(640, 485), new V(640, -200), Li), Stable(0, -15));
                        l.DrawingColor = C.Lerp(CW, C.Black, 0.6f);
                        l.Depth = 0;
                        l.Width = 1;
                        CreateEntity(l);
                        l.DelayDispose(T(6));
                    });
                }
                if (InBeat(136, 147.75f) && At0thBeat(1.5f))
                {
                    Line l = new(EaseOut(T(3), new V(0, -5), new V(0, 740), Li), Stable(0, -20));
                    l.Alpha = 0.8f;
                    l.Width = 4;
                    CreateEntity(l);
                    l.DelayDispose(T(6));
                    DelayBeat(0.75f, () =>
                    {
                        Line l = new(EaseOut(T(3), new V(640, -5), new V(640, 740), Li), Stable(0, 20));
                        l.Alpha = 0.8f;
                        l.Width = 4;
                        CreateEntity(l);
                        l.DelayDispose(T(6));
                    });
                }
                if (InBeat(136, 147.75f) && At0thBeat(0.5f))
                {
                    Line l = new(
                        LinkEase(Stable(0, new V(0, 0)), EaseIn(T(3), new V(100, 0), Cu),EaseOut(T(3),new V(100,0),Cu)),
                        Stable(0, 90))
                    { Alpha = 0, TransverseMirror = true };
                    RunEase((s) => { l.Alpha = s; },
                        LinkEase(Stable(0, 0), EaseIn(T(3), 1, ES.Sine), EaseOut(T(3), -1, ES.Sine)));
                    CreateEntity(l);
                    l.DelayDispose(T(6));
                }
                if (InBeat(136, 147.75f))
                {
                    EfEn ef = new(new V(Rand(0, 640), -100), Sprites.square) { Size = new V(1f),Alpha=0.5f };
                    ef.AutoDis(T(16));
                    float s = Rand(1, 5f) * RandSignal();
                    float b = RandSignal();
                    float sin = 0;
                    float power = Rand(1, 2f);
                    float rand = Rand(T(4), T(8));
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        ef.centre += new V(0, (200+sin*1.2f) / rand);
                        ef.centre = new V(ef.centre.X + Sin(sin/10f) * s, ef.centre.Y);
                        sin += power;
                        ef.Alpha -= 0.5f / T(16);
                        ef.rot += 3 * b;
                    }));
                    CreateEntity(ef);
                }
                if (InBeat(148.01f)) 
                {
                    for (int i = 1; i <= 7; i++) 
                    {
                        RotateLine l = new(WCentre,0,6,i%2==0?1.3f:-1.3f,i*50);
                        CreateEntity(l);
                    }
                }
                if (InBeat(148, 167.75f) && At0thBeat(0.3f)) 
                {
                    Line l1 = new(Stable(0, new V(-5, -85)), EaseOut(T(4), 0, 45, Li));
                    Line l2 = new(Stable(0, new V(645, -85)), EaseOut(T(4), 90, 135, Li));
                    Line l3 = new(Stable(0, new V(-5, 565)), EaseOut(T(4), 90, 135, Li));
                    Line l4 = new(Stable(0, new V(645, 565)), EaseOut(T(4), 0, 45, Li));
                    foreach (Line ls in new[] { l1, l2, l3, l4 })
                    {
                        CreateEntity(ls);
                        ls.AlphaDecrease(T(4));
                    }
                }
                #endregion
                #region Sudden Effect?

                if (InBeat(167.99f)) 
                {
                    foreach (RotateLine.rotLine s in GetAll<RotateLine.rotLine>())
                    {
                        s.Dispose();
                    }
                    foreach (Line s in GetAll<Line>())
                    {
                        s.Dispose();
                    }
                }
                if (InBeat(168)) 
                {
                    RegisterFunctionOnce("SC1", () => 
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, Arguments[0]), EaseOut(T(1), -Arguments[0], Ba)));
                        RunEase((s) => { ScreenScale = s; },
                            LinkEase(Stable(0, 1.2f), EaseOut(T(1), -.2f, Ba)));
                    });
                    CreateChart(0, T(2), 0, new string[] 
                    {
                        "<9>SC1","","","",  "<9>SC1","","","",
                        "<-9>SC1","","","",  "<-9>SC1","","","",
                        "<9>SC1","","","",  "<9>SC1","","","",
                        "<-9>SC1","","","",  "<-9>SC1","","","",
                    });
                    RegisterFunctionOnce("Lf1", () => 
                    {
                        var s = EaseIn(T(2), new V(0,240), new V(640,240), ES.Cubic);
                        Line l = new(s, Stable(0,90)) {Alpha=0 };
                        l.AlphaIncrease(T(2),1);
                        l.DelayDispose(T(2));
                        CreateEntity(l);
                        l.Depth = 0.3f;
                        for (int i = 0; i < 5; i++) 
                        {
                            l.InsertRetention(new(i*0.5f,0.8f-i*0.1f));
                        }
                        DelayBeat(2, () => 
                        {
                            for (int i = 1; i <= 8; i++) 
                            {
                                var g2 = T(i * 0.125f + 0.5f);
                                var g =i%2==0?100:380;
                                var s = LinkEase(EaseOut(T(2), new V(640, g), new V(640 - i * 60,g), ES.Cubic));
                                var s2 = LinkEase(Stable(g2, 90), EaseOut(T(1),i%2==0?10:-10,ES.Circ));
                                Line l = new(s,s2);
                                CreateEntity(l);
                                l.DelayAlphaIncrease(g2,1,0.4f);
                                l.DelayAlphaDecrease(g2,T(0.5f),1);
                                l.Depth = 0.3f;
                                l.Alpha = 0.6f;
                                DelayBeat(g2 / T(1), () => l.DrawingColor = C.Lerp(CW, C.Magenta, 0.4f));
                            }
                            RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                                LinkEase(Stable(0, 0), EaseOut(T(2f), 40, ES.Sine)));
                        });
                    });
                    RegisterFunctionOnce("Lf2", () => 
                    {
                        var s = EaseIn(T(2), new V(640, 240), new V(0, 240), ES.Cubic);
                        Line l = new(s, Stable(0, 90)) { Alpha = 0 };
                        l.AlphaIncrease(T(2), 1);
                        l.DelayDispose(T(2));
                        CreateEntity(l);
                        l.Depth = 0.3f;
                        RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                                LinkEase(Stable(0, 40), EaseIn(T(2f), -40, ES.Expo)));
                        for (int i = 0; i < 5; i++)
                        {
                            l.InsertRetention(new(i * 0.5f, 0.8f - i * 0.1f));
                        }
                        DelayBeat(2, () =>
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                var g2 = T(i * 0.125f + 0.5f);
                                var g = i % 2 == 0 ? 100 : 380;
                                var s = LinkEase(EaseOut(T(2), new V(0, g), new V(i * 60, g), ES.Cubic));
                                var s2 = LinkEase(Stable(g2, 90), EaseOut(T(1), i % 2 == 0 ? -10 : 10, ES.Circ));
                                Line l = new(s, s2);
                                CreateEntity(l);
                                l.DelayAlphaIncrease(g2, 1, 0.4f);
                                l.DelayAlphaDecrease(g2, T(0.5f), 1);
                                l.Depth = 0.3f;
                                l.Alpha = 0.6f;
                                DelayBeat(g2/T(1), () => l.DrawingColor = C.Lerp(CW, C.Magenta,0.4f));
                                DelayBeat(0, () => 
                                {
                                    var s = LinkEase(EaseOut(T(2), new V(0, g), new V(i * 60, g), ES.Cubic));

                                });
                            }
                            RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                                LinkEase(Stable(0, 0), EaseOut(T(2f), -40, ES.Sine)));
                        });

                    });
                    CreateChart(0, T(2), 0, new string[] 
                    {
                        "Lf1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Lf2","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                    ForBeat(8, () => 
                    {
                        ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = Color.Lerp(Color.White, Color.Magenta, MathF.Max(0, AdvanceFunctions.Sin01(0.08f + GametimeF / T(1)) * 0.1f));
                    });
                    
                }
                if (InBeat(168)) 
                {
                    RegisterFunctionOnce("Line1", () => 
                    {
                        Line l = new(EaseOut(T(1),new V(320,480),new V(320,-70),ES.Cubic),Stable(0,0));
                        l.DelayDispose(T(1));
                        CreateEntity(l);
                        for (int i = 1; i < 3; i++)
                            l.InsertRetention(new(i,1-i*0.33f));
                    });
                    RegisterFunctionOnce("Line2", () => 
                    {
                        Line l = new(EaseOut(T(1), new V(320, 0), new V(320, 550), ES.Cubic), Stable(0, 0));
                        l.DelayDispose(T(1));
                        CreateEntity(l);
                        for (int i = 1; i < 3; i++)
                            l.InsertRetention(new(i, 1 - i * 0.33f));
                    });
                    RegisterFunctionOnce("Line3", () => 
                    {
                        Line l = new(EaseOut(T(1), new V(320, 0), new V(320, 240), ES.Cubic), Stable(0, 0));
                        l.DelayDispose(T(1));
                        CreateEntity(l);
                    });
                    CreateChart(0, T(2), 0, new[] 
                    {
                        "Line1","","","",  "Line2","","","",
                        "Line1","","","",  "Line2","","","",
                        "Line1","","","",  "Line2","","","",
                        "Line1","","","",  "Line3","","","",
                    });
                }
                if (InBeat(168 + 8))
                {
                    for (int i = 0; i < 7; i++)
                    {
                        ValueEasing.EaseBuilder t = new();
                        t.Insert(T(0.75f), EaseOut(T(2 * 0.75f), 0, 25, ES.Linear));
                        t.Insert(T(4*0.75f - (7 - i) * (4*0.75f) / 7f), EaseOut(T(4*0.75f),0,180,ES.Linear));
                        Line l = new(
                            LinkEase(Stable(T(3),WCentre),EaseIn(T(5),new V(0,550),ES.Sine)),t);
                        CreateEntity(l);
                        l.DelayAlphaDecrease(T(7),T(1));
                        RunEase(s => l.DrawingColor = C.Lerp(C.White,C.LightPink,s), EaseOut(T(4),0,1,ES.Quad));
                    }
                    ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = CW;
                    RunEase((s) => { ScreenPositionDelta = new V(s, ScreenPositionDelta.Y); },
                                LinkEase(Stable(0,-40),EaseOut(T(4),40,ES.Elastic)));
                    for (int i = 0; i < 20; i++)
                    {
                        Line l1 = new(EaseOut(T(4), new V(320, 240), new V(20 + i * 30, 240), ES.Sine), Stable(0, 90))
                        {
                            Alpha = 0.3f
                        };

                        CreateEntity(l1);
                        for (int i2 = 0; i2 < 80; i2++) 
                        {
                            l1.DelayAlphaIncrease(T(i * 0.05f + i2),1,1);
                            l1.DelayAlphaDecrease(T(i * 0.05f + i2),T(1),1);
                        }
                    }
                    ForBeat(8,()=> ScreenAngle = Sin((Gametime - 176) * 180 / T(2)) * 15);
                }
                if (InBeat(168 + 8, 183) && At0thBeat(1)) 
                {
                    RunEase((s) => { ScreenPositionDelta = new V(ScreenPositionDelta.X, s); },
                                LinkEase(Stable(0, 0), EaseOut(T(0.2f), -30, Li),EaseIn(T(0.8f),30,ES.Back)));
                    RunEase((s) => { ScreenScale = s; },
                                LinkEase(Stable(0, 1), EaseOut(T(0.2f), 0.05f, Li), EaseIn(T(0.8f), -0.05f, ES.Back)));
                }
                if (InBeat(184))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        if (Arguments[0] == 0)
                        {
                            Line l = new(
                                LinkEase(
                                    EaseOut(T(1), new V(-50, 500), new V(0, 480), ES.Sine),
                                    EaseIn(T(3), new V(0, 480), new V(0, -100), ES.Cubic)),
                                LinkEase(EaseOut(T(2), -90, 0, ES.Sine)));
                            CreateEntity(l);
                            l.AlphaDecrease(T(6));
                            for (int i = 0; i < 10; i++) l.InsertRetention(new(i, 0.7f - i * 0.05f));
                        }
                        if (Arguments[0] == 1) 
                        {
                            Line l = new(
                            LinkEase(
                                EaseOut(T(1), new V(690, 500), new V(640, 480), ES.Sine),
                                EaseIn(T(3), new V(640, 480), new V(640, -100), ES.Cubic)),
                            LinkEase(EaseOut(T(2), 90, 0, ES.Sine)));
                            CreateEntity(l);
                            l.AlphaDecrease(T(6));
                            for (int i = 0; i < 10; i++) l.InsertRetention(new(i,0.7f-i*0.05f));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        EfEn ef = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3) };
                        ef.vecEase(false, new V(-200, 240), new V(320, 240), T(3), Cu);
                        ef.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.rotEase(false, 0, 225, T(3), Cu);
                        ef.rotEase(true, 225, 25, T(3), T(3), Cu);
                        EfEn ef2 = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3) };
                        ef2.vecEase(false, new V(840, 240), new V(320, 240), T(3), Cu);
                        ef2.rotEase(false, 0, -225, T(3), Cu);
                        ef2.rotEase(true, -225, -25, T(3), T(3), Cu);
                        ef2.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        ef2.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        CreateEntity(new Entity[] { ef, ef2 });
                        ef.AutoDis(T(8));
                        ef2.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        EfEn ef = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3) };
                        ef.vecEase(false, new V(-200, 240), new V(320, 240), T(3), Cu);
                        ef.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.rotEase(false, 0, 225, T(3), Cu);
                        ef.rotEase(true, 225, 25, T(3), T(3), Cu);
                        ef.SizeEase(false, new V(3), new V(5), T(6), T(2), Cu);
                        ef.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        ef.ColorEase(false, C.Red, C.Black, T(6), T(2), Cu);
                        EfEn ef2 = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3) };
                        ef2.vecEase(false, new V(840, 240), new V(320, 240), T(3), Cu);
                        ef2.rotEase(false, 0, -225, T(3), Cu);
                        ef2.rotEase(true, -225, -25, T(3), T(3), Cu);
                        ef2.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef2.SizeEase(false, new V(3), new V(5), T(6), T(2), Cu);
                        ef2.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        ef2.ColorEase(false, C.Red, C.Black, T(6), T(2), Cu);
                        CreateEntity(new Entity[] { ef, ef2 });
                        ef.AutoDis(T(8));
                        ef2.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        EfEn ef = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3), Depth = 0.01f };
                        ef.vecEase(false, new V(-200, 240), new V(320, 240), T(3), Cu);
                        ef.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.rotEase(false, 0, 225, T(3), Cu);
                        ef.rotEase(true, 225, 40, T(3), T(3), Cu);
                        EfEn ef2 = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3), Depth = 0.01f };
                        ef2.vecEase(false, new V(840, 240), new V(320, 240), T(3), Cu);
                        ef2.rotEase(false, 0, -225, T(3), Cu);
                        ef2.rotEase(true, -225, -40, T(3), T(3), Cu);
                        ef2.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        ef2.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        CreateEntity(new Entity[] { ef, ef2 });
                        ef.AutoDis(T(8));
                        ef2.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef5", () =>
                    {
                        EfEn ef = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3), Depth = 0f };
                        ef.vecEase(false, new V(-200, 240), new V(320, 240), T(3), Cu);
                        ef.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.rotEase(false, 0, 225, T(3), Cu);
                        ef.rotEase(true, 225, 40, T(3), T(3), Cu);
                        ef.ColorEase(false, C.Red, C.Black, T(6), T(2), Cu);
                        EfEn ef2 = new(new V(320, 240), null) { Bone = true, Len = 150, Size = new V(3), Depth = 0f };
                        ef2.vecEase(false, new V(840, 240), new V(320, 240), T(3), Cu);
                        ef2.rotEase(false, 0, -225, T(3), Cu);
                        ef2.rotEase(true, -225, -40, T(3), T(3), Cu);
                        ef2.vecEase(true, WCentre, new V(320, -300), T(6), T(2), Ba);
                        ef.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        ef2.ColorEase(false, CW, C.Red, T(2), T(3), Cu);
                        ef2.ColorEase(false, C.Red, C.Black, T(6), T(2), Cu);
                        ef.SizeEase(false, new V(3), new V(5), T(6), T(2), Cu);
                        ef2.SizeEase(false, new V(3), new V(5), T(6), T(2), Cu);
                        CreateEntity(new Entity[] { ef, ef2 });
                        ef.AutoDis(T(8));
                        ef2.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef6", () =>
                    {
                        for (int i = 0; i < 36; i++)
                        {
                            float alpha = Rand(0.5f, 1f);
                            EfEn ef = new(new V(), null)
                            {
                                Bone = true,
                                Len = 150,
                                Size = new V(3),
                                Depth = 0f,
                                rot = i * 10 + Rand(-4, 4) + 90,
                            };
                            ef.vecEase(false, WCentre + GV(700, i * 10), WCentre + GV(400 + Rand(-50, 50), i * 10), T(2), Cu);
                            ef.AutoDis(T(8));
                            ef.ColorEase(false, C.Lerp(C.Black, CW, alpha), C.Lerp(C.Black, C.Red, alpha), T(2), T(3), ES.Sine);
                            CreateEntity(ef);
                        }
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "<0>Ef1","","","",  "<1>Ef1","","","",
                        "<0>Ef1","","","",  "<1>Ef1","","","",
                        "<0>Ef1","","","",  "<1>Ef1","","","",
                        "<0>Ef1","","","",  "<1>Ef1","","","",
                        "Ef2(Ef3)(Ef4)(Ef5)(Ef6)","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                    RegisterFunctionOnce("SC1", () => 
                    {
                        V v = new(Arguments[0], Arguments[1]);
                        RunEase(s => { ScreenPositionDelta = s; },
                            LinkEase(EaseOut(T(2),V.Zero,v,ES.Quad),EaseOut(T(2),v,V.Zero,El)));
                    });
                    RegisterFunctionOnce("SC2", () => 
                    {
                        RunEase(s => { ScreenScale = s; },
                            LinkEase(Stable(0,1),EaseOut(T(1),0.4f,ES.Quad)));
                        RunEase(s => { ScreenAngle = s; },
                            LinkEase(Stable(0, 180), EaseOut(T(1), 15f * Arguments[0], ES.Quad)));
                    });
                    RegisterFunctionOnce("SC3", () =>
                    {
                        RunEase(s => { ScreenScale = s; },
                            LinkEase(Stable(0, ScreenScale), EaseOut(T(0.5f), -0.2f, Arguments[0]==0?ES.Quad:ES.Back)));
                        RunEase(s => { ScreenAngle = s; },
                            LinkEase(Stable(0, ScreenAngle), EaseOut(T(0.5f), Arguments[1], ES.Quad)));
                    });
                    RegisterFunctionOnce("SC4", () => 
                    {
                        RunEase(s => { ScreenScale = s; }, LinkEase(EaseOut(T(1f), 1, 1.5f, ES.Quad), EaseIn(T(1f), 1.5f,1f, ES.Quad)));
                    });
                    CreateChart(0, T(2), 0, new string[] 
                    {
                        "<50,-50>SC1","","","",  "","","","",
                        "","","","",  "","","","",
                        "<-50,-50>SC1","","","",  "","","","",
                        "","","","",  "","","","",
                        "<1>SC2","","","",  "<0,-30>SC3","","<1,15>SC3","",
                        "<-1>SC2","","","",  "<0,30>SC3","","<1,-15>SC3","",
                        "<1>SC2","","","",  "<0,-30>SC3","","<1,15>SC3","",
                        "SC4","","","",  "","","","",
                    });
                }
                if (InBeat(192, 199))
                {
                    float rot = Rand(0, 360);
                    EfEn en = new(V.Zero, Sprites.spear, V.One, rot + 180) { Color = C.Red };
                    en.vecEase(false, WCentre + GV(600, rot), WCentre + GV(Rand(150, 200), rot), T(2), Cu);
                    en.AlphaEase(false, 1, 0, T(2), Cu);
                    en.ColorEase(false, C.Red, CW, T(8), Cu);
                    en.AutoDis(T(2));
                    CreateEntity(en);

                }
                #endregion
                #region Transcendence Effect
                if (InBeat(200)) 
                {
                    foreach (EfEn s in GetAll<EfEn>()) 
                    {
                        s.Dispose();
                    }
                }
                if (InBeat(200))
                {
                    RegisterFunctionOnce("XLine", () =>
                    {
                        float s = 0;
                        for (int i = 0; i < 3; i++)
                        {

                            DelayBeat(i * 0.25f, () =>
                            {
                                V vec = new V(120 + s * 100);
                                Line l = new(vec + new V(15, 0), 75);
                                Line l2 = new(vec - new V(15, 0), 75);
                                Line l3 = new(vec + new V(15, 0), 105);
                                Line l4 = new(vec - new V(15, 0), 105);
                                foreach (Line ls in new Line[] { l, l2, l3, l4 })
                                {
                                    ls.Alpha = 0;
                                    CreateEntity(ls);
                                    DelayBeat(0.25f, () =>
                                    {
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    DelayBeat(0.5f, () =>
                                    {
                                        ls.Alpha = 0;
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    DelayBeat(1, () =>
                                    {
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    DelayBeat(1.25f, () =>
                                    {
                                        ls.Alpha = 0;
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    ls.DelayDispose(T(1.5f));
                                }
                                s++;
                            });
                        }
                        
                    });
                    RegisterFunctionOnce("XLine2", () =>
                    {
                        float s = 0;
                        for (int i = 0; i < 3; i++)
                        {

                            DelayBeat(i * 0.25f, () =>
                            {
                                V vec = new V(640 - 120 + s * -100, 120 + s * 100);
                                Line l = new(vec + new V(15, 0), 75);
                                Line l2 = new(vec - new V(15, 0), 75);
                                Line l3 = new(vec + new V(15, 0), 105);
                                Line l4 = new(vec - new V(15, 0), 105);
                                foreach (Line ls in new Line[] { l, l2, l3, l4 })
                                {
                                    ls.Alpha = 0;
                                    CreateEntity(ls);
                                    DelayBeat(0.25f, () =>
                                    {
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    DelayBeat(0.5f, () =>
                                    {
                                        ls.Alpha = 0;
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    DelayBeat(1, () =>
                                    {
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    DelayBeat(1.25f, () =>
                                    {
                                        ls.Alpha = 0;
                                        ls.AlphaIncrease(T(0.25f), 1);
                                    });
                                    ls.DelayDispose(T(1.5f));
                                }
                                s++;
                            });
                        }
                        
                    });
                    RegisterFunctionOnce("LLine", () => 
                    {
                        CentreEasing.EaseBuilder c1 = new();
                        c1.Insert(T(0.25f), CentreEasing.Linear(new V(0, 240), new V(640 + 160, 240), T(0.25f)));
                        CentreEasing.EaseBuilder c2 = new();
                        c2.Insert(T(0.25f), CentreEasing.Linear(new V(0 - 160, 0), new V(640, 0), T(0.25f)));
                        Line line = new(c1.GetResult(), c2.GetResult()) { Alpha = 0.85f * 0.65f };
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++)
                        {
                            int t = i;
                            line.InsertRetention(new(i * 4, 0.85f - 0.85f / 3 * t));
                        }
                        line.AlphaDecrease(T(0.5f));
                        line.VerticalMirror = true;
                    });
                    RegisterFunctionOnce("RLine", () => 
                    {
                        CentreEasing.EaseBuilder c1 = new();
                        c1.Insert(T(0.25f), CentreEasing.Linear(new V(640, 240), new V(0 - 160, 240), T(0.25f)));
                        CentreEasing.EaseBuilder c2 = new();
                        c2.Insert(T(0.25f), CentreEasing.Linear(new V(640 + 160, 0), new V(0, 0), T(0.25f)));
                        Line line = new(c1.GetResult(), c2.GetResult()) { Alpha = 0.85f * 0.65f };
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++)
                        {
                            int t = i;
                            line.InsertRetention(new(i * 4, 0.85f - 0.85f / 3 * t));
                        }
                        line.AlphaDecrease(T(0.5f));
                        line.VerticalMirror = true;
                    });
                    RegisterFunctionOnce("Line1", () =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Line l = new(LinkEase(Stable(0, WCentre), EaseOut(T(1), new V(-50 - i * 25, 0), Cu)),
                                         LinkEase(Stable(0, 45)))
                            { TransverseMirror = true };
                            l.AlphaDecrease(T(1));
                            l.DelayDispose(T(1));
                            CreateEntity(l);

                        }
                        for (int i = 0; i < 5; i++)
                        {
                            Line l = new(LinkEase(Stable(0, WCentre), EaseOut(T(1), new V(-50 - i * 25, 0), Cu)),
                                         LinkEase(Stable(0, -45)))
                            { TransverseMirror = true };
                            l.AlphaDecrease(T(1));
                            l.DelayDispose(T(1));
                            CreateEntity(l);

                        }
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {

                        for (int i = 0; i < 5; i++)
                        {
                            V vec = new V(-50 - i * 25, 0);
                            Line l = new(LinkEase(Stable(0, WCentre + vec * 2), EaseOut(T(1), -vec, Cu)),
                                         LinkEase(Stable(0, 45)))
                            { TransverseMirror = true };
                            l.AlphaDecrease(T(1));
                            l.DelayDispose(T(1));
                            CreateEntity(l);

                        }
                        for (int i = 0; i < 5; i++)
                        {
                            V vec = new V(-50 - i * 25, 0);
                            Line l = new(LinkEase(Stable(0, WCentre + vec * 2), EaseOut(T(1), -vec, Cu)),
                                         LinkEase(Stable(0, -45)))
                            { TransverseMirror = true };
                            l.AlphaDecrease(T(1));
                            l.DelayDispose(T(1));
                            CreateEntity(l);

                        }
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Line l = new(
                                LinkEase(EaseOut(T(2), new V(-640+640/6f*i,0),new V(640/6f * i, 0), Cu)),
                                LinkEase(Stable(0, 65)))
                            { TransverseMirror = true };
                            CreateEntity(l);
                            Line l2 = new(
                                LinkEase(EaseOut(T(2), new V(-640 + 640 / 6f * i-10, 0), new V(640/6f * i-10, 0), Cu)),
                                LinkEase(Stable(0, 65)))
                            { TransverseMirror = true };
                            CreateEntity(l2);
                            l.DelayDispose(T(7));
                            l2.DelayDispose(T(7));
                            DelayBeat(1 + i * 0.25f, () =>
                            {
                                l.AlphaDecrease(T(1));
                                l2.AlphaDecrease(T(1));
                            });
                        }
                    });
                    RegisterFunctionOnce("Line4", () =>
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            Line l = new(
                                LinkEase(Stable(0, new V(200, 490))),
                                LinkEase(Stable(0, 0), EaseOut(T(2), 20 * i, Cu),
                                         Stable(0, 0), EaseIn(T(2), 180 - 20 * i, Cu)))
                            { TransverseMirror = true, Alpha = i / 10f };
                            CreateEntity(l);
                            Line l2 = new(
                                LinkEase(Stable(0, new V(-10, 240))),
                                LinkEase(Stable(0, 90), EaseOut(T(2), 20 * i, Cu),
                                         Stable(0, 0), EaseIn(T(2), 180 - 20 * i, Cu)))
                            { TransverseMirror = true, Alpha = i / 10f };
                            CreateEntity(l2);
                            Line l3 = new(
                                LinkEase(Stable(0, new V(200, -10))),
                                LinkEase(Stable(0, 0), EaseOut(T(2), 20 * i, Cu),
                                         Stable(0, 0), EaseIn(T(2), 180 - 20 * i, Cu)))
                            { TransverseMirror = true, Alpha = i / 10f };
                            CreateEntity(l3);
                            foreach (Line ls in new Entity[] { l, l2, l3 })
                            {
                                ls.DelayDispose(T(5));
                            }
                        }
                    });
                    RegisterFunctionOnce("XLine3", () =>
                    {
                        ValueEasing.EaseBuilder alp2 = new();
                        alp2.Insert(T(0.15f) - 2, ValueEasing.Stable(0.1f * 0.25f));
                        alp2.Insert(2, ValueEasing.Linear(0.1f * 0.25f, 0.65f * 0.25f, 2));
                        alp2.Insert(T(0.5f), ValueEasing.EaseInCubic(0.65f * 0.25f, 0, T(0.5f)));
                        ValueEasing.EaseBuilder alp3 = new();
                        alp3.Insert(T(0.15f) - 2, ValueEasing.Stable(0.1f * 0.25f));
                        alp3.Insert(2, ValueEasing.Linear(0.1f * 0.25f, 0.65f * 0.25f, 2));
                        alp3.Insert(T(0.5f), ValueEasing.EaseInCubic(0.65f * 0.25f, 0, T(0.5f)));
                        Line lb = new(new V(320, 240), 45);
                        Line lc = new(new V(320, 240), -45);
                        CreateEntity(lb);
                        CreateEntity(lc);
                        alp2.Run((s) => { lb.Alpha = s; });
                        alp3.Run((s) => { lc.Alpha = s; });
                        DelayBeat(0.5f, () =>
                        {
                            lb.Dispose();
                            lc.Dispose();
                        });
                    });
                    RegisterFunction("LLine2", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(T(0.5f), ValueEasing.EaseInCubic(0.75f * 0.5f, 0, T(0.5f)));
                        Line la = new(80, 90);
                        Line lb = new(80 + 60, 90);
                        Line[] l = { la, lb };
                        foreach (Line a in l)
                        {
                            CreateEntity(a);
                            alp.Run((s) => { a.Alpha = s; });
                            DelayBeat(0.5f, () => { a.Dispose(); });
                        }
                    });
                    RegisterFunction("RLine2", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(T(0.5f), ValueEasing.EaseInCubic(0.75f * 0.5f, 0, T(0.5f)));
                        Line la = new(640 - 80, 90);
                        Line lb = new(640 - 80 - 60, 90);
                        Line[] l = { la, lb };
                        foreach (Line a in l)
                        {
                            CreateEntity(a);
                            alp.Run((s) => { a.Alpha = s; });
                            DelayBeat(0.5f, () => { a.Dispose(); });
                        }
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "","","","",  "","","","",
                        "Line1","","","",  "","","","",
                        "XLine(LLine)","","","",  "","","","",
                        "Line2","","","",  "","","","",
                        "XLine2(RLine)","","","",  "","","","",
                        "Line3","","","",  "","","","",
                        "XLine(XLine2)(XLine3)(LLine2)","","XLine3(RLine2)","",  "","","","",
                        "Line4","","","",  "","","","",
                    });
                }
                if (InBeat(200, 231) && Gametime % 24 == 0)
                {
                    int count = 0;
                    int[] num = { 2, 0, 2, 1, 1, 0, 1, 8 };
                    AddInstance(new TimeRangedEvent(24, () =>
                    {
                        if (count % 3 == 0)
                        {
                            NameShower.level = num[count / 3].ToString();
                        }
                        count++;
                    }));
                }
                if (InBeat(200, 216) && Gametime % 8 == 0)
                {
                    EfEn ef = new(new V(Rand(0f, 640), 500), Sprites.player) { rot = 180 };
                    ef.vec2Ease(true, ef.centre, new V(0, -520), T(4), Cu);
                    ef.AutoDis(T(4));
                    ef.AlphaEase(true, 1, 0, T(4), Cu);
                    CreateEntity(ef);
                }
                if (InBeat(216))
                {
                    RegisterFunctionOnce("LineRotL", () =>
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            float x = 640 / 15 * i;
                            float y = 480 / 15 * i;
                            Line l =
                            new Line(
                                LinkEase(Stable(0, new V(0, 480)), EaseOut(T(2), new V(x, 0), Cu),
                                         Stable(0, V.Zero), EaseOut(T(2), new V(640 - x, 0), Cu)),
                                LinkEase(Stable(0, new V(640, 480)), EaseOut(T(2), new V(0, -y), Cu),
                                         Stable(0, V.Zero), EaseOut(T(2), new V(0, -480 + y), Cu)))
                            {
                                controlLayer = UIS().UISurface,
                                Alpha = 0.5f,
                                ObliqueMirror = true
                            };
                            CreateEntity(l);
                            l.DelayAlphaDecrease(T(2), T(2));
                            l.DelayDispose(T(4));
                        }

                    });
                    RegisterFunctionOnce("LineRotR", () =>
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            float x = 640 / 15 * i;
                            float y = 480 / 15 * i;
                            Line l =
                            new Line(
                                LinkEase(Stable(0, new V(0, 0)), EaseOut(T(2), new V(x, 0), Cu),
                                         Stable(0, V.Zero), EaseOut(T(2), new V(640 - x, 0), Cu)),
                                LinkEase(Stable(0, new V(640, 0)), EaseOut(T(2), new V(0, y), Cu),
                                         Stable(0, V.Zero), EaseOut(T(2), new V(0, 480 - y), Cu)))
                            {
                                controlLayer = UIS().UISurface,
                                Alpha = 0.5f,
                                ObliqueMirror = true
                            };
                            CreateEntity(l);
                            l.DelayAlphaDecrease(T(2), T(2));
                            l.DelayDispose(T(4));
                        }

                    });
                    const float line1Alpha = 0.5f;
                    float time = T(2);
                    RegisterFunctionOnce("Line1", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(120, 480), new(-20, 200), time), ValueEasing.EaseOutCubic(0, -90, time));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(2, line.Dispose);
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(520, 480), new(660, 200), time), ValueEasing.EaseOutCubic(0, 90, time));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(2, line.Dispose);
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(-10, 280), new(120, -15), time), ValueEasing.EaseOutCubic(90, 0, time));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(2, line.Dispose);
                    });
                    RegisterFunctionOnce("Line4", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(650, 280), new(520, -15), time), ValueEasing.EaseOutCubic(-90, 0, time));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(2, line.Dispose);
                    });

                    RegisterFunctionOnce("Line5", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuart(new(320, 500), new(320, -20), time), ValueEasing.Stable(0));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(0.5f, 0.5f));
                        line.InsertRetention(new(1f, 0.3f));
                        DelayBeat(2, line.Dispose);
                    });
                    RegisterFunctionOnce("LLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutCubic(new(320, 550), new(320, -80), time), ValueEasing.Stable(11));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(2, line.Dispose);
                    });
                    RegisterFunctionOnce("RLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutCubic(new(320, 550), new(320, -80), time), ValueEasing.Stable(-11));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(2, line.Dispose);
                    });
                    CreateChart(0, T(2), 0, new[]
                    {
                        "Line1","Line2","Line1","Line2",  "Line1","Line2","Line1","Line2",
                        "Line1","Line2","Line1","Line2",  "Line1","Line2","Line1","Line2",
                        "Line3","Line4","Line3","Line4",  "Line3","Line4","Line3","Line4",
                        "Line3","Line4","Line3","Line4",  "Line3","Line4","Line3","Line4",
                        "LLine","RLine","LLine","RLine",  "LLine","RLine","LLine","RLine",
                        "LLine","RLine","LLine","RLine",  "LLine","RLine","LLine","RLine",
                        "Line1","Line2(Line5)","Line1","Line2(Line5)",  "Line1","Line2(Line5)","Line1","Line2(Line5)",
                        "Line1","Line2(Line5)","Line1","Line2(Line5)",  "Line1","Line2(Line5)","Line1","Line2(Line5)",
                    });
                    RegisterFunctionOnce("CentreLine", () =>
                    {
                        Line l = new(Stable(0, WCentre),
                                         LinkEase(Stable(0, 0), EaseIn(T(14), 360 * 3, Cu),
                                                  Stable(0, 0), EaseOut(T(2), 15, Cu)));
                        Line l2 = new(Stable(0, WCentre),
                                     LinkEase(Stable(0, 90), EaseIn(T(14), 360 * 3, Cu),
                                              Stable(0, 0), EaseOut(T(2), 15, Cu)));
                        Line l3 = new(Stable(0, WCentre),
                                     LinkEase(Stable(0, 0), EaseIn(T(14), 360 * 3, Cu),
                                              Stable(0, 0), EaseOut(T(2), -15, Cu)));
                        Line l4 = new(Stable(0, WCentre),
                                     LinkEase(Stable(0, 90), EaseIn(T(14), 360 * 3, Cu),
                                              Stable(0, 0), EaseOut(T(2), -15, Cu)));
                        foreach (Line ls in new Line[] { l, l2, l3, l4 })
                        {
                            float alpha = 1;
                            CreateEntity(ls);
                            ls.Alpha = 0;
                            ls.AlphaIncrease(T(0.5f), alpha);
                            for (int i2 = 0; i2 < 5; i2++)
                            {
                                DelayBeat(0.25f * i2, () =>
                                {
                                    ls.Alpha = 0;
                                    ls.AlphaIncrease(T(0.25f), alpha);
                                });
                            }
                            ls.DelayAlphaDecrease(T(14), T(2));
                            for (int i = 1; i < 10; i++)
                                ls.InsertRetention(new(i,0.5f-i*0.05f));
                            RunEase(s => ls.DrawingColor = C.Lerp(C.Lerp(C.LightSkyBlue, C.LightCoral, s.X),C.White,s.Y),
                                Combine(LinkEase(
                                    Stable(T(4), 0), EaseOut(T(2), 1, ES.Linear),
                                    Stable(T(2), 0), EaseOut(T(2), -1, ES.Linear)),
                                    LinkEase(Stable(T(12)),EaseOut(T(2),0,1,ES.Linear))));
                        }
                    });
                    
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "LineRotL(CentreLine)","","","",  "","","","",
                        "","","","",  "","","","",
                        "LineRotR","","","",  "","","","",
                        "","","","",  "","","","",
                        "LineRotL","","","",  "","","","",
                        "","","","",  "","","","",
                        "LineRotL(LineRotR)","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region Minecraft Effect
                if (InBeat(232,248)) 
                {
                    ScreenAngle = Sin(Gametime*180/T(2)) * 20;
                }
                if (InBeat(232)) 
                {
                    RegisterFunctionOnce("zome1", () => RunEase(s => ScreenScale = s,EaseOut(T(1),1.2f,1,ES.Sine)));
                    RegisterFunctionOnce("zome2", () => RunEase(s => ScreenScale = s, EaseOut(T(2), 1.4f, 1, ES.Sine)));
                    for(int i=0;i<4;i++)
                    CreateChart(T(i*4), T(2), 0, new[] 
                    {
                        "zome1","","","",  "zome1","","","",
                        "zome2","","","",  "","","","",
                    });
                }
                if (InBeat(232))
                {
                    BackGroundColor = new C(0, 255, 0);
                    Line3d l = new(new V(320, 240), new Vector3(200)) { Width = 3, DrawingColor = new C(0, 255, 0) };
                    CreateEntity(l);
                    Line3d l2 = new(new V(320, 240), new Vector3(100)) { Width = 3, DrawingColor = new C(0, 255, 0) };
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        l.回転 += new Vector3(2*Sin(Gametime));
                        l2.回転 -= new Vector3(2*Cos(Gametime));
                    }));
                    DelayBeat(16, () =>
                    {
                        l.Dispose();
                        l2.Dispose();
                    });
                }
                if (InBeat(232, 246))
                {
                    EfEn ef = new(new V(-50, Rand(0, 480)), Sprites.square) { Size = new(1f), Color = C.Green };
                    ef.AlphaEase(false, 1, 0, T(4), ES.Linear);
                    ef.vec2Ease(false, new V(-80, Rand(0, 480)), new V(Rand(200, 400), 0), T(4), Cu);
                    ef.rotEase(false, 0, T(4) * -3*RandSignal(), T(4), Cu);
                    CreateEntity(ef);
                    ef.AutoDis(T(4));
                    EfEn ef2 = new(new V(-50, Rand(0, 480)), Sprites.square) { Size = new(1f), Color = C.Green };
                    ef2.AlphaEase(false, 1, 0, T(4), Cu);
                    ef2.vec2Ease(false, new V(720, Rand(0, 480)), new V(Rand(200, 400) * -1, 0), T(4), Cu);
                    ef2.rotEase(false, 0, T(4) * 3*RandSignal(), T(4), ES.Linear);
                    CreateEntity(ef2);
                    ef2.AutoDis(T(4));
                }
                if (InBeat(242)) 
                {
                    RunEase(s =>
                    {
                        foreach (var t in GetAll<EfEn>())
                            t.Color = C.Lerp(C.Green, C.DarkRed, s);
                        foreach (var t in GetAll<Line3d>())
                            t.DrawingColor = C.Lerp(new C(0,255,0), C.Red, s);
                    }, EaseOut(T(4), 0, 1, ES.Sine));
                }
                #endregion
                #region Ressurection
                if (InBeat(248))
                {
                    foreach (var t in GetAll<EfEn>()) t.Dispose();
                    ScreenAngle = 0;
                    float s = 0;
                    for (int i = 0; i < 16; i++)
                    {
                        DelayBeat(i * 0.5f, () =>
                        {
                            Line l = new(LinkEase(Stable(0, new V(0, 240)), EaseIn(T(2), new V(200, 0), Cu)), Stable(0, 90)) { TransverseMirror = true, DrawingColor = C.Lerp(C.Transparent, C.Red, s) };
                            l.AlphaDecrease(T(2));
                            CreateEntity(l);
                            l.DelayDispose(T(2));
                            s += 0.125f;
                        });
                    }
                }
                if (InBeat(248))
                {
                    RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                        LinkEase(Stable(0, 0), EaseOut(T(1f), -20, ES.Cubic)));
                    DelayBeat(1, () =>
                    {
                        RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                        LinkEase(Stable(0, 0), EaseOut(T(1f), 40, ES.Cubic)));
                    });
                    for (int i = 0; i < 3; i++)
                    {
                        DelayBeat(2 + i * 2, () =>
                        {
                            RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                            LinkEase(Stable(0, 20), EaseOut(T(1f), -40, ES.Cubic)));
                            DelayBeat(1, () =>
                            {
                                RunEase((s) => { ScreenPositionDelta = new V(s, 0); },
                                LinkEase(Stable(0, -20), EaseOut(T(1f), 40, ES.Cubic)));
                            });
                        });
                    }
                }
                if (InBeat(248, 255) && At0thBeat(2f))
                {
                    Line l = new(new V(BoxStates.Left, 0), 90) { DrawingColor = C.Gold };
                    l.AlphaDecrease(T(1));
                    CreateEntity(l);
                    l.DelayDispose(T(2));
                    DelayBeat(1f, () =>
                    {
                        Line l = new(new V(BoxStates.Right, 0), 90) { DrawingColor = C.Gold };
                        l.AlphaDecrease(T(1));
                        CreateEntity(l);
                        l.DelayDispose(T(2));

                    });
                }
                #endregion
                #region ??? Effect
                if (InBeat(264))
                {
                    Shader shader = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\Split"));
                    RenderProduction e = ActivateShader(shader, 0.999321234f);
                    shader.Parameters["width"].SetValue(1);
                    shader.Parameters["count"].SetValue(170);
                    shader.Parameters["width2"].SetValue(-1);
                    shader.Parameters["count2"].SetValue(170);
                    float time = 0;
                    AddInstance(new TimeRangedEvent(T(64), () =>
                    {
                        shader.Parameters["time"].SetValue(time / 50f);
                        shader.Parameters["count2"].SetValue(170+120*Sin(Gametime/2f));
                        shader.Parameters["count"].SetValue(170 + 120 * Cos(Gametime / 2f));
                        time++;
                    }));
                    DelayBeat(64, () =>
                    {
                        e.Dispose();
                    });
                }
                if (InBeat(264, 293) && At0thBeat(8)) 
                {
                    DelayBeat(1.9f, () =>
                    {
                        for (int a = 0; a < 90; a++)
                        {
                            AddInstance(new InstantEvent(a * 0.2f, () =>
                            {
                                float rot = -Rand(80, 105);
                                float speed = Rand(10f, 18f);
                                CreateEntity(new Particle(
                                    new(Rand(150, 222), LastRand, LastRand),
                                   new(
                                       Cos(rot) * speed,
                                       Sin(rot) * speed),
                                   Rand(6, 54),
                                   new(Rand(0, 640), 500), Sprites.lightBall)
                                {
                                    AutoRotate = true,
                                    SlowLerp = -Rand(0.070f, 0.080f)
                                });
                            }));

                        }
                        DelayBeat(4, () =>
                        {
                            if (Gametime > T(290f)) return;
                            for (int a = 0; a < 90; a++)
                            {
                                AddInstance(new InstantEvent(a * 0.2f, () =>
                                {
                                    float rot = Rand(80, 100);
                                    float speed = Rand(10f, 18f);
                                    CreateEntity(new Particle(
                                        new(Rand(150, 222), LastRand, LastRand),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(6, 73),
                                       new(Rand(0, 640), -20), Sprites.lightBall)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.070f, 0.080f)
                                    });
                                }));
                            }
                        });
                    });
                }
                if (InBeat(264, 293) && At0thBeat(2))
                {
                    float rot = Rand(0, 360);
                    for (int i = 0; i < 4; i++)
                    {
                        Line l = new(LinkEase(Stable(0, WCentre), EaseIn(T(8), GV(500, rot + i * 360 / 8), Cu)),
                                     Stable(0, rot + 90 + i * 360 / 8))
                        { ObliqueMirror = true, Alpha = 0 };
                        CreateEntity(l);
                        l.AlphaIncrease(T(4), 0.5f);
                        l.DelayDispose(T(8));
                    }
                }
                if (InBeat(264, 294) && At0thBeat(0.1f))
                {
                    float rot = Rand(0, 64) * 360 / 64f;
                    Line l = new(LinkEase(Stable(0, WCentre + GV(300, rot)), EaseIn(T(8), GV(-300, rot), Cu)),
                                     Stable(0, rot + 90));
                    CreateEntity(l);
                    l.AlphaDecrease(T(Rand(7f, 8f)));
                    l.DelayDispose(T(8));
                }
                if (InBeat(264, 293) && At0thBeat(1f))
                {
                    float rot = Rand(0, 360);
                    Line l = new(LinkEase(Stable(0, WCentre + GV(400, rot)), EaseIn(T(8), GV(-800, rot), ES.Sine)),
                                 LinkEase(Stable(0, rot + 90)));
                    Line l2 = new(LinkEase(Stable(0, WCentre + GV(420, rot)), EaseIn(T(8), GV(-800, rot), ES.Sine)),
                                 LinkEase(Stable(0, rot + 90)));
                    l.DelayDispose(T(8));
                    l2.DelayDispose(T(8));
                    CreateEntity(new Entity[] { l, l2 });


                }
                if (InBeat(264, 293) && At0thBeat(0.2f))
                {
                    EfEn ef = new(V.Zero, Sprites.arrow[Rand(0, 3), Rand(0, 3), 0]) { Size = new V(Rand(0f, 1f)) };
                    ef.vec2Ease(false, new V(Rand(0, 640), 480), new V(0, -480), T(8), Cu);
                    CreateEntity(ef);
                    float Ra = Rand(0, 360);
                    ef.rotEase(false, Ra, (Ra + 1 * T(8)) * RandSignal(), T(8), Cu);
                    ef.AutoDis(T(8));
                }

                #endregion
                #region ??? Effect2
                if (InBeat(296, 303) && At0thBeat(2f))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Line l = new(WCentre + GV(300 - i * 10, 0), WCentre + GV(250 - i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l);
                        l.DelayDispose(T(i * 0.1f + 2));
                        Line l2 = new(WCentre + GV(300 - i * 10, 0), WCentre - GV(250 - i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l2.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l2.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l2);
                        l2.DelayDispose(T(i * 0.1f + 2));
                    }
                }
                if (InBeat(304, 311) && At0thBeat(2f))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Line l = new(WCentre + GV(100 + i * 10, 0), WCentre + GV(50 + i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l);
                        l.DelayDispose(T(i * 0.1f + 2));
                        Line l2 = new(WCentre + GV(100 + i * 10, 0), WCentre - GV(50 + i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l2.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l2.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l2);
                        l2.DelayDispose(T(i * 0.1f + 2));
                    }
                }
                if (InBeat(312, 319) && At0thBeat(2f))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Line l = new(WCentre + GV(250 - i * 10, 0), WCentre + GV(300 - i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l);
                        l.DelayDispose(T(i * 0.1f + 2));
                        Line l2 = new(WCentre + GV(250 - i * 10, 0), WCentre - GV(300 - i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l2.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l2.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l2);
                        l2.DelayDispose(T(i * 0.1f + 2));
                    }
                }
                if (InBeat(320, 327) && At0thBeat(2f))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Line l = new(WCentre + GV(50 + i * 10, 0), WCentre + GV(100 + i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l);
                        l.DelayDispose(T(i * 0.1f + 2));
                        Line l2 = new(WCentre + GV(50 + i * 10, 0), WCentre - GV(100 + i * 10, -90))
                        { Alpha = 0, ObliqueMirror = true };
                        l2.DelayAlphaIncrease(T(i * 0.1f), 1, 1);
                        l2.DelayAlphaDecrease(T(i * 0.1f), T(2), 1);
                        CreateEntity(l2);
                        l2.DelayDispose(T(i * 0.1f + 2));
                    }
                }
                if (InBeat(312))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Line l = new(Stable(0, new V(0, 240)),
                                    LinkEase(Stable(i, 60), EaseIn(T(12), -60, ES.Sine),
                                             Stable(0), EaseOut(T(12), -60, ES.Sine)))
                        { TransverseMirror = true };
                        Line l2 = new(Stable(0, new V(0, 240)),
                                    LinkEase(Stable(i, -60), EaseIn(T(12), 60, ES.Sine),
                                             Stable(0), EaseOut(T(12), 60, ES.Sine)))
                        { TransverseMirror = true };
                        Line l3 = new(Stable(0, new V(0, 0)),
                                    LinkEase(Stable(i, -45), EaseIn(T(12), GetAngle((float)Math.Atan2(240, 320)) + 45, ES.Sine),
                                             Stable(0), EaseOut(T(12), GetAngle((float)Math.Atan2(240, 320)) + 45, ES.Sine)))
                        { TransverseMirror = true };
                        Line l4 = new(Stable(0, new V(0, 480)),
                                    LinkEase(Stable(i, 45), EaseIn(T(12), GetAngle((float)Math.Atan2(240 - 480, 320)) - 45, ES.Sine),
                                             Stable(0), EaseOut(T(12), GetAngle((float)Math.Atan2(240 - 480, 320)) - 45, ES.Sine)))
                        { TransverseMirror = true };
                        Line l5 = new(Stable(0, new V(0, 120)),
                                    LinkEase(Stable(i, -30), EaseIn(T(12), GetAngle((float)Math.Atan2(240 - 120, 320)) + 30, ES.Sine),
                                             Stable(0), EaseOut(T(12), GetAngle((float)Math.Atan2(240 - 120, 320)) + 30, ES.Sine)))
                        { TransverseMirror = true };
                        Line l6 = new(Stable(0, new V(0, 360)),
                                    LinkEase(Stable(i, 30), EaseIn(T(12), GetAngle((float)Math.Atan2(240 - 360, 320)) - 30, ES.Sine),
                                             Stable(0), EaseOut(T(12), GetAngle((float)Math.Atan2(240 - 360, 320)) - 30, ES.Sine)))
                        { TransverseMirror = true };
                        CreateEntity(new Entity[] { l, l2, l3, l4, l5, l6 });
                        foreach (Line ls in new Line[] { l, l2, l3, l4, l5, l6 })
                        {
                            ls.Alpha = 1 - i * 0.1f;
                        };
                    }
                }
                if (InBeat(296, 327) && At0thBeat(0.5f))
                {
                    V vec = new V(Rand(120, 520), Rand(40, 440));
                    for (int i = 0; i < 16; i++)
                    {
                        Line l = new(
                            LinkEase(Stable(0, vec), EaseOut(T(4), GV(50, i * 360 / 16f), Cu)),
                            LinkEase(Stable(0, vec), EaseOut(T(4), GV(50, (i + 1) * 360 / 16f), Cu)));
                        CreateEntity(l);
                        l.DelayDispose(T(4));
                        RunEase((s) => { l.Width = s; },
                            LinkEase(Stable(0, 5f), EaseOut(T(4), -4f, Cu)));
                        l.AlphaDecrease(T(4));
                        l.DelayDispose(T(4));
                    }
                }
                #endregion
                #region BIGSHOT Effect
                if (InBeat(328))
                {
                    shaderProduction3 = ActivateShader(FightResources.Shaders.Blur);
                    //FightResources.Shaders.Blur.Sigma = 20;
                    RunEase((s) => { FightResources.Shaders.Blur.Factor = new V(Sin(90 + s), Cos(90 + s)); },
                        LinkEase(Stable(0, 0), EaseOut(T(4f), 25, ES.Cubic)));
                    RunEase((s) => { FightResources.Shaders.Blur.Sigma = s; },
                        LinkEase(Stable(0, 0), EaseOut(T(8f), 30, ES.Cubic)));
                }
                if (InBeat(336))
                {
                    RunEase((s) => { FightResources.Shaders.Blur.Factor = new V(Sin(90 + s), Cos(90 + s)); },
                        LinkEase(Stable(0, 25), EaseOut(T(4f), -50, ES.Cubic)));
                }
                if (InBeat(342))
                {
                    RunEase((s) => { FightResources.Shaders.Blur.Sigma = s; },
                        LinkEase(Stable(0, 30), EaseOut(T(2f), -30, ES.Cubic)));
                    DelayBeat(2, () => { shaderProduction3.Dispose(); });
                }

                if (InBeat(328, 335) && At0thBeat(1))
                {
                    float s = 15;
                    for (int i = 0; i < 16; i++)
                    {
                        s *= -1;
                        Line l = new(new V(650 - i * 80, 240), 90 + s);
                        Line l2 = new(new V(600 - i * 80, 240), 90 + s);
                        foreach (Line ls in new Line[] { l, l2 })
                        {
                            CreateEntity(ls);
                            ls.Alpha = 0;
                            ls.DelayAlphaIncrease(T(0.125f * i), 1, 1);
                            ls.DelayAlphaDecrease(T(0.125f * i) + 3, T(1));
                        }
                    }

                }
                if (InBeat(336, 342) && At0thBeat(1))
                {
                    float s = 15;
                    for (int i = 0; i < 16; i++)
                    {
                        s *= -1;
                        Line l = new(new V(-10 + i * 80, 240), 90 + s);
                        Line l2 = new(new V(40 + i * 80, 240), 90 + s);
                        foreach (Line ls in new Line[] { l, l2 })
                        {
                            CreateEntity(ls);
                            ls.Alpha = 0;
                            ls.DelayAlphaIncrease(T(0.125f * i), 1, 1);
                            ls.DelayAlphaDecrease(T(0.125f * i) + 3, T(1));
                        }
                    }

                }
                if (InBeat(328, 335) && At0thBeat(2))
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Line l = new(
                            LinkEase(Stable(i * 0.5f, new V(670, 240)), EaseOut(T(4), new V(-900, 0), Cu)),
                            LinkEase(Stable(i * 0.5f, new V(860, -150)), EaseOut(T(4), new V(-900, 0), Cu)))
                        { VerticalMirror = true, Alpha = 1 - i * 1 / 40f, DrawingColor = C.Gold };
                        CreateEntity(l);
                    }
                }
                if (InBeat(336, 341) && At0thBeat(2))
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Line l = new(
                            LinkEase(Stable(i * 0.5f, new V(-30, 240)), EaseOut(T(4), new V(900, 0), Cu)),
                            LinkEase(Stable(i * 0.5f, new V(-230, -150)), EaseOut(T(4), new V(900, 0), Cu)))
                        { VerticalMirror = true, Alpha = 1 - i * 1 / 40f, DrawingColor = C.Gold };
                        CreateEntity(l);
                    }
                }
                if (InBeat(328))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    RegisterFunctionOnce("Mtt", () =>
                    {
                        float speed = Rand(5f, 8f) * -1;
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new V(700, 380 + offset)),
                          Combine(InfLinear(0, speed), SineWave(40, T(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.damage = false;
                        mt.InsertShot(4, T(2f));
                    });
                    RegisterFunctionOnce("Mtt2", () =>
                    {
                        float speed = Rand(5f, 8f) * -1;
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new V(700, 100 + offset)),
                          Combine(InfLinear(0, speed), SineWave(40, T(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.damage = false;
                        mt.InsertShot(4, T(2f));
                    });
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(Stable(0), EaseOut(T(8), 25, Cu)));
                    RunEase((s) => { ScreenPositionDelta = s; },
                        LinkEase(Stable(0, new V()), EaseOut(T(4), new V(50, 0), Cu)));
                    CreateChart(T(0), T(2), 0, new string[]
                    {
                        "Mtt","","Mtt2","",  "Mtt","","Mtt2","",
                        "Mtt","","Mtt2","",  "Mtt","","Mtt2","",
                        "Mtt","","Mtt2","",  "Mtt","","Mtt2","",
                        "Mtt","","Mtt2","",  "Mtt","","Mtt2","",

                    });
                }
                if (InBeat(336))
                {
                    RegisterFunctionOnce("Mtt", () =>
                    {
                        float speed = Rand(5f, 8f) * 1;
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new V(-60, 380 + offset)),
                          Combine(InfLinear(0, speed), SineWave(40, T(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.damage = false;
                        mt.InsertShot(4, T(2f));
                    });
                    RegisterFunctionOnce("Mtt2", () =>
                    {
                        float speed = Rand(5f, 8f) * 1;
                        float offset = Rand(-80, 80);
                        var ce = LinkEase(Stable(0, new V(-60, 100 + offset)),
                          Combine(InfLinear(0, speed), SineWave(40, T(6)))
                            );
                        ParasolMett mt = new(ce);
                        CreateEntity(mt);
                        mt.BulletDepth = 0.3f;
                        mt.damage = false;
                        mt.InsertShot(4, T(2f));
                    });
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(Stable(0, 25), EaseOut(T(4), -50, Cu)));
                    RunEase((s) => { ScreenPositionDelta = s; },
                        LinkEase(Stable(0, new V(50, 0)), EaseOut(T(4), new V(-100, 0), Cu)));
                    CreateChart(T(0), T(2), 0, new string[]
                    {
                        "Mtt","","Mtt2","",  "Mtt","","Mtt2","",
                        "Mtt","","Mtt2","",  "Mtt","","Mtt2","",

                    });
                }
                if (InBeat(342))
                {
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(Stable(0, -25), EaseOut(T(4), 25, Cu)));
                    RunEase((s) => { ScreenPositionDelta = s; },
                        LinkEase(Stable(0, new V(-50, 0)), EaseOut(T(4), new V(50, 0), Cu)));
                }
                if (InBeat(344))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        var f = FightResources.Shaders.Fire;
                        shaderProduction4 = ActivateShaderBack(f, 0.6f);
                        f.Speed = 3;
                        f.Blend = C.Red;
                        RunEase((s) => { FightResources.Shaders.Fire.Height = s; },
                            LinkEase(Stable(0, 0), EaseOut(T(2f), 500, ES.Cubic)));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1",
                    });
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; 
                    },
                            LinkEase(Stable(0, 1), EaseIn(T(4f), 180f, ES.Cubic),
                                     Stable(0, 0), EaseOut(T(4f), 180f, ES.Cubic)));
                    DelayBeat(8, () => { ScreenDrawing.ScreenAngle = 0; });
                }

                if (InBeat(344, 351) && At0thBeat(2))
                {
                    RunEase((s) => { ScreenScale = s;
                    },
                            LinkEase(Stable(0, 1), EaseOut(T(1f), 0.5f, ES.Cubic),
                                     Stable(0, 0), EaseIn(T(1f), -0.5f, ES.Cubic)));
                }
                #endregion
                #region spin Effect
                if (InBeat(360))
                {
                    RegisterFunctionOnce("LF+", () => 
                    {
                        RunEase((s) => { ScreenScale = s; },
                            LinkEase(Stable(0, 1.4f), EaseOut(T(2f), -0.4f, ES.Cubic))); SetSoul(0);
                        V rand = new V(Rand(100f, 540f), Rand(120f, 300f));
                        Line l = new(
                            LinkEase(Stable(0, rand),
                            EaseIn(T(2), new V(0, -200), Cu)), EaseOut(T(5), 45, 135, ES.Quart));
                        Line l2 = new(
                            LinkEase(Stable(0, rand),
                            EaseIn(T(2), new V(0, -200), Cu)), EaseOut(T(5), 135, 225, ES.Quart));
                        foreach (Line ls in new[] { l, l2 })
                        {
                            CreateEntity(ls);
                            ls.AlphaDecrease(T(2));
                            for (int i = 0; i < 20; i++) 
                            {
                                ls.InsertRetention(new(i,0.75f-i*0.0375f));
                            }
                            
                        }
                        Line l3 = new(EaseOut(T(2),new V(-20,240),new V(70,240),Cu),Stable(0,80));
                        Line l4 = new(EaseOut(T(2), new V(-20, 240), new V(70, 240), Cu), Stable(0, 100));
                        foreach (Line ls in new[] { l3,l4 }) 
                        {
                            CreateEntity(ls);
                            ls.AlphaDecrease(T(2));
                            ls.Width = 40;
                            ls.DrawingColor = C.LightSkyBlue;
                        }
                    });
                    RegisterFunctionOnce("LF-", () =>
                    {
                        RunEase((s) => { ScreenScale = s; },
                            LinkEase(Stable(0, 1.4f), EaseOut(T(2f), -0.4f, ES.Cubic))); SetSoul(0);
                        V rand = new V(Rand(100f, 540f), Rand(120f, 300f));
                        Line l = new(
                            LinkEase(Stable(0, rand),
                            EaseIn(T(2), new V(0, -200), Cu)), EaseOut(T(5), 45, -45, ES.Quart));
                        Line l2 = new(
                            LinkEase(Stable(0, rand),
                            EaseIn(T(2), new V(0, -200), Cu)), EaseOut(T(5), 135, 45, ES.Quart));
                        foreach (Line ls in new[] { l, l2 })
                        {
                            CreateEntity(ls);
                            ls.AlphaDecrease(T(2));
                            for (int i = 0; i < 20; i++)
                            {
                                ls.InsertRetention(new(i, 0.75f - i * 0.0375f));
                            }
                        }
                        Line l3 = new(EaseOut(T(2), new V(660, 240), new V(570, 240), Cu), Stable(0, 80));
                        Line l4 = new(EaseOut(T(2), new V(660, 240), new V(570, 240), Cu), Stable(0, 100));
                        foreach (Line ls in new[] { l3, l4 })
                        {
                            CreateEntity(ls);
                            ls.AlphaDecrease(T(2));
                            ls.Width = 40;
                            ls.DrawingColor = C.LightSkyBlue;
                        }
                    });
                    RegisterFunctionOnce("s1", () => 
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Line l5 = new(Stable(0, new V(320, 480)), EaseOut(T(2), 0, i * 18, Cu));
                            CreateEntity(l5);
                            l5.AlphaDecrease(T(1+i*0.1f));
                            l5.DrawingColor = C.LightPink;
                        }
                    });
                    RegisterFunctionOnce("s2", () =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Line l5 = new(Stable(0, new V(320, 480)), EaseOut(T(2), 0, i * -18, Cu));
                            CreateEntity(l5);
                            l5.AlphaDecrease(T(1+i*0.1f));
                            l5.DrawingColor = C.LightPink;
                        }
                    });
                    CreateChart(0, T(2), 0, new string[] 
                    {
                        "LF+(s1)","","","",  "LF-","","","",
                        "LF+(s2)","","","",  "LF-","","","",
                        "LF+(s1)","","","",  "LF-","","","",
                        "LF+(s2)","","","",  "LF-(s1)","","LF+(s2)","",
                        "LF-(s2)","","","",  "LF+","","","",
                        "LF-(s1)","","","",  "LF+","","","",
                        "LF-(s2)","","","",  "LF+","","","",
                        "LF-(s1)","","","",  "","","","",
                    });
                }
                if (InBeat(362, 374) && At0thBeat(4))
                {
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(
                            Stable(0, 25), EaseOut(T(1f), -25, ES.Cubic),
                            Stable(0, 0), EaseIn(T(1), -25, Cu)));
                    //RunEase((s) => { ScreenPositionDelta = s; }, LinkEase(Stable(0, new V(-150,0)), EaseOut(T(2f), new V(150,0), ES.Cubic)));
                    DelayBeat(2, () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(
                            Stable(0, -25), EaseOut(T(1f), 25, ES.Cubic),
                            Stable(0, 0), EaseIn(T(1), 25, Cu)));
                        //RunEase((s) => { ScreenPositionDelta = s; }, LinkEase(Stable(0, new V(150, 0)), EaseOut(T(2f), new V(-150, 0), ES.Cubic)));
                    });
                }
                if (InBeat(375))
                {
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(Stable(0, ScreenDrawing.ScreenAngle), EaseOut(T(2f), 360 - ScreenDrawing.ScreenAngle, ES.Cubic)));
                }
                #endregion
                #region ULBEffect
                if (InBeat(392))
                {
                    float sin = 0;
                    for (int i = 0; i < 64; i++)
                    {
                        DelayBeat(0.25f * i, () =>
                        {
                            EfEn ef1 = new(V.Zero, null) { Bone = true, Size = new V(2f), Depth = 0.15f };
                            ef1.Len = 75 + Sin(sin) * 50;
                            ef1.vec2Ease(false, new V(690, 5 + ef1.Len), new V(-740, 0), T(3), Li);
                            EfEn ef2 = new(V.Zero, null) { Bone = true, Size = new V(2f), Depth = 0.15f };
                            ef2.Len = 75 - Sin(sin) * 50;
                            ef2.vec2Ease(false, new V(690, 475 - ef2.Len), new V(-740, 0), T(3), Li);
                            CreateEntity(new EfEn[] { ef1, ef2 });
                            foreach (EfEn i in new EfEn[] { ef1, ef2 })
                            { i.AutoDis(T(3)); }
                            sin += 7;
                        });
                        DelayBeat(0.25f * i + 0.125f, () =>
                        {
                            EfEn ef1 = new(V.Zero, null) { Bone = true, Size = new V(2f), Depth = 0.15f };
                            ef1.Len = 75 + Sin(sin) * 90;
                            ef1.vec2Ease(false, new V(690, 5 + ef1.Len), new V(-740, 0), T(3), Li);
                            EfEn ef2 = new(V.Zero, null) { Bone = true, Size = new V(2f), Depth = 0.15f };
                            ef2.Len = 75 - Sin(sin) * 90;
                            ef2.vec2Ease(false, new V(690, 475 - ef2.Len), new V(-740, 0), T(3), Li);
                            CreateEntity(new EfEn[] { ef1, ef2 });
                            foreach (EfEn i in new EfEn[] { ef1, ef2 })
                            { i.AutoDis(T(3)); }
                            sin += 7;
                        });
                    }
                }
                if (InBeat(392, 407) && At0thBeat(0.15f))
                {
                    EfEn ef1 = new(new V(690, 0), null) { Bone = true, Len = Rand(50f, 100f), Size = new V(3), Depth = 0.1f };
                    ef1.Color = C.Lerp(C.White, C.Black, 0.5f);
                    ef1.vecEase(false, new V(690, 0), new V(-50, 0), T(4), Li);
                    ef1.AutoDis(T(4));
                    CreateEntity(ef1);
                    EfEn ef2 = new(new V(-50, 0), null) { Bone = true, Len = Rand(50, 100f), Size = new V(3), Depth = 0.1f };
                    ef2.Color = C.Lerp(C.White, C.Black, 0.5f);
                    ef2.vecEase(false, new V(690, 480), new V(-50, 480), T(4), Li);
                    ef2.AutoDis(T(4));
                    CreateEntity(ef2);
                    foreach (EfEn i in new EfEn[] { ef1, ef2 })
                    { i.AutoDis(T(4)); }
                }
                if (InBeat(400, 407) && At0thBeat(2))
                {
                    EfEn ef = new(new V(0, -50), null) { Bone = true, Len = 0, Size = new V(7), Depth = 0.13f, rot = Rand(-25f, 25f) };
                    ef.Color = C.Lerp(C.White, C.Black, 0.3f);
                    CreateEntity(ef);
                    ef.vec2Ease(false, new V(0, 520), new V(850, 0), T(7), Li);
                    ef.LenEase(false, 0, 100, T(1), Cu);
                    ef.AutoDis(T(7));
                    DelayBeat(1, () =>
                    {
                        EfEn ef = new(new V(0, -50), null) { Bone = true, Len = 0, Size = new V(7), Depth = 0.13f, rot = Rand(-25f, 25f) };
                        ef.Color = C.Lerp(C.White, C.Black, 0.3f);
                        CreateEntity(ef);
                        ef.vec2Ease(false, new V(0, -50), new V(850, 0), T(7), Li);
                        ef.LenEase(false, 0, 100, T(1), Cu);
                        ef.AutoDis(T(7));
                    });
                }
                #endregion
                #region indhome? Effect
                if (InBeat(408, 415) && At0thBeat(0.25f))
                {
                    EfEn ef = new(V.Zero, null) { Bone = true, Tags = new string[] { "a" }, rot = 90, Size = new V(3), Depth = 0.16f };
                    CreateEntity(ef);
                    ef.vec2Ease(false, new V(0, -50), new V(0, 600), T(4), Li);
                    EfEn ef2 = new(V.Zero, null) { Bone = true, Tags = new string[] { "b" }, rot = 90, Size = new V(3), Depth = 0.16f };
                    CreateEntity(ef2);
                    ef2.vec2Ease(false, new V(640, -50), new V(0, 600), T(4), Li);
                    ef.AutoDis(T(4));
                    ef2.AutoDis(T(4));
                }
                if (InBeat(408))
                {
                    EfEn ef = new(new V(0, 600), Sprites.player) { Size = new V(3), rot = 180, Color = C.Blue };
                    CreateEntity(ef);
                    float sin = 0;
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        foreach (EfEn e in GetAll<EfEn>("a"))
                        {
                            e.Len = 150 + Sin(sin) * 100;
                        }
                        foreach (EfEn e in GetAll<EfEn>("b"))
                        {
                            e.Len = 150 - Sin(sin) * 100;
                        }
                        ef.centre = new V(320 + Sin(sin) * 100, ef.centre.Y - 3);
                        sin += 3;

                    }));
                    for (int i = 0; i < 4; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                EfEn ef2 = new(V.Zero, null) { Bone = true, Len = 40, Size = new V(3), Depth = 0.16f };
                                CreateEntity(ef2);
                                ef2.vec2Ease(false, new V(-100, ef.centre.Y), new V(870, 0), T(3), Li);
                                ef2.rotEase(false, 90 * i, T(6) * 3 + 90 * i, T(3), Li);
                                ef2.AutoDis(T(3));
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                EfEn ef2 = new(V.Zero, null) { Bone = true, Len = 40, Size = new V(3), Depth = 0.16f };
                                CreateEntity(ef2);
                                ef2.vec2Ease(false, new V(770, ef.centre.Y), new V(-870, 0), T(3), Li);
                                ef2.rotEase(false, 90 * i, T(6) * 3 + 90 * i, T(3), Li);
                                ef2.AutoDis(T(3));
                            }
                        });
                    }
                }
                #endregion
                #region HelltakerEffect
                if (InBeat(416))
                {
                    PlaySound(Sounds.pierce);
                    for (int i = 0; i < 16; i++)
                    {
                        EfEn ef = new(WCentre, null) { Bone = true, Size = new(3), Len = 100, rot = i * 360f / 16f };
                        CreateEntity(ef);
                        float ss = 0;
                        RunEase((s) => { ss = s; },
                            LinkEase(Stable(0, 300), EaseOut(T(4f), -200, Ba)));
                        AddInstance(new TimeRangedEvent(T(9), () =>
                        {
                            ef.rot += 2;
                            ef.centre = WCentre + GV(ss * ef.Size.X, ef.rot - 90f);
                        }));
                    }
                    EfEn ef2 = new(WCentre, null) { Bone = true, Size = new(3), Color = C.Aqua, rot = 90 };
                    CreateEntity(ef2);
                    EfEn ef3 = new(WCentre, null) { Bone = true, Size = new(3), Color = C.Orange, rot = 0 };
                    CreateEntity(ef3);
                    RunEase((s) =>
                    {
                        foreach (EfEn e in new EfEn[] { ef2, ef3 })
                        { e.Len = s; }
                    },
                        LinkEase(Stable(0, 0), EaseOut(T(4f), 80, El)));
                    AddInstance(new TimeRangedEvent(T(9), () =>
                    {
                        foreach (EfEn e in new EfEn[] { ef2, ef3 })
                        { e.rot -= 2; }
                    }));

                }
                if (InBeat(422))
                {
                    Shader shader = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\SCSplit"));
                    shaderProduction4 = ActivateShader(shader, 0.62347234f);
                    shader.Parameters["type"].SetValue(0);
                    shader.Parameters["width"].SetValue(0);
                    shader.Parameters["height"].SetValue(0);
                    shader.Parameters["centre"].SetValue(new V(320, 240));
                    RunEase((s) => { shader.Parameters["width"].SetValue(s); },
                        LinkEase(Stable(0, 0), EaseIn(T(2f), 640, ES.Cubic)));
                    DelayBeat(2, () =>
                    {
                        shader.Parameters["height"].SetValue(640);
                        shader.Parameters["width"].SetValue(0);
                        RunEase((s) => { shader.Parameters["height"].SetValue(s); },
                        LinkEase(Stable(0, 640), EaseOut(T(4f), -640, El)));
                        DelayBeat(4, () =>
                        {
                            shaderProduction4.Dispose();
                        });
                    });
                }
                if (InBeat(424))
                {
                    foreach (EfEn e in GetAll<EfEn>())
                    {
                        e.Dispose();
                    }

                }
                #endregion
                #region Undyne&&asgore Effect
                if (InBeat(424, 430) && At0thBeat(1))
                {
                    SetPlayerMission(0);
                    Line l = new(new V(Functions.Heart.Centre.X + 10, 240), 90);
                    Line l2 = new(new V(Functions.Heart.Centre.X - 10, 240), 90);
                    foreach (Line ls in new Line[] { l, l2 })
                    {
                        CreateEntity(ls);
                        ls.DrawingColor = new(255, 100, 100);
                        ls.DelayDispose(T(1));
                        ls.AlphaDecrease(T(0.5f));
                    }
                    DelayBeat(0.5f, () =>
                    {
                        SetPlayerMission(1);
                        Line l = new(new V(Functions.Heart.Centre.X + 10, 240), 90);
                        Line l2 = new(new V(Functions.Heart.Centre.X - 10, 240), 90);
                        foreach (Line ls in new Line[] { l, l2 })
                        {
                            CreateEntity(ls);
                            ls.DrawingColor = new(100, 180, 255);
                            ls.DelayDispose(T(1));
                            ls.AlphaDecrease(T(0.5f));
                        }
                    });
                }
                if (InBeat(424, 431))
                {
                    EfEn e = new(new(), Sprites.arrow[0, 1, 0]) { rot = 180 };
                    EfEn e2 = new(new(), Sprites.arrow[1, 1, 0]);
                    e.vec2Ease(false, new V(-20, Rand(0, 480)), new V(Rand(100, 200), 0), T(1), Cu);
                    e2.vec2Ease(false, new V(660, Rand(0, 480)), new V(Rand(100, 200) * -1, 0), T(1), Cu);
                    foreach (EfEn efs in new EfEn[] { e, e2 })
                    {
                        efs.AlphaEase(false, 1, 0, T(0.7f), Li);
                        efs.AutoDis(T(2));
                        CreateEntity(efs);
                        efs.SetTag("Dispose");
                    }
                }
                if (InBeat(432))
                {
                    foreach (EfEn efs in GetAll<EfEn>("Dispose"))
                    {
                        efs.Dispose();
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        EfEn ef = new(V.Zero, Loader.Load<Texture2D>("Musics\\BadAppleRE\\asgoreArrow")) { Size = new V(0.5f), rot = 180 + Sin(i * 72) * 10 };
                        CreateEntity(ef);
                        ef.vec2Ease(false, new V(320 - 300 + i * 150, 900), new V(0, -250 - Sin(36 * i) * 200), T(4), Cu);
                        ef.AutoDis(T(8));
                    }
                }
                if (InBeat(432, 438) && At0thBeat(2))
                {
                    for (int i = 0; i < 9; i++)
                    {
                        Line l = new(Stable(0, new V(320 - 75 * 4 + i * 75, 240)), LinkEase(Stable(T(i * 0.125f), 90), EaseOut(T(0.5f), 20, Cu))) { Alpha = 0, DrawingColor = C.Red };
                        l.DelayAlphaIncrease(T(i * 0.125f), 1, 1);
                        l.DelayAlphaDecrease(T(i * 0.125f) + 2, T(0.5f));
                        CreateEntity(l);
                        l.DelayDispose(T(20));
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        Line l = new(new V(0, 480), 5 - i * 10) { Alpha = 0, ObliqueMirror = true, DrawingColor = C.Gold };
                        l.DelayAlphaIncrease(T(i * 0.125f), 1, 1);
                        l.DelayAlphaDecrease(T(i * 0.125f) + 2, T(0.5f));
                        CreateEntity(l);
                        l.DelayDispose(T(20));
                    }
                    DelayBeat(1, () =>
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            Line l = new(new V(0, 0), 5 + i * 10) { Alpha = 0, ObliqueMirror = true, DrawingColor = C.Gold };
                            l.DelayAlphaIncrease(T(i * 0.125f), 1, 1);
                            l.DelayAlphaDecrease(T(i * 0.125f) + 2, T(0.5f));
                            CreateEntity(l);
                            l.DelayDispose(T(20));
                        }
                        for (int i = 0; i < 9; i++)
                        {
                            Line l = new(Stable(0, new V(320 + 75 * 4 - i * 75, 240)), LinkEase(Stable(T(i * 0.125f), 90), EaseOut(T(0.5f), -20, Cu))) { Alpha = 0, DrawingColor = C.Red };
                            l.DelayAlphaIncrease(T(i * 0.125f), 1, 1);
                            l.DelayAlphaDecrease(T(i * 0.125f) + 2, T(0.5f));
                            CreateEntity(l);
                            l.DelayDispose(T(20));
                        }
                    });
                }
                if (InBeat(432, 440))
                {
                    ScreenDrawing.ScreenAngle = Sin((Gametime - T(432)) * 2.5f) * 5;
                }
                if (InBeat(432, 439) && At0thBeat(0.1f))
                {
                    EfEn ef = new(WCentre + new V(0, 100), Sprites.star) { Size = new(0.6f) };
                    ef.vec2Ease(false, new V(Rand(0, 640), -50), new V(0, 200), T(4), Cu);
                    ef.rotEase(false, Rand(0, 360), LastRand + 500 * RandSignal(), T(4), Cu);
                    ef.AlphaEase(false, 1, 0, T(4), Cu);
                    //ef.rot = Rand(0, 360);
                    CreateEntity(ef);
                    ef.AutoDis(T(4));
                }
                #endregion
                #region GOODWORLD Effect
                if (InBeat(440))
                {
                    float scale = 2;
                    FightResources.Shaders.Cos1Ball.ScreenScale = scale;
                    FightResources.Shaders.Cos1Ball.Intensity = 3 / scale;
                    for (int i = 0; i < 360; i++)
                    {
                        Line l = new(new V(320, 240) + GV(133 * scale, i), new V(320, 240) + GV(133 * scale, i + 1)) { controlLayer = HiddenS().UISurface, Depth = 0.2f, DrawingColor = C.Transparent, Tags = new string[] { "ball" } };
                        CreateEntity(l);
                    }
                }
                if (InBeat(410, 455) && At0thBeat(0.5f))
                {
                    Line l = new(LinkEase(Stable(0, new V(3, 240)), EaseOut(T(8), new V(634, 0), ES.Linear)), Stable(0, 90)) { controlLayer = ballL().UISurface, DrawingColor = C.Transparent, Tags = new string[] { "ball" } };
                    CreateEntity(l);
                    l.AlphaIncreaseAndDecrease(T(4), 1);
                    l.DelayDispose(T(8));
                    Line l2 = new(LinkEase(Stable(0, new V(320, 3)), EaseOut(T(8), new V(0, 474), ES.Linear)), Stable(0, 0)) { controlLayer = ballL().UISurface, Alpha = 0.5f, DrawingColor = C.Transparent, Tags = new string[] { "ball" } };
                    CreateEntity(l2);
                    l2.AlphaIncrease(T(4), 0.5f);
                    l2.DelayAlphaDecrease(T(4), T(4), 0.5f);
                    l2.DelayDispose(T(8));
                    DelayBeat(0, () =>
                    {
                        Line l = new(LinkEase(Stable(0, new V(640, 240)), EaseOut(T(8), new V(-637, 0), ES.Linear)), Stable(0, 90)) { controlLayer = ballL().UISurface, Alpha = 0.5f, DrawingColor = C.Transparent, Tags = new string[] { "ball" } };
                        CreateEntity(l);
                        l.DelayDispose(T(8));
                        Line l2 = new(LinkEase(Stable(0, new V(320, 477)), EaseOut(T(8), new V(0, -474), ES.Linear)), Stable(0, 0)) { controlLayer = ballL().UISurface, Alpha = 0.5f, DrawingColor = C.Transparent, Tags = new string[] { "ball" } };
                        CreateEntity(l2);
                        l2.DelayDispose(T(8));
                    });
                }
                if (InBeat(440))
                {
                    RunEase((s) => { foreach (Line l in GetAll<Line>("ball")) { l.DrawingColor = C.Green * s; } },
                        LinkEase(Stable(0, 0), EaseOut(T(2f), 1f, ES.Cubic)));
                    DelayBeat(4, () =>
                    {
                        AddInstance(new TimeRangedEvent(T(11), () =>
                        {
                            foreach (Line l in GetAll<Line>("ball")) { l.DrawingColor = C.Green; }
                        }));
                    });

                }
                if (InBeat(456))
                {
                    foreach (Line l in GetAll<Line>())
                    {
                        l.Dispose();

                    }
                    Resetball();
                }
                #endregion
                #region Rrhar'il Effect
                if (InBeat(439f))
                {
                    RegisterFunctionOnce("LF", () =>
                    {

                        Line ef = new(LinkEase(Stable(4, new V(-20, 240)), EaseIn(T(1), new V(340, 0), Cu), EaseOut(T(1), new V(100), Cu)), Stable(0, 90)) { TransverseMirror = true, Width = 5 };
                        CreateEntity(ef);
                        ef.DelayAlphaDecrease(T(1), T(1));
                        ef.DelayDispose(T(2));
                    });
                    RegisterFunctionOnce("LF2", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Line l = new(
                                LinkEase(
                                    Stable(i, new V(320, 500)), EaseOut(T(2), new V(0, -260), Cu),
                                    Stable(2 - i, V.Zero), EaseOut(T(1), new V(100 - i * 10, 0), Cu)),
                                LinkEase(Stable(i, 0), EaseOut(T(2), 135, Cu)))
                            { Alpha = 1 - i * 0.3f };
                            CreateEntity(l);
                            l.DelayAlphaDecrease(T(2), T(1));
                            Line l2 = new(
                                LinkEase(
                                    Stable(i, new V(320, 500)), EaseOut(T(2), new V(0, -260), Cu),
                                    Stable(2 - i, V.Zero), EaseOut(T(1), new V(-100 + i * 10, 0), Cu)),
                                LinkEase(Stable(i, 0), EaseOut(T(2), 135, Cu)))
                            { Alpha = 1 - i * 0.3f };
                            CreateEntity(l2);
                            l2.DelayAlphaDecrease(T(2), T(1));
                        }
                    });
                    RegisterFunctionOnce("LF3", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Line l = new(
                                LinkEase(
                                    Stable(i, new V(320, -20)), EaseOut(T(2), new V(0, 260), Cu),
                                    Stable(2 - i, V.Zero), EaseOut(T(1), new V(100 - i * 10, 0), Cu)),
                                LinkEase(Stable(i, 0), EaseOut(T(2), -135, Cu)))
                            { Alpha = 1 - i * 0.3f };
                            CreateEntity(l);
                            l.DelayAlphaDecrease(T(2), T(1));
                            Line l2 = new(
                                LinkEase(
                                    Stable(i, new V(320, -20)), EaseOut(T(2), new V(0, 260), Cu),
                                    Stable(2 - i, V.Zero), EaseOut(T(1), new V(-100 + i * 10, 0), Cu)),
                                LinkEase(Stable(i, 0), EaseOut(T(2), -135, Cu)))
                            { Alpha = 1 - i * 0.3f };
                            CreateEntity(l2);
                            l2.DelayAlphaDecrease(T(2), T(1));
                        }
                    });
                    RegisterFunctionOnce("LF4", () =>
                    {
                        Clock Clock1 = new(320, 240, 0, T(4), 1, 92, 12, C.Cyan) { Depth = 0.2f };
                        Clock Clock2 = new(320, 240, 100, T(4), 1, 70, 12, C.Orange) { Depth = 0.3f };
                        CreateEntity(new Clock[] { Clock1, Clock2 });
                        for (int A = 0; A < 3; A++)
                        {
                            float b = 6;
                            AddInstance(new TimeRangedEvent(T(2 * A), T(0.5f), () =>
                            {
                                Clock1.rotate += b * b * 0.2f;
                                Clock2.rotate += b * b * 0.07f;
                                b += 0.09f;
                            }));
                        }
                        float c = 0;
                        AddInstance(new TimeRangedEvent(T(4), T(4), () =>
                        {
                            Clock1.xCenter -= c * c;
                            Clock2.xCenter += c * c;
                            c += 0.03f;
                        }));
                        DelayBeat(8, () =>
                        {
                            Clock1.Dispose();
                            Clock2.Dispose();
                        });
                    });
                    RegisterFunctionOnce("LF5", () =>
                    {
                        Line l = new(LinkEase(Stable(0, new V(320, 500)), EaseIn(T(2), new V(0, -260), Cu), EaseOut(T(2), new V(0, -260), Cu)), Stable(0, 0)) { VerticalMirror = true, DrawingColor = C.Gold };
                        l.DelayAlphaDecrease(T(2), T(2));
                        CreateEntity(l);
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "","","","",  "LF2","","","",
                        "","","","",  "LF3","","","",
                        "","","","",  "LF2","","","",
                        "","","","",  "LF2(LF3)(LF5)","","","",
                        "LF","","","",  "LF4","","","",
                        "LF","","","",  "","","","",
                        "LF","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region Univarsal collapse Effect
                if (InBeat(488))
                {
                    Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\Wave"));
                    shaderProduction4 = ActivateShader(s, 0.11f);
                    s.Parameters["intensity"].SetValue(0.2f);
                    s.Parameters["flush"].SetValue(1.2f);
                    //s.Parameters["flash2"].SetValue(0.02f);
                    s.Parameters["count"].SetValue(12f);
                    AddInstance(new TimeRangedEvent(T(390), () =>
                    {
                        s.Parameters["time"].SetValue(Gametime / 20f);
                    }));
                    DelayBeat(12, () =>
                    {
                        RunEase((i) => { s.Parameters["flush"].SetValue(i); },
                            LinkEase(Stable(0, 1.2f), EaseOut(T(4), -1.2f, ES.Cubic)));
                        DelayBeat(4, () =>
                        {
                            shaderProduction4.Dispose();
                        });
                    });
                    RegisterFunctionOnce("LF1", () =>
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            BlockLine bl;
                            CreateEntity(bl = new BlockLine(new V(-100), 135, 50, 50) { mirror = true });
                            float time = 0;
                            AddInstance(new TimeRangedEvent(i * 3, T(10), () =>
                            {
                                bl.Centre = new V(-100 + time, -100 + time) + new V(Sin(time) * 100, 0);
                                bl.rot = (-Cos(time) * 90) < -22.5f ? -22.5f : -Cos(time) * 90;
                                time += 4;
                            }));
                            bl.DelayDispose(T(10) + i);
                        }
                    });
                    RegisterFunctionOnce("LF2", () =>
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            BlockLine bl = new(new V(-100, 580), 45, 50, 50) { mirror = true };
                            CreateEntity(bl);
                            float time = 0;
                            AddInstance(new TimeRangedEvent(i * 3, T(10), () =>
                            {
                                bl.Centre = new V(-100 + time + Sin(time) * 100, 580 - time);
                                bl.rot = (Cos(time) * 90 > 22.5f) ? 22.5f : Cos(time) * 90;
                                time += 5;
                            }));
                            bl.DelayDispose(T(10) + i);
                        }
                    });
                    RegisterFunctionOnce("LF4", () =>
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            float rand = Rand(50, 200), rand2 = Rand(0, 360);
                            for (int temp = 0; temp < 5; temp++)
                            {
                                EfEn ef = new(WCentre, Sprites.lightBall)
                                { Size = new V(1f + temp * 0.1f), Color = C.Lerp(C.White, C.Red, temp * 0.2f), Depth = 0.2f - temp * 0.01f };
                                ef.vecEase(false, WCentre, WCentre + GV(rand, rand2), T(2), Cu);
                                CreateEntity(ef);
                                ef.AutoDis(T(2));

                            }
                        }
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "LF1","","","",  "","","","",
                        "","","","",  "","","","",
                        "LF2","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",  "","","","",
                        "","","","",   "","","","",
                        "","","","",  "","","","",
                        "LF4",
                    });
                }
                #endregion
                #region Dreadnaught Effect
                if (InBeat(504))
                {
                    Shader sh = new(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\ball"));
                    sh.Parameters["intensity"].SetValue(1);
                    sh.Parameters["intensity2"].SetValue(2);
                    sh.Parameters["icolor"].SetValue(new C(105, 0, 0).ToVector3());
                    shaderProduction3 = null;
                    shaderProduction3 = ActivateShader(sh, 0.9635235f);
                    RunEase((s) => { sh.Parameters["iy"].SetValue(s); },
                        LinkEase(Stable(0, -240), EaseOut(T(4f), 480, Cu)));
                    DelayBeat(8, () => { shaderProduction3.Dispose(); });
                }
                if (InBeat(504, 511.9f))
                {
                    EfEn ef = new(new V(-100), Sprites.square) { Size = new V(0.3f, Rand(0.3f, 0.7f)), Color = C.Red };
                    CreateEntity(ef);
                    ef.AutoDis(T(4));
                    ef.vec2Ease(true, new V(Rand(0, 640f), -20), new V(0, Rand(100f, 400f)), T(4), Li);
                    ef.AlphaEase(true, 1, 0, T(2), T(2), Li);
                    DelayBeat(0, () =>
                    {
                        EfEn ef = new(new V(-100), Sprites.square) { Size = new V(0.3f, Rand(0.3f, 0.7f)), Color = C.Red };
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                        ef.vec2Ease(true, new V(Rand(0, 640f), 500), new V(0, -Rand(100f, 400f)), T(4), Li);
                        ef.AlphaEase(true, 1, 0, T(2), T(2), Li);
                    });
                }
                if (InBeat(512))
                {
                    foreach (EfEn ef in GetAll<EfEn>())
                    {
                        ef.Dispose();
                    }
                }
                if (InBeat(512))
                {
                    Winder s = new()
                    {
                        Intensity = 3,
                        BasicSpeed = 1.6f,
                        Width = 2.5f,
                        DrawingColor = Color.Lerp(Color.Aqua, Color.White, 0.7f),
                        Direction = true
                    };
                    CreateEntity(s);
                    DelayBeat(8, () =>
                    {
                        s.Dispose();
                    });
                }
                #endregion
                #region Eternal spring dream Effect
                if (InBeat(520, 548f) && Gametime % 5 == 0)
                {
                    Texture2D ball = bubble;
                    EfEn ef = new(new V(Rand(-50f, 690f), 800), ball) { Size = new V(Rand(0.3f, 0.7f)),Alpha=0.6f ,Depth=0f};
                    ef.speed = new V(0, -Rand(3f, 7f));
                    CreateEntity(ef);
                    ef.AutoDis(T(10));
                }
                if (InBeat(548))
                {
                    foreach (EfEn ef in GetAll<EfEn>())
                    {
                        ef.stop = true;
                    }
                }
                if (InBeat(552))
                {
                    foreach (EfEn ef in GetAll<EfEn>())
                    {
                        if (ef.stop)
                        {
                            ef.Dispose();
                        }
                    }
                }
                #endregion
            }
            public void TextEffect()
            {

                #region 流れてく時の中ででも
                if (InBeat(136))
                {

                    RegisterFunctionOnce("Ef1", () =>
                    {
                        string text = "流れてく";
                        char[] chs = text.ToCharArray();
                        int e = 0;
                        float vec = 320 - 80 * 1.5f;
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {

                                Ef Tex = new Ef($"{chs[e]}", new V(), Color.White, 1.3f) { edging = true };
                                Tex.rotOut(0, 15, T(1), ES.Cubic);
                                Tex.rotIn(15, 0, T(1), T(1), ES.Cubic);
                                CreateEntity(Tex);
                                Tex.vecOut(new(vec, -150), new(vec, 150), 0, T(2), ES.Cubic);
                                Tex.vecIn(new(vec, 150), new(vec, -150), T(2), T(2), ES.Cubic);
                                Tex.alphaOut(0, 1, T(2), ES.Cubic);
                                Tex.alphaIn(1, 0, T(2), T(2), ES.Cubic);

                                vec += 80;
                                e++;
                                Tex.AutoDis(T(8));
                            });
                        }

                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {

                        Ef Tex = new Ef("時\nの", V.Zero, Color.Red, 2) { edging = true };
                        Tex.vecOut(new V(320f, 580), new V(320f, 240f), T(1), ES.Cubic);
                        Tex.vecIn(new V(320f, 240f), new V(320f, -100f), T(1), T(1), ES.Cubic);
                        Tex.AutoDis(T(4));
                        CreateEntity(Tex);
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef e = new Ef("中", new V(), Color.White, 1) { edging = true };
                        e.vecIn(new V(700, -60), new V(500, 350), T(2), ES.Quad);
                        e.vecOut(new V(500, 350), new V(320, 240), T(2), T(1), ES.Elastic);

                        e.rotOut(0, 3 * T(2), T(2), ES.Linear);
                        e.rotOut(T(2) * 3, 0, T(2), T(2), ES.Elastic);
                        e.alphaOut(1, 0, T(8), T(1), ES.Quad);
                        e.sizeIn(1, 0.5f, T(2), ES.Cubic);
                        e.sizeOut(0.5f, 3f, T(2), T(2f), ES.Cubic);
                        e.sizeOut(3f, 0, T(4f), T(0.5f), ES.Cubic);
                        CreateEntity(e);
                        e.AutoDis(T(16));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        int e = 0;
                        string text = "で";

                        Ef Tex = new Ef(text, new V(320, 240), Color.White, 1.4f) { edging = true };
                        CreateEntity(Tex);
                        Tex.vecOut(new V(320, 500), new V(320, 360), T(1), ES.Cubic);
                        Tex.vecIn(new V(320, 360), new V(320, 500), T(1), T(1), ES.Cubic);
                        Tex.rotOut(0, 360, T(4), ES.Cubic);
                        e++;
                        Tex.AutoDis(T(8));

                    });
                    RegisterFunctionOnce("Ef5", () =>
                    {

                        Ef t = new Ef("で", new(), Color.White, 1.4f) { edging = true };
                        t.vecIn(new V(240, -50), new V(240, 600), T(4), ES.Cubic);
                        t.rotIn(0, 360, T(4), ES.Cubic);
                        CreateEntity(t);
                        DelayBeat(0.5f, () =>
                        {
                            Ef t2 = new Ef("も", new(), Color.White, 1.4f) { edging = true };
                            t2.vecIn(new V(400, -50), new V(400, 600), T(4), ES.Cubic);
                            t2.rotIn(0, -360, T(4), ES.Cubic);
                            CreateEntity(t2);
                            t2.AutoDis(T(4));
                        });
                        t.AutoDis(T(4));

                    });
                    RegisterFunctionOnce("s", () =>
                    {
                        PlaySound(Sounds.change);

                    });
                    CreateChart(0, T(2), 5, new string[]
                    {
                        "Ef1","","","", "","","","",
                        "","","","", "Ef2","","","",
                        "Ef3","","","", "","","Ef5","",
                        "","","Ef4","", "","","","",
                    });
                }
                #endregion
                #region 気だるさが、ほらグルグル廻って
                if (InBeat(144))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        string text = "気だるさが";
                        char[] cha = text.ToCharArray();
                        float x = 150;
                        for (int i = 0; i < cha.Length; i++)
                        {
                            Ef t1 = new Ef($"{cha[i]}", V.Zero, CW, 1.2f) { edging = true };
                            t1.vecOut(new V(320, 240), new V(320 - x, 240), T(2), ES.Expo);
                            t1.vecIn(new V(320 - x, 240), new V(320, 240), T(2), T(2), ES.Expo);
                            CreateEntity(t1);
                            x -= 75;
                            t1.AutoDis(T(4));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {

                        Ef t1 = new Ef("ほ", new V(100, 320), CW, 3) { edging = true };
                        t1.vecIn(new V(100, 320), new V(-100, 320), T(1), T(2), ES.Cubic);
                        CreateEntity(t1);

                        DelayBeat(0.5f, () =>
                        {
                            Ef t2 = new Ef("ら", new V(540, 160), CW, 3) { edging = true };
                            t2.vecIn(new V(540, 160), new V(740, 160), T(0.5f), T(2), ES.Cubic);
                            CreateEntity(t2);
                        });

                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {

                        string text = "グルグル廻って";
                        char[] cha = text.ToCharArray();
                        float x = 75 * 3;
                        for (int i = 0; i < cha.Length; i++)
                        {

                            Ef t1 = new Ef($"{cha[i]}", V.Zero, CW, 1.2f) { edging = true };
                            t1.scf = true;
                            t1.vecOut(new V(320, 240), new V(320 - x, 240), T(2), ES.Expo);
                            t1.vecIn(new V(320 - x, 240), new V(320 - x, 240 + Sig(i % 2) * 300), T(2), T(2), ES.Cubic);
                            CreateEntity(t1);
                            x -= 75;
                            t1.AutoDis(T(6));
                        }

                    });
                    CreateChart(0, T(2), 5, new string[]
                    {
                    "Ef1","","","",  "","","","",
                    "","","","",  "Ef2","","","",
                    "Ef3","","","",  "","","","",
                    "","","","",  "","","","",
                    });
                }
                #endregion
                #region 私から　離れる心も
                if (InBeat(152))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {

                        Ef text = new Ef("私", new V(320, 240), CW, 0) { edging = true };
                        text.sizeOut(0, 3, T(0.75f), Cu);
                        text.sizeOut(3, 0, T(0.75f), T(0.75f), Cu);
                        text.rotOut(-250, 15, T(0.75f), Cu);
                        text.rotOut(15, -250, T(0.75f), T(0.75f), Cu);
                        CreateEntity(text);
                        text.scf = true;
                        text.AutoDis(T(1.5f));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef text1 = new Ef("か", new V(320, 240), CW, 0) { edging = true };
                        text1.vecOut(new V(320, 240), new V(240), T(1), Cu);
                        text1.vecIn(new V(240), new V(240, -100), T(1.5f), T(2), Ba);
                        text1.sizeOut(0, 1.5f, T(2), Cu);
                        text1.alphaIn(1, 0, T(1.5f), T(2), Cu);
                        text1.AutoDis(T(3.5f));
                        text1.rotOut(-25, 0, T(1), Ex);
                        text1.scf = true;
                        CreateEntity(text1);
                        DelayBeat(0.5f, () =>
                        {

                            Ef text1 = new Ef("ら", new V(320, 240), CW, 0) { edging = true };
                            text1.vecOut(new V(320, 240), new V(400, 240), T(1), Cu);
                            text1.vecIn(new V(400, 240), new V(400, 740), T(1), T(2), Ba);
                            text1.sizeOut(0, 1.5f, T(2), Cu);
                            text1.alphaIn(1, 0, T(1), T(2), Cu);
                            text1.AutoDis(T(3));
                            text1.rotOut(25, 0, T(1), Ex);
                            text1.scf = true;
                            CreateEntity(text1);
                        });
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef text1 = new("離", new V(), CW, 2) { edging = true };
                        text1.vecOut(new V(320, 240) + GV(Rand(100, 300), Rand(0, 360)), new V(320, 160), T(2), Cu);
                        text1.alphaOut(0, 1, T(2), Cu);
                        CreateEntity(text1);
                        text1.vecOut(new V(320, 160), new V(320, 240) + GV(Rand(200, 400), Rand(0, 360)), T(3), T(4), Cu);
                        text1.alphaOut(1, 0, T(3), T(4), Cu);
                        text1.rotOut(0, Rand(-360, 360), T(3), T(4), Cu);
                        text1.scf = true;
                        text1.AutoDis(T(7));
                        DelayBeat(1, () =>
                        {

                            Ef text1 = new("れ", new V(), CW, 2) { edging = true };
                            text1.vecOut(new V(320, 240) + GV(Rand(100, 300), Rand(0, 360)), new V(320, 240), T(2), Cu);
                            text1.alphaOut(0, 1, T(2), Cu);
                            CreateEntity(text1);
                            text1.vecOut(new V(320, 240), new V(320, 240) + GV(Rand(200, 400), Rand(0, 360)), T(2), T(4), Cu);
                            text1.alphaOut(1, 0, T(2), T(4), Cu);
                            text1.rotOut(0, Rand(-360, 360), T(2), T(4), Cu);
                            text1.scf = true;
                            text1.AutoDis(T(7));
                        });
                        DelayBeat(2, () =>
                        {

                            Ef text1 = new("る", new V(), CW, 2) { edging = true };
                            text1.vecOut(new V(320, 240) + GV(Rand(100, 300), Rand(0, 360)), new V(320, 320), T(1), Cu);
                            text1.alphaOut(0, 1, T(2), Cu);
                            CreateEntity(text1);
                            text1.vecOut(new V(320, 320), new V(320, 240) + GV(Rand(200, 400), Rand(0, 360)), T(1), T(4), Cu);
                            text1.alphaOut(1, 0, T(1), T(4), Cu);
                            text1.rotOut(0, Rand(-360, 360), T(1), T(4), Cu);
                            text1.scf = true;
                            text1.AutoDis(T(7));
                        });
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {

                        Ef text = new Ef("心", new V(320, 240), CW, 0) { edging = true };
                        text.sizeOut(new V(4, 0), new V(4), T(0.75f), Cu);
                        text.sizeOut(new V(4), new V(0, 4), T(0.75f), T(0.75f), Cu);
                        CreateEntity(text);
                        text.AutoDis(T(3));
                        text.scf = true;
                    });
                    RegisterFunctionOnce("Ef5", () =>
                    {

                        Ef text = new Ef("も", new V(335, 240), CW, 0) { edging = true };
                        text.sizeOut(new V(0, 4), new V(4), T(0.75f), Cu);
                        text.sizeOut(new V(4), new V(4, 0), T(1), T(1), Cu);
                        CreateEntity(text);
                        text.AutoDis(T(3));
                        text.scf = true;

                    });
                    CreateChart(0, T(2), 4, new string[]
                    {
                    "Ef1","","","",  "Ef2","","","",
                    "","","","",  "Ef3","","","",
                    "","","","",  "","","","",
                    "Ef4","","","",  "","","Ef5","",
                    });

                }
                #endregion
                #region 見えないわ　そう知らない?
                if (InBeat(160))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        string t = "見えないわ";
                        char[] c = t.ToCharArray();
                        float e = 320 - 150;
                        int es = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {

                                Ef text = new Ef($"{c[es]}", new V(e, 400), C.Red, 1) { edging = true };
                                text.alphaOut(0, 1, T(2), Cu);
                                text.alphaOut(1, 0, T(2), T(2), Cu);
                                CreateEntity(text);
                                Ef text2 = new Ef($"{c[es]}", new V(e - 10, 400 + 10), C.DarkRed * 0.5f, 1.2f, 0.5f) { edging = true };
                                text2.alphaOut(0, 1, T(2), Cu);
                                text2.alphaOut(1, 0, T(2), T(2), Cu);
                                CreateEntity(text2);
                                e += 75;
                                es++;
                                text.AutoDis(T(10));
                                text2.AutoDis(T(10));
                                text.scf = true;
                                text2.scf = true;
                            });
                        }

                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new Ef("そ\nう", WCentre, CW, 3) { edging = true };
                        CreateEntity(ef);
                        ef.AutoDis(T(1));
                        ef.scf = true;
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        char[] c = "知らない?".ToCharArray();
                        int es = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i * 1, () =>
                            {
                                float x = 170 + es * 100;
                                Ef e = new Ef($"{c[es]}", new V(x, 240), CW, 2) { edging = true,scf=true };
                                CreateEntity(e);
                                Ef e2 = new Ef($"{c[es]}", new V(x, 240), CW, 2) { edging = true,scf=true };
                                CreateEntity(e2);
                                e2.alphaOut(1, 0, T(2), Cu);
                                e2.sizeOut(2, 4, T(2), Cu);
                                es++;
                                e.AutoDis(T(6));
                                e2.AutoDis(T(6));
                            });
                        }

                    });
                    CreateChart(0, T(2), 3, new string[]
                    {

                        "Ef1","","","",  "","","","",
                        "","","","",  "Ef2","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",

                    });

                }
                #endregion
                #region 自分から、動くこともなく 時の隙間に流され続けて
                if (InBeat(168))
                {
                    Ef ef = new("自分から", new V(320, 240), CW, 2) { edging = true };
                    ef.vecOut(new V(-200, 50), new V(320, 50), T(2), Cu);
                    ef.vecIn(new V(320, 50), new V(840, 50), T(2), T(2), Cu);
                    CreateEntity(ef);
                    ef.AutoDis(T(4));
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("動", new V(150, 150), CW, 2) { edging = true };
                        Ef ef2 = new("動", new V(150, 150), CW, 2) { edging = true };
                        CreateEntity(ef);
                        CreateEntity(ef2);
                        ef.AutoDis(T(4));
                        ef2.AutoDis(T(2));
                        ef2.sizeOut(2, 4, T(2), Cu);
                        ef2.alphaOut(1, 0, T(2), Cu);
                        ef.rot = 30;
                        ef2.rot = 30;
                        ef.vecIn(new V(150, 150), new V(150, 150 + 500), T(2), T(2), Cu);
                        ef.rotIn(30, 30 - 360, T(2), T(2), Cu);
                        DelayBeat(1, () =>
                        {
                            Ef ef = new("く", new V(490, 320), CW, 2) { edging = true };
                            Ef ef2 = new("く", new V(490, 320), CW, 2) { edging = true };
                            CreateEntity(ef);
                            CreateEntity(ef2);
                            ef.AutoDis(T(4));
                            ef2.AutoDis(T(2));
                            ef2.sizeOut(2, 4, T(2), Cu);
                            ef2.alphaOut(1, 0, T(2), Cu);
                            ef.rot = -30;
                            ef2.rot = -30;
                            ef.vecIn(new V(490, 320), new V(490, 320 + 500), T(1), T(2), Cu);
                            ef.rotIn(-30, -30 + 360, T(1), T(2), Cu);
                        });
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("ことも無く", CW, 2) { edging = true };
                        ef.vecOut(new V(840, 440), new V(320, 440), T(2), Cu);
                        ef.vecIn(new V(320, 440), new V(-200, 440), T(2), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("時の隙間に", new V(320, 100), CW, 0) { edging = true };
                        CreateEntity(ef);
                        ef.sizeOut(new(0, 2), new V(2, 2), T(2), Cu);
                        ef.sizeIn(new V(2), new(0, 2), T(2), T(2), Cu);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("流され続けて", new V(320, 100), CW, 0) { edging = true };
                        CreateEntity(ef);
                        ef.sizeOut(new(0, 2), new V(2), T(4), Cu);
                        ef.vecOut(new V(320, 100), new(320, -200), T(4), T(2), Cu);
                        ef.AutoDis(T(6));
                    });
                    CreateChart(T(3), T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "Ef3","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef4",
                    });
                }
                #endregion
                #region 知らないわ、周りのことなど
                if (InBeat(183))
                {

                    RegisterFunctionOnce("Ef1", () =>
                    {
                        //UIS();
                        for (int i = 0; i < 5; i++)
                        {
                            V vec = new((320 - 95 * 2) + 95 * i, 50 + 95 * i);
                            string st = "知らないわ";
                            char[] c = st.ToCharArray();
                            Ef ef = new($"{c[i]}", new V(-200), C.Red, 2) { edging = true };
                            ef.scf = true;
                            ef.vecOut(vec + new V(0, -500), vec, T(i * 0.5f), T(2), Cu);
                            ef.vecIn(vec, vec + new V(600, 0), T(i * 0.5f + 2), T(2), Cu);
                            ef.rotOut(180, 0, T(i * 0.5f), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(16));
                        }

                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        char[] st = "周りのことなど".ToCharArray();
                        int s = 0;
                        for (int i = 0; i < 7; i++)
                        {

                            Ef ef = new($"{st[s]}", new V(320 + T(1) * 20, 440), CW, 2) { edging = true };
                            s++;
                            ef.scf = true;
                            CreateEntity(ef);
                            ef.alphaOut(0, 1, T(2), Cu);
                            float et = 360f / 7f * i;
                            AddInstance(new TimeRangedEvent(T(3.5f), () =>
                            {
                                ef.centre = new V(320 + Sin(et) * 200, 240 + Cos(et) * 200);
                                ef.rot = -et;
                                et -= 360 / T(4);
                            }));
                            float num = 320 + -75 * 3 + i * 75;
                            AddInstance(new InstantEvent(T(3), () =>
                            {
                                ef.vecOut(ef.centre, new V(num, 190), T(2), El);
                                ef.rotOut(ef.rot, 0, T(2), El);
                            }));
                            DelayBeat(4 + i * 0.25f, () =>
                            {
                                ef.alphaOut(1, 0, T(2), Cu);
                                ef.AutoDis(T(2));
                            });
                        }
                    });
                    CreateChart(0, T(2), 4, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region 私は私それだけ
                if (InBeat(192))
                {
                    Ef ef1 = new Ef("私", new V(0, 0), CW, 4) { edging = true };
                    ef1.vecOut(new V(150, -100), new V(150, 230), T(2), Cu);
                    ef1.vecOut(new V(150, 230), new V(150, 250), T(2), T(2), Li);
                    ef1.vecOut(new V(150, 250), new V(150, 580), T(4), T(2), Cu);
                    ef1.scf = true;
                    CreateEntity(ef1);
                    Ef ef2 = new Ef("私", new V(0, 0), CW, 4) { edging = true };
                    ef2.rot = 180;
                    ef2.vecOut(new V(490, 580), new V(490, 250), T(2), Cu);
                    ef2.vecOut(new V(490, 250), new V(490, 230), T(2), T(2), Li);
                    ef2.vecOut(new V(490, 230), new V(490, -100), T(4), T(2), Cu);
                    ef2.scf = true;
                    ef1.colOut(CW, C.DarkRed, T(2), T(2), Cu);
                    ef2.colOut(CW, C.DarkRed, T(2), T(2), Cu);
                    CreateEntity(ef2);
                    DelayBeat(4, () =>
                    {
                        int set = 0;
                        char[] c = "それだけ".ToCharArray();
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i, () =>
                            {

                                Ef ef = new($"{c[set]}", new V(320, 75 * 1.5f + 240 - (75 * set)), CW, 2) { edging = true };
                                Ef ef2 = new($"{c[set]}", new V(320, 75 * 1.5f + 240 - (75 * set)), CW, 2) { edging = true };
                                ef.rot = 180;
                                ef2.rot = 180;
                                ef2.sizeOut(2, 4, T(2), Cu);
                                ef2.alphaOut(1, 0, T(2), Cu);
                                CreateEntity(ef);
                                CreateEntity(ef2);
                                ef.AutoDis(T(4 - set));
                                ef2.AutoDis(T(2));
                                set++;
                            });
                        }

                    });
                }
                #endregion
                #region 夢見てる？なにも見てない？語るも無駄な、自分の言葉？
                if (InBeat(198.5f))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("夢", new V(100, 180), CW, 1);
                        ef.TSEf();
                        CreateEntity(ef);
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                char[] c = "見てる?".ToCharArray();
                                Ef ef = new($"{c[s]}", new V(140 + 40 * s, 180), CW, 1);
                                ef.TSEf();
                                CreateEntity(ef);
                                s++;
                            });
                        }

                    });

                    RegisterFunctionOnce("Ef4", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                char[] c = "なにも見てない?".ToCharArray();
                                Ef ef = new($"{c[s]}", new V(350 + 40 * s, 320), CW, 1);
                                ef.TSEf();
                                CreateEntity(ef);
                                s++;
                            });
                        }

                    });
                    RegisterFunctionOnce("Ef5", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                char[] c = "語るも無駄な".ToCharArray();
                                Ef ef = new($"{c[s]}", new V(100 + 40 * s, 320), CW, 1);
                                ef.TSEf();
                                if (s == 3 || s == 4)
                                {
                                    ef.Color = C.Red;
                                }
                                CreateEntity(ef);
                                s++;
                            });
                        }
                    });
                    RegisterFunctionOnce("Ef6", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            char[] c = "自分の言葉?".ToCharArray();
                            Ef ef = new($"{c[i]}", new V(400 + 40 * i, 120), CW, 1);
                            if (i == 2 || i == 4)
                            {
                                ef.TSEf(T(s * 0.5f + 0.5f));
                                CreateEntity(ef);
                                s++;
                            }
                            else
                            {
                                ef.TSEf(T(s * 0.5f));
                                CreateEntity(ef);
                            }
                            s++;
                        }
                    });

                    CreateChart(0, T(2), 7, new string[]
                    {
                    "Ef1","","","", "Ef2","","","",
                    "","","","", "","","","",
                    "Ef4","","","", "","","","",
                    "","","","", "","","","",
                    "Ef5","","","", "","","","",
                    "","","","",  "","","","",
                    "Ef6","","","",  "","","","",
                    "","","","",
                    });
                }
                #endregion
                #region 悲しむなんて、疲れるだけよ、何も感じず、過ごせばいいの
                if (InBeat(214.5f))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            char[] c = "悲しむなんて".ToCharArray();
                            Ef ef = new($"{c[i]}", new V(100 + 40 * i, 100), CW, 1);
                            CreateEntity(ef);
                            ef.Alpha = 0;
                            if (s == 1)
                            {
                                s++;
                            }
                            if (i < 3)
                            {
                                ef.Color = new C(100, 200, 255);
                            }
                            ef.TSEf(T(s * 0.5f));
                            s++;
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            char[] c = "疲れるだけよ".ToCharArray();
                            Ef ef = new($"{c[i]}", new V(400 + 40 * i, 200), C.Red, 1);
                            CreateEntity(ef);
                            ef.Alpha = 0;
                            if (s == 1)
                            {
                                s++;
                            }

                            ef.TSEf(T(s * 0.5f));
                            s++;
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            char[] c = "何も感じず".ToCharArray();
                            Ef ef = new($"{c[i]}", new V(240 + 40 * i, 340), C.Gray, 1);
                            CreateEntity(ef);
                            ef.Alpha = 0;
                            if (s == 1 || s == 3)
                            {
                                s++;
                            }

                            ef.TSEf(T(s * 0.5f));

                            s++;
                        }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        int s = 0;
                        for (int i = 0; i < 7; i++)
                        {
                            char[] c = "過ごせばいいの".ToCharArray();
                            Ef ef = new($"{c[i]}", new V(200 + 40 * i, 100), C.DarkRed, 1);
                            CreateEntity(ef);
                            ef.Alpha = 0;

                            ef.TSEf(T(s * 0.5f));

                            s++;
                        }
                    });
                    CreateChart(0, T(2), 6, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","", "","","","",
                        "","","","", "","","","",
                        "Ef3","","","", "","","","",
                        "","","","", "","","","",
                        "Ef4",
                    });
                }
                #endregion
                #region 戸惑う言葉、與えられても、自分の心、ただ上の空
                if (InBeat(231))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef[] e = GetAll<Ef>();
                        foreach (Ef es in e)
                        {
                            es.Dispose();
                        }
                        Ef ef = new("戸惑う言葉", CW, 2) { edging = true };
                        ef.vecOut(new V(0, -200), new V(320, 240), T(4), Cu);
                        ef.vecIn(new V(320, 240), new V(0, 680), T(4), T(4), Cu);
                        ef.rotOut(-90, 0, T(4), Cu);
                        ef.rotIn(0, 90, T(4), T(4), Cu);
                        ef.AutoDis(T(8));
                        ef.scf = true;
                        CreateEntity(ef);
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("与えられても", CW, 2) { edging = true };
                        ef.vecOut(new V(640, -200), new V(320, 240), T(4), Cu);
                        ef.vecIn(new V(320, 240), new V(640, 680), T(4), T(4), Cu);
                        ef.rotOut(90, 0, T(4), Cu);
                        ef.rotIn(0, -90, T(4), T(4), Cu);
                        ef.AutoDis(T(8));
                        //ef.scf = true;
                        CreateEntity(ef);
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("自分の心", CW, 2) { edging = true };
                        ef.vecOut(new V(0, -200), new V(320, 240), T(4), Cu);
                        ef.vecIn(new V(320, 240), new V(0, 680), T(4), T(4), Cu);
                        ef.rotOut(-90, 0, T(4), Cu);
                        ef.rotIn(0, 90, T(4), T(4), Cu);
                        ef.AutoDis(T(8));
                        //ef.scf = true;
                        CreateEntity(ef);
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("ただ上の空", CW, 2) { edging = true };
                        ef.colOut(CW, C.DarkRed, T(4), Cu);
                        ef.vecOut(new V(0, -200), new V(320, 240), T(4), Cu);
                        ef.vecIn(new V(320, 240), new V(0, 680), T(4), T(4), Cu);
                        ef.rotOut(-90, 0, T(4), Cu);
                        ef.rotIn(0, 90, T(4), T(4), Cu);
                        ef.AutoDis(T(4));
                        CreateEntity(ef);
                        DelayBeat(0, () =>
                        {
                            Ef ef = new("ただ上の空", CW, 2) { edging = true };
                            ef.colOut(CW, C.DarkRed, T(4), Cu);
                            ef.vecOut(new V(640, -200), new V(320, 240), T(4), Cu);
                            ef.vecIn(new V(320, 240), new V(640, 680), T(4), T(4), Cu);
                            ef.rotOut(90, 0, T(4), Cu);
                            ef.rotIn(0, -90, T(4), T(4), Cu);
                            ef.AutoDis(T(4));
                            CreateEntity(ef);
                        });
                    });
                    CreateChart(0, T(2), 5, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","", "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","", "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","", "","","","",
                        "Ef4",
                    });
                }
                #endregion
                #region もし私から　動くのならば　すべて変えるのなら　黒にする
                if (InBeat(248))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("も\nし\n私\nか\nら", CW, 1.5f) { edging = true };
                        ef.rot = 180;
                        Ef ef2 = new("も\nし\n私\nか\nら", CW, 1.5f) { edging = true };
                        ef.vecOut(new V(440, -300), new V(440, 240), T(2), Cu);
                        ef.sizeIn(new V(1.5f), new V(0, 1.5f), T(2), T(2), Cu);
                        ef2.vecOut(new V(200, 780), new V(200, 240), T(2), Cu);
                        ef2.sizeIn(new V(1.5f), new V(0, 1.5f), T(2), T(2), Cu);
                        CreateEntity(ef);
                        CreateEntity(ef2);
                        ef.AutoDis(T(4));
                        ef2.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("動\nく\nの\nな\nら\nば", new V(440, 240), CW, 1.5f) { edging = true };
                        Ef ef2 = new("動\nく\nの\nな\nら\nば", new V(200, 240), CW, 1.5f) { edging = true };
                        ef2.rot = 180;
                        ef.sizeOut(new V(0, 1.5f), new V(1.5f), T(2), Cu);
                        ef.vecIn(new V(440, 240), new V(440, 780), T(2), T(2), Cu);
                        ef2.sizeOut(new V(0, 1.5f), new V(1.5f), T(2), Cu);
                        ef2.vecIn(new V(200, 240), new V(200, -300), T(2), T(2), Cu);
                        CreateEntity(ef);
                        CreateEntity(ef2);
                        ef.AutoDis(T(4));
                        ef2.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("すべて変えるのなら", new V(320, 240), CW, 2) { Alpha = 0, edging = true };
                        ef.scf = true;
                        ef.rotOut(0, 720, T(4), Cu);
                        ef.rotIn(720, 0, T(4), T(4), Cu);
                        ef.sizeOut(V.Zero, new V(2), T(4), Cu);
                        ef.sizeIn(new V(2), V.Zero, T(4), T(1), Cu);
                        ef.Alpha = 1;
                        CreateEntity(ef);
                        ef.AutoDis(T(5));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {

                        Ef ef = new("黒にする", new V(320, 240), CW, 2) { Alpha = 0, edging = true };
                        ef.scf = true;

                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaOut(1, 0, T(2), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "Ef3","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4",
                    });
                }
                #endregion
                #region こんな自分に、未来はあるの?こんな世界に、私はいるの?
                if (InBeat(263.5f))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("こんな自分に", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("未来はあるの?", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("こんな世界に", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("私はいるの?", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region 今切ないの、今悲しいの、自分のことも、わからないまま
                if (InBeat(279.5f))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("今切ないの", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("今悲しいの", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("自分のことも", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("わからないまま", CW, 1);
                        ef.vecOut(WCentre + GV(300, Rand(0, 360)), WCentre, T(8), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region 歩むことさえ、疲れるだけよ、人のことなど、知りもしないわ
                if (InBeat(295f))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("歩\nむ\nこ\nと\nさ\nえ", CW, 1);
                        CreateEntity(ef);
                        ef.alphaOut(0, 1, T(4), Cu);
                        ef.alphaOut(1, 0, T(4), T(4), Cu);
                        ef.vecOut(new V(640, 240), new V(440, 240), T(4), Cu);
                        ef.vecIn(new V(440, 240), new V(640, 240), T(4), T(4), Cu);
                        ef.AutoDis(T(8));

                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("疲\nれ\nる\nだ\nけ\nよ", CW, 1);
                        CreateEntity(ef);
                        ef.alphaOut(0, 1, T(4), Cu);
                        ef.alphaOut(1, 0, T(4), T(4), Cu);
                        ef.vecOut(new V(0, 240), new V(200, 240), T(4), Cu);
                        ef.vecIn(new V(200, 240), new V(0, 240), T(4), T(4), Cu);
                        ef.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("人\nの\nこ\nと\nな\nど", CW, 1);
                        CreateEntity(ef);
                        ef.alphaOut(0, 1, T(4), Cu);
                        ef.alphaOut(1, 0, T(4), T(4), Cu);
                        ef.vecOut(new V(640, 240), new V(440, 240), T(4), Cu);
                        ef.vecIn(new V(440, 240), new V(640, 240), T(4), T(4), Cu);
                        ef.AutoDis(T(8));

                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("知\nり\nも\nし\nな\nい\nわ", CW, 1);
                        CreateEntity(ef);
                        ef.alphaOut(0, 1, T(4), Cu);
                        ef.alphaOut(1, 0, T(4), T(4), Cu);
                        ef.vecOut(new V(0, 240), new V(200, 240), T(4), Cu);
                        ef.vecIn(new V(200, 240), new V(0, 240), T(4), T(4), Cu);
                        ef.AutoDis(T(8));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region こんな私も、変われるのなら、もし変われるのなら、白になる
                if (InBeat(311))
                {

                    float sin = 0;
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        sin += 0.1f;
                    }));
                    RegisterFunctionOnce("Ef1", () =>
                    {

                        Ef ef = new("こんな私も", CW, 1.2f);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaIn(1, 0, T(2), T(2), Cu);
                        AddInstance(new TimeRangedEvent(T(16), () =>
                        {
                            ef.centre = new V(320 + Sin(sin * 3) * 20, 240 + Sin(sin * 7) * 20);
                        }));
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("変われるのなら", CW, 1.2f);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaIn(1, 0, T(2), T(2), Cu);
                        CreateEntity(ef);
                        AddInstance(new TimeRangedEvent(T(16), () =>
                        {
                            ef.centre = new V(320 + Sin(sin * 3) * 20, 240 + Sin(sin * 7) * 20);
                        }));
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("もし変われるのなら", CW, 1.2f);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaIn(1, 0, T(2), T(2), Cu);
                        CreateEntity(ef);
                        AddInstance(new TimeRangedEvent(T(16), () =>
                        {
                            ef.centre = new V(320 + Sin(sin * 3) * 20, 240 + Sin(sin * 7) * 20);
                        }));
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("白になる", CW, 1.2f) { scf = true };
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaIn(1, 0, T(2), T(2), Cu);
                        CreateEntity(ef);
                        Ef ef2 = new("白になる", CW, 1.2f) { scf2 = true };
                        ef2.alphaOut(0, 1, T(2), Cu);
                        ef2.alphaIn(1, 0, T(2), T(2), Cu);
                        CreateEntity(ef2);
                        AddInstance(new TimeRangedEvent(T(16), () =>
                        {
                            ef.centre = new V(320 + Sin(sin * 3) * 20, 240 + Sin(sin * 7) * 20);
                            ef2.centre = new V(320 + Sin(sin * 3) * 20, 240 + Sin(sin * 7) * 20);
                        }));
                        ef.AutoDis(T(4));
                        ef2.AutoDis(T(4));
                        DelayBeat(4, () =>
                        {
                            RemoveUI1();
                            RemoveUI2();
                        });
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef4","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region 流れてく、時の中ででも、気だるさがほら、グルグル廻って
                if (InBeat(392))
                {
                    float sin = 0;
                    int textcount = 0;
                    for (int i = 0; i < 64; i++)
                    {
                        DelayBeat(i * 0.25f, () =>
                        {
                            sin += 7;
                        });
                    }
                    char[] c = "流れてく".ToCharArray();
                    RegisterFunctionOnce("Text1", () =>
                    {
                        string st = $"{c[textcount]}";
                        Ef ef = new(st, CW, 2.5f) { edging = true,Depth=0.3f };
                        ef.vecOut(new V(690, 155 + 85 + Sin(sin) * 180), new V(-50, 155 + 85 + Sin(sin) * 180), T(3), Li);
                        CreateEntity(ef);
                        AddInstance(new TimeRangedEvent(T(3), () =>
                        {
                            ef.rot = Sin(Gametime * 2) * 20;
                        }));
                        textcount++;
                        ef.AutoDis(T(3));
                    });
                    RegisterFunctionOnce("Text2", () =>
                    {
                        float time = T(1.2f);
                        Ef ef1 = new("時", C.Aqua, 4) { edging = true };
                        ef1.rotOut(180, 0, time, ES.Sine);
                        ef1.rotIn(0, -180, time, T(2), ES.Sine);
                        DelayBeat(time, () => { ef1.rot = 0; });
                        ef1.vecOut(new V(100, -70), new V(100, 240), time, ES.Sine);
                        ef1.vecIn(new V(100, 240), new V(100, 560), time, T(2), ES.Sine);
                        CreateEntity(ef1);
                        Ef ef2 = new("の", C.Aqua, 4) { edging = true };
                        ef2.rotOut(180, 0, time, ES.Sine);
                        ef2.rotIn(0, -180, time, T(2f), ES.Sine);
                        DelayBeat(time, () => { ef2.rot = 0; });
                        ef2.vecOut(new V(540, 530), new V(540, 240), time, ES.Sine);
                        ef2.vecIn(new V(540, 240), new V(540, -100), time, T(2), ES.Sine);
                        CreateEntity(ef2);
                        ef1.AutoDis(T(3));
                        ef2.AutoDis(T(3));
                    });
                    RegisterFunctionOnce("Text3", () =>
                    {
                        Ef ef = new("中\nで", C.Red, 3) { edging = true };
                        ef.vecOut(new V(700, 240), new V(120, 240), T(1.5f), Cu);
                        ef.vecIn(new V(120, 240), new V(120, -150), T(2.5f), T(1), Ba);
                        ef.rotOut(0, 360, T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Text4", () =>
                    {
                        Ef ef = new("で\nも", C.Red, 3) { Depth = 0.7f, edging = true };
                        ef.vecOut(new V(-60, 240), new V(520, 240), T(1.5f), Cu);
                        ef.vecIn(new V(520, 240), new V(520, -150), T(1.5f), T(1), Ba);
                        ef.rotOut(0, -360, T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Text5", () =>
                    {
                        char[] c = "気だるさが".ToCharArray();
                        for (int i = 0; i < 5; i++)
                        {
                            Ef ef = new($"{c[i]}", new V(-300, -300), CW, 2) { edging = true };
                            ef.vecOut(new V(120 + 100 * i, 550), new V(120 + 100 * i, 420), T(i * 0.5f), T(2), Cu);
                            ef.vecIn(new V(120 + 100 * i, 420), new V(120 + 100 * i, 550), T(i * 0.5f + 2), T(2), Cu);
                            ef.rotOut(180, 0, T(i * 0.5f), T(2), Cu);
                            ef.rotIn(0, 180, T(i * 0.5f + 2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(16));
                        }
                    });
                    RegisterFunctionOnce("Text6", () =>
                    {
                        Ef ef = new("ほ", C.Red, 4) { edging = true };
                        ef.vecOut(new V(-100, 240), new V(200, 240), T(2), Cu);
                        ef.rotOut(45, 5, T(2), Cu);
                        ef.sizeOut(new V(4, 4), new V(4, 0), T(1.5f), T(1), Cu);
                        CreateEntity(ef);
                        Ef ef2 = new("ら", C.Red, 4) { edging = true };
                        ef2.vecOut(new V(770, 240), new V(470, 240), T(2), Cu);
                        ef2.rotOut(-45, -5, T(2), Cu);
                        ef2.sizeOut(new V(4, 4), new V(4, 0), T(1.5f), T(1), Cu);
                        CreateEntity(ef2);
                        ef.AutoDis(T(8));
                        ef2.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Text7", () =>
                    {
                        char[] text = "グルグル廻って".ToCharArray();
                        int InOrOut = 1;
                        for (int i = 0; i < text.Length; i++)
                        {
                            Ef ef = new($"{text[i]}", C.Aqua, 2f) { edging = true };
                            float rot = Rand(0, 360);
                            float temp = i;
                            float temp2 = InOrOut;
                            ef.Depth = 0.9f;
                            CreateEntity(ef);
                            ef.alphaOut(0, 1, T(2), Qd);
                            ef.AutoDis(T(16));
                            AddInstance(new TimeRangedEvent(T(0.5f + 0.5f * i), () =>
                            {
                                ef.centre = WCentre + GV(50 + temp * 50, rot);
                                ef.rot = rot * InOrOut;
                                rot += 3;
                            }));
                            DelayBeat(0.5f + 0.5f * i, () =>
                            {
                                float temp3;
                                float temp4;
                                if (temp < 4) 
                                { 
                                    temp3 = 320 - 75 * 1.5f + temp * 75;
                                    temp4 = 50;
                                }
                                else 
                                { 
                                    temp3 = 320 - 75 + (temp-4) * 75;
                                    temp4 = 125;
                                }
                                ef.vecOut(ef.centre, new V(temp3, temp4), T(2f), ES.Sine);
                                ef.rotOut(ef.rot, 0, T(2f), ES.Sine);
                                ef.vecIn(new V(temp3, temp4), new V(temp3 - 640, temp4 + Rand(-200, 200)), T(3-0.2f*temp), T(2), ES.Quad);
                                ef.rotIn(0, 720*RandSignal(), T(3), T(2), ES.Quad);
                            });
                            InOrOut *= -1;
                        }
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Text1","","Text1","",  "Text1","","Text1","",
                        "","","","",  "Text2","","","",
                        "","","","",  "","","Text3","",
                        "","","Text4","", "","","","",
                        "Text5","","","",  "","","","",
                        "","","","",  "Text6","","Text7","",
                        "",
                    });
                }
                #endregion
                #region 私から、離れる心も、見えないわ、そう知らない？
                if (InBeat(408))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("私", WCentre, C.Red, 3) { edging = true };
                        ef.sizeOut(new V(3, 0), new V(3), T(1), Cu);
                        ef.sizeOut(new V(3), new V(0, 3), T(2.5f), T(1), Cu);
                        ef.AutoDis(T(3.5f));
                        CreateEntity(ef);
                        ef.Depth = 0.95f;
                        DelayBeat(1f, () =>
                        {
                            Ef ef = new("か", WCentre - new V(150), C.Red, 3) { edging = true };
                            ef.sizeOut(new V(0, 3), new V(3), T(1), Cu);
                            ef.sizeOut(new V(3), new V(3, 0), T(1.5f), T(1), Cu);
                            ef.AutoDis(T(2));
                            CreateEntity(ef);
                            ef.Depth = 0.95f;
                        });
                        DelayBeat(1.5f, () =>
                        {
                            Ef ef = new("ら", WCentre + new V(150), C.Red, 3) { edging = true };
                            ef.sizeOut(new V(0, 3), new V(3), T(1), Cu);
                            ef.sizeOut(new V(3), new V(3, 0), T(1), T(1), Cu);
                            ef.AutoDis(T(2));
                            CreateEntity(ef);
                            ef.Depth = 0.95f;
                        });
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        char[] c = "離れる心も".ToCharArray();
                        int num = 1;
                        for (int i2 = 1; i2 < 6; i2++)
                        {
                            Ef ef = new($"{c[i2 - 1]}", WCentre, C.Red, 2) { Mirror = true, edging = true };
                            ef.vecIn(new V(100, -75 * i2), new V(100, 75 - 75 * (i2 - 1)), T(1), Cu);
                            ef.rotIn(45, -45, T(1), Cu);
                            int s = 1;
                            int temp = num;
                            for (int i = 0; i < 7; i++)
                            {
                                ef.vecIn(new V(100, 75 - 75 * (i2 - 1) + i * 75), new V(100, 75 - 75 * (i2 - 1) + (i + 1) * 75), T(1 + i), T(1), Cu);
                                ef.rotIn(-45 * s * temp, 45 * s * temp, T(1 + i), T(1), Cu);
                                s *= -1;
                            }
                            ef.alphaOut(1, 0, T(6), T(1), Cu);
                            ef.AutoDis(T(7));
                            CreateEntity(ef);
                            num *= -1;
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        char[] c = "見えないわ".ToCharArray();
                        for (int i = 0; i < 5; i++)
                        {
                            Ef ef = new($"{c[i]}", new V(320 - 200 + i * 100, 240), C.Gray, 2) { Alpha = 0 };
                            CreateEntity(ef);
                            ef.alphaOut(0, 1, T(i * 0.5f), T(1), Cu);
                            ef.alphaOut(1, 0, T(i * 0.5f + 1), T(1), Cu);
                            ef.AutoDis(T(8));
                        }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("そ\nう", WCentre, CW, 3) { edging = true };
                        CreateEntity(ef);
                        ef.AutoDis(T(1));
                        production1 = ActivateShader(FightResources.Shaders.Gray);
                        FightResources.Shaders.Gray.Intensity = 1;
                        DelayBeat(1, () => { production1.Dispose(); });
                    });
                    RegisterFunctionOnce("Ef5", () =>
                    {
                        RGBS sp = new(0.9564f) { Blend1 = new(0, 255, 0), Blend2 = C.Red, Blend3 = C.Blue };
                        SceneRendering.InsertProduction(sp);
                        char[] c = "知らない".ToCharArray();
                        for (int i = 0; i < 4; i++)
                        {
                            V temp = WCentre - new V(130 * 1.5f, 80 * 1.5f) + new V(130 * i, 80 * i);
                            float randtemp = Rand(-10, 10);
                            Ef ef = new($"{c[i]}", temp, C.Gold, 3) { Alpha = 0, scf = true, edging = true };
                            ef.rot = randtemp;
                            ef.alphaOut(0, 1, T(i), 1, Cu);
                            CreateEntity(ef);
                            Ef ef2 = new($"{c[i]}", temp, C.Gold, 3) { Alpha = 0, scf2 = true, edging = true };
                            ef2.rot = randtemp;
                            ef2.alphaOut(1, 0, T(i), T(1), Cu);
                            ef2.sizeOut(new V(3), new V(6), T(i), T(1), Cu);
                            CreateEntity(ef2);
                            foreach (Ef efs in new Ef[] { ef, ef2 })
                            {
                                efs.AutoDis(T(4));
                            }
                            DelayBeat(4, () => { RemoveUI2(); RemoveUI1(); });
                            DelayBeat(i, () =>
                            {
                                RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 1.5f),
                                    EaseOut(T(0.5f), -0.5f, Cu)));
                                for (int a = 0; a < 5; a++)
                                {
                                    AddInstance(new TimeRangedEvent(a * 2.1f, 1, () =>
                                    { ScreenDrawing.ScreenPositionDelta = new V(Rand(-7.0f, 7.0f), Rand(-7.0f, 7.0f)); }
                                    ));
                                    AddInstance(new TimeRangedEvent((a + 1) * 2.1f, 1, () =>
                                    { ScreenDrawing.ScreenPositionDelta = V.Zero; }
                                    ));
                                }
                                ScreenDrawing.MakeFlicker(Color.White * 1f);
                                int e = 50;
                                sp.rot = Rand(0, 360);
                                AddInstance(new TimeRangedEvent(25, () =>
                                {
                                    sp.intensity = e;
                                    e -= 2;
                                }));
                            });
                        }
                        DelayBeat(5, () => { sp.Dispose(); });
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "Ef4","","","",
                        "Ef5",
                    });
                }
                #endregion
                #region 自分から、動くこともなく、時の隙間に、流され続けて
                if (InBeat(424))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        char[] text = "自分から".ToCharArray();
                        int num = 1;
                        for (int i = 0; i < 4; i++)
                        {
                            float t = 240 - 150 + 100 * i;
                            Ef ef = new($"{text[i]}", CW, 2) { Alpha = 0, edging = true };
                            ef.vecOut(new V(320 - num * 200, t), new V(320, t), T(i * 0.5f), T(2), Cu);
                            ef.vecIn(new V(320, t), new V(320 + num * 200, t), T(i * 0.5f + 2), T(2), Cu);
                            ef.alphaOut(0, 1, T(i * 0.5f), T(2), Cu);
                            ef.alphaOut(1, 0, T(i * 0.5f + 2), T(2), Cu);
                            CreateEntity(ef);
                            num *= -1;
                            ef.AutoDis(T(8));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("動", CW, 4) { edging = true };
                        ef.vecOut(new V(-100, -100), new V(200, 150), T(1f), Cu);
                        ef.vecOut(new V(200, 150), new V(440, 330), T(1f), T(1f), Cu);
                        ef.rotOut(90, 10, T(1), Cu);
                        ef.rotOut(10, -10, T(1), T(1.5f), Cu);
                        CreateEntity(ef);
                        Ef ef2 = new("く", CW, 4) { edging = true };
                        ef2.vecOut(new V(740, 580), new V(440, 330), T(1f), Cu);
                        ef2.vecOut(new V(440, 330), new V(200, 150), T(1f), T(1f), Cu);
                        ef2.rotOut(-90, -10, T(1), Cu);
                        ef2.rotOut(-10, 10, T(1), T(1.5f), Cu);
                        float intensity = -10;
                        AddInstance(new TimeRangedEvent(T(2f), T(4), () =>
                        {
                            ef.centre += new V(-3, intensity);
                            ef2.centre += new V(3, intensity);
                            intensity += 0.5f;
                        }));
                        CreateEntity(ef2);
                        ef.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("こともなく", WCentre, C.Aqua, 3) { scf = true, edging = true };
                        CreateEntity(ef);
                        ef.sizeOut(new V(0, 3), new V(3), T(1.5f), Cu);
                        ef.sizeIn(new V(3), V.Zero, T(1.5f), T(1.5f), Cu);
                        ef.rotOut(180, -10, T(1.5f), Cu);
                        ef.rotIn(-10, 180, T(1.5f), T(1.5f), Cu);
                        ef.AutoDis(T(3));
                        DelayBeat(3, () => { RemoveUI1(); });
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        char[] text = "時の隙間に".ToCharArray();
                        for (int i = 0; i < 5; i++)
                        {
                            Ef ef = new($"{text[i]}", CW, 3) { edging = true };
                            ef.vecOut(new V(320 - 250 + i * 125, -60), new V(320 - 250 + i * 125, 100 + Sin(40 * i) * 280), T(2), Cu);
                            ef.vecIn(new V(320 - 250 + i * 125, 100 + Sin(40 * i) * 280), new V(320 - 250 + i * 125, 560), T(2), T(2), Cu);
                            float rand = Rand(-15f, 15f);
                            ef.rotOut(0, rand, T(2), Cu);
                            ef.rotIn(rand, -rand, T(2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(16));
                        }
                    });
                    RegisterFunctionOnce("Ef5", () =>
                    {
                        char[] text = "れさ流続けて".ToCharArray();
                        for (int i = 0; i < 3; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(680, 0), CW, 3) { edging = true };
                            CreateEntity(ef);
                            ef.alphaOut(1, 0, T(1.5f), T(2), Cu);
                            float tempy = 300 + Rand(-20, 20f);
                            float tempx = (320 - 100 + 100 * i) / T(1.5f);
                            float rot = GetAngle((float)Math.Atan2(tempy - 500, (320 - 100 + 100 * i))) * RandSignal();
                            float n = 0;
                            float time = T(2);
                            AddInstance(new TimeRangedEvent(T(16), () =>
                            {
                                ef.centre = new V(ef.centre.X - tempx, 590 - Sin(n) * tempy);
                                ef.rot = Cos(n) * -rot * 1.3f;
                                n += 90f / time;
                            }));
                            ef.AutoDis(T(16));
                        }
                        DelayBeat(1.5f, () =>
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                float tempy = 300 + Rand(-20, 20f);
                                float tempx = (320 - 100 + 100 * i) / T(1.5f);
                                float rot = GetAngle((float)Math.Atan2(tempy - 500, (320 - 100 + 100 * i) + 40)) * RandSignal();
                                float n = 0;
                                float time = T(2);
                                Ef ef = new($"{text[3 + i]}", new V(-40, 0), CW, 3) { edging = true };
                                CreateEntity(ef);
                                ef.alphaOut(1, 0, T(1.5f), T(2), Cu);
                                AddInstance(new TimeRangedEvent(T(16), () =>
                                {
                                    ef.centre = new V(ef.centre.X + tempx, 590 - Sin(n) * tempy);
                                    ef.rot = Cos(n) * -rot * 1.3f;
                                    n += 90f / time;
                                }));
                            }
                        });
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "Ef2","","","",
                        "","","","",  "Ef3","","","",
                        "","","","",  "","","","",
                        "Ef4","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef5","","","",
                    });
                }
                #endregion
                #region 知らないわ、周りのことなど、私は私、それだけ
                if (InBeat(440))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Vector3[] pos = new[] 
                        {
                            new Vector3(50,150,0),
                            new Vector3(120,300,20),
                            new Vector3(520,120,-40),
                            new Vector3(174,74,20),
                            new Vector3(320,150,-30),
                            new Vector3(320,50,20),
                        };
                        char[] s = "知らない".ToCharArray();
                        for (int i = 0; i < s.Length; i++)
                        {
                            V[] vec = new V[2];
                            float[] st = new float[2];
                            for (int i2 = 0, j = i; i2 < 2; i2++, j++)
                            {
                                vec[i2] = new V(pos[j].X, pos[j].Y);
                                st[i2] = pos[j].Z;
                            }
                            Ef ef = new(s[i].ToString(), CW, 1.7f) { edging = true, Alpha = 0 };
                            CreateEntity(ef);
                            ef.vecOut(vec[0], vec[1], T(i * 0.5f), T(0.7f), ES.Sine);
                            ef.rotOut(st[0], st[1], T(i * 0.5f), T(0.7f), ES.Sine);
                            ef.alphaOut(0, 1, T(i * 0.5f), 1, Li);
                            ef.AutoDis(T(0.5f * i + 0.5f));
                            Ef ef2 = new(s[i].ToString(), vec[0], CW, 1.7f) { edging = true, rot = st[0], Alpha = 0 };
                            ef2.alphaOut(0, 1, T(0.5f * i), 1, Li);
                            ef2.alphaOut(1, 0, T(0.5f * i), T(2), Li);
                            ef2.sizeOut(new V(1.7f), new V(3), T(0.5f * i), T(2), ES.Cubic);
                            CreateEntity(ef2);
                            ef2.AutoDis(T(0.5f * i + 2));
                        }
                        DelayBeat(2f, () => 
                        {
                            Ef ef = new("わ", new V(320, 150), CW, 1.7f) {edging=true };
                            ef.alphaOut(1, 0, T(2), Cu);
                            ef.sizeOut(new V(1.7f),new V(3),T(2),Cu);
                            CreateEntity(ef);
                            for (int i = 0; i < 7; i++)
                            {
                                Ef ef2 = new("わ", new V(320, 150), CW, 1.7f) { edging = true };
                                float x=Sin(i*360/7f)*7, y=Cos(i*360/7f)*-7, z=0;
                                ForBeat(T(8), () => 
                                {
                                    ef2.centre += new V(x, y);
                                    V vec = ef2.centre;
                                    z+=0.03f;
                                    y+= z;
                                    V vec2 = vec + new V(x, y);
                                    ef2.rot =MathF.Atan2(vec2.Y-vec.Y,vec2.X-vec.X)*180/MathF.PI+90;
                                });
                                CreateEntity(ef2);
                            }
                        });
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        char[] text = "周りのことなど".ToCharArray();
                        for (int i = 0; i < text.Length; i++)
                        {
                            Ef ef = new($"{text[i]}", CW, 1.5f) { edging = true };
                            CreateEntity(ef);
                            ef.vecOut(WCentre, WCentre + GV(200, -90 + i * 360 / 7f), T(2), El);
                            ef.sizeOut(new V(0), new V(2f), T(2), El);
                            ef.alphaOut(1, 0, T(2 + i * 0.5f), T(1), Cu);
                            ef.AutoDis(T(16));
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("私", new(320, 100), CW, 2) { edging = true };
                        ef.rotIn(0, 90, T(1.5f), T(1), Cu);
                        ef.rotOut(90, 180, T(2.5f), T(1), Cu);
                        ef.sizeOut(V.Zero, new(3), T(1), Cu);
                        ef.colOut(CW, C.Red, T(1.5f), T(1.5f), Cu);
                        ef.vecIn(new V(320, 100), new V(640, 580), T(3), T(2), Cu);
                        CreateEntity(ef);
                        Ef ef2 = new("私", new(320, 380), CW, 2) { rot = 180, edging = true };
                        ef2.rotIn(180, 90, T(1.5f), T(1), Cu);
                        ef2.rotOut(90, 0, T(2.5f), T(1), Cu);
                        ef2.sizeOut(V.Zero, new(3), T(1), Cu);
                        ef2.colOut(CW, C.Aqua, T(1.5f), T(1.5f), Cu);
                        ef2.vecIn(new V(320, 380), new V(0, -100), T(3), T(2), Cu);
                        CreateEntity(ef2);
                        ef.AutoDis(T(5));
                        ef2.AutoDis(T(5));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        RGBS sp = new(0.9564f) { Blend1 = new(0, 255, 0), Blend2 = C.Red, Blend3 = C.Blue };
                        SceneRendering.InsertProduction(sp);
                        Ef ef1 = new("そ", new(320 - 100, 240 - 100), C.Red, 3) { scf = true, edging = true };
                        Ef ef2 = new("れ", new(320 - 100, 240 + 100), C.Red, 3) { scf = true, edging = true };
                        Ef ef3 = new("だ", new(320 + 100, 240 - 100), C.Red, 3) { scf = true, edging = true };
                        Ef ef4 = new("け", new(320 + 100, 240 + 100), C.Red, 3) { scf = true, edging = true };
                        ef1.alphaOut(0, 1, 0, 1, Cu);
                        ef2.alphaOut(0, 1, T(1), 1, Cu);
                        ef3.alphaOut(0, 1, T(2), 1, Cu);
                        ef4.alphaOut(0, 1, T(3), 1, Cu);
                        V vec1 = ef1.centre;
                        V vec2 = ef2.centre;
                        V vec3 = ef3.centre;
                        V vec4 = ef4.centre;
                        foreach (Ef ef in new Ef[] { ef1, ef2, ef3, ef4 })
                        {
                            ef.Alpha = 0;
                            CreateEntity(ef);
                            ef.vecIn(ef.centre, ef.centre + new V(Rand(-50, 50), -500), T(4), T(2), Cu);
                            ef.rotIn(0, Rand(-50, 50), T(4), T(2), Cu);
                            ef.colIn(C.Red, C.Gray, T(4), T(2), Cu);
                            ef.AutoDis(T(6));
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i, () =>
                            {
                                RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 1.5f),
                                    EaseOut(T(0.5f), -0.5f, Cu)));
                                for (int a = 0; a < 5; a++)
                                {
                                    AddInstance(new TimeRangedEvent(a * 2.1f, 1, () =>
                                    {
                                        ScreenPositionDelta = new V(Rand(-7.0f, 7.0f), Rand(-7.0f, 7.0f));
                                        ef1.centre = vec1 + ScreenPositionDelta;
                                        ef2.centre = vec2 + ScreenPositionDelta;
                                        ef3.centre = vec3 + ScreenPositionDelta;
                                        ef4.centre = vec4 + ScreenPositionDelta;
                                    }
                                    ));
                                    AddInstance(new TimeRangedEvent((a + 1) * 2.1f, 1, () =>
                                    {
                                        ScreenPositionDelta = V.Zero;
                                        ef1.centre = vec1 + ScreenPositionDelta;
                                        ef2.centre = vec2 + ScreenPositionDelta;
                                        ef3.centre = vec3 + ScreenPositionDelta;
                                        ef4.centre = vec4 + ScreenPositionDelta;
                                    }
                                    ));
                                }
                                MakeFlicker(CW * 1f);
                                float e = 50;
                                sp.rot = Rand(0, 360);
                                AddInstance(new TimeRangedEvent(25, () =>
                                {
                                    sp.intensity = e;
                                    e *= -0.86f;
                                }));
                            });
                        }
                        DelayBeat(5, () => { sp.Dispose(); });
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "Ef2","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4",
                    });
                }
                #endregion
                #region 夢見てる？なにも見てない？語るも無駄な、自分の言葉
                if (InBeat(456))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        char[] text = "夢見てる?".ToCharArray();
                        for (int i = 0; i < 5; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(-400), CW, 1f) { Depth = 0.8f, edging = true };
                            ef.AutoDis(T(4 + i * 0.5f));
                            V pos = new V(50 + i * 50, 600 + Rand(-20f, 20f));
                            CreateEntity(ef);
                            float setrand = Rand(0, 180);
                            float time = Rand(0, 360f);
                            float addnum = Rand(0.3f, 2f) * RandSignal();
                            AddInstance(new TimeRangedEvent(T(i * 0.5f), T(4), () =>
                            {
                                ef.centre = pos + new V(Sin(setrand * 4) * 10, Sin(setrand * 3) * 10);
                                ef.rot = Sin(setrand * 3) * 10;
                                setrand += addnum / 1.5f;
                                EfEn efs = new(ef.centre, Sprites.square) { Depth = 0.7f, Color = new(DrawingLab.HsvToRgb(time, 255, 255, 255)) };
                                efs.vec2Ease(true, ef.centre, new V(Rand(-10f, 10f), 10), T(1), Cu);
                                efs.AlphaEase(true, 0.5f, 0, T(0.1f), T(0.9f), Cu);
                                efs.AlphaEase(true, 0, 0.5f, T(0.1f), Cu);
                                efs.rotEase(false, 0, Rand(-40f, 40f), T(1), Cu);
                                CreateEntity(efs);
                                efs.AutoDis(T(1));
                                time++;
                            }));
                            AddInstance(new InstantEvent(T(i * 0.5f), () =>
                            {
                                RunEase((s) => { pos = s; }, LinkEase(
                                    Stable(0, pos), EaseOut(T(2f), new V(0, -400), Cu),
                                    Stable(0, pos), EaseIn(T(2f), new V(0, -400), Cu)));
                            }));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        char[] text = "何も見てない?".ToCharArray();
                        for (int i = 0; i < 7; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(-400), CW, 1f) { Depth = 0.8f, edging = true };
                            ef.AutoDis(T(4 + i * 0.5f));
                            V pos;
                            if (i < 2)
                            {
                                pos = new V(400 + i * 50, 600 + Rand(-20f, 20f));
                            }
                            else
                            {
                                pos = new V(375 + (i - 2) * 50, 700 + Rand(0f, 20f));
                            }
                            CreateEntity(ef);
                            float setrand = Rand(0, 180);
                            float time = Rand(0, 360f);
                            float addnum = Rand(0.3f, 2f) * RandSignal();
                            AddInstance(new TimeRangedEvent(T(i * 0.5f), T(4), () =>
                            {
                                ef.centre = pos + new V(Sin(setrand * 4) * 10, Sin(setrand * 3) * 10);
                                ef.rot = Sin(setrand * 3) * 10;
                                setrand += addnum / 1.5f;
                                EfEn efs = new(ef.centre, Sprites.square) { Depth = 0.7f, Color = new(DrawingLab.HsvToRgb(time, 255, 255, 255)) };
                                efs.vec2Ease(true, ef.centre, new V(Rand(-10f, 10f), 10), T(1), Cu);
                                efs.AlphaEase(true, 0.5f, 0, T(0.1f), T(0.9f), Cu);
                                efs.AlphaEase(true, 0, 0.5f, T(0.1f), Cu);
                                efs.rotEase(false, 0, Rand(-40f, 40f), T(1), Cu);
                                CreateEntity(efs);
                                efs.AutoDis(T(1));
                                time++;
                            }));
                            AddInstance(new InstantEvent(T(i * 0.5f), () =>
                            {
                                RunEase((s) => { pos = s; }, LinkEase(
                                    Stable(0, pos), EaseOut(T(2f), new V(0, -400), Cu),
                                    Stable(0, pos), EaseIn(T(2f), new V(0, -400), Cu)));
                            }));
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        char[] text = "語るも無駄な".ToCharArray();
                        for (int i = 0; i < 6; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(-400), CW, 1f) { Depth = 0.8f, edging = true };
                            ef.AutoDis(T(4 + i * 0.5f));
                            V pos = new V(50 + i * 50, 600 + Rand(-20f, 20f));
                            if (i < 3)
                            { pos = new V(50 + i * 50, 600 + Rand(-20f, 20f)); }
                            else
                            { pos = new V(75 + (i - 3) * 50, 650 + Rand(-20f, 20f)); }
                            CreateEntity(ef);
                            float setrand = Rand(0, 180);
                            float time = Rand(0, 360f);
                            float addnum = Rand(0.3f, 2f) * RandSignal();
                            AddInstance(new TimeRangedEvent(T(i * 0.5f), T(4), () =>
                            {
                                ef.centre = pos + new V(Sin(setrand * 4) * 10, Sin(setrand * 3) * 10);
                                ef.rot = Sin(setrand * 3) * 10;
                                setrand += addnum / 1.5f;
                                EfEn efs = new(ef.centre, Sprites.square) { Depth = 0.7f, Color = new(DrawingLab.HsvToRgb(time, 255, 255, 255)) };
                                efs.vec2Ease(true, ef.centre, new V(Rand(-10f, 10f), 10), T(1), Cu);
                                efs.AlphaEase(true, 0.5f, 0, T(0.1f), T(0.9f), Cu);
                                efs.AlphaEase(true, 0, 0.5f, T(0.1f), Cu);
                                efs.rotEase(false, 0, Rand(-40f, 40f), T(1), Cu);
                                CreateEntity(efs);
                                efs.AutoDis(T(1));
                                time++;
                            }));
                            AddInstance(new InstantEvent(T(i * 0.5f), () =>
                            {
                                RunEase((s) => { pos = s; }, LinkEase(
                                    Stable(0, pos), EaseOut(T(2f), new V(0, -400), Cu),
                                    Stable(0, pos), EaseIn(T(2f), new V(0, -400), Cu)));
                            }));
                        }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        char[] text = "自分の言葉".ToCharArray();
                        for (int i = 0; i < 5; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(-400), C.Black, 1f) { Depth = 0.8f, scf2 = true, edging = true, edgingColor = CW };
                            ef.AutoDis(T(4 + i));
                            V pos = new V(220 + i * 50, 850 + Rand(-20f, 20f));
                            CreateEntity(ef);
                            float setrand = Rand(0, 180);
                            float time = Rand(0, 360f);
                            float addnum = Rand(0.3f, 2f) * RandSignal();
                            AddInstance(new TimeRangedEvent(T(i * 0.5f), T(4), () =>
                            {
                                ef.centre = pos + new V(Sin(setrand * 4) * 10, Sin(setrand * 3) * 10);
                                ef.rot = Sin(setrand * 3) * 10;
                                setrand += addnum / 1.5f;
                                EfEn efs = new(ef.centre, Sprites.square) { Depth = 0.7f, Color = new(DrawingLab.HsvToRgb(time, 10, 10, 10)) };
                                efs.vec2Ease(true, ef.centre, new V(Rand(-10f, 10f), 10), T(1), Cu);
                                efs.AlphaEase(true, 0.5f, 0, T(0.1f), T(0.9f), Cu);
                                efs.AlphaEase(true, 0, 0.5f, T(0.1f), Cu);
                                efs.rotEase(false, 0, Rand(-40f, 40f), T(1), Cu);
                                CreateEntity(efs);
                                efs.AutoDis(T(1));
                                time++;
                            }));
                            AddInstance(new InstantEvent(T(i * 0.5f), () =>
                            {
                                RunEase((s) => { pos = s; }, LinkEase(
                                    Stable(0, pos), EaseOut(T(2f), new V(0, -500), Cu),
                                    Stable(0, pos), EaseIn(T(2f), new V(0, -500), Cu)));
                            }));
                        }
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "Ef2","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef3","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef4",
                    });
                }
                #endregion
                #region 悲しむなんて、疲れるだけよ、何も感じず、過ごせばいいの
                if (InBeat(471))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        char[] text = "悲しむなんて".ToCharArray();
                        for (int i = 0; i < text.Length; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(100 + i * 50, 200), C.Gray, 0) { Alpha = 0, edging = true,scf=true };
                            //ef.vecOut();
                            ef.sizeOut(new V(1.5f, 0), new V(1.5f), T(i * 0.5f), T(2), ES.Circ);
                            ef.alphaOut(0, 1, T(i * 0.5f), T(2), Cu);
                            ef.sizeOut(new V(1.5f), new V(1.5f, 0), T(i * 0.5f + 2), T(2), ES.Circ);
                            ef.alphaOut(1, 0, T(i * 0.5f + 2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(i * 0.5f + 6));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        char[] text = "疲れるだけよ".ToCharArray();
                        for (int i = 0; i < text.Length; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(350 + i * 50, 300), C.DarkRed, 0) { Alpha = 0, edging = true, scf = true };
                            //ef.vecOut();
                            ef.sizeOut(new V(1.5f, 0), new V(1.5f), T(i * 0.5f), T(2), Cu);
                            ef.alphaOut(0, 1, T(i * 0.5f), T(2), Cu);
                            ef.sizeOut(new V(1.5f), new V(1.5f, 0), T(i * 0.5f + 2), T(2), Cu);
                            ef.alphaOut(1, 0, T(i * 0.5f + 2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(i * 0.5f + 6));
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        char[] text = "何も感じず".ToCharArray();
                        for (int i = 0; i < text.Length; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(220 + i * 50, 100), new(90, 112, 125), 0) { Alpha = 0, edging = true, scf = true };
                            //ef.vecOut();
                            ef.sizeOut(new V(1.5f, 0), new V(1.5f), T(i * 0.5f), T(2), Cu);
                            ef.alphaOut(0, 1, T(i * 0.5f), T(2), Cu);
                            ef.sizeOut(new V(1.5f), new V(1.5f, 0), T(i * 0.5f + 2), T(2), Cu);
                            ef.alphaOut(1, 0, T(i * 0.5f + 2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(i * 0.5f + 6));
                        }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        char[] text = "過ごせばいいの".ToCharArray();
                        for (int i = 0; i < text.Length; i++)
                        {
                            Ef ef = new($"{text[i]}", new V(320 - 3 * 70 + i * 70, 100), C.DarkRed, 0) { Alpha = 0, edging = true, scf = true };
                            //ef.vecOut();
                            ef.sizeOut(new V(2f, 0), new V(2f), T(i * 0.5f), T(2), Cu);
                            ef.alphaOut(0, 1, T(i * 0.5f), T(2), Cu);
                            ef.sizeOut(new V(2f), new V(2f, 0), T(i * 0.5f + 2), T(2), Cu);
                            ef.alphaOut(1, 0, T(i * 0.5f + 2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(i * 0.5f + 6));
                        }
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4",
                    });
                }
                #endregion
                #region 戸惑う言葉、与えられても、自分の心、ただ上の空
                if (InBeat(488))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        char[] ct = "戸惑う言葉".ToCharArray();
                        for (int i = 0; i < 3; i++)
                        {
                            Ef ef = new($"{ct[i]}", new V(150, 140 + 100 * i), C.Aqua, 2) { Depth = 0.15f, edging = true };
                            ef.rot = Rand(-10, 10);
                            ef.vecIn(ef.centre, WCentre, T(2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(8));
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            Ef ef = new($"{ct[i + 3]}", new V(490, 190 + 100 * i), C.Aqua, 2) { Depth = 0.15f, edging = true };
                            ef.rot = Rand(-10, 10);
                            ef.vecIn(ef.centre, WCentre, T(2), T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(8));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        char[] ct = "与えられても".ToCharArray();
                        for (int i = 0; i < 3; i++)
                        {
                            Ef ef = new($"{ct[i]}", WCentre, C.Aqua, 2) { Depth = 0.15f, edging = true };
                            ef.vecOut(WCentre, new V(150, 140 + 100 * i), T(2), Cu);
                            ef.vecIn(new V(150, 140 + 100 * i), new V(150, -400 + 100 * i), T(2 + i * 0.5f), T(2), Cu);
                            ef.rot = Rand(-10, 10);
                            CreateEntity(ef);
                            ef.AutoDis(T(8));
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            Ef ef = new($"{ct[i + 3]}", WCentre, C.Aqua, 2) { Depth = 0.15f, edging = true };
                            ef.vecOut(WCentre, new V(490, 140 + 100 * i), T(2), Cu);
                            ef.vecIn(new V(490, 140 + 100 * i), new V(490, -400 + 100 * i), T(2.25f + i * 0.5f), T(2), Cu);
                            ef.rot = Rand(-10, 10);
                            CreateEntity(ef);
                            ef.AutoDis(T(8));
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        //100,540,100,380
                        Ef ef = new("自", C.Aqua, 2) { edging = true };
                        ef.vecOut(new V(-100, 100), new V(320, 100), T(2), Cu);
                        ef.vecIn(new V(320, 100), new V(740, 100), T(2), T(2), Cu);
                        Ef ef2 = new("分", C.Aqua, 2) { edging = true };
                        float temp = 320 - 140;
                        ef2.vecOut(new V(640 - temp, -100), new V(640 - temp, 240), T(2), Cu);
                        ef2.vecIn(new V(640 - temp, 240), new V(640 - temp, 580), T(2), T(2), Cu);
                        Ef ef3 = new("の", C.Aqua, 2) { edging = true };
                        ef3.vecOut(new V(740, 380), new V(320, 380), T(2), Cu);
                        ef3.vecIn(new V(320, 380), new V(-100, 380), T(2), T(2), Cu);
                        Ef ef4 = new("心", C.Aqua, 2) { edging = true };
                        ef4.vecOut(new V(temp, 580), new V(temp, 240), T(2), Cu);
                        ef4.vecIn(new V(temp, 240), new V(temp, -100), T(2), T(2), Cu);
                        foreach (Ef efs in new Ef[] { ef, ef2, ef3, ef4 })
                        {
                            CreateEntity(efs);
                            efs.AutoDis(T(4));
                            efs.rotOut(360, 0, T(2), Cu);
                            efs.rotIn(0, 360, T(2), T(2), Cu);
                        }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("ただ上の空", WCentre, C.Red, 3) { Alpha = 0, edging = true };
                        ef.sizeOut(20, 3, T(4), Cu);
                        ef.alphaOut(0, 1, T(4), Cu);
                        ef.rotOut(720, 0, T(4), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(5));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "Ef3","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef4","","","",
                    });
                }
                #endregion
                #region もし私から、動くのならば、全て変えるのなら、黒にする
                if (InBeat(504))
                {
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("も\nし\n私\nか\nら", CW, 1.5f) { edging = true };
                        ef.vecOut(new V(-100, 240), new V(150, 240), T(2), Cu);
                        ef.vecIn(new V(150, 240), new V(150, 700), T(2), T(2), Cu);
                        ef.rotIn(0, 45, T(2), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("動\nく\nの\nな\nら\nば", CW, 1.5f) { edging = true };
                        ef.vecOut(new V(740, 240), new V(490, 240), T(2), Cu);
                        ef.vecIn(new V(490, 240), new V(490, 700), T(2), T(2), Cu);
                        ef.rotIn(0, -45, T(2), T(2), Cu);
                        CreateEntity(ef);
                        ef.AutoDis(T(4));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 1),
                            EaseIn(T(1f), 2, ES.Cubic)));
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0),
                            EaseIn(T(1f), 45, ES.Cubic)));
                        Ef ef = new("全て", WCentre, C.Red, 3f) { scf2 = true, edging = true };
                        Ef ef2 = new("変えるのなら", WCentre, C.Gold, 2f) { Alpha = 0, edging = true };
                        ef.rotOut(180, 380, T(4), Cu);
                        ef.sizeOut(V.Zero, new V(4), T(4), Cu);
                        ef2.alphaOut(0, 1, T(1), 1, Cu);
                        ef2.rotOut(180, 380, T(4), Cu);
                        ef2.rotIn(380, 0, T(4), T(2), Cu);
                        ef2.sizeOut(V.Zero, new V(2), T(4), Cu);
                        ef2.sizeOut(new V(2), V.Zero, T(4), T(2), Cu);
                        ef.AutoDis(T(1));
                        DelayBeat(1, () => RemoveUI2());
                        ef2.AutoDis(T(6));
                        foreach (Ef efs in new Ef[] { ef, ef2 })
                        { CreateEntity(efs); }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Ef ef = new("黒にする", new(DrawingLab.HsvToRgb(i * 72, 255, 255, 255)), 3f) { scf = true, Alpha = 0 };
                            ef.colIn(ef.Color, C.Black, T(2), Cu);
                            ef.vecOut(WCentre + GV(400, i * 72), WCentre, T(4), Cu);
                            ef.rotOut(i * 72, -25, T(4), Cu);
                            ef.alphaOut(0, 1, T(2), Cu);
                            CreateEntity(ef);
                            ef.AutoDis(T(5));
                        }
                    });
                    CreateChart(0, T(2), 4, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "Ef2","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef3","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef4",
                    });
                }
                #endregion
                #region 無駄な時間に、未来はあるの?こんな所に、私はいるの?
                if (InBeat(520))
                {

                    V pos = new V();
                    AddInstance(new TimeRangedEvent(T(20), () =>
                    {
                        pos = new V(320 + Sin(Gametime * 1.2f) * 70, 240 + Sin(Gametime * 1.7f) * 70);
                    }));
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        Ef ef = new("無駄な時間に", CW, 2);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        AddInstance(new TimeRangedEvent(T(8), () =>
                        {
                            ef.centre = pos;
                        }));
                        CreateEntity(ef);
                        ef.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        Ef ef = new("未来はあるの?", CW, 2);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        AddInstance(new TimeRangedEvent(T(8), () =>
                        {
                            ef.centre = pos;
                        }));
                        CreateEntity(ef);
                        ef.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        Ef ef = new("こんな所に", CW, 2);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        AddInstance(new TimeRangedEvent(T(8), () =>
                        {
                            ef.centre = pos;
                        }));
                        CreateEntity(ef);
                        ef.AutoDis(T(8));
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        Ef ef = new("私はいるの?", CW, 2);
                        ef.alphaOut(0, 1, T(2), Cu);
                        ef.alphaOut(1, 0, T(4), T(2), Cu);
                        AddInstance(new TimeRangedEvent(T(8), () =>
                        {
                            ef.centre = pos;
                        }));
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",   "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",   "","","","",
                        "Ef4","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
                #endregion
                #region 私のことを、言いたいならば、言葉にするなら、「ろくでなし」
                if (InBeat(535))
                {
                    CreateEntity(inyouT);
                    inyouT.AlphaM(EaseOut(T(2),0,1,ES.Sine));
                    //ActivateShader(FightResources.Shaders.Blur);
                    FightResources.Shaders.Blur.Sigma = 1f;
                    inyouT.controlLayer = CustomRender1(BackGroundRendering, "inyou", FightResources.Shaders.Blur).UISurface;
                    float rot = 0;
                    inyouT.RotM(LinkEase(
                        EaseOut(T(3), 0, 180, ES.Sine),
                        EaseInOut(T(4), 0, 720, ES.Cubic),
                        EaseInOut(T(4), 0, 680, ES.Cubic),
                        EaseIn(T(2),180,ES.Cubic),Stable(T(2)),EaseIn(T(2),0,-1440,ES.Cubic))).Run(s => rot = s);
                    RunEase(s => FightResources.Shaders.Blur.Factor = GV(s, rot),
                        LinkEase(
                            EaseOut(T(3),0,ES.Sine),
                            EaseIn(T(2), 15, ES.Cubic), EaseOut(T(2), -15, ES.Cubic),
                            EaseIn(T(2), 15, ES.Cubic), EaseOut(T(2), -15, ES.Cubic)));
                    RegisterFunctionOnce("Ef1", () =>
                    {
                        inyouT.count = 10;
                        char[] str = "私のことを".ToCharArray();
                        for (int i = 0; i < str.Length; i++) 
                        {
                            Ef ef = new(str[i].ToString(),C.White,1.5f);
                            //ef.controlLayer= CustomRender2(BackGroundRendering, "string1", FightResources.Shaders.Blur).UISurface;
                            ef.Alpha = 0;
                            ef.Depth = 0.1f;
                            ef.alphaOut(0,1,T(1+i*0.5f),ES.Sine);
                            ef.alphaOut(1, 0, T(4),T(1), ES.Sine);
                            var temp = 180f / str.Length;
                            var myrot = i * temp*2+temp/2;
                            ForBeat120(16, () => 
                            {
                                ef.centre = inyouT.Centre + GV(150,myrot+inyouT.Rotation);
                                ef.rot = inyouT.Rotation + myrot+90;
                            });
                            CreateEntity(ef);
                            ef.AutoDis(T(5));
                        }
                    });
                    RegisterFunctionOnce("Ef2", () =>
                    {
                        inyouT.count = 7;
                        //RemoveCustom("string1",1);
                        char[] str = "言いたいならば".ToCharArray();
                        for (int i = 0; i < str.Length; i++)
                        {
                            Ef ef = new(str[i].ToString(), C.White, 1.5f);
                            ef.controlLayer = CustomRender2(BackGroundRendering, "string2", FightResources.Shaders.Blur).UISurface;
                            ef.Alpha = 0;
                            ef.Depth = 0.1f;
                            ef.alphaOut(0, 1, T(1 + i * 0.5f), ES.Sine);
                            ef.alphaOut(1, 0, T(4),T(1), ES.Sine);
                            var temp = 360f / str.Length;
                            var myrot = i * temp + temp / 2;
                            ForBeat120(16, () =>
                            {
                                ef.centre = inyouT.Centre + GV(150, myrot + inyouT.Rotation);
                                ef.rot = inyouT.Rotation + myrot + 90;
                            });
                            CreateEntity(ef);
                            ef.AutoDis(T(5));
                        }
                    });
                    RegisterFunctionOnce("Ef3", () =>
                    {
                        inyouT.count = 8;
                        char[] str = "言葉にするのなら".ToCharArray();
                        for (int i = 0; i < str.Length; i++)
                        {
                            Ef ef = new(str[i].ToString(), C.White, 1.5f);
                            ef.controlLayer = CustomRender3(BackGroundRendering, "string3", FightResources.Shaders.Blur).UISurface;
                            ef.Depth = 0.1f;
                            ef.Alpha = 0;
                            ef.alphaOut(0, 1, T(1 + i * 0.5f), ES.Sine);
                            ef.alphaOut(1, 0, T(4),T(1), ES.Sine);
                            var temp = 360f / str.Length;
                            var myrot = i * temp + temp / 2;
                            ForBeat120(16, () =>
                            {
                                ef.centre = inyouT.Centre + GV(150, myrot + inyouT.Rotation);
                                ef.rot = inyouT.Rotation + myrot + 90;
                            });
                            CreateEntity(ef);
                            ef.AutoDis(T(5));
                        }
                    });
                    RegisterFunctionOnce("Ef4", () =>
                    {
                        inyouT.Depth = 0.9f;
                        inyouT.stop = true;
                        RemoveCustom("string3");
                        RemoveCustom("string2");
                        char[] text = "ろくでなし".ToCharArray();
                        for (int i = 0; i < 5; i++)
                        {
                            var centre = WCentre - new V(120 * 2f, 0) + new V(120 * i,0);
                            Ef ef = new($"{text[i]}", centre, CW, 3) { Alpha = 0, font = Resources.BadAppleRE.Final,Depth=1 };
                            ef.alphaOut(0, 1, T(i * 0.5f), 1, Cu);

                            CreateEntity(ef);
                            ef.AutoDis(T(4));
                            ef.edging = true;
                            ForBeat120(4, () => 
                            {
                                ef.centre = centre + GV(Rand(3,25f)*RandSignal(),Rand(0,359f));
                            });
                        }
                        inyouT.count = 14;
                        inyouT.controlLayer = Surface.Normal;
                        char[] str = "XXXXXXXXXXXXXX".ToCharArray();
                        for (int i = 0; i < str.Length; i++)
                        {
                            Ef ef = new(str[i].ToString(), C.White, 1.5f) {font=Final };
                            ef.Alpha = 1;
                            var temp = 360f / str.Length;
                            var myrot = i * temp + temp / 2;
                            ForBeat120(16, () =>
                            {
                                ef.centre = inyouT.Centre + GV(150, myrot + inyouT.Rotation);
                                ef.rot = inyouT.Rotation + myrot + 90;
                            });
                            CreateEntity(ef);
                            ef.AutoDis(T(4));
                        }
                        DelayBeat(4, inyouT.Dispose);
                        DelayBeat(2, () => 
                        {
                            RunEase(s => ScreenPositionDelta=new V(s.X,RandSignal()*s.Y), 
                                Combine(Alternate(1,EaseIn(T(2),0,100,ES.Sine),EaseIn(T(2),0,-100,ES.Sine)),EaseIn(T(2),0,40,ES.Sine)));
                        });
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "Ef1","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef2","","","",  "","","","",
                        "","","","",  "","","","",
                        "Ef3","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "Ef4",

                    });
                }
                #endregion
                #region 動くのならば、動くのならば、すべて壊すわ、すべて壊すわ
                if (InBeat(552 + 95f)) 
                {
                    string[] text = new[] 
                    {
                        "動くのならば",
                        "すべて壊すわ","すべて壊すわ",
                        "悲しむならば","悲しむならば",
                        "私の心","白く変われる?",
                        "貴方のことも","私のことも",
                        "全てのことも","まだ知らないの",
                        "重い目蓋を","開けたのならば",
                        "全て壊すのなら","黒になれ"
                    };
                    int count = 0;
                    RegisterFunctionOnce("Efs", () =>
                    {
                        var str = text[count].ToCharArray();
                        List<V> vec = new();
                        for (int i = 0; i < str.Length; i++)
                        {
                            var pos = new V(Rand(20f, 620), Rand(20f, 460));
                            vec.Add(pos);
                            for (int i2 = 0; i2 < vec.Count - 1; i2++)
                                while ((vec[i2] - pos).Length() < 100)
                                    pos = new V(Rand(50f, 590), Rand(50, 430));
                            Ef ef = new(str[i].ToString(), pos, C.Black, 1) { font = Final2, Alpha = 0 };
                            ef.alphaOut(0, 1, T(i * 0.5f), T(2), ES.Linear);
                            ef.alphaOut(1, 0, T(2 + i * 0.5f), T(2), ES.Linear);
                            ef.AutoDis(T(4.5f + i * 0.5f));
                            CreateEntity(ef);
                            ef.scf = true;
                            Ef ef2 = new(str[i].ToString(), ef.Centre, C.Black, 1) { font = Final2, Alpha = 0 };
                            ef2.alphaOut(0, 1, T(i * 0.5f), T(2), ES.Linear);
                            ef2.alphaOut(1, 0, T(2 + i * 0.5f), T(2), ES.Linear);
                            ef2.AutoDis(T(4 + i * 0.5f));
                            CreateEntity(ef2);
                            ef2.scf2 = true;
                            ef.scf = true;
                            var time = Rand(0, 360f);
                            var runx = Rand(0.7f, 1.8f) * RandSignal();
                            var runy = Rand(0.7f, 1.8f) * RandSignal();
                            var runrot = Rand(0.7f, 1.8f) * RandSignal();
                            var rot = Rand(5f, 15f) * RandSignal();
                            ForBeat(32, () =>
                            {
                                ef.centre = ef.Centre + new V(Sin((time + Gametime / 3f) * runx) * 10, Sin((time + Gametime / 3f) * runx) * 10);
                                ef.rot = Sin(time + Gametime / 10f) * rot;
                                ef2.centre = ef2.Centre + new V(Sin((time + Gametime / 3f) * runx) * 10, Sin((time + Gametime / 3f) * runx) * 10);
                                ef2.rot = Sin(time + Gametime / 10f) * rot;
                            });
                        }
                        count++;
                    });
                    RegisterFunctionOnce("Ef1", () => 
                    {
                        var str = text[count].ToCharArray();
                        List<V> vec = new();
                        for (int i = 0; i < str.Length; i++)
                        {
                            var pos = new V(Rand(20f, 620), Rand(20f, 460));
                            vec.Add(pos);
                            for (int i2 = 0; i2 < vec.Count - 1; i2++)
                                while ((vec[i2] - pos).Length() < 100)
                                    pos = new V(Rand(20f, 620), Rand(20, 460));
                            Ef ef = new(str[i].ToString(),pos, C.Black, 1) {font=Final2,Alpha=0 };
                            ef.alphaOut(0,1,T(i*0.5f),T(2),ES.Linear);
                            ef.AutoDis(T(6));
                            CreateEntity(ef);
                            ef.scf = true;
                            Ef ef2 = new(str[i].ToString(), ef.Centre, C.Black, 1) { font = Final2, Alpha = 0 };
                            ef2.alphaOut(0, 1, T(i * 0.5f), T(2), ES.Linear);
                            ef2.AutoDis(T(6));
                            CreateEntity(ef2);
                            ef2.scf2 = true;
                            ef.scf = true;
                            var time = Rand(0,360f);
                            var runx = Rand(0.7f,1.8f) * RandSignal();
                            var runy = Rand(0.7f, 1.8f) * RandSignal();
                            var runrot = Rand(0.7f, 1.8f) * RandSignal();
                            var rot = Rand(5f, 15f) * RandSignal();
                            ForBeat(32, () => 
                            {
                                ef.centre = ef.Centre + new V(Sin((time+Gametime/3f)*runx)*10, Sin((time + Gametime / 3f) * runx) * 10);
                                ef.rot = Sin(time+Gametime/10f)*runrot+rot;
                                ef2.centre = ef.Centre + new V(Sin((time + Gametime / 3f) * runx) * 10, Sin((time + Gametime / 3f) * runx) * 10);
                                ef2.rot = Sin(time + Gametime / 10f)*runrot + rot;
                            });
                            DelayBeat(2, () =>
                            {
                                RunEase(s => ef.Centre = s, LinkEase(EaseIn(T(4),ef.Centre,new V(320,240),ES.Quad)));
                                RunEase(s => rot = s, LinkEase(EaseIn(T(4), rot, MathF.Atan2(ef.Centre.Y-240,ef.Centre.X-320)*180f/MathF.PI, ES.Quad)));
                            });
                            DelayBeat(4, () => WhiteOut(T(2)));
                            DelayBeat(5.99f, () => ef.Alpha=ef2.Alpha=0);
                        }
                        count++;
                    });
                    RegisterFunctionOnce("Ef2", () => 
                    {
                        var str = "黒になれ".ToCharArray();
                        for (int i = 0; i < 4; i++) 
                        {
                            Ef ef = new(str[i].ToString(),new V(320,240), C.Black, 1.5f) { font = Final2 };
                            Ef ef2 = new(str[i].ToString(), new V(320, 240), C.Black, 1.5f) { font = Final2 };
                            CreateEntity(new[] {ef,ef2 });
                            ef.scf = true;
                            ef2.scf2 = true;
                            RunEase(s => ef.Centre = s, EaseOut(T(8), new V(320, 240), new V(320 - 100 * 1.5f + i * 100f,320),ES.Back));
                            var time = Rand(0, 360f);
                            var runx = Rand(0.7f, 1.8f) * RandSignal();
                            var runy = Rand(0.7f, 1.8f) * RandSignal();
                            var runrot = Rand(0.7f, 1.8f) * RandSignal();
                            var rot = Rand(5f, 15f) * RandSignal();
                            RunEase(s => rot = s, EaseOut(T(8), -(i - 2) * 10, 0, ES.Back));
                            ForBeat(100, () => 
                            {
                                ef.centre = ef2.centre= ef.Centre + new V(Sin((time + Gametime / 3f) * runx) * 10, Sin((time + Gametime / 3f) * runx) * 10);
                                ef.rot = ef2.rot = Sin(time + Gametime / 10f) * runrot + rot;

                            });
                            foreach (var t in new[] { ef, ef2 })
                            {
                                t.alphaOut(1, 0, T(8), T(8), ES.Linear);
                                t.AutoDis(T(16.5f));
                            }
                        }
                    });
                    for (int i = 1; i < text.Length+1; i++)
                    {
                        if (i-1 < text.Length - 2)
                            CreateChart(T(4 * i), T(2), 0, new string[]
                            {
                        "Efs"
                            });

                        if (i-1 == text.Length - 2)
                            CreateChart(T(4 * i), T(2), 0, new string[]
                            {
                        "Ef1"
                            });

                        if (i-1 == text.Length - 1)
                            CreateChart(T(4 * i+1.5f), T(2), 0, new string[]
                            {
                        "Ef2"
                            });
                    }
                }
                #endregion

            }

            public void ExtremePlus()
            {
                
                ExtendEffect();
                TextEffect();
                #region Intro undyne
                if (InBeat(0.9f))
                {
                    NameShower.level = 4.ToString();
                    var shader = new ScreenDrawing.Shaders.Blur();
                    shader.Sigma = 0.4f;

                    SceneRendering.InsertProduction(shader);
                    DelayBeat(38.1f, () => { shader.Dispose(); });

                }
                if (InBeat(2f))
                {
                    rainer.Scale = 0.5f;
                    rainer.Intensity = 1;
                    rainer.Speed = 5;



                    for (int i = 0; i < 8; i++)
                    {
                        DelayBeat(i * 4, () =>
                        {
                            Functions.CreateArrow(T(6f), 3, 2f, 0, 0);
                            Functions.CreateArrow(T(6f), 1, 2f, 1, 1);
                            Functions.CreateArrow(T(6f), 3, 2f, 0, 0);
                            Functions.CreateArrow(T(6f), 1, 2f, 1, 1);
                        });
                    }

                }
                if (InBeat(6f))
                {

                    for (int i = 0; i < 3; i++)
                    {
                        DelayBeat(i * 8, () =>
                        {
                            RegisterFunctionOnce("Ef1", () =>
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    DelayBeat(i * 0.5f, () =>
                                    {
                                        Line l = new(
                                            LinkEase(Stable(0, new V(240, 600)), EaseOut(T(7), new V(240, -780), Cu)),
                                            LinkEase(Stable(0, Rand(-15f, 15)), EaseOut(0, 0, Cu)));
                                        CreateEntity(l);
                                        l.Alpha = Rand(0, 100) * 0.01f;
                                        l.DelayDispose(T(7));
                                    });
                                }

                            });
                            CreateChart(T(2), T(2), 6.5f, new string[]
                            {
                            "","","","",   "R","","","",
                            "(R1)","","","",   "R","R1","R","R1",
                            "","","","",   "R1","","","",
                            "R","","","",   "R(R1)","","R(R1)","",

                            });

                        });
                    }
                    DelayBeat(24, () =>
                    {

                        RegisterFunctionOnce("GB", () =>
                        {


                            GreenSoulGB a;
                            GreenSoulGB b;
                            CreateGB(a = new(T(4), 2, 1, T(1)));
                            CreateGB(b = new(T(4), 0, 0, T(1)));
                            DelayBeat(5, () =>
                            {
                                a.Dispose();
                                b.Dispose();
                            });
                            DelayBeat(4, () =>
                            {
                                Line[] get = GetAll<Line>();
                                foreach (Line s in get)
                                {
                                    s.Dispose();
                                }
                            });
                        });

                        RegisterFunctionOnce("Ef2", () =>
                        {
                            float rot = 0;
                            for (int i = 0; i < 7 * 4; i++)
                            {
                                DelayBeat(i * 0.25f, () =>
                                {
                                    Line l = new(LinkEase(Stable(0, new V(0, 0))),
                                         LinkEase(Stable(0, 0), EaseIn(T(2), rot, Cu)));
                                    l.TransverseMirror = true;
                                    CreateEntity(l);
                                    l.AlphaDecrease(T(2));
                                    l.DelayDispose(T(2));
                                    Line l2 = new(LinkEase(Stable(0, new V(0, 480))),
                                         LinkEase(Stable(0, 0), EaseIn(T(2), -rot, Cu)));
                                    l2.TransverseMirror = true;
                                    CreateEntity(l2);
                                    l2.AlphaDecrease(T(2));
                                    l2.DelayDispose(T(2));
                                    Line l3 = new(LinkEase(Stable(0, new V(0, 0))),
                                         LinkEase(Stable(0, 90), EaseIn(T(2), -rot, Cu)));
                                    l3.VerticalMirror = true;
                                    CreateEntity(l3);
                                    l3.AlphaDecrease(T(2));
                                    l3.DelayDispose(T(2));
                                    Line l4 = new(LinkEase(Stable(0, new V(640, 0))),
                                         LinkEase(Stable(0, 90), EaseIn(T(2), rot, Cu)));
                                    l4.VerticalMirror = true;
                                    CreateEntity(l4);
                                    l4.AlphaDecrease(T(2));
                                    l4.DelayDispose(T(2));
                                    rot += 3;
                                });
                            }
                            float t = 0.5f;
                            AddInstance(new TimeRangedEvent(10, () =>
                            {

                                for (int i = 0; i < 32; i++)
                                {
                                    Line l = new(LinkEase(Stable(0, WCentre + GV(400, 360 / 32f * i)), EaseIn(T(7), -GV(350, 350 / 32f * i), Cu)),
                                                 LinkEase(Stable(0, (360 / 32f * i) + 90), EaseIn(T(7), 0, Cu)));
                                    CreateEntity(l);
                                    l.Alpha = t;
                                }
                                t -= 1 / 20f;
                            }));
                        });
                        CreateChart(T(2), T(2), 6.5f, new string[]
                        {
                            "Ef2","","","",   "R","","","",
                            "(R1)","","","",   "R(GB)","R1","R","R1",
                            "","","","",   "R1","","","",
                            "R","","","",   "",

                        });



                    });

                }
                if (InBeat(8))
                {

                    for (int i = 0; i < 8; i++)
                    {

                        DelayBeat(i * 4f, () =>
                        {
                            RunEase((s) => { ScreenDrawing.BackGroundColor = Color.Lerp(Color.White, Color.Black, s); },
                        EaseOut(T(2f), 0, 1f, ES.Quad));
                        });
                    }

                }
                #endregion
                #region BadApple+Sudden changes+dreamBattle+棺桶ダンス
                if (InBeat(39))
                {
                    Shader shader = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\SquareNega"));
                    RenderProduction shaderProduction = ScreenDrawing.ActivateShader(shader, 0.2f);
                    Shaderblack(new V(320, 240), 1000, 500, shader);
                    rainer.Intensity = 0.5f;
                    rainer.Speed = 7f;
                    rainer.Scale = 5f;
                    ScreenDrawing.MasterAlpha = 2;
                    EasingUtil.ValueEasing.EaseBuilder easeBuilder = new EasingUtil.ValueEasing.EaseBuilder();
                    easeBuilder.Insert(T(0.5f), EasingUtil.ValueEasing.EaseOutQuad(0f, 16f, T(0.5f)));
                    easeBuilder.Insert(T(1f), EasingUtil.ValueEasing.EaseInQuad(16f, 0f, T(1f)));
                    easeBuilder.Run(delegate (float s)
                    {
                        splitter.Intensity = s * 2f;
                    });
                    EasingUtil.ValueEasing.EaseBuilder easeBuilder2 = new EasingUtil.ValueEasing.EaseBuilder();
                    easeBuilder2.Insert(0f, EasingUtil.ValueEasing.Stable(1f));
                    easeBuilder2.Insert(T(1f), EasingUtil.ValueEasing.EaseOutQuart(1f, 0.8f, T(1f)));
                    easeBuilder2.Insert(T(1f), EasingUtil.ValueEasing.EaseInQuad(0.8f, 1f, T(1f)));
                    easeBuilder2.Run(delegate (float s)
                    {
                        Functions.ScreenDrawing.ScreenScale = s;
                    });
                    Functions.ScreenDrawing.CameraEffect.Convulse(30f, T(3f), direction: false);
                    EasingUtil.ValueEasing.EaseBuilder easeBuilder3 = new EasingUtil.ValueEasing.EaseBuilder();
                    easeBuilder3.Insert(0f, EasingUtil.ValueEasing.Stable(0f));
                    easeBuilder3.Insert(T(1f), EasingUtil.ValueEasing.EaseOutQuad(0f, 0.8f, T(1f)));
                    easeBuilder3.Insert(T(1f), EasingUtil.ValueEasing.EaseInQuad(0.8f, 0f, T(1f)));
                    easeBuilder3.Run(delegate (float s)
                    {
                        step.Intensity = s;
                    });
                    new EasingUtil.ValueEasing.EaseBuilder().Insert(0f, EasingUtil.ValueEasing.Stable(0f));
                    RenderProduction production = new Functions.ScreenDrawing.Shaders.Filter(blur, 0.99f);
                    Functions.ScreenDrawing.SceneRendering.InsertProduction(production);
                    RenderProduction ps;
                    Functions.ScreenDrawing.SceneRendering.InsertProduction(ps = new Functions.ScreenDrawing.Shaders.Filter(step, 0.1f));
                    splitter = new Functions.ScreenDrawing.Shaders.RGBSplitting(0.1f)
                    {
                        Disturbance = false
                    };
                    splitter.RandomDisturb = 0f;
                    splitter.Intensity = 0f;
                    Functions.ScreenDrawing.SceneRendering.InsertProduction(splitter);

                    FightResources.Shaders.DislocationX.Intensity = 15;
                    FightResources.Shaders.DislocationX.RGBSplitEnabled = true;

                    //ScreenDrawing.ActivateShader(FightResources.Shaders.DislocationX, 0.5f);

                    RunEase((s) => { InstantSetBox(s, 84, 84); InstantTP(s); },
                        EaseIn(T(1f), new V(320, 240), new V(-100, 240), ES.Expo));
                    RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                        EaseIn(T(1f), 0, 1, ES.Expo));
                    DelayBeat(1, () =>
                    {
                        shaderProduction.Dispose();
                        production.Dispose(); splitter.Dispose(); ps.Dispose();
                    });
                }
                if (InBeat(40))
                {
                    RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                       EaseOut(T(1f), 1, 0, ES.Expo));
                    RunEase((s) => { InstantSetBox(s, 200, 200); InstantTP(s); },
                        EaseOut(T(1f), new V(800, 240), new V(320, 240), ES.Expo));
                }
                if (InBeat(40))
                {
                    Functions.Regenerate(1);
                    SetSoul(3);
                    SetBox(240, 200, 200);

                    Shader fat = FightResources.Shaders.DislocationX;
                    RenderProduction pop = ScreenDrawing.ActivateShader(fat, 0.9f);
                    DelayBeat(32, () => { pop.Dispose(); });
                    Functions.CreateEntity(new Boneslab(90f, 50f, 20, (int)T(30), Motions.LengthRoute.sin, new float[4]
                    {
                        35f,
                        75,
                        0f,
                        55f
                    }));
                    for (int i = 0; i < 100 * 32; i++)
                    {

                        DelayBeat(i * 0.01f, () =>
                        {
                            FightResources.Shaders.DislocationX.ChunkHeight = Rand(-25, 25);
                        });
                    }
                    Functions.CreateEntity(new Boneslab(270f, 50f, 20, (int)T(30), Motions.LengthRoute.sin, new float[4]
                    {
                        35f,
                        75,
                        37.5f,
                        55f
                    }));
                    RegisterFunctionOnce("Step1", () =>
                    {
                        EasingUtil.ValueEasing.EaseBuilder easeBuilder = new EasingUtil.ValueEasing.EaseBuilder();
                        easeBuilder.Insert(T(0.5f), EasingUtil.ValueEasing.EaseOutQuad(0f, 16f, T(0.5f)));
                        easeBuilder.Insert(T(1f), EasingUtil.ValueEasing.EaseInQuad(16f, 0f, T(1f)));
                        easeBuilder.Run(delegate (float s)
                        {

                            splitter.Intensity = s * 2;
                        });
                        EasingUtil.ValueEasing.EaseBuilder easeBuilder2 = new EasingUtil.ValueEasing.EaseBuilder();
                        easeBuilder2.Insert(0f, EasingUtil.ValueEasing.Stable(1f));
                        easeBuilder2.Insert(T(1f), EasingUtil.ValueEasing.EaseOutQuart(1f, 0.8f, T(1f)));
                        easeBuilder2.Insert(T(1f), EasingUtil.ValueEasing.EaseInQuad(0.8f, 1f, T(1f)));
                        easeBuilder2.Run(delegate (float s)
                        {
                            Functions.ScreenDrawing.ScreenScale = s;
                        });
                        Functions.ScreenDrawing.CameraEffect.Convulse(30f, T(3f), direction: false);
                        EasingUtil.ValueEasing.EaseBuilder easeBuilder3 = new EasingUtil.ValueEasing.EaseBuilder();
                        easeBuilder3.Insert(0f, EasingUtil.ValueEasing.Stable(0f));
                        easeBuilder3.Insert(T(1f), EasingUtil.ValueEasing.EaseOutQuad(0f, 1f, T(1f)));
                        easeBuilder3.Insert(T(1f), EasingUtil.ValueEasing.EaseInQuad(1f, 0f, T(1f)));
                        easeBuilder3.Run(delegate (float s)
                        {
                            step.Intensity = s;
                        });
                        new EasingUtil.ValueEasing.EaseBuilder().Insert(0f, EasingUtil.ValueEasing.Stable(0f));
                        RenderProduction production = new Functions.ScreenDrawing.Shaders.Filter(blur, 0.99f);
                        Functions.ScreenDrawing.SceneRendering.InsertProduction(production);
                        RenderProduction pro;
                        Functions.ScreenDrawing.SceneRendering.InsertProduction(pro = new Functions.ScreenDrawing.Shaders.Filter(step, 0.98f));
                        splitter = new Functions.ScreenDrawing.Shaders.RGBSplitting(0.97f)
                        {
                            Disturbance = false
                        };

                        DelayBeat(4, () =>
                        {
                            production.Dispose(); splitter.RandomDisturb = 0f;
                            splitter.Intensity = 0; splitter.Dispose(); pro.Dispose();
                        });
                    });

                    RegisterFunctionOnce("Click1", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        EaseOut(T(1f), 0, 25f, ES.Cubic));
                        DelayBeat(1f, () =>
                        {
                            RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            EaseOut(T(1f), 25, 0f, ES.Cubic));
                        });
                    });
                    RegisterFunctionOnce("Click2", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        EaseOut(T(1f), 0, -25f, ES.Cubic));
                        DelayBeat(1f, () =>
                        {
                            RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            EaseOut(T(1f), -25, 0f, ES.Cubic));
                        });
                    });
                    RegisterFunctionOnce("Click3", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; },
                        EaseOut(T(1f), 1, 1.5f, ES.Back));
                        DelayBeat(1f, () =>
                        {
                            RunEase((s) => { ScreenDrawing.ScreenScale = s; },
                            EaseIn(T(1f), 1.5f, 1f, ES.Back));
                        });
                    });
                    RegisterFunctionOnce("GB1", () => { CreateGB(new NormalGB(Functions.Heart.Centre+GV(Rand(50f,200f),Rand(-70f,70f)+(RandBool()?0:180)), new V(), new V(1f, 0.3f), T(2f), T(0.5f))); });
                    RegisterFunctionOnce("GB2", () =>
                    {
                        float a = 0;
                        for (int i = 0; i < 4; i++)
                        {

                            DelayBeat(i * 0.25f, () =>
                            {

                                CreateGB(new NormalGB(new V(440, a * 30 + 140), new V(640, 480), new V(1, 0.5f), 180f, T(2f), T(1f)));
                                CreateGB(new NormalGB(new V(200, a * 30 + 140), new V(640, 480), new V(1, 0.5f), 0f, T(2f), T(1f)));
                                a++;
                            });
                        }
                    });
                    RegisterFunctionOnce("GB3", () =>
                    {
                        float a = 0;
                        for (int i = 0; i < 4; i++)
                        {

                            DelayBeat(i * 0.25f, () =>
                            {

                                CreateGB(new NormalGB(new V(440, a * -30 + 340), new V(640, 480), new V(1, 0.5f), 180f, T(2f), T(1f)));
                                CreateGB(new NormalGB(new V(200, a * -30 + 340), new V(640, 480), new V(1, 0.5f), 0f, T(2f), T(1f)));
                                a++;
                            });
                        }
                    });
                    RegisterFunction("BoxUp", () =>
                    {
                        RunEase((s) => { InstantSetBox(s, 200, 200); },
                        EaseIn(T(4f), new V(320, 240), new V(320, -200), ES.Back));
                    });
                    float rotate = 27.5f;

                    RegisterFunctionOnce("b2", () => { rotate = rotate + 23.4f; });
                    RegisterFunctionOnce("Bone", () =>
                    {
                        float f = Rand(0, 50);
                        V s = new V(200, 100);
                        CreateEntity(new FireWorker(s, 50, 15, T(2), Color.Gold));
                        DelayBeat(2f, () =>
                        {

                            Bone A;
                            PlaySound(Sounds.pierce);

                            var 青骨2 = 6;
                            var startrotate = Rand(0, 360);
                            for (int i2 = 0; i2 < 青骨2; i2++)
                            {
                                CreateBone(A = new CustomBone(s, Motions.PositionRoute.linear, 0 - 360 / 青骨2 * i2 - rotate, 25, T(32))
                                { PositionRouteParam = new float[] { Sin((360 / 青骨2) * i2 + rotate) * 4, Cos((360 / 青骨2) * i2 + rotate) * 4 }, ColorType = 1, });
                                A.IsMasked = false;
                            }



                        });

                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        float f = Rand(0, 50);
                        V s = new V(440, 100);
                        CreateEntity(new FireWorker(s, 50, 15, T(2), Color.Gold));
                        DelayBeat(2f, () =>
                        {

                            Bone A;
                            PlaySound(Sounds.pierce);

                            var 青骨2 = 6;
                            var startrotate = Rand(0, 360);
                            for (int i2 = 0; i2 < 青骨2; i2++)
                            {
                                CreateBone(A = new CustomBone(s, Motions.PositionRoute.linear, 0 - 360 / 青骨2 * i2 - rotate, 25, T(32))
                                { PositionRouteParam = new float[] { Sin((360 / 青骨2) * i2 + rotate) * 4, Cos((360 / 青骨2) * i2 + rotate) * 4 }, ColorType = 1, });
                                A.IsMasked = false;
                            }



                        });

                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        float f = Rand(0, 50);
                        V s = new V(200, 380);
                        CreateEntity(new FireWorker(s, 50, 15, T(2), Color.Gold));
                        DelayBeat(2f, () =>
                        {

                            Bone A;
                            PlaySound(Sounds.pierce);

                            var 青骨2 = 6;
                            var startrotate = Rand(0, 360);
                            for (int i2 = 0; i2 < 青骨2; i2++)
                            {
                                CreateBone(A = new CustomBone(s, Motions.PositionRoute.linear, 0 - 360 / 青骨2 * i2 - rotate, 25, T(32))
                                { PositionRouteParam = new float[] { Sin((360 / 青骨2) * i2 + rotate) * 4, Cos((360 / 青骨2) * i2 + rotate) * 4 }, ColorType = 1, });
                                A.IsMasked = false;
                            }



                        });

                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        float f = Rand(0, 50);
                        V s = new V(440, 380);
                        CreateEntity(new FireWorker(s, 50, 15, T(2), Color.Gold));
                        DelayBeat(2f, () =>
                        {

                            Bone A;
                            PlaySound(Sounds.pierce);

                            var 青骨2 = 6;
                            var startrotate = Rand(0, 360);
                            for (int i2 = 0; i2 < 青骨2; i2++)
                            {
                                CreateBone(A = new CustomBone(s, Motions.PositionRoute.linear, 0 - 360 / 青骨2 * i2 - rotate, 25, T(32))
                                { PositionRouteParam = new float[] { Sin((360 / 青骨2) * i2 + rotate) * 4, Cos((360 / 青骨2) * i2 + rotate) * 4 }, ColorType = 1, });
                                A.IsMasked = false;
                            }



                        });

                    });
                    for (int i = 0; i < 3; i++)
                    {
                        CentreCircleBone boneA;
                        CreateBone(boneA = new CentreCircleBone(i * 60, 4, 2000, T(30)));
                        boneA.ColorType = 2;
                        boneA.IsMasked = true;
                    }

                    CreateChart(T(0), T(2), 6.5f, new string[] {
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB2(Click3)", "", "", "",
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB1", "", "GB1", "",
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB3(Click3)", "", "", "",
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB1", "", "GB1", "",
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB2(Click3)", "", "", "",
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB1", "", "GB1", "",
                            "GB1(Bone)(Bone2)(b2)(Click1)", "", "", "",  "GB1", "", "", "",
                            "GB1(Bone3)(Bone4)(Click2)", "", "", "",  "GB3(Click3)", "", "", "",
                            "GB1(BoxUp)", "", "", "",  "", "", "", "",
                            "(Step1)", "", "", "",  "","","","",
                            });
                }
                #endregion
                #region BadApple+Etermal spring dream+freedomdive+universal
                if (InBeat(72))
                {
                    FightResources.Shaders.DislocationX.Intensity = 0;
                    HeartAttribute.MaxHP = 54;
                    HeartAttribute.KR = true;
                    HeartAttribute.KRDamage = 1;
                    HeartAttribute.PurpleLineCount = 5;
                    HeartAttribute.Speed = 4;
                    Functions.Heart.EnabledRedShield = true;
                    SetSoul(4);

                    V x = new V();

                    RunEase((s) => { x = s; },
                    EaseOut(T(4f), new V(600, -300), new V(300, 240), ES.Cubic));
                    V SinX = new V();
                    float S = 0;
                    ForBeat(28, () =>
                    {
                        S += 0.2f;
                        InstantSetBox(x + SinX, 300, 150);
                        SinX = new V(Cos(S * 4) * 40f, Cos(S * 6) * 50f);
                    });



                    CreateChart(T(0), T(2), 6.5f, new string[] {
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            "$011","","$111","", "$211", "", "$311", "",
                            });


                }
                if (InBeat(72, 100) && At0thBeat(1.5f))
                {
                    Spider.LineSpider l = new(Rand(1, 5), false, 5);
                    CreateEntity(l);
                    DelayBeat(0.5f, () =>
                    {
                        Spider.LineSpider l = new(Rand(1, 5), true, 3.4f);
                        CreateEntity(l);
                    });
                }
                if (InBeat(72, 99) && At0thBeat(2))
                {
                    bool v = false;
                    switch (Rand(0, 3))
                    {
                        case 0:
                            CreateEntity(new DownBone(v, 4, 30 * 2));
                            CreateEntity(new UpBone(v, 4, 30 * 1));
                            break;
                        case 1:
                            CreateEntity(new DownBone(v, 4, 30 * 1));
                            CreateEntity(new UpBone(v, 4, 30 * 2));
                            break;
                        case 2:
                            CreateEntity(new UpBone(v, 4, 30 * 3));
                            break;
                        case 3:
                            CreateEntity(new DownBone(v, 4, 30 * 3));
                            break;
                    }
                    PlaySound(Sounds.pierce);
                    DelayBeat(1, () =>
                    {
                        bool v = true;
                        switch (Rand(0, 3))
                        {
                            case 0:
                                CreateEntity(new DownBone(v, 4, 30 * 2));
                                CreateEntity(new UpBone(v, 4, 30 * 1));
                                break;
                            case 1:
                                CreateEntity(new DownBone(v, 4, 30 * 1));
                                CreateEntity(new UpBone(v, 4, 30 * 2));
                                break;
                            case 2:
                                CreateEntity(new UpBone(v, 4, 30 * 3));
                                break;
                            case 3:
                                CreateEntity(new DownBone(v, 4, 30 * 3));
                                break;
                        }
                        PlaySound(Sounds.pierce);
                    });
                }
                #endregion            
                #region 回想1(最後の抵抗+棺桶ダンス)
                if (InBeat(100))
                {

                    RunEase((s) => { ScreenDrawing.MasterAlpha = s; BoundColor = C.Lerp(C.Black, C.MediumPurple, s); },
                    EaseOut(T(2f), 1f, 0f, ES.Cubic));
                }
                if (InBeat(104))
                {
                    NameShower.level = 4.ToString();
                    rgb.Dispose();
                    DisposeBarrage();
                    foreach (Spider l in GetAll<Spider>())
                    {
                        l.Dispose();
                    }
                    foreach (Line l in GetAll<Line>())
                    {
                        l.Dispose();
                    }
                    foreach (EfEn l in GetAll<EfEn>())
                    {
                        l.Dispose();
                    }
                    // when you need to dispose:
                    // shaderProduction.Dispose();

                    rainer.Dispose();

                    BoundColor = CW;
                    var glitch = new ScreenDrawing.Shaders.Glitching(0.834572f);
                    var blur = new Blur(0.9735f);
                    glitch.Duration = new V(3,3);
                    glitch.AverageInterval = 0;
                    glitch.Intensity = 1280;
                    glitch.AverageDelta = 1.2f;
                    glitch.BlockScale = 0.5f;
                    blur.Sigma = 1.6f;
                    float sin = 270f;
                    SceneRendering.InsertProduction(glitch);
                    SceneRendering.InsertProduction(blur);
                    DelayBeat(30, () =>
                    {
                        glitch.Dispose();
                        blur.Dispose();
                    });
                    Functions.Heart.GiveForce(0f, 0f);
                    Functions.SetSoul(1);
                    float sin3 = 0f;
                    DrawingUtil.LerpScreenScale(bpm * 4f, 1.4f, 0.075f);
                    Functions.InstantSetBox(new V(-50f, 240f + Functions.Sin(sin3) * 40f + Functions.Sin(sin3 * 0.5f) * 20f), 84f, 84f);
                    Functions.InstantTP(new V(-50f, 240f + Functions.Sin(sin3) * 40f + Functions.Sin(sin3 * 0.5f) * 20f));
                    ForBeat(16f, delegate
                    {
                        sin3 += 360f / bpm / 4f;
                        Functions.InstantSetBox(new V(Functions.BoxStates.Centre.X + 1.5f, 240f + Functions.Sin(sin3) * 40f + Functions.Sin(sin3 * 0.5f) * 20f), 84f, 84f);
                        Functions.InstantTP(new V(Functions.Heart.Centre.X + 1.5f, 240f + Functions.Sin(sin3) * 40f + Functions.Sin(sin3 * 0.5f) * 20f));
                    });
                    ForBeat(16f, delegate
                    {
                        sin += 1;
                        Functions.ScreenDrawing.DownBoundDistance = 62f + Functions.Sin(sin) * 60f;
                        Functions.ScreenDrawing.UpBoundDistance = 62f + Functions.Sin(sin) * 60f;
                        if (AtKthBeat(0.1f, 0f))
                        {
                            Functions.CreateEntity(new Particle(new Color(195, 31, 255), new V(Functions.Rand(-0.1f, 0.1f), Functions.Rand(7, 9)), Functions.Rand(14, 19), new V(Functions.Rand(0, 640), 0f), FightResources.Sprites.square)
                            {
                                AutoRotate = true,
                                Rotation = Functions.Rand(0, 359),
                                SlowLerp = 0.015f,
                                DarkingSpeed = Functions.Rand(3.2f, 4.1f)
                            });
                            Functions.CreateEntity(new Particle(new Color(195, 31, 255), new V(Functions.Rand(-0.1f, 0.1f), -Functions.Rand(7, 9)), Functions.Rand(14, 19), new V(Functions.Rand(0, 640), 480f), FightResources.Sprites.square)
                            {
                                AutoRotate = true,
                                Rotation = Functions.Rand(0, 359),
                                SlowLerp = 0.015f,
                                DarkingSpeed = Functions.Rand(3.2f, 4.1f)
                            });
                        }
                    });
                    RunEase((s) => { ScreenDrawing.MasterAlpha = s; Functions.ScreenDrawing.BoundColor = Color.White * s; },
                    EaseOut(T(4f), 0f, 0.25f, ES.Cubic));
                    DelayBeat(12f, () =>
                    {
                        RunEase((s) => { ScreenDrawing.MasterAlpha = s; Functions.ScreenDrawing.BoundColor = Color.White * s; },
                    EaseOut(T(4f), 0.25f, 0f, ES.Cubic));
                    });
                }
                if (InBeat(120))
                {
                    ScreenDrawing.ScreenScale = 1;
                    Functions.Heart.Speed = 0;
                    RunEase((s) => { ScreenDrawing.MasterAlpha = s; },
                   EaseOut(T(4f), 0f, 0.25f, ES.Cubic));
                    Functions.InstantSetBox(240f, 90f, 48f);
                    SetSoul(0);
                    Functions.Heart.EnabledRedShield = false;
                    NameShower.level = 2.ToString();
                    Functions.ScreenDrawing.ThemeColor = Color.Brown;
                    for (int i = 0; i < 6; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            Functions.ScreenDrawing.CameraEffect.Convulse(T(1f), Functions.RandBool());
                        });
                    }
                    DelayBeat(10, () =>
                    {
                        RunEase((s) => { ScreenDrawing.MasterAlpha = s; },
                        EaseOut(T(4f), 0.25f, 0f, ES.Cubic));
                    });
                }
                if (InBeat(120.4f)) { InstantTP(320f, 240f); }
                #endregion         
                #region EtherStrike igallta Lostmemory Underfell
                if (InBeat(134f))
                {
                    NameShower.level = 5.ToString();

                    RegisterFunctionOnce("角度変化", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-65, 65);
                    });
                    CreateChart(T(0), T(2), 6.5f, new string[] {
                    "!!3", "($202)($012)(角度変化)", "($102)($312)(角度変化)", "($302)($112)(角度変化)", "!!3", "($202)($112)(角度変化)", "($102)($012)(角度変化)", "($002)($312)(角度変化)",
                    "R1","","R1","",  "R1","","R1","",
                    "(R1)(+011)","","","",  "R1","","R1","",
                    "(R1)(+011)","","","",  "R1","","","",
                    "R1","","R1","",  "R1","","R1","",
                    "R1","","R1","",  "R1","","R1","",
                    "(R1)(+011)","","","",  "R1","","R1","",
                    "$11","","$11","",  "$31","","($11)(+011)","",
                    "$31","","$11","",  "$31","","($31)(+011)","",
                    "$11","","$31","",  "$11","","$11","",
                    "($31)(+011)","","","",  "$11","","$11","",
                    "($31)(+011)","","","",  "$11","","","",
                    "$11","","$31","",  "$11","","($11)(+011)","",
                    "$31","","$31","",  "$11","","$11","",
                    "($31)(+011)","","","",  "$11","","$31","",
                    "($31)(+011)","","","",  "($11)(+011)","","","",
                    "($31)(+011)","","","",  "($11)(+011)","","","",});
                }
                if (InBeat(134))
                {
                    ScreenDrawing.MasterAlpha = 1;

                    Functions.ScreenDrawing.ThemeColor = Color.White;
                    InstantSetGreenBox();
                    SetSoul(1);
                    InstantTP(320, 240);
                }
                if (InBeat(136))
                {
                    ScreenDrawing.HPBar.HPExistColor = Color.Yellow;
                    ScreenDrawing.HPBar.HPLoseColor = Color.Red;
                    HeartAttribute.MaxHP = 7;
                    HeartAttribute.KR = false;
                    Functions.Heart.Speed = 2;

                    ScreenDrawing.ScreenAngle = 0;
                    Heart key1;
                    Heart key2;
                    key1 = Functions.Heart;
                    key2 = CreateHeart(240, 84, 84);
                    SetPlayerBoxMission(key1);
                    TP(new V(220, 240));
                    SetBox(new V(220, 240), 84, 84);

                    SetPlayerBoxMission(key2);
                    SetBox(new V(420, 240), 84, 84);

                    SetSoul(0);
                    for (int i = 0; i < 5; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            int num6 = Functions.Rand(1, 4);
                            if (num6 == 1)
                            {
                                Functions.CreateEntity(new Boneslab(0f, 50f, 45f, 15f));
                                Functions.CreateEntity(new Boneslab(90f, 50f, 45f, 15f));
                            }

                            if (num6 == 2)
                            {
                                Functions.CreateEntity(new Boneslab(90f, 50f, 45f, 15f));
                                Functions.CreateEntity(new Boneslab(180f, 50f, 45f, 15f));
                            }

                            if (num6 == 3)
                            {
                                Functions.CreateEntity(new Boneslab(180f, 50f, 45f, 15f));
                                Functions.CreateEntity(new Boneslab(270f, 50f, 45f, 15f));
                            }

                            if (num6 == 4)
                            {
                                Functions.CreateEntity(new Boneslab(270f, 50f, 45f, 15f));
                                Functions.CreateEntity(new Boneslab(0f, 50f, 45f, 15f));
                            }
                        });
                    }
                    DelayBeat(10, () => 
                    {
                        Functions.CreateEntity(new Boneslab(0f, 50f, 45f, 15f));
                        Functions.CreateEntity(new Boneslab(90f, 50f, 45f, 15f));
                    });
                    float a3 = 0f;


                    DrawLine.NormalLine l9 = new DrawLine.NormalLine(-20f, 240f, 660f, 240f, 60f, 0.55f, Color.White, 0.99f);
                    DrawLine.NormalLine l18 = new DrawLine.NormalLine(-20f, 240f, 660f, 240f, 60f, 0.55f, Color.White, 0.99f);
                    DrawLine.Clock c1 = new DrawLine.Clock(320f, 240f, 0f, 1432.5f, 0.99f, 92f, 12f, Color.Gray);
                    DrawLine.Clock c2 = new DrawLine.Clock(320f, 240f, 100f, 1432.5f, 0.99f, 70f, 12f, Color.Gray);
                    Functions.CreateEntity(c1);
                    Functions.CreateEntity(c2);
                    Functions.CreateEntity(l9);
                    Functions.CreateEntity(l18);
                    Functions.AddInstance(new TimeRangedEvent(0f, T(14), delegate
                    {

                        l9.alpha -= 0.01f;
                        l18.alpha -= 0.01f;
                        l9.y1 -= 2.75f;
                        l18.y1 += 2.75f;
                        l9.y2 -= 2.75f;
                        l18.y2 += 2.75f;

                        c1.rotate -= a3 * -0.2f;
                        c2.rotate -= a3 * -0.07f;
                        a3 -= 0.01f;
                    }));
                    Functions.AddInstance(new TimeRangedEvent(T(14), T(16), delegate
                    {

                        l9.alpha -= 0.01f;
                        l18.alpha -= 0.01f;
                        l9.y1 -= 2.75f;
                        l18.y1 += 2.75f;
                        l9.y2 -= 2.75f;
                        l18.y2 += 2.75f;

                        c1.rotate += a3 * -0.2f;
                        c2.rotate += a3 * -0.07f;
                        a3 -= 0.01f;
                    }
                    ));
                    Functions.AddInstance(new TimeRangedEvent(T(30), 72.5f, delegate
                    {
                        c1.alpha -= 0.0137f;
                        c2.alpha -= 0.0137f;
                    }));
                    if (Functions.GametimeF == 2847.5f)
                    {
                        c1.Dispose();
                        c2.Dispose();
                    }
                    DelayBeat(11.5f, () =>
                    {
                        WhiteOut(T(0.75f));

                    });
                    DelayBeat(10, () =>
                    {
                        RunEase((s) =>
                        {
                            SetPlayerBoxMission(key1); InstantSetBox(s, 84, 84); InstantTP(s);
                        },
                        LinkEase(
                                 Stable(0,new V(220,240)),
                                 EaseIn(T(2), new V(100, 0), Cu),
                                 EaseOut(T(2),new V(-100,0),Cu)));
                        RunEase((s) => { SetPlayerBoxMission(key2); InstantSetBox(s,84,84); },
                        LinkEase(Stable(0,new V(420,240)),
                            EaseIn(T(2f), new V(-100,0), Cu),
                            EaseOut(T(2),new V(100,0),Cu)));
                    });
                    DelayBeat(12, () =>
                    {
                        RunEase((s) =>
                        {
                            SetPlayerBoxMission(key1); key1.RotateTo(s); ScreenDrawing.ScreenAngle = s;
                        },
                            EaseOut(T(4f), 0, 540, ES.Back));
                        shader1 = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\squareNega"));
                        foreach (Line l in GetAll<Line>()) 
                        {
                            l.Dispose();
                        }
                        foreach (EfEn l in GetAll<EfEn>())
                        {
                            l.Dispose();
                        }
                        shaderProduction1 = ScreenDrawing.ActivateShader(shader1, 0.88f);
                        shaderProduction2 = ScreenDrawing.ActivateShader(FightResources.Shaders.Gray, 0.92f);
                        FightResources.Shaders.Gray.Intensity = 1;
                        Shaderblack(V.Zero, 0, 0, 0, shader1);
                        RunEase((s) => { ScreenDrawing.MasterAlpha = s; },
                        EaseOut(T(0.5f), 0f, 1f, ES.Cubic));
                    });
                    DelayBeat(14, () =>
                    {
                        RunEase((s) => { Shaderblack(new V(640, 320), s, 400, 0, shader1); }, LinkEase(Stable(0, 0), EaseOut(T(1f), 1280, ES.Cubic)));
                    });
                    DelayBeat(16, () =>
                    {

                        DelayBeat(14, () =>
                        {
                            RunEase((s) => { ShaderblackT2(s, shader1); },
                            LinkEase(Stable(0, 0), EaseOut(T(4f), 320, ES.Elastic)));
                        });
                        RunEase((s) => { Shaderblack(s, 480, shader1); },
                        LinkEase(Stable(0, 0), EaseOut(T(0f), 640, ES.Cubic),
                                          Stable(T(10), 0), EaseOut(T(4f), -640, ES.Cubic),

                                          Stable(0, 0), EaseOut(T(4f), 0, ES.Cubic),
                                          Stable(0, 0), EaseOut(T(4f), 0, ES.Cubic)));
                        float rot = 0;
                        for (int i = 0; i < 1800; i++)
                        {
                            DelayBeat(i * 0.01f, () =>
                        {
                            rot += 1800f / 2160;
                            Shaderblack(Sin(rot) * 5, shader1);
                        });
                        }
                        RunEase((s) => { Shaderblack(s, shader1); },
                         LinkEase(Stable(0, new V(320, 240)), EaseOut(T(4f), new V(320, 0), ES.Cubic),
                                  Stable(0, new V()), EaseOut(T(4f), new V(-640, 0), ES.Cubic),
                                  Stable(0, new V()), EaseOut(T(4f), new V(320, 0), ES.Cubic),
                                  Stable(0, new V()), EaseOut(T(4f), new V(0, 0), ES.Cubic)));
                        DelayBeat(16.5f, () =>
                        { shaderProduction1.Dispose(); shaderProduction2.Dispose(); });

                        PlaySound(Sounds.spearAppear);
                        SetPlayerBoxMission(key2);

                        Functions.CreateSpear(new Pike(new V(420 - 64f, 240f), 0f, T(15f))
                        {
                            IsHidden = true
                        });
                        Functions.CreateSpear(new Pike(new V(420 + 64f, 240f), 180f, T(15f))
                        {
                            IsHidden = true
                        });
                        Functions.CreateSpear(new Pike(new V(420f, 176f), 90f, T(15f))
                        {
                            IsHidden = true
                        });
                        Functions.CreateSpear(new Pike(new V(420f, 304f), 270f, T(15f))
                        {
                            IsHidden = true
                        });
                        for (int i = 0; i < 2; i++)
                        {
                            Functions.CreateSpear(new Pike(new V(420 - 60f, 210 + i * 15), 0f, T(14 + i * 0.25f))
                            {
                                IsHidden = true
                            });
                            Functions.CreateSpear(new Pike(new V(420 - 30 + i * 15, 180f), 90f, T(14 + i * 0.25f))
                            {
                                IsHidden = true
                            });
                            Functions.CreateSpear(new Pike(new V(420 + 60, 210 + i * 15), 180f, T(14 + i * 0.25f))
                            {
                                IsHidden = true
                            });
                            Functions.CreateSpear(new Pike(new V(420 - 30 + i * 15, 300f), 270f, T(14 + i * 0.25f))
                            {
                                IsHidden = true
                            });
                        }

                        for (int j = 0; j < 2; j++)
                        {
                            Functions.CreateSpear(new Pike(new V(420 + 30 - j * 15, 180f), 90f, T(14 + j * 0.25f))
                            {
                                IsHidden = true
                            });
                            Functions.CreateSpear(new Pike(new V(420 + 30 - j * 15, 300f), 270f, T(14 + j * 0.25f))
                            {
                                IsHidden = true
                            });
                            Functions.CreateSpear(new Pike(new V(360f, 270 - j * 15), 0f, T(14 + j * 0.25f))
                            {
                                IsHidden = true
                            });
                            Functions.CreateSpear(new Pike(new V(420 + 60f, 270 - j * 15), 180f, T(14 + j * 0.25f))
                            {
                                IsHidden = true
                            });
                        }

                        DelayBeat(16.5f, () =>
                         {

                             ScreenDrawing.ScreenAngle = 0;

                             SetPlayerBoxMission(key2);
                             key2.Dispose();


                         });

                        DelayBeat(16.5000001f, () =>
                        {
                            SetPlayerBoxMission(key1);
                            Functions.Heart.RotateTo(0);

                            InstantTP(new V(320, 240)); Functions.Heart.RotateTo(0);
                            InstantSetBox(310f, 280f, 160f);
                            Functions.Heart.Speed = 4;
                        });
                    });

                    DelayBeat(18, () =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            PlaySound(Sounds.spearAppear);
                            switch (Functions.Rand(0, 3))
                            {
                                case 0:
                                    Functions.CreateSpear(new Pike(new V(420 - 75f, 240f), 0f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(420 - 75f, 220f), 0f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                                case 2:
                                    Functions.CreateSpear(new Pike(new V(420 + 75f, 240f), 180f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(420 + 75f, 260f), 180f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                                case 1:
                                    Functions.CreateSpear(new Pike(new V(420f, 215 - 50f), 90f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(440f, 215 - 50f), 90f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                                case 3:
                                    Functions.CreateSpear(new Pike(new V(420f, 315f), 270f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(400f, 315f), 270f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                            }

                            switch ((Functions.LastRand + 1) % 4)
                            {
                                case 0:
                                    Functions.CreateSpear(new Pike(new V(420 - 75f, 240f), 0f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(420 - 75f, 220f), 0f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                                case 2:
                                    Functions.CreateSpear(new Pike(new V(420 + 75f, 240f), 180f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(420 + 75f, 260f), 180f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                                case 1:
                                    Functions.CreateSpear(new Pike(new V(420f, 215 - 50f), 90f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(440f, 215 - 50f), 90f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                                case 3:
                                    Functions.CreateSpear(new Pike(new V(420f, 315f), 270f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    Functions.CreateSpear(new Pike(new V(400f, 315f), 270f, T(2f))
                                    {
                                        IsHidden = true,
                                        DrawingColor = Color.Red
                                    });
                                    break;
                            }
                        });
                    }
                });

                }
                #endregion
                #region Conflict+GOODTEK+Suddenchange+Night of Knights
                if (InBeat(166.5f)) { WhiteOut(T(2)); }
                if (InBeat(168, 168 + 7) && At0thBeat(1))
                {
                    for (int i = -1; i < 6; i++)
                    {
                        for (int i2 = -1; i2 < 8; i2++)
                        {
                            Line l = new(LinkEase(Stable(0, new V(320 - (120 * 4) + i * 120, i2 * 120)), EaseOut(T(1), new V(0, -120), Li)),
                                         LinkEase(Stable(0, 45), EaseOut(T(2), 0, Cu)))
                            { DrawingColor = C.Lerp(C.White, C.Black, 0.5f),Depth=0 };
                            Line l2 = new(LinkEase(Stable(0, new V(320 - (120 * 4) + i * 120, i2 * 120)), EaseOut(T(1), new V(0, -120), Li)),
                                         LinkEase(Stable(0, -45), EaseOut(T(2), 0, Cu)))
                            { DrawingColor = C.Lerp(C.White, C.Black, 0.5f), Depth = 0 };
                            CreateEntity(l);
                            CreateEntity(l2);
                            l.DelayDispose(T(1));
                            l2.DelayDispose(T(1));
                        }
                    }
                }
                if (InBeat(168 + 8))
                {
                    NameShower.level = 4.ToString();
                    for (int i = -1; i < 9; i++)
                    {
                        for (int i2 = -1; i2 < 5; i2++)
                        {
                            Line l = new(LinkEase(Stable(0, new V(320 - (120 * 4) + i * 120, i2 * 120)), EaseOut(T(4), new V(0, -120), Cu)),
                                          LinkEase(Stable(0, 45), EaseOut(T(2), 0, Cu)))
                            { DrawingColor = C.Lerp(C.White, C.Black, 0.5f), Depth = 0 };
                            Line l2 = new(LinkEase(Stable(0, new V(320 - (120 * 4) + i * 120, i2 * 120)), EaseOut(T(4), new V(0, -120), Cu)),
                                         LinkEase(Stable(0, -45), EaseOut(T(2), 0, Cu)))
                            { DrawingColor = C.Lerp(C.White, C.Black, 0.5f), Depth = 0 };
                            RunEase((s) => { l.DrawingColor = l2.DrawingColor = C.Lerp(C.White, C.Black, s); },
                                LinkEase(Stable(0, 0), EaseOut(T(8f), 1, ES.Cubic)));
                            RunEase((s) => { l.Width = l2.Width = s; },
                                LinkEase(Stable(0, 3), EaseOut(T(1f), 15, ES.Cubic)),
                                LinkEase(Stable(0, 0), EaseOut(T(1f), -18, ES.Cubic)));
                            CreateEntity(l);
                            CreateEntity(l2);
                            l.DelayDispose(T(4));
                            l2.DelayDispose(T(4));
                        }
                    }
                }
                if (InBeat(168))
                {

                    Ef[] ef = GetAll<Ef>();
                    foreach (Ef ef1 in ef)
                    {
                        ef1.Dispose();
                    }
                    NameShower.level = 4.ToString();
                    SetSoul(0);
                    RemoveUI1();
                    RemoveUI2();
                    InstantSetBox(310f, 280f, 160f);
                    InstantTP(150, 300);
                    for (int i = 0; i < 4; i++)
                    {
                        DelayBeat(i * 2f, () =>
                        {
                            V vec = new V(Functions.Heart.Centre.X, 150);
                            CreateGB(new NormalGB(vec, vec, new(1, 1f), T(2), T(0.5f)));
                        });
                        DelayBeat(i * 2f + 1, () =>
                        {
                            V vec = new V(100, Functions.Heart.Centre.Y);
                            CreateGB(new NormalGB(vec, vec, new(1, 1f), T(2), T(0.5f)));
                            V vec2 = new V(540, Functions.Heart.Centre.Y);
                            CreateGB(new NormalGB(vec2, vec2, new(1, 1f), T(2), T(0.5f)));
                        });
                    }
                    Functions.Heart.Speed = 3;
                    for (int i = 0; i < 2; i++)
                    {
                        float cur = game.T(i * 4);
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.T(7 / 4f), game.T(3 / 4f), () =>
                            {
                                bone.Speed -= 10.6f / game.T(3 / 4f);
                                bone.MissionLength += 78 / game.T(3 / 4f);
                            }));
                            AddInstance(new InstantEvent(game.T(2), () =>
                            {
                                bone.ColorType = 1;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.T(7 / 4f), game.T(3 / 4f), () =>
                            {
                                bone.Speed -= 10.6f / game.T(3 / 4f);
                                bone.MissionLength += 78 / game.T(3 / 4f);
                            }));
                            AddInstance(new InstantEvent(game.T(2), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));

                    }

                }
                if (InBeat(168 + 8))
                {
                    InstantSetBox(new V(320, 296), 226, 138);
                    SetSoul(2);
                    Functions.Heart.GiveForce(0, 9);
                    Functions.Heart.Speed = 3;
                }
                if (InBeat(168 + 8, 182) && At0thBeat(2))
                {
                    NameShower.level = 3.ToString();
                    Functions.Heart.GiveForce(RandBool() ? 0 : 180, 15);

                    if (Rand(0, 1) == 0)
                    {
                        float Aspeed = 0;
                        float R = Rand(0, 360);
                        UpBone bone1 = new(true, 0.9f, Rand(24, 64));
                        DownBone bone2 = new(true, 0.9f, 128 - LastRand - 40);
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(0, 1, () =>
                            {
                                CreateBone(bone1);
                                CreateBone(bone2);

                            }));
                        }
                        AddInstance(new TimeRangedEvent(0, T(8), () =>
                        {
                            bone1.MissionLength = Sin(R + Aspeed) * 28 + 40;
                            bone2.MissionLength = Sin(-R - Aspeed) * 28 + 40;
                            Aspeed += 4.5f;
                        }));
                        AddInstance(new InstantEvent(T(1), () => { CreateBone(new UpBone(true, 3.2f, 131) { ColorType = 1 }); }));
                        PlaySound(Sounds.pierce);
                    }
                    else
                    {
                        float Aspeed = 0;
                        float R = Rand(0, 360);
                        UpBone bone1 = new(false, 0.9f, Rand(24, 64));
                        DownBone bone2 = new(false, 0.9f, 128 - LastRand - 40);
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(a * 3, 1, () =>
                            {
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                        }
                        AddInstance(new TimeRangedEvent(0, T(8), () =>
                        {
                            bone1.MissionLength = Sin(R + Aspeed) * 28 + 40;
                            bone2.MissionLength = Sin(-R - Aspeed) * 28 + 40;
                            Aspeed += 4.5f;
                        }));
                        AddInstance(new InstantEvent(T(1), () => { CreateBone(new UpBone(false, 3.2f, 131) { ColorType = 1 }); }));
                        PlaySound(Sounds.pierce);
                    }
                }
                if (InBeat(186, 190)&&At0thBeat(3)) 
                {
                    CreateEntity(new UpBone(false, 3, 50) {ColorType=1 });
                    DelayBeat(1.5f,()=> CreateEntity(new DownBone(false, 3, 50) {ColorType=1 }));
                }
                if (InBeat(184, 192) && At0thBeat(1))
                {
                    PlaySound(Sounds.pierce);
                    CreateGB(new NormalGB(new V(Rand(320+140,320-140f),400), new V(LastRand, 400),new V(1f,0.5f),-90,T(2),T(0.5f)));
                }
                if (InBeat(192,198)&&At0thBeat(2)) 
                {
                    CreateEntity(new GunBullet(Functions.Heart.Centre, T(1), new float[] { 0, 180 }));
                    CreateEntity(new GunBullet(Functions.Heart.Centre, T(1), new float[] { 90, 270 }));
                    CreateEntity(new GunBullet(Functions.Heart.Centre, T(1.5f), new float[] { 45, 225 }));
                    CreateEntity(new GunBullet(Functions.Heart.Centre, T(1.5f), new float[] { 135, 315 }));
                }
                if (InBeat(184))
                {
                    RunEase((s) => 
                    { 
                        ScreenDrawing.ThemeColor = C.Lerp(C.White, C.Red, s); 
                        if(s!=0) foreach (var t in GetAll<Boneslab>()) t.UpdateColor(); 
                    }, LinkEase(Stable(T(8), 0), EaseOut(T(2), 0, 1, Cu)));
                    Bone[] b1 = GetAll<UpBone>();
                    foreach (Bone bs in b1) { bs.ColorType = 2; }
                    Bone[] b2 = GetAll<DownBone>();
                    foreach (Bone bs in b2) { bs.ColorType = 2; }
                    RunEase((s) =>
                    {
                        Bone[] b1 = GetAll<UpBone>();
                        foreach (Bone bs in b1) { bs.Alpha = s; }
                    }, LinkEase(Stable(0, 1), EaseOut(T(2), 0, Cu)));
                    RunEase((s) =>
                    {
                        Bone[] b2 = GetAll<DownBone>();
                        foreach (Bone bs in b2) { bs.Alpha = s; }
                    }, LinkEase(Stable(0, 1), EaseOut(T(2), 0, Cu)));
                    
                    float s = 90;
                    RunEase((s) => { InstantSetBox(s, 280, 105); },
                        LinkEase(EaseOut(T(4),240,180,ES.Sine)));
                    AddInstance(new TimeRangedEvent(T(8),T(6), () =>
                    {
                        s +=  180/T(3f);
                        InstantSetBox(240 + Sin(s) * -60, 280, 105);
                    }));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                    EaseOut(T(2f), 0f, 180f, ES.Cubic));
                    SetSoul(3);
                    DelayBeat(2, () =>
                    {
                        CreateEntity(new Boneslab(0, 12, 15, T(14)));
                        CreateEntity(new Boneslab(180, 12, 15, T(14)));

                    });

                    SetBox(240f, 280f, 105f);
                    Functions.Heart.Speed = 3;
                    DelayBeat(14, () =>
                    {
                        for (int i = 0; i < 10; i++)
                        {

                            SetBox(240, 200, 200);
                            CreateEntity(new GunBullet(new V(320 + Sin(i * 36) * 150, 240 + Cos(i * 36) * 150), T(2), new float[] { i * 36 + 90, i * 36 - 90 }));
                        }
                    });
                    DelayBeat(16, () =>
                    {
                        Boneslab[] bs = GetAll<Boneslab>();
                        foreach (Boneslab b in bs) { b.Dispose(); }
                        Bone[] bs2 = GetAll<Bone>();
                        foreach (Boneslab b3 in bs) { b3.Dispose(); }
                    });

                }
                #endregion
                #region Transcendence+sustenance of moon
                if (InBeat(198f))
                {


                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = new V(0, s); },
                    EaseIn(T(2f), 0f, -1000f, ES.Back));

                }
                if (InBeat(200f))
                {
                    NameShower.level = 4.ToString();
                    splitter = new RGBSplitting(0.92345f)
                    {
                        Disturbance = false,
                        Intensity = 1.0f,
                        RandomDisturb = 0
                    };
                    ScreenDrawing.SceneRendering.InsertProduction(splitter);
                    //splitter.SpriteSortMode = SpriteSortMode.Texture;

                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), T(1f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), T(1f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new V(42, 42), new V(88 + t * 44, 88 + t * 44), T(1f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new V(42, 42), new V(88 + t * 44, 88 + t * 44), T(1f)), rot2);
                                Line line = new(CentreEasing.Combine(new V(320, 240), ease1), CentreEasing.Combine(new V(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(T(1f), ValueEasing.EaseOutQuad(p, 0, T(1)));
                                DelayBeat(1, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);

                            }
                        }
                    });
                    RegisterFunctionOnce("shakeX", () =>
                    {

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(T(1), ValueEasing.EaseOutQuad(5.0f, 0.6f, T(1)));
                        builder2.Run(s => splitter.Intensity = s);

                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(T(1), ValueEasing.EaseOutCubic(-12, 0, T(1)));
                        builder3.Run(s => ScreenPositionDelta = new(0, s));
                    });
                    RegisterFunction("shakeY", () =>
                    {

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(T(1), ValueEasing.EaseOutQuad(5.0f, 0.6f, T(1)));
                        builder2.Run(s => splitter.Intensity = s);

                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(T(1), ValueEasing.EaseOutCubic(12, 0, T(1)));
                        builder3.Run(s => ScreenPositionDelta = new(0, s));
                    });
                    ThemeColor = Color.White;
                    SideLerpEase(C.Gray, T(4));
                    Arrow.UnitEasing easing = new();
                    AddInstance(easing);
                    easing.ApplyTime = T(2);
                    easing.PositionEase = LinkEase(Stable(0, new V(0, 600)), EaseOut(T(3), new V(0, -600), El));
                    easing.TagApply("A");
                    CreateChart(T(0), T(2), 6.5f, new string[] {
                        "$1($31)(SpreadN)", "", "$30($11)", "",  "$10($31)","","$3($11)","",
                        "R1(R1)(R)(SpreadN)","","+2","+2",  "R1(R1)","","R1(R1)","",
                        "R(R1)(SpreadN)","","R(R1)","",  "R(R1)","","R(R1)","",
                        "R1(R1)(R)(SpreadN)","","+2","+2",  "R(R1)", "", "R(R1)", "",
                        "R1(R)(SpreadN)","","R(R1)","",  "R1(R)","","R(R1)","",
                        "R1(R1)(R)(SpreadN)","","+2","+2",  "R(R1)", "", "R(R1)", "",
                        "R1(R)(SpreadN)","","R(R1)","",  "R1(R)","","R(R1)","",
                        "R1(R1)(R)(SpreadN)","","+2","+2",  "R(R1)", "", "R(R1)", "",

                        "($0@A)($2@A)(R1)(shakeX)","","R1(R)","",  "($0@A)($2@A)(R1)(shakeX)","","R(R1)","",
                        "($0@A)($2@A)($01@A)($21@A)","","+2","+2",  "R1(R)","","R1(R)","",
                        "($01@A)($21@A)(R)(shakeY)","","R1(R)","",  "($01@A)($21@A)(R)(shakeY)","","R(R1)","",
                        "($0@A)($2@A)($01@A)($21@A)","","+2","+2",  "R1(R)","","R1(R)","",
                        "($0@A)($2@A)(R1)(shakeX)","","R1(R)","",  "($0@A)($2@A)(R1)(shakeX)","","R(R1)","",
                        "($0@A)($2@A)($01@A)($21@A)","","+2","+2",  "R1(R)","","R1(R)","",
                        "($0@A)($2@A)(R1)(shakeY)","","R1(R)","",  "($01@A)($21@A)(R)(shakeY)","","R(R1)","",
                        "($0@A)($2@A)($01@A)($21@A)",

                    });
                }
                if (InBeat(200))
                {

                    BackGroundColor = Color.DimGray * 0.35f;
                    CreateEntity(rainer = new Rainer());
                    rainer.Intensity = 1;
                    rainer.Speed = 7f;
                    rainer.Scale = 2;
                    HPBar.HPExistColor = Color.Silver;
                    HPBar.HPLoseColor = Color.Gray;
                    HeartAttribute.BuffedLevel = 7;
                    HeartAttribute.MaxHP = 10;
                    ScreenDrawing.ScreenAngle = 0;
                    ScreenScale = 1;
                    InstantSetGreenBox();
                    SetSoul(1);
                    Functions.Heart.RotateTo(0);
                    InstantTP(320, 240);
                    RunEase((s) => { ScreenPositionDelta = s; FightResources.Shaders.StepSample.CentreY = 240 + s.Y; }, LinkEase(Stable(0, new V(0, -1000)), EaseOut(T(2f), new V(0, 1000), ES.Back)));
                    RegisterFunctionOnce("C1", () =>
                    {
                        RunEase((s) => { ScreenPositionDelta = new V(s, 0); FightResources.Shaders.StepSample.CentreX = 320 + s; },
                            LinkEase(Stable(0, ScreenPositionDelta.X), EaseOut(T(2f), 20, ES.Back)));
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, 5), EaseOut(T(2f), -5, ES.Back)));
                    });
                    RegisterFunctionOnce("C2", () =>
                    {
                        RunEase((s) => { ScreenPositionDelta = new V(s, 0); FightResources.Shaders.StepSample.CentreX = 320 + s; },
                            LinkEase(Stable(0, ScreenPositionDelta.X), EaseOut(T(2f), -40, ES.Back)));
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, -5), EaseOut(T(2f), 5, ES.Back)));
                    });
                    RegisterFunctionOnce("C3", () =>
                    {
                        RunEase((s) => { ScreenPositionDelta = new V(s, 0); FightResources.Shaders.StepSample.CentreX = 320 + s; },
                            LinkEase(Stable(0, ScreenPositionDelta.X), EaseOut(T(2f), 20, ES.Back)));
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, 5), EaseOut(T(2f), -5, ES.Back)));
                    });
                    RegisterFunctionOnce("C4", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, ScreenDrawing.ScreenAngle), EaseOut(T(4f), -5, ES.Cubic)));
                    });
                    RegisterFunctionOnce("C5", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, ScreenDrawing.ScreenAngle), EaseOut(T(4f), 10, ES.Cubic)));
                    });
                    RegisterFunctionOnce("C6", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, ScreenDrawing.ScreenAngle), EaseOut(T(4f), -10, ES.Cubic)));
                    });
                    RegisterFunctionOnce("C8", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, ScreenDrawing.ScreenAngle), EaseOut(T(4f), 5, ES.Cubic)));
                    });
                    CreateChart(T(0), T(2), 0, new string[]
                    {
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "C1","","","",  "","","","",
                        "","","","",  "","","","",
                        "C2","","","",  "","","","",
                        "","","","",  "","","","",
                        "C3","","","",  "","","","",
                        "","","","",  "","","","",
                        "C4","","","",  "","","","",
                        "","","","",  "","","","",
                        "C5","","","", "","","","",
                        "","","","",  "","","","",
                        "C6","","","",  "","","","",
                        "","","","",  "","","","",
                        "C8","","","", "","","","",
                    });
                }
                if (InBeat(200.1f))
                {
                    BoundColor = C.Silver * 0.55f;
                    RunEase((s) =>
                    {
                        ScreenDrawing.DownBoundDistance = s;
                        ScreenDrawing.UpBoundDistance = s;
                    },
                            EaseOut(T(8f), 0f, 140f, ES.Cubic));

                    production1 = ScreenDrawing.ActivateShader(step, 0.643342f);
                    DelayBeat(2, () =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            DelayBeat(i * 4, () =>
                            {

                                RunEase((s) => { BackGroundColor = C.Lerp(C.White, C.DimGray * 0.35f, s); },
                                EaseOut(T(2f), 0, 1f, ES.Cubic));
                                RunEase((s) => { ScreenScale = s; },
                                EaseOut(T(2f), 1.2f, 1f, ES.Cubic));
                                RunEase((s) => { splitter.Intensity = s; },
                                    LinkEase(Stable(0, 4), EaseOut(T(1f), -3f, ES.Cubic)));
                                RunEase((s) => { step.Intensity = s; },
                                    LinkEase(Stable(0, 0.2f), EaseOut(T(1f), -0.2f, ES.Cubic)));
                            });
                        }
                    });


                }
                #endregion
                #region Creeper+GOORRAGE+Resurrection
                if (InBeat(230f))
                {
                    SideLerpEase(Color.Black, T(2));
                    RunEase((s) =>
                    {
                        ScreenDrawing.DownBoundDistance = s;
                        ScreenDrawing.UpBoundDistance = s;
                    },

                               EaseOut(T(2f), 150, 0f, ES.Cubic));
                    RunEase((s) => { MasterAlpha = s; },
                                EaseIn(T(2f), 1, 0f, ES.Cubic));
                    RunEase((s) => { ScreenScale = s; },
                                EaseIn(T(2f), 1, 0f, ES.Cubic));
                    ValueEasing.EaseBuilder t = new();
                    t.Insert(T(2),EaseOut(T(6f), ScreenDrawing.ScreenAngle, 360 * -5, ES.Cubic));
                    t.Run(s=>ScreenDrawing.ScreenAngle=s);
                }
                if (InBeat(232))
                {
                    NameShower.level = 4.ToString();
                    if (splitter != null)
                    {
                        splitter.Dispose();
                        production1.Dispose();
                    }
                    rainer.Intensity = 0;
                    rainer.Speed = 7000f;
                    rainer.Scale = 2000;
                    BackGroundColor = Color.Black;
                    RenderProduction de = ScreenDrawing.ActivateShader(FightResources.Shaders.Mosaic, 0.5f);
                    DelayBeat(32, () => { de.Dispose(); });
                    FightResources.Shaders.Blur.Sigma = -100;
                    RunEase((s) => { FightResources.Shaders.Mosaic.MosiacSize = new V(s, s); },
                                EaseIn(T(16f), 0, 30f, ES.Cubic));

                    HPBar.HPExistColor = Color.Yellow;
                    HPBar.HPLoseColor = Color.Red;
                    HeartAttribute.BuffedLevel = 0;
                    HeartAttribute.MaxHP = 20;
                    Functions.HeartAttribute.DamageTaken = 6;
                    RunEase((s) => { MasterAlpha = s; },
                                EaseIn(T(2f), 0, 1f, ES.Cubic));
                    RunEase((s) => { ScreenScale = s; },
                                EaseOut(T(5f), 0, 1f, ES.Back));


                    Player.Heart main = Functions.Heart;
                    Player.Heart extra = Functions.CreateHeart(240f, 84f, 84f);
                    RegisterFunctionOnce("Return", delegate
                    {
                        Functions.SetPlayerBoxMission(main);
                        Functions.SetBox(new V(320f, 140f), 220f, 140f);
                        Functions.TP(320f, 80f);
                        Functions.SetSoul(2);
                        Functions.Heart.JumpSpeed = 6.5f;
                        Functions.Heart.Gravity = 7;
                        Functions.Heart.JumpTimeLimit = 2;
                        Functions.SetPlayerBoxMission(extra);
                        Functions.SetBox(new V(320f, 340f), 220f, 140f);
                        Functions.TP(320f, 80f);
                        Functions.SetSoul(2);
                        Functions.Heart.JumpSpeed = 6.5f;
                        Functions.Heart.GiveForce(180f, 0f);
                        Functions.Heart.Gravity = 7;
                        Functions.Heart.JumpTimeLimit = 2;
                    });
                    RegisterFunctionOnce("Box", delegate
                    {
                        Platform[] all2 = Functions.GetAll<Platform>();
                        for (int j = 0; j < all2.Length; j++)
                        {
                            int num19 = j;
                            all2[num19].Dispose();
                        }

                        EasingUtil.CentreEasing.EaseBuilder easeBuilder5 = new EasingUtil.CentreEasing.EaseBuilder();
                        easeBuilder5.Insert(0f, EasingUtil.CentreEasing.Stable(320f, 140f));
                        easeBuilder5.Insert(T(15f) - 1f, EasingUtil.CentreEasing.Circle(new V(0f, 0f), 110f, T(8f), 270f));
                        easeBuilder5.Run(delegate (V s)
                        {
                            Functions.SetPlayerBoxMission(main);
                            Functions.InstantSetBox(s, 220f, 140f);
                            Functions.InstantTP(s.X, Functions.Heart.Centre.Y);
                        });
                        EasingUtil.CentreEasing.EaseBuilder easeBuilder6 = new EasingUtil.CentreEasing.EaseBuilder();
                        easeBuilder6.Insert(0f, EasingUtil.CentreEasing.Stable(320f, 340f));
                        easeBuilder6.Insert(T(15f) - 1f, EasingUtil.CentreEasing.Circle(new V(0f, 0f), 110f, T(8f), 450f));
                        easeBuilder6.Run(delegate (V s)
                        {
                            Functions.SetPlayerBoxMission(extra);
                            Functions.InstantSetBox(s, 220f, 140f);
                            Functions.InstantTP(s.X, Functions.Heart.Centre.Y);
                        });
                    });
                    RegisterFunctionOnce("Bone1", delegate
                    {
                        Functions.SetPlayerBoxMission(main);
                        Functions.PlaySound(FightResources.Sounds.pierce);
                        Functions.CreateBone(new DownBone(way: true, 6f, 42f));
                        Functions.CreateBone(new DownBone(way: false, 6f, 42f));
                        Functions.CreateBone(new DownBone(way: true, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                        Functions.CreateBone(new DownBone(way: false, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                        Functions.SetPlayerBoxMission(extra);
                        Functions.CreateBone(new UpBone(way: true, 6f, 42f));
                        Functions.CreateBone(new UpBone(way: false, 6f, 42f));
                        Functions.CreateBone(new DownBone(way: true, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                        Functions.CreateBone(new DownBone(way: false, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                    });
                    RegisterFunctionOnce("Bone2", delegate
                    {
                        Functions.SetPlayerBoxMission(extra);
                        Functions.PlaySound(FightResources.Sounds.pierce);
                        Functions.CreateBone(new DownBone(way: true, 6f, 42f));
                        Functions.CreateBone(new DownBone(way: false, 6f, 42f));
                        Functions.CreateBone(new DownBone(way: true, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                        Functions.CreateBone(new DownBone(way: false, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                        Functions.SetPlayerBoxMission(main);
                        Functions.CreateBone(new UpBone(way: true, 6f, 42f));
                        Functions.CreateBone(new UpBone(way: false, 6f, 42f));
                        Functions.CreateBone(new DownBone(way: true, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                        Functions.CreateBone(new DownBone(way: false, 4.5f, 140f)
                        {
                            ColorType = 2f
                        });
                    });
                    CreateChart(T(0), T(2), 6.5f, new string[] {
                        "Box(Return)", "","","",  "","","","",
                        "(Bone1)","","","",  "","","","",
                        "(Bone1)","","","",  "","","","",
                        "(Bone1)","","","",  "","","","",
                        "(Bone1)","","","",  "","","","",
                        "(Bone1)","","","",  "","","","",
                        "(Bone1)","","","",  "","","","",
                        "(Bone1)","","","",  "","","","",


                    });
                    DelayBeat(14f, () =>
                    {
                        float time = 0;
                        for (int i = 0; i <= 4; i++)
                            AddInstance(new InstantEvent(T(2 * i), () =>
                            {

                                List<SideBone> bones = new();
                                bones.Add(new LeftBone(false, 10f, 70) { ColorType = 2 });
                                bones.Add(new LeftBone(true, 10f, 70) { ColorType = 2 });
                                bones.Add(new UpBone(false, 10f, 70) { ColorType = 2 });
                                bones.Add(new UpBone(true, 10f, 70) { ColorType = 2 });
                                bones.Add(new RightBone(false, 10f, 70) { ColorType = 2 });
                                bones.Add(new RightBone(true, 10f, 70) { ColorType = 2 });
                                bones.Add(new DownBone(false, 10f, 70) { ColorType = 2 });
                                bones.Add(new DownBone(true, 10f, 70) { ColorType = 2 });
                                float intensity = 0.315f;
                                AddInstance(new TimeRangedEvent(11, 5, () =>
                                {
                                    bones.ForEach(s => s.Speed -= intensity);
                                    intensity += 0.151f;
                                })
                                { UpdateIn120 = true });
                                AddInstance(new TimeRangedEvent(T(1), 15, () =>
                                {
                                    bones.ForEach(s => s.Speed += 0.4f);
                                })
                                { UpdateIn120 = true });
                                SetPlayerBoxMission(main);
                                bones.ForEach(s => CreateBone(s));

                                if (time > 0) { PlaySound(Sounds.pierce); }
                                time++;
                            }));
                    });
                    DelayBeat(16, () =>
                    {
                        SetPlayerBoxMission(extra);
                        InstantSetBox(9999, 0, 0);
                        extra.Dispose();
                        SetPlayerBoxMission(main);
                        SetSoul(0);
                        SetBox(new V(320, 240), 160, 160);
                        Functions.Heart.Speed = 3.25f;
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i * 2, () =>
                            {
                                CreateGB(new NormalGB(new V(Rand(320 - 120, 320 + 120), Rand(320 - 120, 320 + 120)), new V(Rand(320 - 120, 320 + 120), Rand(320 - 120, 320 + 120)),
                                new V(1f, 0.5f), T(2), T(1)));
                            });
                        }
                        CentreCircleBone a, b, c;
                        CustomBone a2, b2, c2;
                        a2 = new CustomBone(new V(320, 240), (s) => { return V.Zero; }, 45, 75) { ColorType = 1 };
                        b2 = new CustomBone(new V(320, 240), (s) => { return V.Zero; }, 135, 75) { ColorType = 2 };
                        c2 = new CustomBone(new V(320, 240), (s) => { return V.Zero; }, 45, 96) { ColorType = 0 };
                        CreateBone(a = new CentreCircleBone(45, 720 / T(16), 240, T(16)) { IsMasked = true, ColorType = 1 });
                        CreateBone(b = new CentreCircleBone(135, 720 / T(16), 240, T(16)) { IsMasked = true });
                        CreateBone(c = new CentreCircleBone(45, 720 / T(16), 240, T(16)) { IsMasked = true, ColorType = 1 });


                        DelayBeat(8, () =>
                        {
                            RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 1), EaseOut(T(4f), 1.5f, ES.Cubic)));
                            RunEase((s) => { ScreenPositionDelta = s; },
                        LinkEase(Stable(0, ScreenPositionDelta), EaseOut(T(1f), -ScreenPositionDelta, ES.Cubic)));
                            a.Kill();
                            b.Kill();
                            c.Kill();
                            CreateBone(a2);
                            CreateBone(b2);
                            CreateBone(c2);
                            RunEase((s) => { a2.Rotation = s; }, LinkEase(Stable(0, 45), EaseOut(T(8f), 2 * T(8), ES.Linear)));
                            RunEase((s) => { b2.Rotation = s; }, LinkEase(Stable(0, 135), EaseOut(T(8f), 2 * T(8), ES.Linear)));
                            RunEase((s) => { c2.Rotation = s; }, LinkEase(Stable(0, 45), EaseOut(T(8f), -2 * T(8), ES.Linear)));
                            RunEase((s) => { c2.Length = s; }, LinkEase(Stable(0, 240), EaseOut(T(2f), 96 - 240, ES.Cubic)));
                            RunEase((s) => { a2.Length = s; b2.Length = s; }, LinkEase(Stable(0, 240), EaseOut(T(2f), 75 - 240, ES.Cubic)));
                            PlaySound(Sounds.Ding);

                            for (int i = 0; i < 12; i++) CreateBone(new SideCircleBone(i * 360f / 12, 2.8f, 45, T(8)) { ColorType = 0 });
                        });
                        DelayBeat(16, () =>
                        {
                            Bone[] bs = GetAll<SideCircleBone>();
                            Bone[] bs2 = { a2, b2, c2 };
                            foreach (Bone f in bs) { f.Dispose(); }
                            foreach (Bone f in bs2) { f.Dispose(); }
                        });
                    });
                }
                if (InBeat(232f)) { WhiteOut(1); }
                if (InBeat(232))
                {
                    Filter filter;
                    ScreenDrawing.SceneRendering.InsertProduction(filter = new Filter(FightResources.Shaders.Mosaic, 0.99f));
                    RunEase((s) => { FightResources.Shaders.Mosaic.MosiacSize = new V(s, s); },
                                    Stable(T(15),0.5f),EaseOut(T(1f), 0, 70f, ES.Cubic));
                    DelayBeat(16, () =>
                    {

                        RunEase((s) => { FightResources.Shaders.Mosaic.MosiacSize = new(s, s); },
                                    EaseOut(T(2f), 30, 0.1f, ES.Cubic));


                    });


                    DelayBeat(32, () => { filter.Dispose(); });
                }
                #endregion
                #region Sustenance of moon
                if (InBeat(260)) { SceneOut(Color.Black, T(4)); }
                if (InBeat(262))
                {

                    for (int i = 0; i < 7; i++)
                    {
                        DelayBeat(i * 4, () =>
                        {
                            SetPlayerBoxMission(0);
                            int A = Rand(0, 3);
                            int B = Rand(0, 3);

                            CreateGB(new GreenSoulGB(T(4), A, 0, T(2)));
                            CreateGB(new GreenSoulGB(T(4), B, 1, T(2)));
                            CreateGB(new GreenSoulGB(T(4), A, 0, T(2)));
                            CreateGB(new GreenSoulGB(T(4), B, 1, T(2)));
                            DelayBeat(0, () =>
                            {

                                CreateArrow(T(4), B, 4, 1, 0);
                                CreateArrow(T(4), A, 4, 0, 0);
                            });
                            DelayBeat(4, () =>
                            {

                                Arrow[] arrows = GetAll<Arrow>();
                                foreach (Arrow ar in arrows) { ar.Kill(); }
                            });

                        });
                    }
                }
                if (InBeat(264))
                {
                    HeartAttribute.MaxHP = 5;
                    Regenerate(5);
                    HeartAttribute.DamageTaken = 1;
                    var st = new ScreenDrawing.Shaders.Blur(0.95f);
                    SceneRendering.InsertProduction(st);
                    st.Sigma = 1;
                    RunEase((s) => { MasterAlpha = s; }, LinkEase(Stable(0, 0), EaseOut(T(2f), 1, ES.Cubic)));

                    RenderProduction rend = ActivateShader(FightResources.Shaders.Gray, 0.9237583f);
                    FightResources.Shaders.Gray.Intensity = 1;
                    Lighting shader = new(0.343f); shader.LightingMode = Lighting.LightMode.Limit;
                    Lighting.Light l = new() { size = 400, color = Color.White, position = new(320, 240) };
                    shader.Lights.Add(l);
                    SceneRendering.InsertProduction(shader);
                    DelayBeat(64, () =>
                    {
                        shader.Dispose();
                        st.Dispose();
                        rend.Dispose();
                    });
                    ScreenScale = 1.25f;
                    Heart K1 = Functions.Heart; BoxStates.CurrentBox.InstantSetAlpha(1);
                    DelayBeat(28, () =>
                    {

                        RunEase((s) => { SetPlayerBoxMission(K1); InstantSetBox(s, 84, 84); InstantTP(320, s); },
                        EaseIn(T(4f), 240f, 600f, ES.Back));
                    });
                    InstantSetBox(240, 84, 84);
                    InstantTP(320, 240);
                    float x = 0;
                    SetSoul(1);
                    AddInstance(new TimeRangedEvent(T(64), () =>
                    {
                        x++;
                        V b = new V(Sin(x * 0.7f) * 10, Cos(x * 0.8f) * 10);
                        ScreenDrawing.ScreenAngle = Sin(x * 0.5f) * -20;
                        ScreenPositionDelta = b;
                        step.CentreX = 320 + b.X;
                        step.CentreY = 240 + b.Y;
                        st.Sigma = 1.25f + Cos((x * 90) / T(4)) * 0.25f;
                    }));

                    for (int i = 0; i < 7; i++)
                    {
                        DelayBeat(i * 4, () =>
                    {

                        Heart key;
                        key = CreateHeart(240, 84, 84);
                        key.Depth = 0.2f;
                        float sin = Rand(0, 360);


                        RunEase((s) =>
                        {
                            SetPlayerBoxMission(key);
                            InstantSetBox(s, 84, 84); InstantTP(s);
                        },
                        EaseIn(T(2f), new V(320 + Cos(sin) * 300, 240 + Sin(sin) * 300), new V(320, 240), Cu));


                        RunEase((s) =>
                        {
                            SetPlayerBoxMission(key);
                            BoxStates.CurrentBox.InstantSetAlpha(s);
                            key.Alpha = s;
                        },
                        EaseIn(T(2f), 0f, 1f, Cu));

                        DelayBeat(1.999f, () =>
                        {
                            SetPlayerBoxMission(key);
                            InstantSetBox(9999, 0, 0); key.Dispose();

                        });
                    });
                    }

                    DelayBeat(4 * 7, () =>
                    {

                        SetPlayerBoxMission(0);
                        InstantSetBox(240, 84, 84);
                        InstantTP(320, 240);
                    });

                    // when you need to dispose:
                    // shaderProduction.Dispose();

                }
                if (InBeat(266))
                {
                    RenderProduction rend = ScreenDrawing.ActivateShader(FightResources.Shaders.StepSample, 0.9f);
                    for (int i = 0; i < 7; i++)
                    {
                        
                        DelayBeat(i * 4, () =>
                        {

                            RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                            EaseOut(T(3f), 0.75f, 0f, ES.Cubic));
                        });
                        if (i != 0)
                            for (int i2 = 1; i2 <= 2; i2++)
                                DelayBeat(4*i-i2, () => 
                                {
                                    RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                                    EaseOut(T(1f), 0.1f, 0f, ES.Cubic));
                                });
                    }
                    DelayBeat(30, () => { rend.Dispose(); });
                }
                if (InBeat(296))
                {
                    //Regenerate(2);
                    RegisterFunctionOnce("CS", () =>
                    {
                        Spear spear1 = new CircleSpear(Functions.Heart.Centre, 4f, 1.3f, 146, Gametime);
                        Spear spear2 = new CircleSpear(Functions.Heart.Centre, 4f, 1.3f, 146, Gametime + 60);
                        Spear spear3 = new CircleSpear(Functions.Heart.Centre, 4f, 1.3f, 146, Gametime + 120);
                        Spear spear4 = new CircleSpear(Functions.Heart.Centre, 4f, 1.3f, 146, Gametime + 180);
                        Spear spear5 = new CircleSpear(Functions.Heart.Centre, 4f, 1.3f, 146, Gametime + 240);
                        Spear spear6 = new CircleSpear(Functions.Heart.Centre, 4f, 1.3f, 146, Gametime + 300);
                        PlaySound(Sounds.spearAppear);
                        PlaySound(Sounds.spearAppear);
                        CreateSpear(spear1);
                        CreateSpear(spear2);
                        CreateSpear(spear3);
                        CreateSpear(spear4);
                        CreateSpear(spear5);
                        CreateSpear(spear6);
                    });
                    RegisterFunctionOnce("RS", () =>
                    {
                        int R1 = Rand(0, 360);
                        PlaySound(Sounds.spearAppear);
                        Spear spear1 = new NormalSpear(Functions.Heart.Centre + new V(Cos(R1) * 216, Sin(R1) * 216)) { IsMute = true };
                        CreateSpear(spear1);
                    });
                    CreateChart(0, T(2), 0, new string[]
                    {
                        "RS","","","",  "","","","",
                        "CS","","","",  "","","","",
                        "RS","","","",  "","","","",
                        "CS","","","",  "","","","",
                        "RS","","","",  "","","","",
                        "CS","","","",  "","","","",
                        "RS","","","",  "","","","",
                        "CS","","","",  "","","","",
                        "RS","","","",  "","","","",
                        "CS","","","",  "","","","",
                        "RS","","","",  "","","","",
                        "CS","","","",  "","","","",
                    });
                }
                if (InBeat(296))
                {


                    Functions.Heart.Speed = 4;
                    SetSoul(0);
                    float e = 0;
                    V vec = new();
                    RunEase((s) => { vec = s; InstantTP(s); }, LinkEase(Stable(0, new V()), EaseOut(T(2f), new V(320, 240), ES.Cubic)));
                    AddInstance(new TimeRangedEvent(T(32), () =>
                    {
                        e += T(32) / 1080f;
                        InstantSetBox(new V(vec.X + Cos(e * 2) * 50, vec.Y + Sin(e * 1.3f) * 50), 150, 150);
                    }));
                    RenderProduction ppp = ScreenDrawing.ActivateShader(FightResources.Shaders.StepSample, 0.933333f);
                    for (int i = 0; i < 7; i++)
                    {
                        DelayBeat(i * 4, () =>
                        {



                            DelayBeat(2f, () =>
                            {

                                RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                            EaseOut(T(3f), 0.5f, 0f, ES.Cubic));
                                for (int k = 0; k < 355; k += 90)
                                {
                                    Functions.CreateBone(new SideCircleBone(k, 2.8f, 140f, T(1f)));
                                }
                            });

                        });
                    }
                    DelayBeat(64, () => { ppp.Dispose(); });

                }
                if (InBeat(324f))
                {
                    WhiteOut(T(4));
                }
                #endregion
                #region BIG SHOT+Helltaker
                if (InBeat(328))
                {
                    foreach (Line i in GetAll<Line>())
                    {
                        i.Dispose();
                    }

                    ScreenScale = 1; ScreenPositionDelta = new();
                    InstantTP(WCentre);
                    InstantSetBox(320 - 120, 650, 240 - 60, 240 + 60);
                    SetSoul(Souls.YellowSoul);
                    Functions.Heart.Speed = 2.5f;
                    Functions.Heart.RotateTo(-90);
                }
                if (InBeat(327))
                {
                    HeartAttribute.MaxHP = 32;
                    HeartAttribute.KR = true;
                    HeartAttribute.DamageTaken = 1;
                    HeartAttribute.KRDamage = 2;
                    RegisterFunctionOnce("SetSoul", () =>
                    {
                        InstantTP(WCentre);
                        SetSoul(Souls.YellowSoul);
                        SetBox(320 - 120, 710, 240 - 60, 240 + 60);
                        Functions.Heart.RotateTo(270);
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        //-70, 320 + 120, 240 - 60, 240 + 60
                        SetSoul(Souls.YellowSoul);
                        RunEase((s) => { BoxStates.Centre = s; },
                            LinkEase(Stable(0, BoxStates.Centre), EaseOut(T(4), new V(-270,0), ES.Cubic)));
                        Functions.Heart.RotateTo(90);
                    });

                    var val = EaseIn(T(1), new V(-80, 0), ES.Quad);
                    float spd = -5;
                    RegisterFunctionOnce("Block1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Block5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))) { AbleLink = false });
                    });
                    RegisterFunctionOnce("Bomb2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    RegisterFunctionOnce("Bomb5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 + 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 24)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240)), val, InfLinear(new V(spd, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(650, 240 + 48)), val, InfLinear(new V(spd, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(650, 240 - 48)), val, InfLinear(new V(spd, 0)))));
                    });
                    var vals = EaseIn(T(1), new V(80, 0), ES.Quad);
                    float spds = 5;
                    RegisterFunctionOnce("sBlock1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBlock5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ToughBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))) { AbleLink = false });
                    });
                    RegisterFunctionOnce("sBomb2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
                    });
                    RegisterFunctionOnce("sBomb5", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 + 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 24)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240)), vals, InfLinear(new V(spds, 0)))));
                        CreateEntity(new Bomb(20, LinkEase(Stable(0, new V(-10, 240 + 48)), vals, InfLinear(new V(spds, 0)))) { AbleLink = false });
                        CreateEntity(new ShotableBlock(LinkEase(Stable(0, new V(-10, 240 - 48)), vals, InfLinear(new V(spds, 0)))));
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
                    RegisterFunctionOnce("GetAll", () =>
                    {
                        Bomb[] b = GetAll<Bomb>();
                        ToughBlock[] b2 = GetAll<ToughBlock>();
                        ShotableBlock[] b3 = GetAll<ShotableBlock>();
                        RunEase((s) =>
                        {
                            foreach (Bomb r in b) { r.Alpha = s; }
                            foreach (ToughBlock r in b2) { r.Alpha = s; }
                            foreach (ShotableBlock r in b3) { r.Alpha = s; }
                        }, LinkEase(Stable(0, 1), EaseOut(T(2f), -1, ES.Cubic)));

                        DelayBeat(2, () =>
                        {
                            foreach (Bomb r in b) { r.Dispose(); }
                            foreach (ToughBlock r in b2) { r.Dispose(); }
                            foreach (ShotableBlock r in b3) { r.Dispose(); }
                        });
                    });
                    Vector3 rand1 = blockpos();
                    Vector3 rand2 = blockpos();
                    Vector3 rand3 = blockpos();
                    Vector3 rand4 = blockpos();
                    Vector3 rand5 = blockpos();
                    Vector3 rand6 = blockpos();
                    Vector3 rand7 = blockpos();
                    Vector3 rand8 = blockpos();
                    for (int i = 0; i < 9; i++)
                    {
                        DelayBeat(i * 2, () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            CreateBone(new CustomBone(new V(Functions.Heart.Centre.X - 550, Functions.Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { 15, 0 },
                                LengthRouteParam = new float[] { 25 },
                                RotationRouteParam = new float[] { 3, i * 90 }
                            });
                            CreateBone(new CustomBone(new V(Functions.Heart.Centre.X + 550, Functions.Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { -15, 0 },
                                LengthRouteParam = new float[] { 25 },
                                RotationRouteParam = new float[] { -3, i * 90 }
                            });
                        }
                    });
                    }
                    CreateChart(0, T(2), 5, new string[]
                    {
                        "","","","",  $"Block{Rand(1,5)}","","","",
                        $"Bomb{Rand(1,5)}","","","",  $"Block{Rand(1,5)}","","","",
                        $"Bomb{Rand(1,5)}","","","",  $"Block{Rand(1,5)}","","","",
                        $"Bomb{Rand(1,5)}","","","",  $"Block{Rand(1,5)}","","","",
                        $"Bomb{Rand(1,5)}","","","",
                        $"GetAll(Change)(sBlock{Rand(1,5)})","","","",  $"sBomb{Rand(1,5)}","","","",
                        $"(sBlock{Rand(1,5)})","","","",  $"sBomb{Rand(1,5)}","","","",
                        $"(sBlock{Rand(1,5)})","","","",  $"sBomb{Rand(1,5)}","","","",
                        $"(sBlock{Rand(1,5)})","","","",  $"sBomb{Rand(1,5)}","","","",
                        "GetAll","","","",
                    });
                }
                if (InBeat(343.5f))
                {
                    Functions.Heart.RotateTo(0);
                    SetSoul(0);
                    HeartAttribute.Speed = 3.5f;
                    SetBox(290, 160, 160);
                }
                if (InBeat(344))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            CreateGB(new NormalGB(Functions.Heart.Centre + GV(100, Rand(-135, -45)), Functions.Heart.Centre, new(1, 0.5f), T(1), 10));
                            PlaySound(FightResources.Sounds.pierce);
                            CreateBone(new DownBone(false, 4.5f, 40));
                            CreateBone(new UpBone(false, 4.5f, 80));

                        });

                    }

                }
                if (InBeat(345))
                {
                    NameShower.level = 3.ToString();
                    for (int i = 0; i < 4; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            CreateGB(new NormalGB(Functions.Heart.Centre + GV(180, 0), Functions.Heart.Centre, new(1, 0.5f), T(1), 10));
                            CreateGB(new NormalGB(Functions.Heart.Centre - GV(180, 0), Functions.Heart.Centre, new(1, 0.5f), T(1), 10));
                            PlaySound(FightResources.Sounds.pierce);
                            CreateBone(new UpBone(false, 3.0f, 40));
                            CreateBone(new DownBone(false, 3.0f, 80));
                        });
                    }
                }
                #endregion  
                #region 回れ！雪月花+いざ参ります+World Revolving+???
                if (InBeat(351))
                {
                    var cameraProduction = new Functions.ScreenDrawing.Shaders.Filter(Effect3D, 0.7f);
                    Functions.ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);
                    NameShower.level = 4.ToString();
                    Effect3D.TextureSize = new V(640f, 480f);
                    Effect3D.ProjectPointOffect = new V(320f, 240);
                    Effect3D.CameraPosition = new Vector3(0, 0, 0);
                    Effect3D.ProjectPoint = new Vector3(0, 0, 200);
                    float rot = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        DelayBeat(i * 0.01f, () =>
                        {

                            rot += 90f / 100f;
                            Effect3D.ProjectAxisX = new Vector3(Cos(rot), 0, 0 - Sin(rot));
                            Effect3D.ProjectAxisZ = new Vector3(Sin(rot), 0, Cos(rot));
                            Effect3D.CameraPosition = new Vector3(0f, Sin(rot) * 1f, 0f);
                        });
                    }
                    DelayBeat(1, () =>
                    {
                        ScreenDrawing.LeftBoundDistance = 150;
                        ScreenDrawing.RightBoundDistance = 150;
                        BoundColor = Color.White;
                        shaderProduction4?.Dispose();
                        Regenerate(15);
                        float rot = 270;
                        for (int i = 0; i < 100; i++)
                        {
                            DelayBeat(i * 0.01f, () =>
                            {

                                rot += 90f / 100f;
                                Effect3D.ProjectAxisX = new Vector3(Cos(rot), 0, 0 - Sin(rot));
                                Effect3D.ProjectAxisZ = new Vector3(Sin(rot), 0, Cos(rot));

                                Effect3D.CameraPosition = new Vector3(0f, Sin(rot) * 1f, 0f);
                            });
                        }
                    });
                    DelayBeat(2, () => { cameraProduction.Dispose(); });
                }
                if (InBeat(352))
                {

                    //Settings.GreenTap = true;
                    RegisterFunctionOnce("Move", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenPositionDelta = new V(0, s * -1);
                            InstantTP(new V(320, 240 + s));
                            InstantSetBox(new V(320, 240 + s), 84, 84);
                        }, LinkEase(Stable(0, 0), EaseOut(T(2f), -40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(2f), -40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(2f), -40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(0.333f), 40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(0.333f), 40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(0.333f), 40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(0.333f), -40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(0.333f), -40, ES.Cubic),
                                Stable(0, 0), EaseOut(T(0.33f), 80, ES.Back)));
                        DelayBeat(8, () =>
                        {
                            SetBox(310, 280, 98);
                            SetSoul(0);
                        });
                    });
                    RegisterFunctionOnce("GB1", () =>
                    {
                        SetPlayerBoxMission(0);
                        CreateGB(new GreenSoulGB(T(4), 1, 0, T(1)));
                    });
                    RegisterFunctionOnce("GB2", () =>
                    {
                        SetPlayerBoxMission(0);
                        CreateGB(new GreenSoulGB(T(4), 3, 1, T(1)));
                    });
                    RegisterFunctionOnce("Block1", () =>
                    {
                        float r = Rand(0, 359);

                        var let = LinkEase(Stable(0, 0), EaseOut(T(1), 60, ES.Quad));
                        EaseUnit<float> Rot(int times)
                        {
                            if (times % 4 == 0) return LinkEase(Stable(0, r), EaseOut(T(1), 90, ES.Quint));
                            if (times % 4 == 1) return LinkEase(Stable(0, r + 90), EaseOut(T(1), 90, ES.Quint));
                            if (times % 4 == 2) return LinkEase(Stable(0, r + 180), EaseOut(T(1), 90, ES.Quint));
                            else return LinkEase(Stable(0, r + 270), EaseOut(T(1), 90, ES.Quint));
                        }
                        V ct = new(R(100, 540), R(100, 380));
                        var p1 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(0)),
                                Stable(1145, ct))
                            );
                        var p2 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(1)),
                                Stable(1145, ct))
                            );
                        var p3 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(2)),
                                Stable(1145, ct))
                            );
                        var p4 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(3)),
                                Stable(1145, ct))
                            );
                        Line l1 = new(p1, p2);
                        Line l2 = new(p2, p3);
                        Line l3 = new(p3, p4);
                        Line l4 = new(p4, p1);
                        CreateEntity(l1, l2, l3, l4);
                        l1.AlphaDecrease(T(2));
                        l2.AlphaDecrease(T(2));
                        l3.AlphaDecrease(T(2));
                        l4.AlphaDecrease(T(2));
                    });
                    RegisterFunctionOnce("Block2", () =>
                    {
                        float r = Rand(0, 359);

                        var let = LinkEase(Stable(0, 0), EaseOut(T(1), 60, ES.Quad));
                        EaseUnit<float> Rot(int times)
                        {
                            if (times % 4 == 0) return LinkEase(Stable(0, r), EaseOut(T(1), -90, ES.Quint));
                            if (times % 4 == 1) return LinkEase(Stable(0, r + 90), EaseOut(T(1), -90, ES.Quint));
                            if (times % 4 == 2) return LinkEase(Stable(0, r + 180), EaseOut(T(1), -90, ES.Quint));
                            else return LinkEase(Stable(0, r + 270), EaseOut(T(1), -90, ES.Quint));
                        }
                        V ct = new(R(100, 540), R(100, 380));
                        var p1 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(0)),
                                Stable(1145, ct))
                            );
                        var p2 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(1)),
                                Stable(1145, ct))
                            );
                        var p3 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(2)),
                                Stable(1145, ct))
                            );
                        var p4 = LinkEase(
                            Stable(0, ct),
                            Add(
                                Polar(let, Rot(3)),
                                Stable(1145, ct))
                            );
                        Line l1 = new(p1, p2);
                        Line l2 = new(p2, p3);
                        Line l3 = new(p3, p4);
                        Line l4 = new(p4, p1);
                        CreateEntity(l1, l2, l3, l4);
                        l1.AlphaDecrease(T(2));
                        l2.AlphaDecrease(T(2));
                        l3.AlphaDecrease(T(2));
                        l4.AlphaDecrease(T(2));
                    });
                    CreateChart(T(0), T(2), 7, new string[]
                    {
                        "$1($3)($21)($01)(Move)","$1","", "$3($21)","",
                        "$1","$21","$3",
                        "$1($3)($21)($01)","R","", "R1(R)","",
                        "R","R1","R",
                        "$1($3)($21)($01)","R1","", "R(R1)","",
                        "R","R1","R",

                    });
                    CreateChart(T(0), T(2), 8, new string[]
                    {
                        "Block1(Block2)","Block2","","Block1",  "","Block2","Block1","Block2",
                        "Block1(GB1)","Block2","", "Block1",  "GB2","Block2","Block1","Block2",
                        "Block1(Block2)","Block1","", "Block2",  "","Block1","Block2","Block1",
                        "!!3","","(*$31)(+01)","(*$31)(+01)", "!!3","","(*$1)(+0)","(*$1)(+0)",
                    });
                }
                if (InBeat(352))
                {
                    SetBox(new V(320, 240), 84, 84);
                    SetSoul(1);

                }
                if (InBeat(358))
                {
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0), EaseIn(T(2f), 360, ES.Cubic),
                                                                                 Stable(0, 0), EaseOut(T(2f), 360, ES.Cubic)));
                }
                if (InBeat(360))
                {
                    RunEase(s => BoundColor = C.Lerp(C.White,C.LightPink,s),EaseOut(T(2),0,1,ES.Sine));
                    NameShower.level = 5.ToString();
                    HeartAttribute.Speed = 3.3f;
                    SetBox(310, 280, 98);
                    SetSoul(0);
                    float range = 12;
                    AddInstance(hull1 = new VertexHull(
                        new Vector3[] {
                            new(range, range, range),
                            new(range, -range, range),
                            new(-range, range, range),
                            new(-range, -range, range),
                            new(range, range, -range),
                            new(range, -range, -range),
                            new(-range, range, -range),
                            new(-range, -range, -range),
                        },
                        new Vector3(0.5f, -0.5f, -3)
                    ));
                    AddInstance(hull2 = new VertexHull(
                        new Vector3[] {
                            new(range, range, range),
                            new(range, -range, range),
                            new(-range, range, range),
                            new(-range, -range, range),
                            new(range, range, -range),
                            new(range, -range, -range),
                            new(-range, range, -range),
                            new(-range, -range, -range),
                        },
                        new Vector3(-0.5f, 0.5f, 3)
                    ));

                    Point[] points = new Point[] {
                        new(0, 1),
                        new(1, 3),
                        new(2, 3),
                        new(2, 0),
                        new(4, 5),
                        new(5, 7),
                        new(6, 7),
                        new(6, 4),
                        new(0, 4),
                        new(1, 5),
                        new(2, 6),
                        new(3, 7),
                    };

                    for (int cnt = 1; cnt <= 3; cnt++)
                    {
                        CreateBone(new CustomBone(new V(365 - cnt * 90, 310), Motions.PositionRoute.stableValue,
                            (s) =>
                            {
                                float alpha = (s as CustomBone).Rotation;
                                float res = 1 / Cos(((alpha + 45) % 90) - 45) * 84f;
                                return res;
                            },
                            (s) =>
                            {
                                return s.AppearTime / T(1.5f) * 270;
                            }
                        )
                        { AlphaIncrease = true });
                        CreateBone(new CustomBone(new V(275 + cnt * 90, 310), Motions.PositionRoute.stableValue,
                            (s) =>
                            {
                                float alpha = (s as CustomBone).Rotation;
                                float res = 1 / Cos(((alpha + 45) % 90) - 45) * 84f;
                                return res;
                            },
                            (s) =>
                            {
                                return s.AppearTime / T(1.5f) * 270;
                            }
                        )
                        { AlphaIncrease = true });
                        for (int i = 0; i < points.Length; i++)
                        {
                            CreateBone(new CustomBone(new V(365 - cnt * 90, 310),
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    V vec1 = (hull1 as VertexHull).Translated[cur.X];
                                    V vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                    return vec1 + (vec2 - vec1) / 2;
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    V vec1 = (hull1 as VertexHull).Translated[cur.X];
                                    V vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                    return MathF.Max(0, (vec2 - vec1).Length() - 4);
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    float v;
                                    V vec1 = (hull1 as VertexHull).Translated[cur.X];
                                    V vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                    return v = (vec2 - vec1).Direction() + 90;
                                }
                            )
                            { Tags = new string[] { "" }, Extras = points[i], AlphaIncrease = true });
                        }
                        for (int i = 0; i < points.Length; i++)
                        {
                            CreateBone(new CustomBone(new V(275 + cnt * 90, 310),
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    V vec1 = (hull2 as VertexHull).Translated[cur.X];
                                    V vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                    return vec1 + (vec2 - vec1) / 2;
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    V vec1 = (hull2 as VertexHull).Translated[cur.X];
                                    V vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                    return MathF.Max(0, (vec2 - vec1).Length() - 4);
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    float v;
                                    V vec1 = (hull2 as VertexHull).Translated[cur.X];
                                    V vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                    return v = (vec2 - vec1).Direction() + 90;
                                }
                            )
                            { Tags = new string[] { "" }, Extras = points[i], AlphaIncrease = true });
                        }
                    }

                }
                if (InBeat(360 + 6))
                {
                    SetBox(180, 270, 261, 359);
                }
                if (InBeat(360 + 8))
                {
                    SetBox(180, 460, 261, 359);
                }
                if (InBeat(360 + 10, 360 + 12))
                {
                    float x = MathHelper.Lerp(200, 370, (GametimeF - T(360 + 10)) / T(2));
                    SetBox(x, 460, 261, 359);
                }
                if (InBeat(360 + 13))
                {
                    SetBox(310, 240, 98);
                }
                if (InBeat(360 + 14))
                {
                    SetBox(310, 80, 94);
                }
                if (InBeat(360 + 15f))
                {
                    SetBox(310, 60, 90);
                }
                if (InBeat(375))
                {
                    RunEase((s) =>
                    {
                        LeftBoundDistance = s;
                        RightBoundDistance = s;
                    }, LinkEase(Stable(0, 150), EaseOut(T(0.5f), -150, ES.Cubic)));
                    RunEase((s) => { BoxStates.Centre = new V(320, s); },
                    LinkEase(Stable(0, 310), EaseOut(T(1f), -380, ES.Cubic)));
                    var shaker = new Shader(Loader.Load<Effect>("Musics\\DustTrust\\shake"));
                    RenderProduction shaderProduction = ScreenDrawing.ActivateShader(shaker, 0.8484f);
                    shaker.Parameters["frequency"].SetValue(0f);
                    shaker.Parameters["distance"].SetValue(new V(0f, 0f));
                    shaker.Parameters["range"].SetValue(0f);
                    shaker.Parameters["frequency2"].SetValue(0f);
                    shaker.Parameters["range2"].SetValue(0f);
                    shaker.Parameters["time"].SetValue(0f);
                    shaker.Parameters["time2"].SetValue(0f);
                    float a = 0;
                    float b = 0;
                    RunEase((s) => { shaker.Parameters["range"].SetValue(s); },
                    LinkEase(Stable(0, 0), EaseIn(T(0.5f), 50, ES.Cubic),
                             Stable(0, 0), EaseOut(T(0.5f), -40, ES.Linear)));
                    RunEase((s) => { shaker.Parameters["distance"].SetValue(new V(s * (1 - (Rand(0, 1) * 2)), s * (1 - (Rand(0, 1) * 2)))); },
                    LinkEase(Stable(0, 0), EaseIn(T(0.5f), 0.1f, ES.Cubic),
                             Stable(0, 0), EaseOut(T(0.5f), -0.095f, ES.Linear)));
                    RunEase((s) => { shaker.Parameters["frequency"].SetValue(s); },
                    LinkEase(Stable(0, 0), EaseIn(T(0.5f), 500, ES.Cubic),
                             Stable(0, 0), EaseOut(T(0.5f), -200, ES.Linear)));
                    DelayBeat(1, () =>
                    {
                        shaker.Parameters["distance"].SetValue(new V(0.005f, 0.005f));
                        for (float i = 0; i < (T(16) / 10); i++)
                        {
                            AddInstance(new TimeRangedEvent(10 * i, 1, () =>
                            {
                                AddInstance(new TimeRangedEvent(0, 5, () =>
                                {
                                    a += 1.2f;
                                    b -= 300 / 5;
                                    shaker.Parameters["range"].SetValue(a);
                                    shaker.Parameters["frequency"].SetValue(b);
                                }));
                                AddInstance(new TimeRangedEvent(6, 5, () =>
                                {
                                    a -= 1.2f;
                                    b += 300 / 5;
                                    shaker.Parameters["range"].SetValue(a);
                                    shaker.Parameters["frequency"].SetValue(b);
                                }));
                            }));
                        }
                    });
                    DelayBeat(17, () => { shaderProduction.Dispose(); });
                }
                #endregion
                #region DT2+GOODDRILL
                if (InBeat(375.5f))
                {
                    HeartAttribute.MaxHP = 48;
                    Regenerate(48);
                    HPBar.HPExistColor = Color.Yellow;
                    HPBar.HPLoseColor = Color.Red;
                    HeartAttribute.KR = true;
                    HeartAttribute.KRDamage = 8;
                    CustomBone[] f = GetAll<CustomBone>();
                    foreach (CustomBone fs in f)
                    {
                        fs.Dispose();
                    }
                }
                if (InBeat(376))
                {
                    ScreenDrawing.BoundColor = new(243, 3, 218);
                    UpBoundDistance = 0;
                    DownBoundDistance = 0;
                    RunEase((s) =>
                    {
                        UpBoundDistance = s;
                        DownBoundDistance = s;
                    }, LinkEase(Stable(0, 0), EaseOut(T(0.5f), 150, ES.Cubic)));

                    InstantSetBox(480 + 560, 480, 240);
                    RunEase((s) => { InstantSetBox(new V(320, s), 480, 240); }, LinkEase(Stable(0, 480 + 560), EaseOut(T(0.9f), -240 - 560, ES.Cubic)));
                }
                if (InBeat(376))
                {
                    A21();
                    A22();
                }
                if (InBeat(377))
                {
                    float height = 0;
                    AddInstance(new TimeRangedEvent(T(15), () =>
                    {
                        UpBoundDistance = 150 + Sin(height) * 100;
                        DownBoundDistance = 150 + Sin(height) * -100;
                        height += 3;
                    }));
                }
                if (InBeat(376f, 380) && At0thBeat(4))
                {

                    A23();
                    DelayBeat(2, () => { A24(); });

                }
                if (InBeat(376, 382) && At0thBeat(2))
                {
                    Knife(Functions.Heart.Centre, Rand(0, 360), false, T(1f));
                    DelayBeat(1, () =>
                    {
                        Rand(0, 1);
                        Knife(Functions.Heart.Centre, LastRand * 90, false, T(1f));
                    });
                }
                if (InBeat(376, 380) && At0thBeat(1))
                {
                    A25();
                }
                if (InBeat(382)) { SetSoul(0); }
                if (InBeat(384))
                {
                    for (int i = 0; i < 1; i++)
                    {
                        DelayBeat(i * 4, () =>
                        {
                            CreateBone(new CustomBone(new(80, 120), Motions.PositionRoute.linear, 60, 6400, 600)
                            {
                                ColorType = 1,
                                PositionRouteParam = new float[] { 6.55f, 4.2f }
                            });
                            CreateBone(new CustomBone(new(560, 360), Motions.PositionRoute.linear, 60, 6400, 600)
                            {
                                ColorType = 2,
                                PositionRouteParam = new float[] { -6.55f, -4.2f }
                            });
                        });
                        DelayBeat((i * 4) + 2, () =>
                        {

                            CreateBone(new CustomBone(new(80, 360), Motions.PositionRoute.linear, 120, 6400, 600)
                            {
                                ColorType = 2,
                                PositionRouteParam = new float[] { 6.55f, -4.2f }
                            });
                            CreateBone(new CustomBone(new(560, 120), Motions.PositionRoute.linear, 120, 6400, 600)
                            {
                                ColorType = 1,
                                PositionRouteParam = new float[] { -6.55f, 4.2f }
                            });

                        });
                        DelayBeat((i * 4) + 4, () =>
                        {
                            CreateBone(new CustomBone(new(80, 120), Motions.PositionRoute.linear, 60, 6400, 600)
                            {
                                ColorType = 1,
                                PositionRouteParam = new float[] { 6.55f, 4.2f }
                            });
                            CreateBone(new CustomBone(new(560, 360), Motions.PositionRoute.linear, 60, 6400, 600)
                            {
                                ColorType = 2,
                                PositionRouteParam = new float[] { -6.55f, -4.2f }
                            });
                        });
                    }
                }
                if (InBeat(384))
                {

                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(Stable(0, 0), EaseOut(T(1.5f), 10, ES.Cubic),
                                 Stable(0, 0), EaseIn(T(1.5f), -10, ES.Cubic),
                                 Stable(0, 0), EaseOut(T(1.5f), -10, ES.Cubic),
                                 Stable(0, 0), EaseIn(T(1.5f), 10, ES.Cubic)));

                    for (int i = 0; i < 5; i++)
                    {
                        DelayBeat(i * 1, () =>
                      {
                          DrawingUtil.Linerotate l2 = new(Rand(80, 560), 240, 90, 120, 0.65f);
                          CreateEntity(l2);
                          AddInstance(new TimeRangedEvent(10, 110, () =>
                          {
                              l2.alpha -= 0.15f;
                          }));
                          CreateBone(new CustomBone(new(LastRand, -90), Motions.PositionRoute.linear, 0, 50)
                          {
                              PositionRouteParam = new float[] { 0, 7.25f }
                          });
                          CreateGB(new NormalGB(new V(Rand(120, 520), Rand(50, 430)), V.Zero, new V(1, 0.5f), T(2), T(0.5f)));
                      });
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        DelayBeat(i * 2 + 0f, () =>
                        {
                            A26();
                        });
                        DelayBeat(i * 2 + 0.25f, () =>
                        {
                            A27();
                        });
                        DelayBeat(i * 2 + 0.5f, () =>
                        {
                            A28();
                        });
                        DelayBeat(i * 2 + 0.75f, () =>
                        {
                            A29();
                        });
                        DelayBeat(i * 2 + 1f, () =>
                        {
                            A30();
                        });
                        DelayBeat(i * 2 + 1.25f, () =>
                        {
                            A29();
                        });
                        DelayBeat(i * 2 + 1.5f, () =>
                        {
                            A28();
                        });
                        DelayBeat(i * 2 + 1.75f, () =>
                        {
                            A27();
                        });
                    }
                }
                if (InBeat(389))
                {
                    float rotate = 270;
                    for (int a = 0; a < 8; a++)
                    {
                        V center = Functions.Heart.Centre;
                        CreateGB(new NormalGB(center + new V(Cos(rotate) * 140, Sin(rotate) * 140), center + new V(Cos(rotate) * 140, Sin(rotate) * 140), new(0.75f, 0.75f), rotate + 90, T(1 + (a * 0.333f)), T(10)));
                        rotate += 360 / 8;
                    }
                }
                if (InBeat(389.75f))
                {
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 4;

                }
                if (InBeat(390))
                {
                    InstantSetBox(300, 500, 84 * 1.5f);
                    Boneslab[] b = GetAll<Boneslab>();
                    foreach (Boneslab boneslab in b)
                    {
                        boneslab.Dispose();
                    }
                    AddInstance(new TimeRangedEvent(T(2), () =>
                    {
                        ScreenDrawing.ScreenPositionDelta = Functions.Heart.Centre - new V(320, 240);
                    }));

                    RegisterFunctionOnce("S", () =>
                    {
                        Knife(Functions.Heart.Centre, 45, true, T(0));
                        Knife(Functions.Heart.Centre, 135, true, T(0));
                        Knife(Functions.Heart.Centre, -45, true, T(0));
                        Knife(Functions.Heart.Centre, -135, true, T(0));

                        ScreenDrawing.ScreenScale += 0.25f;

                    });
                    RegisterFunctionOnce("b", () =>
                    {
                        float rot = 0;
                        for (int i = 0; i < 6; i++)
                        {

                            DelayBeat(1 / 3f * i, () =>
                            {
                                ScreenDrawing.ScreenAngle = 360f / 8 * ((8 / 6f) * rot);
                                rot++;
                            });

                        }
                    });
                    RegisterFunction("Reset", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenScale = 1;
                        SetSoul(2);
                        ScreenPositionDelta = V.Zero;
                        BoundColor = Color.Transparent;
                        ResetBarrage();
                        TP(320, 240);
                        //InstantSetBox(240,150,150);
                        HeartAttribute.Gravity = 9.8f;

                    });
                    CreateChart(T(0), T(2), 4, new string[]
                    {
                    "!!3","S(b)","S","S",  "!!3","S","S","S",
                    "!!4","Reset",
                    });

                }
                #endregion
                #region Uber Uber+ULB p2+Helltaker+spacedrift
                if (InBeat(392))
                {
                    NameShower.level = 4.ToString();
                    HeartAttribute.MaxHP = 5;
                    HeartAttribute.KR = false;
                    HeartAttribute.DamageTaken = 1;
                    Regenerate(5);
                    HPBar.HPExistColor = new(0, 255, 0);

                    SetSoul(2);
                    InstantSetBox(290, 340, 160);
                    for (int i = 0; i < 4; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 3f, 15));
                            int p = Rand(0, (int)T(8));
                            CreateBone(new CustomBone(
                                new V(500, 290 - 80),
                                Motions.PositionRoute.linear,
                                (s) => { return Sin01((s.AppearTime + p) / T(1)) * 60 + 131; },
                                (s) => { return -30; }
                            )
                            { PositionRouteParam = new float[] { -3, 0 } });
                            CreateBone(new CustomBone(
                                new V(500 + 93, 290 + 80),
                                Motions.PositionRoute.linear,
                                (s) => { return Sin01((s.AppearTime + p) / T(1)) * 60 + 131; },
                                (s) => { return -30; }
                            )
                            { PositionRouteParam = new float[] { -3, 0 } });
                        });
                    }
                    DelayBeat(8, () =>
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i * 2, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(new DownBone(false, 3f, 15));
                                int p = Rand(0, (int)T(8));
                                CreateBone(new CustomBone(
                                    new V(160, 290 - 80),
                                    Motions.PositionRoute.linear,
                                    (s) => { return Sin01((s.AppearTime + p) / T(1)) * 60 + 131; },
                                    (s) => { return 30; }
                                )
                                { PositionRouteParam = new float[] { 3, 0 } });
                                CreateBone(new CustomBone(
                                    new V(160 - 93, 290 + 80),
                                    Motions.PositionRoute.linear,
                                    (s) => { return Sin01((s.AppearTime + p) / T(1)) * 60 + 131; },
                                    (s) => { return 30; }
                                )
                                { PositionRouteParam = new float[] { 3, 0 } });
                            });
                        }
                    });
                    for (int i = 0; i < 80; i++)
                    {
                        AddInstance(new TimeRangedEvent(T(i * 0.2f), 1, () =>
                        {
                            CreateBone(new CustomBone(new V(500, 210), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                            {
                                PositionRouteParam = new float[] { -3.7f, 0.0f },
                                LengthRouteParam = new float[] { 20, 48, Gametime * 2, 80 },
                                RotationRouteParam = new float[] { 0 }
                            });
                            CreateBone(new DownBone(false, 3.5f, 50) { MarkScore = false, ColorType = 2 });
                        }));
                    }

                }
                if (InBeat(408))
                {
                    Bone[] b = GetAll<Bone>();
                    NameShower.level = 5.ToString();
                    RunEase((s) =>
                    { foreach (Bone b2 in b) { b2.Alpha = s; } }, LinkEase(Stable(0, 1), EaseOut(T(2f), -1, ES.Cubic)));
                    SetSoul(0);
                    SetBox(290, 180, 180);
                    Functions.Heart.Speed = 3f;
                    for (int i = 0; i < 8; i++)
                    {
                        DelayBeat(i * 1, () =>
                    {
                        CreateGB(new NormalGB(Functions.Heart.Centre + GV(160, Rand(0, 359)), Functions.Heart.Centre, new V(1.0f, 0.5f), 58, 6));
                    });
                    }

                    for (int i = 0; i < 2; i++)
                    {

                        DelayBeat(4 * i, () =>
                        {

                            CreateBone(new CustomBone(new V(410, 200), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                            {
                                PositionRouteParam = new float[] { -3.6f, 0 },
                                LengthRouteParam = new float[] { 50 * 2, 60, Rand(0, 49), 88 * 2 },
                                RotationRouteParam = new float[] { 0 },
                                ColorType = 1
                            });
                            CreateBone(new CustomBone(new V(410, 380), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                            {
                                PositionRouteParam = new float[] { -3.6f, 0 },
                                LengthRouteParam = new float[] { 50 * 2, 60, LastRand + 30, 88 * 2 },
                                RotationRouteParam = new float[] { 0 },
                                ColorType = 2
                            });
                            PlaySound(FightResources.Sounds.pierce);
                        });
                        DelayBeat(4 * i + 2, () =>
                        {

                            CreateBone(new CustomBone(new V(230, 200), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                            {
                                PositionRouteParam = new float[] { 3.6f, 0 },
                                LengthRouteParam = new float[] { 50 * 2, 60, Rand(0, 49), 88 * 2 },
                                RotationRouteParam = new float[] { 0 },
                                ColorType = 2
                            });
                            CreateBone(new CustomBone(new V(230, 380), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                            {
                                PositionRouteParam = new float[] { 3.6f, 0 },
                                LengthRouteParam = new float[] { 50 * 2, 60, LastRand + 30, 88 * 2 },
                                RotationRouteParam = new float[] { 0 },
                                ColorType = 1
                            });
                            PlaySound(FightResources.Sounds.pierce);

                        });

                    }

                }
                if (InBeat(416))
                {
                    NameShower.level = 4.ToString();
                    CreateBone(new SwarmBone(80, 160, 0, 920 + T(8)) { ColorType = 2 });
                    CreateBone(new SwarmBone(80, 160, 40, 920 + T(8)) { ColorType = 1 });
                    CreateBone(new SwarmBone(80, 160, 80, 920 + T(8)) { ColorType = 2 });
                    CreateBone(new SwarmBone(80, 160, 120, 920 + T(8)) { ColorType = 1 });
                    CreateBone(new CustomBone(new V(230, 290), Motions.PositionRoute.YAxisSin, 0, 180, T(8))
                    {
                        PositionRouteParam = new float[] { 0, 76, T(4), 0 }
                    });
                    CreateBone(new CustomBone(new V(410, 290), Motions.PositionRoute.YAxisSin, 0, 180, T(8))
                    {
                        PositionRouteParam = new float[] { 0, 76, T(4), 0 }
                    });
                }
                if (InBeat(420))
                {
                    CreateGB(new NormalGB(new V(320 + 75, 290 - 75), new V(320, 240),
                             new V(1, 1), 90, T(2), T(0.5f)));
                    CreateGB(new NormalGB(new V(320 - 75, 290 - 75), new V(320, 240),
                             new V(1, 1), 0, T(2), T(0.5f)));
                    CreateGB(new NormalGB(new V(320 - 75, 290 + 75), new V(320, 240),
                             new V(1, 1), 270, T(2), T(0.5f)));
                    CreateGB(new NormalGB(new V(320 + 75, 290 + 75), new V(320, 240),
                             new V(1, 1), 180, T(2), T(0.5f)));
                }
                if (InBeat(421))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        CreateGB(new NormalGB(new V(320, 290) + GV(75, 45 + i * 90), new V(320, 290),
                                                           V.One, 45 + i * 90 + 180, T(2), T(0.5f)));
                    }
                }
                if (InBeat(416, 420) && At0thBeat(0.2f))
                {
                    float height = Sin(Gametime * 2) * 44 + 65;

                    CreateBone(new UpBone(false, 4.0f, height)
                    {
                        MarkScore = Gametime % T(2) == 0
                    });
                    CreateBone(new DownBone(false, 4.0f, 130 - height)
                    {
                        MarkScore = Gametime % T(2) == 0
                    });

                }
                if (InBeat(423.6f)) { SceneOut(Color.Black, T(0.4f)); }
                #endregion   
                #region UndyneRE + SAVE + asgore
                if (InBeat(424))
                {
                    PlayerInstance.hpControl.GiveProtectTime(5, true);
                    ScreenDrawing.BoundColor = Color.Red * 0.56f;
                    ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = 110;
                    SetSoul(0);
                    ResetBarrage();
                    TP(320, 240);
                    HPBar.HPExistColor = Color.Yellow;
                    HeartAttribute.MaxHP = 5;

                    PlaySound(Sounds.switchScene);
                    Heart heart = Functions.Heart;
                    Heart clone = CreateHeart(290, 180, 180);
                    V mission1 = new(260, 190), mission2 = new(380, 290);
                    SetPlayerBoxMission(clone);
                    InstantSetBox(mission1, 84, 84);
                    SetPlayerBoxMission(heart);
                    InstantSetBox(mission2, 84, 84);
                    float y1, y2, x1, x2;
                    SetBoxMission(0);
                    x2 = BoxStates.Right;
                    y2 = BoxStates.Down;
                    SetBoxMission(1);
                    x1 = BoxStates.Left;
                    y1 = BoxStates.Up;
                    SetBoxMission(0);
                    BoxStates.BoxMovingScale = 0.25f;
                    InstantSetBox(320 + 1, x2 + 15, y1, y2);
                    SetBoxMission(1);
                    BoxStates.BoxMovingScale = 0.25f;
                    InstantSetBox(x1 - 15, 320 - 1, y1, y2);
                    SetPlayerMission(0);
                    Functions.Heart.RotateTo(180);
                    for (int i = 0; i < 16; i++)
                    {
                        DelayBeat(i * 0.5f, () =>
                        {
                            Line up = new(CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), T(2)), (s) => 0);
                            Line down = new(CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), T(2)), (s) => 0);
                            Line[] lines = { up, down };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.4f;
                                line.DrawingColor = Color.LightBlue;
                                line.InsertRetention(new(2, 0.56f));
                                line.InsertRetention(new(4, 0.3f));
                                DelayBeat(1, () =>
                                {
                                    line.AlphaDecrease(T(1));
                                });
                                CreateEntity(line);
                            }
                        });
                    }

                    ForBeat120(0, 8, () =>
                    {
                        SetBoxMission(0);
                        BoxStates.Up += 0.034f * 3;
                        BoxStates.Down -= 0.034f * 3;
                        SetBoxMission(1);
                        BoxStates.Up += 0.034f * 3;
                        BoxStates.Down -= 0.034f * 3;
                    });
                    RegisterFunction("LPike", () =>
                    {
                        SetBoxMission(1);
                        float x = Rand(0, 5) * -20 + 320 - 8;
                        float y = BoxStates.Down + 33;
                        DelayBeat(2, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), -90, T(2)) { IsHidden = true });
                        CreateSpear(new Pike(new(x - 7, y), -90, T(2)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("RPike", () =>
                    {
                        SetBoxMission(0);
                        float x = Rand(0, 5) * 20 + 320 + 8;
                        float y = BoxStates.Up - 33;
                        DelayBeat(2, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), 90, T(2)) { IsHidden = true });
                        CreateSpear(new Pike(new(x + 7, y), 90, T(2)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("Merge", () =>
                    {
                        float y1, y2, x1, x2;
                        SetBoxMission(0);
                        x2 = BoxStates.Right;
                        y2 = BoxStates.Down;
                        SetBoxMission(1);
                        x1 = BoxStates.Left;
                        y1 = BoxStates.Up;

                        float yCentre = (y1 + y2) / 2f;
                        float width = x2 - x1, height = y2 - y1;
                        V centre = new(320, yCentre);
                        CreateLine(yCentre, height);

                        SetBoxMission(0);
                        InstantSetBox(centre, width, height);
                        SetBoxMission(1);
                        InstantSetBox(centre, width, height);
                        RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 1), EaseOut(T(2f), 0.5f, ES.Quad),
                                                                                    EaseOut(T(2f), -0.5f, ES.Cubic)));
                        PlaySound(Sounds.pierce);
                        DelayBeat(2, () =>
                        {
                            PlaySound(Sounds.damaged);

                            SetPlayerMission(1);
                            Player.Heart mission = Functions.Heart;
                            SetPlayerMission(0);
                            Functions.Heart.RotateTo(0);
                            Functions.Heart.Merge(mission);

                            CentreEasing.EaseBuilder builder = new();
                            builder.Insert(T(3), CentreEasing.Alternate(1,
                                CentreEasing.Linear(new(-9, 0), new(0, 0), T(3)),
                                CentreEasing.Linear(new(9, 0), new(0, 0), T(3))
                            ));
                            builder.Run((s) =>
                            {
                                ScreenDrawing.ScreenPositionDelta = s;
                            });
                            DelayBeat(3.2f, () =>
                            {
                                ScreenDrawing.ScreenPositionDelta = V.Zero;
                            });
                        });
                    });
                    RegisterFunctionOnce("Fireball", () =>
                    {
                        float r = Rand(0, 359);
                        PlaySound(Sounds.pierce, 0.7f);
                        for (int a = 0; a < 36; a++)
                        {
                            int colortype = 0;

                            int x = a;
                            if ((0 < x && x < 9) || (18 < x && x < 27)) colortype = 2;
                            V mt = GV(MathF.Sqrt(360 * 360 + 280 * 280), r + x * 10);
                            var e = LinkEase(
                                Stable(0, mt),
                                Linear(T(4), -mt)
                                );
                            var i = LinkEase(
                                Stable(0, 0),
                                Linear(T(24), 360)
                                );
                            var end = Add(Polar(e, i), new V(320, 240));
                            //Line l = new(end.Easing, Stable(0, new V(320, 240)).Easing);
                            //CreateEntity(l);
                            //l.AlphaDecrease(T(4));
                            FireBall f = new(end) { AutoDispose = false, ColorType = colortype };
                            //Hand h = new(end, Stable(0));
                            CreateEntity(f);
                            DelayBeat(4, () => { f.Dispose(); });
                        }
                    });
                    CreateChart(T(0f), T(2), 0, new string[]
                    {
                       "LPike(Fireball)","","","",  "LPike","","RPike","",
                       "LPike(Fireball)","","","", "RPike","","","",
                       "LPike(Fireball)","","RPike","",  "LPike","","RPike","",
                       "Merge",
                    });
                }
                if (InBeat(430))
                {

                    RegisterFunctionOnce("BackGround", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Purple * 0.9f;
                        ScreenDrawing.UpBoundDistance = 0;
                        RunEase((s) =>
                        {
                            ScreenDrawing.DownBoundDistance = s;
                        },
                        EaseOut(T(2), 160, ES.Back));
                        ForBeat(8, () =>
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
                    RegisterFunctionOnce("star1", () =>
                    {
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(T(0.5f), CentreEasing.LerpTo(new(320, -50), 0.0295f * 4, new V(320, 100)));
                        Extends.Star s = new(new V(320, -50), 0.8f) { rotatespeed = 0.5f };
                        e.Run((m) => { s.Centre = new V(0, 0) + m; });
                        s.rotatespeed = 1.8f;
                        CreateEntity(s);
                        DelayBeat(7.5f, () => { s.Dispose(); });
                        ForBeat(4, () => { s.rotatespeed -= 0.005f; });
                    });
                    RegisterFunctionOnce("star2", () =>
                    {
                        PlaySound(Sounds.change);
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(T(8), CentreEasing.Accelerating(new(Rand(2f, 6f)*RandSignal(), Rand(-7.00f, -10.00f)), new(0, 0.4f)));
                        Extends.Star s = new(e.GetResult(), 0.6f) { rotate = false };
                        CreateEntity(s);
                        e.Run((m) => { s.Centre = new V(320, 100) + m; });
                        s.rotatespeed = 0;
                        ForBeat(16, () =>
                        {
                            s.rotatespeed += 0.1f;
                        });

                    });
                    RegisterFunctionOnce("star3", () =>
                    {
                        PlaySound(Sounds.change);
                        PlaySound(Sounds.change);
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(T(8), CentreEasing.Accelerating(new(Rand(4f, 6f) * RandSignal(), Rand(-7.00f, -10.00f)), new(0, 0.4f)));
                        Extends.Star s = new(new V(320, 100), 0.6f) { rotate = false };
                        CreateEntity(s);
                        e.Run((m) => { s.Centre = new V(320, 100) + m; });
                        CentreEasing.EaseBuilder e1 = new();
                        e1.Insert(T(8), CentreEasing.Accelerating(new(Rand(4f, 6f) * RandSignal(), Rand(-7.00f, -10.00f)), new(0, 0.4f)));
                        Extends.Star s1 = new(new V(320, 100), 0.6f) { rotate = false };
                        CreateEntity(s1);
                        e1.Run((m) => { s1.Centre = new V(320, 100) + m; });
                        float add = 0;
                        s.rotatespeed = 0;
                        s1.rotatespeed = 0;
                        ForBeat(16, () =>
                        {
                            add += 0.1f;
                            s.rotatespeed -= 0.1f;
                            s1.rotatespeed += 0.1f;
                        });
                    });
                    RegisterFunctionOnce("HandAttackR", () =>
                    {
                        V v = new(BoxStates.Right, BoxStates.Up);
                        var ce = LinkEase(
                            Stable(T(0.5f), v),
                            Linear(T(0.5f), new V(0, BoxStates.Height)),
                            Linear(T(1f), new V(0, 40))
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
                                float rot = MathUtil.Direction(h.Centre, Functions.Heart.Centre);
                                V vec = GV(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(T(2f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 1.5f };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(1f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(T(1f), 1, 0)); });
                        DelayBeat(2f, () => { h.Dispose(); });
                    });
                    RegisterFunctionOnce("HandAttackL", () =>
                    {
                        V v = new(BoxStates.Left, BoxStates.Down);
                        var ce = LinkEase(
                            Stable(T(0.5f), v),
                            Linear(T(0.5f), new V(0, -BoxStates.Height)),
                            Linear(T(1f), new V(0, -40))
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
                                float rot = MathUtil.Direction(h.Centre, Functions.Heart.Centre);
                                V vec = GV(550, rot);
                                var final = LinkEase(
                                    Stable(0, h.Centre),
                                    Linear(T(2f), vec)
                                    );
                                FireBall f = new(final) { Alpha = 1, Depth = 0.4f, Scale = 1.5f };
                                CreateEntity(f);
                            });
                        }
                        DelayBeat(1f, () => { RunEase((s) => { h.Alpha = s; }, Stable(0, 1), Linear(T(1f), 1, 0)); });
                        DelayBeat(2f, () => { h.Dispose(); });
                    });
                    CreateChart(T(2), T(2), 0f, new string[]
                    {
                    "(BackGround)(star1)(HandAttackL)","","star2","",  "star2","","star2","",
                    "star2(HandAttackR)","","","",  "star2","","star2","",
                    "star2(HandAttackL)","","star2","",  "star2","","star2","",
                    "star2(HandAttackR)","","star2","",  "star2","","(star3)(star3)(star3)(star3)","",
                    });
                    float curAlpha = 0.05f;
                    float alphaMax = 0;
                    RegisterFunctionOnce("HPike", () =>
                    {
                        curAlpha = 0.2f;
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(1, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, T(1)) { IsHidden = true });
                        Line line = new(new V(BoxStates.Left, y), new V(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(T(2));
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("CreateLine", () =>
                    {
                        float rotation = MathUtil.Direction(new(320, 240));
                        Line bottomLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, T(32), 0), new V(160, 360)), (s) => rotation);
                        Line bottomRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, T(32), 0), new V(480, 360)), (s) => -rotation);
                        Line topLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, T(32), 0), new V(160, 120)), (s) => -rotation);
                        Line topRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, T(32), 0), new V(480, 120)), (s) => rotation);
                        Line[] lines = { bottomLeft, bottomRight, topLeft, topRight };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = curAlpha;
                            line.DrawingColor = Color.Gold;
                        }
                        ForBeat(8, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = curAlpha;
                            }
                            curAlpha = curAlpha * 0.8f + 0.05f * 0.2f;
                            alphaMax += 0.002f;
                            curAlpha = MathF.Min(alphaMax, curAlpha);
                        });
                        DelayBeat(6, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(T(2));
                            }
                        });
                    });
                    CreateChart(T(2), T(2), 0f, new string[]
                    {
                    "(HPike)(CreateLine)","","","", "(HPike)","","","",
                    "(HPike)","","","", "(HPike)","","","",
                    "(HPike)","","","", "(HPike)","","","",
                    "(HPike)","","","", "(HPike)","","","",
                    });
                }
                if (InBeat(439.5f))
                {
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.3f);
                    SceneRendering.InsertProduction(splitter);
                    RunEase((s) => { splitter.Intensity = s; }, LinkEase(Stable(0, 0), EaseOut(T(1f), -400, ES.Cubic)

                                                                   ));
                    DelayBeat(1, () => { splitter.Intensity = 1; });
                    //RunEase((s) => { ScreenPositionDelta = new(0,s); }, LinkEase(Stable(0, 0), EaseOut(T(0.5f), 480, ES.Cubic)));
                    var blur = new ScreenDrawing.Shaders.Blur();
                    DelayBeat(2, () => { blur.Dispose(); });
                    ScreenDrawing.SceneRendering.InsertProduction(blur);
                    RunEase((s) => { blur.Sigma = s; }, LinkEase(Stable(0, 0), EaseOut(T(1f), 24, ES.Cubic),
                                                 Stable(0, 0), EaseOut(T(1f), -24, ES.Cubic)));
                }
                if (InBeat(439.9f))
                {
                    InstantSetGreenBox();
                    InstantTP(BoxStates.Centre);
                }
                #endregion
                #region GOODWORLD + Death By Glamour
                if (InBeat(440f))
                {
                    NameShower.level = 3.ToString();
                    HeartAttribute.MaxHP = 32;
                    HeartAttribute.KR = true;
                    SetSoul(1);
                    Heart clone = CreateHeart(new V(-150, 480 - 90), 84, 84);
                    SetPlayerBoxMission(clone);
                    InstantSetBox(new V(-150, 480 - 90), 84, 84);
                    BoundColor = new(200, 18, 35);
                    ResetBarrage();
                    Star[] stars = GetAll<Star>(); foreach (Star star in stars) { star.Dispose(); }
                    FireBall[] fire = GetAll<FireBall>(); foreach (FireBall ball in fire) { ball.Dispose(); }
                    //RunEase((s) => { ScreenPositionDelta = new V(s,0); }, LinkEase(Stable(0, -640), EaseOut(T(2f), 640, ES.Cubic)));
                    DownBoundDistance = 0;
                    ForBeat(2, () =>
                    {
                        DownBoundDistance = DownBoundDistance * 0.91f + 300 * 0.09f;

                    });

                    DelayBeat(2, () =>
                    {
                        SetPlayerMission(1); SetBoxMission(1);
                        DrawingUtil.LerpGreenBox(T(2), new(640 - 50, 480 - 90), 0.09f);
                    });
                    DelayBeat(4, () =>
                    {
                        SetPlayerMission(1); SetBoxMission(1);
                        DrawingUtil.LerpGreenBox(T(2), new(50, 480 - 90), 0.09f);
                    });
                    DelayBeat(6, () =>
                    {
                        SetPlayerMission(1); SetBoxMission(1);
                        DrawingUtil.LerpGreenBox(T(1.5f), new(750, 480 - 90), 0.09f);
                    });
                    DelayBeat(7.9f, () =>
                    {
                        clone.Dispose();
                    });
                    for (int i = 0; i < 64; i++)
                    {
                        DelayBeat(i * 0.25f, () =>
                        {
                            Linerotatee line = new(Rand(50, 640 - 50), 240, 90 + Rand(-35, 35), 26, 1) { width = 0, depth = 0.001f };
                            float sin = -1;
                            AddInstance(new TimeRangedEvent(35, () =>
                            {
                                line.width = 42 * sin * sin * sin * sin;
                                sin += 1f / 35f;
                                if (line.width <= 1) line.width = 0;
                            }));
                            CreateEntity(line);


                        });
                    }
                    float e = 1;
                    for (int i = 0; i < 6; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            float temp = e % 2 == 0?-1:1;
                            RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 5 * temp), EaseOut(T(1f), 5 * temp * -1, ES.Cubic)));
                            e++;
                        });
                    }
                    float e2 = 1;
                    for (int i = 0; i < 6; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            float temp = e % 2 == 0 ? -1 : 1;
                            RunEase((s) => { Functions.Heart.InstantSetRotation(s); }, LinkEase(Stable(0, 5 * temp), EaseOut(T(1f), 5 * temp * -1, ES.Cubic)));
                            e2++;
                        });
                    }
                    DelayBeat(12, () =>
                    {
                        float b = 1;
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i * 1, () =>
                            {
                                float e = b;
                                RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, ScreenScale), EaseOut(T(1f), ScreenScale / 1.1f, ES.Cubic)));
                                RunEase((s) => { Functions.Heart.InstantSetRotation(s); }, LinkEase(Stable(0, 25 * e), EaseOut(T(1f), 25 * e * -1, ES.Cubic)));
                                b *= -1;
                            });
                        }
                    });
                }
                if (InBeat(440))
                {
                    SetPlayerBoxMission(0);
                    InstantSetGreenBox();
                    InstantTP(BoxStates.Centre);
                    CreateChart(T(0), T(2), 6, new string[]
                        {
                      "$1($31)","","$3(11)","",  "$1($31)","","$3(11)","",
                      "($1)(+0)",">+1","<-1",">+1",  "<-1",">+1","<-1",">+1",
                      "($2)(+0)",">+1","<-1",">+1",  "<-1",">+1","<-1",">+1",
                      "($3)(+0)",">+1","<-1",">+1",  "<-1",">+1","<-1",">+1",



                        });
                    Settings.GreenTap = true;
                    CreateChart(T(2), T(2), 6, new string[]
                    {
                      "","","","",  "!!8","$011","+011","+011","+011","+011","+011","+011","+011",
                      "","","","",  "!!8","$111","+011","+011","+011","+011","+011","+011","+011",
                      "","","","",  "!!8","$211","+011","+011","+011","+011","+011","+011","+011",

                    });
                }
                if (InBeat(448))
                {
                    Heart main = Functions.Heart;
                    Heart clone = CreateHeart(200, 84, 84);
                    NameShower.level = 5.ToString();
                    RunEase((s) =>
                    {
                        SetPlayerBoxMission(main);
                        InstantTP(new V(s, 240));
                        InstantSetBox(new V(s, 240), 84, 84);

                    }, LinkEase(Stable(0, 320), EaseOut(T(1f), -100, ES.Cubic)));

                    RunEase((s) =>
                    {
                        SetPlayerBoxMission(clone);
                        InstantTP(new V(s, 240));
                        InstantSetBox(new V(s, 240), 84, 84);

                    }, LinkEase(Stable(0, 750), EaseOut(T(1f), -330, ES.Cubic)));
                    SetPlayerBoxMission(clone);
                    CreateChart(T(0), T(2), 7, new string[]
                    {
                    "$1($1)","","$3($3)","",  "$0($0)","","$1($1)","",
                    "#0.75#$31","","","",  "$3($3)","","$1($1)","",
                    "*^$0'1.5","","","",  "*^$01'1.5","","","",
                    "*^$0'1.5","","","",  "*^$01'1.5","","","",
                    });
                    SetPlayerBoxMission(main);
                    CreateChart(T(0), T(2), 7, new string[]
                    {
                    "","$31($31)","","$21($21)",  "","$31($31)","","$11($11)",
                    "#0.75#$3","","","",  "$11($11)","","$21($21)","",
                    "*^$21'1.5","","","",  "*^$2'1.5","","","",
                    "*^$21'1.5","","","",  "*^$2'1.5","","","",
                    });
                    DelayBeat(4, () =>
                    {

                        RunEase((s) =>
                        {
                            SetPlayerBoxMission(main);
                            InstantTP(new V(s, 240));
                            InstantSetBox(new V(s, 240), 84, 84);

                        }, LinkEase(Stable(0, 220), EaseOut(T(1f), 25, ES.Cubic),
                                    Stable(0, 0), EaseOut(T(1f), 25, ES.Cubic),
                                    Stable(0, 0), EaseOut(T(1f), 25, ES.Cubic),
                                    Stable(0, 0), EaseOut(T(1f), 25, ES.Cubic)));

                        RunEase((s) =>
                        {
                            SetPlayerBoxMission(clone);
                            InstantTP(new V(s, 240));
                            InstantSetBox(new V(s, 240), 84, 84);

                        }, LinkEase(Stable(0, 420), EaseOut(T(1f), -25, ES.Cubic),
                                    Stable(0, 0), EaseOut(T(1f), -25, ES.Cubic),
                                    Stable(0, 0), EaseOut(T(1f), -25, ES.Cubic),
                                    Stable(0, 0), EaseOut(T(1f), -25, ES.Cubic)));

                    });
                    DelayBeat(7.9f, () => { clone.Dispose(); });
                }
                #endregion*/
                
                #region FreedomDive
                if (InBeat(454))
                {
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        CreateEntity(new Particle(Color.Lerp(Color.Aqua, Color.White, MathF.Pow(Rand(0, 90) / 100f, 2)) * MathF.Pow(Rand(2, 14) / 20f, 1.5f),
                                          new(Rand(-10, 10) / 10f, -7.4f + Rand(-15, 15) / 10f), Rand(10, 16), new(Rand(-100, 740), 495), Sprites.square)
                        {
                            DarkingSpeed = 1.7f,
                            AutoRotate = true
                        });
                    }));
                }
                if (InBeat(456))
                {
                    NameShower.level = 5.ToString();
                    SetPlayerBoxMission(0);
                    Linerotatee[] linerotatees = GetAll<Linerotatee>();
                    foreach (Linerotatee f in linerotatees) { f.Dispose(); }
                    DownBoundDistance = 0;
                    ScreenDrawing.BoundColor = Color.DarkBlue * 0.9f;
                    UIColor = Color.White;
                    RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, ScreenScale), EaseOut(T(2f), ScreenScale * -1 + 1, ES.Expo)));
                    Regenerate(4);
                    ScreenDrawing.DownBoundDistance = 1800;
                    ScreenDrawing.UIColor = Color.LightBlue;

                    //ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.LightBlue * 0.38f, 0.02f);
                    HPBar.HPExistColor = new(0, 255, 0);
                    float speedY = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(T(15), () =>
                    {
                        SetPlayerBoxMission(0);
                        float cycleTick = Gametime / FDBeat(16);
                        Func<float, float> sin01 = AdvanceFunctions.Sin01;
                        InstantSetBox(new V(320, 240) + new V(sin01(cycleTick / 2.72f) * b, sin01(cycleTick) * speedY), 84, 84);
                        InstantTP(BoxStates.Centre);
                        if (b < 15) { b += 0.5f; }
                        speedY = speedY * 0.99f + 16 * 0.01f;
                    }));
                    AddInstance(new TimeRangedEvent(0, FDBeat(64), () =>
                    {
                        if (Gametime % 5 == 0)
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.LightBlue * 0.38f, 0.02f);
                    }));
                    RegisterFunctionOnce("Lrot", () =>
                    {
                        AddInstance(new ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    });
                    RegisterFunctionOnce("Rrot", () =>
                    {
                        AddInstance(new ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    });
                    CreateChart(T(0), T(2), 7, new string[]
                    {
                      "$1($31)","","$3($11)","",  "$21($0)","","$2($01)","",
                      "!!16","$2'2(R1)(Rrot)","$1'2","$0'2","", "!!4","R1","R1","R1", "R(R1)","","R(R1)","",
                      "$1($31)","","$3($11)","",  "R1(R)","","R(R1)","",
                      "!!16","$21'2(R)(Lrot)","$11'2","$01'2","", "!!4","R","R","R", "R(R1)","","R(R1)","",
                      "$1($31)","","$3($11)","",  "R1(R)","","R(R1)","",
                      "!!16","$2'2(R1)(Rrot)","$1'2","$0'2","", "!!4","R1","R1","R1", "R(R1)","","R(R1)","",
                      "$1{P}($31{P})","","$3{P}($11{P})","",  "R1{P}(R{P})","","R{P}(R1{P})","",
                      "!!16","$21'2{P}(R)(Lrot)","$11'2{P}","$01'2{P}","", "!!4","","","",
                    });
                }
                if (InBeat(456))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        float alp = (i % 2 == 0 ? -1 : 1) * 10;
                        AddInstance(new InstantEvent(T(i * 4), () =>
                        {
                            ScreenDrawing.CameraEffect.RotateTo(alp, T(2));
                        }));
                    }
                }
                if (InBeat(466))
                {
                    RunEase((s) => { DownBoundDistance = s; },
                        LinkEase(Stable(0, DownBoundDistance), EaseOut(T(2f), DownBoundDistance, ES.Cubic)));
                    Color bonce = BoundColor;
                    RunEase((s) => { BoundColor = Color.Lerp(bonce, Color.Black, s); },
                        LinkEase(Stable(0, 0), EaseOut(T(4f), 1, ES.Linear)));

                }
                #endregion
                #region EndTime
                if (InBeat(470))
                {
                    DelayBeat(0.5f, () =>
                    {
                        foreach (Arrow a in GetAll<Arrow>("P"))
                        {
                            a.Dispose();
                        }
                    });
                    ScreenDrawing.CameraEffect.RotateTo(0, T(2));
                    RunEase((s) => { UISettings.HPShowerPos = new(s, UISettings.HPShowerPos.Y); },
                        LinkEase(Stable(0, UISettings.HPShowerPos.X), EaseIn(T(2f), 350, ES.Cubic)));
                    DelayBeat(2, () =>
                    {
                        ScreenDrawing.HPBar.Vertical = true;
                        ScreenDrawing.HPBar.HPExistColor = new(90, 112, 125);
                        ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                        HeartAttribute.BuffedLevel = 10f;
                        HeartAttribute.DamageTaken = 2;
                        HeartAttribute.MaxHP = 8;
                        HeartAttribute.KR = false;
                        RunEase((s) =>
                        { ScreenDrawing.HPBar.AreaOccupied = new CollideRect(s, 140, 20, 220); }, LinkEase(Stable(0, -50), EaseOut(T(2f), 70, ES.Cubic)));

                    });
                }
                if (InBeat(470))
                {
                    Color uic = ScreenDrawing.UIColor;
                    ValueEasing.EaseBuilder scale = new();
                    scale.Insert(0, ValueEasing.Stable(1));
                    scale.Insert(game.T(4), ValueEasing.EaseOutQuad(1, 0.1f, game.T(4)));
                    scale.Run(scale => { ScreenDrawing.UIColor = uic * scale; ScreenDrawing.HPBar.HPExistColor = new Color(90, 112, 125) * scale; ScreenDrawing.HPBar.HPLoseColor = Color.Gray * scale; });
                    ScreenDrawing.UISettings.CreateUISurface();
                    for (int a = 0; a < 5; a++)
                    {
                        Line l1 = new(new V(320 - 126 - 42 + a * 84, 480), new V(320 - 126 - 42 + a * 84, -800)) { Alpha = 0 };
                        CreateEntity(l1);
                        l1.AlphaIncrease(game.T(2), 0.5f);
                    }
                    Line l2 = new(new V(320 - 126 - 42, 480 - 84 - 42), new V(320 - 126 - 42 + 4 * 84, 480 - 84 - 42)) { Alpha = 0 };
                    CreateEntity(l2);
                    l2.AlphaIncrease(game.T(2), 1f);
                    Apply3D();
                    Functions.Heart.Split();
                    SetBoxMission(0);
                    MainEffect.SoftSetBox(new(320 - 126, 480 - 84), 84, 84, game.T(1), 0, 1);
                    SetPlayerMission(0);
                    Functions.Heart.Alpha = 0.2f;
                    UISettings.CreateUISurface();
                    Functions.Heart.controlingBox.GreenSoulAlpha = 0f;
                    game.DelayBeat(0, () => { SetPlayerMission(0); Functions.Heart.Shields.RemoveShield(Functions.Heart.Shields.BShield); });
                    Functions.Heart.InstantSetRotation(180);
                    MainEffect.SoftTP(new(320 - 126, 480 - 84), game.T(1), 0, 1);

                    Functions.Heart.Split();
                    SetBoxMission(1);
                    MainEffect.SoftSetBox(new(320 - 42, 480 - 84), 84, 84, game.T(1), 1, 1);
                    SetPlayerMission(2);
                    Functions.Heart.Alpha = 0.2f;
                    Functions.Heart.controlingBox.GreenSoulAlpha = 0f;
                    game.DelayBeat(0, () => { SetPlayerMission(2); Functions.Heart.Shields.RemoveShield(Functions.Heart.Shields.RShield); });
                    Functions.Heart.InstantSetRotation(90);
                    MainEffect.SoftTP(new(320 + 42, 480 - 84), game.T(1), 2, 1);
                    SetBoxMission(2);
                    MainEffect.SoftSetBox(new(320 + 42, 480 - 84), 84, 84, game.T(1), 2, 1);
                    SetPlayerMission(1);
                    Functions.Heart.Alpha = 0.2f;
                    Functions.Heart.controlingBox.GreenSoulAlpha = 0f;
                    game.DelayBeat(0, () => { SetPlayerMission(1); Functions.Heart.Shields.RemoveShield(Functions.Heart.Shields.BShield); });
                    Functions.Heart.InstantSetRotation(-90);
                    MainEffect.SoftTP(new(320 - 42, 480 - 84), game.T(1), 1, 1);

                    Functions.Heart.Split();
                    SetBoxMission(3);
                    MainEffect.SoftSetBox(new(320 + 126, 480 - 84), 84, 84, game.T(1), 3, 1);
                    SetPlayerMission(3);
                    Functions.Heart.Alpha = 0.2f;
                    Functions.Heart.controlingBox.GreenSoulAlpha = 0f;
                    game.DelayBeat(0, () => { SetPlayerMission(3); Functions.Heart.Shields.RemoveShield(Functions.Heart.Shields.RShield); });
                    Functions.Heart.InstantSetRotation(-180);
                    MainEffect.SoftTP(new(320 + 126, 480 - 84), game.T(1), 3, 1);
                }
                if (InBeat(470f))
                {

                    StepSample = FightResources.Shaders.StepSample;
                    Polar = FightResources.Shaders.Polar;
                    production2 = new ScreenDrawing.Shaders.Filter(FightResources.Shaders.Polar, 0.8f);
                    production1 = new ScreenDrawing.Shaders.Filter(FightResources.Shaders.StepSample, 0.99f);
                    RenderProduction production3 = Blurs = new ScreenDrawing.Shaders.Blur(0.5f);
                    Blurs.Sigma = 0.0f;
                    ScreenDrawing.SceneRendering.InsertProduction(production1);
                    ScreenDrawing.SceneRendering.InsertProduction(production2);
                    game.StepSample.CentreX = 320f;
                    game.StepSample.CentreY = 240f;
                    Polar.Intensity = 0.0f;
                    StepSample.Intensity = 0f;
                    ScreenDrawing.SceneRendering.InsertProduction(Blurs);
                    RegisterFunction("Polor", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.T(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.T(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            StepSample.Intensity = s * 0.8f;

                        });
                        ValueEasing.EaseBuilder e2 = new();
                        e2.Insert(0, ValueEasing.Stable(0f));
                        e2.Insert(game.T(16), ValueEasing.SinWave(0.1f, game.T(4), 0));
                        e2.Insert(1, ValueEasing.Stable(0));
                        e2.Run((s) =>
                        {

                            game.Polar.Intensity = s * 0.07f;
                            game.Polar.IType = false;
                        });
                    });
                    game.RegisterFunction("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.2f));
                        e1.Insert(game.T(0.5f), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.T(0.5f)));
                        e1.Insert(game.T(0.5f), ValueEasing.EaseInSine(0.3f, 0.1f, game.T(0.5f)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;
                            game.Blurs.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.T(2), 1, 26f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.T(2), 0, 26f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.T(2), 2, 26f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.T(2), 1, 26f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.T(2), 1, 26f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.T(2), 0, 26f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.T(2), 2, 26f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.T(2), 1, 26f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    CreateChart(T(0), T(2), 6, new string[]
                    {
                        "Polor","","","",  "","","","",
                        "Step","","","",  "","","","",
                        "","","","", "","","","",
                        "Step","","","",  "","","","",
                        "","","","", "","","","",
                        "Step","","","",  "","","","",
                        "","","","", "","","","",

                    });
                    CreateChart(T(0), T(2), 6, new string[]
                    {
                    "!!8","(k0)(k1)(k2)","(h0)(h1)","(h0)(h1)(k3)","(h0)(h1)","(h0)(h1)(k2)","(h0)(h1)","(h0)(h1)(k3)","(h0)(h1)",  "k0(k1)","k1","k2(k3)","k2",
                    "!!8","k0","h1","k3","h2",  "k1","h0","k2","h3", "!!8", "h0","k1","h3","k2",  "h1","k0","h2","k3",
                    "!!8","(k2)(k3)(k0)","(h2)(h3)","(h2)(h3)(k1)","(h2)(h3)",  "(h2)(h3)(k0)","(h2)(h3)","(h2)(h3)(k1)","(h2)(h3)", "k0(k1)","k1","k2(k3)","k2",
                    "!!8","k3","h2","k1","h0","k3","h2","k1","h0", "!!8","k1","h2","k3","h2","k1","h0","k1","h2",
                    "","","","",  "","","","",
                    });
                }
                if (InBeat(478))
                {
                    game.To6KIn3D();

                    Player.Heart ExHeart1 = CreateHeart(new CollideRect(320 - 168, 480 - 84, 84, 84));
                    Player.Heart ExHeart2 = CreateHeart(new CollideRect(320 + 168, 480 - 84, 84, 84));
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(ExHeart1);
                        Arrow ar = MakeArrow(game.T(2), 2, 28f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.T(2), 1, 28f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.T(2), 0, 28f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.T(2), 2, 28f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k4", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.T(2), 1, 28f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k5", () =>
                    {
                        SetPlayerMission(ExHeart2);
                        Arrow ar = MakeArrow(game.T(2), 0, 28f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });

                    SetPlayerBoxMission(ExHeart1);
                    MainEffect.SoftTP(new(320 - 126 - 84, 480 - 84), game.T(1), 4, 1);
                    MainEffect.SoftSetBox(new(320 - 126 - 84, 480 - 84), 84, 84, game.T(1), 4, 1);
                    ExHeart1.InstantSetRotation(90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart1.Shields.RemoveShield(ExHeart1.Shields.BShield); });
                    SetPlayerBoxMission(ExHeart2);
                    MainEffect.SoftTP(new(320 + 126 + 84, 480 - 84), game.T(1), 5, 1);
                    MainEffect.SoftSetBox(new(320 + 126 + 84, 480 - 84), 84, 84, game.T(1), 5, 1);
                    ExHeart2.InstantSetRotation(-90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart2.Shields.RemoveShield(ExHeart2.Shields.RShield); });
                    Line l = new(new V(320 - 3 * 84, 480), new V(320 - 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.T(1), 0.5f);
                    Line l2 = new(new V(320 + 3 * 84, 480), new V(320 + 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l2);
                    l2.AlphaIncrease(game.T(1), 0.5f);
                    CreateChart(0, T(2), 10, new string[]
                    {
                       "(k1)(k2)","k0","k1","k2",  "(k3)(k4)","k5","k4","k3",
                       "k2(k3)","k1","k2(k3)","k4",  "k1(k0)","k1(k2)","(k2)k3","k3(k4)",
                       "!!8","k5","k4","k3","k0", "k1","k2","k3","k2","!!8","k1","k0","k1","k2", "k3","k4","k5","k4",
                       "k3(k2)",
                    });
                    DelayBeat(8, () =>
                    {

                        //RunEase((s) => { MasterAlpha = s; }, LinkEase(Stable(0, 1), EaseOut(T(2f), -1, ES.Cubic)));
                        Shader sa = new(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\SCSplit"));
                        sa.Parameters["width"].SetValue(0f);
                        sa.Parameters["height"].SetValue(1f);
                        sa.Parameters["type"].SetValue(4);
                        bool point = false;
                        RenderProduction shaderProduction4s = ActivateShader(sa, 0.997f);
                        //float rot = 0;
                        RunEase((s) =>
                        {
                            sa.Parameters["rots"].SetValue(s);
                            if (point)
                                ScreenDrawing.ScreenAngle = -s + 45;
                        },
                            LinkEase(Stable(0, 45), EaseIn(T(2f), 180, ES.Cubic), Stable(0, 0), EaseOut(T(4), 180, Cu)));
                        RunEase((s) => { sa.Parameters["width"].SetValue(s); },
                            LinkEase(Stable(0, 0), EaseIn(T(2f), 0.7f, ES.Cubic), Stable(0, 0), EaseOut(T(2), -0.7f, Cu)));
                        DelayBeat(4, () =>
                        {
                            shaderProduction4s.Dispose();
                        });
                        DelayBeat(2, () =>
                        {
                            Blurs.Dispose();
                            point = true;
                            RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 2), EaseOut(T(4f), -1, ES.Cubic)));
                        });

                    });
                }
                #endregion             
                #region DreamBattle+Universal Collapse
                if (InBeat(488))
                {
                    NameShower.level = 4.ToString();
                    if (splitter != null)
                    {
                        splitter.Intensity = 0;
                        splitter.RandomDisturb = 0;
                        splitter.Dispose();
                    }
                    ScreenDrawing.UISettings.RemoveUISurface();
                    RemoveUI1();
                    RemoveUI2();
                    RemoveUI3();
                    ResetRendering();
                    if (production2 != null)
                    {
                        production1.Dispose();
                        FightResources.Shaders.StepSample.Intensity = 0;
                        FightResources.Shaders.Polar.Intensity = 0;
                        FightResources.Shaders.Scale.Intensity = 0;
                        Blurs.Dispose();
                        production2.Dispose();
                        MasterAlpha = 1;
                        HeartAttribute.BuffedLevel = 0;
                        HeartAttribute.MaxHP = 92;
                        HeartAttribute.KR = true;
                        Effect3D.TextureSize = new V(640, 480);
                        Effect3D.CameraRotation = new Vector3(0f, 0f, 0f);
                        Effect3D.ProjectAxisX = new Vector3(1f, 0, 0);
                        Effect3D.ProjectAxisY = new Vector3(0f, 1, 0);
                        Effect3D.ProjectAxisZ = new Vector3(0f, 0, 1f);
                        Effect3D.ProjectPoint = new(0, 0, 300);
                        Effect3D.ProjectPointOffect = new(320, 240);
                        Effect3D.Visuospatial = new(640, 480, 200);
                        Effect3D.CameraPosition = new(0);
                        Effect3D.CameraRotation = new(0);
                        Effect3D.CameraAhead = new Vector3(0, 0, 1);
                        Effect3D.CameraRight = new(1, 0, 0);
                        Effect3D.CameraDown = new(0, 1, 0);
                        ScreenDrawing.DownExtending = ScreenDrawing.UpExtending = 0;
                        ScreenDrawing.ScreenPositionDelta = V.Zero;
                        ScreenDrawing.ScreenScale = 1;
                        cameraProduction.Dispose();
                        UIColor = Color.White;
                        HPBar.HPLoseColor = Color.Red;
                        HPBar.HPExistColor = new(0, 255, 0);
                        ScreenDrawing.HPBar.AreaOccupied = new(320, 443, 100, 24);
                        ScreenDrawing.HPBar.Vertical = false;
                        hearts[1].Dispose();
                        hearts[2].Dispose();
                        hearts[3].Dispose();
                        hearts[4].Dispose();
                        hearts[5].Dispose();
                        Line[] ls = GetAll<Line>();
                        foreach (Line e in ls) { e.Dispose(); }
                        SetPlayerBoxMission(0);
                        Functions.Heart.Shields.AddShield(Functions.Heart.Shields.BShield);
                        InstantSetBox(320 - 84, 320 + 84, 240 - 84, 240 + 84);
                        InstantTP(320, 240);
                        SetSoul(0);
                        Functions.Heart.InstantSetRotation(0);
                        Functions.Heart.Alpha = 1;
                        Functions.Heart.controlingBox.GreenSoulAlpha = 0.5f;
                        ScreenDrawing.ScreenAngle = 0;
                    }
                    BadShader = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BackGround1"));
                    BadShader.Parameters["R"].SetValue(118 / 255f);
                    BadShader.Parameters["G"].SetValue(25 / 255f);
                    BadShader.Parameters["B"].SetValue(111 / 255f);
                    BadShader.Parameters["Alpha"].SetValue(1f);
                    BadShader.Parameters["position"].SetValue(new V(320 / 640, 240 / 480));
                    BadShaderrend = ScreenDrawing.ActivateShaderBack(BadShader, 0.6541f);

                }
                if (InBeat(488))
                {

                    CentreCircleBone o1 = new CentreCircleBone(300, 1, 224, T(7));
                    DelayBeat(1, () =>
                    {
                        CreateBone(o1);
                        CreateBone(new CentreCircleBone(210, 1, 224, T(7)) { ColorType = 2 });
                    });
                    DelayBeat(8, () => { o1.RotateSpeed = 2; });
                    for (int i = 0; i < 4; i++)
                    {
                        DelayBeat(i * 2, () =>
                    {
                        if (Rand(1, 2) == 1)
                        {
                            for (int i = 0; i < 5; ++i)
                            {
                                //横向gb墙
                                CreateGB(new NormalGB(new(320 + 84, 240 - 84 + 42 * i), new(320 + 84, 240 - 84 + 42 * i), new(1, 0.2f), 180, 60, 15));
                                CreateGB(new NormalGB(new(320 - 84, 240 - 84 + 42 * i), new(320 - 84, 240 - 84 + 42 * i), new(1, 0.2f), 0, 60, 15));
                                //纵向gb墙
                                CreateGB(new NormalGB(new(320 + 84 - 42 * i, 240 - 84), new(320 + 84 - 42 * i, 240 - 84), new(1, 0.2f), 90, 60, 15));
                                CreateGB(new NormalGB(new(320 - 84 + 42 * i, 240 + 84), new(320 - 84 + 42 * i, 240 + 84), new(1, 0.2f), 270, 60, 15));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 4; ++i)
                            {
                                //横向gb墙
                                CreateGB(new NormalGB(new(320 + 21 * 3, 240 - 21 * 3 + 42 * i), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 180, 60, 15));
                                CreateGB(new NormalGB(new(320 - 21 * 3, 240 - 21 * 3 + 42 * i), new(320 - 21 * 3, 240 - 21 * -3), new(1, 0.2f), 0, 60, 15));
                                //纵向gb墙
                                CreateGB(new NormalGB(new(320 - 21 * 3 + 42 * i, 240 + 21 * 3), new(320 - 21 * 3 + 42 * i, 240 - 21 * 3), new(1, 0.2f), 270, 60, 15));
                                CreateGB(new NormalGB(new(320 + 21 * 3 - 42 * i, 240 - 21 * 3), new(320 - 21 * 3 + 42 * i, 240 - 21 * 3), new(1, 0.2f), 90, 60, 15));
                            }
                        }
                    });
                    }

                }
                if (InBeat(496))
                {

                    BallCreate(16, 84);

                }
                if (InBeat(496, 500) && At0thBeat(2))
                {
                    CreateGB(new NormalGB(Functions.Heart.Centre + new V(-100, 0), Functions.Heart.Centre, new V(1, 0.5f), 0, T(1f), T(0.5f)));
                    CreateGB(new NormalGB(Functions.Heart.Centre + new V(100, 0), Functions.Heart.Centre, new V(1, 0.5f), 180, T(1f), T(0.5f)));
                    DelayBeat(1, () =>
                    {
                        CreateGB(new NormalGB(Functions.Heart.Centre + new V(0, 100), Functions.Heart.Centre, new V(1, 0.5f), -90, T(1f), T(0.5f)));
                        CreateGB(new NormalGB(Functions.Heart.Centre + new V(0, -100), Functions.Heart.Centre, new V(1, 0.5f), 90, T(1f), T(0.5f)));
                    });
                }

                if (InBeat(502))
                {

                    float y = -25;

                    AddInstance(new TimeRangedEvent(T(2), () =>
                    {
                        ScreenPositionDelta += new V(8, y);

                        BadShader.Parameters["position"].SetValue(new V(ScreenPositionDelta.X / 640f, ScreenPositionDelta.Y / 480f));

                        y += 1.2f;

                    }));
                    DelayBeat(2, () =>
                    {
                        BadShaderrend.Dispose();
                        RunEase((s) => { ScreenPositionDelta = new(0, s); }, LinkEase(Stable(0, -480), EaseOut(T(4f), 480, ES.Cubic)));
                    });
                }
                if (InBeat(488))
                {
                    UNLine linedown = new(0, 480, 640, 480, 500, 0.5f),
                         lineup = new(0, 0, 640, 0, 500, 0.5f);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 4, () =>
                        {
                            CreateEntity(linedown);
                            float beattime = 0;
                            AddInstance(new TimeRangedEvent(0, T(1), () =>
                            {
                                linedown.y1 -= beattime * beattime;
                                linedown.y2 -= beattime * beattime;
                                beattime += 0.08f;
                            }));
                            AddInstance(new TimeRangedEvent(T(1), T(1), () =>
                            {
                                linedown.y1 += beattime * beattime;
                                linedown.y2 += beattime * beattime;
                                beattime -= 0.08f;
                            }));
                        });
                        DelayBeat((a * 4) + 2, () =>
                        {
                            CreateEntity(lineup);
                            float beattime = 0;
                            AddInstance(new TimeRangedEvent(0, T(1), () =>
                            {
                                lineup.y1 += beattime * beattime;
                                lineup.y2 += beattime * beattime;
                                beattime += 0.08f;
                            }));
                            AddInstance(new TimeRangedEvent(T(1), T(1), () =>
                            {
                                lineup.y1 -= beattime * beattime;
                                lineup.y2 -= beattime * beattime;
                                beattime += 0.08f;
                            }));
                        });
                    }

                }
                #endregion
                #region Dreadnaught + Travel at sunset
                //Dread
                if (InBeat(504))
                {
                    ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                    ScreenDrawing.UIColor = new(190, 190, 190);


                    RunEase((s) => { HPBar.HPExistColor = Color.Lerp(Color.Lerp(Color.Red, Color.Black, 0.4f), Color.DarkMagenta, s); }, LinkEase(Stable(0, 0), EaseOut(T(8f), 1, ES.Linear)));
                    UISettings.HPShowerPos += new V(100, 0);
                    ScreenDrawing.ThemeColor = new(105, 0, 0);
                    SetSoul(2);
                    DreadStep = FightResources.Shaders.StepSample;
                    Dreadpro1 = ActivateShader(FightResources.Shaders.StepSample, 0.95f);
                    Dreadsplit = new ScreenDrawing.Shaders.RGBSplitting(0.93259f) { Disturbance = false };
                    ScreenDrawing.SceneRendering.InsertProduction(Dreadsplit);
                    SetPlayerBoxMission(0);
                    InstantTP(new V(320, 60));
                    InstantSetBox(new V(320, 240), 180, 500);
                    float y = 60;
                    ForBeat(8, () =>
                    {
                        InstantTP(new(Functions.Heart.Centre.X, y));
                        HeartAttribute.Gravity = 0;
                        HeartAttribute.JumpTimeLimit = 0;
                        if (GameStates.IsKeyDown(InputIdentity.MainDown))
                        {
                            y += 0.35f;
                        }
                        if (GameStates.IsKeyDown(InputIdentity.MainUp))
                        {
                            y -= 0.35f;
                        }

                    });
                    ForBeat(8, () =>
                    {
                        DreadStep.CentreX = Functions.Heart.Centre.X;
                        DreadStep.CentreY = Functions.Heart.Centre.Y;
                    });
                    ScreenDrawing.ScreenScale = 1f;
                    DreadStep.Intensity = 0.1f;
                    HeartAttribute.Speed = 2.1f;
                    Dreadsplit.Intensity = 1 + 2f;

                }
                if (InBeat(504))
                {
                    CreateEntity(new Boneslab(180, 120, T(6), T(2)) { ColorType = 0 });
                    RegisterFunctionOnce("RightB", () =>
                    {
                        PlaySound(Sounds.pierce);
                        float rd = Rand(1, 2);
                        if (rd == 1)
                        {
                            DrawingUtil.CrossBone(new V(Rand(320 - 10, 320 + 50), 500), new V(0, -8), 30, 1, 2);
                        }
                        else if (rd == 2)
                        {
                            CreateBone(new CustomBone(new V(Rand(320 - 10, 320 + 50), 500), Motions.PositionRoute.linear, 0, 40)
                            {
                                PositionRouteParam = new float[] { 0, -7 },
                                ColorType = 0
                            });
                        }
                    });
                    RegisterFunctionOnce("Kick1", () =>
                    {

                        float rot = Rand(9, 20);
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270 + rot)), rot, 40) { ColorType = 0 });
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270)), 180, 40) { ColorType = 0 });
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270 - rot)), -rot, 40) { ColorType = 0 });
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("Kick2", () =>
                    {
                        float rot = Rand(9, 20);
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270 + rot / 2)), rot / 2, 40) { ColorType = 0 });
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270 - rot / 2)), -rot / 2, 40) { ColorType = 0 });
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270 + rot * 1.5f)), rot * 1.5f, 40) { ColorType = 0 });
                        CreateBone(new CustomBone(new(Functions.Heart.Centre.X, 520), CentreEasing.Linear(GV(7.5f, 270 - rot * 1.5f)), -rot * 1.5f, 40) { ColorType = 0 });
                        PlaySound(Sounds.pierce);
                    });
                    float height = 0;
                    RunEase((s) =>
                    {
                        height = s;
                    }, LinkEase(Stable(T(4), 40), EaseOut(T(0.5f), -15, ES.Linear),
                                Stable(0, 0), EaseOut(T(0.5f), 35, ES.Linear)));
                    for (float i = 0; i < T(4); i++)
                    {
                        AddInstance(new InstantEvent(2 * i, () =>
                        {
                            Bone b1 = new();
                            Bone b2 = new();
                            b1.ContainTag("a");
                            b2.ContainTag("a");
                            CreateBone(b1 = new LeftBone(true, 9, height) { ColorType = 0, MarkScore = false, });
                            CreateBone(b2 = new RightBone(true, 9, height) { ColorType = 0, MarkScore = false });
                        }));
                    }
                    RegisterFunctionOnce("SideBone", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            DelayBeat(i, () =>
                            {
                                PlaySound(Sounds.pierce);
                                float rd = Rand(40, 90);
                                CreateBone(new LeftBone(true, 7, rd) { ColorType = 0 });
                                CreateBone(new RightBone(true, 7, 130 - rd) { ColorType = 0 });
                                DelayBeat(0.125f, () =>
                                {
                                    CreateBone(new LeftBone(true, 7, rd) { ColorType = 0 });
                                    CreateBone(new RightBone(true, 7, 130 - rd) { ColorType = 0 });
                                });
                            });
                        }
                    });
                    RegisterFunctionOnce("atk2", () =>
                    {
                        float length = 0;
                        ValueEasing.EaseBuilder len = new();
                        len.Insert(0, ValueEasing.Stable(80));
                        len.Insert(T(0.5f), ValueEasing.Linear(80, 100, T(0.5f)));
                        len.Run((s) =>
                        {
                            length = s;
                        });
                        for (int i = 0; i < 4; i++)
                        {
                            AddInstance(new InstantEvent(i * 1, () =>
                            {
                                CreateBone(new LeftBone(true, 12, length) { ColorType = 0 });
                            }));
                        }
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("atk3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        float length = 0;
                        ValueEasing.EaseBuilder len = new();
                        len.Insert(0, ValueEasing.Stable(80));
                        len.Insert(T(0.5f), ValueEasing.Linear(80, 100, T(0.5f)));
                        len.Run((s) =>
                        {
                            length = s;
                        });
                        for (int i = 0; i < 4; i++)
                        {
                            AddInstance(new InstantEvent(i * 1, () =>
                            {
                                CreateBone(new RightBone(true, 12, length) { ColorType = 0 });
                            }));
                        }
                    });
                    CreateChart(T(0), T(2), 5, new string[]
                    {
                    "Kick1","","RightB","",  "Kick2","","RightB","",
                    "Kick1","","RightB","",  "Kick2","","RightB","",
                    "SideBone","","Kick1","",  "","","Kick2","",
                    "atk2","","atk3","",  "atk2","","atk3","",
                    });
                }
                if (InBeat(511.5f))
                { WhiteOut(T(0.5f)); }
                //TaS
                if (InBeat(512))
                {
                    NameShower.level = 5.ToString();
                    BadShaderrend = ScreenDrawing.ActivateShaderBack(shaderGrid, 0.9919f);
                    HPBar.HPLoseColor = new(255, 0, 0);
                    HPBar.HPExistColor = new(0, 255, 0);
                    UIColor = new(255, 255, 255);
                    ThemeColor = new(255, 255, 255);
                    Dreadpro1.Dispose();
                    Dreadsplit.Dispose();
                    Bone[] b = GetAll<Bone>();
                    foreach (Bone bs in b) { bs.Dispose(); }
                    RunEase((s) => { ScreenDrawing.ScreenScale = s; }, LinkEase(Stable(0, 2f), EaseOut(T(4f), -1f, ES.Cubic)));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0f), EaseOut(T(4f), 360f, ES.Cubic)));
                    shaderGrid.BlendColor = Color.Transparent;
                    shaderGrid.BlendColor = new Color(1.0f, 0.7f, 0.4f) * 0.3257f;
                    shaderGrid.SideColor = new Color(1.0f, 0.7f, 0.4f);
                    shaderGrid.Intensity1 = 0.05f;
                    shaderGrid.Intensity2 = 1;
                    shaderGrid.Intensity3 = 1.0f;
                    shaderGrid.GlowIntensity = 0f;
                    ScreenDrawing.BoxBackColor = Color.Black * 0.5f;
                    SetSoul(Souls.RedSoul);
                    Functions.Heart.Speed = 3;
                    Functions.Heart.InstantSetRotation(180);
                    RunEase((s) =>
                    {
                        InstantTP(new(Functions.Heart.Centre.X, s));
                    },
                            LinkEase(EaseIn(T(1), 380, 360, ES.Sine),
                            EaseIn(T(3), 360, 300, ES.Linear)));
                    InstantSetBox(270 - 42, 370 + 42, -5, 485);
                }
                if (InBeat(512))
                {
                    RegisterFunctionOnce("BoneGA", () =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            float h = i;
                            LeftBone b1 = new(false, 640 + i * -16, 0, 35 + i * 0.3f) { MarkScore = false };
                            RightBone b2 = new(false, 640 + i * -16, 0, 35 + i * 0.3f) { MarkScore = false };
                            CreateBone(b1);
                            CreateBone(b2);
                            RunEase(k => b1.Speed = b2.Speed = k, EaseOut(T(1), 0, 7.8f, ES.Linear), Stable(1, 7.8f));
                            DelayBeat(4, () =>
                            {
                                RunEase((s) => { b1.Length = b2.Length = s; }, LinkEase(Stable(0, b1.Length), EaseOut(T(4f), 0 - b1.Length, ES.Expo)));
                            });
                        }
                    });
                    RegisterFunctionOnce("CrossL", () =>
                    {
                        float angle = 0.0f;
                        for (int i = 0; i < 4; i++)
                        {
                            angle += 180.0f / 4;
                            CreateBone(new CustomBone(
                                Add(LinkEase(EaseOut(T(2f), new(-110, 0), V.Zero, ES.Back),
                                    Stable(T(1), V.Zero)
                                ),
                                InfLinear(new V(270 - 46, -10), new(0, 8))),
                                InfLinear(angle, 4), 200));
                        }
                    });
                    RegisterFunctionOnce("CrossR", () =>
                    {
                        float angle = 0.0f;
                        for (int i = 0; i < 4; i++)
                        {
                            angle += 180.0f / 4;
                            CreateBone(new CustomBone(
                                Add(LinkEase(EaseOut(T(2f), new(110, 0), V.Zero, ES.Back),
                                    Stable(T(1), V.Zero)
                                ),
                                InfLinear(new V(370 + 46, -10), new(0, 8))),
                                InfLinear(angle, -4), 200));
                        }
                    });
                    RegisterFunctionOnce("BoneWallX", () =>
                    {
                        for (int i = 0; i < 5; i += 2)
                        {
                            LeftBone b = new(false, i * (-16), 18.0f, 194)
                            {
                                ColorType = 2,
                                LengthLerpScale = 0.1637f
                            };
                            CreateBone(b);
                        }
                        for (int i = 1; i < 5; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 18.0f, 194)
                            {
                                ColorType = 2,
                                LengthLerpScale = 0.1637f
                            };
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWallX2", () =>
                    {
                        for (int i = 0; i < 5; i += 2)
                        {
                            LeftBone b = new(false, i * (-16), 18.0f, 194)
                            {
                                ColorType = 2,
                                LengthLerpScale = 0.1637f
                            };
                            CreateBone(b);
                        }
                        for (int i = 1; i < 5; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 18.0f, 194)
                            {
                                ColorType = 2,
                                LengthLerpScale = 0.1637f
                            };
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall1", () =>
                    {
                        for (int i = 0; i < 3; i += 2)
                        {
                            LeftBone b = new(false, i * (-16), 8.0f, 194)
                            {
                                ColorType = 1,
                                LengthLerpScale = 0.1137f
                            };
                            CreateBone(b);
                        }
                        for (int i = 1; i < 3; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 8.0f, 194)
                            {
                                ColorType = 1,
                                LengthLerpScale = 0.1137f
                            };
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall2", () =>
                    {
                        for (int i = 0; i < 3; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 8.0f, 194);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                        for (int i = 1; i < 3; i += 2)
                        {
                            LeftBone b = new(false, i * (-16), 8.0f, 194);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall3", () =>
                    {
                        for (int i = 0; i < 4; i += 2)
                        {
                            RightBone b = new(false, i * (-16) - 10, 12.0f, 194);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1237f;
                            CreateBone(b);
                        }
                        for (int i = 1; i < 4; i += 2)
                        {
                            LeftBone b = new(false, i * (-16) - 10, 12.0f, 194);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1237f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWallBr1", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LeftBone b = new(false, i * -16, 8, 70 + i * 8);
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWallBr2", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            RightBone b = new(false, i * -16, 8, 70 + i * 8);
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWallBr3", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            var ease = Add(LinkEase(EaseOut(T(0.9f), new(-110, 0), V.Zero, ES.Back),
                                    Stable(T(1), V.Zero)
                                ),
                                InfLinear(V.Zero, new(0, 8)));
                            float rot = Rand(60, 80);
                            CustomBone b = new(new V(270 - 42, i * -16), ease.Easing, -rot, 165);
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWallBr4", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            var ease = Add(LinkEase(EaseOut(T(0.9f), new(110, 0), V.Zero, ES.Back),
                                    Stable(T(1), V.Zero)
                                ),
                                InfLinear(V.Zero, new(0, 8)));
                            float rot = Rand(60, 80);
                            CustomBone b = new(new V(370 + 42, i * -16), ease.Easing, rot, 165);
                            CreateBone(b);
                        }
                    });

                    RegisterFunctionOnce("s", () =>
                    {
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("BoneMid", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LeftBone b = new(false, i * -16, 8, 45 + i * 11);
                            b.LengthLerpScale = 0.1537f;
                            CreateBone(b);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            RightBone b = new(false, i * -16, 8, 45 + i * 11);
                            b.LengthLerpScale = 0.1537f;
                            CreateBone(b);
                        }
                    });
                    CreateChart(0, T(2), 16, new string[]
                    {
                        "BoneGA(BoneWallBr1)(s)","","(BoneWallBr2)(s)","(BoneWall2)",  "BoneMid(s)","","CrossL(s)","",
                        "BoneWall1(s)","","CrossR","", "BoneMid(s)","","BoneMid(s)","",
                        "","","","", "","","","",
                        "","","","", "","","","",

                    });
                }
                if (InBeat(516))
                {
                    RunEase((s) => shaderGrid.Intensity2 = s,
                                EaseOut(T(2), 1.0f, 1.2f, ES.Elastic),
                                EaseOut(T(2), 1.2f, 1.86f, ES.Quad));
                }
                if (InBeat(516))
                {
                    RemoveUI2();
                    RemoveUI1();
                    Shader shader = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\ball2"));
                    shader.Parameters["intensity"].SetValue(0);
                    RenderProduction shaderProduction = ScreenDrawing.ActivateShader(shader, 0.98352f);
                    Shader shader2 = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\WhiteOut"));
                    shader2.Parameters["set"].SetValue(0);
                    RenderProduction shaderProduction2 = ScreenDrawing.ActivateShader(shader2, 0.8533421f);
                    Shader shader3 = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\luminescence"));
                    shader3.Parameters["set"].SetValue(0);
                    RenderProduction shaderProduction3 = ScreenDrawing.ActivateShader(shader2, 0.650392f);
                    production1 = ScreenDrawing.ActivateShader(FightResources.Shaders.StepSample, 0.4f);
                    production2 = ScreenDrawing.ActivateShader(FightResources.Shaders.Scale, 0.99325f);
                    FightResources.Shaders.StepSample.Intensity = 0; FightResources.Shaders.Scale.Intensity = 0;
                    FightResources.Shaders.StepSample.CentreX = 320; FightResources.Shaders.StepSample.CentreY = 240;
                    RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                        EaseOut(T(2), 0, 0.2f, ES.Cubic),
                        EaseOut(T(2), 0.2f, 0.4f, ES.Cubic));
                    RunEase((s) => { FightResources.Shaders.Scale.Intensity = s; },
                        EaseOut(T(2), 0, -0.08f, ES.Cubic),
                        EaseOut(T(2), -0.08f, -0.17f, ES.Cubic));
                    SetGreenBox();
                    SetSoul(1);
                    TP(320, 240);



                    Functions.Heart.InstantSetRotation(0);
                    RunEase((s) => { ScreenScale = s; },
                        EaseOut(T(4f), 1, 2, ES.Cubic));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        EaseOut(T(2f), 0, 4, ES.Elastic),
                        EaseOut(T(2f), 4, 0, ES.Cubic));
                    DelayBeat(3, () =>
                    {
                        RunEase((s) => { shader.Parameters["intensity"].SetValue(s); },
                        EaseIn(T(2f), 0, 700, ES.Cubic),
                        EaseOut(T(1f), -700, ES.Cubic));
                        RunEase((s) => { shader3.Parameters["set"].SetValue(s); },
                        EaseIn(T(2f), 1, 12f, ES.Cubic),
                        EaseOut(T(4f), -5f, ES.Cubic));
                        RunEase((s) => { shader2.Parameters["set"].SetValue(s); },
                        EaseIn(T(2f), 0, 0.7f, ES.Cubic),
                        EaseOut(T(4f), -0.5f, ES.Cubic));
                        var blur = new ScreenDrawing.Shaders.Blur(0.3f);
                        ScreenDrawing.SceneRendering.InsertProduction(blur);
                        RunEase((s) => { blur.Sigma = s; }, EaseOut(T(2f), 0, 15, ES.Cubic),
                                                            EaseOut(T(4f), 15, 1, ES.Cubic));
                        DelayBeat(2, () =>
                        {
                            ScreenDrawing.ScreenAngle = 180;
                            RemoveUI1();
                            RemoveUI2();
                        });
                        RunEase((s) => { ScreenScale = s; },
                            EaseOut(T(2f), ScreenScale, ScreenScale, ES.Cubic),
                            EaseOut(T(1f), ScreenScale, 1, ES.Cubic));
                        DelayBeat(6, () =>
                        {
                            shaderProduction.Dispose();

                        });
                        DelayBeat(29, () =>
                        {
                            blur.Dispose();
                            shaderProduction2.Dispose();
                            shaderProduction3.Dispose();
                        });
                    });
                    DelayBeat(5, () =>
                    {
                        MasterAlpha = 1;
                        production1.Dispose();
                        production2.Dispose();
                        BadShaderrend.Dispose();
                    });
                }
                if (InBeat(514))
                {
                    RegisterFunctionOnce("Shake", () =>
                    {
                        float rand = 7;
                        AddInstance(new TimeRangedEvent(T(4), () =>
                        {
                            rand -= 7f / 1500;
                            float x = Rand(-rand, rand);
                            float y = Rand(-rand, rand);
                            ScreenPositionDelta = new V(x, y);
                            FightResources.Shaders.StepSample.CentreX = x + 320;
                            FightResources.Shaders.StepSample.CentreY = y + 240;
                        }));

                    });
                    CreateChart(T(4), T(2), 4, new string[]
                        {"#2#$21(#2#$0)(Shake)", });
                }
                #endregion*/
                
                #region EtherStrike + Eternal Spring Dream
                if (InBeat(520))
                {
                    ScreenAngle = 180;
                    BoxBackColor = Color.Black;
                    Heart key2 = Functions.Heart;
                    Player.Heart.Shield shield =
                    new Player.Heart.Shield(3, key2)
                    {
                        UpdateKeys = new InputIdentity[4]
                            {
                            InputIdentity.FourthRight,InputIdentity.FourthDown,
                            InputIdentity.FourthLeft,InputIdentity.FourthUp
                            }
                    };
                    key2.Shields.AddShield(shield);
                    Player.Heart.Shield shield2 =
                    new Player.Heart.Shield(2, key2)
                    {
                        UpdateKeys = new InputIdentity[4]
                            {
                            InputIdentity.ThirdRight,InputIdentity.ThirdDown,
                            InputIdentity.ThirdLeft,InputIdentity.ThirdUp
                            }
                    };
                    key2.Shields.AddShield(shield2);
                    DelayBeat(20, () =>
                    {
                        Functions.Heart.Shields.RemoveShield(shield);
                        Functions.Heart.Shields.RemoveShield(shield2);
                    });
                    CreateGB(new GreenSoulGB(T(2), 1, 3, T(3)));
                    CreateGB(new GreenSoulGB(T(2), 3, 2, T(3)));
                    CreateGB(new GreenSoulGB(T(6), 0, 1, T(3)));
                    CreateGB(new GreenSoulGB(T(6), 2, 0, T(3)));
                    CreateGB(new GreenSoulGB(T(10), 3, 1, T(3)));
                    CreateGB(new GreenSoulGB(T(10), 2, 3, T(3)));
                    CreateGB(new GreenSoulGB(T(14), 1, 1, T(1.5f)));
                    CreateGB(new GreenSoulGB(T(14), 2, 2, T(1.5f)));
                    CreateGB(new GreenSoulGB(T(14), 3, 3, T(1.5f)));
                    CreateGB(new GreenSoulGB(T(14), 0, 0, T(1.5f)));
                    DelayBeat(2, () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate180(T(4));

                    });
                    DelayBeat(6, () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.T(3));
                        Rotate(15, game.T(3));
                    });
                    DelayBeat(10, () =>
                    {
                        float intensity2 = 30f;
                        for (int i = 0; i < 32; i++)
                        {
                            AddInstance(new InstantEvent(T(1 / 8f * i), () =>
                            {
                                ScreenDrawing.CameraEffect.Convulse(intensity2, game.T(1f / 8f), i % 2 == 0);
                                intensity2 -= 30f / 32f;
                            }));
                        }
                    });
                    DelayBeat(14, () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.T(3));
                        ScreenDrawing.CameraEffect.Rotate(360, game.T(3));
                        
                    });
                }
                if (InBeat(536))
                {
                    MakeFlicker(C.White);
                    for (int i = 0; i < 4; i++)
                        CreateEntity(new Boneslab(i * 90, 10, 40, (int)T(16)));
                    SetSoul(5);
                    SetBox(240, 300, 300);
                    Functions.Heart.Gravity = 3.0f;
                    NameShower.level = 4.ToString();
                }
                if (InBeat(536))
                {
                    for (int i = 0; i < 10; i++)
                        for (int j = -2; j < 7; j++)
                        {
                            CreateBone(new CustomBone(new V(120 + i * 50, 170 + j * 50), Motions.PositionRoute.stableValue, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                            {
                                LengthRouteParam = new float[] { 8, T(128 + 120) },
                                PositionRouteParam = new float[] { },
                                RotationRouteParam = new float[] { 2, 3 },
                                AlphaIncrease = true
                            });
                        }
                }
                if (InBeat(526, 544) && At0thBeat(1))
                {
                    CreateBone(new CustomBone(new V(60, 270), Motions.PositionRoute.linear, 90, 18)
                    {
                        PositionRouteParam = new float[] { 2f, 0 },
                        Tags = new string[] { "a" },
                        Alpha = 0
                    });
                    CreateBone(new CustomBone(new V(580, 220), Motions.PositionRoute.linear, 90, 18)
                    {
                        PositionRouteParam = new float[] { -2f, 0 },
                        Tags = new string[] { "a" },
                        Alpha = 0
                    });
                    CreateBone(new CustomBone(new V(580, 320), Motions.PositionRoute.linear, 90, 18)
                    {
                        PositionRouteParam = new float[] { -2f, 0 },
                        Tags = new string[] { "a" },
                        Alpha = 0
                    });

                }
                if (InBeat(536, 544))
                {
                    foreach (Bone b in GetAll<Bone>("a"))
                    {
                        b.Alpha = 1;
                    }
                }
                if (InBeat(536, 548))
                {
                    float yc = 270 + Sin((Gametime - T(5) * 360)) * 110;
                    float xc = 95;
                    if (Gametime >= T(542))
                        xc = 240 - Cos((Gametime - T(542)) / T(24) * 360) * (240 - 95);
                    InstantSetBox(xc, xc + 160, yc - 65, yc + 65);

                }
                #endregion*/
                #region BadApple!!
                if (InBeat(548))
                {
                    RemoveCustom("inyou");
                    HeartAttribute.DamageTaken = 1;
                    HeartAttribute.KRDamage = 4;
                    Functions.Heart.Gravity = 9.8f;
                    BSet.SideColor = C.White;
                    HPBar.AreaOccupied = new(0, -5000, 0, 0);
                    SetSoul(SoulOriginal.WhiteSoul, false);
                    Functions.Heart.Speed = 0;
                    Shader shader2 = nega;
                    Shaderblack(new(320, 240), 640, 480, 0, shader2);
                    RenderProduction shaderProduction = ScreenDrawing.ActivateShader(shader2, 0.990f);
                    BSet.timestop = true;
                    BSet.final = true;
                    NameShower.name = "e78b90e381aee789a1e4b8b9";
                    //again = false;
                    HPBar.HPLoseColor = Color.Black * 0;
                    HPBar.HPExistColor = Color.White * 0;
                    DelayBeat(4, () => { shaderProduction.Dispose(); });
                    int ins = 0;
                    SongFightingScene scene = GameStates.CurrentScene as SongFightingScene;
                    scene.Accuracy.Dispose();

                    BSet.Windowname = "";
                    for (int i = 0; i < 10; i++)
                    {
                        DelayBeat(0.25f * i, () =>
                        {

                            char[] cha = "ろ　く　で　な　し　".ToCharArray();
                            string st = BSet.Windowname + $"{cha[ins]}";
                            BSet.Windowname = st;
                            ins++;
                        });
                    }
                }
                if (InBeat(552))
                {
                    SceneRendering.InsertProduction(SidePro=new Side(0.9999837582f));
                    Shader PT = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\ball"));
                    PT.Parameters["icolor"].SetValue(C.White.ToVector3());
                    PT.Parameters["iy"].SetValue(240);
                    PT.Parameters["intensity"].SetValue(0);
                    PT.Parameters["intensity2"].SetValue(1);
                    ActivateShader(PT,0.9984211156f);
                    Shader back = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BackGround1"));
                    back.Parameters["position"].SetValue(new V(0));
                    back.Parameters["R"].SetValue(1);
                    back.Parameters["G"].SetValue(1);
                    back.Parameters["B"].SetValue(1);
                    back.Parameters["Alpha"].SetValue(0);
                    ActivateShader(back,0.998482f);
                    Glitch = new GlitchPro(0.94f);
                    SceneRendering.InsertProduction(Glitch);
                    BSet.Windowname = "何もかも終わり";
                    //BSet.FullScreen = true;
                    shaderProduction1 = ActivateShader(n, 0.95829f);
                    n.circmode = true;
                    n.intensity = 0.003f;
                    BSet.timestop = false;
                    ResetBarrage();
                    InstantTP(320, 240);
                    HeartAttribute.MaxHP = 92;
                    HeartAttribute.KR = true;
                    HeartAttribute.DamageTaken = 1;
                    BadShader = nega;
                    Shaderblack(new(320, 240), 640, 480, 0, BadShader);
                    Dreadpro1 = ScreenDrawing.ActivateShader(BadShader, 0.972f);
                    var time = 0;
                    var r = 3.5f;
                    var temp = true;
                    Flash();
                    ForBeat120(1,()=>
                    {
                        time++;
                        if (time > r) 
                        {
                            Shaderblack(new(320, 240), 640 * BoolToInt(temp), 480 * BoolToInt(temp), 0, BadShader);
                            BSet.SideColor = !temp ? C.White : C.Black;
                            temp = !temp;
                            time = 0;
                        }
                    });
                    shaderProduction3 = ActivateShader(FightResources.Shaders.StepSample, 0.02f);
                    shaderProduction4 = ActivateShader(FightResources.Shaders.Seismic, 0.99993f);
                    FightResources.Shaders.StepSample.Intensity = 0;
                    FightResources.Shaders.StepSample.CentreY = 240;
                    FightResources.Shaders.StepSample.CentreX = 320;
                    Shader shader = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\WhiteBlack"));
                    shaderProduction5 = ScreenDrawing.ActivateShader(shader, 0.888f);

                    for (int i = 0; i < 3; i++) { PlaySound(Sounds.explode); }
                    float vec = 200;
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        ScreenPositionDelta = new(R(vec * -1, vec), R(vec * -1, vec));
                        vec *= 0.99f;
                    }));
                    AddInstance(new TimeRangedEvent(T(16), T(12), () =>
                    {
                        ScreenPositionDelta = new(R(vec * -1, vec), R(vec * -1, vec));
                    }));
                    AddInstance(new TimeRangedEvent(T(32), T(36), () =>
                    {

                        ScreenPositionDelta = new(R(vec * -1, vec), R(vec * -1, vec));
                    }));
                    AddInstance(new TimeRangedEvent(T(32 + 48), T(8), () =>
                    {
                        vec = 23;
                        ScreenPositionDelta = new(R(vec * -1, vec), R(vec * -1, vec));
                    }));
                    AddInstance(new TimeRangedEvent(T(40 + 48), T(8), () =>
                    {
                        vec = 28;
                        ScreenPositionDelta = new(R(vec * -1, vec), R(vec * -1, vec));
                    }));
                    BSet.timestop = false;
                    BSet.final = true;
                    InstantSetBox(240, 645, 485);
                    NameShower.nameAlpha = 0;
                    UIColor = Color.Transparent;
                    ThemeColor = Color.Transparent;
                    HPBar.HPLoseColor = Color.Transparent;
                    HPBar.HPExistColor = Color.Transparent;
                    HPBar.AreaOccupied = new(0,-5000,0,0);
                    BadBreak();
                    BoxBackColor = (Color.Transparent);
                    RunEase((s) =>
                    {
                        Functions.Heart.InstantSetRotation(s);
                    },
                        LinkEase(Stable(0, 0), EaseOut(T(16f), 360 * 10, ES.Expo)));
                    RunEase((s) => { ScreenDrawing.ScreenScale = s; },
                        LinkEase(Stable(0, 3), EaseOut(T(4f), -2, ES.Expo)));
                    DelayBeat(16, () =>
                    {
                        Functions.Heart.InstantSetRotation(0);
                    });

                }
                if (InBeat(552, 552 + 27) && At0thBeat(1f))
                {
                    Line l = new(LinkEase(Stable(0, new V(0, 480)), EaseOut(T(2), new V(0, -960), Li)), Stable(0, 20)) { Width = 5 };
                    CreateEntity(l);
                    l.DelayDispose(T(4));
                    DelayBeat(0.5f, () =>
                    {
                        Line l = new(LinkEase(Stable(0, new V(640, 480)), EaseOut(T(2), new V(0, -960), Li)), Stable(0, -20)) { Width = 5 };
                        CreateEntity(l);
                        l.DelayDispose(T(4));
                    });
                }
                #region Final Barrage
                if (InBeat(552, 552 + 27f))
                {
                    BSet.col = true;
                    Line l = new(LinkEase(Stable(0, new V(Rand(0f, 640), 600)), EaseOut(T(Rand(0.5f, 1.5f)), new V(0, -800), Li)), Stable(0, 90), Stable(0, Rand(15f, 30f))) { DrawingColor = C.Lerp(C.White, C.Black, Rand(0f, 1f)), Width = 1 };
                    CreateEntity(l);
                    l.DelayDispose(T(2));
                    EfEn ef = new(new V(Rand(0f, 640f), 500), Sprites.square) { Color = CW };
                    CreateEntity(ef);
                    ef.vec2Ease(true, ef.centre, new V(0, -Rand(350f, 700f)), T(2), Cu);
                    ef.AlphaEase(true, 1, 0, T(1), T(1), Li);
                    ef.rot = Rand(0f, 360f);
                    ef.AutoDis(T(2));
                    DelayBeat(0, () =>
                    {
                        EfEn ef = new(new V(Rand(0f, 640f), -20), Sprites.player) { Color = CW };
                        CreateEntity(ef);
                        ef.vec2Ease(true, ef.centre, new V(0, Rand(350f, 700f)), T(2), Cu);
                        ef.AlphaEase(true, 1, 0, T(1), T(1), Li);
                        ef.rot = Rand(0f, 360f);
                        ef.AutoDis(T(2));
                    });
                }
                if (InBeat(552))
                {
                    var rand1 = Rand(5f,25f);
                    var time = 0;
                    var Single = 1;
                    ForBeat120(27.5f, () => 
                    {
                        var s = new Shader(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\flash"));
                        time++;
                        if (time > rand1)
                        {
                            s.Parameters["intensity"].SetValue(MathHelper.Clamp(Single,0,1));
                            Single *= -1;
                            time = 0;
                            rand1 = Rand(5f,25f);
                        }
                    });
                    DelayBeat(28, () => 
                    {
                        var s = new Shader(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\flash"));
                        s.Parameters["intensity"].SetValue(0);
                    });
                    V vec = new(320, 240);
                    for (int i = 0; i < 36; i++)
                    {
                        RBones b = new RBones();
                        CreateBone(b);
                        float rot = i * 10;
                        float rand = i % 2 == 0 ? 50 : 75;
                        AddInstance(new TimeRangedEvent(T(28), () =>
                        {
                            Rb(b, vec + GV(rand, rot), vec + GV(500, rot), 0, 0);
                            rot += 8;
                        }));
                        DelayBeat(28, () =>
                        {
                            RunEase((s) => { b.Alpha = s; }, LinkEase(Stable(0, 1), EaseOut(T(4f), 1, ES.Cubic)));
                            b.Dispose();
                        });
                    }
                    RegisterFunctionOnce("zoom", () =>
                    {
                        SetSoul(SoulOriginal.WhiteSoul);
                        RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 3), EaseOut(T(4f), -2, ES.Cubic)));
                        RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                            LinkEase(Stable(0, 0.2f), EaseOut(T(4f), -0.2f, ES.Cubic)));
                    });
                    RegisterFunctionOnce("zoom2", () =>
                    {
                        RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(T(2), 3), EaseOut(T(4f), -2, ES.Cubic)));
                        RunEase((s) => { FightResources.Shaders.StepSample.Intensity = s; },
                            LinkEase(Stable(0, 0.2f), EaseOut(T(6f), -0.2f, ES.Cubic)));
                    });
                    RegisterFunctionOnce("rot", () =>
                    {
                        ImageEntity em = new(Loader.Load<Texture2D>("Musics\\BadAppleRE\\BreakScreen\\BlackMove_00000")) { controlLayer = sps().UISurface };

                        CreateEntity(em);

                        em.Depth = 0f;
                        em.Alpha = 0;
                        em.Centre = new(320, 240);
                        em.Scale = 2;
                        int sp = 0;
                        for (int i = 0; i < 105; i++)
                        {
                            AddInstance(new InstantEvent(i * hz, () =>
                            {
                                em.Scale = 1;
                                sp++;
                                em.Image = Resources.BadAppleRE.BreakScreen[sp];
                                em.Alpha = 1f;
                            }));
                        }
                        FightResources.Shaders.DislocationX.Intensity = 0;
                        FightResources.Shaders.DislocationX.Time = 0;
                        FightResources.Shaders.DislocationX.Speed = 0;
                        foreach (Line b in GetAll<Line>()) { b.Dispose(); }
                        foreach (EfEn b in GetAll<EfEn>()) { b.Dispose(); }
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0), EaseIn(T(4.2f), -720, ES.Cubic),
                                                                                    Stable(0, 0), EaseOut(T(0.1f), 0, ES.Cubic)));
                        RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(T(2),1),EaseIn(T(2),1,5,ES.Expo),EaseOut(T(2),0,-4,ES.Elastic)));
                    });
                    RegisterFunctionOnce("final", () =>
                    {
                        n.intensity = 0.00001f;
                        shaderProduction3.Dispose();
                        ImageEntity em = new(Loader.Load<Texture2D>("Musics\\BadAppleRE\\BreakScreen\\BlackMove_00000")) { controlLayer = sps().UISurface };
                        AddInstance(new InstantEvent(1, () => { Shaderblack(640, 640, BadShader); }));
                        Glitch.Dispose();
                    });
                    DelayBeat(16, () =>
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, 10, 0.75f, 375, i * 22.5f));
                        }
                        for (int i = 0; i < 36; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, 10, 0.75f, 425, i * 10));
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, 10, 0.75f, 475, i * 6));
                        }
                        for (int i = 0; i < 72; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, 10, 0.75f, 525, i * 5));
                        }
                        for (int i = 0; i < 120; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, 10, 0.75f, 575, i * 3));
                        }
                        for (int i = 0; i < 120; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, -10, 0.75f, 625, i * 3));
                        }
                        for (int i = 0; i < 72; i++)
                        {
                            CreateEntity(new CircleSpear(Functions.Heart.Centre, 10, 0.75f, 675, i * 5));
                        }
                        DelayBeat(16, () =>
                        {
                            CircleSpear[] sper = GetAll<CircleSpear>();
                            foreach (CircleSpear s in sper)
                            { s.Dispose(); }
                        });
                    });
                    CreateChart(T(0), T(2), 0, new string[]
                    {
                       "","","","",  "","","","",
                       "","","","",  "","","","",
                       "zoom","","","",  "","","","",
                       "","","","",  "","","","",
                       "zoom","","","",  "","","","",
                       "","","","",  "","","","",
                       "zoom","","","",  "","","","",
                       "zoom","","","",  "zoom","","","",
                       "zoom","","","", "","","","",
                       "","","","",  "","","","",
                       "zoom","","","","","","","",
                       "","","","","","","","",
                       "zoom","","","","","","","",
                       "","","","","","","","(rot)",
                       "(final)","","","", "","","","",
                       "","","","", "","","","",
                       "",
                    });

                }
                if (InBeat(552 + 32, 552 + 48) && At0thBeat(2))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        CreateEntity(new CenB(WCentre, 72 * i + Gametime, 1, 0.5f, 400, 20) { Prot = 0, Srot = 25 });
                        CreateEntity(new CenB(WCentre, 72 * i + Gametime, 1.2f, -0.5f, 400, 20) { Prot = 2, Srot = 25 });
                    }
                }
                if (InBeat(552 + 96, 552 + 96 + 64 + 19))
                {
                    n.intensity = Sin(Gametime) * 0.005f;
                }
                if (InBeat(552 + 32))
                {
                    RotationBone[] b = new RotationBone[12];
                    for (int i = 0; i < 12; i++)
                    {
                        if (i < 3)
                            b[i] = new(new V(0, 0), i * 60, 5);
                        else if (i < 6)
                            b[i] = new(new V(640, 0), i * 60, 5);
                        else if (i < 9)
                            b[i] = new(new V(0, 480), i * 60, 5);
                        else
                            b[i] = new(new V(640, 480), i * 60, 5);
                    }
                    CreateEntity(b);
                    RunEase((s) =>
                    {
                        foreach (Bone bs in b)
                        {
                            bs.Length = s;
                            bs.ColorType = 0;
                        }
                    },
                        LinkEase(Stable(0, 0), EaseOut(T(4), 100, Cu), Stable(T(12), 0), EaseOut(T(4), -100, Cu)));
                    DelayBeat(20, () =>
                    {
                        foreach (Bone bs in b)
                        {
                            bs.Dispose();
                        }
                    });
                    Functions.Heart.Speed = 2.5f;
                    RunEase(s => FightResources.Shaders.Seismic.Progress = s,
                        EaseOut(T(7f), 0f, 1f, Cu));
                    RunEase(s => FightResources.Shaders.Seismic.Radius = s,
                        EaseOut(T(7f), -100, 300, ES.Circ));
                    RunEase((s) => { ShaderblackBall(s, BadShader); },
                    LinkEase(Stable(0, 0f), EaseOut(T(1f), 0.23f, ES.Cubic)));
                    for (int i = 0; i < 16; i++)
                    {
                        DelayBeat(i * 1f, () =>
                        {

                            PlaySound(Sounds.pierce);
                            PlaySound(Sounds.pierce);
                            DelayBeat(0.5f, () =>
                            {
                                PlaySound(Sounds.pierce);
                                PlaySound(Sounds.pierce);
                            });
                        });
                    }
                    for (int i = 0; i < 15; i++)
                    {
                        DelayBeat(i * 1f + 1, () =>
                        {
                            BreakBone(7, WCentre + GV(100, Gametime), 2, 30, Gametime * 3.2f);
                            BreakBone(7, WCentre + GV(100, Gametime + 120), 2, 30, Gametime * 3.2f);
                            BreakBone(7, WCentre + GV(100, Gametime + 240), 2, 30, Gametime * 3.2f);
                        });
                    }

                    float time = 100;
                    float y = 0;
                    AddInstance(new TimeRangedEvent(45, () =>
                    {

                        PlaySound(Sounds.spearAppear);
                        for (int i = 0; i < 3; i++)
                        {
                            CreateSpear(new Pike(new V(520, 480 - y), time % 2 == 0 ? 0 : 180, 5, 100 + 5 * i) { IsSpawnMute=true,IsShootMute=true});
                            CreateSpear(new Pike(new V(120, y), time % 2 == 0 ? 0 : 180, 5, 100 + 5 * i) { IsSpawnMute = true, IsShootMute = true });
                        }
                        y += 480 / 45f;
                        time--;
                    }));

                    DelayBeat(8, () =>
                    {
                        float time = 100;
                        float y = 0;
                        AddInstance(new TimeRangedEvent(60, () =>
                        {

                            PlaySound(Sounds.spearAppear);
                            for (int i = 0; i < 3; i++)
                            {
                                CreateSpear(new Pike(new V(640 - y, 440), time % 2 == 0 ? 90 : 270, 5, 100 + 5 * i) { IsSpawnMute = true, IsShootMute = true });
                                CreateSpear(new Pike(new V(y, 40), time % 2 == 0 ? 270 : 90, 5, 100 + 5 * i) { IsSpawnMute = true, IsShootMute = true });
                            }
                            y += 640 / 60f;
                            time--;
                        }));
                    });


                }
                if (InBeat(552 + 46)) 
                {
                    EfEn ef = new(WCentre, inyou) {rot=-45 };
                    ef.AlphaEase(false,0,1,T(2),ES.Sine);
                    ef.AlphaEase(false,1,0,T(2),T(2),ES.Sine);
                    ef.AutoDis(T(4));
                    ef.rotEase(false,-45,-360,T(2),T(2),ES.Sine);
                    CreateEntity(ef);
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0), EaseOut(T(4f), -45, ES.Cubic)));
                }
                if (InBeat(552 + 48))
                {
                    WaveformR = ActivateShader(Waveform, 0.984245f);
                    Flash();
                    shaderProduction3 = ActivateShader(FightResources.Shaders.StepSample,0.84f);
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0), EaseOut(T(4f), 180, ES.Cubic)));
                    RunEase(s => { FightResources.Shaders.StepSample.Intensity = s; ScreenScale = 1 + s*2; },LinkEase(EaseOut(T(1),0,0.3f,ES.Quad),EaseIn(T(1),-0.3f,ES.Quad)));
                    DelayBeat(2, shaderProduction3.Dispose);
                    Bone3d bb = new Bone3d(new V(320, 240), new Vector3(0, 0, 0), false) { Depth = 1, len = 50 };
                    CreateEntity(bb);
                    Bone3d bb2 = new Bone3d(new V(320, 240), new Vector3(0, 0, 0), false) { Depth = 1, len = 20 };
                    CreateEntity(bb2);
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        bb.回転 += new Vector3(1f, -1f, 1f);
                        bb2.回転 -= new Vector3(-1f, 1f, 1f);
                    }));
                    DelayBeat(20, () => { bb.Dispose(); bb2.Dispose(); });
                    RunEase((s) => {
                        bb.length = new Vector3(s);
                        bb2.length = new Vector3(s / 2);
                    }, LinkEase(Stable(0, 0), EaseOut(T(4f), 60, ES.Cubic),
                                 Stable(T(12), 0), EaseOut(T(4f), -60, ES.Cubic)));
                    SetSoul(2);
                    Functions.Heart.JumpTimeLimit = 1000000;
                    Functions.Heart.UmbrellaAvailable = true;
                    const float len = 60;
                    AddInstance(new TimeRangedEvent(T(16), () =>
                    {
                        CreateBone(new CustomBone(new(640, 480),
                        CentreEasing.Linear(GV(10, -90)), 90, len * 1.5f + Rand(-60, 100))
                        { ColorType = 0 });
                        CreateBone(new CustomBone(new(0, 0),
                        CentreEasing.Linear(GV(10, 90)), 90, len * 1.5f + Rand(-60, 100))
                        { ColorType = 0 });
                        CreateBone(new CustomBone(new(640, 480),
                        CentreEasing.Linear(GV(10, 180)), 0, len * 1.5f + Rand(-60, 100))
                        { ColorType = 0 });
                        CreateBone(new CustomBone(new(0, 0),
                        CentreEasing.Linear(GV(10, 0)), 0, len * 1.5f + Rand(-60, 100))
                        { ColorType = 0 });


                    }));

                }
                if (InBeat(600, 616) && At0thBeat(2f))
                {
                    for (int i = 0; i < 16; i++)
                        CreateEntity(new CenB(WCentre, i * 11.25f + -Gametime, -1.3f, 0.5f, 1, 10) { Prot = 2, Srot = 20 });
                    PlaySound(Sounds.pierce);
                    PlaySound(Sounds.pierce);
                    RunEase(s => 
                    {
                        Waveform.Parameters["Iintensity"].SetValue(s);
                        Shader PT = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\ball"));
                        PT.Parameters["intensity"].SetValue((1-s)*2);
                    }, EaseOut(T(2), 0f, 1f, ES.Sine));
                    
                }
                if (InBeat(600, 616) && At0thBeat(1f))
                {
                    for (int i = 0; i < 8; i++)
                        CreateEntity(new CenB(WCentre, i * 45f + Gametime, -1, -0.5f, 1, 10) { Prot = -2, Srot = 20 });
                    PlaySound(Sounds.pierce);
                    DelayBeat(0.5f, () => PlaySound(Sounds.pierce));
                }
                if (InBeat(600, 608) && At0thBeat(1f))
                {
                    DelayBeat(0.5f, () =>
                    {
                        for (int i = 0; i < 7; i++)
                            CreateEntity(new CenB(WCentre, i * (360f / 7f) + Gametime, 1, -0.5f, 500, 20) { Prot = -3, Srot = 20 });
                    });
                }
                if (InBeat(552 + 62))
                {
                    RunEase((s) => ScreenDrawing.ScreenAngle = s, LinkEase(Stable(0, 180), EaseOut(T(8f), 180, ES.Elastic)));
                }
                if (InBeat(552 + 64)) 
                {
                    for (int i = 0; i < 3; i++) 
                        DelayBeat(i*2, () =>
                        {
                            for(int i=0;i<4;i++)
                            PlaySound(Sounds.die1);
                            EfEn ef = new(WCentre, Loader.Load<Texture2D>("Musics\\BadAppleRE\\inyou")) { rot=Gametime*-70, Size = new V(0.5f) };
                            ef.AlphaEase(false,1,0,T(2),ES.Sine);
                            CreateEntity(ef);
                            ef.AutoDis(T(2));
                            RunEase(s =>
                            {
                                ScreenScale = s;
                                ScreenPositionDelta = new V(RandSignal() * s * 3, RandSignal() * s * 3);
                            },
                            EaseOut(T(2), 1.25f, 1, ES.Sine));
                        });
                    DelayBeat(6, () => 
                    {
                        EfEn ef = new(WCentre, inyou) { rot = Gametime*-70,Size=new V(0.5f) };
                        ef.AlphaEase(false, 0, 1f, T(2), ES.Sine);
                        ef.rotEase(false,0,180,T(2),ES.Quad);
                        ef.AlphaEase(false,1,0,T(2),T(4),ES.Sine);
                        ef.rotEase(false,180,-720,T(2),T(4),ES.Sine);
                        CreateEntity(ef);
                        ef.AutoDis(T(6));
                        RunEase(s =>
                        {
                            ScreenScale = s;
                            ScreenPositionDelta = new V(RandSignal() * s * 3, RandSignal() * s * 3);
                        },
                           LinkEase(EaseIn(T(2), 1, 2, ES.Sine),EaseOut(T(4),-1,ES.Sine)));
                        DelayBeat(6, () => 
                        {
                            ForBeat(4, () => ScreenPositionDelta = new V(RandSignal() * 3, RandSignal() * 3));
                        });
                    });
                    for(int i=0;i<4;i++)
                    DelayBeat(8, () => PlaySound(Sounds.switchScene));
                    Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\3circle"));
                    shaderProduction5 = ActivateShader(s,0.26f);
                    s.Parameters["count"].SetValue(0);
                    ForBeat(24, () => 
                    {
                        Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\3circle"));
                        s.Parameters["time"].SetValue(Gametime/15f);
                        s.Parameters["intensity2"].SetValue(new float[] { 16f,-16f,16f,-16,16 });
                    });
                    DelayBeat(8, () => 
                    {
                        RunEase(r =>
                        {
                            Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\3circle"));
                            s.Parameters["intensity1"].SetValue(new float[] { 0.2f * r, 0.15f*r });
                            s.Parameters["count"].SetValue(2);
                        },
                                EaseOut(T(2), 0f, 1, ES.Sine));
                    });
                    DelayBeat(15.8f, shaderProduction5.Dispose);
                }
                if (InBeat(552 + 63))
                {
                    float speed = 0;
                    for (int i = 0; i < 32; i++)
                        DelayBeat(i * 0.25f, () =>
                        {
                            V v = new V(320 + (
                                Cos(speed * 4) + Sin(speed * 5.177f) + Sin(speed * 3.56f)) / 3 * 300,
                                240 + (Cos(speed * 7.26f) + Sin(speed * 6.12f) + Sin(speed * 2.73f)) / 3 * 240);
                            CreateEntity(new FireWorker(v, 50, 20, T(1), CW));
                            BreakBone(T(1), 5, v, 2, 30, Gametime * 3);
                            DelayBeat(1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                PlaySound(Sounds.pierce);
                            });

                            speed += 6;

                        });
                }
                if (InBeat(552 + 72))
                {
                    RunEase(s =>
                    {
                        Shader r = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BackGround1"));
                        r.Parameters["Alpha"].SetValue(s);
                    }, LinkEase(EaseOut(30, 1, 0, ES.Sine)));
                    SetSoul(0);
                    CreateEntity(new CentreCircleBone(0, -1.2f, 600, T(16)) { ColorType = 0 });
                    CreateEntity(new CentreCircleBone(60, -1.2f, 600, T(16)) { ColorType = 0 });
                    CreateEntity(new CentreCircleBone(120, -1.2f, 600, T(16)) { ColorType = 0 });
                    for (int i = 1; i < 10; i++) 
                    {
                        var rot = i % 2 == 0 ? 0 : 180;
                        CreateEntity(new CustomBone
                        (
                            new V(320, 70 + i * 30), CentreEasing.SinWave(250, T(5), 0, rot), InfLinear(0,3), 30) 
                        {ColorType=0 }
                        );
                        CreateEntity(new CustomBone
                        (
                            new V(320, 70 + i * 30), CentreEasing.SinWave(250, T(5), 0, rot), InfLinear(90, 3), 30)
                        { ColorType = 0 }
                        );
                    }
                }
                if (InBeat(552 + 72, 552 + 79) && At0thBeat(1))
                {
                    float s = Gametime * 10;
                    for (int i = 0; i < 7; i++)
                    {
                        CreateSpear(new CircleSpear(Functions.Heart.Centre, 2, 3, 300, i * 120 + s + 72));
                        CreateSpear(new CircleSpear(Functions.Heart.Centre, -2, 3, 300, i * 120 + s + 72));
                        CreateSpear(new CircleSpear(Functions.Heart.Centre, -3, 4, 300, i * 120 + s + 72));
                    }
                    PlaySound(Sounds.pierce);
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(552 + 72, 552 + 80) && At0thBeat(0.25f))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(552 + 72, 552 + 79.73f) && Gametime % 3 == 0)
                {
                    CreateBone(new CustomBone(
                        new V(Rand(0, 150), 700), InfLinear(new V(0, -20)),
                        LinkEase(Stable(0, Rand(0, 180)), EaseOut(T(4), Rand(100, 400) * 5 * RandSignal(), Li)),
                        120
                        )
                    { ColorType = 0, Depth = 1 });
                    CreateBone(new CustomBone(
                        new V(Rand(490, 640), 700), InfLinear(new V(0, -20)),
                        LinkEase(Stable(0, Rand(0, 180)), EaseOut(T(4), Rand(100, 400) * 5 * RandSignal(), Li)),
                        120
                        )
                    { ColorType = 0, Depth = 1 });
                    CreateBone(new CustomBone(
                        new V(Rand(340, 540), 700), InfLinear(GV(-20, 45)),
                        LinkEase(Stable(0, Rand(0, 180)), EaseOut(T(4), Rand(100, 400) * 5 * RandSignal(), Li)),
                        120
                        )
                    { ColorType = 0, Depth = 1 });
                    CreateBone(new CustomBone(
                       new V(Rand(100, 300), 700), InfLinear(GV(20, -45)),
                       LinkEase(Stable(0, Rand(0, 180)), EaseOut(T(4), Rand(100, 400) * 5 * RandSignal(), Li)),
                       120
                       )
                    { ColorType = 0, Depth = 1 });
                    CreateBone(new CustomBone(
                       new V(Rand(-540, -340), 700), InfLinear(GV(20, -45)),
                       LinkEase(Stable(0, Rand(0, 180)), EaseOut(T(4), Rand(100, 400) * 5 * RandSignal(), Li)),
                       120
                       )
                    { ColorType = 0, Depth = 1 });
                    CreateBone(new CustomBone(
                       new V(Rand(980, 1180), 700), InfLinear(GV(-20, 45)),
                       LinkEase(Stable(0, Rand(0, 180)), EaseOut(T(4), Rand(100, 400) * 5 * RandSignal(), Li)),
                       120
                       )
                    { ColorType = 0, Depth = 1 });

                }
                if (InBeat(552 + 64))
                {

                    float rot = 0;

                    for (int i = 0; i < 16; i++)
                    {
                        DelayBeat(i, () =>
                        {
                            BreakFire(7, new V(-180, 0), 8, rot, 3, 120, 0, 4);
                            BreakFire(7, new V(-180, 0), 8, rot * -1, -3, 60, 0, 2);
                            BreakFire(7, new V(820, 480), -8, rot * -1, 3, 120, 0, 4);
                            BreakFire(7, new V(820, 480), -8, rot, -3, 60, 0, 2);

                            BreakFire(7, new V(0, -200), 8, rot, 3, 120, 90, 4);
                            BreakFire(7, new V(0, -200), 8, rot * -1, -3, 60, 90, 2);
                            BreakFire(7, new V(640, 680), -8, rot * -1, 3, 120, 90, 4);
                            BreakFire(7, new V(640, 680), -8, rot, -3, 60, 90, 2);
                            rot += 40;
                        });
                    }
                }
                if (InBeat(552 + 78))
                {

                    float rand = Gametime;
                    for (int i = 0; i < 20; i++)
                    {
                        for (int i2 = 0; i2 < 6; i2++)
                            CreateEntity(new SwarmSpear(Functions.Heart.Centre, (i + 1) * 5, 100 + (i + 1) * 25, rand + i2 * 60, T(1)));
                        for (int i3 = 0; i3 < 5; i3++)
                            for (int i2 = 0; i2 < 12; i2++)
                                CreateEntity(new SwarmSpear(Functions.Heart.Centre, (i + 1) * 9, 300 + (i + 1) * 25, rand + i2 * 30 + 25 + i3 * 2.5f, T(1.5f)));
                    }
                }
                if (InBeat(552 + 79.75f))
                {

                    DisposeBarrage();
                    CustomBall[] s = GetAll<CustomBall>();
                    foreach (CustomBall ball in s) ball.Dispose();
                    RunEase(s =>
                    {
                        Shader r = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BackGround1"));
                        r.Parameters["Alpha"].SetValue(s);
                    }, LinkEase(EaseOut(10, 1, 0, ES.Sine)));
                    PlaySound(Sounds.destroy);
                    PlaySound(Sounds.destroy);
                }
                if (InBeat(552 + 80))
                {
                    HeartAttribute.DamageTaken = 3;
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                        LinkEase(Stable(0, 0), EaseIn(T(3f), 0, ES.Cubic),
                                 Stable(0, 0), EaseOut(T(3f), 0, ES.Cubic)));
                    SetSoul(SoulOriginal.WhiteSoul);
                    float x = Gametime * 15;
                    AddInstance(new TimeRangedEvent(T(6), () =>
                    {
                        CreateEntity(new NormalGB(WCentre + GV(500, x), WCentre, new V(2), x + 180, T(1f), T(0.25f)));
                        CreateEntity(new NormalGB(WCentre + GV(500, x + 90), WCentre, new V(2), x + 270, T(1f), T(0.25f)));
                        CreateEntity(new NormalGB(WCentre + GV(500, x + 180), WCentre, new V(2), x, T(1f), T(0.25f)));
                        CreateEntity(new NormalGB(WCentre + GV(500, x + 270), WCentre, new V(2), x + 90, T(1f), T(0.25f)));
                        x += 270 / T(6);
                    }));
                    V vec = new(320, 240);
                    for (int i = 0; i < 60; i++)
                    {
                        RBones b = new RBones();
                        CreateBone(b);
                        float rot = i * 6;
                        float bas = 500;
                        AddInstance(new TimeRangedEvent(T(16), () =>
                        {
                            Rb(b, vec + GV(bas, rot), vec + GV(500, rot), 0, 0);
                            rot += 3;
                            bas -= 320 / T(6);

                        }));

                        DelayBeat(6, () =>
                        {
                            RunEase((s) => { b.Alpha = s; }, LinkEase(Stable(0, 1), EaseOut(T(1f), -1, ES.Cubic)));
                        });
                        DelayBeat(7, () => b.Dispose());
                    }
                    float bas2 = 450;
                    AddInstance(new TimeRangedEvent(T(6), () =>
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            float rand = Rand(0, 360);
                            CreateBone(new CustomBone(EaseOut(T(1), WCentre + GV(500, rand), WCentre + GV(bas2 + Rand(0, 20), rand), Qd),
                                                          EaseOut(T(1), Rand(0f, 360f), T(2) * 1000 * (RandBool() ? 1 : -1), Qd), 15)
                            { ColorType = 0 });
                        }
                        bas2 -= 320 / T(6);
                    }));

                }
                if (InBeat(552 + 87))
                {
                    RunEase((s) => { ScreenScale = s; }, LinkEase(Stable(0, 1), EaseOut(T(8f), 3, ES.Cubic),
                                                                                EaseOut(T(8f), -3, ES.Linear)));
                    CreateChart(T(1), T(2), 8, new string[]
                    {
                    "^$0'2(^$21'2)",
                    });
                    RunEase((s) => { InstantTP(s); }, EaseOut(T(1f), Functions.Heart.Centre, new V(320, 240), ES.Back));
                    SetSoul(1);
                    shaderProduction4 = ActivateShader(FightResources.Shaders.StepSample, 0.2798435f);
                    DelayBeat(0, () =>
                    {
                        AddInstance(new TimeRangedEvent(T(8), () =>
                        {
                            FightResources.Shaders.StepSample.CentreX = Functions.Heart.Centre.X;
                            FightResources.Shaders.StepSample.CentreY = Functions.Heart.Centre.Y;
                            Arrow arrow2 = Functions.MakeArrow(T(1), 0, 25, 0, 0);
                            arrow2.Scale = 1;
                            RunEase((s) =>
                            { arrow2.CentreRotationOffset = s; }, EaseOut(T(3f), Rand(-170, 170), 0, ES.Cubic));
                            Functions.CreateEntity(arrow2);
                            Arrow arrow = Functions.MakeArrow(T(1), 2, 25, 1, 0);
                            arrow.Scale = 1;
                            RunEase((s) =>
                            { arrow.CentreRotationOffset = s; }, EaseOut(T(3f), Rand(-170, 170), 0, ES.Cubic));
                            Functions.CreateEntity(arrow);
                            Arrow arrow3 = Functions.MakeArrow(T(1), 0, 25, 0, 0);
                            arrow3.Scale = 1;
                            RunEase((s) =>
                            { arrow3.CentreRotationOffset = s; }, EaseOut(T(3f), Rand(-170, 170), 0, ES.Cubic));
                            Functions.CreateEntity(arrow3);
                            Arrow arrow4 = Functions.MakeArrow(T(1), 2, 25, 1, 0);
                            arrow4.Scale = 1;
                            RunEase((s) =>
                            { arrow4.CentreRotationOffset = s; }, EaseOut(T(3f), Rand(-170, 170), 0, ES.Cubic));
                            Functions.CreateEntity(arrow4);
                            FightResources.Shaders.StepSample.Intensity = (Gametime - T(552 + 87)) / T(4);
                        }));
                    });
                }
                if (InBeat(552 + 87, 552 + 95))
                {
                    CreateSpear(new Pike(WCentre + GV(50, Gametime * 10) + GV(300, Gametime * 10 + 90), Gametime * 10 - 90, 10, T(1)) { DrawingColor = CW ,IsSpawnMute=true,IsShootMute=true});
                    CreateSpear(new Pike(WCentre + GV(50, Gametime * 10 + 180) + GV(300, Gametime * 10 + 90 + 180), Gametime * 10 - 90 + 180, 10, T(1)) { DrawingColor = CW, IsSpawnMute = true, IsShootMute = true });
                }
                if (InBeat(552 + 96))
                {
                    foreach (Spear e in GetAll<Spear>())
                    {
                        e.Dispose();
                    }
                    ScreenPositionDelta = V.Zero;
                }
                if (InBeat(552 + 92))
                {
                    Shader r = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BackGround1"));
                    r.Parameters["position"].SetValue(new V(0));
                    r.Parameters["R"].SetValue(1);
                    r.Parameters["G"].SetValue(1);
                    r.Parameters["B"].SetValue(1);
                    RunEase(s => 
                    {
                        Shader r = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BackGround1"));
                        r.Parameters["Alpha"].SetValue(s);
                    }, LinkEase(EaseOut(T(4),0,1,ES.Sine),EaseOut(T(8),1,0,ES.Sine)));
                }
                #endregion
                #endregion*/*/
                #region LastPart
                if (InBeat(552 + 96))
                {
                    FightBox.instance.AlphaBlend = false;
                    BSet.final=true;
                    BSet.col = false;
                    ResetBarrage();
                    var scene = CurrentScene as SongFightingScene;
                    complete = (scene.Mode != GameMode.None||HeartAttribute.HP ==0);
                    complete = !complete;
                    if (BadShader == null)
                    {
                        ThemeColor = C.Transparent;
                        NameShower.nameAlpha = 0;
                        HPBar.HPExistColor = C.Transparent;
                        HPBar.HPLoseColor = C.Transparent;
                        UIColor = C.Transparent;

                    }
                    if (complete)
                    {
                        BSet.SideColor = C.White;
                        ShaderblackBall(0, BadShader);
                        check = true;
                    }
                    shaderProduction4?.Dispose();
                    if (!complete)
                    {
                        RunEase((s) => { BSet.SideColor = C.Lerp(CW, C.Black, s); },
                            LinkEase(Stable(0, 0), EaseOut(T(8), 1, Cu)));
                    }
                    FightResources.Shaders.Aurora.ThemeColorA = Color.White * 0.8f;
                    FightResources.Shaders.Wave.Intensity1 = 3f;
                    FightResources.Shaders.Wave.Intensity2 = 1f;
                    FightResources.Shaders.Wave.Intensity3 = 2f;
                    FightResources.Shaders.Wave.Frequency1 = 20f;
                    FightResources.Shaders.Wave.Frequency2 = 20f;
                    FightResources.Shaders.Wave.Frequency3 = 20f;
                    FightResources.Shaders.Wave.Speed = 0.001f;
                    FightResources.Shaders.Wave.Time = 400;
                    FightResources.Shaders.Aurora.ThemeColorB = Color.Black;
                    FightResources.Shaders.Aurora.Slope = 1000;
                    FightResources.Shaders.Aurora.Type = 1;
                    BlurG = new ScreenDrawing.Shaders.Glitching(0.46362f);
                    BlurB = new ScreenDrawing.Shaders.Blur(0.893234f);
                    BlurG.Duration = new V(3,3);
                    BlurG.Intensity = 100;
                    BlurG.BlockScale = 1f;
                    BlurG.AverageDelta = 2;
                    BlurG.AverageInterval = 0;
                    SceneRendering.InsertProduction(BlurG);
                    SceneRendering.InsertProduction(BlurB);
                    production1 = ActivateShader(FightResources.Shaders.Wave, 0.9387498f);
                    production2 = ActivateShader(FightResources.Shaders.Blur, 0.9587498f);
                    Auroras aurora = new(0.42f);
                    BackGroundRendering.InsertProduction(aurora);
                    ThemeColor = Color.White;
                    SetSoul(Souls.RedSoul, false);
                    Functions.Heart.Speed = 0.2f;
                    InstantTP(Functions.Heart.Centre.X, 240);
                    InstantSetBox(240, 280, 100);
                    InstantSetBox(240, 300, 100);
                    BoxUtils.Vertexify();
                    FightBox.instance.AlphaBlend = false;
                    float i = 0;
                    BoundColor = Color.Orange;
                    BoxSize = new V(300, 150);
                    AddInstance(new TimeRangedEvent(T(62), () =>
                    {
                        CustomBox();
                        i += 0.5f;
                        float sin = Sin(i) * 25;
                        float cos = Sin(i * 1.3f) * 20;
                        ScreenPositionDelta = new V(cos, sin);
                        ScreenDrawing.ScreenAngle = Sin(i * -1.5f) * 20;

                        BoxStates.CurrentBox.InstantSetAlpha(0.3f + Sin(i * 5) * 0.3f);
                        Functions.Heart.InstantSetRotation(Sin(i * -0.5f) * 8);
                        brot = Sin(i) * 15;
                        LeftBoundDistance = 400 + Sin(i * 2) * 100;
                        RightBoundDistance = 400 + Sin(i * 2) * 100;
                        DownBoundDistance = 400 + Sin(i * 2) * 100;
                        FightResources.Shaders.Blur.Sigma = 0.5f + Sin(i) * 0.2f;
                        FightResources.Shaders.Blur.Factor = new V(1+Sin(i * 2.3479f)*0.5f, 1+Cos(i * 4.234798f)*0.5f);
                        BlurB.Sigma = Sin(i * 0.7f) * 0.5f+1.5f;
                    }));

                    ScreenDrawing.BoxBackColor = Color.Black * 0.3f;

                    BoundColor = CW;

                }
                if (InBeat(552 + 96))
                {
                    SidePro?.Dispose();
                    foreach (Bone l in GetAll<Bone>())
                    {
                        l.Dispose();
                    }
                    foreach (CustomBall l in GetAll<CustomBall>())
                    {
                        l.Dispose();
                    }
                    Arrow.UnitEasing ease = new();
                    AddInstance(ease);
                    ease.ApplyTime = T(16);
                    ease.AlphaEase = LinkEase(Stable(T(12), 0), EaseOut(T(8), 0.5f, ES.Quad));
                    ease.TagApply("s");
                    RegisterFunctionOnce("SR", () =>
                    {
                        
                        SetSoul(1, false);
                        BSet.LastShield = true;
                        RunEase((s) => { BoxSize = s; },
                        LinkEase(Stable(0, BoxSize), EaseOut(T(8f), new V(84, 84) - BoxSize, ES.Quart)));
                        RunEase((s) => { InstantTP(s); }, LinkEase(Stable(0, Functions.Heart.Centre),
                            EaseOut(T(8f), WCentre - Functions.Heart.Centre, ES.Cubic)));
                    });
                    RegisterFunctionOnce("Break", () =>
                    {
                        ScreenDrawing.MakeFlicker(CW);
                        BoxBackColor = Color.Transparent;
                        BoxStates.CurrentBox.InstantSetAlpha(0);
                        Functions.Heart.Depth = 1;
                        RunEase((s) =>
                        {
                            InstantTP(s); boxvec = s;
                            CustomBox();
                        },
                            LinkEase(Stable(0, Functions.Heart.Centre), EaseIn(T(4f), GV(400, 90 + Functions.Heart.Rotation), ES.Cubic)));
                        for (int i = 0; i < 14; i++)
                        {
                            V vec = new(320 - 42, 240 + 42);

                            BoxFrame box;
                            CreateEntity(box = new BoxFrame(vec + new V(i * 6, 0), 0));
                            RunEase((s) => { box.Centre = s; },
                                LinkEase(Stable(0, box.Centre), EaseIn(T(Rand(8, 12)), GV(500, Rand(100, 160)), ES.Cubic)));
                            RunEase((s) => { box.rot = s; },
                                LinkEase(Stable(0, box.rot), EaseIn(T(Rand(8, 12f)), T(4) * Rand(4, 7) * (RandBool() ? 1 : -1), ES.Cubic)));
                        }
                        for (int i = 0; i < 14; i++)
                        {
                            V vec = new(320 + 42, 240 + 42);

                            BoxFrame box;
                            CreateEntity(box = new BoxFrame(vec + new V(0, i * -6), 90));
                            RunEase((s) => { box.Centre = s; },
                                LinkEase(Stable(0, box.Centre), EaseIn(T(Rand(8, 12)), GV(500, Rand(100, 160)), ES.Cubic)));
                            RunEase((s) => { box.rot = s; },
                                LinkEase(Stable(0, box.rot), EaseIn(T(Rand(8, 12f)), T(4) * Rand(4, 7) * (RandBool() ? 1 : -1), ES.Cubic)));
                        }
                        for (int i = 0; i < 14; i++)
                        {
                            V vec = new(320 + 42, 240 - 42);

                            BoxFrame box;
                            CreateEntity(box = new BoxFrame(vec + new V(i * -6, 0), 180));
                            RunEase((s) => { box.Centre = s; },
                                LinkEase(Stable(0, box.Centre), EaseIn(T(Rand(8, 12)), GV(500, Rand(100, 160)), ES.Cubic)));
                            RunEase((s) => { box.rot = s; },
                                LinkEase(Stable(0, box.rot), EaseIn(T(Rand(8, 12f)), T(4) * Rand(4, 7) * (RandBool() ? 1 : -1), ES.Cubic)));
                        }
                        for (int i = 0; i < 14; i++)
                        {
                            V vec = new(320 - 42, 240 - 42);

                            BoxFrame box;
                            CreateEntity(box = new BoxFrame(vec + new V(0, i * 6), 90));
                            RunEase((s) => { box.Centre = s; },
                                LinkEase(Stable(0, box.Centre), EaseIn(T(Rand(8, 12)), GV(500, Rand(100, 160)), ES.Cubic)));
                            RunEase((s) => { box.rot = s; },
                                LinkEase(Stable(0, box.rot), EaseIn(T(Rand(8, 12f)), T(4) * Rand(4, 7) * (RandBool() ? 1 : -1), ES.Cubic)));
                        }
                    });
                    RegisterFunctionOnce("ScreenOut", () =>
                    {
                        if (complete)
                        {
                            RunEase((s) =>
                            {
                                FightResources.Shaders.Aurora.ThemeColorA = C.Lerp(C.White, C.Black, s);
                                BoundColor = C.Lerp(C.White, C.Black, s);
                            },
                                LinkEase(Stable(0, 0), EaseOut(T(16f), 1, ES.Linear)));
                            RunEase((s) => { BSet.SideColor = CW * s; },
                                LinkEase(Stable(0, 1), EaseOut(T(16), -1, Li)));
                            DelayBeat(16, () =>
                            {
                                BlurG.Dispose();
                                FightResources.Shaders.Wave.Intensity1 = 0;
                                FightResources.Shaders.Wave.Intensity2 = 0;
                                FightResources.Shaders.Wave.Intensity3 = 0;
                                FightResources.Shaders.Blur.Sigma = 0;
                                ScreenPositionDelta = V.Zero;
                                ScreenDrawing.ScreenAngle = 0;
                                BlurB.Sigma = 2;
                            });
                            if (true)
                            {
                                RunEase((s) => { BackGroundColor = C.Lerp(C.Black, CW, s); },
                                    LinkEase(Stable(0, 0), EaseOut(T(16f), 1, Li)));
                            }
                        }
                        else
                        {
                            EfEn ef = new(WCentre, Sprites.square) { Size = new V(100), Depth = 0.74f };
                            ef.AlphaEase(false, 0, 1, T(16), Li);
                            CreateEntity(ef);
                        }
                    });
                    CreateChart(T(28), 2, 0, new string[]
                    {
                        "SR",
                    });
                    CreateChart(T(32f), T(2), 2, new string[]
                    {

                        "(R@s)(+01@s)","","(R@s)(+01@s)","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","", "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(R@s)(+01@s)","","(R@s)(+01@s)","",  "(R@s)(+01@s)","","(R@s)(+01@s)","",
                        "(#$4#$3)(#4#+01)(Break)(ScreenOut)",

                    });
                }
                if (InBeat(552 + 96 + 64 + 19))
                {
                    if (production1 != null)
                    {
                        shaderProduction3.Dispose();
                        shaderProduction1.Dispose();
                        production1.Dispose();
                        production2.Dispose();
                        BlurG.Dispose();
                        BlurB.Sigma = 1f;
                        ScreenAngle = 0;
                        shaderProduction5.Dispose();
                    }
                    else check = true;
                    completed = false;
                    if (!check)
                    {
                        BlurB.Dispose();
                        DelayBeat(3, () =>
                        {

                            float intensity = 0;
                            ImageEntity s;
                            CreateEntity(s = new ImageEntity(Loader.Load<Texture2D>("Musics\\BadAppleRE\\dead\\f1"))
                            { Alpha = 1, Size = new V(0.5f), Centre = WCentre, Depth = 0.99f,controlLayer=UIS2().UISurface });
                            PlaySound(Sounds.damaged);
                            PlaySound(Sounds.damaged);
                            PlaySound(Sounds.damaged);
                            
                            intensity = 20;
                            DelayBeat(5, () =>
                            {

                                s.Image = Loader.Load<Texture2D>("Musics\\BadAppleRE\\dead\\f2");
                                PlaySound(Sounds.damaged);
                                PlaySound(Sounds.damaged);
                                PlaySound(Sounds.damaged);
                                intensity = 20;
                            });
                            DelayBeat(10, () =>
                            {
                                s.Image = Loader.Load<Texture2D>("Musics\\BadAppleRE\\dead\\f3");
                                PlaySound(Sounds.damaged);
                                PlaySound(Sounds.damaged);
                                PlaySound(Sounds.damaged);
                                intensity = 40;

                            });
                            DelayBeat(15, () =>
                            {
                                s.Image = Loader.Load<Texture2D>("Musics\\BadAppleRE\\dead\\f4");
                                PlaySound(Sounds.damaged);
                                PlaySound(Sounds.damaged);
                                PlaySound(Sounds.damaged);
                                intensity = 50;
                            });
                            DelayBeat(20, () =>
                            {
                                throw new ArgumentException("DEAD.");
                            });
                            ForBeat(20, () => s.Centre = WCentre+GV(intensity *= 0.9f, Rand(0, 360f)));
                        });
                    }
                    else if (PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("CompleteBadAppleRE")||PlayerManager.CurrentUser==null)
                    {
                        EndSong();
                    }
                    else
                    {
                        
                        completed = true;
                        texter = true;
                        textnum = 0;
                        ContainKey = true;
                    }

                }
                if (completed && GametimeF > T(552 + 96 + 64 + 20) )
                {
                    if (IsKeyDown(InputIdentity.Confirm) && texter && delay)
                    {
                        texter = false;
                        textnum++;
                        delay = false;
                        Ef[] ef = GetAll<Ef>();
                        foreach (Ef ef1 in ef)
                        {
                            ef1.alphaOut(1, 0, T(2), Cu);
                            ef1.AutoDis(T(2));
                        }
                        DelayBeat(4, () => ContainKey = true);
                    }
                    if (!IsKeyDown(InputIdentity.Confirm))
                    {
                        texter = true;
                    }
                    if (InBeat(552 + 96 + 64 + 20))
                    {
                        Ender("Congratulations.\n" +
                            "You have beaten this game.\n" +
                            "It probably took a lot of time\nto beat it.\n" +
                            "Your determination has shattered it.\n" +
                            "I salute you for being here now.\nBut I also wonder.\nBecause I didn't think anyone could\nbeat this game.\n" +
                            "You really have great playing skills.\n" +
                            "Once again I salute you.");
                    }
                    if (NextText(1))
                    {
                        delay = false;
                        ContainKey = false;
                        string tex =
                            "Credits\n" +
                            "Undyne - T-mas  \nThe Slaughter Continues - T-mas\n" +
                              "Eternal spring Dream - T-mas  \nBadApple T-mas\n" +
                              "Astronomia - T-mas  \nHelltaker - T-mas\n" +
                              "Goyang Uber Uber - T-mas  \nNever gonna give you up - T-mas\n" +
                              "Underfell - T-mas  \nSuddenChanges - T-mas\n" +
                              "SpaceDrift - T-mas  \nSustenance of moon - T-mas\n" +
                              "Maware!Setsugekka - T-mas  \nNight of Knights - T-mas\n" +
                              "DreamBattle - T-mas  \nFreedomDive - T-mas\n" +
                              "Brain power - Tlottgodinf  \nRrhar'il - Tlottgodinf\n" +
                              "Universal Collapse - Tlottgodinf  \nLostmemory - zkronO\n" +
                              "Grievous Lady - Tlottgodinf  \nEther Strike - IceAgeDOT\n" +
                              "GOODDRILL - zkronO  \nGOODTEK - zkronO\n" +
                              "GOODRAGE - T-mas  \nGOODWORLD - Tlottgodinf\n" +
                              "Let's go now! - Tlottgodinf  \nManiac's Revenge - TK\n" +
                              "The Last Resistance - Tlottgodinf  \nResurrection. - Tlottgodinf\n" +
                              "Igallta - Tlottgodinf\n" +
                              "Undyne(RE) - T-mas & Walar & IceAgeDOT\n" +
                              "SAVE(Hopes and Dreams) - Tlottgodinf\n" +
                              "Minecraft Revenge - Tlottgodinf\n" +
                              "Dreadnaught - Tlottgodinf VS zkronO\n" +
                              "Conflict - T-mas & Tlottgodinf \n" +
                              "           & zkronO\n" +
                              "End time - ParaDOXXX\n" +
                              "Transcendence - T-mas & Tlottgodinf\n" +
                              "                & zkronO & Walar\n" +
                              "R.I.P. - Tlottgodinf & zkron0" +
                              "Death By Glamour - Tlottgodinf\n" +
                              "Asgore - Tlottgodinf\n" +
                              "The World Revolving - Tlottgodinf\n" +
                              "BIG SHOT - Tlottgodinf\n" +
                              "Spider Dance - zkronO,mentototo\n" +
                              "Travelar at Sunset\n" +
                              "T-mas & zkronO \n " +
                              "& TK & Tlottgodinf\n\n" +
                              "BadApple!! - mentosu\n" +
                              "Anomaly作成ソフト - Aviutl & Audacity"
                            ;
                        Ef text = new(tex, new V(320, 20), C.White * 2, 0.7f) { AutoDispose = false, scf = true };
                        Ef text2 = new(tex, new V(320, 20), C.White * 2, 0.7f) { AutoDispose = false, scf2 = true };
                        CreateEntity(text);
                        CreateEntity(text2);
                        text.vecOut(new V(320, 860 + 0.16f * T(128)), new V(320, -390 - 0.16f * T(128)), T(190), ES.Linear);
                        text2.vecOut(new V(320, 860 + 0.16f * T(128)), new V(320, -390 - 0.16f * T(128)), T(190), ES.Linear);
                        DelayBeat(190, () =>
                        {
                            tex = "Rhythm Recall by T-mas";
                            Ef text = new(tex, WCentre, C.White * 1, 1.2f) { AutoDispose = false, scf = true };
                            Ef text2 = new(tex, WCentre, C.White * 1, 1.2f) { AutoDispose = false, scf2 = true };
                            CreateEntity(text);
                            CreateEntity(text2);
                            text.alphaOut(0, 1, T(16), ES.Cubic);
                            text2.alphaOut(0, 1, T(16), ES.Cubic);
                            text.alphaOut(1, 0, T(16), T(16), ES.Cubic);
                            text2.alphaOut(1, 0, T(16), T(16), ES.Cubic);
                            DelayBeat(32, () =>
                            {
                                tex = "Thanks for playing!";
                                string tex2 = "Stay tuned for new content in the future!";
                                Ef text = new(tex, WCentre, C.Yellow * 1, 0.6f) { AutoDispose = false, scf = true };
                                Ef text2 = new(tex, WCentre, C.Yellow * 1, 0.6f) { AutoDispose = false, scf2 = true };
                                Ef text3 = new(tex2, WCentre+new V(0,50), C.White * 1, 0.6f) { AutoDispose = false, scf = true };
                                Ef text4 = new(tex2, WCentre+new V(0,50), C.White * 1, 0.6f) { AutoDispose = false, scf2 = true };
                                foreach (Ef ef in new[] { text, text2, text3, text4 }) 
                                {
                                    CreateEntity(ef);
                                    ef.alphaOut(0, 1, T(16), ES.Cubic);
                                    ef.alphaOut(1, 0, T(16), T(16), ES.Cubic);
                                }
                            });
                            DelayBeat(64, () =>
                            {
                                EndSong();
                                if (PlayerManager.CurrentUser != null)
                                {
                                    PlayerManager.CurrentUser.Custom.PushNext(new("CompleteBadAppleRE:value=0"));
                                }
                            });
                        });
                        
                    }

                }


                #endregion*/

            }
            private bool check = false;
            public void Noob()
            {
                TextEffect();
                ExtendEffect();
            }
            public override void Start()
            {
                AddInstance(new InstantEvent(1, () =>
                {
                    var scene = CurrentScene as SongFightingScene;
                    if (scene.Mode == GameMode.Buffed) GameStates.ResetScene(new GameMenuScene());
                }));
                if ((int)CurrentDifficulty >= 4)
                {

                    Waveform = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\Waveform"));
                    ResetRendering();
                    BSet.final = false;
                    BSet.timestop = false;
                    BSet.again = true;
                    NameShower.level = 5.ToString();
                    HandImage = Loader.Load<Texture2D>("Musics\\Asgore\\Hand");
                    Functions.Heart.SoftFalling = true;
                    game = this;
                    float gametime = T(0
                        //+7
                        //+38
                        //+70
                        //+130
                        //+164
                        //+180
                        //+196
                        //+210
                        //+230
                        //+260
                        //+324
                        //+345
                        //+358
                        //+390
                        //+422
                        //+435
                        //+450
                        //+486
                        //+502
                        //+518
                        //+546
                        //+598
                        //+610
                        //+552+90
                        //+552+90+16+64
                        );
                    bool Debug = false;
                    bool finalsetting = false;
                    PlayOffset = gametime;
                    AutoEnd = false;
                    pause = false;
                    //
                    GametimeDelta = gametime + (gametime == 0 ? T(0.8f) : T(0.7f));
                    base.Start();
                    InstantSetGreenBox();
                    SetSoul(Souls.RedSoul);
                    SetSoul(1);
                    InstantTP(320, 240);
                    HeartAttribute.MaxHP = 5;
                    if (finalsetting)
                    {
                        InstantSetBox(240, 645, 485);
                        SetSoul(SoulOriginal.WhiteSoul);
                        BoxBackColor = C.Transparent;
                    }
                    if (!Debug)
                    {
                        RunEase((s) => { MasterAlpha = s; },
                                EaseOut(T(16f), 0, 0.5f, ES.Quad));
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                                EaseOut(T(7.5f), 360 * 8, 0f, ES.Sine));
                        DelayBeat(8f, () => { ScreenDrawing.ScreenAngle = 0; });
                        RunEase((s) => { ScreenScale = s; },
                                EaseOut(T(7.5f), 0, 1f, Ba));
                    }
                    else
                    {
                        CreateEntity(new Entitycount());
                    }
                    int count = 0;
                    AddInstance(new TimeRangedEvent(105, () =>
                    {
                        string st = "0";
                        char[] s = $"{count}".ToCharArray();
                        for (int i2 = 0; i2 < 4 - s.Length; i2++) st += "0";
                        st += $"{count}";
                        BadAppleRE.BreakScreen[count] = Loader.Load<Texture2D>($"Musics\\BadAppleRE\\BreakScreen\\BlackMove_{st}");
                        count++;
                    }));
                    #region shaders 
                    Effect3D = FightResources.Shaders.Camera;
                    Effect3D.TextureSize = new V(640, 480);
                    Effect3D.CameraRotation = new Vector3(0f, 0f, 0f);
                    Effect3D.ProjectAxisX = new Vector3(1f, 0, 0);
                    Effect3D.ProjectAxisY = new Vector3(0f, 1, 0);
                    Effect3D.ProjectAxisZ = new Vector3(0f, 0, 1f);
                    Effect3D.ProjectPoint = new(0, 0, 300);
                    Effect3D.ProjectPointOffect = new(320, 240);
                    Effect3D.Visuospatial = new(640, 480, 200);
                    Effect3D.CameraPosition = new(0);
                    Effect3D.CameraRotation = new(0);
                    Effect3D.CameraAhead = new Vector3(0, 0, 1);
                    Effect3D.CameraRight = new(1, 0, 0);
                    Effect3D.CameraDown = new(0, 1, 0);
                    FightResources.Shaders.Wave.Intensity1 = 0f;
                    FightResources.Shaders.Wave.Intensity2 = 0f;
                    FightResources.Shaders.Wave.Intensity3 = 0f;
                    FightResources.Shaders.Wave.Frequency1 = 0f;
                    FightResources.Shaders.Wave.Frequency2 = 0f;
                    FightResources.Shaders.Wave.Frequency3 = 0f;
                    FightResources.Shaders.Mosaic.MosiacSize = new V();
                    FightResources.Shaders.Blur.Factor = new V();
                    FightResources.Shaders.DislocationX.ChunkHeight = 0;
                    FightResources.Shaders.DislocationX.RGBSplitEnabled = false;
                    FightResources.Shaders.Fire.Height = 0;
                    FightResources.Shaders.StepSample.Intensity = 0;
                    FightResources.Shaders.Gray.Intensity = 0;
                    FightResources.Shaders.LightSweep.Width = 0;
                    Functions.CreateEntity(rainer = new Rainer());
                    shaderGrid = new();
                    rainer.Intensity = 0f;
                    rainer.Speed = 2000f;
                    rainer.Scale = -10f;
                    Shader t = new(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\BlurX"));
                    btest = new();
                    Shader t2 = new(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\BlurY"));
                    btest2 = new();
                    Shader t3 = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\Noise"));
                    nega = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\SquareNega"));
                    #endregion
                }
                else AnomalyStart();
                btest = new();
                btest2 = new();
            }
            public static void anomaly()
            {
                audio?.Stop();
                BadApple_RE.IntoUnlockScene();
            }
        }

        private readonly Dictionary<string, Difficulty> difficulties = new Dictionary<string, Difficulty>();

        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public BadApple_RE()
        {
            difficulties = new Dictionary<string, Difficulty>();
            difficulties.Add("BadApple", Difficulty.ExtremePlus);
#if DEBUG
            difficulties.Add("anomaly", Difficulty.Noob);
            difficulties.Add("TEST1", Difficulty.Easy);
            difficulties.Add("TEST2", Difficulty.Extreme);
            difficulties.Add("TEST3", Difficulty.Normal);
            //difficulties.Add("TEST6", Difficulty.Hard);

#endif
        }
    }
}
