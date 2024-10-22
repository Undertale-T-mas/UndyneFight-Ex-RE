﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{

    internal partial class Traveler_at_Sunset : IChampionShip
    {

        public partial class Project
        {

            public void Normal()
            {

                //Manual end song
                if (GametimeF >= 310 * 60) EndSong();
                NRPre();
                NRBuildup();
                if (InBeat(206, 232) && At0thBeat(1f))
                {
                    var dir = Posmod(GametimeF, 90);
                    for (int i = -1; i < 2; ++i)
                    {
                        CreateEntity(new NormalSpear(new(0, 0), dir + i * 40, 2.1f)
                        {
                            DelayTargeting = false,
                            IsMute = true,
                            Acceleration = 0.015f,
                        });
                        CreateEntity(new NormalSpear(new(640, 0), dir + i * 40 + 90, 2.1f)
                        {
                            DelayTargeting = false,
                            IsMute = true,
                            Acceleration = 0.015f,
                        });
                    }
                }
                if (InBeat(72 + 128))
                {
                    RegisterFunctionOnce("pre", () =>
                    {
                        production1.Dispose();
                        BoxUtils.Vertexify();
                        var box = BoxUtils.VertexBoxInstance;
                        for (int i = 0; i < 360; ++i)
                        {
                            box.SetPosition((i < 3) ? i : box.Split(i, 0.5f), GetVector2(100, i) + new Vector2(320, 240));
                        }
                        box.SetPosition(3, GetVector2(100, 3) + new Vector2(320, 240));

                        //BoxUtils.Move(new(0, -40));
                        ScreenDrawing.BoxBackColor = Color.Transparent;

                        SetSoul(Souls.RedSoul);
                        ForBeat(28, () =>
                        {

                        });

                    });
                    RegisterFunctionOnce("Green", () =>
                    {
                        var box = BoxUtils.VertexBoxInstance;
                        for (int i = 0; i < 4; ++i)
                        {
                            box.SetPosition(i, GetVector2(80, i * 90) + new Vector2(320, 240));
                        }
                        SetSoul(1);
                        TP();
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.BoxBackColor = Color.Black;
                        });
                        /* Unused v2
                        var spears = GetAll<NormalSpear>();
                        foreach (NormalSpear sprs in spears)
                        {
                            sprs.Acceleration = 0.7f;
                            sprs.Rebound = false;
                        }*/

                    });
                    RegisterFunctionOnce("Box", () =>
                    {
                        BoxUtils.DeVertexify(new(320 - 42, 240 - 42, 84, 84));
                        ScreenDrawing.BoxBackColor = Color.Black;
                    });
                    RegisterFunctionOnce("BounceSpear", () =>
                    {
                        var dir = 45;
                        Vector2 pos = GetVector2(150, GametimeF * 360 / BeatTime(1)) + new Vector2(320, 240);
                        for (int i = 0; i < 4; i++)
                        {
                            CreateEntity(new NormalSpear(pos, dir + i * 60, 3)
                            {
                                DelayTargeting = false,
                                Rebound = true,
                                ReboundCount = 5,
                                Duration = 600,
                                Acceleration = 0.012f,
                                WaitingTime = BeatTime(1)
                            });
                        }
                    });
                    RegisterFunctionOnce("CornerSpear", () =>
                    {
                        var dir = (GametimeF / BeatTime(1) - 200) * 30 + 165;
                        CreateEntity(new NormalSpear(new(0, 0), dir, 2.6f)
                        {
                            DelayTargeting = false,
                            Rebound = true,
                            ReboundCount = 3,
                            Duration = 600,
                            Acceleration = 0.012f,
                            WaitingTime = BeatTime(1)
                        });
                        CreateEntity(new NormalSpear(new(0, 0), 90 - dir, 2.6f)
                        {
                            DelayTargeting = false,
                            Rebound = true,
                            ReboundCount = 3,
                            Duration = 600,
                            Acceleration = 0.012f,
                            WaitingTime = BeatTime(1)
                        });
                        CreateEntity(new NormalSpear(new(640, 0), dir + 90, 2.6f)
                        {
                            DelayTargeting = false,
                            Rebound = true,
                            ReboundCount = 3,
                            Duration = 600,
                            Acceleration = 0.012f,
                            WaitingTime = BeatTime(1)
                        });
                        CreateEntity(new NormalSpear(new(640, 0), 180 - dir, 2.6f)
                        {
                            DelayTargeting = false,
                            Rebound = true,
                            ReboundCount = 3,
                            Duration = 600,
                            Acceleration = 0.012f,
                            WaitingTime = BeatTime(1)
                        });
                    });
                    RegisterFunctionOnce("RainSpear", () =>
                    {
                        for (int i = -10; i < 30; i += 3)
                        {
                            CreateEntity(new NormalSpear(new(i * 32, 0), 65, 2.6f)
                            {
                                DelayTargeting = false,
                                Duration = 600,
                                Acceleration = 0.012f,
                                WaitingTime = BeatTime(1),
                                IsMute = true
                            });
                            CreateEntity(new NormalSpear(new(i * 32, 0), 125, 2.6f)
                            {
                                DelayTargeting = false,
                                Duration = 600,
                                Acceleration = 0.012f,
                                WaitingTime = BeatTime(2),
                                IsMute = true
                            });
                        }
                    });
                    RegisterFunctionOnce("SpearFake", () =>
                    {
                        float rot = 0;
                        Fortimes(15, () =>
                        {
                            CreateSpear(new CircleSpear(Heart.Centre, 8, 5f, 200, rot += 24, 0.005f) { MarkScore = false });
                            CreateSpear(new CircleSpear(Heart.Centre, -8, 5f, 200, rot, 0.005f) { MarkScore = false });
                        });
                    });
                    RegisterFunctionOnce("BoundA", () =>
                    {
                        RunEase((s) => { ScreenDrawing.BoundColor = Color.Lerp(Color.Black, Color.Red, s); },
                            LinkEase(EaseOut(BeatTime(2), 0.65f, 0.35f, EaseState.Quad), Stable(0, 0.45f)));
                        RunEase(s => ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(4), 160, EaseState.Quint), Stable(0, 160)));
                        RunEase(s => ScreenDrawing.DownBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(8), 24 * 8, 120, EaseState.Quint), Stable(0, 120)));
                    });
                    RegisterFunctionOnce("BoundB", () =>
                    {
                        RunEase(s => ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = ScreenDrawing.DownBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(2), 160, 0, EaseState.Quint), Stable(0, 0)));
                        RunEase(s => ScreenDrawing.DownBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(2), 120, 0, EaseState.Quint), Stable(0, 0)));
                    });
                    RegisterFunctionOnce("GaussBlur", () =>
                    {
                        RunEase((s) => { Blur.Sigma = s * 1.5f; },
                            LinkEase(EaseOut(BeatTime(1), 0, 1.75f, EaseState.Linear), EaseOut(BeatTime(3), 1.75f, 0.5f, EaseState.Quint), Stable(0, 0)));
                        DelayBeat(30, () => { RunEase(s => Blur.Sigma = s, EaseOut(BeatTime(1), 0.5f, 0, EaseState.Linear)); });
                        DelayBeat(0.5f, () =>
                        {
                            RunEase(s => StepSample.Intensity = s,
                                LinkEase(EaseOut(BeatTime(0.75f), 0, 0.13f, EaseState.Linear), EaseOut(BeatTime(1.2f), 0.13f, 0, EaseState.Sine)));
                        });
                    });
                    RegisterFunctionOnce("SCL", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), 4.0f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCR", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), -4.0f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCS", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.037f, 1.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("Eff1", () =>
                    {
                        production4 = ScreenDrawing.ActivateShader(Shaders.Scatter, 0.50125f);
                        Shaders.Scatter.Ratio = 0.05f;
                        Shaders.Scatter.Intensity = 12f;
                        RunEase(s => splitter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 6, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 12, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Ratio = s,
                            EaseOut(BeatTime(1.5f), 0.4f, 0.05f, EaseState.Quad)
                        );
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            Alternate(1.0f,
                                EaseOut(BeatTime(1.1f), 3.5f, 0, EaseState.Circ),
                                EaseOut(BeatTime(1.1f), -3.5f, 0, EaseState.Circ)
                            )
                        );
                        DelayBeat(1.5f, () =>
                        {
                            production4.Dispose();
                        });
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line a = new(EaseOut(BeatTime(0.7f), new Vector2(0, 240), new Vector2(640, 240), EaseState.Quad).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(0.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(1.0f, 9.0f, false);
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line a = new(EaseOut(BeatTime(0.7f), new Vector2(640, 240), new Vector2(0, 240), EaseState.Quad).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(0.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(1.0f, 9.0f, true);
                    });
                    RegisterFunctionOnce("TurnR", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640, 240), new Vector2(120, 240), EaseState.Quart),
                            EaseOut(BeatTime(0.7f), new Vector2(80, 240), new Vector2(640, 240), EaseState.Quad)).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(1.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(true);
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(false);
                        });
                    });
                    RegisterFunctionOnce("TurnL", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0, 240), new Vector2(640 - 120, 240), EaseState.Quart),
                            EaseOut(BeatTime(0.7f), new Vector2(640 - 120, 240), new Vector2(0, 240), EaseState.Quad)).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(1.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(false);
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(true);
                        });
                    });
                    RegisterFunctionOnce("LineG1", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0), new Vector2(160, 120), EaseState.Quart),
                            Stable(0, new Vector2(160, 120))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.65f };
                        Line b = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0 - 40, 0 - 30), new Vector2(160 - 40, 120 - 30), EaseState.Quart),
                            Stable(0, new Vector2(160 - 40, 120 - 30))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.4f };
                        Line c = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640 - 0, 480 - 0), new Vector2(640 - 160, 480 - 120), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160, 480 - 120))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.65f };
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640 - 0 + 40, 480 + 30), new Vector2(640 - 160 + 40, 480 - 120 + 30), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160 + 40, 480 - 120 + 30))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.4f };
                        Line[] lines = { a, b, c, d };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(0.7f, () => { l.AlphaDecrease(BeatTime(1.2f)); });
                            DelayBeat(1.9f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineG2", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640, 0), new Vector2(640 - 160, 120), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160, 120))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.65f };
                        Line b = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640 + 40, 0 - 30), new Vector2(640 - 160 + 40, 120 - 30), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160 + 40, 120 - 30))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.4f };
                        Line c = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0, 480), new Vector2(160, 480 - 120), EaseState.Quart),
                            Stable(0, new Vector2(160, 480 - 120))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.65f };
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0 - 40, 480 + 30), new Vector2(160 - 40, 480 - 120 + 30), EaseState.Quart),
                            Stable(0, new Vector2(160 - 40, 480 - 120 + 30))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.4f };
                        Line[] lines = { a, b, c, d };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(0.7f, () => { l.AlphaDecrease(BeatTime(1.2f)); });
                            DelayBeat(1.9f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("SCS-G", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.07f, 1.0f, EaseState.Quint));
                        ScreenDrawing.MakeFlicker(Color.White * 0.6f);
                        Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 - 320), new Vector2(320 - 160, 240 - 160), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(320 - 160, 240 - 160)),
                            EaseIn(BeatTime(1.5f), new Vector2(320 - 160, 240 - 160), new Vector2(320 - 320, 240 - 320), EaseState.Cubic)).Easing,
                            Stable(0, -45).Easing)
                        { Alpha = 0.7f };
                        Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 + 320), new Vector2(320 - 160, 240 + 160), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(320 - 160, 240 + 160)),
                            EaseIn(BeatTime(1.5f), new Vector2(320 - 160, 240 + 160), new Vector2(320 - 320, 240 + 320), EaseState.Cubic)).Easing,
                            Stable(0, 45).Easing)
                        { Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                            }
                            DelayBeat(6, () => { l.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(7.5f, () => { l.Dispose(); });
                        }
                        DelayBeat(2, () =>
                        {
                            RunEase(s => ScreenDrawing.ScreenScale = s,
                                EaseOut(BeatTime(2), 1.12f, 1.0f, EaseState.Quint));
                            ScreenDrawing.MakeFlicker(Color.White * 0.6f);
                            Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320, 240 - 320), new Vector2(320, 240 - 120), EaseState.Quart),
                                Stable(BeatTime(2), new Vector2(320, 240 - 120)),
                                EaseIn(BeatTime(1.5f), new Vector2(320, 240 - 120), new Vector2(320, 240 - 320), EaseState.Cubic)).Easing,
                                Stable(0, 0).Easing)
                            { Alpha = 0.7f };
                            Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240), new Vector2(320 - 120, 240), EaseState.Quart),
                                Stable(BeatTime(2), new Vector2(320 - 120, 240)),
                                EaseIn(BeatTime(1.5f), new Vector2(320 - 120, 240), new Vector2(320 - 320, 240), EaseState.Cubic)).Easing,
                                Stable(0, 90).Easing)
                            { Alpha = 0.7f };
                            CreateEntity(a, b);
                            a.VerticalMirror = true;
                            b.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                a.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                                b.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                            }
                            DelayBeat(4, () => { a.AlphaDecrease(BeatTime(1)); b.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(5.5f, () => { a.Dispose(); b.Dispose(); });
                        });
                        DelayBeat(4, () =>
                        {
                            RunEase(s => ScreenDrawing.ScreenScale = s,
                                EaseOut(BeatTime(1), 1.17f, 1.0f, EaseState.Quint));
                            ScreenDrawing.MakeFlicker(Color.White * 0.6f);
                            Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 - 320), new Vector2(320 - 80, 240 - 80), EaseState.Quart),
                                Stable(BeatTime(0), new Vector2(320 - 80, 240 - 80)),
                                EaseIn(BeatTime(1.5f), new Vector2(320 - 80, 240 - 80), new Vector2(320 - 320, 240 - 320), EaseState.Cubic)).Easing,
                                Stable(0, -45).Easing)
                            { Alpha = 0.7f };
                            Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 + 320), new Vector2(320 - 80, 240 + 80), EaseState.Quart),
                                Stable(BeatTime(0), new Vector2(320 - 80, 240 + 80)),
                                EaseIn(BeatTime(1.5f), new Vector2(320 - 80, 240 + 80), new Vector2(320 - 320, 240 + 320), EaseState.Cubic)).Easing,
                                Stable(0, 45).Easing)
                            { Alpha = 0.7f };
                            Line[] lines = { a, b };
                            foreach (Line l in lines)
                            {
                                CreateEntity(l);
                                l.TransverseMirror = true;
                                for (int i = 0; i < 5; i++)
                                {
                                    l.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                                }
                                DelayBeat(2, () => { l.AlphaDecrease(BeatTime(1)); });
                                DelayBeat(3.5f, () => { l.Dispose(); });
                            }
                        });
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "(pre)", "", "", "", //Red soul begin
                        //1
                        "(BoundA)(GaussBlur)(CornerSpear)", "", "CornerSpear", "",    "", "RainSpear", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",    
                        //2
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //3
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",    
                        //4
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(BoundB)", "", "", "SpearFake",    "", "", "", "", //Red soul end
                        "(Green)(Box)", "", "", "",    "", "", "", "",      
                        //5
                        "(d1)(+01)(LineL)", "", "", "",    "(d1)(+01)(SCS)(LineL)", "", "+01", "",
                        "(d1)(+01)(LineL)", "", "+01", "",    "(d1)(+01)(SCS)(LineL)", "", "+01", "",
                        "(d1)(+01)(LineL)", "", "+01", "",    "(d1)(+01)(SCS)(LineL)", "", "+01", "",
                        "(d1)(+01)(LineL)", "", "+01", "",    "(d1)(+01)(SCS)(TurnR)", "", "+01", "",    
                        //6
                        "(d)(+0)", "", "", "",    "(d)(+0)(SCS)(LineR)", "", "+0", "",
                        "(d)(+0)(LineR)", "", "+0", "",    "(d)(+0)(SCS)(LineR)", "", "", "",
                        "(d)(+0)(LineR)", "", "+0", "",    "(d)(+0)(SCS)(LineR)", "", "+0", "",
                        "(d)(+0)(LineR)", "", "+0", "",    "(d)(+0)(SCS)(TurnL)", "", "+0", "",      
                        //7
                        "*^$01'1.3(*^$21'1.3)(Eff1)", "", "", "",    "(d)(+0)(SCS)(SCL)(LineG2)", "", "+0", "",
                        "(d)(+0)(LineR)", "", "+0", "",    "(d)(+0)(SCS)(SCR)(LineG1)", "", "+0", "",
                        "(d)(+0)(LineL)", "", "+0", "",    "(d)(+0)(SCS)(SCL)(LineG2)", "", "+0", "",
                        "(d)(+0)(LineR)", "", "+0", "",    "(d)(+0)(SCS)(SCR)(LineG1)", "", "n3", "",    
                        //8
                        "$1(SCS-G)", "", "$1", "$1",    "$31", "", "$31", "",
                        "$31", "", "$31", "$31",    "$1", "", "$1", "",
                        "(D)(+01)", "", "(D)(+01)", "",    "(D)(+01)", "", "(D)(+01)", "",
                        "(D)(+01)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(72 + 192))
                {
                    easeA.TagApply("B");
                    easeB.TagApply("A");
                    easeX.RevolutionEase(Stable(2, -45));
                    RegisterFunctionOnce("pre", () =>
                    {
                        easeX.TagApply("X");
                        Arrow[] all = GetAll<Arrow>();
                        foreach (Arrow arr in all)
                        {
                            arr.LateWaitingScale = 0.3f;
                        }
                    });
                    RegisterFunctionOnce("changeEase", () =>
                    {
                    });
                    RegisterFunctionOnce("ROTL", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("ML", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenPositionDelta = new Vector2(s * 1.45f, 0),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine)
                        );
                        RunEase((s) => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic)
                        );
                        RunEase((s) => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.0f, 1.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("ROTR", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenAngle = -s,
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("MR", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenPositionDelta = new Vector2(-s * 1.45f, 0),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine)
                        );
                        RunEase((s) => ScreenDrawing.ScreenAngle = -s,
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic)
                        );
                        RunEase((s) => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.0f, 1.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("ShakeA", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenAngle = s, false,
                            EaseOut(BeatTime(0.5f), -2.1f, 0, EaseState.Circ),
                            EaseOut(BeatTime(0.5f), -2.1f, 0, EaseState.Circ),
                            EaseOut(BeatTime(0.5f), 2.1f, 0, EaseState.Circ),
                            EaseOut(BeatTime(0.5f), 2.1f, 0, EaseState.Circ)
                        );
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line l;
                        CreateEntity(l = new Line(
                                EaseOut(BeatTime(1.2f), new Vector2(666, 0), EaseState.Quart).Easing,
                                Stable(BeatTime(2), 90).Easing
                            )
                        { Alpha = 0.5f });
                        l.DelayDispose(BeatTime(1.2f));
                        l.InsertRetention(new(0.5f, 0.6f));
                        l.InsertRetention(new(1.0f, 0.4f));
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line l;
                        CreateEntity(l = new Line(
                                EaseOut(BeatTime(1.2f), new(640, 0), new Vector2(-26, 0), EaseState.Quart).Easing,
                                Stable(BeatTime(2), 90).Easing
                            )
                        { Alpha = 0.5f });
                        l.DelayDispose(BeatTime(1.2f));
                        l.InsertRetention(new(0.5f, 0.6f));
                        l.InsertRetention(new(1.0f, 0.4f));
                    });
                    RegisterFunctionOnce("SCL", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), 1.7f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCR", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), -1.7f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCL2", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.5f), 1.1f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCR2", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.5f), -1.1f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCS", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.04f, 1.0f, EaseState.Quint)
                        );
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.7f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "($2)(ROTL)", "$2", "$20", "$20",     "($01)", "$01", "$01", "$01",
                        "($3)", "", "($0)", "$00",    "$00", "", "($3)", "",
                        "$3(ML)", "", "($1)", "$1",    "$1", "", "($1)", "$1",
                        "$1", "", "($3)", "$3",    "$3", "", "", "",     
                        //2
                        "($11)(ROTR)", "", "$31", "",    "($21)", "$21", "($0)", "$0",
                        "($11)", "$11", "$11", "",    "($21)", "", "$21", "",
                        "($31)(MR)", "", "($2)", "$2",    "$2", "", "($01)", "",
                        "($11)", "", "($3)", "$3",    "$3", "", "$31", "",      
                        //3
                        "^$3'1.1(^$31'1.1)(ROTL)", "", "$20", "$20",     "$01", "$01", "$00", "$00",
                        "$00", "", "$21", "$21",    "$21", "", "$0", "",
                        "$0(ML)", "", "$01", "$01",    "$01", "", "$2", "$2",
                        "$2", "", "$21", "$21",    "$01", "", "", "",      
                        //4      
                        "$20(ROTR)", "", "$00", "$00",    "$10", "$10", "$20", "$20",
                        "$30", "$30", "$01", "$01",    "$11", "$11", "$21", "$21",
                        "$31", "", "$31", "$30",    "$31", "", "$0", "",
                        "($00'1.2)($21'1.2)", "", "($00'1.2)($21'1.2)", "",    "($00'1.2)($21'1.2)", "", "($00'1.2)($21'1.2)", "",   
                        //5
                        "", "", "", "",
                        "$1(SCS)(LineL)", "$1", "$1", "",    "$3", "", "$1", "$1",
                        "$1(SCS)(LineL)", "", "$31", "",    "$11", "", "$11", "$11",
                        "$11(SCS)(LineL)", "", "$31", "$31",    "$31", "", "$11", "$11",
                        "$11(SCS)(LineL)", "", "", "",    
                        //6
                        "$31(SCL)", "", "$31", "",    "$31(SCS)(LineR)", "$11", "$11", "$11",
                        "$2(SCR)", "$2", "$2", "",    "$0(SCS)(LineR)", "", "$2", "",
                        "$0(SCL)", "", "$2", "$2",    "$2(SCS)(LineR)", "", "$31", "",
                        "$31(SCR)", "", "$11", "$11(changeEase)",    "$11(SCS)(LineR)", "", "$0", "",      
                        //7
                        "^$1'1.8(^$11'1.8)(SCL)", "", "$11(SCS)(LineL)", "",    "$0", "$00", "$1", "$1",
                        "$2(SCR)", "", "$21", "$21",    "$21(SCS)(LineL)", "", "$01", "",
                        "$21(SCL)", "", "$0", "$0",    "$0(SCS)(LineL)", "", "$2", "$2",
                        "$2(SCR)", "", "$0", "$0",    "$0(SCS)", "", "", "",    
                        //8
                        "$20'1.2(SCL2)", "", "$31'1.1(SCR2)", "$31'1.1(SCR2)",    "$31'1.1(SCR2)", "", "$20'1.2(SCL2)", "",
                        "$0'1.2(SCR2)", "", "$20'1.2(SCL2)", "",    "", "", "($30'1.2)(SCR2)", "",
                        "($30'1.2) ", "", "$10", "$11",    "$10", "$11", "$10", "",
                        "$11", "$10",    "$11", "$10", "$11",
                    });
                }
                NRArea1();
                if (InBeat(392))
                {
                    RegisterFunctionOnce("heal", () =>
                    {
                        Regenerate(3); PlaySound(Sounds.heal);
                    });
                    Settings.GreenTap = true;
                    RegisterFunctionOnce("BugLine", () =>
                    {
                        var cen = LinkEase(false, Stable(0, new Vector2(180, 240)),
                            Linear(BeatTime(2f), new Vector2(180, 240), new Vector2(180, 240) + GetVector2(180, -24)),
                            Stable(0f, new Vector2(520, 300)),
                            Linear(BeatTime(2.5f), new Vector2(520, 300), new Vector2(520, 300) + GetVector2(240, -130)),
                            Stable(0f, new Vector2(320, 440)),
                            Linear(BeatTime(2.5f), new Vector2(320, 440), new Vector2(320, 440) + new Vector2(0, -240)),
                            Stable(0f, new Vector2(0)),
                            Linear(BeatTime(5f), new Vector2(0), new Vector2(0) + new Vector2(240, 0)),
                            Stable(BeatTime(8f), new Vector2(320, 240))
                            );
                        var rot = LinkEase(false, Stable(0, -24 + 90),
                            Stable(BeatTime(2), -24 + 90),
                            Stable(BeatTime(2.5f), -130 + 90),
                            Stable(BeatTime(2.5f), 0),
                            Stable(BeatTime(5f), 90),
                            Linear(BeatTime(8), -60, -60 + 720)
                            );
                        Line l = new(cen.Easing, rot.Easing) { DrawingColor = Color.DarkRed, Width = 8 };
                        CreateEntity(l);
                        RunEase((sr) => { l.Alpha = sr; }, false, Alternate(2, Stable(BeatTime(16), 0f), Stable(BeatTime(16), 0.8f)));
                        for (int a = 0; a < 4; a++)
                        {
                            int x = a;
                            Line.RetentionEffect r = new(4 * x);
                            l.InsertRetention(r);
                        }
                    });
                    RegisterFunctionOnce("BugLine2", () =>
                    {
                        var cen = LinkEase(false, Stable(0, new Vector2(0, 80)),
                            EaseOut(BeatTime(2f), new Vector2(0, 80), new Vector2(0, 160), EaseState.Sine)
                            );
                        var rot = LinkEase(false, Stable(0, 0)
                            );
                        Line l = new(cen.Easing, rot.Easing) { DrawingColor = Color.DarkRed, Width = 8, VerticalMirror = true };
                        CreateEntity(l);
                        RunEase((sr) => { l.Alpha = sr; }, false, Alternate(2, Stable(BeatTime(4) + 0.5f, 0f), Stable(BeatTime(4), 0.8f)));
                        for (int a = 0; a < 4; a++)
                        {
                            int x = a;
                            Line.RetentionEffect r = new(BeatTime(1) * x);
                            l.InsertRetention(r);
                        }
                    });
                    RegisterFunctionOnce("CrossLine", () =>
                    {
                        Line[] ls = GetAll<Line>();
                        for (int a = 0; a < ls.Length; a++)
                        {
                            int x = a;
                            ls[x].Dispose();
                        }

                        var ce = EaseInOut(BeatTime(16), 0, 360 * 3, EaseState.Quad).Easing;
                        Line l = new(Stable(0, new Vector2(320, 240)).Easing, ce) { DrawingColor = Color.Gray };
                        CreateEntity(l);
                        RunEase((s) => { l.Alpha = s; },
                            Stable(0, 0),
                            EaseOut(BeatTime(1), 1, EaseState.Sine),
                            Stable(BeatTime(12), 1),
                            EaseIn(BeatTime(1), -1.5f, EaseState.Sine)
                            );
                        for (int a = 0; a < 8; a++)
                        {
                            int x = a;
                            Line.RetentionEffect r = new(8 * x, (8 - x) * 0.1f);
                            l.InsertRetention(r);
                        }
                    });
                    RegisterFunctionOnce("pre", () =>
                    {
                        ScreenDrawing.SceneRendering.ResetProduction();
                        easeA?.Dispose(); easeB?.Dispose();
                        AddInstance(easeA = new Arrow.UnitEasing()
                        {
                            ApplyTime = BeatTime(3.25f),
                            RotationEase = LinkEase(
                                Stable(BeatTime(0.5f), 0),
                                EaseOut(BeatTime(2.7f), 0, -45, EaseState.Sine))
                        });
                        AddInstance(easeB = new Arrow.UnitEasing()
                        {
                            ApplyTime = BeatTime(3.25f),
                            RotationEase = LinkEase(
                                Stable(BeatTime(0.5f), -90),
                                EaseOut(BeatTime(2.7f), -90, -45, EaseState.Sine))
                        });
                        easeA.TagApply("A"); easeB.TagApply("B");
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "pre", "", "", "",    "", "", "", "",
                        "", "", "", "",    "heal", "", "", "",  
                          
                        //1
                        "!!3", "$1(BugLine)(<0.5,1>Drum)", "$1", "$1",    "!!3", "$1(<0.5,1>Drum)", "$1", "$1",
                        "!!3", "d(<0.5,1>Drum)", "+0", "d",    "!!3", "+0(<0.5,1>Drum)", "d", "+0",
                        "!!3", "d(<0.5,1>Drum)", "+0", "d",    "!!3", "+0(<0.5,1>Drum)", "d", "+0",
                        "!!3", "d(<0.5,1>Drum)", "+0", "d",    "!!3", "+0(<0.5,1>Drum)", "+0", "+0", 

                        //2
                        "!!3", "D(<0.5,-1>Drum)", "+0", "+10",    "!!3", "+0(<0.5,-1>Drum)", "+10", "+0",
                        "!!3", "+10(<0.5,-1>Drum)", "+0", "+10",    "!!3", "+0(<0.5,-1>Drum)", "+10", "+0",
                        "!!3", "-10(<0.5,-1>Drum)(BugLine2)", "+0", "-10",    "!!3", "+0(<0.5,-1>Drum)", "-10", "+0",
                        "!!3", "-10(<0.5,-1>Drum)", "+0", "-10",    "!!3", "+0(<0.5,-1>Drum)", "", "",

                        //3
                        "!!3", "d1(<0.5,1>Drum)(CrossLine)", "+01", "d1",    "!!3", "+01(<0.5,1>Drum)", "d1", "+01",
                        "!!3", "d1(<0.5,1>Drum)", "+01", "d1",    "!!3", "+01(<0.5,1>Drum)", "d1", "+01",
                        "!!3", "d1(<0.5,1>Drum)", "+01", "d1",    "!!3", "+01(<0.5,1>Drum)", "d1", "+01",
                        "!!3", "d1(<0.5,1>Drum)", "+01", "d1",    "!!3", "+01(<0.5,1>Drum)", "d1", "+01",  

                        //4
                        "!!3", "D1(<0.5,-1>Drum)", "+01", "+11",    "!!3", "+01(<0.5,-1>Drum)", "+11", "+01",
                        "!!3", "+11(<0.5,-1>Drum)", "+01", "+11",    "!!3", "+01(<0.5,-1>Drum)", "+11", "+01",
                        "!!3", "-11(<0.5,-1>Drum)", "+01", "-11",    "!!3", "+01(<0.5,-1>Drum)", "-11", "+01",
                        "!!3", "-11(<0.5,-1>Drum)", "+01", "-11",    "-01(<0.5,-1>Drum)"
                    });
                }
                if (InBeat(424))
                {
                    float x1 = 0;
                    float x2 = 0;
                    RegisterFunctionOnce("Value", () =>
                    {
                        RunEase((s) =>
                        {
                            x1 = s;
                        },
                        EaseOut(BeatTime(24), 320, 100, EaseState.Back),
                        EaseOut(BeatTime(8), 100, 180, EaseState.Back)
                        );
                        RunEase((s) =>
                        {
                            x2 = s;
                        },
                        EaseOut(BeatTime(24), 320, 540, EaseState.Back),
                        EaseOut(BeatTime(8), 540, 460, EaseState.Back)
                        );
                    });
                    RegisterFunctionOnce("Line", () =>
                    {
                        for (int i = 0; i < 42; i++)
                        {
                            int x = i;
                            DelayBeat(x * 0.66f, () =>
                            {
                                Line l = new(320, 90) { DrawingColor = Color.DarkRed, Width = 17, Image = Sprites.lightLine };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                            DelayBeat(x * 0.66f, () =>
                            {
                                Line l = new(320, 90) { DrawingColor = Color.DarkRed * 0.25f, Width = 17 };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                        }
                        for (int i = 0; i < 28; i++)
                        {
                            int x = i;
                            DelayBeat(0.33f + x * 1f, () =>
                            {
                                Line l = new(x2, 90) { DrawingColor = Color.DarkRed, Width = 17, Image = Sprites.lightLine };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                            DelayBeat(0.33f + x * 1f, () =>
                            {
                                Line l = new(x2, 90) { DrawingColor = Color.DarkRed * 0.25f, Width = 17 };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                            DelayBeat(1f + x * 1f, () =>
                            {
                                Line l = new(x1, 90) { DrawingColor = Color.DarkRed, Width = 17, Image = Sprites.lightLine };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                            DelayBeat(1f + x * 1f, () =>
                            {
                                Line l = new(x1, 90) { DrawingColor = Color.DarkRed * 0.25f, Width = 17 };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                        }
                    });
                    RegisterFunctionOnce("KickLine", () =>
                    {
                        Line l = new(LinkEase(Stable(0, new Vector2(320, 240)), EaseOut(BeatTime(2f), new Vector2(20, 0), EaseState.Quart)).Easing, Stable(0, 45).Easing) { Alpha = 0.67f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        Line l2 = new(LinkEase(Stable(0, new Vector2(320, 240)), EaseOut(BeatTime(2f), new Vector2(100, 0), EaseState.Quart)).Easing, Stable(0, 45).Easing) { Alpha = 0.67f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        CreateEntity(l, l2);
                        l.AlphaDecrease(BeatTime(2));
                        l2.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("QuickKick", () =>
                    {
                        var ce = LinkEase
                        (Stable(0, new Vector2(320, 240)),
                        EaseOut(BeatTime(1f), new Vector2(20, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(80, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-20, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-80, 0), EaseState.Quart),
                        EaseOut(BeatTime(2f), new Vector2(100, 0), EaseState.Quart)
                        ).Easing;
                        var ce2 = LinkEase
                        (Stable(0, new Vector2(320, 240)),
                        EaseOut(BeatTime(1f), new Vector2(80, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(120, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-80, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-120, 0), EaseState.Quart),
                        EaseOut(BeatTime(2f), new Vector2(200, 0), EaseState.Quart)
                        ).Easing;
                        Line l = new(ce, Stable(0, 45).Easing) { Alpha = 0.5f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        Line l2 = new(ce2, Stable(0, 45).Easing) { Alpha = 0.67f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        CreateEntity(l, l2);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(2));
                        l2.DelayAlphaDecrease(BeatTime(4), BeatTime(2));
                    });

                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "(^d'1.3)(^+01'1.3)(Value)(Line)(KickLine)(<1.9,-4>Drum)", "", "", "",    "d", "", "", "",
                        "d(<0>SetScreenAngle)", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",   

                        //2
                        "(^d'1.3)(^+01'1.3)(KickLine)(<1.9,4>Drum)", "", "", "",    "d", "", "", "",
                        "d(<0>SetScreenAngle)", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",   

                        //3
                        "(^d'1.3)(^+01'1.3)(KickLine)(<1.9,-4>Drum)", "", "", "",    "d", "", "", "",
                        "d(<0>SetScreenAngle)", "", "", "",    "d", "", "", "",
                        "(^d'1.3)(^+01'1.3)(KickLine)(<1.9,4>Drum)", "", "", "",    "d", "", "", "",
                        "d(<0>SetScreenAngle)", "", "", "",    "d", "", "", "",

                        //4
                        "(^$1'1.6)(^+21'1.6)(QuickKick)(<0.4,-1>Drum)", "", "", "",    "(^+1'1.6)(^+21'1.6)(<0.4,1>Drum)", "", "", "",
                        "(^+1'1.6)(^+21'1.6)(<0.4,-1>Drum)", "", "", "",    "(^+1'1.6)(^+21'1.6)(<0.4,1>Drum)", "", "", "",
                        "(^+1'1.6)(^+21'1.6)(<1.9,-5>Drum)", "", "", "",    "", "", "", "",
                        "(<0>SetScreenAngle)", "", "", "",    "", "", "", "",

                    });
                }
                if (InBeat(456))
                {
                    RegisterFunctionOnce("KickR", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; },
                            EaseOut(BeatTime(2), new Vector2(-20, 20), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(-10, -40), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(-30, 30), EaseState.Quad),
                            EaseOut(BeatTime(1), new Vector2(-40, 30), EaseState.Quad),
                            EaseOut(BeatTime(1.3f), new Vector2(100, -40), EaseState.Back)
                            );
                    });
                    RegisterFunctionOnce("KickL", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; },
                            EaseOut(BeatTime(2), new Vector2(+20, 20), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(+10, -40), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(+30, 30), EaseState.Quad),
                            EaseOut(BeatTime(1), new Vector2(+40, 30), EaseState.Quad),
                            EaseOut(BeatTime(1.3f), new Vector2(-100, -40), EaseState.Back)
                            );
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "n2(KickR)", "", "", "",    "", "", "", "",
                        "(n2)", "", "", "",    "", "", "($0)", "",
                        "(R)(+01)", "", "(R)(+01)", "",    "(R)(+01)", "", "(R)(+01)", "",
                        "(R)(+01)", "", "(R)(+01)", "",    "(R)(+01)", "", "$0($01)", "",  

                        //2
                        "!!6/3", "$01(KickL)", "$01", "$01",    "$01", "",    "^$01'2", "", "", "",
                        "#4#$0", "", "", "",     "", "", "$11", "",
                        "", "", "", "",     "$11", "", "", "",
                        "(n21)", "", "$2", "",     "", "", "$1", "",

                        //3
                        "(d)(+01)(KickR)", "", "", "",    "n2", "", "n2", "",
                        "n2", "", "n2", "",    "", "", "($1)(+01)", "",
                        "", "", "n1", "",    "($1)(+01)", "", "", "",
                        "($1)(+01)", "", "", "",    "n1", "", "", "",  

                        //4
                        "($1)(+01)(KickL)", "", "", "",    "(d)(+01)", "", "", "",
                        "(d)(+01)", "", "n0", "",    "n0", "", "(d)(+01)", "",
                        "", "", "", "",    "n0", "", "", "",
                        "(n0)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(488))
                {
                    RegisterFunctionOnce("ConvL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(Arguments[1], BeatTime(Arguments[0]), true);
                    });
                    RegisterFunctionOnce("ConvR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(Arguments[1], BeatTime(Arguments[0]), false);
                    });
                    RegisterFunctionOnce("KickLine", () =>
                    {
                        var ce = LinkEase(
                            Stable(0, new Vector2(320, 240) + GetVector2(40, -30)),
                            EaseOut(BeatTime(2), GetVector2(80, -30), EaseState.Quad)
                            );
                        Line l = new(ce.Easing, Stable(0, -30 + 90).Easing) { ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "(d)(+01)(<3.8,6>Drum)(KickLine)", "", "", "",    "d", "", "", "",
                        "(d)(+01)", "", "+0", "",    "", "", "(d)(+01)", "",
                        "<0>SetScreenAngle", "", "", "",    "(d)(+01)", "", "", "",
                        "(d)(+01)", "", "+0", "",    "", "", "d", "",  

                        //2
                        "(#2.1#+0)(<6,8>ConvR)", "", "", "",    "", "", "", "",
                        "d1", "", "", "",    "", "", "d1", "",
                        "", "", "", "",    "(d)(+01)", "", "", "",
                        "(d)(+01)", "", "d", "",    "", "", "", "",  

                        //3
                        "(#10.5#d)(+01)(<8,8>ConvL)", "", "", "",    "", "", "", "",
                        "d1", "", "", "",    "", "", "d1", "",
                        "", "", "", "",    "d1", "", "", "",
                        "d1", "", "", "",    "", "", "", "",  

                        //4
                        "d1(<8,8>ConvR)", "", "", "",    "", "", "", "",
                        "d1", "", "", "",    "", "", "d1", "",
                        "", "", "d1", "",    "d1", "", "d1", "",
                        "($1)(+01)", "", "+0", "",    "($3)(+01)", "", "+0", "",    "(^$0'1.3)(^$21'1.3)"
                    });
                }
                if (InBeat(520))
                {
                    Arrow.UnitEasing arse = new();
                    arse.ApplyTime = BeatTime(3);
                    arse.RotationEase = EaseOut(BeatTime(3), -40, 0, EaseState.Sine);
                    arse.TagApply("L");
                    AddInstance(arse);
                    Arrow.UnitEasing arse1 = new();
                    arse1.ApplyTime = BeatTime(3);//BeatTime(8*(6/0.8f));
                    arse1.RotationEase = EaseOut(BeatTime(3), 40, 0, EaseState.Sine);
                    //arse1.DistanceEase = LinkEase(Stable(BeatTime(8 * (6 / 0.8f)-4), BeatTime(8*6)),EaseOut(BeatTime(4), BeatTime(8*6), 0, EaseState.Quint));
                    /*arse1.PositionEase = LinkEase(
                        Stable(BeatTime(8 * (6 / 0.5f) - 4), new Vector2(BeatTime(8 * (6 / 0.5f)) * 0.5f)),
                        EaseOut(BeatTime(4),new Vector2(BeatTime(8 * (6 / 0.5f)) * 0.5f,0),new Vector2( 0,0), EaseState.Quint));*/
                    //arse1.PositionEase = LinkEase(
                    //Stable(BeatTime(2), new Vector2(0, 100)),
                    //EaseOut(BeatTime(4), new Vector2(0, 100), new Vector2(0), EaseState.Quint));

                    arse1.TagApply("R");
                    AddInstance(arse1);
                    RegisterFunctionOnce("ScreenPoint", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        }, Stable(0, new Vector2(0)),
                        EaseOut(BeatTime(12), new Vector2(-120, 0), EaseState.Sine),
                        EaseIn(BeatTime(4), new Vector2(120, 0), EaseState.Quad),
                        EaseOut(BeatTime(12), new Vector2(120, 0), EaseState.Sine),
                        EaseIn(BeatTime(4), new Vector2(-120, 0), EaseState.Quad));
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 7f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",

                        "(<0.8,1.5>Drum)(ScreenPoint)", "", "", "",    "(~_$21@R)(<0.8,1.5>Drum)(N2)", "", "(N2)", "",
                        "~_$21@L(<0.8,1.5>Drum)", "", "(N2)", "",    "~_$21@R(<0.8,1.5>Drum)", "", "(N2)", "",
                        "~_$21@L(<0.8,1.5>Drum)", "", "(N2)", "",    "~_$21@R(<0.8,1.5>Drum)", "", "(N2)", "",
                        "~_$21@L(<0.8,1.5>Drum)", "", "", "",    "~_$21@R(<0.8,1.5>Drum)", "", "", "",

                        //2
                        "~_$21@L(<0.8,1.5>Drum)(N2)", "", "", "",    "~_$21@R(<0.8,1.5>Drum)(N2)", "", "(N2)", "",
                        "~_$21@L(<0.8,1.5>Drum)", "", "(N2)", "",    "~_$21@R(<0.8,1.5>Drum)", "", "(N2)", "",
                        "~_$21@L(<0.8,1.5>Drum)", "", "(N2)", "",    "~_$21@R(<0.8,1.5>Drum)(N2)", "", "(N2)", "",
                        "~_$21@L(<0.8,1.5>Drum)(N2)", "", "(N2)", "",    "~_$21@R(<0.8,1.5>Drum)", "", "", "",  

                        //3
                        "~_$01@L(<0.8,-1.5>Drum)(N0)", "", "", "",    "~_$01@R(<0.8,-1.5>Drum)(N0)", "", "(N0)", "",
                        "~_$01@L(<0.8,-1.5>Drum)", "", "(N0)", "",    "~_$01@R(<0.8,-1.5>Drum)", "", "(N0)", "",
                        "~_$01@L(<0.8,-1.5>Drum)", "", "(N0)", "",    "~_$01@R(<0.8,-1.5>Drum)", "", "(N0)", "",
                        "~_$01@L(<0.8,-1.5>Drum)", "", "(N0)", "",    "~_$01@R(<0.8,-1.5>Drum)", "", "", "",

                        //0
                        "~_$01@L(<0.8,-1.5>Drum)(N0)", "", "", "",    "~_$01@R(<0.8,-1.5>Drum)(N0)", "", "", "",
                        "~_$01@L(<0.8,-1.5>Drum)(N0)", "", "(N0)", "",    "~_$01@R(<0.8,-1.5>Drum)(N0)", "", "(N0)", "",
                        "~_$01@L(<0.8,-1.5>Drum)", "", "", "",    "~_$01@R(<0.8,-1.5>Drum)", "", "(N0)", "",
                        "~_$01@L(<0.8,-1.5>Drum)", "", "", "",    "~_$01@R(<0.8,-1.5>Drum)", "", "", "",
                    });
                }
                if (InBeat(552))
                {
                    int rand1 = Rand(0, 3);
                    int rand2 = Rand(0, 3);
                    void Allocate()
                    {
                        ArrowAllocate(1, rand1);
                        ArrowAllocate(2, rand2);
                    }
                    while (rand2 == rand1)
                    {
                        rand2 = Rand(0, 3);
                    }
                    RegisterFunctionOnce("Rerand", () =>
                    {
                        rand1 = Rand(0, 3);
                        rand2 = Rand(0, 3);
                        while (rand2 == rand1)
                        {
                            rand2 = Rand(0, 3);
                        }
                        Allocate();
                    });
                    Allocate();
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",

                        "(d)(+01)", "", "", "",    $"(A1)(A11)", "", $"(A2)(A21)", "",
                        "", "", "(d)(+01)", "",    "", "", $"(<!,4>Rerand)(A1)(A11)", "",
                        "", "", $"(A2)(A21)", "",    "", "", $"(#2#$1)($11)", "",
                        "", "", "", "",    "", "", "", "",

                        "(d)(+01)", "", "", "",    $"(<!,4>Rerand)(A2)(A21)", "", $"(A1)(A11)", "",
                        "", "", "(d)(+01)", "",    "", "", "(d)(+01)", "",
                        "", "", "(d)(+01)", "",    "", "", "(d)(+01)", "",
                        "", "", "(d)(+01)", "",    "(d)(+01)", "", "(d)(+01)", "",

                        "(#2.75#+0)(+01)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(#2.75#d1)(+0)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",

                        "(d)(+01)", "", "", "",    $"(<!,4>Rerand)(A1)(A11)", "", $"(A2)(A21)", "",
                        "", "", "(d)(+01)", "",    "", "", "(d)(+01)", "",
                        "", "", "", "",    "(d)(+01)", "", "", "",
                        "(d)(+01)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(584))
                {
                    RegisterFunctionOnce("KickR", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; },
                            EaseOut(BeatTime(2), new Vector2(-20, 20), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(-10, -40), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(-30, 30), EaseState.Quad),
                            EaseOut(BeatTime(1), new Vector2(-40, 30), EaseState.Quad),
                            EaseOut(BeatTime(1.3f), new Vector2(100, -40), EaseState.Back)
                            );
                    });
                    RegisterFunctionOnce("KickL", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; },
                            EaseOut(BeatTime(2), new Vector2(+20, 20), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(+10, -40), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(+30, 30), EaseState.Quad),
                            EaseOut(BeatTime(1), new Vector2(+40, 30), EaseState.Quad),
                            EaseOut(BeatTime(1.3f), new Vector2(-100, -40), EaseState.Back)
                            );
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",

                        "$0(KickR)", "", "+1", "",    "+1", "", "+1", "",
                        "+1", "", "+1", "",    "+1", "", "+1", "",
                        "+1", "", "+1", "",    "+1", "", "+1", "",
                        "(+11)(+0)", "", "(+11)(+0)", "",    "(+11)(+0)", "", "(+11)(+0)", "",

                        "(-11)(+0)(KickL)", "", "-11", "",    "-11", "", "-11", "",
                        "-11", "", "-11", "",    "-11", "", "-11", "",
                        "-11", "", "-11", "",    "-11", "", "-11", "",
                        "(-11)(+0)", "", "(+11)(+0)", "",    "(+11)(+0)", "", "(+11)(+0)", "",

                        "(+11)(+0)", "", "+1", "",    "+1", "", "+1", "",
                        "+1", "", "+1", "",    "+1", "", "+1", "",
                        "(+1)(+01)", "", "+1", "",    "+1", "", "+1", "",
                        "+1", "", "+1", "",    "+1", "", "+1", "",

                        "(R)(+01)", "", "+2", "",    "(d)(+01)", "", "(+0)(+01)", "",
                        "+2", "","(d)(+01)", "",   "+0", "","(d)(+01)", "",
                        "+0", "", "+2", "",    "(d)(+01)", "", "+0", "",
                        "(^$3'1.4)($31'1.4)", "", "$311'1.5", "$301'1.5",    "$311'1.5", "", "$301'1.5", "",
                    });
                }
                if (InBeat(616))
                {
                    RegisterFunctionOnce("heal", () =>
                    {
                        Regenerate(3); PlaySound(Sounds.heal);
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 8f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "heal", "", "", "",

                        "($0)($21)", "", "($0)($21)", "",    "($0)($21)", "", "($0)($21)", "",
                        "($3)($31)", "", "($3)($31)", "($3)($31)",    "($3)($31)", "", "($3)($31)", "",
                        "($2)($01)", "", "($2)($01)", "",    "($20)($01)", "", "($20)($01)", "",
                        "$21", "", "$21", "",    "$0", "", "$0", "",

                        "($2)($01)", "", "($2)($01)", "",    "($20)($01)", "", "($20)($01)", "",
                        "($30)($31)", "($3)($31)",    "($3)($31)", "",    "($3)($31)", "", "($3)($31)", "",
                        "($0)($21)", "", "($0)($21)", "",    "($0)($21)", "", "($0)($21)", "",
                        "($0)", "", "($0)", "",    "($21)", "", "($21)", "",

                        "($1)($11)", "", "($1)($11)", "",    "($1)($11)", "(~_$0)(~_$21)", "($1)($11)", "",
                        "($1)($11)", "", "($1)($11)", "(~_$0)(~_$21)",    "($1)($11)", "", "($1)($11)", "",
                        "($1)($11)", "", "($1)($11)", "",    "($3)($31)", "", "($3)($31)", "",
                        "($1)($11)", "", "($1)($11)", "",    "($3)($31)", "", "($3)($31)", "",

                        "($3)($31)", "", "($3)($31)", "",    "($3)($31)", "(~_$0)(~_$21)", "($3)($31)", "",
                        "($3)($31)", "", "($3)($31)", "(~_$0)(~_$21)",    "($3)($31)", "", "($3)($31)", "",
                        "($3)($31)", "", "($3)($31)", "",    "($1)($11)", "", "($1)($11)", "",
                        "($3)($31)", "", "($3)($31)", "",    "($1)($11)", "", "($1)($11)", "",
                    });
                }
                if (InBeat(648))
                {
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                         
                        //1
                        "($0)($21)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "($01)($2)", "", "", "",
                        "R1", "", "+11", "",    "+11", "", "+11", "",
                        "R", "", "+1", "",    "+1", "", "+1", "",
                        //2
                       "!!6/3", "($1)", "$1", "$1",    "$1", "",    "$1", "", "", "",
                        "(#4.6#$1)", "", "", "",     "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "d", "",    "", "", "d", "", 
                        //3
                        "d", "", "", "",    "d1", "", "+01", "",
                        "", "", "d1", "",    "", "", "d", "",
                        "d", "", "+0", "",    "d", "", "+0", "",
                        "d", "", "", "",    "d", "", "", "",
                        //4
                        "^d'1.6(^d1'1.6)", "", "", "",    "^d'1.6(^d1'1.6)", "", "", "",
                        "(^$00'1.6)(^$21'1.6)", "", "(^$00'1.6)(^$21'1.6)", "",    "(^$00'1.6)(^$21'1.6)", "", "(^#1#$00'1.6)(^#1#$21'1.6)", "",
                        "", "", "", "",    "", "", "$2", "$2",
                        "$2", "", "", "",    "", "", "", "",

                    });
                }
                if (InBeat(680))
                {
                    int temp;
                    int rand1 = Rand(0, 3), rand2 = (temp = Rand(0, 2)) < rand1 ? temp : temp + 1;
                    void Allocate()
                    {
                        ArrowAllocate(1, rand1);
                        ArrowAllocate(2, rand2);
                    }
                    RegisterFunctionOnce("Rerand", () =>
                    {
                        rand1 = Rand(0, 3);
                        rand2 = Rand(0, 3);
                        while (rand2 == rand1)
                        {
                            rand2 = Rand(0, 3);
                        }
                        Allocate();
                    });
                    Allocate();
                    easeA?.Dispose(); easeB?.Dispose();
                    easeA = new(); easeB = new();
                    easeA.RotationEase = EaseOut(BeatTime(3), 90, 0, EaseState.Sine);
                    easeB.RotationEase = EaseOut(BeatTime(3), -90, 0, EaseState.Sine);
                    easeA.ApplyTime = BeatTime(3); easeB.ApplyTime = BeatTime(3);
                    easeA.TagApply("A"); easeB.TagApply("B");
                    AddInstance(easeA); AddInstance(easeB);

                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        
                        //1
                         "(d1)(+01)", "", "", "",    $"(A11)(A11)", "", $"(A21)(A21)", "",
                        "", "", "(d)(+0)", "",    "", "", $"(<!>Rerand)(A11)(A11)", "",
                        "", "", $"(A21)(A21)", "",    "", "", $"(#2#A1)(A1)", "",
                        "", "", "", "",    "", "", "", "",
                        //2
                        "(d)(+0)", "", "", "",    $"(<!>Rerand)(A11)(A11)", "", $"(A2)(A2)", "",
                        "", "", "(d)(+0)", "",    "", "", "(d)(+0)", "",
                        "", "", "(d)(+0)", "",    "", "", "(d)(+0)", "",
                        "", "", "(d)(+0)", "",    "(d)(+0)", "", "(+0)(+0)", "",
                        //3
                        "(#2.75#+0)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(#2.75#d)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "''9",
                        //4 
                        "($00@A)($00@B)", "", "($00@A)($00@B)", "",    "($00@A)($00@B)", "", "($00@A)($00@B)", "",
                        "($21@B)($21@A)", "", "($21@A)($21@B)", "",    "($21@A)($21@B)", "", "($21@A)($21@B)", "",
                        "$0@A", "+00@B", "+00@A", "+00@B",    "+20@A", "+00@B", "+00@A", "+00@B",
                        "+21@A", "+01@B", "+01@A", "+01@B",    "+21@A", "+01@B", "+01@A", "+01@B",

                    });
                }
                NRRest1();
                NRFinal();
            }

            private void NRArea1()
            {
                if (InBeat(328))
                {
                    Settings.VoidArrowVolume = 0.0f;
                    DelayBeat(6.5f, () =>
                    {
                        easeA.Dispose(); easeA = new(); AddInstance(easeA);
                        easeB.Dispose(); easeB = new(); AddInstance(easeB);
                        easeX.RevolutionEase(Stable(1, 13));
                        easeY.RevolutionEase(Stable(1, 26));
                        easeZ.RevolutionEase(Stable(1, 39));
                        easeX.TagApply("X");
                        easeY.TagApply("Y");
                        easeZ.TagApply("Z");
                        easeU.RevolutionEase(Stable(1, 52));
                        easeV.RevolutionEase(Stable(1, 65));
                        easeW.RevolutionEase(Stable(1, 78));
                        easeU.TagApply("U");
                        easeV.TagApply("V");
                        easeW.TagApply("W");

                        float selfRota = 30f;
                        easeX.SelfRotationEase(Stable(1, selfRota));
                        easeY.SelfRotationEase(Stable(1, selfRota));
                        easeZ.SelfRotationEase(Stable(1, selfRota));
                        easeU.SelfRotationEase(Stable(1, selfRota));
                        easeV.SelfRotationEase(Stable(1, selfRota));
                        easeW.SelfRotationEase(Stable(1, selfRota));

                        float time = BeatTime(1.5f);
                        easeA.ApplyTime = time;
                        easeB.ApplyTime = time;
                        easeC.ApplyTime = time;
                        easeD.ApplyTime = time;
                        easeE.ApplyTime = time;
                        easeF.ApplyTime = time;
                        easeA.RotationEase = Linear(3f, -13);
                        easeB.RotationEase = Linear(4f, -26);
                        easeC.RotationEase = Linear(5f, -39);
                        easeD.RotationEase = Linear(6f, -52);
                        easeE.RotationEase = Linear(7f, -65);
                        easeF.RotationEase = Linear(8f, -78);
                        easeA.SelfRotation = -selfRota;
                        easeB.SelfRotation = -selfRota;
                        easeC.SelfRotation = -selfRota;
                        easeD.SelfRotation = -selfRota;
                        easeE.SelfRotation = -selfRota;
                        easeF.SelfRotation = -selfRota;

                        easeA.TagApply("A");
                        easeB.TagApply("B");
                        easeC.TagApply("C");
                        easeD.TagApply("D");
                        easeE.TagApply("E");
                        easeF.TagApply("F");
                    });

                    (CurrentScene as SongFightingScene).Accuracy.SpecifyTime = 0.71f;

                    RegisterFunctionOnce("pre", () =>
                    {
                        Regenerate();
                        PlaySound(Sounds.heal);
                        RunEase(s => sans.Alpha = s, Linear(BeatTime(1), 0.48f));
                        RunEase(s => sans.Offset = new(s, 0), EaseOut(BeatTime(1.2f), 200, 0, EaseState.Back));
                    });

                    // Generate the effects
                    DelayBeat(4, () =>
                    {
                        Effect01();
                    });

                    // Generate the Snakes
                    CreateChart(BeatTime(4 + 2), BeatTime(1), 7.3f, new string[] {  
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "", 
                        //1
                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",
                        "$1", "~_!$1", "~_!$1", "~_!$1",    "~_!$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@A", "~_!$1@B", "~_!$1@C", "~_!$1@D", "~_!$1@E", "~_$1@F",
                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",
                        "$1", "~_!$1", "~_!$1", "~_!$1",    "~_!$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@A", "~_!$1@B", "~_!$1@C", "~_!$1@D", "~_!$1@E", "~_$1@F",
                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",
                        "$1", "~_!$1", "~_!$1", "~_!$1",    "~_!$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@A", "~_!$1@B", "~_!$1@C", "~_!$1@D", "~_!$1@E", "~_$1@F",
                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",

                        "$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@X", "~_!$1@Y", "~_!$1@Z", "~_!$1@U", "~_!$1@V", "~_$1@W",
                        "$2", "~_!$2", "~_!$2", "",
                        //2
                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",
                        "$31", "~_!$31", "~_!$31", "~_!$31",    "~_!$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@A", "~_!$31@B", "~_!$31@C", "~_!$31@D", "~_!$31@E", "~_$31@F",
                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",
                        "$31", "~_!$31", "~_!$31", "~_!$31",    "~_!$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@A", "~_!$31@B", "~_!$31@C", "~_!$31@D", "~_!$31@E", "~_$31@F",
                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",
                        "$31", "~_!$31", "~_!$31", "~_!$31",    "~_!$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@A", "~_!$31@B", "~_!$31@C", "~_!$31@D", "~_!$31@E", "~_$31@F",
                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",
                        "$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@X", "~_!$31@Y", "~_!$31@Z", "~_!$31@U", "~_!$31@V", "~_$31@W",
                        "$01", "~_!$01", "~_!$01", "",
                    });
                    easeG.TagApply("G");
                    RegisterFunction("WindSlow", () =>
                    {
                        RunEase((speed) =>
                        {
                            r.BasicSpeed = speed;
                        },
                        Stable(0, 1),
                        EaseOut(BeatTime(1), -0.7f, EaseState.Quad)
                        );
                        RunEase((speed) =>
                        {
                            r.Intensity = speed;
                        },
                        Stable(0, 10),
                        EaseOut(BeatTime(1), 10f, EaseState.Quad)
                        );
                    });
                    RegisterFunction("WindFast", () =>
                    {
                        RunEase((speed) =>
                        {
                            r.BasicSpeed = speed;
                        },
                        Stable(0, 0.3f),
                        EaseOut(BeatTime(1), 0.7f, EaseState.Quad)
                        );
                        RunEase((speed) =>
                        {
                            r.Intensity = speed;
                        },
                        Stable(0, 20),
                        EaseOut(BeatTime(1), -10f, EaseState.Quad)
                        );
                    });
                    RegisterFunction("WindFaster", () =>
                    {
                        RunEase((speed) =>
                        {
                            r.BasicSpeed = speed;
                        },
                        Stable(0, 1f),
                        EaseOut(BeatTime(1), 0.25f, EaseState.Quad)
                        );
                        RunEase((speed) =>
                        {
                            r.Intensity = speed;
                        },
                        Stable(0, 10),
                        EaseOut(BeatTime(1), -2f, EaseState.Quad)
                        );
                    });

                    RegisterFunctionOnce("HandR", () =>
                    {
                        sans.MoveHand(0);
                    });
                    RegisterFunctionOnce("HandB", () =>
                    {
                        sans.MoveHand(1);
                    });
                    RegisterFunctionOnce("HandL", () =>
                    {
                        sans.MoveHand(2);
                    });
                    RegisterFunctionOnce("HandU", () =>
                    {
                        sans.MoveHand(3);
                    });
                    RegisterFunctionOnce("MoveSans", () =>
                    {
                        RunEase(s => sans.Offset = new(s, 0),
                            EaseOut(BeatTime(0.5f), 0, 40, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), 0, 40, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("MoveBack", () =>
                    {
                        RunEase(s => sans.Offset = new(s, 0),
                            EaseOut(BeatTime(1.5f), 80, 0, EaseState.Cubic)
                            );
                    });

                    CreateChart(BeatTime(4), BeatTime(2), 12, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "pre", "", "", "",    "", "", "", "", 

                        //1
                        "($01@G)(HandR)", "", "", "",    "($01@G)(HandB)", "", "", "",
                        "($01@G)(HandR)", "", "", "",    "($01@G)(HandB)", "", "", "",
                        "($01@G)(HandR)", "", "", "",    "($01@G)(HandB)", "", "", "",
                        "($01@G)(HandR)", "", "", "",    "($01@G)(MoveSans)", "", "($01@G)", "",     

                        //2
                        "($20@G)(HandL)", "", "", "",    "($20@G)(HandU)", "", "", "",
                        "($20@G)(HandL)", "", "", "",    "($20@G)(HandU)", "", "", "",
                        "($20@G)(HandL)", "", "", "",    "($20@G)(HandU)", "", "", "",
                        "($20@G)(WindSlow)(HandL)", "", "", "($20)",    "MoveBack", "", "", "",     
                        //3
                        "''5",
                        "n01", "~_!+01", "~_!+01", "~_!+01",    "~_+01", "~_!+01", "~_+01", "",
                        "d", "~_!+0", "~_+0", "",    "d1", "~_!+01", "~_+01", "",
                        "d", "~_!+0", "~_+0", "",    "d1", "~_!+01", "~_+01", "",
                        "n2", "~_!+0", "~_+0", "",    "+0", "~_!+0", "~_+0", "", 
 
                        //4
                        "!!3", "$1", "$1", "$1",    "!!3", "$2", "$2", "$2",
                        "!!3", "$3", "$3", "$3",    "!!3", "$0", "$0", "$0",
                        "!!3", "$2", "$2", "$1",    "!!3", "$1", "$0", "$0",
                        "!!3", "$2", "$1", "$0",
                    });
                }
                if (InBeat(358))
                {
                    easeJ.PositionEase = LinkEase(
                        Stable(BeatTime(0), new Vector2(-42, 0)),
                        EaseOut(BeatTime(1.75f), new Vector2(-42, 0), new Vector2(18, 0), EaseState.Quad));
                    easeJ.ApplyTime = BeatTime(1.75f);
                    easeJ.TagApply("K1C");
                    easeK.PositionEase = LinkEase(
                        Stable(BeatTime(0), new Vector2(42, 0)),
                        EaseOut(BeatTime(1.75f), new Vector2(42, 0), new Vector2(-18, 0), EaseState.Quad));
                    easeK.ApplyTime = BeatTime(1.75f);
                    easeK.TagApply("K1D");
                    RegisterFunctionOnce("pre", () =>
                    {
                        easeH.PositionEase = LinkEase(
                            Stable(BeatTime(4), new Vector2(0)),
                            EaseOut(BeatTime(0.75f), new Vector2(0), new Vector2(-18, 0), EaseState.Quad));
                        easeH.ApplyTime = BeatTime(4.75f);
                        easeH.TagApply("K1A");
                        easeI.PositionEase = LinkEase(
                            Stable(BeatTime(4), new Vector2(0)),
                            EaseOut(BeatTime(0.75f), new Vector2(0), new Vector2(18, 0), EaseState.Quad));
                        easeI.ApplyTime = BeatTime(4.75f);
                        easeI.TagApply("K1B");
                        AddInstance(easeK1 = new Arrow.ClassicApplier());
                        DelayBeat(3.75f, () =>
                        {
                            easeK1.ApplyDelay(BeatTime(2));
                        });
                        easeK1.TagApply("K1");
                        easeS1.TagApply("S1");
                        easeT1.TagApply("T1");
                        easeS1.RevolutionEase(Stable(1, 13));
                        easeT1.RevolutionEase(Stable(1, -13));
                        easeS2.TagApply("S2");
                        easeT2.TagApply("T2");
                        easeS2.RevolutionEase(Stable(1, 17));
                        easeT2.RevolutionEase(Stable(1, -17));
                    });
                    RegisterFunctionOnce("HandR", () =>
                    {
                        sans.MoveHand(0);
                    });
                    RegisterFunctionOnce("HandB", () =>
                    {
                        sans.MoveHand(1);
                    });
                    RegisterFunctionOnce("HandL", () =>
                    {
                        sans.MoveHand(2);
                    });
                    RegisterFunctionOnce("HandU", () =>
                    {
                        sans.MoveHand(3);
                    });
                    RegisterFunctionOnce("MoveSans", () =>
                    {
                        RunEase(s => sans.Offset = new(s, 0),
                            EaseOut(BeatTime(1f), 0, 70, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("MoveBack", () =>
                    {
                        RunEase(s => sans.Offset = new(s, 0),
                            EaseOut(BeatTime(1.5f), 70, 0, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("Remove", () => { sans.Alpha = 0f; });
                    CreateChart(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "pre", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "($3@K1,K1A)(_$3@K1,K1B)(WindFast)", "", "", "",    "($3@K1,K1A)(_$3@K1,K1B)(MoveSans)", "", "", "", 

                        //1
                        "(*>$01)(*<$21)(~_$01'1.4)(~_$21'2.2)(HandU)", "", "", "",    "(*<$01)(*>$21)(~_$01'2.2)(~_$21'2.2)(HandU)", "", "", "",
                        "($3@K1C)($3@K1D)(*>$01)(*<$21)(~_$01'2.2)(~_$21'2.2)(HandU)", "", "", "",    "(<$3@K1C)(>$3@K1D)(*<$01)(*>$21)(~_$01'2.2)(~_$21'2.2)(HandU)", "", "", "",
                        "($3@K1C)($3@K1D)(*>$01)(*<$21)(~_$01'2.2)(~_$21'2.2)(HandU)", "", "", "",    "(<$3@K1C)(>$3@K1D)(*<$01)(*>$21)(~_$01'2.2)(~_$21'2.2)(HandU)", "", "", "",
                        "($3@K1C)($3@K1D)(*>$01)(*<$21)(~_$01'2.2)(~_$21'2.2)(HandU)", "", "", "",    "(<$3@K1C)(>$3@K1D)(HandU)", "", "(>$3@K1C)(_<$3@K1D)", "",
                        
                        //2
                        "(<$11@K1C)(>$11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)(HandB)", "", "", "",    "(HandB)($11@K1C)($11@K1D)(*>$0)(*<$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",
                        "(<$11@K1C)(>$11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)(HandB)", "", "", "",    "(HandB)($11@K1C)($11@K1D)(*>$0)(*<$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",
                        "(<$11@K1C)(>$11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)(HandB)", "", "", "",    "(HandB)($11@K1C)($11@K1D)(*>$0)(*<$2)(~_$0'2.2)(~_$2'2.2)", "", "(<$11@K1C)(>$11@K1D)", "",
                        "($11@K1C)($11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)(WindSlow)(HandB)", "", "", "(<$11@K1C)(>$11@K1D)",    "MoveBack($11@K1C)($11@K1D)", "", "(<$11@K1C)(>$11@K1D)", "",
                        //3 
                        "''5",
                        "n0", "~_!+0", "~_!+0", "~_!+0",    "~_+0", "~_!+0", "~_+0", "",
                        "d1", "~_!+01", "~_+01", "",    "d", "~_!+0", "~_+0", "",
                        "!!3","$21", "$21", "$21",    "!!3","$01", "$01", "$01",
                        "!!3","$21(WindFast)", "$21", "$21","!!3","$01", "$01", "$01",
 
                        //4
                        "!!3", "($31)(+01)", "+01", "+01",    "!!3", "$01", "-01", "-01",
                        "!!3", "($11)(+01)", "$11", "$21",    "!!3", "$21", "$31", "$31",
                        "(*$01'1.4@S1)(*$21'1.4@S2)(*$00'2.1@T1)(*$20'2.1@T2)", "", "", "",    "", "", "", "",
                        "", "", "", "Remove",    "", "", "", "",     
 
                        //5
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",     

                        //6
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",     

                        //7
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",     

                        //8 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                    });
                }
            }
            private void NRBuildup()
            {
                if (InBeat(72))
                {
                    RegisterFunctionOnce("LoadA", () =>
                    {
                        Line a = new(EaseOut(BeatTime(2), new Vector2(0), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), -40).Easing) { Alpha = 0.7f };
                        Line b = new(EaseOut(BeatTime(2), new Vector2(640, 480), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), -40).Easing) { Alpha = 0.7f };
                        Line[] line = { a, b };
                        foreach (Line l in line)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 6; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.7f - i * 0.07f));
                            }
                            DelayBeat(4f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LoadB", () =>
                    {
                        Line a = new(EaseOut(BeatTime(2), new Vector2(640, 0), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), 40).Easing) { Alpha = 0.7f };
                        Line b = new(EaseOut(BeatTime(2), new Vector2(0, 480), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), 40).Easing) { Alpha = 0.7f };
                        Line[] line = { a, b };
                        foreach (Line l in line)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 6; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.7f - i * 0.07f));
                            }
                            DelayBeat(4f, () => { l.Dispose(); });
                        }
                        DelayBeat(2, () =>
                        {
                            float k = -20f;
                            AddInstance(new TimeRangedEvent(20f, () =>
                            {
                                Line c = new(new Vector2(320 + (-20 + k) * (-20 + k) * 0.0625f - 100, 240), 130) { Alpha = 0.75f };
                                Line d = new(new Vector2(320 - (-20 + k) * (-20 + k) * 0.0625f + 100, 240), 130) { Alpha = 0.75f };
                                CreateEntity(c);
                                CreateEntity(d);
                                AddInstance(new TimeRangedEvent(10, () =>
                                {
                                    c.Alpha -= 0.075f;
                                    d.Alpha -= 0.075f;
                                }));
                                k++;
                                DelayBeat(3, () => { c.Dispose(); d.Dispose(); });
                            }));
                        });//你要不要看看你写了什么史
                    });
                    RegisterFunctionOnce("KickA", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(0.8f), new Vector2(0), new Vector2(120, 90), EaseState.Quart),
                            Stable(BeatTime(0), new Vector2(320, 240))).Easing,
                            Stable(BeatTime(1), -37.5f).Easing)
                        { Alpha = 0.6f };
                        Line b = new(LinkEase(EaseOut(BeatTime(0.8f), new Vector2(0, 480), new Vector2(120, 480 - 90), EaseState.Quart),
                            Stable(BeatTime(0), new Vector2(160, 120))).Easing,
                            Stable(BeatTime(1), 37.5f).Easing)
                        { Alpha = 0.6f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.75f - i * 0.05f));
                            }
                            DelayBeat(0, () => { l.AlphaDecrease(BeatTime(1.2f), 0.75f); });
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("KickB1", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(1), new Vector2(160, 240)),
                            EaseIn(BeatTime(3), new Vector2(160, 240), new Vector2(580, 240), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(580, 240))).Easing, Stable(BeatTime(8), 90).Easing)
                        { Alpha = 0.75f };
                        Line b = new(EaseOut(BeatTime(1.5f), new Vector2(160, 240), new Vector2(160 + 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line c = new(EaseOut(BeatTime(1.5f), new Vector2(160, 240), new Vector2(160 - 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line[] linesA = { b, c };
                        foreach (Line lA in linesA)
                        {
                            CreateEntity(lA);
                            lA.AlphaDecrease(BeatTime(1.5f), 0.55f);
                            DelayBeat(1.5f, () => { lA.Dispose(); });
                        }
                        CreateEntity(a);
                        DelayBeat(5, () => { a.AlphaDecrease(BeatTime(1), 0.75f); });//Group1
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(580, 240), new Vector2(500, 240), EaseState.Quart),
                            Stable(BeatTime(1), new Vector2(500, 240))).Easing,
                            Stable(BeatTime(2), 90).Easing)
                        { Alpha = 0.55f };
                        Line e = new(Stable(BeatTime(2), new Vector2(580, 420)).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 70, EaseState.Quad),
                            Stable(BeatTime(0.5f), 70)).Easing)
                        { Alpha = 0.75f };
                        Line f = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(580, 60), new Vector2(480 - 50, 60), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(480 - 50, 60))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 120, EaseState.Quad),
                            Stable(BeatTime(0.5f), 120)).Easing)
                        { Alpha = 0.55f };
                        Line g = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(580, 420), new Vector2(380 - 100, 420), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(380 - 100, 420))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 50, EaseState.Quad),
                            Stable(BeatTime(0.5f), 50)).Easing)
                        { Alpha = 0.35f };
                        Line[] linesB = { d, e, f, g };
                        foreach (Line lB in linesB)
                        {
                            DelayBeat(4, () => { CreateEntity(lB); });
                            DelayBeat(5, () => { lB.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(6, () => { lB.Dispose(); });
                        }
                        DelayBeat(6, () => { a.Dispose(); });
                    });//你要不要看看你写了什么史啊？？
                    RegisterFunctionOnce("KickB2", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(1), new Vector2(480, 240)),
                            EaseIn(BeatTime(3), new Vector2(480, 240), new Vector2(60, 240), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(60, 240))).Easing, Stable(BeatTime(8), 90).Easing)
                        { Alpha = 0.75f };
                        Line b = new(EaseOut(BeatTime(1.5f), new Vector2(480, 240), new Vector2(480 + 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line c = new(EaseOut(BeatTime(1.5f), new Vector2(480, 240), new Vector2(480 - 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line[] linesA = { b, c };
                        foreach (Line lA in linesA)
                        {
                            CreateEntity(lA);
                            lA.AlphaDecrease(BeatTime(1.5f), 0.55f);
                            DelayBeat(1.5f, () => { lA.Dispose(); });
                        }
                        CreateEntity(a);
                        DelayBeat(5, () => { a.AlphaDecrease(BeatTime(1), 0.75f); });
                        Line a1 = new(LinkEase(Stable(BeatTime(0), new Vector2(480 - 240, 240)),
                            EaseIn(BeatTime(2), new Vector2(480 - 240, 240), new Vector2(60, 240), EaseState.Quart)).Easing,
                            Stable(BeatTime(2), 90).Easing)
                        { Alpha = 0.75f };
                        Line b1 = new(EaseOut(BeatTime(1.5f), new Vector2(480 - 240, 240), new Vector2(480 + 60 - 240, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line c1 = new(EaseOut(BeatTime(1.5f), new Vector2(480 - 240, 240), new Vector2(480 - 60 - 240, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line[] linesA1 = { b1, c1 };
                        foreach (Line lA1 in linesA1)
                        {
                            DelayBeat(2, () =>
                            {
                                CreateEntity(lA1);
                                lA1.AlphaDecrease(BeatTime(1.5f), 0.55f);
                                DelayBeat(1.5f, () => { lA1.Dispose(); });
                            });
                        }
                        DelayBeat(2, () =>
                        {
                            CreateEntity(a1);
                            DelayBeat(2, () => { a1.Dispose(); });
                        });//Group1
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(60, 240), new Vector2(140, 240), EaseState.Quart),
                            Stable(BeatTime(1), new Vector2(140, 240))).Easing,
                            Stable(BeatTime(2), 90).Easing)
                        { Alpha = 0.55f };
                        Line e = new(Stable(BeatTime(2), new Vector2(60, 60)).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 70, EaseState.Quad),
                            Stable(BeatTime(0.5f), 70)).Easing)
                        { Alpha = 0.75f };
                        Line f = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(60, 420), new Vector2(160 + 50, 420), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(160 + 50, 420))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 120, EaseState.Quad),
                            Stable(BeatTime(0.5f), 120)).Easing)
                        { Alpha = 0.55f };
                        Line g = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(60, 60), new Vector2(260 + 100, 60), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(260 + 100, 60))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 50, EaseState.Quad),
                            Stable(BeatTime(0.5f), 50)).Easing)
                        { Alpha = 0.35f };
                        Line[] linesB = { d, e, f, g };
                        foreach (Line lB in linesB)
                        {
                            DelayBeat(4, () => { CreateEntity(lB); });
                            DelayBeat(5, () => { lB.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(6, () => { lB.Dispose(); });
                        }
                        DelayBeat(6, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("Step", () =>
                    {
                        RunEase((s) =>
                        {
                            StepSample.Intensity = s;
                            splitter.Intensity = s * 15;
                        },
                        Stable(0, 0.15f),
                        EaseOut(BeatTime(2), 0.15f, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("SplitterR", () =>
                    {
                        RunEase((s) =>
                        {
                            splitter.Intensity = s;
                        },
                        Stable(0, 9),
                        EaseOut(BeatTime(3), 9, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("SplitterL", () =>
                    {
                        RunEase((s) =>
                        {
                            splitter.Intensity = -s;
                        },
                        Stable(0, 9),
                        EaseOut(BeatTime(3), 9, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("Bound", () =>
                    {
                        ScreenDrawing.BoundColor = Color.White * 0.6f;
                        RunEase((s) => { ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = s; },
                            EaseOut(BeatTime(2), 0, 160, EaseState.Quad),
                            SineWave(15, BeatTime(10), 3, 0),
                            Stable(0, 160));
                    });
                    RegisterFunctionOnce("ConvL", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            Stable(0, 2.5f), EaseOut(BeatTime(3.5f), 2.5f, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("ConvR", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = -s,
                            Stable(0, 2.5f), EaseOut(BeatTime(3.5f), 2.5f, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("SoulL", () =>
                    {
                        RunEase(s => Heart.Rotation = s * 2,
                            EaseOut(BeatTime(0.5f), 0, 1.2f, EaseState.Quart),
                            EaseOut(BeatTime(3.25f), 1.2f, 0, EaseState.Linear),
                            Stable(0, 0));
                    });
                    RegisterFunctionOnce("SoulR", () =>
                    {
                        RunEase(s => Heart.Rotation = -s * 2,
                            EaseOut(BeatTime(0.5f), 0, 1.2f, EaseState.Quart),
                            EaseOut(BeatTime(3.25f), 1.2f, 0, EaseState.Linear),
                            Stable(0, 0));
                    });
                    RegisterFunctionOnce("a", () => { });
                    RegisterFunctionOnce("winder", () =>
                    {
                        //r.Intensity = 6.0f;
                    });

                    RegisterFunctionOnce("pre", () =>
                    {
                        DelayBeat(0.1f, () =>
                        {
                            production4 = ScreenDrawing.ActivateShader(Shaders.Seismic, 0.2341f);

                            Shaders.Seismic.Radius = 460f;
                            RunEase(s => { Shaders.Seismic.Progress = s; },
                                EaseOut(BeatTime(0.85f), 1.0f, EaseState.Quad)
                                );

                            RunEase(s => { ScreenDrawing.ScreenScale = s; },
                                Alternate(0.5f,
                                    EaseOut(BeatTime(0.85f), 1.15f, 1.0f, EaseState.Cubic),
                                    EaseOut(BeatTime(0.85f), 1.1f, 1.0f, EaseState.Cubic),
                                    EaseOut(BeatTime(0.85f), 1.05f, 1.0f, EaseState.Cubic),
                                    Stable(1.0f, 1.0f)

                                    )
                                );

                            DelayBeat(1, production4.Dispose);
                        });
                    });

                    CreateChart(BeatTime(4), BeatTime(2), 6.4f, new string[]
                    {   //10
                        "$0(LoadA)","+0","+0","+0",    "+2","+0","+0","+0",
                        "+11(LoadB)","+01","+01","+01",   "+21","+01","+01","+01",
                        //11

                        "(*$0'1.5)(*$2'1.5)(KickA)(pre)","","","",    "","","","",
                        "(d)","","","",    "","","","",
                        "(d)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        //12
                        "d1","","","",    "","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        //13
                        "(d)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        "d","","","",    "d","","","",
                        "d","","","",    "d","","","",
                        //14
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "","","","",
                        //15
                        "(Step)(KickB1)(Bound)(d)(+01)(winder)","","","",    "","","","",
                        "(d)","","","",    "","","","",
                        "(KickA)(ConvR)(SplitterR)(d)(+01)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        //16
                        "(Step)(KickB2)(d)(+01)","","","",    "","","","",
                        "(Step)(d1)","","","",    "d1","","","",
                        "(KickA)(ConvL)(SplitterL)(d)(+01)","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        //17
                        "(Step)(KickB1)(d)(+01)","","","",    "","","","",
                        "","","","",    "d","","","",
                        "(KickA)(ConvR)(SplitterR)(d)(+01)","","","",    "d","","","",
                        "d","","","",    "d","","","",
                        //18
                        "(Step)(KickB2)(#7.75#$0)(SoulR)","","","",    "","","","",
                        "(Step)","","","",    "","","","",
                        "(KickA)(ConvL)(SplitterL)(#3.75#$21)(SoulL)","","","",    "","","","",
                        "","","","",    "","","","",
                    });
                }
                if (InBeat(72 + 64))
                {
                    RegisterFunctionOnce("UaD", () =>
                    {
                        bool a = true, b = true, c = true;
                        RunEase((s) =>
                        {
                            float u = Clamp(0, (240 - s) / 56f, 14);
                            InstantSetBox(s, 84 + u, 84 + u);
                            InstantTP(new Vector2(320, s));
                            if (s < 150 && a) { a = false; sans.CompLeg.ChangeState(500, (t) => t.Alpha = -10); }
                            if (s < 100 && b) { b = false; sans.CompBody.ChangeState(500, (t) => t.Alpha = 0); }
                            if (s < 50 && c) { c = false; sans.CompHead.ChangeState(500, (t) => t.Alpha = 0); }
                        }, EaseIn(BeatTime(3), 240, -86, EaseState.Quad),
                        EaseOut(BeatTime(16), -86, 240, EaseState.Elastic),
                        Stable(0, 240));
                        DelayBeat(4, () =>
                        {
                            sans.Alpha = 0.0f;
                        });
                    });
                    RegisterFunctionOnce("DownLine", () =>
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Line a = new(LinkEase(Stable(0, new Vector2(0)),
                                EaseOut(BeatTime(3), new Vector2(320, 0), new Vector2(320, i * 32), EaseState.Quart),
                                Stable(BeatTime(1), new Vector2(320, i * 32))).Easing, Stable(0, 0).Easing)
                            { Alpha = 0.75f - i * 0.06f };
                            CreateEntity(a);
                            DelayBeat(2, () =>
                            {
                                a.AlphaDecrease(BeatTime(2));
                            });
                            DelayBeat(4, () => { a.Dispose(); });
                        }
                    });

                    RegisterFunctionOnce("pre", () =>
                    {
                        s = new()
                        {
                            BasicSpeed = 1.2f,
                            Width = 2.2f,
                            DrawingColor = Color.Lerp(Color.Aqua, Color.White, 0.7f),
                            Direction = true
                        };
                        GameStates.InstanceCreate(s);
                        easeA.TagApply("A");
                        DelayBeat(128.5f, () =>
                        {
                            s.Dispose();
                            {
                            }
                        });
                    });
                    RegisterFunctionOnce("Wave", () =>
                    {
                        RunEase(s => Heart.Rotation = s,
                            LinkEase(EaseOut(BeatTime(20), 330, 360, EaseState.Elastic), Stable(0, 0)));
                    });
                    RegisterFunctionOnce("a", () => { });
                    RegisterFunctionOnce("BoundA", () =>
                    {
                        RunEase((s) => { ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = s; },
                            LinkEase(EaseOut(BeatTime(4f), 160, 0, EaseState.Quart),
                            Stable(0, 0)));
                    });
                    RegisterFunctionOnce("BoundB", () =>
                    {
                        int k = 0;
                        ScreenDrawing.BoundColor = Color.Lerp(Color.Black, Color.Red, 0.65f);
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(i * 2, () =>
                            {
                                RunEase((s) => { ScreenDrawing.DownBoundDistance = s; },
                                    EaseOut(BeatTime(1.4f), 24 * k, 24 * k + 48, EaseState.Cubic),
                                    EaseOutIn(BeatTime(0.6f), 24 * k + 48, 24 * k + 24, EaseState.Cubic),
                                    Stable(0, 24 * k + 24));
                                k++;
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGA", () =>
                    {
                        Line a = new(EaseOut(BeatTime(1), new Vector2(320, 0), new Vector2(320, 80), EaseState.Quad).Easing,
                            Stable(BeatTime(1), 0).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        a.VerticalMirror = true;
                        a.AlphaDecrease(BeatTime(1.3f));
                        for (int i = 0; i < 3; i++)
                        {
                            a.InsertRetention(new(BeatTime(0.2f * i)));
                        }
                        DelayBeat(1.3f, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineGA2", () =>
                    {
                        Line a = new(EaseOut(BeatTime(1), new Vector2(0, 240), new Vector2(60, 240), EaseState.Quad).Easing,
                            Stable(BeatTime(1), 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        a.TransverseMirror = true;
                        a.AlphaDecrease(BeatTime(1.3f));
                        for (int i = 0; i < 3; i++)
                        {
                            a.InsertRetention(new(BeatTime(0.2f * i)));
                        }
                        DelayBeat(1.3f, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineGB1", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                k++;
                                Line a = new(Stable(BeatTime(1.5f), new Vector2(640f / 9f * k, 240)).Easing,
                                    LinkEase(EaseOut(BeatTime(1.25f), 90, 110, EaseState.Quad), Stable(0, 110)).Easing)
                                { Alpha = 0.7f };
                                CreateEntity(a);
                                a.AlphaDecrease(BeatTime(1.75f));
                                DelayBeat(1.75f, () => { a.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGB2", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                k++;
                                Line a = new(Stable(BeatTime(1.75f), new Vector2(640 - 640f / 9f * k, 240)).Easing,
                                    LinkEase(EaseOut(BeatTime(1.25f), 90, 70, EaseState.Quad), Stable(0, 70)).Easing)
                                { Alpha = 0.7f };
                                CreateEntity(a);
                                a.AlphaDecrease(BeatTime(1.75f));
                                DelayBeat(1.75f, () => { a.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGC", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.25f * i, () =>
                            {
                                Line a = new(new Vector2(k * 36 - 64, 240), new Vector2(320, k * 27 - 48)) { Alpha = 0.7f };
                                Line b = new(new Vector2(k * 36 - 64, 240), new Vector2(320, 480 - k * 27 + 48)) { Alpha = 0.7f };
                                Line[] lines = { a, b };
                                foreach (Line l in lines)
                                {
                                    CreateEntity(l);
                                    l.TransverseMirror = true;
                                    l.AlphaDecrease(BeatTime(1.75f));
                                    DelayBeat(1.75f, () => { l.Dispose(); });
                                }
                                k++;
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGD1", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(0), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(-320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(2), 90),
                            EaseOut(BeatTime(1.5f), 90, 315, EaseState.Sine),
                            Stable(BeatTime(2), 315)).Easing)
                        { Alpha = 0.7f };
                        Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(0), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(-320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(2), 0),
                            EaseOut(BeatTime(1.5f), 0, 225, EaseState.Sine),
                            Stable(BeatTime(2), 225)).Easing)
                        { Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(BeatTime(0.125f * i), 0.7f - i * 0.08f));
                            }
                            DelayBeat(5.5f, () => { l.Dispose(); });
                            DelayBeat(2, () => { l.AlphaDecrease(1, 0.35f); });
                            DelayBeat(4, () => { l.AlphaIncrease(1, 0.7F); });
                        }
                    });
                    RegisterFunctionOnce("LineGD2", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(0.5f), new Vector2(640, 480), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(640 + 320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(0.5f), 90),
                            EaseOut(BeatTime(1.5f), 90, 315, EaseState.Sine),
                            Stable(BeatTime(2), 315)).Easing)
                        { Alpha = 0.7f };
                        Line b = new(LinkEase(EaseOut(BeatTime(0.5f), new Vector2(640, 480), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(640 + 320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(0.5f), 0),
                            EaseOut(BeatTime(1.5f), 0, 225, EaseState.Sine),
                            Stable(BeatTime(2), 225)).Easing)
                        { Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(BeatTime(0.125f * i), 0.7f - i * 0.08f));
                            }
                            DelayBeat(4, () => { l.Dispose(); });
                            DelayBeat(0.5f, () => { l.AlphaDecrease(1, 0.35f); });
                            DelayBeat(2.5f, () => { l.AlphaIncrease(1, 0.35f); });
                        }
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(2), 5f, EaseState.Cubic),
                            EaseOut(BeatTime(2), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(2), 5f, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("Eff1", () =>
                    {
                        Filter f1 = ScreenDrawing.ActivateShader(Shaders.Scatter, 0.50125f);
                        Shaders.Scatter.Ratio = 0.05f;
                        Shaders.Scatter.Intensity = 24f;
                        RunEase(s => splitter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 12, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 24, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Ratio = s,
                            EaseOut(BeatTime(1.5f), 0.6f, 0.05f, EaseState.Quad)
                        );
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            Alternate(1.0f,
                                EaseOut(BeatTime(1.2f), 5, 0, EaseState.Circ),
                                EaseOut(BeatTime(1.2f), -5, 0, EaseState.Circ)
                            )
                        );
                        DelayBeat(1.5f, () =>
                        {
                            f1.Dispose();
                        });
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "(UaD)",    "", "", "", "",
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "(DownLine)(BoundA)(Wave)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",    
                        //2
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",     
                        //3
                        "R", "", "", "",    "R1", "", "+01", "",
                        "", "", "R", "",    "+0", "", "", "",
                        "R1", "", "+01", "",    "", "", "R", "",
                        "R", "", "+0", "",    "R1", "", "+01", "",    
                        //4
                        "R", "", "", "",    "R1", "", "+01", "",
                        "", "", "R", "",    "+0", "", "", "",
                        "R1", "", "+01", "",    "R", "", "+0", "",
                        "R1", "", "+01", "",    "R", "", "+0", "",    
                        //5
                        "(d)(+01)(BoundB)(LineGA)(Eff1)", "", "", "",    "+2", "", "", "",
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        //6
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        "(d)(+01)(LineGA)", "", "", "",    "+2", "", "", "",
                        //7
                        "(+0)(LineGB1)(LineGA2)(Eff1)", "", "+0", "",    "+1", "", "+0", "",
                        "-1", "", "-0", "",    "-1", "", "-0", "",
                        "(d1)(LineGB2)", "", "+01", "",    "+11", "", "+01", "",
                        "-11", "", "-01", "",    "-11", "", "-01", "",
                        //8 
                        "(d)(LineGC)(Rotate)", "+01", "-0", "+01",    "(-1)", "+01", "-0", "+01",
                        "$3(LineGC)", "$31", "$3", "$31",    "$1", "$11", "$1", "$11",
                        "($00)(LineGD1)", "", "", "",    "", "", "$10(LineGD2)", "",
                        "$30", "", "", "",    "", "", "", "",
                    });
                }
            }
            private void NRPre()
            {
                if (InBeat(0))
                {
                    RegisterFunction("FadeOut", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.MasterAlpha = s;
                        },
                        Stable(BeatTime(8), 0),
                        EaseIn(BeatTime(64), 1, EaseState.Sine)
                        );
                        RunEase((q) =>
                        {
                            ScreenDrawing.ScreenScale = q;
                        },
                        Stable(BeatTime(8), 5f),
                        EaseOut(BeatTime(72), -4f, EaseState.Sine)
                        );
                        RunEase((f) =>
                        {
                            r.BasicSpeed = f;
                        },
                        Stable(BeatTime(8), 0.1f),
                        EaseIn(BeatTime(78), 0.9f, EaseState.Quad));
                    });
                    RegisterFunctionOnce("GaussBlur", () =>
                    {
                        RunEase((s) =>
                        {
                            Blur.Sigma = s;
                        }, Stable(BeatTime(0), 2.2f),
                        EaseOut(BeatTime(16), -2.2f, EaseState.Quad));
                    });
                    RegisterFunctionOnce("Soul1", () =>
                    {
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("Line", () =>
                    {
                        float LineX = Rand(200, 440);
                        float Rot = -100;
                        Line line = new(EaseIn(BeatTime(6), new Vector2(LineX, 240), new Vector2(0, 240), EaseState.Cubic).Easing, Stable(BeatTime(2), Rot).Easing);
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("LineCenter", () =>
                    {
                        Line line1 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        CreateEntity(line1);
                        Line line2 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        CreateEntity(line2);
                        Line line3 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        CreateEntity(line3);
                        Line line4 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        CreateEntity(line4);
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.0f;
                            line.AlphaIncrease(BeatTime(2), 0.8f);
                            DelayBeat(6, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineCenter45", () =>
                    {
                        Line line1 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        CreateEntity(line1);
                        Line line2 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        CreateEntity(line2);
                        Line line3 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        CreateEntity(line3);
                        Line line4 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        CreateEntity(line4);
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.0f;
                            line.AlphaIncrease(BeatTime(2), 0.8f);
                            DelayBeat(6, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineCenter8", () =>
                    {
                        Line line1 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        Line line2 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        Line line3 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        Line line4 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        Line line5 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        Line line6 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        Line line7 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        Line line8 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        Line[] lines = { line1, line2, line3, line4, line5, line6, line7, line8 };
                        foreach (Line l in lines)
                        {
                            l.Alpha = 0f;
                            CreateEntity(l);
                            l.AlphaIncrease(BeatTime(2), 0.8f);
                            for (int i = 1; i < 2; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.7f - i * 0.07f));
                            }
                            DelayBeat(10.0f, () => { l.AlphaDecrease(BeatTime(1.3f)); });
                            DelayBeat(12f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Test", () =>
                    {
                        float[] args = Arguments;
                        ;
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.4f, new string[]
                    {   //0
                        "FadeOut","","","",    "","","","",
                        "<4,5>Test(<2,3>Test)","","","",    "","","","",
                        //1
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //2
                        "GaussBlur(Soul1)(Line)","","Line","",    "Line","","Line","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //3
                        "GaussBlur(Line)","","Line","",    "Line","","(Soul1)(Line)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","(Soul1)","","",    "","","","",
                        //4
                        "GaussBlur(Line)","","Line","",    "Line","","Line","",
                        "","","","",    "(Soul1)","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //5
                        "GaussBlur(Soul1)(Line)","","Line","",    "Line","","Line","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //6
                        "GaussBlur(LineCenter)","","LineCenter","",    "LineCenter","","(Soul1)(LineCenter)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","(Soul1)","","",    "","","","",
                        //7
                        "GaussBlur(LineCenter45)","","LineCenter45","",    "LineCenter45","","LineCenter45","",
                        "","","","",    "(Soul1)","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //8
                        "GaussBlur(Soul1)(LineCenter8)","","","",    "LineCenter8","","","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //9
                        "GaussBlur","","","",    "","","(Soul1)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //注意每4个字符串为1beat
                    });
                }
            }
            private void NRRest1()
            {
                if (InBeat(712))
                {
                    RegisterFunctionOnce("Scale", () =>
                    {
                        production2?.Dispose();
                        production2 = ScreenDrawing.ActivateShader(Shaders.Scale, 0.5593f);

                        RunEase((s) => Shaders.Scale.Intensity = s,
                            EaseOut(BeatTime(16.25f), -0.19f, 0.0f, EaseState.Sine),
                            Stable(BeatTime(0.1f), 0.0f)
                            );

                        production3?.Dispose();
                        production3 = ScreenDrawing.ActivateShader(Shaders.StepSample, 0.55933f);

                        RunEase((s) => Shaders.StepSample.Intensity = s,
                            EaseOut(BeatTime(16.25f), 0.59f, 0.0f, EaseState.Sine),
                            Stable(BeatTime(0.1f), 0.0f)
                            );
                        DelayBeat(17, production2.Dispose);
                        DelayBeat(17, production3.Dispose);
                    });
                    RegisterFunctionOnce("Throw", () =>
                    {
                        RunEase((s) =>
                        { BoxStates.Centre = s; Heart.InstantTP(s); },
                            Combine(Linear(BeatTime(6 / 1.3f), 320, 800),
                                LinkEase(
                                    EaseOut(BeatTime(2 / 1.3f), 240, 50, EaseState.Quad),
                                    EaseIn(BeatTime(4 / 1.3f), 50, 650, EaseState.Quad)

                                ))
                            );
                    });
                    Settings.GBFollowing = true;

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.25f);
                        RunEase(s => ScreenDrawing.MasterAlpha = s,
                            Linear(BeatTime(6), 1, 0),
                            Linear(BeatTime(6), 0, 1)
                            );
                        DelayBeat(5.75f, () =>
                        {
                            production1?.Dispose();
                            production1 = ScreenDrawing.ActivateShader(Shaders.Spiral, 0.9898125f);
                            Shaders.Spiral.Time = 0;
                            Shaders.Spiral.Intensity = 200.0f;
                            Shaders.Spiral.Speed = 0.15f;
                            r.BasicSpeed = 0.4f;
                            r.Intensity = 12;
                        });
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        RunEase(s => ScreenDrawing.MasterAlpha = s,
                            Linear(BeatTime(8), 1, 0),
                            Linear(BeatTime(8), 0, 1)
                            );
                        DelayBeat(8, () =>
                        {
                            production1?.Dispose();
                            production1 = ScreenDrawing.ActivateShader(Shaders.Spiral, 0.9898125f);
                            Shaders.Spiral.Time = 0;
                            Shaders.Spiral.Intensity = 200.0f;
                            Shaders.Spiral.Speed = 0.15f;
                        });
                    });

                    CreateChart(BeatTime(4), BeatTime(2), 7f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                         
                        //1 
                        "(^#3#$00'2)(^#3#$21'2)(Scale)(Throw)(Flicker)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //2 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //3 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //4 
                        "(Change)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",

                    });
                }
                if (InBeat(744))
                {
                    RegisterFunctionOnce("Eff", () =>
                    {
                        production1?.Dispose();
                        production1 = ScreenDrawing.ActivateShaderBack(Shaders.LightSweep, 0.91238951f);

                        Shaders.LightSweep.Width = 1400f;
                        Shaders.LightSweep.Intensity = 0f;
                        Shaders.LightSweep.Direction = 100f;
                    });

                    RegisterFunctionOnce("Move", () =>
                    {
                        RunEase(s => { BoxStates.Centre = s; Heart.InstantTP(s); Shaders.LightSweep.Centre = s; },
                            EaseOut(BeatTime(16), new Vector2(320, -100), new(320, 240), EaseState.Cubic)
                            );
                        RunEase(s =>
                        {
                            Shaders.LightSweep.Intensity = s;
                        },
                            EaseOut(BeatTime(16), 0.0f, 1.01f, EaseState.Cubic),
                            EaseOut(BeatTime(16), 1.01f, 0.0f, EaseState.Quad)
                            );
                        DelayBeat(16, () =>
                        {
                            RunEase(s =>
                            {
                                Shaders.LightSweep.Centre = new(s, 240);
                            },
                            EaseIn(BeatTime(16), 320, 640, EaseState.Quad)
                            );
                        });
                    });

                    CreateChart(BeatTime(4), BeatTime(2), 8f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                         
                        //1 
                        "Eff(Move)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //2 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //3 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //4 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",

                    });
                }
                if (InBeat(776))
                {
                    easeA?.Dispose(); easeB?.Dispose();
                    easeA = new(); easeB = new();
                    easeA.RotationEase = EaseOut(BeatTime(4), 90, 0, EaseState.Sine);
                    easeB.RotationEase = EaseOut(BeatTime(4), -90, 0, EaseState.Sine);
                    easeA.ApplyTime = BeatTime(4); easeB.ApplyTime = BeatTime(3);
                    easeA.TagApply("A"); easeB.TagApply("B");
                    AddInstance(easeA); AddInstance(easeB);
                    CreateChart(BeatTime(4), BeatTime(4), 5.6f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "", 
                         
                        //1 
                        "#3#d0", "", "", "",    "+20@A", "", "", "",
                        "#3#d1", "", "", "",    "+21@B", "", "", "",
                        //2
                        "#3#d0", "", "", "",    "+20@A", "", "", "",
                        "#3#d1", "", "", "",    "+21@B", "", "", "",
                        //3 
                        "#7#d0", "", "", "",    "+20@A", "", "", "",
                        "+00@B", "", "", "",    "+00@A", "", "", "",
                        //4
                        "#7#d1", "", "", "",    "+21@B", "", "", "",
                        "+01@A", "", "", "+01@B",    "+01@A", "", "", "",
                        //5
                        "#3#d0", "", "", "",    "+20@A", "", "", "",
                        "#3#d1", "", "", "",    "+21@B", "", "", "",
                        //6
                        "#3#d0", "", "", "",    "+20@A", "", "", "",
                        "#3#d1", "", "", "",    "+21@B", "", "", "",
                        //7
                        "#7#d0", "", "", "",    "+20@A", "", "", "",
                        "+00@B", "", "", "",    "+00@A", "", "", "",
                        //8
                        "#7#d1", "", "", "",    "+21@B", "", "", "",
                        "+01@A", "", "", "",    "+01@A", "", "", "",

                    });
                }
                if (InBeat(840))
                {
                    DelayBeat(12, () =>
                    {
                        easeA?.Dispose(); easeB?.Dispose();
                        easeA = new(); easeB = new();
                        easeA.RotationEase = EaseOut(BeatTime(4), 60, 0, EaseState.Sine);
                        easeB.RotationEase = EaseOut(BeatTime(4), -60, 0, EaseState.Sine);
                        easeA.ApplyTime = BeatTime(4); easeB.ApplyTime = BeatTime(3);
                        easeA.TagApply("A"); easeB.TagApply("B");
                        AddInstance(easeA); AddInstance(easeB);

                        easeX?.Dispose();
                        easeX = new();
                        easeX.TagApply("X");
                        AddInstance(easeX);
                    });

                    RegisterFunctionOnce("DoX", () =>
                    {
                        easeX.DeltaEase(EaseOut(BeatTime(1.9f), new Vector2(0, -400), Vector2.Zero, EaseState.Elastic));
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 7f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                         
                        //1 
                        "(#8#$0)(#8#$21)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //2 
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "DoX", "", "", "",    "", "", "", "",
                        //3 
                        "(^$01'1.8@X)(^$2'1.8@X)", "", "", "",    "", "", "$2@A", "$2@B",
                        "$2@A", "", "", "",    "", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        //4 
                        "d1", "", "", "",    "", "", "d1", "",
                        "d1", "", "", "",    "", "", "$0@B", "$0@A",
                        "$0@B", "", "", "",    "!!6", "$3@A", "$3@B", "$31@A", "$31@B", "$1@A", "$1@B",
                        "!!6", "$11@A", "$11@B", "$3@A", "$3@B", "$31@A", "$31@B",    "$1", "", "$2@A", "$2@B", "$2@A"
                    });
                }

                if (InBeat(872))
                {
                    RegisterFunctionOnce("pre", () =>
                    {
                        easeA?.Dispose(); easeB?.Dispose();
                        easeA = new(); easeB = new();
                        easeA.RotationEase = EaseOut(BeatTime(3.8f), 10, 0, EaseState.Sine);
                        easeB.RotationEase = EaseOut(BeatTime(3.8f), -10, 0, EaseState.Sine);
                        easeA.ApplyTime = BeatTime(4); easeB.ApplyTime = BeatTime(4);
                        easeA.TagApply("A"); easeB.TagApply("B");
                        AddInstance(easeA); AddInstance(easeB);
                        easeC?.Dispose(); easeD?.Dispose();
                        easeC = new(); easeD = new();
                        easeC.RotationEase = EaseOut(BeatTime(3.8f), 100, 0, EaseState.Sine);
                        easeD.RotationEase = EaseOut(BeatTime(3.8f), -100, 0, EaseState.Sine);
                        easeC.ApplyTime = BeatTime(4); easeD.ApplyTime = BeatTime(4);
                        easeC.TagApply("C"); easeD.TagApply("D");
                        AddInstance(easeC); AddInstance(easeD);

                        easeX?.Dispose();
                        easeX = new();
                        easeX.TagApply("X");
                        AddInstance(easeX);
                    });

                    RegisterFunctionOnce("DoX", () =>
                    {
                        easeX.DeltaEase(EaseOut(BeatTime(1.6f), new Vector2(0, 400), Vector2.Zero, EaseState.Elastic));
                    });
                    Settings.GreenTap = true;
                    CreateChart(BeatTime(4), BeatTime(2), 8f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "pre",
                         
                        //1 
                        "", "", "", "",    "", "", "d", "",
                        "d", "", "", "",    "", "","$2@A", "$2@B",
                        "$2@A", "", "", "",    "", "", "$01@B", "$01@A",
                        "$01@B",  "", "", "",    "", "", "$2@A", "$2@B",
                        //2 
                        "$2@A($2@B)", "", "", "",    "d1", "", "d1", "",
                        "", "", "d1", "",    "", "", "", "",
                        "d", "", "", "",    "", "", "d", "",
                        "", "", "DoX", "",    "", "", "", "",
                        //3 
                        "(^$0'1.7@X)(^$21'1.7@X)", "", "", "$21@A",    "$0@B", "", "$21@A", "",
                        "$0@B", "", "$31", "",    "$0@B", "", "$31", "",
                        "($0@A)($21@B)", "", "", "$21@B",    "$0@A", "", "", "",
                        "($0@A)($21@B)", "", "", "$21@B",    "$0@A", "", "$21@B", "",
                        //4 
                        "$1($1)", "$1", "$1", "",    "$0@B", "", "$0@B", "",
                        "$0@B($21@A)", "", "$21@A", "",    "$10@C", "$11@D", "$10@C", "$11@D",
                        "$0@A", "", "$0@A", "",    "$0@A", "", "$21@B", "",
                        "($0@A)($21@B)", "", "", "$21@B",    "$0@A", "", "$21@B", "",

                    });
                }

                if (InBeat(904))
                {
                    RegisterFunctionOnce("pre", () =>
                    {
                        easeC?.Dispose(); easeD?.Dispose();
                        easeC = new(); easeD = new();
                        easeC.RotationEase = EaseOut(BeatTime(3.8f), 940, 0, EaseState.Sine);
                        easeD.RotationEase = EaseOut(BeatTime(3.8f), -940, 0, EaseState.Sine);
                        easeC.ApplyTime = BeatTime(3.8f); easeD.ApplyTime = BeatTime(3.8f);
                        easeC.TagApply("C"); easeD.TagApply("D");
                        AddInstance(easeC); AddInstance(easeD);
                        easeX?.Dispose();
                        easeX = new();
                        easeX.TagApply("X1");
                        AddInstance(easeX);
                    });
                    RegisterFunctionOnce("XEase", () =>
                    {
                        DelayBeat(0.125f, () =>
                        {
                            easeX.DeltaEase(EaseOut(BeatTime(1.125f), new Vector2(0, 400), Vector2.Zero, EaseState.Elastic));
                        });
                    });
                    Settings.GreenTap = true;
                    RegisterFunctionOnce("Box", () =>
                    {
                        RunEase(s => { BoxStates.Centre = s; InstantTP(s); },
                            Combine(
                                Alternate(2,
                                    EaseOut(BeatTime(6.91f), 320, 270, EaseState.Cubic),
                                    EaseOut(BeatTime(6.91f), 320, 370, EaseState.Cubic)
                                ),
                                EaseOut(BeatTime(6.91f), 240, 380, EaseState.Quad)
                            )
                        );
                    });
                    RegisterFunctionOnce("Shake", () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(2, 24, 2f, 180, 180, 0.6f));
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.45f), 8.0f, 0.0f, EaseState.Cubic));
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(0.45f), 1.17f, 1.0f, EaseState.Cubic));
                    });
                    RegisterFunctionOnce("ShakeR", () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(2, 24, 2f, 0, 180, 0.6f));
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.45f), -8.0f, 0.0f, EaseState.Cubic));
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(0.45f), 1.17f, 1.0f, EaseState.Cubic));
                    });
                    RegisterFunctionOnce("ShakeLong", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s, false,
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), 3.0f, 0.0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.25f), -3.0f, 0.0f, EaseState.Cubic)
                            );
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(4), 1.0f, 1.6f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -0.1f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -0.1f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -0.1f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -0.1f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -0.1f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -0.1f, EaseState.Cubic)
                            );
                        RunEase(s => ScreenDrawing.ScreenPositionDelta = new(0, -s * 0.5f),
                            EaseOut(BeatTime(4), 60f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(0.5f), -10f, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("Dim", () =>
                    {
                        RunEase(s => ScreenDrawing.MasterAlpha = s, false,
                            Linear(BeatTime(0.5f), 0.3f, 1.0f),
                            Linear(BeatTime(0.5f), 0.3f, 1.0f),
                            Linear(BeatTime(0.5f), 0.3f, 1.0f),
                            Linear(BeatTime(0.5f), 0.3f, 1.0f),
                            Linear(BeatTime(0.5f), 0.3f, 1.0f),
                            EaseOut(BeatTime(1f), 0.1f, 1.0f, EaseState.Cubic),
                            Stable(1.0f, 1f)

                            );
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 9f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "pre", "", "", "(XEase)",    "", "", "", "",
                         
                        //1 
                        "(^$00'1.8@X1)(^$20'1.8@X1)(Shake)", "(XEase)", "", "",    "$30", "",
                        "(^$01'1.8@X1)(^$21'1.8@X1)(ShakeR)", "(XEase)", "", "",    "$31", "",
                        "(^$00'1.8@X1)(^$20'1.8@X1)(Shake)", "(XEase)", "", "",    "$30", "",
                        "(^$01'1.8@X1)(^$21'1.8@X1)(ShakeR)", "(XEase)", "", "",    "$31", "",
                        "(^$00'1.8@X1)(^$20'1.8@X1)(Shake)", "", "", "",    "", "", "", "",
                        //2 
                        "$30@C(Box)(ShakeLong)", "$31@D", "$30@C", "$31@D",    "$30@C", "$31@D", "$30@C", "$31@D",
                        "$30@C", "$31@D", "$30@C", "$31@D",    "$30@C", "$31@D", "$30@C", "$31@D",
                        "$30(Dim)", "", "$30", "",    "$30", "", "$30", "",
                        "$30", "", "$30", "",    "", "", "", "",
                    });
                }

                if (InBeat(927))
                {
                    RegisterFunctionOnce("Save", () =>
                    {
                        SetBox(240, 480, 360);
                        SetSoul(0);
                        HeartAttribute.HP = 5;
                        Color c = ScreenDrawing.HPBar.HPExistColor;
                        ScreenDrawing.HPBar.HPExistColor = Color.Gold;
                        ForBeat(4, () => { ScreenDrawing.HPBar.HPExistColor = Color.Lerp(ScreenDrawing.HPBar.HPExistColor, c, 0.03f); });
                        Vector2 v1 = Vector2.Zero;
                        Vector2 v2 = Vector2.Zero;
                        Vector2 v3 = Vector2.Zero;
                        Vector2 v4 = Vector2.Zero;
                        var cd = ScreenDrawing.HPBar.AreaOccupied;
                        ScreenDrawing.HPBar.Vertical = true;
                        RunEase((s) => { v1 = s; }, Stable(0, cd.TopLeft), EaseOut(BeatTime(2), cd.TopLeft, new Vector2(15, 240 - 110), EaseState.Cubic));
                        RunEase((s) => { v2 = s; }, Stable(0, cd.TopRight), EaseOut(BeatTime(2), cd.TopRight, new Vector2(15 + 25, 240 - 110), EaseState.Cubic));
                        RunEase((s) => { v3 = s; }, Stable(0, cd.BottomRight), EaseOut(BeatTime(2), cd.BottomRight, new Vector2(15 + 25, 240 + 110), EaseState.Cubic));
                        RunEase((s) => { v4 = s; ScreenDrawing.HPBar.AreaOccupied = new CollideRect(v1.X, v1.Y, v2.X - v1.X, v3.Y - v2.Y); }, Stable(0, cd.BottomLeft), EaseOut(BeatTime(2), cd.BottomLeft, new Vector2(15, 240 + 110), EaseState.Cubic));
                        RunEase((s) => { ScreenDrawing.ScreenScale = s; }, EaseOut(BeatTime(2), -0.15f, EaseState.Quad));
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        //CustomBone b=new()
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {

                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {

                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {

                    });
                    /*    BarrageCreate(0, BeatTime(2), 9, new string[]
                        {
                            "Save"
                        });*/
                }
            }
            private void NRFinal()
            {
                if (InBeat(926f)) { ScreenDrawing.WhiteOut(BeatTime(1.5f)); }
                if (InBeat(927.5f))
                {
                    RunEase((s) => { ScreenDrawing.ScreenScale = s; }, LinkEase(Stable(0, 2f), EaseOut(BeatTime(4f), -1f, EaseState.Cubic)));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, LinkEase(Stable(0, 0f), EaseOut(BeatTime(4f), 360f, EaseState.Cubic)));
                    DelayBeat(65, () =>
                    {
                        float rand = 7;
                        for (int i = 0; i < 1500; i++)
                        {
                            DelayBeat(i * 0.01f, () =>
                            {
                                rand -= 7f / 1500;
                                float x = Rand(-rand, rand);
                                float y = Rand(-rand, rand);
                                ScreenDrawing.ScreenPositionDelta = new Vector2(x, y);

                                Shaders.StepSample.CentreX = x + 320;
                                Shaders.StepSample.CentreY = y + 240;
                            });
                        }
                    });
                    const int COUNT = 4;
                    ScreenDrawing.BoxBackColor = Color.Black * 0.5f;
                    RegisterFunctionOnce("Shader", () =>
                    {
                        r.Intensity = 3;
                        r.BasicSpeed = 1.6f; r.Width = 2.5f;
                        ScreenDrawing.DownBoundDistance = 0;
                        ScreenDrawing.UpBoundDistance = 0;
                        ScreenDrawing.LeftBoundDistance = 0;
                        ScreenDrawing.RightBoundDistance = 0;
                        production5 = ScreenDrawing.ActivateShaderBack(shaderGrid, 0.9919f);
                        shaderGrid.BlendColor = Color.Transparent;

                        RunEase((s) => shaderGrid.BlendColor = new Color(0.3f, 0.5f, 0.6f) * s,
                            EaseOut(BeatTime(2), 0.0f, 0.3257f, EaseState.Sine
                            ));
                        RunEase((s) => shaderGrid.SideColor = new Color(0.3f, 0.5f, 0.6f) * s,
                            EaseOut(BeatTime(2), 0.0f, 1f, EaseState.Sine
                            ));
                        RunEase((s) => shaderGrid.Intensity1 = s,
                            EaseOut(BeatTime(2), 0.0f, 0.05f, EaseState.Sine
                            ));
                        RunEase((s) => shaderGrid.Intensity2 = s,
                            EaseOut(BeatTime(2), 0.124f, 1.0f, EaseState.Sine
                            ));
                        shaderGrid.Intensity3 = 1.0f;
                        shaderGrid.GlowIntensity = 0.0f;
                    });
                    RegisterFunctionOnce("Box", () =>
                    {
                        Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                        ForBeat(4, () =>
                        {
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(ScreenDrawing.UISettings.HPShowerPos, cur + new Vector2(110, 0), 0.23f);
                        });
                        HeartAttribute.Speed = 3.0f;

                        InstantSetBox(380, 184, 252);
                        SetBox(270 - 42, 370 + 42, -60, 650);
                        SetSoul(Souls.RedSoul);
                        Heart.InstantSetRotation(180);
                        InstantTP(new(320, 380));
                        DelayBeat(0.5f, () =>
                        {
                            InstantSetBox(270 - 42, 370 + 42, -5, 485);
                        });
                        RunEase((s) => { InstantTP(new(Heart.Centre.X, s)); },
                            LinkEase(EaseIn(BeatTime(1), 380, 360, EaseState.Sine),
                            EaseIn(BeatTime(16), 360, 300, EaseState.Linear)));
                    });
                    RegisterFunctionOnce("BoneGA", () =>
                    {
                        for (int i = 0; i < 148; i++)
                        {
                            float h = i;
                            LeftBone b1 = new(false, 640 + i * -16, 0, 35 + i * 0.105f) { MarkScore = false };
                            RightBone b2 = new(false, 640 + i * -16, 0, 35 + i * 0.105f) { MarkScore = false };
                            CreateBone(b1);
                            CreateBone(b2);
                            RunEase(k => b1.Speed = b2.Speed = k, EaseOut(BeatTime(1), 0, 7.8f, EaseState.Linear), Stable(1, 7.8f));
                        }
                    });
                    RegisterFunctionOnce("CrossL", () =>
                    {
                        float angle = 0.0f;
                        for (int i = 0; i < COUNT; i++)
                        {
                            angle += 180.0f / COUNT;
                            CreateBone(new CustomBone(
                                Add(LinkEase(EaseOut(BeatTime(2f), new(-110, 0), Vector2.Zero, EaseState.Back),
                                    Stable(BeatTime(1), Vector2.Zero)
                                ),
                                InfLinear(new Vector2(270 - 52, -10), new(0, 8))),
                                InfLinear(angle, 4), 200));
                        }

                        //  Extends.DrawingUtil.CrossBone(new Vector2(270 - 42, -10), new Vector2(0, 8), 200, 4, 0, -8);
                    });
                    RegisterFunctionOnce("CrossR", () =>
                    {
                        float angle = 0.0f;
                        for (int i = 0; i < COUNT; i++)
                        {
                            angle += 180.0f / COUNT;
                            CreateBone(new CustomBone(
                                Add(LinkEase(EaseOut(BeatTime(2f), new(110, 0), Vector2.Zero, EaseState.Back),
                                    Stable(BeatTime(1), Vector2.Zero)
                                ),
                                InfLinear(new Vector2(370 + 52, -10), new(0, 8))),
                                InfLinear(angle, -4), 200));
                        }
                    });
                    RegisterFunctionOnce("BoneWallX", () =>
                    {
                        for (int i = 0; i < 5; i += 2)
                        {
                            LeftBone b = new(false, i * (-16), 18.0f, 188);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1637f;
                            CreateBone(b);
                        }
                        for (int i = 1; i < 5; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 18.0f, 188);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1637f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall1", () =>
                    {
                        for (int i = 0; i < 5; i += 2)
                        {
                            LeftBone b = new(false, i * (-16), 8.0f, 188);
                            b.ColorType = 1;
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                        for (int i = 1; i < 5; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 8.0f, 188);
                            b.ColorType = 1;
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall2", () =>
                    {
                        for (int i = 0; i < 5; i += 2)
                        {
                            RightBone b = new(false, i * (-16), 8.0f, 194);
                            b.ColorType = 2;
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                        for (int i = 1; i < 5; i += 2)
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
                    RegisterFunctionOnce("BoneWall0Al", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LeftBone b = new(false, i * -16, 8, 62 + i * 8);
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall0Ar", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            RightBone b = new(false, i * -16, 8, 62 + i * 8);
                            b.LengthLerpScale = 0.1137f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall0Bl", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            var ease = Add(LinkEase(EaseOut(BeatTime(0.9f), new(-110, 0), Vector2.Zero, EaseState.Back),
                                    Stable(BeatTime(1), Vector2.Zero)
                                ),
                                InfLinear(Vector2.Zero, new(0, 8)));
                            float rot = 70;
                            CustomBone b = new(new Vector2(270 - 47, i * -16), ease.Easing, -rot, 165);
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneWall0Br", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            var ease = Add(LinkEase(EaseOut(BeatTime(0.9f), new(110, 0), Vector2.Zero, EaseState.Back),
                                    Stable(BeatTime(1), Vector2.Zero)
                                ),
                                InfLinear(Vector2.Zero, new(0, 8)));
                            float rot = 70;
                            CustomBone b = new(new Vector2(370 + 47, i * -16), ease.Easing, rot, 165);
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("text", () =>
                    {

                    });
                    RegisterFunctionOnce("s", () =>
                    {
                        PlaySound(Sounds.pierce);
                    });
                    List<SideBone> rbones = new(), lbones = new();
                    RegisterFunctionOnce("MoreBone", () =>
                    {
                        for (int i = 0; i < 162; i++)
                        {
                            RightBone bone = new(false, -15 - i * 16, 7.8f, 50f);
                            if (i % 16 < 2 && i > 15 && i < 60)
                                rbones.Add(bone);
                            bone.Length = 50f;
                            bone.LengthLerpScale = 0.2f;
                            AddInstance(bone);
                            if (i < 30)
                            {
                                DelayBeat(2.25f, () =>
                                {
                                    bone.MissionLength = 15f;
                                });
                            }
                            else
                            {
                                bone.Length = 15f;
                                bone.MissionLength = 15f;
                            }
                            if (i % 3 < 2) bone.MarkScore = false;
                        }
                        for (int i = 0; i < 162; i++)
                        {
                            LeftBone bone = new(false, -15 - i * 16, 7.8f, 50f);
                            if (i % 16 >= 8 && i % 16 < 10 && i > 15 && i < 60)
                                lbones.Add(bone);
                            bone.LengthLerpScale = 0.2f;
                            bone.Length = 50f;
                            AddInstance(bone);
                            if (i < 30)
                            {
                                DelayBeat(2.25f, () =>
                                {
                                    bone.MissionLength = 15f;
                                });
                            }
                            else
                            {
                                bone.Length = 15f;
                                bone.MissionLength = 15f;
                            }
                            if (i % 3 < 2) bone.MarkScore = false;
                        }
                        DelayBeat(2.75f, () =>
                        {
                            lbones.ForEach(s => { s.MissionLength = 60f; s.MarkScore = false; });
                            rbones.ForEach(s => { s.MissionLength = 60f; s.MarkScore = false; });
                        });
                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        ForBeat(4, () =>
                        {
                            Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(cur, new(61, 407), 0.23f);
                        });
                        RunEase(s => BoxStates.Centre = new(s, 240), EaseOut(BeatTime(1), 320, 360, EaseState.Cubic));
                    });
                    RegisterFunctionOnce("BoneTunnel", () =>
                    {

                    });
                    float y = 0;
                    RegisterFunctionOnce("GB", () =>
                    {
                        y = Heart.Centre.Y - 5;
                        CreateGB(new NormalGB(new(250, y + 3), Heart.Centre, new(1, 0.5f), 0, BeatTime(1), 4));
                        CreateGB(new NormalGB(new(550, y + 3), Heart.Centre, new(1, 0.5f), 180, BeatTime(1.5f), 4));
                        y -= 4;
                    });
                    RegisterFunctionOnce("BoneHorizon1", () =>
                    {
                        CreateBone(new CustomBone(new Vector2(350 - 42, y), InfLinear(new Vector2(21, 0)).Easing, InfLinear(0, 5).Easing, 25) { ColorType = 2 });
                        CreateBone(new CustomBone(new Vector2(450 + 42, y), InfLinear(new Vector2(-21, 0)).Easing, InfLinear(0, -5).Easing, 25) { ColorType = 2 });
                        CreateBone(new CustomBone(new Vector2(350 - 42, y), InfLinear(new Vector2(21, 0)).Easing, InfLinear(90, 5).Easing, 25) { ColorType = 2 });
                        CreateBone(new CustomBone(new Vector2(450 + 42, y), InfLinear(new Vector2(-21, 0)).Easing, InfLinear(90, -5).Easing, 25) { ColorType = 2 });
                    });
                    RegisterFunctionOnce("BoneHorizon2", () =>
                    {
                        CreateBone(new CustomBone(new Vector2(350 - 42, y), InfLinear(new Vector2(21, 0)).Easing, InfLinear(0, 5).Easing, 25) { ColorType = 1 });
                        CreateBone(new CustomBone(new Vector2(450 + 42, y), InfLinear(new Vector2(-21, 0)).Easing, InfLinear(0, -5).Easing, 25) { ColorType = 1 });
                        CreateBone(new CustomBone(new Vector2(350 - 42, y), InfLinear(new Vector2(21, 0)).Easing, InfLinear(90, 5).Easing, 25) { ColorType = 1 });
                        CreateBone(new CustomBone(new Vector2(450 + 42, y), InfLinear(new Vector2(-21, 0)).Easing, InfLinear(90, -5).Easing, 25) { ColorType = 1 });
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        lbones.ForEach(s => s.Length = 65f);
                        rbones.ForEach(s => s.Length = 65f);
                    });
                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        lbones.ForEach(s => s.Length = 45f);
                        lbones.ForEach(s => s.ColorType = 1);
                        DelayBeat(0.25f, () =>
                        {
                            lbones.ForEach(s => s.ColorType = 0);
                        });
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        rbones.ForEach(s => s.Length = 45f);
                        rbones.ForEach(s => s.ColorType = 1);
                        DelayBeat(0.25f, () =>
                        {
                            rbones.ForEach(s => s.ColorType = 0);
                        });
                    });
                    RegisterFunctionOnce("MoveL", () =>
                    {
                        float cur = BoxStates.Centre.X;
                        RunEase(s => BoxStates.Centre = new(s, 240), EaseOut(BeatTime(1), cur, cur - 40, EaseState.Cubic));
                    });
                    RegisterFunctionOnce("MoveR", () =>
                    {
                        float cur = BoxStates.Centre.X;
                        RunEase(s => BoxStates.Centre = new(s, 240), EaseOut(BeatTime(1), cur, cur + 40, EaseState.Cubic));
                    });
                    RegisterFunctionOnce("DeMove", () =>
                    {
                        ForBeat(4, () =>
                        {
                            Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(cur, new(430, 443), 0.23f);
                        });
                    });
                    RegisterFunctionOnce("BoneMid", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LeftBone b = new(false, i * -16, 15, 34 + i * 7);
                            b.LengthLerpScale = 0.1537f;

                            CreateBone(b);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            RightBone b = new(false, i * -16, 15, 34 + i * 7);
                            b.LengthLerpScale = 0.1537f;

                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneMid2", () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LeftBone b = new(false, i * -16, 15, 50 + i * 7);
                            b.LengthLerpScale = 0.1537f;
                            b.ColorType = 1;
                            CreateBone(b);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            RightBone b = new(false, i * -16, 15, 50 + i * 7);
                            b.LengthLerpScale = 0.1537f;
                            b.ColorType = 1;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneLeft", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            LeftBone b = new(false, i * -16, 15, 60);
                            b.LengthLerpScale = 0.1537f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("BoneRight", () =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            RightBone b = new(false, i * -16, 15, 60);
                            b.LengthLerpScale = 0.1537f;
                            CreateBone(b);
                        }
                    });
                    RegisterFunctionOnce("Heal", () =>
                    {
                        Regenerate();
                    });
                    RegisterFunctionOnce("IndicateR", () =>
                    {
                        PlaySound(Sounds.Ding);
                        TextPrinter text = new(-1, "$$$>>>", new Vector2(200, 200), new TextAttribute[]
                        {
                            new TextFadeoutAttribute(0, BeatTime(4)),
                            new TextColorAttribute(Color.Red),
                            new TextSizeAttribute(6f),
                        })
                        {
                            Depth = 0.99f
                        };
                        CreateEntity(text);
                    });
                    RegisterFunctionOnce("IndicateL", () =>
                    {
                        PlaySound(Sounds.Ding);
                        TextPrinter text = new(-1, "$$$<<<", new Vector2(200, 200), new TextAttribute[]
                        {
                            new TextFadeoutAttribute(0, BeatTime(2)),
                            new TextColorAttribute(Color.Red),
                            new TextSizeAttribute(6f),
                        })
                        {
                            Depth = 0.99f
                        };
                        CreateEntity(text);
                    });
                    RegisterFunctionOnce("IndicateM", () =>
                    {
                        PlaySound(Sounds.Ding);
                        TextPrinter text = new(-1, "$$$>  <", new Vector2(150, 150), new TextAttribute[]
                        {
                            new TextFadeoutAttribute(0, BeatTime(4)),
                            new TextColorAttribute(Color.Red),
                            new TextSizeAttribute(6f),
                        })
                        {
                            Depth = 0.99f
                        };
                        CreateEntity(text);
                    });

                    CreateChart(0, BeatTime(2), 15.4f, new string[]
                    {
                        //pre 
                        "(Box)(BoneGA)(Heal)(Shader)", "", 
                        //1 
                        "CrossL(s)", "", "", "",    "BoneWall2(s)", "", "", "",
                        "CrossR(s)", "", "", "",    "BoneWall0Ar(s)", "", "BoneWall0Al(s)", "",
                        "BoneWall0Ar(s)", "", "", "",    "BoneWall1(s)", "", "", "",
                        "BoneWall0Al(s)", "", "", "",    "BoneWall0Br(s)", "", "BoneWall0Bl(s)", "",
                        //2 
                        "BoneWall2(s)", "", "", "",    "CrossR(s)", "", "", "",
                        "BoneWall1(s)", "", "", "",    "BoneWall0Bl(s)", "", "BoneWall0Br(s)", "",
                        "BoneWall0Bl(s)", "", "", "(MoreBone)",    "BoneWall1(s)", "", "", "",
                        "CrossL(s)", "", "", "",    "(s)(IndicateR)", "", "", "",
                        //3  
                        "Move(s)", "", "", "",    "BoneWall3(s)(Shrink)", "", "", "",
                        "(GB)(BoneHorizon1)(s)", "", "", "",    "BoneHorizon1(s)(ShrinkL)", "", "BoneHorizon2(s)(ShrinkR)", "",
                        "", "", "", "",    "BoneWall3(s)(Shrink)", "", "", "",
                        "(GB)(BoneHorizon1)(s)", "", "", "",    "BoneHorizon1(s)(ShrinkL)(IndicateL)", "", "BoneHorizon2(s)(ShrinkR)", "",
                        //4 
                        "(s)(MoveL)(DeMove)(BoneMid)", "", "", "",    "(s)(IndicateL)BoneWallX", "", "", "",
                        "(s)(MoveL)(BoneMid2)(IndicateM)", "", "", "",    "(s)(MoveR)(BoneWallX)", "", "", "",
                        "(s)(BoneMid)", "", "(s)(BoneMid)", "",    "(s)(BoneMid)", "", "", "",
                        "(s)BoneWallX", "", "(s)(BoneLeft)", "(s)(BoneLeft)",    "(s)BoneWallX", "", "(s)BoneRight", "",
                    });//zKronO's version 
                }
                if (InBeat(959))
                {
                    Settings.VoidArrowVolume = 0f;
                    Settings.GreenTap = true;

                    RegisterFunctionOnce("Box", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.25f;
                        Heart.InstantSetRotation(0);
                        SetBox(270 - 42, 370 + 42, 190 - 42, 190 + 42);
                        RunEase(s => shaderGrid.Intensity1 = s,
                            EaseOut(BeatTime(16), 0.05f, 0.12f, EaseState.Sine)
                            );
                        RunEase(s => shaderGrid.GlowIntensity = s,
                            EaseOut(BeatTime(16), 0.20f, 0.368f, EaseState.Sine)
                            );
                    });
                    RegisterFunctionOnce("BoxG", () =>
                    {
                        shaderGrid.GlowDistance = -1.4f;
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    });

                    ArrowApply("ANC", (s) => s.MissionCentre = new(320, 240));

                    RegisterFunctionOnce("Eff", () =>
                    {
                        RunEase(s => shaderGrid.GlowDistance = s,
                            EaseOut(BeatTime(4), -0.3f, 1.0f, EaseState.Sine));
                    });
                    int u;
                    ArrowAllocate(1, u = Rand(0, 1) * 2 + 1);
                    ArrowAllocate(2, u + 1);
                    ArrowAllocate(3, u + 2);
                    ArrowAllocate(4, u + 3);
                    CreateChart(0, BeatTime(2), 7f, new string[]
                    {
                        // pre 
                        "", "", "", "",      
                        // 1
                        "Box", "", "", "",      "BoxG", "", "(d@ANC)(Eff)", "",
                        "", "", "(d@ANC)(+0@ANC)", "",      "(d@ANC)(+01@ANC)", "", "(+01@ANC)", "",
                        "(d1)(+01)", "", "",   "!!12/3", "", "", "d1@LN2",
                        "A4@LN2", "", "(Eff)", "",
                        "A11@LN2", "", "", "",      "($2)($2'1.5)", "", "($4)($4'1.5)", "",   
                        
                        // 2
                        "A21@LN2(A21@LN2+)", "", "(A1@LN2+)", "",     "(A21@LN2+)", "", "(Eff)", "",
                        "A31@LN2", "", "A41@LN1+", "",     "A1@LN1", "", "A21",    "!!12/3", "", "", "",
                        "A41@LN1(A2@LN2)", "", "", "",     "(A41@LN1)(A41@LN1)", "", "(Eff)",    "!!12/3", "", "", "D1@LN1",
                        "A4@LN1", "", "", "",     "(A2)(A2'1.5)", "", "(A4)(A4'1.5)", "",
                    });
                }
                if (InBeat(956 + 16))
                {
                    RegisterFunctionOnce("EffL", () =>
                    {
                        shaderGrid.GlowIntensity = 0.38f;
                        RunEase(s => shaderGrid.GlowDistance = s,
                            EaseOut(BeatTime(1.8f), -0.2f, 0.9f, EaseState.Sine));
                        shaderGrid.Intensity2 = 1.15f;
                        RunEase(s => shaderGrid.Intensity2 = s,
                            EaseOut(BeatTime(2.8f), 1.15f, 1.0f, EaseState.Sine));
                    });
                    RegisterFunctionOnce("EffL2", () =>
                    {
                        shaderGrid.GlowIntensity = 0.298f;
                        RunEase(s => shaderGrid.GlowDistance = s,
                            EaseOut(BeatTime(1f), 1, -0.2f, EaseState.Sine));
                    });
                    RegisterFunctionOnce("EffL3", () =>
                    {
                        shaderGrid.GlowIntensity = 0.498f;
                        RunEase(s => shaderGrid.GlowDistance = s,
                            EaseOut(BeatTime(2.5f), -0.2f, 0.9f, EaseState.Linear));
                    });
                    CreateHeart(new(320 - 1142, 240 - 42, 84, 84));
                    InstantTP(320, 240);
                    Heart.Alpha = 0.0f;
                    RegisterFunctionOnce("Split", () =>
                    {
                        Regenerate(2); PlaySound(Sounds.heal);
                        DelayBeat(0.1f, () =>
                        {
                            SetPlayerBoxMission(0);
                            BoxStates.BoxMovingScale = 0.2f;
                            SetPlayerBoxMission(1);
                            Heart.Alpha = 1.0f;
                            BoxStates.BoxMovingScale = 0.2f;
                            BoxStates.Centre = new(320, 240);
                            RunEase(s => { SetPlayerBoxMission(0); BoxStates.Centre = s; InstantTP(s); }, EaseOut(
                                    BeatTime(0.35f), new Vector2(320, 240), new(320 - 66, 240 - 44), EaseState.Cubic)
                                );
                            RunEase(s => { SetPlayerBoxMission(1); BoxStates.Centre = s; InstantTP(s); }, EaseOut(
                                   BeatTime(0.35f), new Vector2(320, 240), new(320 + 66, 240 + 44), EaseState.Cubic)
                               );
                        });
                    });
                    RegisterFunctionOnce("Move1", () =>
                    {
                        RunEase(s => { SetPlayerBoxMission(0); BoxStates.Centre = s; InstantTP(s); }, EaseOut(
                                    BeatTime(0.95f), new Vector2(320 - 66, 240 - 44), new(320 + 66, 240 - 44), EaseState.Back)
                                );
                        RunEase(s => { SetPlayerBoxMission(1); BoxStates.Centre = s; InstantTP(s); }, EaseOut(
                               BeatTime(0.95f), new Vector2(320 + 66, 240 + 44), new(320 - 66, 240 + 44), EaseState.Back)
                           );
                        ArrowApply("A", (s) =>
                        {
                            s.Delay(BeatTime(1.26f));
                        });
                    });

                    int count = 4;
                    this.ArrowProcesser = (s) =>
                    {
                        if (s.Way % 2 == 0)
                        {
                            if (s.ArrowColor == 1) s.Mission = Player.hearts[1];
                            else s.Mission = Player.hearts[0];
                            if (count > 0)
                            {
                                count--;
                                s.Offset = new(0, -(s.ArrowColor * 2 - 1) * 15);
                            }
                        }
                        else
                        {
                            if (s.Way % 4 == 3) s.Mission = Player.hearts[1];
                            else s.Mission = Player.hearts[0];
                        }
                    };

                    RegisterFunctionOnce("Converge", () =>
                    {
                        RunEase(s => { SetPlayerBoxMission(0); BoxStates.Centre = s; InstantTP(s); }, EaseOut(
                                    BeatTime(0.85f), new Vector2(320 + 66, 240 - 44), new(320, 240), EaseState.Cubic)
                                );
                        RunEase(s => { SetPlayerBoxMission(1); BoxStates.Centre = s; InstantTP(s); }, EaseOut(
                               BeatTime(0.85f), new Vector2(320 - 66, 240 + 44), new(320, 240), EaseState.Cubic)
                           );
                        RunEase(s => { SetPlayerBoxMission(1); BoxStates.CurrentBox.GreenSoulAlpha = s * 0.5f; Heart.Alpha = s; },
                            Stable(0.6f, 1.0f),
                            EaseOut(BeatTime(0.25f), 1.0f, 0.0f, EaseState.Cubic)
                        );
                    });


                    RegisterFunctionOnce("Reset", () =>
                    {
                        ArrowProcesser = null;
                    });
                    production2?.Dispose();
                    production1?.Dispose();
                    production1 = ScreenDrawing.ActivateShader(Shaders.StepSample, 0.9125f);
                    production2 = ScreenDrawing.ActivateShader(Shaders.Scale, 0.99325f);
                    Shaders.StepSample.Intensity = 0; Shaders.Scale.Intensity = 0;

                    RegisterFunctionOnce("Eff1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.42f);
                        RunEase(s => Shaders.StepSample.Intensity = s,
                            EaseOut(BeatTime(0.225f), 0.4f, EaseState.Cubic),
                            EaseOut(BeatTime(3.125f), 0.4f, 0f, EaseState.Quart)
                            );
                        RunEase(s => Shaders.Scale.Intensity = s,
                            EaseOut(BeatTime(0.225f), -0.14f, EaseState.Cubic),
                            EaseOut(BeatTime(3.125f), -0.14f, 0f, EaseState.Quart)
                            );
                        RunEase(s => splitter.Intensity = s,
                            EaseOut(BeatTime(0.225f), 6.14f, EaseState.Cubic),
                            EaseOut(BeatTime(3.125f), 6.14f, 0f, EaseState.Cubic)
                            );

                    });
                    RegisterFunctionOnce("Eff0", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.36f);

                        RunEase(s => Shaders.StepSample.Intensity = s,
                            EaseOut(BeatTime(0.125f), 0.37f, EaseState.Cubic),
                            EaseOut(BeatTime(0.525f), 0.37f, 0f, EaseState.Cubic)
                            );
                        RunEase(s => Shaders.Scale.Intensity = s,
                            EaseOut(BeatTime(0.125f), -0.09f, EaseState.Cubic),
                            EaseOut(BeatTime(0.525f), -0.09f, 0f, EaseState.Cubic)
                            );
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(0.125f), 1f, 1.1f, EaseState.Cubic),
                            EaseOut(BeatTime(0.525f), 1.1f, 1.0f, EaseState.Quart)
                            );
                        RunEase(s => splitter.Intensity = s,
                            EaseOut(BeatTime(0.125f), 4.14f, EaseState.Cubic),
                            EaseOut(BeatTime(0.525f), 4.14f, 0f, EaseState.Cubic)
                            );

                    });
                    RegisterFunctionOnce("Eff2", () =>
                    {
                        RunEase(s => Shaders.StepSample.Intensity = s,
                            EaseOut(BeatTime(0.225f), 0.4f, EaseState.Cubic),
                            EaseOut(BeatTime(3.775f), 0.4f, 0.1f, EaseState.Quad),
                            EaseOut(BeatTime(0.225f), 0.4f, EaseState.Cubic),
                            EaseOut(BeatTime(3.775f), 0.4f, 0.1f, EaseState.Quad),
                            EaseOut(BeatTime(0.725f), 0.3f, EaseState.Quad),
                            EaseOut(BeatTime(8.775f), 0.4f, 0.1f, EaseState.Cubic),
                            EaseOut(BeatTime(4), 0.2f, 0.0f, EaseState.Quint)
                            );
                        RunEase(s => Shaders.Scale.Intensity = s,
                            EaseOut(BeatTime(0.225f), -0.11f, EaseState.Cubic),
                            EaseOut(BeatTime(3.775f), -0.11f, -0.04f, EaseState.Quad),
                            EaseOut(BeatTime(0.225f), -0.04f, -0.15f, EaseState.Cubic),
                            EaseOut(BeatTime(3.775f), -0.15f, -0.08f, EaseState.Quad),
                            EaseOut(BeatTime(0.725f), -0.17f, EaseState.Cubic),
                            EaseOut(BeatTime(8.775f), -0.14f, -0.04f, EaseState.Cubic),
                            EaseOut(BeatTime(4), -0.15f, -0.0f, EaseState.Quint)
                            );
                    });
                    RegisterFunctionOnce("ShakeL", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenPositionDelta = s,
                            EaseOut(BeatTime(0.57f), new(-18, 0), new Vector2(0), EaseState.Cubic)
                            );
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.57f), 3, 0f, EaseState.Quart)
                            );
                    });
                    RegisterFunctionOnce("ShakeR", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenPositionDelta = s,
                            EaseOut(BeatTime(0.57f), new(18, 0), new Vector2(0), EaseState.Cubic)
                            );
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.57f), -3, 0f, EaseState.Quart)
                            );
                    });
                    RegisterFunctionOnce("Scale2", () =>
                    {
                        RunEase((s) => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(4), 1.0f, 1.1f, EaseState.Elastic),
                            EaseOut(BeatTime(4), 1.0f, 1.08f, EaseState.Elastic),
                            EaseOut(BeatTime(8), 1.0f, 1.12f, EaseState.Cubic)
                            );
                        RunEase((s) => shaderGrid.Intensity2 = s,
                            EaseOut(BeatTime(4), 1.0f, 1.1f, EaseState.Elastic),
                            EaseOut(BeatTime(4), 1.1f, 1.2f, EaseState.Elastic),
                            EaseOut(BeatTime(8), 1.2f, 1.86f, EaseState.Quad)
                            );
                        DelayBeat(16, () => RunEase(s => shaderGrid.Intensity3 = s,
                            EaseOut(BeatTime(6), 1.0f, 0.0f, EaseState.Cubic)
                            ));
                        RunEase((s) => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(4), 0, 4, EaseState.Elastic),
                            EaseOut(BeatTime(4), 4, -4, EaseState.Elastic),
                            EaseOut(BeatTime(8), -4, 1, EaseState.Cubic)
                            );
                    });
                    string s = "($20'1.3)($01'1.3)";
                    string ss = "($00'1.3)($21'1.3)";
                    string s1 = "($10)($31)";
                    string s11 = "($11@A)($30@A)";
                    string s2 = "($11)($30)";
                    CreateChart(BeatTime(2), BeatTime(2), 7.4f, new string[]
                    {
                        // pre 
                        "", "", "", "",    "", "", "Split", "(EffL)",      
                        // 1 
                        s1 + "Eff1(ShakeL)", "", s1, s1,    s1, "", s, "",
                        s1, s1, s1, "(EffL2)",    s + "(Move1)" + "(Eff0)(ShakeL)", s11, s11, s11 + "(EffL)",
                        s2+ "Eff1(ShakeR)", "", "", "",    "", "", ss, "",
                        s2, s2, s2, "(EffL2)",    ss + "(Converge)" + "(Eff0)(ShakeR)", "", "(EffL3)", "",
                        
                        // 2 
                        "(<!>Reset)(#3#$0(#3#$21))(Eff2)(Scale2)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "(EffL3)", "",
                        "(#3#$1(#3#$31))", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "", "(#6#$2(#6#$01))",
                    });
                }

                if (InBeat(956 + 32))
                {

                    RegisterFunctionOnce("Alpha", () =>
                    {
                        RunEase(s => ScreenDrawing.MasterAlpha = s,
                            EaseInOut(BeatTime(12), 1.0f, 0.0f, EaseState.Quad),
                            Linear(BeatTime(12), 0.0f, 1.0f)
                            );
                        DelayBeat(4, () =>
                        {
                            shaderGrid.Intensity1 = 0.0f;
                        });
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        RunEase((s) =>
                        {
                            shaderGrid.SideColor = new Color(0.8f, 0.4f, 1.0f) * s;
                            shaderGrid.BlendColor = new Color(0.8f, 0.4f, 1.0f) * s;
                            ScreenDrawing.BoundColor = Color.White * s;
                        },
                        EaseOut(BeatTime(6), 1, 0, EaseState.Sine
                        ));
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1.0f;
                        r.BasicSpeed = 0.35f;
                        r.Intensity = 15f;

                        RunEase(s => sans.Alpha = s, Linear(BeatTime(8), 0, 0.6f));
                        RunEase(s => sans.Offset = new(s, 0), EaseOut(BeatTime(8), 200, 0, EaseState.Sine));
                    });
                    RegisterFunctionOnce("Dispose", () =>
                    {
                        production5?.Dispose();
                        production1?.Dispose();
                        production2?.Dispose();
                    });
                    CreateChart(BeatTime(2), BeatTime(2), 7.4f, new string[] { 
                        // pre 
                        "", "", "", "",    "", "", "", "",      
                        // 1 
                        "Alpha", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        // 2
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "Change", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "Dispose",
                    });
                }
            }
        }
    }
}