using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static Extends.ShadowLibrary;
using System;
using UndyneFight_Ex.UserService;
using System.Linq;

namespace Rhythm_Recall.Waves
{
    public class DreadNaught : IChampionShip
    {
        public DreadNaught()
        {

            difficulties = new()
            {
                { "div.3", Difficulty.Easy },
                { "div.2", Difficulty.Normal },
                { "div.1", Difficulty.Extreme }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (120f / 60f)) { }
            private static int AnomalyExist() {
                if (PlayerManager.CurrentUser == null) return 0;
                var customData = PlayerManager.CurrentUser.Custom;
                if (!customData.Nexts.ContainsKey("TaSAnomaly"))
                    customData.PushNext(new("TaSAnomaly:value=0"));
                int t = customData.Nexts["TaSAnomaly"].IntValue;
                if (t == 2) return 0; 
                DateTime time = DateTime.UtcNow;
                bool test = false;
#if DEBUG
                test = true;
#endif
                if (
                    (time.Month == 10 && time.Year == 2023 && time.Day == 1) ||
                    (time.Day == 3)
                    || test)
                {
                    var songs = PlayerManager.CurrentUser.SongManager;
                    Dictionary<string, SongData> dic = new();
                    foreach(var v in songs.AllDatas)
                    {
                        dic.Add(v.SongName, v);
                    }
                    int res = 0;
                    if (dic.ContainsKey("BIG SHOT") && dic.ContainsKey("Spider Dance"))
                    {
                        SongData bs = dic["BIG SHOT"], sd = dic["Spider Dance"];

                        if (t >= 1) // div.2 finished, check div.1, not check div.2
                        {
                            if (
                            bs.CurrentSongStates.ContainsKey(Difficulty.Easy) &&
                            sd.CurrentSongStates.ContainsKey(Difficulty.Easy)
                            )
                            {
                                // div.2 accessibility test
                                if (bs.CurrentSongStates[Difficulty.Easy].Accuracy > 0.9f &&
                                    sd.CurrentSongStates[Difficulty.Easy].Accuracy > 0.9f
                                    )
                                    res = 1;
                            }
                        }
                        if (
                                bs.CurrentSongStates.ContainsKey(Difficulty.Extreme) &&
                                sd.CurrentSongStates.ContainsKey(Difficulty.Extreme)
                                )
                        {
                            // div.1 accessibility test
                            if (bs.CurrentSongStates[Difficulty.Extreme].Accuracy > 0.9f &&
                                sd.CurrentSongStates[Difficulty.Extreme].Accuracy > 0.9f
                                )
                                res = 2;
                        }
                    }
                    return res;
                }
                return 0;
            }
            public string Music => "Dreadnaught";
            public string FightName => "Dreadnaught";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 5.0f),
                            new(Difficulty.Normal, 12.0f),
                            new(Difficulty.Extreme, 18.3f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 5.0f),
                            new(Difficulty.Normal, 12.0f),
                            new(Difficulty.Extreme, 18.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 10.0f),
                            new(Difficulty.Normal, 17.9f),
                            new(Difficulty.Extreme, 21.2f),
                        }
                    );
                public override string BarrageAuthor => "zKronO vs Tlottgodinf";
                public override string AttributeAuthor => "Tlottgodinf x zKronO";
                public override string PaintAuthor => "Sour";
                public override string SongAuthor => "SK_kent";

                public override string DisplayName
                {
                    get
                    {
                        if (PlayerManager.CurrentUser == null) return "Dreadnaught";
                        int p = AnomalyExist();
                        if (p >= 1) return "RHJlYWRuYXVnaHQ=";
                        return "Dreadnaught";
                    }
                }
            }
            public SongInformation Attributes => new ThisInformation();
            private bool notRegistered = true;
            public static Game game;
           
            public void ScreenScaleAdd(float scale, float time)
            {
                time /= 2;
                ValueEasing.EaseBuilder e1 = new();
                e1.Insert(time, ValueEasing.EaseInQuint(ScreenDrawing.ScreenScale, ScreenDrawing.ScreenScale + scale / 2, time));
                e1.Insert(time, ValueEasing.EaseOutQuint(ScreenDrawing.ScreenScale + scale / 2, ScreenDrawing.ScreenScale + scale, time));
                e1.Insert(1, ValueEasing.Stable(0));
                e1.Run((s) =>
                {
                    ScreenDrawing.ScreenScale = s;
                });
            }
            void MakeLine(bool direction)
            {
                if (direction)
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(640, 0), BeatTime(1)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(9, l);
                }
                else
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(0, 0), BeatTime(1)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(9, l);
                }
            }
            #region EX
            void Part1()
            {
                ScreenDrawing.UISettings.CreateUISurface();
                DelayBeat(4, () =>
                {
                    CentreEasing.EaseBuilder e1 = new();
                    CentreEasing.EaseBuilder e2 = new();
                    CentreEasing.EaseBuilder e3 = new();
                    CentreEasing.EaseBuilder e4 = new();
                    e1.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(320, 240 - 340), game.BeatTime(2)));
                    e2.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(320, 240 + 340), game.BeatTime(2)));
                    e3.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(660, 240), game.BeatTime(2)));
                    e4.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(-20, 240), game.BeatTime(2)));
                    Line a = new(e1.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.55f };
                    Line b = new(e2.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.55f };
                    Line c = new(e3.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    Line d = new(e4.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    CreateEntity(c);
                    CreateEntity(d);
                    DelayBeat(4, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                        c.Dispose();
                        d.Dispose();
                    });
                    LineShadow(3, 0.9f, 9, a);
                    LineShadow(3, 0.9f, 9, b);
                    LineShadow(3, 0.9f, 9, c);
                    LineShadow(3, 0.9f, 9, d);
                });
                RegisterFunctionOnce("lineL", () =>
                {
                    CentreEasing.EaseBuilder ease = new();
                    ease.Insert(0, CentreEasing.Stable(new(840, 240)));
                    ease.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(840, 240), new(-20, 240), BeatTime(2)));
                    Line ce = new(ease.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(ce);

                    DelayBeat(4, () =>
                    {
                        ce.Dispose();
                    });
                });
                RegisterFunctionOnce("lineR", () =>
                {
                    CentreEasing.EaseBuilder ease = new();
                    ease.Insert(0, CentreEasing.Stable(new(-200, 240)));
                    ease.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(-200, 240), new(660, 240), BeatTime(2)));
                    Line ce = new(ease.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(ce);

                    game.DelayBeat(4, () =>
                    {
                        ce.Dispose();
                    });
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(-3, game.BeatTime(2));
                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(3, game.BeatTime(2));
                });
                RegisterFunctionOnce("ShiningSoul", () => { SetSoul(1); });
                RegisterFunctionOnce("Bound", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ValueEasing.EaseBuilder vea = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    vea.Insert(game.BeatTime(32), ValueEasing.Stable(0.2f));
                    vea.Insert(game.BeatTime(16), ValueEasing.EaseOutSine(0.4f, 0.99f, game.BeatTime(16)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutCubic(0, 80, game.BeatTime(4)));
                    //ve.Insert(game.BeatTime(28), ValueEasing.Stable(28));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(80, 120, game.BeatTime(4)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(120, 160, game.BeatTime(4)));
                    for (int i = 0; i < 16; i++)
                    {
                        ve.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutBack(160 + i * 12, 172 + i * 12, game.BeatTime(0.5f)));
                    }
                    ve.Insert(0, ValueEasing.Stable(240));
                    ve.Run((s) =>
                    {
                        ScreenDrawing.UpBoundDistance = s;
                        ScreenDrawing.DownBoundDistance = s;
                    });
                    vea.Run((s) =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.DarkRed * 0.7f, s);
                    });
                });
                game.RegisterFunctionOnce("upload", () =>
                {
                    float z = Rand(0, 1);
                    if (z == 0)
                    {
                        CentreEasing.EaseBuilder v = new();
                        CentreEasing.EaseBuilder vb = new();
                        CentreEasing.EaseBuilder va = new();
                        v.Insert(0, CentreEasing.Stable(new(320, 500)));
                        va.Insert(0, CentreEasing.Stable(new(0, 820)));
                        vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                        v.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(2)));
                        va.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(2)));
                        vb.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(2)));
                        Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                        Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                        CreateEntity(a);
                        CreateEntity(b);
                        LineShadow(3, 0.9f, 4, a);
                        LineShadow(3, 0.9f, 4, b);
                        game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                        ValueEasing.EaseBuilder bl = new();
                        bl.Insert(0, ValueEasing.Stable(0));
                        bl.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 8, BeatTime(0.25f)));
                        bl.Insert(BeatTime(2.75f), ValueEasing.EaseOutCubic(8, 0, BeatTime(2.75f)));
                        bl.Run((x) =>
                        {
                            Blur.Sigma = x * 2;
                            splitter.Intensity = 1 + x * 0.1f;
                            StepSample.Intensity = 0.01f + x * 0.01f;
                        });
                    }
                    else if (z == 1)
                    {
                        CentreEasing.EaseBuilder v = new();
                        CentreEasing.EaseBuilder vb = new();
                        CentreEasing.EaseBuilder va = new();
                        v.Insert(0, CentreEasing.Stable(new(320, 500)));
                        va.Insert(0, CentreEasing.Stable(new(0, 820)));
                        vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                        v.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(2)));
                        va.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(2)));
                        vb.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(2)));
                        Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                        Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                        CreateEntity(a);
                        CreateEntity(b);
                        LineShadow(3, 0.9f, 4, a);
                        LineShadow(3, 0.9f, 4, b);
                        game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                        ValueEasing.EaseBuilder bl = new();
                        bl.Insert(0, ValueEasing.Stable(0));
                        bl.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 8, BeatTime(0.25f)));
                        bl.Insert(BeatTime(2.75f), ValueEasing.EaseOutCubic(8, 0, BeatTime(2.75f)));
                        bl.Run((x) =>
                        {
                            Blur.Sigma = x * 2;
                            splitter.Intensity = 1 - x * 0.1f;
                            StepSample.Intensity = 0.01f + x * 0.01f;
                        });
                    }
                    //写个色散+模糊的blur
                });
                RegisterFunctionOnce("soulR", () => { SetSoul(0); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                game.RegisterFunctionOnce("convL", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(2, game.BeatTime(1), false);
                });
                game.RegisterFunctionOnce("convR", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(2, game.BeatTime(1), true);
                });
                RegisterFunctionOnce("soulG", () => { SetSoul(1); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder v = new();
                    ValueEasing.EaseBuilder vb1 = new();
                    ValueEasing.EaseBuilder vb2 = new();
                    ValueEasing.EaseBuilder co = new();
                    v.Insert(game.BeatTime(1), ValueEasing.EaseOutSine(0, 2.4f, game.BeatTime(1)));
                    v.Insert(game.BeatTime(1), ValueEasing.EaseInSine(2.4f, 0, game.BeatTime(1)));
                    vb1.Insert(0, ValueEasing.Stable(240));
                    vb1.Insert(BeatTime(2), ValueEasing.EaseInQuad(240, 0, BeatTime(2)));
                    vb1.Insert(BeatTime(1), ValueEasing.Stable(0));
                    vb2.Insert(0, ValueEasing.Stable(240));
                    vb2.Insert(BeatTime(2), ValueEasing.EaseInQuad(240, 0, BeatTime(2)));
                    vb2.Insert(BeatTime(1), ValueEasing.Stable(0));
                    co.Insert(BeatTime(2), ValueEasing.Linear(0.1f, 0.99f, BeatTime(2)));
                    co.Insert(BeatTime(4), ValueEasing.Stable(0.99f));
                    v.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s * 0.2f;
                        splitter.Intensity = 1 + s;
                    });
                    vb1.Run((s) => { ScreenDrawing.UpBoundDistance = s; });
                    vb2.Run((s) => { ScreenDrawing.DownBoundDistance = s; });
                    co.Run((s) => { ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.DarkRed, s); });
                    ScreenDrawing.CameraEffect.SizeShrink(0.45f, BeatTime(2));
                });
                RegisterFunctionOnce("WaveR", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(0, ValueEasing.Stable(0));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0, 7.2f, BeatTime(1)));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(7.2f, -3.6f, BeatTime(1)));
                    a.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-3.6f, 0, BeatTime(1.5f)));
                    a.Insert(0, ValueEasing.Stable(0));
                    a.Run((s) => { ScreenDrawing.ScreenAngle = s * 0.3f; });
                });
                RegisterFunctionOnce("wLineL", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, Rand(100, 300)));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -1), new(0, 0.2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(-32, -20)));
                    CreateEntity(l);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 0.5f, () =>
                        {
                            Line ls = l.Split(false);
                            CreateEntity(ls);
                            ls.Alpha = 0.7f;
                            ls.AlphaDecrease(BeatTime(3));
                        });
                    }
                    LineShadow(6, 0.4f, 8, l);
                });
                RegisterFunctionOnce("WaveL", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    ValueEasing.EaseBuilder alp = new();
                    a.Insert(0, ValueEasing.Stable(0));
                    alp.Insert(0, ValueEasing.Stable(0.85f));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0, -7.2f, BeatTime(1)));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(-7.2f, 3.6f, BeatTime(1)));
                    a.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(3.6f, 0, BeatTime(1.5f)));
                    a.Insert(0, ValueEasing.Stable(0));
                    alp.Insert(BeatTime(3), ValueEasing.EaseOutBounce(0.85f, 0, BeatTime(3)));
                    a.Run((s) => { ScreenDrawing.ScreenAngle = s * 0.3f; });
                });
                RegisterFunctionOnce("wLineR", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, Rand(100, 300)));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -1), new(0, 0.2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(180 + Rand(20, 32)));
                    CreateEntity(l);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 0.5f, () =>
                        {
                            Line ls = l.Split(false);
                            ls.Alpha = 0.7f;
                            CreateEntity(ls);
                            ls.AlphaDecrease(BeatTime(3));
                        });
                    }
                    LineShadow(6, 0.4f, 8, l);
                });
                RegisterFunctionOnce("RDcross", () =>
                {
                    float random = Rand(1, 4);
                    CentreEasing.EaseBuilder ce1 = new();
                    CentreEasing.EaseBuilder ce2 = new();
                    ValueEasing.EaseBuilder alp = new();
                    ce1.Insert(0, CentreEasing.Stable(new(320, 240)));
                    ce2.Insert(0, CentreEasing.Stable(new(320, 240)));
                    alp.Insert(0, ValueEasing.Stable(0.85f));
                    alp.Insert(BeatTime(2), ValueEasing.EaseOutSine(0.85f, 0, BeatTime(2)));
                    if (random == 1)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 0; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 0; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else if (random == 2)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else if (random == 3)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 90; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 90; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                });
                RegisterFunctionOnce("FakeNotes", () =>
                {
                    for (int a = 0; a < 5; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(6 + a * 0.1f, 0), new(-0.2f, 0.4f)));
                        FakeNote.LeftNote note = new(BeatTime(4), 6 + a * 0.1f, Rand(0, 1), 0, CentreEasing.Accerlating(new(9 + a * 1f, 0), new(-0.05f, 0.1f)), BeatTime(4));
                        note.Offset = new(Rand(-200, 0), Rand(-30, 30));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(4), ValueEasing.Stable(180));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(180, 180 + Rand(50, 90), BeatTime(4)));
                        ve.Run((s) => { note.Rotation = 180; });
                        CreateEntity(note);
                    }
                    for (int a = 0; a < 5; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(-6 + a * 0.1f, 0), new(0.2f, 0.4f)));
                        FakeNote.RightNote note = new(BeatTime(4), 6 + a * 0.1f, Rand(0, 1), 0, CentreEasing.Accerlating(new(-9 + a * 1f, 0), new(+0.05f, 0.1f)), BeatTime(4));
                        note.Offset = new(Rand(0, 200), Rand(-30, 30));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(4), ValueEasing.Stable(180));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(180, 180 + Rand(50, 90), BeatTime(4)));
                        ve.Run((s) => { note.Rotation = 0; });
                        CreateEntity(note);
                    }
                });
                CreateChart(BeatTime(4), BeatTime(1), 6f, new string[]
                {
            "(#3.5#$3)(<+0'0.8)(>+0'0.8)","","","",   "+2","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N0)(WaveR)(wLineR)","","","",   "+2","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N3)(WaveR)(wLineR)","","","",   "+2","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N2)(WaveL)(wLineL)","","","",   "+2","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "(#3.5#N11)(WaveL)(wLineL)","","","",   "+21","","","",
            "+01","","","",   "+01","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N01)(WaveR)(wLineR)","","","",   "+21","","","",
            "+01","","","",   "+01","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N31)(WaveR)(wLineR)","","","",   "+21","","","",
            "+01","","","",   "+01","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N21)(WaveL)(wLineL)","","","",   "+21","","","",
            "+01","","","",   "+01","","","",
            "","","","",   "","","","",
            "","","","",   "","","(RotateL)","",
            //
            "(R)(+01)(Bound)(lineL)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateR)","",
            "(R)(+01)(lineR)","","+0","",    "R","","","",
            "R(RDcross)","","","",    "R","","(RotateR)","",

            "(R)(+01)(lineR)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateL)","",
            "(R)(+01)(lineL)","","+0","",    "R","","","",
            "R(RDcross)","","","",    "R","","(RotateL)","",

            "(R)(+01)(lineL)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateR)","",
            "(R)(+01)(lineR)","","+0","",    "R","","","",
            "R(RDcross)","","","",    "R","","(RotateR)","",

            "(R)(+01)(lineR)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateL)","",
            "(R)(+01)(lineL)","","+0","",    "R","","","",
            "R(RDcross)","","","",    "R","","(RotateL)","",
            //
            "(R)(+01)(lineL)","","","",    "R1","","+01","",
            "+01(RDcross)","","","",    "D1","","(RotateR)","",
            "(R)(+01)(lineR)","","+01","",    "R1","","","",
            "R1(RDcross)","","","",    "R1","","(RotateR)","",

            "(R)(+01)(lineR)","","","",    "R1","","+01","",
            "+01(RDcross)","","","",    "D1","","(RotateL)","",
            "(R)(+01)(lineL)","","+01","",    "R1","","","",
            "R1(RDcross)","","","",    "R1","","(RotateL)","",

            "(R)(+01)(lineL)","","","",    "R1","","+01","",
            "+01(RDcross)","","","",    "D1","","(RotateR)","",
            "(R)(+01)(lineR)","","+01","",    "R1","","","",
            "R1(RDcross)","","","",    "R1","","(RotateR)","",

            "(R)(+01)(lineR)","","","",    "R1","","+01","",
            "+01(RDcross)","","","",    "D1","","(RotateL)","",
            "(R'1.3)(D1'0.9)(lineL)","","","",    "(D'0.9)(R1'1.3)","","","",
            "(R'1.3)(D1'0.9)(RDcross)","","","",    "(D'0.9)(R1'1.3)","","","",
            //
            "(upload)(soulR)(convL)","","","",   "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "(upload)(soulG)(convR)","","","",   "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",

            "(upload)(^R)(^+01)","","","",    "(^D)(^+01)","","","",
            "(^D)(^+01)","","","",    "(^D)(^+01)","","","",
            "(^D)(^+01)(FakeNotes)","","","",    "(^D)(^+01)","","","",
            "(^D)(^+01)","","","",    "(^D)(^+01)","","","",

            "(upload)($0)(+2)","","($21)(+21)","",    "($0)(+2)","","($21)(+21)","",
            "($0)($21)","($0)($21)","($0)($21)","($0)($21)",    "($0)($21)","($0)($21)","($0)($21)","($0)($21)",
            "Blur1","","","",   "","","","",
            "","","","",   "","","","",
                });
            }//zKronO's turn!
            void Part2()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                RegisterFunctionOnce("soulR", () => { SetSoul(0); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("soulB", () => { SetSoul(2); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Change", () =>
                {
                    game.DelayBeat(3, () => { SetBox(new Vector2(320, 240), 240, 240); });
                });
                RegisterFunctionOnce("atk1", () =>
                {
                    game.DelayBeat(0, () =>
                    {
                        Heart.GiveForce(180, 8);
                        CreateEntity(new Boneslab(0, 160, game.BeatTime(0.5f), game.BeatTime(4)));
                        CreateEntity(new Boneslab(90, 100, game.BeatTime(0.5f), game.BeatTime(2)));
                        CreateEntity(new Boneslab(270, 100, game.BeatTime(0.5f), game.BeatTime(2)));
                    });
                    game.DelayBeat(2.5f, () =>
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(i * 0.5f, () =>
                            {
                                UpBone b1 = new(true, 4, 80) { ColorType = Rand(1, 2) };
                                UpBone b2 = new(false, 4, 80) { ColorType = Rand(1, 2) };
                                CreateBone(b1);
                                CreateBone(b2);
                                AddInstance(new TimeRangedEvent(1145, () =>
                                {
                                    b1.Speed -= 0.075f;
                                    b2.Speed -= 0.075f;
                                }));
                            });
                        }
                    });
                    game.DelayBeat(4.5f, () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            game.DelayBeat(i * 0.5f, () =>
                            {
                                CreateBone(new UpBone(true, 5, 20));
                                CreateBone(new UpBone(false, 5, 20));
                            });
                        }
                    });
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    Heart.GiveForce(0, 12);
                    CreateEntity(new Boneslab(180, 160, game.BeatTime(2), game.BeatTime(4)));
                    for (int i = 0; i < 4; i++)
                    {
                        game.DelayBeat(i * 1, () =>
                        {
                            CreateEntity(new Boneslab(0, 30, game.BeatTime(2), game.BeatTime(0.33f)));
                            game.DelayBeat(2, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(new CustomBone(new(320 - 120, 240 + 120 - 55), Motions.PositionRoute.linear, -30, 280)
                                {
                                    PositionRouteParam = new float[] { 12, 0 },
                                    ColorType = 2
                                });
                                CreateBone(new CustomBone(new(320 - 120 - 14, 240 + 120 - 55), Motions.PositionRoute.linear, -30, 280)
                                {
                                    PositionRouteParam = new float[] { 12, 0 },
                                    ColorType = 2
                                });
                                CreateBone(new CustomBone(new(320 + 120, 240 + 120 - 55), Motions.PositionRoute.linear, 30, 280)
                                {
                                    PositionRouteParam = new float[] { -12, 0 },
                                    ColorType = 2
                                });
                                CreateBone(new CustomBone(new(320 + 120 + 14, 240 + 120 - 55), Motions.PositionRoute.linear, 30, 280)
                                {
                                    PositionRouteParam = new float[] { -12, 0 },
                                    ColorType = 2
                                });
                            });
                        });
                    }
                    game.DelayBeat(6, () =>
                    {
                        SetSoul(0);
                        ValueEasing.EaseBuilder v = new();
                        v.Insert(game.BeatTime(1.5f), ValueEasing.EaseInCubic(0, 110, game.BeatTime(1.5f)));
                        v.Insert(game.BeatTime(1.5f), ValueEasing.EaseOutQuart(110, 0, game.BeatTime(1.5f)));
                        v.Run((s) =>
                        {
                            InstantSetBox(new Vector2(320, 240), 240 - s, 240 - s);
                        });
                        for (int i = 0; i < 36; i++)
                        {
                            SideCircleBone b = new(i * 10, 8, 50, game.BeatTime(2.85f));
                            CreateBone(b);
                        }

                    });
                    game.DelayBeat(9, () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(270, 12);
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(i * 2 + 1, () =>
                            {
                                CreateBone(new LeftBone(true, 4, 160 + Rand(0, 30)));
                                CreateBone(new RightBone(true, 4, 30 - LastRand));
                                PlaySound(Sounds.pierce);
                            });
                            game.DelayBeat(i * 2 + 2, () =>
                            {
                                CreateBone(new LeftBone(false, 4, 160 + Rand(0, 30)));
                                CreateBone(new RightBone(false, 4, 30 - LastRand));
                                PlaySound(Sounds.pierce);
                            });
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            game.DelayBeat(i * 4 + 1, () =>
                            {
                                CreateBone(new RightBone(false, 6, 240) { ColorType = 2 });
                            });
                            game.DelayBeat(i * 4 + 3, () =>
                            {
                                CreateBone(new RightBone(true, 6, 240) { ColorType = 2 });
                            });
                        }
                    });
                });
                RegisterFunctionOnce("atk3", () =>
                {
                    SetSoul(0);
                    Heart.GiveInstantForce(0, 0);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(game.BeatTime(1.5f), ValueEasing.EaseInCubic(0, 90, game.BeatTime(1.5f)));
                    v.Insert(game.BeatTime(1.5f), ValueEasing.EaseOutQuart(90, 0, game.BeatTime(1.5f)));
                    v.Run((s) =>
                    {
                        InstantSetBox(new Vector2(320, 240), 240 - s, 240 - s);
                    });
                    for (int i = 0; i < 36; i++)
                    {
                        SideCircleBone b = new(i * 10, 8, 50, game.BeatTime(3));
                        CreateBone(b);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        CreateBone(new CentreCircleBone(90 * i + 70, 2.7f, 300, game.BeatTime(4)) { IsMasked = true }); ;

                    }
                    CreateBone(new CentreCircleBone(90 + 60 + 45, -4f, 140, game.BeatTime(6)) { IsMasked = true, ColorType = 2 }); ;
                    game.DelayBeat(6.75f, () =>
                    {
                        SetGreenBox();
                    });
                });
                RegisterFunctionOnce("Bones1", () =>
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new LeftBone(true, 6, 70));
                    CreateBone(new RightBone(false, 6, 70));
                });
                RegisterFunctionOnce("Scale+", () =>
                {
                    ScreenDrawing.ScreenScale += 0.05f;
                });
                RegisterFunctionOnce("Scale++", () =>
                {
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("ScaleRet", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(2), 1, 0.07f);
                });
                RegisterFunctionOnce("RandomSniperBone", () =>
                {

                    for (int a = 0; a < Rand(2, 3); a++)
                    {
                        float rot = Rand(10, 80);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(BoxStates.Left - 40, BoxStates.Up - 40));
                        ce.Insert(BeatTime(4), CentreEasing.Linear(MathUtil.GetVector2(8, rot)));
                        CustomBone cb = new(new(0, 0), ce.GetResult(), rot + 90, 35);
                        CreateBone(cb);
                    }

                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("soulG", () => { SetSoul(1); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Bound1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(0, 30, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(30, 10, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(10, 40, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(40, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(20, 50, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(50, 30, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(30, 60, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(60, 40, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(40, 70, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(70, 50, BeatTime(0.25f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = s + 90; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), false);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), false);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(0, 0), new(240, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(-240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(-120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(0, 0), new(380, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), true);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), true);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(640, 0), new(400, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(640, 0), new(480, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(640, 0), new(260, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("LeftLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(660, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.5f));
                    LineShadow(3, 0.4f, 2, l);
                });
                RegisterFunctionOnce("RightLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(-20, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.5f));
                    LineShadow(3, 0.4f, 2, l);
                });
                RegisterFunctionOnce("MidLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(360, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1f));
                    LineShadow(3, 0.4f, 2, l);
                    l.TransverseMirror = true;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 1.1f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseInQuad(1.1f, 1f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                });
                RegisterFunctionOnce("MidOutLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(320, 0), new(660, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1f));
                    LineShadow(3, 0.1f, 3, l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("MakeF", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                });
                RegisterFunctionOnce("MakeF2", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "Change","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "soulB","","","",   "atk1","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "atk2","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "Bones1","","","",
            "","","","",   "Bones1","","","",
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

            "","","","",   "","","atk3","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "RandomSniperBone","","","",   "RandomSniperBone","","","",
            "RandomSniperBone","","RandomSniperBone","",   "","","RandomSniperBone","",
            "","","RandomSniperBone","",   "","","RandomSniperBone","",
            "","","RandomSniperBone","",   "RandomSniperBone","soulG","","",
            //green
            "(R)(LeftLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","","",   "D","","+0","",

            "(R)(RightLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+2","","+2","",
            "+2","","+11","",   "+21(LeftLine2)","","+21","",

            "(R1)(LeftLine2)","","","",   "D1","","+21","",
            "+21","","+21","",   "(RightLine2)","","(D1)","",
            "(RightLine2)","","(D1)","",   "+01","","+01","",
            "+01","","","",   "(D1)","","+01","",

            "(+01)MidLine","","","",   "D1","","+21","",
            "+21","","+21","",   "","","(R1)(+0)","",
            "","MakeF","(R1)(+0)Scale+","MakeF",   "($3)($31)Scale+","MakeF","(>$3'1.5)(<$31'1.3)Scale+","MakeF",
            "($2)($01)Scale+","MakeF","(>$2'1.3)(<$01'1.3)Scale+","MakeF",   "($1)($11)Scale+","MakeF","(>$1'1.3)(<$11'1.3)Scale+","MakeF",
            //
            "D(RightLine1)(ScaleRet)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "D","","+0","",
            "+0","","","",   "+11","","+01","",

            "(R1)(LeftLine1)","","","",   "D1","","+21","",
            "+21","","+21","",   "","","(D1)","",
            "","","R1","",   "+21","","+21","",
            "+21","","+1","",   "+2(RightLine2)","","+2","",

            "(R)(RightLine2)","","","",   "D","","+2","",
            "+2","","+2","",   "(LeftLine2)","","(D)","",
            "(LeftLine2)","","(D)","",   "D","","+0","",
            "+0","","","",   "D","","+11","",

            "(+21)MidLine","","","",   "R1","","","",
            "D1","","+21","",   "","","","MakeF2",
            "($0'1.3)(+2'1.3)(MidOutLine)(Scale++)","","","MakeF2",   "($01'1.3)(+21'1.3)(MidOutLine)(Scale++)","","","MakeF2",
            "($0'1.3)(+2'1.3)(MidOutLine)(Scale++)","","","MakeF2",   "($01'1.3)(+21'1.3)(MidOutLine)(Scale++)","","","",
            "($0'1.3)(+2'1.3)(MidOutLine)(ScaleRet)(MakeF2)","","","",
                    //
                });
            }//ParaDOXXX's turn!
            void Part3()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                ScreenDrawing.BoundColor = Color.DarkRed * 0.6f;
                RegisterFunctionOnce("BoundStart", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0, 60, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                });
                RegisterFunctionOnce("Bound1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(60));
                    for (int a = 0; a < 8 * 8 - 8; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(60 + a * 2, 120 + a * 2, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutSine(120 + a * 2, 60 + (a + 1) * 2, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(60 + 56 * 2, 0, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(56), ValueEasing.Linear(0, 1f, BeatTime(56)));
                    ve2.Run((s) => { ScreenDrawing.BoundColor = Color.Lerp(Color.DarkRed, Color.White * 0.75f, s) * 0.6f; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    Line l = new(CentreEasing.EaseOutCubic(new(0, 0), new(640, 0), BeatTime(1f)), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    l.AlphaDecrease(BeatTime(1));
                    CreateEntity(l);
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(0, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    l.AlphaDecrease(BeatTime(1));
                    CreateEntity(l);
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("ConvL1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.6f), false);
                });
                RegisterFunctionOnce("ConvR1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.6f), true);
                });
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 10, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(10, 0, BeatTime(0.25f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.33f), ValueEasing.EaseOutQuart(0, 10, BeatTime(0.33f)));
                    ve.Insert(BeatTime(0.33f), ValueEasing.EaseOutQuad(10, 0, BeatTime(0.33f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(20, 0, BeatTime(0.75f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("ScaleIn", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(0.75f), ScreenDrawing.ScreenScale + 0.175f, 0.1f);
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(0.5f), 1, 0.15f);
                });
                RegisterFunctionOnce("FinalLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(0, 0), new(190, 0), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(190, 0), new(380, 0), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(380, 0), new(650, 0), BeatTime(0.75f)));
                    ValueEasing.EaseBuilder rot = new();
                    rot.Insert(0, ValueEasing.Stable(90));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(90, 80, BeatTime(0.75f)));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(80, 70, BeatTime(0.75f)));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(70, 60, BeatTime(0.75f)));
                    Line l = new(ce.GetResult(), rot.GetResult());
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    LineShadow(10, l);
                    DelayBeat(1.5f, () => { l.AlphaDecrease(BeatTime(1f)); });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                });
                bool another = false;
                if (another)
                    CreateChart(BeatTime(8), BeatTime(0.5f), 6, new string[]
                    {

            "R","","","",   "R1","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "R1","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "R","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "R","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","R1","",   "R","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                    });
                if (!another)
                    CreateChart(BeatTime(4), BeatTime(1f), 6, new string[]
                    {
                "","","","",   "","","","",
                "","","","",   "","","","",

                "BoundStart(LeftLine1)(ConvL1)","","","",   "R1(Bound1)(LeftLine1)(ConvL1)","","+01","",
                "R(LeftLine1)(ConvL1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",
                "R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",
                "R(LeftLine1)(ConvL1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",

                "R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",
                "R(LeftLine1)(ConvL1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",
                "R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",
                "R(LeftLine1)(ConvL1)","","+0","",   "R1(LeftLine1)(ConvL1)","","+01","",

                "R(RightLine1)(ConvR1)(Blur1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",
                "R(RightLine1)(ConvR1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",
                "R(RightLine1)(ConvR1)(Blur1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",
                "R(RightLine1)(ConvR1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",

                "R(RightLine1)(ConvR1)(Blur1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",
                "R(RightLine1)(ConvR1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",
                "R(RightLine1)(ConvR1)(Blur1)","","+1","",   "R1","","-11","",
                "R(RightLine1)(ConvR1)","","+1","",   "R1(RightLine1)(ConvR1)","","-11","",
                //
                "#0.18#R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",

                "#0.18#R(RightLine1)(ConvR1)(Blur1)","","+0","",   "#0.18#R1(RightLine1)(ConvR1)","","+01","",
                "#0.18#R(RightLine1)(ConvR1)","","+0","",   "#0.18#R1(RightLine1)(ConvR1)","","+01","",
                "#0.18#R(RightLine1)(ConvR1)(Blur1)","","+0","",   "#0.18#R1(RightLine1)(ConvR1)","","+01","",
                "#0.18#R(RightLine1)(ConvR1)","","+0","",   "#0.18#R1(RightLine1)(ConvR1)","","+01","",

                "#0.18#R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)(Blur1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","","+01","",

                "#0.18#R(RightLine1)(ConvR1)(Blur1)","","+0","",   "#0.18#R1(RightLine1)(ConvR1)","","+01","",
                "#0.18#R(LeftLine1)(ConvL1)","","+0","",   "#0.18#R1(LeftLine1)(ConvL1)","(Blur2)","","",
                "(^R'1.4)(^+2'1.4)(ScaleIn)(FinalLine)(Flicker)","","","(Blur2)",   "","","(^R'1.4)(^+2'1.4)(ScaleIn)(Flicker)","",
                "","(Blur2)","","",   "(^R'1.4)(^+2'1.4)(ScaleBack)(Flicker)","","(Blur3)","",
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
                        //
                    });
            }//Tlottgodinf's turn!
            void Part4()
            {

                RegisterFunctionOnce("soulG", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("crossbone1", () =>
                {
                    DrawingUtil.CrossBone(new Vector2(320 - 135, Heart.Centre.Y), new Vector2(4, 0), 30, 2, Rand(1, 2));
                    DrawingUtil.CrossBone(new Vector2(320 + 135, Heart.Centre.Y), new Vector2(-4, 0), 30, 2, LastRand);
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("crossbone2", () =>
                {
                    DrawingUtil.CrossBone(new Vector2(320, 120), new Vector2(0, 4), 30, 2, 2);
                    DrawingUtil.CrossBone(new Vector2(320, 360), new Vector2(0, -4), 30, 2, 2);
                    PlaySound(Sounds.pierce);

                });
                RegisterFunctionOnce("atk1", () =>
                {
                    SetSoul(0);
                    Heart.Speed = 3.25f;
                    SetBox(new Vector2(320, 240), 240, 180);
                    float t = 0;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(16), ValueEasing.Linear(360 / BeatTime(3.3f)));
                    ve.Run((s) => { t = s; });
                    float t2 = 0;
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(16), ValueEasing.Linear(360 / BeatTime(3.5f)));
                    ve2.Run((s) => { t2 = s; });
                    for (int i = 5; i < BeatTime(5); i++)
                    {
                        AddInstance(new InstantEvent(i * 3, () =>
                        {

                            CreateBone(new DownBone(true, 8, Sin(t) * 50f + 50) { MarkScore = false });
                            CreateBone(new UpBone(true, 8, -Sin(t) * 50f + 50) { MarkScore = false });
                            CreateBone(new DownBone(false, 6, Cos(t2 - 30) * 50f + 50) { MarkScore = false });
                            CreateBone(new UpBone(false, 6, -Cos(t2 - 30) * 50f + 50) { MarkScore = false });
                        }));
                    }
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    SetSoul(0);
                    Heart.Speed = 3.2f;
                    SetBox(new Vector2(320, 240), 180, 240);
                    PlaySound(Sounds.pierce);
                    DelayBeat(0.5f, () => { PlaySound(Sounds.pierce); });
                    for (int i = 1; i < 8; i++)
                    {
                        DelayBeat(i * 1f, () =>
                        {

                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 2.75f, 80));
                            CreateBone(new RightBone(false, 2.75f, 80));
                        });
                        DelayBeat(i * 1f + 0.5f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(BoxStates.Centre.X, BoxStates.Up - 21));
                            ce.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4)));
                            CreateBone(new CustomBone(new(0, 0), ce.GetResult(), 0, 40));

                        });
                    }
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ScreenDrawing.BoundColor = Color.DarkRed;
                    ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = 0.1f;
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInCubic(0, 0.72f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0.72f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s * 0.2f;
                        StepSample.Intensity = 0.01f + s * 6f;
                        splitter.Intensity = 1f + 7f * s;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 8, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutSine(8, 0, BeatTime(0.25f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;

                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(20, 0, BeatTime(0.5f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    Line[] ls = GetAll<Line>("A");
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1f), CentreEasing.EaseOutQuint(new(0, 0), new(380, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Tags = new string[] { "A" } };
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(1, () => { l.AlphaDecrease(BeatTime(1f)); });
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    Line[] ls = GetAll<Line>("A");
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(1f), CentreEasing.EaseOutQuint(new(320, 0), new(-15, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Tags = new string[] { "A" } };
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(1, () => { l.AlphaDecrease(BeatTime(1f)); });
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("WarnLineBlue", () =>
                {
                    Line l = new(new Vector2(320, 260), 45) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(new Vector2(320, 220), 45) { DrawingColor = Color.CornflowerBlue };
                    CreateEntity(l);
                    CreateEntity(l2);
                    l.AlphaDecrease(BeatTime(0.35f));
                    l2.AlphaDecrease(BeatTime(0.35f));
                    l.TransverseMirror = true;
                    l2.TransverseMirror = true;
                });
                RegisterFunctionOnce("WarnLineRed", () =>
                {
                    Line l = new(new Vector2(320, 260), 45) { DrawingColor = Color.LightCoral };
                    Line l2 = new(new Vector2(320, 220), 45) { DrawingColor = Color.LightCoral };
                    CreateEntity(l);
                    CreateEntity(l2);
                    l.AlphaDecrease(BeatTime(0.35f));
                    l2.AlphaDecrease(BeatTime(0.35f));
                    l.TransverseMirror = true;
                    l2.TransverseMirror = true;
                });
                RegisterFunctionOnce("Scales", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutCubic(1, 1.15f, BeatTime(1)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuart(1.15f, 1.33f, BeatTime(1.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(1.33f, 1.15f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1.15f, 1, BeatTime(1)));
                    ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                });
                RegisterFunctionOnce("SmallFlicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.25f);
                });
                RegisterFunctionOnce("StepFollow", () =>
                {
                    ForBeat(16, () => { StepSample.CentreX = Heart.Centre.X; StepSample.CentreY = Heart.Centre.Y; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 0.08f, BeatTime(1)));
                    ve.Insert(BeatTime(14), ValueEasing.Stable(0.08f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0.08f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                });
                RegisterFunctionOnce("SmallBlur", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseInSine(0, 10, BeatTime(1)));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(10, 0, BeatTime(1)));
                    ve.Run((s) => { Blur.Sigma = s; });
                });
                RegisterFunctionOnce("StepFollow2", () =>
                {
                    ForBeat(8, () => { StepSample.CentreX = Heart.Centre.X; StepSample.CentreY = Heart.Centre.Y; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 0.08f, BeatTime(1)));
                    ve.Insert(BeatTime(6), ValueEasing.Stable(0.08f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0.08f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                });
                RegisterFunctionOnce("SuddenLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(0.5f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutCubic(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    LineShadow(6, l);
                    l.AlphaDecrease(BeatTime(0.75f));
                });
                RegisterFunctionOnce("SuddenLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(0.8f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(0, 0), new(280, 0), BeatTime(0.75f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    LineShadow(6, l);
                    l.AlphaDecrease(BeatTime(0.75f));
                });
                float times = -7.5f;
                float count = 0.2f;
                RegisterFunctionOnce("GravityLine", () =>
                {
                    float randomnumber1 = Rand(-20, 20);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, Rand(200, 400)));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, times), new(0, count)));
                    Line l = new(ce, ValueEasing.Stable(randomnumber1)) { Alpha = 0.7f };
                    CreateEntity(l);
                    l.InsertRetention(new Line.RetentionEffect(BeatTime(0.125f), 0.5f));
                    times -= 1;
                    count += 0.03f;
                });
                RegisterFunctionOnce("FinalShake", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(4, BeatTime(0.125f), false);
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                    DelayBeat(0.125f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(0.125f), true);
                        ScreenDrawing.MakeFlicker(Color.Silver * 0.25f);
                    });
                });
                RegisterFunctionOnce("SilverShake", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.35f);
                    ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1f), RandBool());
                });
                RegisterFunctionOnce("Over", () =>
                {
                    Line[] ls = GetAll<Line>();
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    ScreenDrawing.MakeFlicker(Color.Silver);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "(^R'1.4)(^+21'1.4)","","","",   "R02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "D02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "D","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "D02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "D","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "D02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "D","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "D02{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","+002{Tap}(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "D","","","",

            "(atk1)(crossbone1)(StepFollow)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "crossbone1","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "crossbone1","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "crossbone1","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SuddenLine1)","","","",   "(SuddenLine2)soulG","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R12{Tap}(WarnLineRed)","","+012{Tap}","",
            "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "D12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "D1","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "D12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "D1","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "D12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "D1","","Blur3","",

            "(^R'1.4)(^+21'1.4)","","","",   "R12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "D12{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","+012{Tap}(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "D1","","","",

            "(atk2)(crossbone2)(StepFollow2)","","","",   "","","","",
            "","","","",   "","","","",
            "crossbone2","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "crossbone2","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "soulG","","","",

            "(^R'1.2)(^D1'1.2)(SilverShake)(GravityLine)","","","(^R'1.2)(^D1'1.2)(SilverShake)(GravityLine)",   "","","(^R'1.2)(^D1'1.2)(SilverShake)(GravityLine)","",
            "","","(^$0'1.3)(^$21'1.3)(SilverShake)(GravityLine)","",   "(^$0'1.3)(^$21'1.3)(SilverShake)(GravityLine)","","(^$0'1.3)(^$21'1.3)(SilverShake)(GravityLine)","",
            "(^R'1.3{Down})(+01'1.2{Up})(GravityLine)(FinalShake)","+01'1.2{Up}","","(R'1.2{Down})(^+01'1.3{Up})(GravityLine)(FinalShake)",   "+0'1.2{Down}","","(^R'1.3{Down})(+01'1.2{Up})(GravityLine)(FinalShake)","+01'1.2{Up}",
            "","(R'1.2{Down})(^+01'1.3{Up})(GravityLine)(FinalShake)","^+0'1.3{Down}","",      "!!3","(>^$01'1.5)(+21'1.5)(GravityLine)(FinalShake)","($0'1.5)(>^+2'1.5)","(<^$01'1.5)(+21'1.5)(GravityLine)(FinalShake)",
            "($0'1.5)(<^+2'1.5)(Over)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "Blur","","","",   "","","","",
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
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
            //
            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
                });
            }//zKronO's turn!
            void Part5()
            {
                float del = 50;
                ScreenDrawing.UISettings.RemoveUISurface();
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(2), ValueEasing.EaseInCubic(0, -0.32f, BeatTime(2)));
                    e1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(-0.32f, 0, BeatTime(2)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Polar.Intensity = s;
                    });
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;
                        ScreenDrawing.ScreenScale += s * 0.017f;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseInQuint(0, 0.2f, BeatTime(0.25f)));
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuint(0.2f, 0, BeatTime(0.25f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {

                        StepSample.Intensity = 0.01f + s;


                    });
                });
                RegisterFunctionOnce("Blur4", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseInQuint(0, 0.2f, BeatTime(0.5f)));
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuint(0.2f, 0, BeatTime(0.5f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("Blur5", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseInQuint(0, 0.6f, BeatTime(0.5f)));
                    e1.Insert(BeatTime(3.5f), ValueEasing.EaseOutQuint(0.6f, 0, BeatTime(3.5f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.05f);
                });
                RegisterFunctionOnce("Bound", () =>
                {
                    ForBeat120(12, () =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Lerp(Color.White, Color.Red, 0.21f), 0.4f) * 0.5f;
                    });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(16), ValueEasing.Linear(0, 320, BeatTime(16)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(320, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.LeftBoundDistance = s; ScreenDrawing.RightBoundDistance = s; });
                });
                RegisterFunctionOnce("BlueSoul", () =>
                {
                    HeartAttribute.Gravity = 9f;
                    HeartAttribute.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(320, 260, 128);
                });
                RegisterFunctionOnce("GreenSoul", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("BlueSoul2", () =>
                {
                    HeartAttribute.Gravity = 9f;
                    HeartAttribute.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(150, 260, 128);
                });
                RegisterFunctionOnce("BoneSea", () =>
                {
                    for (int a = 0; a < 32 * 2 * 1.2f; a++)
                        CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 40) { MarkScore = false });

                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), CentreEasing.Stable(500, 335));
                    ve.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(500, 335), new(320, 335), BeatTime(2)));
                    ve.Insert(BeatTime(30), CentreEasing.XSinWave(128, BeatTime(8), 0));
                    Platform p = new(0, new(0, 0), ve.GetResult(), 0, 40);
                    CreateEntity(p);
                    DelayBeat(32, () => { p.Dispose(); });
                });
                RegisterFunctionOnce("BoneSea2", () =>
                {
                    for (int a = 0; a < 32 * 2 * 1.2f; a++)
                        CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 40) { MarkScore = false });

                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), CentreEasing.Stable(500, 165));
                    ve.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(500, 190 + 15), new(320, 190 + 15), BeatTime(2)));
                    ve.Insert(BeatTime(30), CentreEasing.XSinWave(128, BeatTime(8), 0));
                    Platform p = new(0, new(0, 0), ve.GetResult(), 0, 40);
                    CreateEntity(p);
                    DelayBeat(32, () => { p.Dispose(); });
                });
                RegisterFunctionOnce("BoomBone", () =>
                {
                    for (int a = 0; a < Rand(2, 3); a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(Rand(Heart.Centre.X - 10, Heart.Centre.X + 10), Rand(-60, -30)));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.010f, 0.010f), Rand(3.30f, 4.30f)), new(0, Rand(0.10f, 0.20f))));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                        ve.Insert(BeatTime(8), ValueEasing.Accerlating(0, Rand(0.10f, 0.30f) * Someway.Rand0or1()));
                        CustomBone b = new(new(0, 0), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 35 }, IsMasked = false };
                        CreateBone(b);
                    }
                });
                RegisterFunctionOnce("BoomBone2", () =>
                {
                    for (int a = 0; a < Rand(2, 3); a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(Rand(Heart.Centre.X - 10, Heart.Centre.X + 10), 640 + Rand(30, 60)));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-1.60f, 1.60f), Rand(-13.00f, -11.00f)), new(0, Rand(0.10f, 0.20f))));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                        ve.Insert(BeatTime(8), ValueEasing.Accerlating(0, Rand(0.10f, 0.30f) * Someway.Rand0or1()));
                        CustomBone b = new(new(0, 0), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 35 }, IsMasked = false };
                        CreateBone(b);
                    }
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    DrawingUtil.Shock();
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.125f), ValueEasing.EaseInQuint(0, 0.1f, BeatTime(0.125f)));
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuint(0.1f, 0, BeatTime(0.25f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("upload", () =>
                {
                    CentreEasing.EaseBuilder v = new();
                    CentreEasing.EaseBuilder vb = new();
                    CentreEasing.EaseBuilder va = new();
                    v.Insert(0, CentreEasing.Stable(new(320, 500)));
                    va.Insert(0, CentreEasing.Stable(new(0, 820)));
                    vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                    v.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(1f)));
                    va.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(1f)));
                    vb.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(1f)));
                    Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                    Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    LineShadow(3, 0.9f, 4, a);
                    LineShadow(3, 0.9f, 4, b);
                    game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                });
                RegisterFunctionOnce("ScreenRot+", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(2, BeatTime(2f));
                });
                RegisterFunctionOnce("ScreenRot-", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(-2, BeatTime(2f));
                });
                RegisterFunctionOnce("BoundEaseBack", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(30 * a, 40 * (a + 1), BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(40 * (a + 1), 30 * (a + 1), BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.Stable(210));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(210, 140, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(140, 170, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(170, 70, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(70, 100, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(100, 0, BeatTime(0.25f)));
                    ve.Run((s) => { ScreenDrawing.UpBoundDistance = s; ScreenDrawing.DownBoundDistance = s; });
                });
                RegisterFunctionOnce("Con", () => { ScreenDrawing.CameraEffect.Convulse(9, BeatTime(1f), true); });
                RegisterFunctionOnce("Con2", () => { ScreenDrawing.CameraEffect.Convulse(9, BeatTime(1f), false); });
                RegisterFunctionOnce("Line", () =>
                {
                    Line l = new(new Vector2(del * 1.3f, 0), new Vector2(0, del));
                    CreateEntity(l);
                    del += 16;
                    l.AlphaDecrease(BeatTime(0.5f));
                    l.ObliqueMirror = true;
                    l.TransverseMirror = true;
                    l.VerticalMirror = true;
                });
                RegisterFunctionOnce("Mask", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(18), Color.Black, 0.4f);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(BeatTime(16.5f), ValueEasing.Stable(0.4f));
                    v.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.4f, 0, BeatTime(1.5f)));
                    v.Run((s) => { m.alpha = s; });
                    DelayBeat(18, () => { m.Dispose(); });
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
            "","","","",   "Mask","","","",
            //空拍
            "(Bound)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",
            //正片
            "(D{v})(+21{v})","","","",   "+2{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",

            "(D{v})(+21{v})","","","",   "+01{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",

            "(D{v})(+21{v})","","","",   "+2{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur2)","",   "","","","",

            "(R)(+21)(ScaleBack)(upload)","","","",   "D","","+2","",
            "+2","","","",   "(D)(Blur3)","","","",
            "(D)(ScreenRot+)","","+0","",   "+0","","","",
            "D","","Blur3","",   "D","","(Blur3)","",

            "(R1)(ScreenRot-)","","","",   "D1","","+21","",
            "+21","","(Blur3)","",   "(D1)","","","",
            "(D1)(ScreenRot-)","","+01","",   "+01","","","",
            "D1","","","",   "D1(Blur4)","","","",

            "(>^$0'2.0)(>^+21'2.0)(>^$0'2.0)(>^+21'2.0)(upload)(ScreenRot+)(Line)","","","",   "(^$1'1.3)(^+21'1.3)","","","",
            "(>^$2'1.3)(>^+21'1.3)(ScreenRot+)(Line)","","","",   "(^$3'1.3)(^+21'1.3)(Blur4)","","","",
            "(^$0'2.0)(^+21'2.0)(^$0'2.0)(^+21'2.0)(upload)(ScreenRot-)(Line)","","","",   "(<^$1'1.5)(<^+21'1.5)(Line)","","","",
            "(^$2'1.5)(^+21'1.5)(ScreenRot-)(Line)","","(Line)","",   "(<^$3'1.5)(<^+21'1.5)(Blur4)(Line)","","(Line)","",

            "(R)(+01)(upload)(ScreenRot+)(Line)","","","",   "(R)(+01)(Line)","","(R)(+01)(Line)","",
            "","","(R)(+01)(Line)","",   "(R)(+01)(Blur4)(Line)","","","",
            "(R)(+0)(+21)(+01)(ScreenRot+)(Line)","","","",   "(D)(+0)(+21)(+01)(Blur4)(Line)","","","",
            "(D)(+0)(+21)(+01)(Line)","","","",   "(D)(+0)(+21)(+01)(Blur5)(Line)","","","",

            "(D)(+0)(+21)(+01)(BlueSoul)(BoneSea)(upload)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","(BoomBone)(Shake)","",

            "(BoomBone)(Shake)","(BoomBone)(Shake)","(BoomBone)(Shake)","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)(GreenSoul)ScreenRot-","","","",   "","","","",

            "R1(BoundEaseBack)(Con)","","+0","",   "+01(Con)","","+0","",
            "R1(Con)","","+0","",   "+01(Con)","","+0","",
            "R1(Con)","","+0","",   "+01(Con)","","+0","",
            "R1(Con)","","+0","",   "+01(Con)","","+0","",

            "(BlueSoul2)(BoneSea2)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "ScreenRot-","","","",   "","","","",

            "(BoomBone2)(Shake)","","(BoomBone2)(Shake)","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","(BoomBone2)(Shake)","",

            "(BoomBone2)(Shake)","(BoomBone2)(Shake)","(BoomBone2)(Shake)","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)(ScreenRot+)","","(BoomBone2)(Shake)","",   "(BoomBone2)(GreenSoul)(Shake)","","","",

            "R(BoundEaseBack)(Con2)","","+01","",   "+0(Con2)","","+01","",
            "R(Con2)","","+01","",   "+0(Con2)","","+01","",
            "R(Con2)","","+01","",   "+0(Con2)","","+01","",
            "R(Con2)","","+01","",   "+0(Con2)","","+01","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                });
            }//ParaDOXXX vs zKronO!
            void Part6()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                RegisterFunctionOnce("RickRoll", () =>
                {
                    Heart.FixArrow = true;
                    ScreenDrawing.ScreenAngle += 90;
                    Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                });
                RegisterFunctionOnce("Scale", () =>
                {
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    ScreenDrawing.ScreenScale = 1f;
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Rand(480 - 20, 480 + 20), 20));
                    ce.Insert(BeatTime(4f), CentreEasing.Accerlating(new(0, 0), new(0, Rand(0.08f, 0.12f))));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(20, 40)));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(3));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Rand(480 - 20, 480 + 20), 20));
                    ce.Insert(BeatTime(4f), CentreEasing.Accerlating(new(0, 0), new(0, Rand(0.08f, 0.12f))));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(-40, -20)));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(3));
                });
                RegisterFunctionOnce("CentreLine1", () =>
                {
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, 135, BeatTime(2)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(135, 135 + 45f / 2f, BeatTime(0.5f)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(135 + 45f / 2f, 135 + 45f, BeatTime(0.5f)));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, 360 + 45, BeatTime(2)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(360 + 45, 360 + 45 - 45f / 2f, BeatTime(0.5f)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(360 + 45 - 45f / 2f, 360 + 45 - 45f, BeatTime(0.5f)));
                    Line l1 = new(CentreEasing.Stable(320, 240), ve1.GetResult());
                    l1.Alpha = 0.4f;
                    l1.DrawingColor = Color.Red;
                    Line l2 = new(CentreEasing.Stable(320, 240), ve2.GetResult());
                    l2.Alpha = 0.4f;
                    l2.DrawingColor = Color.Red;
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(2f, () => { l1.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); l2.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); });
                    DelayBeat(2.5f, () => { l1.AlphaIncrease(BeatTime(0.125f), 0.6f); l2.AlphaIncrease(BeatTime(0.125f), 0.6f); });
                    DelayBeat(2.625f, () => { l1.AlphaDecrease(BeatTime(0.65f), 1f); l2.AlphaDecrease(BeatTime(0.65f), 1f); });
                });
                RegisterFunctionOnce("CentreLine2", () =>
                {
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, -135, BeatTime(2)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(-135, -135 - 45f / 2f, BeatTime(0.5f)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(-135 - 45f / 2f, -135 - 45f, BeatTime(0.5f)));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, -360 - 45, BeatTime(2)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(-360 - 45, -360 - 45 + 45f / 2f, BeatTime(0.5f)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(-360 - 45 + 45f / 2f, -360 - 45 + 45f, BeatTime(0.5f)));
                    Line l1 = new(CentreEasing.Stable(320, 240), ve1.GetResult());
                    l1.Alpha = 0.4f;
                    l1.DrawingColor = Color.Red;
                    Line l2 = new(CentreEasing.Stable(320, 240), ve2.GetResult());
                    l2.Alpha = 0.4f;
                    l2.DrawingColor = Color.Red;
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(2f, () => { l1.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); l2.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); });
                    DelayBeat(2.5f, () => { l1.AlphaIncrease(BeatTime(0.125f), 0.6f); l2.AlphaIncrease(BeatTime(0.125f), 0.6f); });
                    DelayBeat(2.625f, () => { l1.AlphaDecrease(BeatTime(0.65f), 1f); l2.AlphaDecrease(BeatTime(0.65f), 1f); });
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutSine(new(0, 0), new(150, 0), BeatTime(1)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseInSine(new(150, 0), new(-5, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(2, () => { l.Dispose(); });
                });
                RegisterFunctionOnce("90Arrow1", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("901");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90Arrow2", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("902");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90Arrow3", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("903");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90", () =>
                {
                    Arrow[] ars = GetAll<Arrow>("901");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        ars[x].CentreRotationOffset = 90;
                    }
                    Arrow[] ars2 = GetAll<Arrow>("902");
                    for (int a = 0; a < ars2.Length; a++)
                    {
                        int x = a;
                        ars2[x].CentreRotationOffset = 90;
                    }
                    Arrow[] ars3 = GetAll<Arrow>("903");
                    for (int a = 0; a < ars3.Length; a++)
                    {
                        int x = a;
                        ars3[x].CentreRotationOffset = 90;
                    }
                });
                RegisterFunctionOnce("Mask", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(10), Color.Black, 0);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(0, ValueEasing.Stable(0));
                    v.Insert(BeatTime(2.25f), ValueEasing.EaseOutBack(0, 0.3f, BeatTime(2.25f)));
                    v.Insert(BeatTime(1.75f), ValueEasing.Stable(0.3f));
                    v.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(0.3f, 0.4f, BeatTime(0.5f)));
                    v.Insert(BeatTime(1.5f), ValueEasing.Stable(0.4f));
                    v.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(0.4f, 0.5f, BeatTime(0.5f)));
                    v.Insert(BeatTime(2), ValueEasing.Stable(0.5f));
                    v.Insert(BeatTime(0.75f), ValueEasing.EaseInQuad(0.5f, 0.99f, BeatTime(0.5f)));
                    v.Insert(BeatTime(0.4f), ValueEasing.Stable(0.99f));
                    v.Insert(BeatTime(0.35f), ValueEasing.EaseOutCubic(0.99f, 0, BeatTime(0.5f)));
                    v.Run((s) => { m.alpha = s; });
                    DelayBeat(10, () => { m.Dispose(); });
                    ValueEasing.EaseBuilder bd = new();
                    bd.Insert(BeatTime(2), ValueEasing.EaseInSine(ScreenDrawing.DownBoundDistance, 0, BeatTime(2)));
                    bd.Insert(0, ValueEasing.Stable(0));
                    bd.Run((x) => { ScreenDrawing.UpBoundDistance = x; ScreenDrawing.DownBoundDistance = x; });
                    ValueEasing.EaseBuilder scl = new();
                    scl.Insert(BeatTime(2), ValueEasing.Stable(1));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1, 1.04f, BeatTime(2)));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1.04f, 1.09f, BeatTime(2)));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1.09f, 1.15f, BeatTime(2)));
                    scl.Insert(BeatTime(0.5f), ValueEasing.Stable(1.15f));
                    scl.Insert(BeatTime(1.25f), ValueEasing.EaseOutSine(1.15f, 1.45f, BeatTime(1.25f)));
                    scl.Insert(BeatTime(1.5f), ValueEasing.EaseOutBack(1.45f, 1, BeatTime(1.5f)));
                    scl.Run((a) => { ScreenDrawing.ScreenScale = a; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), false);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), false);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(0, 0), new(240, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(-240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(-120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(0, 0), new(380, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), true);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), true);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(640, 0), new(400, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(640, 0), new(480, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(640, 0), new(260, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });

                RegisterFunctionOnce("H0", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 0, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("H2", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 2, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("H1", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 1, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });

                RegisterFunctionOnce("BoundOutQuad", () =>
                {
                    ScreenDrawing.BoundColor = Color.DarkRed * 0.7f;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 100, BeatTime(1)));
                    for (int a = 0; a < 48 * 2 - 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(100, 180, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(180, 100, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuart(100, 0, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                    ScreenDrawing.CameraEffect.Convulse(3, BeatTime(0.25f), RandBool());
                    ScreenDrawing.ScreenScale += 0.02f;
                });

                RegisterFunctionOnce("ScaleLerp", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(1), 1, 0.09f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 7f, new string[]
                {
            "!!3","R(90)","+21","+2",   "+21","","","",
            "R1","","","",   "R1","","","",
            "R","","","",   "^+21'1.3","","R","",
            "","","^+21'1.3","",   "R","","","",

            "(R)(LeftLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","","",   "D","","+0","",

            "(R)RightLine1","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+2","","+2","",
            "+2","","+11","",   "+202{Tap}(Scale)","","+212{Tap}","",

            "+202{Tap}(RickRoll)(Scale)","","+212{Tap}","",   "+202{Tap}(RickRoll)(Scale)","","+212{Tap}","",
            "+202{Tap}(RickRoll)(Scale)","","+212{Tap}","",   "+202{Tap}(RickRoll)(ScaleBack)","","+212{Tap}","",
            "Mask","","","",   "","","","",
            "","","","",   "","","","",
            //
            "$0{Down,v}(Line1)","","$0{v}(Line1)","",   "$0{Up,v}(Line1)","","","",
            "","","","",   "$0{v}","","","",
            "$01{Down,v}(Line2)","","$01{v}(Line2)","",   "$01{Up,v}(Line2)","","","",
            "","","","",   "$01{v}","","","",

            "$21{Down,v}(Line1)","","$21{v}(Line1)","",   "$21{Up,v}(Line1)","","","",
            "","","","",   "","","","",
            "$21{v}(Line2)","","","",   "","","$21{v}(Line2)","",
            "","","","",   "","","","(90Arrow1)",

            "(#0.8#R)(CentreLine1)(Line3)(+0{901})","","","",   "","","","",
            "","","","",   "R","","","",
            "R","","","",   "+0","","","",
            "","","","",   "","","","(90Arrow2)",

            "(#0.8#R1)(CentreLine2)(Line3)(+01{902})","","","",   "","","","",
            "","","","",   "R1","","","",
            "R1","","","",   "+01","","","",
            "","","","",   "","","","(90Arrow3)",
            //
            "(#0.8#R)(CentreLine1)(Line3)(+0{903})","","","",   "","","","",
            "","","","",   "R","","","",
            "R","","","",   "+0","","","",
            "","","","",   "","","","",

            "R","","","",   "R1","","","",
            "R","","","",   "($3'1.3)(+01'1.3)(Flicker)","","(+0'1.3)(+01'1.3)(Flicker)","",
            "($0'1.3)(+2'1.3)(Flicker)","($31'1.3)($11'1.3)(Flicker)","($0'1.3)(+2'1.3)(Flicker)","",   "($0'1.3)(+2'1.3)(Flicker)","","($0'1.3)(+2'1.3)(Flicker)","",
            "($0'1.3)(+2'1.3)(Flicker)","($31'1.3)($11'1.3)(Flicker)","($0'1.3)(+2'1.3)(Flicker)","",   "($0'1.3)(+2'1.3)(Flicker)","","($0'1.3)(+2'1.3)(Flicker)","",

            "($0'1.3)(+2'1.3)(BoundOutQuad)(Flicker)(ScaleLerp)","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","","",
            "","","R","",   "R","","","",

            "","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",


                });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
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
            //
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "(^R02'1.3{Tap})(^D12'1.3{Tap})","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
                });
            }//Tlott's turn!

            void Part7()
            {
                RegisterFunctionOnce("H3", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 3, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.85f);
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("soulB", () =>
                {
                    HeartAttribute.Speed = 3.4f;
                    TextPrinter t = new(BeatTime(8), "$Press [;] or \n[Down] to \nDown!", new Vector2(30, 160), new TextAttribute[]
                    {

                new TextSpeedAttribute(114),
                        /*
                                    })
                                    { sound=false};
                                    CreateEntity(t);
                                    SetSoul(2);
                                    InstantSetBox(new Vector2(320, 240), 180, 500);
                                    float y = 60;
                                    ForBeat(14 * 3, () =>
                                    {
                                        InstantTP(new(Heart.Centre.X, y));
                                        HeartAttribute.Gravity = 0;

                                        HeartAttribute.JumpTimeLimit = 0;
                                        if (GameStates.IsKeyDown(InputIdentity.MainDown))
                                        {
                                            y += 0.3f;
                                        }
                                        if (GameStates.IsKeyDown(InputIdentity.MainUp))
                                        {
                                            y -= 0.3f;
                                        }

                                    });
                                    ForBeat(14 * 3, () =>
                                    {
                                        StepSample.CentreX = Heart.Centre.X;
                                        StepSample.CentreY = Heart.Centre.Y;
                                    });
                                    ScreenDrawing.ScreenScale = 1f;
                                    StepSample.Intensity = 0.1f;
                                    HeartAttribute.Speed = 2.1f;
                                    splitter.Intensity = 1 + 2f;
                                });
                                RegisterFunctionOnce("Attack", () =>
                                {
                                    float height1 = 0;
                                    float height2 = 0;
                                    ValueEasing.EaseBuilder hf = new();
                                    ValueEasing.EaseBuilder sinf = new();
                                    hf.Insert(0, ValueEasing.Stable(0));
                                    hf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                                    hf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                                    hf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                                    hf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                                    hf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                                    hf.Insert(BeatTime(4), ValueEasing.SinWave(35, BeatTime(2), -1));
                                    hf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                                    hf.Insert(0, ValueEasing.Stable(0));
                                    hf.Run((s) =>
                                    {
                                        height1 = s;
                                    });
                                    sinf.Insert(0, ValueEasing.Stable(0));
                                    sinf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                                    sinf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                                    sinf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                                    sinf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                                    sinf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                                    sinf.Insert(BeatTime(4), ValueEasing.SinWave(35,BeatTime(2),0));
                                    sinf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                                    sinf.Insert(0, ValueEasing.Stable(0));
                                    sinf.Run((s) =>
                                    {
                                        height2 = s;
                                    });
                                    for (int i = 0; i < 14 * 4 * 14.2f; i++)
                                    {
                                        AddInstance(new InstantEvent(2*i, () => 
                                        {
                                            CreateBone(new LeftBone(true, 9, height1) { ColorType = 0, MarkScore = false });
                                            CreateBone(new RightBone(true, 9, height2) { ColorType = 0, MarkScore = false });
                                        }));
                                    }
                                    DelayBeat(5 * 4, () =>
                                    {
                                        for (int i = 0; i < 8; i++)
                                        {
                                            DelayBeat(i, () =>
                        */
                    })
                    { PlaySound = false };
                    CreateEntity(t);
                    SetSoul(2);
                    InstantSetBox(new Vector2(320, 240), 180, 500);
                    float y = 60;
                    ForBeat(13 * 4, () =>
                    {
                        InstantTP(new(Heart.Centre.X, y));
                        HeartAttribute.Gravity = 0;
                        HeartAttribute.JumpTimeLimit = 0;
                        if (GameStates.IsKeyDown(InputIdentity.MainDown))
                        {
                            y += 0.3f;
                        }
                        if (GameStates.IsKeyDown(InputIdentity.MainUp))
                        {
                            y -= 0.3f;
                        }

                    });
                    ForBeat(13 * 4, () =>
                    {
                        StepSample.CentreX = Heart.Centre.X;
                        StepSample.CentreY = Heart.Centre.Y;
                    });
                    ScreenDrawing.ScreenScale = 1f;
                    StepSample.Intensity = 0.1f;
                    HeartAttribute.Speed = 2.1f;
                    splitter.Intensity = 1 + 2f;
                });
                RegisterFunctionOnce("Attack", () =>
                {
                    float height1 = 0;
                    float height2 = 0;
                    ValueEasing.EaseBuilder hf = new();
                    ValueEasing.EaseBuilder sinf = new();
                    hf.Insert(0, ValueEasing.Stable(0));
                    hf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                    hf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                    hf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                    hf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                    hf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                    hf.Insert(BeatTime(4), ValueEasing.SinWave(35, BeatTime(2), -1));
                    hf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                    hf.Insert(0, ValueEasing.Stable(0));
                    hf.Run((s) =>
                    {
                        height1 = s;
                    });
                    sinf.Insert(0, ValueEasing.Stable(0));
                    sinf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                    sinf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                    sinf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                    sinf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                    sinf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                    sinf.Insert(BeatTime(4), ValueEasing.SinWave(35, BeatTime(2), 0));
                    sinf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                    sinf.Insert(0, ValueEasing.Stable(0));
                    sinf.Run((s) =>
                    {
                        height2 = s;
                    });
                    for (int i = 0; i < 14 * 4 * 14.2f; i++)
                    {
                        AddInstance(new InstantEvent(2 * i, () =>
                        {
                            CreateBone(new LeftBone(true, 9, height1) { ColorType = 0, MarkScore = false });
                            CreateBone(new RightBone(true, 9, height2) { ColorType = 0, MarkScore = false });
                        }));
                    }
                    DelayBeat(5 * 4, () =>
                    {
                        for (int i = 0; i < 8; i++)
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
                    DelayBeat(4, () => { CreateEntity(new Boneslab(180, 140, BeatTime(7 * 4), BeatTime(5 * 4)) { ColorType = 0 }); });
                });
                RegisterFunctionOnce("LeftB", () =>
                {
                    float rd = Rand(1, 2);
                    if (rd == 1)
                    {
                        DrawingUtil.CrossBone(new Vector2(Rand(320 - 50, 320 + 10), 500), new Vector2(0, -8), 30, 1, 2);
                    }
                    else if (rd == 2)
                    {
                        CreateBone(new CustomBone(new Vector2(Rand(320 - 50, 320 + 10), 500), Motions.PositionRoute.linear, 0, 30)
                        {
                            PositionRouteParam = new float[] { 0, -7 },
                            ColorType = 0
                        });
                    }
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("Kick1", () =>
                {
                    float rot = Rand(9, 20);
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot)), rot, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270)), 180, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot)), -rot, 40) { ColorType = 0 });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("Kick2", () =>
                {
                    float rot = Rand(9, 20);
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot / 2)), rot / 2, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot / 2)), -rot / 2, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot * 1.5f)), rot * 1.5f, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot * 1.5f)), -rot * 1.5f, 40) { ColorType = 0 });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("RightB", () =>
                {
                    PlaySound(Sounds.pierce);
                    float rd = Rand(1, 2);
                    if (rd == 1)
                    {
                        DrawingUtil.CrossBone(new Vector2(Rand(320 - 10, 320 + 50), 500), new Vector2(0, -8), 30, 1, 2);
                    }
                    else if (rd == 2)
                    {
                        CreateBone(new CustomBone(new Vector2(Rand(320 - 10, 320 + 50), 500), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 0, -7 },
                            ColorType = 0
                        });
                    }
                });//REMEMBER to ADD these STRING INTO the STRING which under this WORD
                RegisterFunctionOnce("Sounds", () =>
                {
                    for (int a = 0; a < 2; a++) PlaySound(Sounds.destroy);
                });
                RegisterFunctionOnce("FirstBone", () =>
                {
                    for (int a = 0; a < 8; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 540 + a * 27));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -Rand(4.00f, 8.00f)), new(0, -Rand(0.040f, 0.090f))));
                        CreateBone(new CustomBone(new(0, 0), ce.GetResult(), 90 + Rand(-30, 30), BoxStates.Width + 80) { ColorType = 2 });
                    }

                });
                CreateChart(BeatTime(4), BeatTime(1), 7f, new string[]
                {
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","","",
            "","","R","",   "R","","","",

            "","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "($0)($2)(Flicker)","","","",   "","","($0)($2)(Flicker)","",
            "","","","",   "($0)($2)(Flicker)","","","",
            //HALLLLLLLLLLLLLLLLLLLLLL
            "(soulB)(Flicker)(Attack)(Sounds)(FirstBone)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(LeftB)","","","",   "(LeftB)(Kick1)","","","",
            "(LeftB)","","(LeftB)","",   "(LeftB)(Kick2)","","","",
            "(LeftB)","","","",   "(LeftB)(Kick1)","","(LeftB)","",
            "","","(LeftB)","",   "(LeftB)(Kick2)","","","",
            //
            "(LeftB)","","","",   "(LeftB)(Kick1)","","","",
            "(LeftB)","","","",   "(LeftB)(Kick2)","","","",
            "(LeftB)","","","",   "(LeftB)(Kick1)","","(LeftB)","",
            "","","(LeftB)","",   "(LeftB)(Kick2)","","(LeftB)","",

            "(RightB)","","","",   "(RightB)(Kick1)","","","",
            "(RightB)","","","",   "(RightB)(Kick2)","","","",
            "(RightB)","","","",   "(RightB)(Kick1)","","(RightB)","",
            "","","(RightB)","",   "(RightB)(Kick2)","","","",

            "(RightB)","","","",   "(RightB)(Kick1)","","","",
            "(RightB)","","","",   "(RightB)(Kick2)","","","",
            "(RightB)","","","",   "(RightB)(Kick1)","","(RightB)","",
            "","","(RightB)","",   "(RightB)(Kick2)","","(RightB)","",

            "","","","",   "Kick1","","","",
            "","","","",   "(Kick2)","","","",
            "","","","",   "Kick1","","","",
            "","","","",   "(Kick2)","","","",

            "","","","",   "Kick1","","","",
            "","","","",   "(Kick2)","","","",
            "","","","",   "Kick1","","","",
            "","","","",   "(Kick2)","","","",
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "(^R02'1.3{Tap})(^D12'1.3{Tap})","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            //
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "(R02{Tap})(D12{Tap})","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",

            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "(R12{Tap})(TapEvent)","","","",
            "","","","",   "($11)(TapEvent)","","","",
            "","","","",   "($31)(TapEvent)","","","",
                });
            }
            void Part8()
            {
                RegisterFunctionOnce("GB", () =>
                {

                    int non = Rand(1, 2);
                    CreateEntity(new NormalGB(new(Heart.Centre.X, 440), new(Heart.Centre.X, 440), new(0.75f, 0.375f), 270, BeatTime(4), BeatTime(0.3f)) { AppearVolume = 0.01f });
                    for (int i = 0; i < 3; i++)
                    {
                        AddInstance(new InstantEvent(i * 2f, () =>
                        {
                            CreateBone(new CustomBone(new(320, 500), Motions.PositionRoute.linear, 90, 180)
                            {
                                ColorType = 2,
                                PositionRouteParam = new float[] { 0, -9 }
                            });
                        }));
                    }
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    float length = 0;
                    ValueEasing.EaseBuilder len = new();
                    len.Insert(0, ValueEasing.Stable(80));
                    len.Insert(BeatTime(0.5f), ValueEasing.Linear(80, 120, BeatTime(0.5f)));
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
                    len.Insert(BeatTime(0.5f), ValueEasing.Linear(80, 120, BeatTime(0.5f)));
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
                RegisterFunctionOnce("ChangeA", () =>
                {
                    SetBox(120, 180, 500);
                    DelayBeat(0, () =>
                    {
                        HeartAttribute.Gravity = 9.8f;
                        HeartAttribute.JumpTimeLimit = 1;
                        Heart.GiveForce(0, 8);
                        DelayBeat(0.5f, () =>
                        {
                            DrawingUtil.CrossBone(new(320 - 90, 380), new(4, 0), 160, 2, 2);
                            DrawingUtil.CrossBone(new(320 + 90, 380), new(-4, 0), 160, 2, 2);
                            PlaySound(Sounds.pierce);
                        });
                    });
                    CreateEntity(new Boneslab(0, 30, BeatTime(1.4f), BeatTime(1)));
                    DrawingUtil.MaskSquare s = new(0, 0, 640, 480, BeatTime(0.9f), Color.Black, 1);
                    DelayBeat(1.5f, () => { TP(320, 240); CreateEntity(s); PlaySound(Sounds.change); DelayBeat(0.5f, () => { s.Dispose(); }); });
                });
                RegisterFunctionOnce("ChangeB", () =>
                {
                    float co1 = 0;
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(BeatTime(2), ValueEasing.Stable(0));
                    a.Insert(1, ValueEasing.Linear(0, 1, 1));
                    a.Run((s) => { co1 = s; });
                    InstantSetBox(new Vector2(320, 240), 160, 160);
                    SetSoul(0);
                    CreateBone(new CentreCircleBone(Rand(0, 359), 6.5f, 140, BeatTime(2)) { ColorType = 1 });
                    CreateBone(new CentreCircleBone(LastRand + 90, -5f, 140, BeatTime(2 * 3)) { ColorType = 1 });
                    for (int i = 0; i < 36; i++)
                    {
                        CreateBone(new SideCircleBone(i * 10, -6, 20, BeatTime(3 * 4 - 2)));
                    }
                    DelayBeat(6, () =>
                    {
                        float rotation = 0;
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(0, ValueEasing.Stable(30));
                        rot.Insert(BeatTime(4), ValueEasing.Linear(30, 390, BeatTime(4)));
                        rot.Run((s) => { rotation = s; });
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(i * 0.5f, () =>
                            {
                                CreateEntity(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(200, rotation), new Vector2(320, -20), new(0.875f, 0.625f), rotation + 180, BeatTime(4), BeatTime(1)));
                            });
                        }
                    });//不要让你的恶习复苏

                });
                RegisterFunctionOnce("SetSoulR", () =>
                {
                    splitter.Intensity = 0f;
                    StepSample.CentreX = 320;
                    StepSample.CentreY = 240;
                    StepSample.Intensity = 0.1f;
                    InstantSetBox(new Vector2(320, 240), 170, 170);
                    SetSoul(0);
                    Heart.Speed = 3f;
                    ScreenDrawing.ScreenScale = 1;
                });
                RegisterFunctionOnce("CentreBone", () =>
                {
                    CentreCircleBone c = new(Rand(0, 359), 4.5f, 180, BeatTime(9)) { ColorType = 1, IsMasked = true };
                    CreateBone(c);
                    DelayBeat(5, () =>
                    {
                        c.ColorType = 2;
                        PlaySound(Sounds.Ding);
                        c.RotateSpeed = 4;
                    });
                });
                float rot = 0;
                RegisterFunctionOnce("Value", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                    ve.Insert(BeatTime(5), ValueEasing.Linear(2));
                    ve.Insert(BeatTime(4), ValueEasing.Linear(-3.5f));
                    ve.Run((s) => { rot = s; });
                    ForBeat(13, () =>
                    {
                        Heart.Speed = GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainUp)
                            ? 3f * 1.414f
                            : GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainUp)
                                ? 3f * 1.414f
                                : GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainDown)
                                                            ? 3f * 1.414f
                                                            : GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight) ? 3f * 1.414f : 3f;
                    });
                });
                RegisterFunctionOnce("CreateGB1", () =>
                {
                    /*
                                    if (GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainUp))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainUp))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainDown))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else
                                    {
                                        Heart.Speed = 3f;
                                    }*/

                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(200, rot), new Vector2(320, 240) + MathUtil.GetVector2(300, rot), new(1, 0.5f), rot + 180, BeatTime(2), BeatTime(0.34f)) { AppearVolume = 0 });

                });
                RegisterFunctionOnce("CreateGB2", () =>
                {
                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(170, rot), new Vector2(320, 240) + MathUtil.GetVector2(300, rot), new(1, 0.5f), rot + 180, BeatTime(2), BeatTime(0.25f)) { AppearVolume = 0 });
                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(170, rot + 90), new Vector2(320, 240) + MathUtil.GetVector2(300, rot + 90), new(1, 0.5f), rot + 180 + 90, BeatTime(2), BeatTime(0.25f)) { AppearVolume = 0 });
                });
                RegisterFunctionOnce("Return", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0.1f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0.1f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    TP();
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "GB","","","",   "GB","","","",
            "GB","","","",   "GB","","","",
            "GB","","","",   "GB","","","",
            "GB","","","",   "GB","","","",

            "GB","","","",   "GB","","","",
            "GB","","","",   "GB","","","",
            "GB","","","",   "GB","","","",
            "GB","","","",   "GB","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",

            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "ChangeA","","","",   "","","","",
            "(Value)","","","",   "","","","",
            "SetSoulR(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB1)(CentreBone)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            //
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",

            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "(Return)","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","","",
            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "","","","",   "","","","",

                });
            }//Tlott's turn!
            #endregion
            #region Nor
            void NorPart1()
            {
                ScreenDrawing.UISettings.CreateUISurface();
                DelayBeat(4, () =>
                {
                    CentreEasing.EaseBuilder e1 = new();
                    CentreEasing.EaseBuilder e2 = new();
                    CentreEasing.EaseBuilder e3 = new();
                    CentreEasing.EaseBuilder e4 = new();
                    e1.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(320, 240 - 340), game.BeatTime(2)));
                    e2.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(320, 240 + 340), game.BeatTime(2)));
                    e3.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(660, 240), game.BeatTime(2)));
                    e4.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(-20, 240), game.BeatTime(2)));
                    Line a = new(e1.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.55f };
                    Line b = new(e2.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.55f };
                    Line c = new(e3.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    Line d = new(e4.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    CreateEntity(c);
                    CreateEntity(d);
                    DelayBeat(4, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                        c.Dispose();
                        d.Dispose();
                    });
                    LineShadow(3, 0.9f, 9, a);
                    LineShadow(3, 0.9f, 9, b);
                    LineShadow(3, 0.9f, 9, c);
                    LineShadow(3, 0.9f, 9, d);
                });
                RegisterFunctionOnce("lineL", () =>
                {
                    CentreEasing.EaseBuilder ease = new();
                    ease.Insert(0, CentreEasing.Stable(new(840, 240)));
                    ease.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(840, 240), new(-20, 240), BeatTime(2)));
                    Line ce = new(ease.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(ce);

                    DelayBeat(4, () =>
                    {
                        ce.Dispose();
                    });
                });
                RegisterFunctionOnce("lineR", () =>
                {
                    CentreEasing.EaseBuilder ease = new();
                    ease.Insert(0, CentreEasing.Stable(new(-200, 240)));
                    ease.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(-200, 240), new(660, 240), BeatTime(2)));
                    Line ce = new(ease.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(ce);

                    game.DelayBeat(4, () =>
                    {
                        ce.Dispose();
                    });
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(-3, game.BeatTime(2));
                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(3, game.BeatTime(2));
                });
                RegisterFunctionOnce("ShiningSoul", () => { SetSoul(1); });
                RegisterFunctionOnce("Bound", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ValueEasing.EaseBuilder vea = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    vea.Insert(game.BeatTime(32), ValueEasing.Stable(0.2f));
                    vea.Insert(game.BeatTime(16), ValueEasing.EaseOutSine(0.4f, 0.99f, game.BeatTime(16)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutCubic(0, 80, game.BeatTime(4)));
                    //ve.Insert(game.BeatTime(28), ValueEasing.Stable(28));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(80, 120, game.BeatTime(4)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(120, 160, game.BeatTime(4)));
                    for (int i = 0; i < 16; i++)
                    {
                        ve.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutBack(160 + i * 12, 172 + i * 12, game.BeatTime(0.5f)));
                    }
                    ve.Insert(0, ValueEasing.Stable(240));
                    ve.Run((s) =>
                    {
                        ScreenDrawing.UpBoundDistance = s;
                        ScreenDrawing.DownBoundDistance = s;
                    });
                    vea.Run((s) =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.DarkRed * 0.7f, s);
                    });
                });
                game.RegisterFunctionOnce("upload", () =>
                {
                    float z = Rand(0, 1);
                    if (z == 0)
                    {
                        CentreEasing.EaseBuilder v = new();
                        CentreEasing.EaseBuilder vb = new();
                        CentreEasing.EaseBuilder va = new();
                        v.Insert(0, CentreEasing.Stable(new(320, 500)));
                        va.Insert(0, CentreEasing.Stable(new(0, 820)));
                        vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                        v.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(2)));
                        va.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(2)));
                        vb.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(2)));
                        Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                        Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                        CreateEntity(a);
                        CreateEntity(b);
                        LineShadow(3, 0.9f, 4, a);
                        LineShadow(3, 0.9f, 4, b);
                        game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                        ValueEasing.EaseBuilder bl = new();
                        bl.Insert(0, ValueEasing.Stable(0));
                        bl.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 8, BeatTime(0.25f)));
                        bl.Insert(BeatTime(2.75f), ValueEasing.EaseOutCubic(8, 0, BeatTime(2.75f)));
                        bl.Run((x) =>
                        {
                            Blur.Sigma = x * 2;
                            splitter.Intensity = 1 + x * 0.1f;
                            StepSample.Intensity = 0.01f + x * 0.01f;
                        });
                    }
                    else if (z == 1)
                    {
                        CentreEasing.EaseBuilder v = new();
                        CentreEasing.EaseBuilder vb = new();
                        CentreEasing.EaseBuilder va = new();
                        v.Insert(0, CentreEasing.Stable(new(320, 500)));
                        va.Insert(0, CentreEasing.Stable(new(0, 820)));
                        vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                        v.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(2)));
                        va.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(2)));
                        vb.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(2)));
                        Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                        Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                        CreateEntity(a);
                        CreateEntity(b);
                        LineShadow(3, 0.9f, 4, a);
                        LineShadow(3, 0.9f, 4, b);
                        game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                        ValueEasing.EaseBuilder bl = new();
                        bl.Insert(0, ValueEasing.Stable(0));
                        bl.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 8, BeatTime(0.25f)));
                        bl.Insert(BeatTime(2.75f), ValueEasing.EaseOutCubic(8, 0, BeatTime(2.75f)));
                        bl.Run((x) =>
                        {
                            Blur.Sigma = x * 2;
                            splitter.Intensity = 1 - x * 0.1f;
                            StepSample.Intensity = 0.01f + x * 0.01f;
                        });
                    }
                    //写个色散+模糊的blur
                });
                RegisterFunctionOnce("soulR", () => { SetSoul(0); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                game.RegisterFunctionOnce("convL", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(2, game.BeatTime(1), false);
                });
                game.RegisterFunctionOnce("convR", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(2, game.BeatTime(1), true);
                });
                RegisterFunctionOnce("soulG", () => { SetSoul(1); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder v = new();
                    ValueEasing.EaseBuilder vb1 = new();
                    ValueEasing.EaseBuilder vb2 = new();
                    ValueEasing.EaseBuilder co = new();
                    v.Insert(game.BeatTime(1), ValueEasing.EaseOutSine(0, 2.4f, game.BeatTime(1)));
                    v.Insert(game.BeatTime(1), ValueEasing.EaseInSine(2.4f, 0, game.BeatTime(1)));
                    vb1.Insert(0, ValueEasing.Stable(240));
                    vb1.Insert(BeatTime(2), ValueEasing.EaseInQuad(240, 0, BeatTime(2)));
                    vb1.Insert(BeatTime(1), ValueEasing.Stable(0));
                    vb2.Insert(0, ValueEasing.Stable(240));
                    vb2.Insert(BeatTime(2), ValueEasing.EaseInQuad(240, 0, BeatTime(2)));
                    vb2.Insert(BeatTime(1), ValueEasing.Stable(0));
                    co.Insert(BeatTime(2), ValueEasing.Linear(0.1f, 0.99f, BeatTime(2)));
                    co.Insert(BeatTime(4), ValueEasing.Stable(0.99f));
                    v.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s * 0.2f;
                        splitter.Intensity = 1 + s;
                    });
                    vb1.Run((s) => { ScreenDrawing.UpBoundDistance = s; });
                    vb2.Run((s) => { ScreenDrawing.DownBoundDistance = s; });
                    co.Run((s) => { ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.DarkRed, s); });
                    ScreenDrawing.CameraEffect.SizeShrink(0.45f, BeatTime(2));
                });
                RegisterFunctionOnce("WaveR", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(0, ValueEasing.Stable(0));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0, 7.2f, BeatTime(1)));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(7.2f, -3.6f, BeatTime(1)));
                    a.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-3.6f, 0, BeatTime(1.5f)));
                    a.Insert(0, ValueEasing.Stable(0));
                    a.Run((s) => { ScreenDrawing.ScreenAngle = s * 0.3f; });
                });
                RegisterFunctionOnce("wLineL", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, Rand(100, 300)));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -1), new(0, 0.2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(-32, -20)));
                    CreateEntity(l);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 0.5f, () =>
                        {
                            Line ls = l.Split(false);
                            CreateEntity(ls);
                            ls.Alpha = 0.7f;
                            ls.AlphaDecrease(BeatTime(3));
                        });
                    }
                    LineShadow(6, 0.4f, 8, l);
                });
                RegisterFunctionOnce("WaveL", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    ValueEasing.EaseBuilder alp = new();
                    a.Insert(0, ValueEasing.Stable(0));
                    alp.Insert(0, ValueEasing.Stable(0.85f));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0, -7.2f, BeatTime(1)));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(-7.2f, 3.6f, BeatTime(1)));
                    a.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(3.6f, 0, BeatTime(1.5f)));
                    a.Insert(0, ValueEasing.Stable(0));
                    alp.Insert(BeatTime(3), ValueEasing.EaseOutBounce(0.85f, 0, BeatTime(3)));
                    a.Run((s) => { ScreenDrawing.ScreenAngle = s * 0.3f; });
                });
                RegisterFunctionOnce("wLineR", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, Rand(100, 300)));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -1), new(0, 0.2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(180 + Rand(20, 32)));
                    CreateEntity(l);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 0.5f, () =>
                        {
                            Line ls = l.Split(false);
                            ls.Alpha = 0.7f;
                            CreateEntity(ls);
                            ls.AlphaDecrease(BeatTime(3));
                        });
                    }
                    LineShadow(6, 0.4f, 8, l);
                });
                RegisterFunctionOnce("RDcross", () =>
                {
                    float random = Rand(1, 4);
                    CentreEasing.EaseBuilder ce1 = new();
                    CentreEasing.EaseBuilder ce2 = new();
                    ValueEasing.EaseBuilder alp = new();
                    ce1.Insert(0, CentreEasing.Stable(new(320, 240)));
                    ce2.Insert(0, CentreEasing.Stable(new(320, 240)));
                    alp.Insert(0, ValueEasing.Stable(0.85f));
                    alp.Insert(BeatTime(2), ValueEasing.EaseOutSine(0.85f, 0, BeatTime(2)));
                    if (random == 1)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 0; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 0; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else if (random == 2)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else if (random == 3)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 90; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 90; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                });
                RegisterFunctionOnce("FakeNotes", () =>
                {
                    for (int a = 0; a < 5; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(6 + a * 0.1f, 0), new(-0.2f, 0.4f)));
                        FakeNote.LeftNote note = new(BeatTime(4), 6 + a * 0.1f, Rand(0, 1), 0, CentreEasing.Accerlating(new(9 + a * 1f, 0), new(-0.05f, 0.1f)), BeatTime(4));
                        note.Offset = new(Rand(-200, 0), Rand(-30, 30));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(4), ValueEasing.Stable(180));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(180, 180 + Rand(50, 90), BeatTime(4)));
                        ve.Run((s) => { note.Rotation = 180; });
                        CreateEntity(note);
                    }
                    for (int a = 0; a < 5; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(-6 + a * 0.1f, 0), new(0.2f, 0.4f)));
                        FakeNote.RightNote note = new(BeatTime(4), 6 + a * 0.1f, Rand(0, 1), 0, CentreEasing.Accerlating(new(-9 + a * 1f, 0), new(+0.05f, 0.1f)), BeatTime(4));
                        note.Offset = new(Rand(0, 200), Rand(-30, 30));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(4), ValueEasing.Stable(180));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(180, 180 + Rand(50, 90), BeatTime(4)));
                        ve.Run((s) => { note.Rotation = 0; });
                        CreateEntity(note);
                    }
                });
                CreateChart(BeatTime(4), BeatTime(1), 6f, new string[]
                {
            "(#3.5#$3)(<+0'0.8)(>+0'0.8)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N0)(WaveR)(wLineR)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N3)(WaveR)(wLineR)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N2)(WaveL)(wLineL)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "(#3.5#N1)(WaveL)(wLineL)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N0)(WaveR)(wLineR)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N3)(WaveR)(wLineR)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(#3.5#N2)(WaveL)(wLineL)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","(RotateL)","",
            //
            "(R)(Bound)(lineL)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateR)","",
            "(R)(lineR)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateR)","",

            "(R)(lineR)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateL)","",
            "(R)(lineL)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateL)","",

            "(R)(lineL)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateR)","",
            "(R)(lineR)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateR)","",

            "(R)(lineR)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateL)","",
            "(R)(lineL)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateL)","",
            //
            "(R)(lineL)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateR)","",
            "(R)(lineR)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateR)","",

            "(R)(lineR)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateL)","",
            "(R)(lineL)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateL)","",

            "(R)(lineL)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateR)","",
            "(R)(lineR)","","+0","",    "+0","","","",
            "R(RDcross)","","","",    "R","","(RotateR)","",

            "(R)(lineR)","","","",    "R","","+0","",
            "+0(RDcross)","","","",    "D","","(RotateL)","",
            "(R'1.3)(+01'0.9)(lineL)","","","",    "(D'0.9)(+01'1.3)","","","",
            "(R'1.3)(+01'0.9)(RDcross)","","","",    "(D'0.9)(+01'1.3)","","","",
            //
            "(upload)(soulR)(convL)","","","",   "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "(upload)(soulG)(convR)","","","",   "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",

            "(upload)(^R)(^+01)","","","",    "(^R)(^+01)","","","",
            "(^R)(^+01)","","","",    "(^R)(^+01)","","","",
            "(^R)(^+01)(FakeNotes)","","","",    "(^R)(^+01)","","","",
            "(^R)(^+01)","","","",    "(^R)(^+01)","","","",

            "(upload)($0)","","(+2)","",    "(+2)","","+2","",
            "($0)($21)","($0)($21)","($0)($21)","($0)($21)",    "($0)($21)","($0)($21)","($0)($21)","($0)($21)",
            "Blur1","","","",   "","","","",
            "","","","",   "","","","",
                });
            }//zKronO's turn!
            void NorPart2()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                RegisterFunctionOnce("soulR", () => { SetSoul(0); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("soulB", () => { SetSoul(2); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Change", () =>
                {
                    game.DelayBeat(3, () => { SetBox(new Vector2(320, 240), 240, 240); });
                });
                RegisterFunctionOnce("atk1", () =>
                {
                    game.DelayBeat(0, () =>
                    {
                        Heart.GiveForce(180, 8);
                        CreateEntity(new Boneslab(0, 160, game.BeatTime(0.5f), game.BeatTime(4)));
                        CreateEntity(new Boneslab(90, 100, game.BeatTime(0.5f), game.BeatTime(2)));
                        CreateEntity(new Boneslab(270, 100, game.BeatTime(0.5f), game.BeatTime(2)));
                    });
                    game.DelayBeat(2.5f, () =>
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(i * 0.5f, () =>
                            {
                                UpBone b1 = new(true, 4, 80) { ColorType = Rand(1, 2) };
                                UpBone b2 = new(false, 4, 80) { ColorType = Rand(1, 2) };
                                CreateBone(b1);
                                CreateBone(b2);
                                AddInstance(new TimeRangedEvent(1145, () =>
                                {
                                    b1.Speed -= 0.075f;
                                    b2.Speed -= 0.075f;
                                }));
                            });
                        }
                    });
                    game.DelayBeat(4.5f, () =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            game.DelayBeat(i * 0.5f, () =>
                            {
                                CreateBone(new UpBone(true, 5, 20));
                                CreateBone(new UpBone(false, 5, 20));
                            });
                        }
                    });
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    Heart.GiveForce(0, 12);
                    CreateEntity(new Boneslab(180, 160, game.BeatTime(2), game.BeatTime(4)));
                    for (int i = 0; i < 4; i++)
                    {
                        game.DelayBeat(i * 1, () =>
                        {
                            CreateEntity(new Boneslab(0, 15, game.BeatTime(2), game.BeatTime(0.23f)));
                            game.DelayBeat(2, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(new CustomBone(new(320 - 120, 240 + 120 - 55), Motions.PositionRoute.linear, -30, 280)
                                {
                                    PositionRouteParam = new float[] { 12, 0 },
                                    ColorType = 2
                                });
                                CreateBone(new CustomBone(new(320 - 120 - 14, 240 + 120 - 55), Motions.PositionRoute.linear, -30, 280)
                                {
                                    PositionRouteParam = new float[] { 12, 0 },
                                    ColorType = 2
                                });
                                CreateBone(new CustomBone(new(320 + 120, 240 + 120 - 55), Motions.PositionRoute.linear, 30, 280)
                                {
                                    PositionRouteParam = new float[] { -12, 0 },
                                    ColorType = 2
                                });
                                CreateBone(new CustomBone(new(320 + 120 + 14, 240 + 120 - 55), Motions.PositionRoute.linear, 30, 280)
                                {
                                    PositionRouteParam = new float[] { -12, 0 },
                                    ColorType = 2
                                });
                            });
                        });
                    }
                    game.DelayBeat(6, () =>
                    {
                        SetSoul(0);
                        ValueEasing.EaseBuilder v = new();
                        v.Insert(game.BeatTime(1.5f), ValueEasing.EaseInCubic(0, 110, game.BeatTime(1.5f)));
                        v.Insert(game.BeatTime(1.5f), ValueEasing.EaseOutQuart(110, 0, game.BeatTime(1.5f)));
                        v.Run((s) =>
                        {
                            InstantSetBox(new Vector2(320, 240), 240 - s, 240 - s);
                        });
                        for (int i = 0; i < 36; i++)
                        {
                            SideCircleBone b = new(i * 10, 8, 50, game.BeatTime(2.85f));
                            CreateBone(b);
                        }

                    });
                    game.DelayBeat(9, () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(270, 12);
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(i * 2 + 1, () =>
                            {
                                CreateBone(new LeftBone(true, 3, 160 + Rand(0, 30)));
                                CreateBone(new RightBone(true, 3, 30 - LastRand));
                                PlaySound(Sounds.pierce);
                            });
                            game.DelayBeat(i * 2 + 2, () =>
                            {
                                CreateBone(new LeftBone(false, 3, 160 + Rand(0, 30)));
                                CreateBone(new RightBone(false, 3, 30 - LastRand));
                                PlaySound(Sounds.pierce);
                            });
                        }
                    });
                });
                RegisterFunctionOnce("atk3", () =>
                {
                    SetSoul(0);
                    Heart.GiveInstantForce(0, 0);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(game.BeatTime(1.5f), ValueEasing.EaseInCubic(0, 90, game.BeatTime(1.5f)));
                    v.Insert(game.BeatTime(1.5f), ValueEasing.EaseOutQuart(90, 0, game.BeatTime(1.5f)));
                    v.Run((s) =>
                    {
                        InstantSetBox(new Vector2(320, 240), 240 - s, 240 - s);
                    });
                    for (int i = 0; i < 36; i++)
                    {
                        SideCircleBone b = new(i * 10, 8, 50, game.BeatTime(3));
                        CreateBone(b);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        CreateBone(new CentreCircleBone(90 * i + 140, 2f, 300, game.BeatTime(3)) { IsMasked = true }); ;

                    }
                    game.DelayBeat(6.75f, () =>
                    {
                        SetGreenBox();
                    });
                });
                RegisterFunctionOnce("Bones1", () =>
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new LeftBone(true, 6, 70));
                    CreateBone(new RightBone(false, 6, 70));
                });
                RegisterFunctionOnce("Scale+", () =>
                {
                    ScreenDrawing.ScreenScale += 0.05f;
                });
                RegisterFunctionOnce("Scale++", () =>
                {
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("ScaleRet", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(2), 1, 0.07f);
                });
                RegisterFunctionOnce("RandomSniperBone", () =>
                {

                    for (int a = 0; a < 1; a++)
                    {
                        float rot = Rand(10, 80);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(BoxStates.Left - 40, BoxStates.Up - 40));
                        ce.Insert(BeatTime(4), CentreEasing.Linear(MathUtil.GetVector2(6.5f, rot)));
                        CustomBone cb = new(new(0, 0), ce.GetResult(), rot + 90, 35);
                        CreateBone(cb);
                    }

                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("soulG", () => { SetSoul(1); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Bound1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(0, 30, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(30, 10, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(10, 40, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(40, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(20, 50, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(50, 30, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(30, 60, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(60, 40, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(40, 70, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(70, 50, BeatTime(0.25f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = s + 90; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), false);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), false);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(0, 0), new(240, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(-240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(-120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(0, 0), new(380, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), true);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), true);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(640, 0), new(400, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(640, 0), new(480, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(640, 0), new(260, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("LeftLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(660, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.5f));
                    LineShadow(3, 0.4f, 2, l);
                });
                RegisterFunctionOnce("RightLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(-20, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.5f));
                    LineShadow(3, 0.4f, 2, l);
                });
                RegisterFunctionOnce("MidLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(360, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1f));
                    LineShadow(3, 0.4f, 2, l);
                    l.TransverseMirror = true;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 1.1f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseInQuad(1.1f, 1f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                });
                RegisterFunctionOnce("MidOutLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(320, 0), new(660, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1f));
                    LineShadow(3, 0.1f, 3, l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("MakeF", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                });
                RegisterFunctionOnce("MakeF2", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "Change","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "soulB","","","",   "atk1","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "atk2","","","",
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

            "","","","",   "","","atk3","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "RandomSniperBone","","","",   "RandomSniperBone","","","",
            "RandomSniperBone","","RandomSniperBone","",   "","","RandomSniperBone","",
            "","","RandomSniperBone","",   "","","RandomSniperBone","",
            "","","RandomSniperBone","",   "RandomSniperBone","soulG","","",
            //green
            "(R)(LeftLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","","",   "D","","+0","",

            "(R)(RightLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","+0","",   "+0(LeftLine2)","","+0","",

            "(R)(LeftLine2)","","","",   "D","","+2","",
            "+2","","+1","",   "(RightLine2)","","(D)","",
            "(RightLine2)","","(D)","",   "+0","","+0","",
            "+0","","","",   "(D)","","+0","",

            "(+0)MidLine","","","",   "D","","+2","",
            "+2","","+2","",   "","","(R)","",
            "","MakeF","(R1)(+0)Scale+","MakeF",   "($3)($31)Scale+","MakeF","(>$3'1.5)(<$31'1.3)Scale+","MakeF",
            "($2)($21)Scale+","MakeF","(>$1'1.3)(<$11'1.3)Scale+","MakeF",   "($1)($11)Scale+","MakeF","(>$0'1.3)(<$01'1.3)Scale+","MakeF",
            //
            "D(RightLine1)(ScaleRet)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","","",   "+0","","+0","",

            "(R)(LeftLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","D","",   "+0","","+0","",
            "+0","","+0","",   "+0(RightLine2)","","+0","",

            "(R)(RightLine2)","","","",   "D","","+2","",
            "+2","","+2","",   "(LeftLine2)","","(D)","",
            "(LeftLine2)","","(D)","",   "D","","+0","",
            "+0","","","",   "D","","+2","",

            "(+2)MidLine","","","",   "R","","","",
            "+2","","+2","",   "","","","MakeF2",
            "($0'1.3)(+2'1.3)(MidOutLine)(Scale++)","","","MakeF2",   "($0'1.3)(+2'1.3)(MidOutLine)(Scale++)","","","MakeF2",
            "($0'1.3)(+2'1.3)(MidOutLine)(Scale++)","","","MakeF2",   "($0'1.3)(+2'1.3)(MidOutLine)(Scale++)","","","",
            "($0'1.3)(+2'1.3)(MidOutLine)(ScaleRet)(MakeF2)","","","",
                    //
                });
            }//ParaDOXXX's turn!
            void NorPart3()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                ScreenDrawing.BoundColor = Color.DarkRed * 0.6f;
                RegisterFunctionOnce("BoundStart", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0, 60, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                });
                RegisterFunctionOnce("Bound1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(60));
                    for (int a = 0; a < 8 * 8 - 8; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(60 + a * 2, 120 + a * 2, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutSine(120 + a * 2, 60 + (a + 1) * 2, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(60 + 56 * 2, 0, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(56), ValueEasing.Linear(0, 1f, BeatTime(56)));
                    ve2.Run((s) => { ScreenDrawing.BoundColor = Color.Lerp(Color.DarkRed, Color.White * 0.75f, s) * 0.6f; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    Line l = new(CentreEasing.EaseOutCubic(new(0, 0), new(640, 0), BeatTime(1f)), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    l.AlphaDecrease(BeatTime(1));
                    CreateEntity(l);
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(0, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    l.AlphaDecrease(BeatTime(1));
                    CreateEntity(l);
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("ConvL1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.6f), false);
                });
                RegisterFunctionOnce("ConvR1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.6f), true);
                });
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 10, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(10, 0, BeatTime(0.25f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.33f), ValueEasing.EaseOutQuart(0, 10, BeatTime(0.33f)));
                    ve.Insert(BeatTime(0.33f), ValueEasing.EaseOutQuad(10, 0, BeatTime(0.33f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(20, 0, BeatTime(0.75f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("ScaleIn", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(0.75f), ScreenDrawing.ScreenScale + 0.175f, 0.1f);
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(0.5f), 1, 0.15f);
                });
                RegisterFunctionOnce("FinalLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(0, 0), new(190, 0), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(190, 0), new(380, 0), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(380, 0), new(650, 0), BeatTime(0.75f)));
                    ValueEasing.EaseBuilder rot = new();
                    rot.Insert(0, ValueEasing.Stable(90));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(90, 80, BeatTime(0.75f)));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(80, 70, BeatTime(0.75f)));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(70, 60, BeatTime(0.75f)));
                    Line l = new(ce.GetResult(), rot.GetResult());
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    LineShadow(10, l);
                    DelayBeat(1.5f, () => { l.AlphaDecrease(BeatTime(1f)); });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                });
                bool another = false;
                if (!another)
                    CreateChart(BeatTime(7.5f), BeatTime(0.5f), 6, new string[]
                    {

            "R","","","",   "R1","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "R1","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "R","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "R","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","R1","",   "R","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                    });
                if (!another)
                    CreateChart(BeatTime(4), BeatTime(1f), 6, new string[]
                    {
                "","","","",   "","","","",
                "","","","",   "","","","",

                "BoundStart(LeftLine1)(ConvL1)","","","",   "(Bound1)(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                //
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",

                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","(Blur2)","","",
                "(^$0'1.4)(^+2'1.4)(ScaleIn)(FinalLine)(Flicker)","","","(Blur2)",   "","","(^$0'1.4)(^+2'1.4)(ScaleIn)(Flicker)","",
                "","(Blur2)","","",   "(^$0'1.4)(^+2'1.4)(ScaleBack)(Flicker)","","(Blur3)","",
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
                        //
                    });
            }//Tlottgodinf's turn!
            void NorPart4()
            {

                RegisterFunctionOnce("soulG", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("crossbone1", () =>
                {
                    DrawingUtil.CrossBone(new Vector2(320 - 135, Heart.Centre.Y), new Vector2(4, 0), 30, 2, Rand(1, 2));
                    DrawingUtil.CrossBone(new Vector2(320 + 135, Heart.Centre.Y), new Vector2(-4, 0), 30, 2, LastRand);
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("crossbone2", () =>
                {
                    DrawingUtil.CrossBone(new Vector2(320, 120), new Vector2(0, 4), 30, 2, 2);
                    DrawingUtil.CrossBone(new Vector2(320, 360), new Vector2(0, -4), 30, 2, 2);
                    PlaySound(Sounds.pierce);

                });
                RegisterFunctionOnce("atk1", () =>
                {
                    SetSoul(0);
                    Heart.Speed = 3.25f;
                    SetBox(new Vector2(320, 240), 240, 180);
                    float t = 0;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(16), ValueEasing.Linear(360 / BeatTime(3.3f)));
                    ve.Run((s) => { t = s; });
                    float t2 = 0;
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(16), ValueEasing.Linear(360 / BeatTime(3.5f)));
                    ve2.Run((s) => { t2 = s; });
                    for (int i = 5; i < BeatTime(5); i++)
                    {
                        AddInstance(new InstantEvent(i * 3, () =>
                        {

                            CreateBone(new DownBone(true, 8, Sin(t) * 50f + 50) { MarkScore = false });
                            CreateBone(new UpBone(true, 8, -Sin(t) * 50f + 50) { MarkScore = false });
                            CreateBone(new DownBone(false, 6, Cos(t2 - 30) * 50f + 50) { MarkScore = false });
                            CreateBone(new UpBone(false, 6, -Cos(t2 - 30) * 50f + 50) { MarkScore = false });
                        }));
                    }
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    SetSoul(0);
                    Heart.Speed = 3.2f;
                    SetBox(new Vector2(320, 240), 180, 240);
                    PlaySound(Sounds.pierce);
                    DelayBeat(0.5f, () => { PlaySound(Sounds.pierce); });
                    for (int i = 1; i < 8; i++)
                    {
                        DelayBeat(i * 1f, () =>
                        {

                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 2.75f, 80));
                            CreateBone(new RightBone(false, 2.75f, 80));
                        });
                        DelayBeat(i * 1f + 0.5f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(BoxStates.Centre.X, BoxStates.Up - 21));
                            ce.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4)));
                            CreateBone(new CustomBone(new(0, 0), ce.GetResult(), 0, 40));

                        });
                    }
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ScreenDrawing.BoundColor = Color.DarkRed;
                    ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = 0.1f;
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInCubic(0, 0.72f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0.72f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s * 0.2f;
                        StepSample.Intensity = 0.01f + s * 6f;
                        splitter.Intensity = 1f + 7f * s;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 8, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutSine(8, 0, BeatTime(0.25f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;

                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(20, 0, BeatTime(0.5f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    Line[] ls = GetAll<Line>("A");
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1f), CentreEasing.EaseOutQuint(new(0, 0), new(380, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Tags = new string[] { "A" } };
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(1, () => { l.AlphaDecrease(BeatTime(1f)); });
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    Line[] ls = GetAll<Line>("A");
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(1f), CentreEasing.EaseOutQuint(new(320, 0), new(-15, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Tags = new string[] { "A" } };
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(1, () => { l.AlphaDecrease(BeatTime(1f)); });
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("WarnLineBlue", () =>
                {
                    Line l = new(new Vector2(320, 260), 45) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(new Vector2(320, 220), 45) { DrawingColor = Color.CornflowerBlue };
                    CreateEntity(l);
                    CreateEntity(l2);
                    l.AlphaDecrease(BeatTime(0.35f));
                    l2.AlphaDecrease(BeatTime(0.35f));
                    l.TransverseMirror = true;
                    l2.TransverseMirror = true;
                });
                RegisterFunctionOnce("WarnLineRed", () =>
                {
                    Line l = new(new Vector2(320, 260), 45) { DrawingColor = Color.LightCoral };
                    Line l2 = new(new Vector2(320, 220), 45) { DrawingColor = Color.LightCoral };
                    CreateEntity(l);
                    CreateEntity(l2);
                    l.AlphaDecrease(BeatTime(0.35f));
                    l2.AlphaDecrease(BeatTime(0.35f));
                    l.TransverseMirror = true;
                    l2.TransverseMirror = true;
                });
                RegisterFunctionOnce("Scales", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutCubic(1, 1.15f, BeatTime(1)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuart(1.15f, 1.33f, BeatTime(1.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(1.33f, 1.15f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1.15f, 1, BeatTime(1)));
                    ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                });
                RegisterFunctionOnce("SmallFlicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.25f);
                });
                RegisterFunctionOnce("StepFollow", () =>
                {
                    ForBeat(16, () => { StepSample.CentreX = Heart.Centre.X; StepSample.CentreY = Heart.Centre.Y; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 0.08f, BeatTime(1)));
                    ve.Insert(BeatTime(14), ValueEasing.Stable(0.08f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0.08f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                });
                RegisterFunctionOnce("SmallBlur", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseInSine(0, 10, BeatTime(1)));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(10, 0, BeatTime(1)));
                    ve.Run((s) => { Blur.Sigma = s; });
                });
                RegisterFunctionOnce("StepFollow2", () =>
                {
                    ForBeat(8, () => { StepSample.CentreX = Heart.Centre.X; StepSample.CentreY = Heart.Centre.Y; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 0.08f, BeatTime(1)));
                    ve.Insert(BeatTime(6), ValueEasing.Stable(0.08f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0.08f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                });
                RegisterFunctionOnce("SuddenLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(0.5f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutCubic(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    LineShadow(6, l);
                    l.AlphaDecrease(BeatTime(0.75f));
                });
                RegisterFunctionOnce("SuddenLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(0.8f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(0, 0), new(280, 0), BeatTime(0.75f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    LineShadow(6, l);
                    l.AlphaDecrease(BeatTime(0.75f));
                });
                float times = -7.5f;
                float count = 0.2f;
                RegisterFunctionOnce("GravityLine", () =>
                {
                    float randomnumber1 = Rand(-20, 20);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, Rand(200, 400)));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, times), new(0, count)));
                    Line l = new(ce, ValueEasing.Stable(randomnumber1)) { Alpha = 0.7f };
                    CreateEntity(l);
                    l.InsertRetention(new Line.RetentionEffect(BeatTime(0.125f), 0.5f));
                    times -= 1;
                    count += 0.03f;
                });
                RegisterFunctionOnce("FinalShake", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(4, BeatTime(0.125f), false);
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                    DelayBeat(0.125f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(0.125f), true);
                        ScreenDrawing.MakeFlicker(Color.Silver * 0.25f);
                    });
                });
                RegisterFunctionOnce("SilverShake", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.35f);
                    ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1f), RandBool());
                });
                RegisterFunctionOnce("Over", () =>
                {
                    Line[] ls = GetAll<Line>();
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    ScreenDrawing.MakeFlicker(Color.Silver);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R02{Tap}(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R02{Tap}(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R02{Tap}(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R02{Tap}(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "+002{Tap}(WarnLineBlue)","","(WarnLineBlue)","",
            "+002{Tap}(WarnLineBlue)","","","",   "","","","",

            "(atk1)(StepFollow)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SuddenLine1)","","","",   "(SuddenLine2)soulG","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineRed)","","","",
            "R12{Tap}(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineRed)","","","",
            "R12{Tap}(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineRed)","","","",
            "R12{Tap}(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "","","Blur3","",

            "(^R'1.4)(^+01'1.4)","","","",   "(WarnLineRed)","","","",
            "R12{Tap}(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "+012{Tap}(WarnLineRed)","","(WarnLineRed)","",
            "+012{Tap}(WarnLineRed)","","","",   "","","","",

            "(atk2)(StepFollow2)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "soulG","","","",

            "(^R'1.2)(^+01'1.2)(SilverShake)(GravityLine)","","","(^R'1.2)(^+01'1.2)(SilverShake)(GravityLine)",   "","","(^R'1.2)(^+01'1.2)(SilverShake)(GravityLine)","",
            "","","(^R'1.2)(^+01'1.2)(SilverShake)(GravityLine)","",   "(^R'1.2)(^+01'1.2)(SilverShake)(GravityLine)","","(^R'1.2)(^+01'1.2)(SilverShake)(GravityLine)","",
            "(R'1.2)(+01'1.2)(GravityLine)(FinalShake)","+01'1.2","","(R'1.2)(+01'1.2)(GravityLine)(FinalShake)",   "+0'1.2","","(R'1.2)(+01'1.2)(GravityLine)(FinalShake)","+01'1.2",
            "","(R'1.2)(+01'1.2)(GravityLine)(FinalShake)","^+0'1.3","",      "!!3","(>^$01'1.5)(+2'1.5)(GravityLine)(FinalShake)","($01'1.5)(>^+2'1.5)","(<^$01'1.5)(+2'1.5)(GravityLine)(FinalShake)",
            "($01'1.5)(<^+2'1.5)(Over)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "Blur","","","",   "","","","",
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
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
            //
            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
                });
            }//zKronO's turn!
            void NorPart5()
            {
                float del = 50;
                ScreenDrawing.UISettings.RemoveUISurface();
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(2), ValueEasing.EaseInCubic(0, -0.32f, BeatTime(2)));
                    e1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(-0.32f, 0, BeatTime(2)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Polar.Intensity = s;
                    });
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;
                        ScreenDrawing.ScreenScale += s * 0.017f;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseInQuint(0, 0.2f, BeatTime(0.25f)));
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuint(0.2f, 0, BeatTime(0.25f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {

                        StepSample.Intensity = 0.01f + s;


                    });
                });
                RegisterFunctionOnce("Blur4", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseInQuint(0, 0.2f, BeatTime(0.5f)));
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuint(0.2f, 0, BeatTime(0.5f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("Blur5", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseInQuint(0, 0.6f, BeatTime(0.5f)));
                    e1.Insert(BeatTime(3.5f), ValueEasing.EaseOutQuint(0.6f, 0, BeatTime(3.5f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.05f);
                });
                RegisterFunctionOnce("Bound", () =>
                {
                    ForBeat120(12, () =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Lerp(Color.White, Color.Red, 0.21f), 0.4f) * 0.5f;
                    });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(16), ValueEasing.Linear(0, 320, BeatTime(16)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(320, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.LeftBoundDistance = s; ScreenDrawing.RightBoundDistance = s; });
                });
                RegisterFunctionOnce("BlueSoul", () =>
                {
                    HeartAttribute.Gravity = 9f;
                    HeartAttribute.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(320, 260, 128);
                });
                RegisterFunctionOnce("GreenSoul", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("BlueSoul2", () =>
                {
                    HeartAttribute.Gravity = 9f;
                    HeartAttribute.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(150, 260, 128);
                });
                RegisterFunctionOnce("BoneSea", () =>
                {
                    for (int a = 0; a < 32 * 2 * 1.2f; a++)
                        CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 40) { MarkScore = false });

                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), CentreEasing.Stable(500, 335));
                    ve.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(500, 335), new(320, 335), BeatTime(2)));
                    ve.Insert(BeatTime(30), CentreEasing.XSinWave(128, BeatTime(8), 0));
                    Platform p = new(0, new(0, 0), ve.GetResult(), 0, 40);
                    CreateEntity(p);
                    DelayBeat(32, () => { p.Dispose(); });
                });
                RegisterFunctionOnce("BoneSea2", () =>
                {
                    for (int a = 0; a < 32 * 2 * 1.2f; a++)
                        CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 40) { MarkScore = false });

                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), CentreEasing.Stable(500, 165));
                    ve.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(500, 190 + 15), new(320, 190 + 15), BeatTime(2)));
                    ve.Insert(BeatTime(30), CentreEasing.XSinWave(128, BeatTime(8), 0));
                    Platform p = new(0, new(0, 0), ve.GetResult(), 0, 40);
                    CreateEntity(p);
                    DelayBeat(32, () => { p.Dispose(); });
                });
                RegisterFunctionOnce("BoomBone", () =>
                {
                    for (int a = 0; a < Rand(2, 3); a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(Rand(Heart.Centre.X - 10, Heart.Centre.X + 10), Rand(-60, -30)));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.010f, 0.010f), Rand(3.30f, 4.30f)), new(0, Rand(0.10f, 0.20f))));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                        ve.Insert(BeatTime(8), ValueEasing.Accerlating(0, Rand(0.10f, 0.30f) * Someway.Rand0or1()));
                        CustomBone b = new(new(0, 0), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 35 }, IsMasked = false };
                        CreateBone(b);
                    }
                });
                RegisterFunctionOnce("BoomBone2", () =>
                {
                    for (int a = 0; a < Rand(1, 2); a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(Rand(Heart.Centre.X - 10, Heart.Centre.X + 10), 640 + Rand(30, 60)));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-1.60f, 1.60f), Rand(-13.00f, -11.00f)), new(0, Rand(0.15f, 0.20f))));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                        ve.Insert(BeatTime(8), ValueEasing.Accerlating(0, Rand(0.10f, 0.20f) * Someway.Rand0or1()));
                        CustomBone b = new(new(0, 0), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 35 }, IsMasked = false };
                        CreateBone(b);
                    }
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    DrawingUtil.Shock();
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.125f), ValueEasing.EaseInQuint(0, 0.1f, BeatTime(0.125f)));
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuint(0.1f, 0, BeatTime(0.25f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("upload", () =>
                {
                    CentreEasing.EaseBuilder v = new();
                    CentreEasing.EaseBuilder vb = new();
                    CentreEasing.EaseBuilder va = new();
                    v.Insert(0, CentreEasing.Stable(new(320, 500)));
                    va.Insert(0, CentreEasing.Stable(new(0, 820)));
                    vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                    v.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(1f)));
                    va.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(1f)));
                    vb.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(1f)));
                    Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                    Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    LineShadow(3, 0.9f, 4, a);
                    LineShadow(3, 0.9f, 4, b);
                    game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                });
                RegisterFunctionOnce("ScreenRot+", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(2, BeatTime(2f));
                });
                RegisterFunctionOnce("ScreenRot-", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(-2, BeatTime(2f));
                });
                RegisterFunctionOnce("BoundEaseBack", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(30 * a, 40 * (a + 1), BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(40 * (a + 1), 30 * (a + 1), BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.Stable(210));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(210, 140, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(140, 170, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(170, 70, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(70, 100, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(100, 0, BeatTime(0.25f)));
                    ve.Run((s) => { ScreenDrawing.UpBoundDistance = s; ScreenDrawing.DownBoundDistance = s; });
                });
                RegisterFunctionOnce("Con", () => { ScreenDrawing.CameraEffect.Convulse(9, BeatTime(1f), true); });
                RegisterFunctionOnce("Con2", () => { ScreenDrawing.CameraEffect.Convulse(9, BeatTime(1f), false); });
                RegisterFunctionOnce("Line", () =>
                {
                    Line l = new(new Vector2(del * 1.3f, 0), new Vector2(0, del));
                    CreateEntity(l);
                    del += 16;
                    l.AlphaDecrease(BeatTime(0.5f));
                    l.ObliqueMirror = true;
                    l.TransverseMirror = true;
                    l.VerticalMirror = true;
                });
                RegisterFunctionOnce("Mask", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(18), Color.Black, 0.4f);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(BeatTime(16.5f), ValueEasing.Stable(0.4f));
                    v.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.4f, 0, BeatTime(1.5f)));
                    v.Run((s) => { m.alpha = s; });
                    DelayBeat(18, () => { m.Dispose(); });
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
            "","","","",   "Mask","","","",
            //空拍
            "(Bound)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",
            //正片
            "(D{v})","","","",   "+2{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",

            "(D{v})","","","",   "+0{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",

            "(D{v})","","","",   "+2{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur2)","",   "","","","",

            "(R)(+01)(ScaleBack)(upload)","","","",   "D","","+2","",
            "+2","","","",   "(D)(Blur3)","","","",
            "(D)(ScreenRot+)","","+0","",   "+0","","","",
            "D","","Blur3","",   "D","","(Blur3)","",

            "(R)(ScreenRot-)","","","",   "D","","+2","",
            "+2","","(Blur3)","",   "(D)","","","",
            "(D)(ScreenRot-)","","+0","",   "+0","","","",
            "D","","","",   "D(Blur4)","","","",

            "R(upload)(ScreenRot+)(Line)","","","",   "","","","",
            "R(ScreenRot+)(Line)","","","",   "(Blur4)","","","",
            "R(upload)(ScreenRot-)(Line)","","","",   "R(Line)","","","",
            "R(ScreenRot-)(Line)","","R(Line)","",   "R(Blur4)(Line)","","R(Line)","",

            "(R)(+01)(upload)(ScreenRot+)(Line)","","","",   "(R)(+01)(Line)","","(R)(+01)(Line)","",
            "","","(R)(+01)(Line)","",   "(R)(+01)(Blur4)(Line)","","","",
            "(R)(+0)(+01)(+01)(ScreenRot+)(Line)","","","",   "(D)(+0)(+01)(+01)(Blur4)(Line)","","","",
            "(D)(+0)(+01)(+01)(Line)","","","",   "(D)(+0)(+01)(+01)(Blur5)(Line)","","","",

            "(D)(+0)(+01)(+01)(BlueSoul)(BoneSea)(upload)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","(BoomBone)(Shake)","",

            "(BoomBone)(Shake)","(BoomBone)(Shake)","(BoomBone)(Shake)","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)","","","",   "(BoomBone)(Shake)","","","",
            "(BoomBone)(Shake)(GreenSoul)ScreenRot-","","","",   "","","","",

            "R(BoundEaseBack)(Con)","","+0","",   "+0(Con)","","+0","",
            "R(Con)","","+0","",   "+0(Con)","","+0","",
            "R(Con)","","+0","",   "+0(Con)","","+0","",
            "R(Con)","","+0","",   "+0(Con)","","+0","",

            "(BlueSoul2)(BoneSea2)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "ScreenRot-","","","",   "","","","",

            "(BoomBone2)(Shake)","","(BoomBone2)(Shake)","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","(BoomBone2)(Shake)","",

            "(BoomBone2)(Shake)","(BoomBone2)(Shake)","(BoomBone2)(Shake)","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)","","","",   "(BoomBone2)(Shake)","","","",
            "(BoomBone2)(Shake)(ScreenRot+)","","(BoomBone2)(Shake)","",   "(BoomBone2)(GreenSoul)(Shake)","","","",

            "R(BoundEaseBack)(Con2)","","+0","",   "+0(Con2)","","+0","",
            "R(Con2)","","+0","",   "+0(Con2)","","+0","",
            "R(Con2)","","+0","",   "+0(Con2)","","+0","",
            "R(Con2)","","+0","",   "+0(Con2)","","+0","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                });
            }//ParaDOXXX vs zKronO!
            void NorPart6()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                RegisterFunctionOnce("RickRoll", () =>
                {
                    Heart.FixArrow = true;
                    ScreenDrawing.ScreenAngle += 90;
                    Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                });
                RegisterFunctionOnce("Scale", () =>
                {
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    ScreenDrawing.ScreenScale = 1f;
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Rand(480 - 20, 480 + 20), 20));
                    ce.Insert(BeatTime(4f), CentreEasing.Accerlating(new(0, 0), new(0, Rand(0.08f, 0.12f))));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(20, 40)));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(3));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Rand(480 - 20, 480 + 20), 20));
                    ce.Insert(BeatTime(4f), CentreEasing.Accerlating(new(0, 0), new(0, Rand(0.08f, 0.12f))));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(-40, -20)));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(3));
                });
                RegisterFunctionOnce("CentreLine1", () =>
                {
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, 135, BeatTime(2)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(135, 135 + 45f / 2f, BeatTime(0.5f)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(135 + 45f / 2f, 135 + 45f, BeatTime(0.5f)));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, 360 + 45, BeatTime(2)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(360 + 45, 360 + 45 - 45f / 2f, BeatTime(0.5f)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(360 + 45 - 45f / 2f, 360 + 45 - 45f, BeatTime(0.5f)));
                    Line l1 = new(CentreEasing.Stable(320, 240), ve1.GetResult());
                    l1.Alpha = 0.4f;
                    l1.DrawingColor = Color.Red;
                    Line l2 = new(CentreEasing.Stable(320, 240), ve2.GetResult());
                    l2.Alpha = 0.4f;
                    l2.DrawingColor = Color.Red;
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(2f, () => { l1.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); l2.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); });
                    DelayBeat(2.5f, () => { l1.AlphaIncrease(BeatTime(0.125f), 0.6f); l2.AlphaIncrease(BeatTime(0.125f), 0.6f); });
                    DelayBeat(2.625f, () => { l1.AlphaDecrease(BeatTime(0.65f), 1f); l2.AlphaDecrease(BeatTime(0.65f), 1f); });
                });
                RegisterFunctionOnce("CentreLine2", () =>
                {
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, -135, BeatTime(2)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(-135, -135 - 45f / 2f, BeatTime(0.5f)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(-135 - 45f / 2f, -135 - 45f, BeatTime(0.5f)));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, -360 - 45, BeatTime(2)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(-360 - 45, -360 - 45 + 45f / 2f, BeatTime(0.5f)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(-360 - 45 + 45f / 2f, -360 - 45 + 45f, BeatTime(0.5f)));
                    Line l1 = new(CentreEasing.Stable(320, 240), ve1.GetResult());
                    l1.Alpha = 0.4f;
                    l1.DrawingColor = Color.Red;
                    Line l2 = new(CentreEasing.Stable(320, 240), ve2.GetResult());
                    l2.Alpha = 0.4f;
                    l2.DrawingColor = Color.Red;
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(2f, () => { l1.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); l2.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); });
                    DelayBeat(2.5f, () => { l1.AlphaIncrease(BeatTime(0.125f), 0.6f); l2.AlphaIncrease(BeatTime(0.125f), 0.6f); });
                    DelayBeat(2.625f, () => { l1.AlphaDecrease(BeatTime(0.65f), 1f); l2.AlphaDecrease(BeatTime(0.65f), 1f); });
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutSine(new(0, 0), new(150, 0), BeatTime(1)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseInSine(new(150, 0), new(-5, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(2, () => { l.Dispose(); });
                });
                RegisterFunctionOnce("90Arrow1", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("901");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90Arrow2", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("902");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90Arrow3", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("903");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90", () =>
                {
                    Arrow[] ars = GetAll<Arrow>("901");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        ars[x].CentreRotationOffset = 90;
                    }
                    Arrow[] ars2 = GetAll<Arrow>("902");
                    for (int a = 0; a < ars2.Length; a++)
                    {
                        int x = a;
                        ars2[x].CentreRotationOffset = 90;
                    }
                    Arrow[] ars3 = GetAll<Arrow>("903");
                    for (int a = 0; a < ars3.Length; a++)
                    {
                        int x = a;
                        ars3[x].CentreRotationOffset = 90;
                    }
                });
                RegisterFunctionOnce("Mask", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(10), Color.Black, 0);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(0, ValueEasing.Stable(0));
                    v.Insert(BeatTime(2.25f), ValueEasing.EaseOutBack(0, 0.3f, BeatTime(2.25f)));
                    v.Insert(BeatTime(1.75f), ValueEasing.Stable(0.3f));
                    v.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(0.3f, 0.4f, BeatTime(0.5f)));
                    v.Insert(BeatTime(1.5f), ValueEasing.Stable(0.4f));
                    v.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(0.4f, 0.5f, BeatTime(0.5f)));
                    v.Insert(BeatTime(2), ValueEasing.Stable(0.5f));
                    v.Insert(BeatTime(0.75f), ValueEasing.EaseInQuad(0.5f, 0.99f, BeatTime(0.5f)));
                    v.Insert(BeatTime(0.4f), ValueEasing.Stable(0.99f));
                    v.Insert(BeatTime(0.35f), ValueEasing.EaseOutCubic(0.99f, 0, BeatTime(0.5f)));
                    v.Run((s) => { m.alpha = s; });
                    DelayBeat(10, () => { m.Dispose(); });
                    ValueEasing.EaseBuilder bd = new();
                    bd.Insert(BeatTime(2), ValueEasing.EaseInSine(ScreenDrawing.DownBoundDistance, 0, BeatTime(2)));
                    bd.Insert(0, ValueEasing.Stable(0));
                    bd.Run((x) => { ScreenDrawing.UpBoundDistance = x; ScreenDrawing.DownBoundDistance = x; });
                    ValueEasing.EaseBuilder scl = new();
                    scl.Insert(BeatTime(2), ValueEasing.Stable(1));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1, 1.04f, BeatTime(2)));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1.04f, 1.09f, BeatTime(2)));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1.09f, 1.15f, BeatTime(2)));
                    scl.Insert(BeatTime(0.5f), ValueEasing.Stable(1.15f));
                    scl.Insert(BeatTime(1.25f), ValueEasing.EaseOutSine(1.15f, 1.45f, BeatTime(1.25f)));
                    scl.Insert(BeatTime(1.5f), ValueEasing.EaseOutBack(1.45f, 1, BeatTime(1.5f)));
                    scl.Run((a) => { ScreenDrawing.ScreenScale = a; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), false);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), false);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(0, 0), new(240, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(-240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(-120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(0, 0), new(380, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), true);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), true);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(640, 0), new(400, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(640, 0), new(480, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(640, 0), new(260, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });

                RegisterFunctionOnce("H0", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 0, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("H2", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 2, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("H1", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 1, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });

                RegisterFunctionOnce("BoundOutQuad", () =>
                {
                    ScreenDrawing.BoundColor = Color.DarkRed * 0.7f;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 100, BeatTime(1)));
                    for (int a = 0; a < 48 * 2 - 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(100, 180, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(180, 100, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuart(100, 0, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                    ScreenDrawing.CameraEffect.Convulse(3, BeatTime(0.25f), RandBool());
                    ScreenDrawing.ScreenScale += 0.02f;
                });

                RegisterFunctionOnce("ScaleLerp", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(1), 1, 0.09f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6f, new string[]
                {
            "!!3","R(90)","+0","+0",   "+0","","","",
            "R","","","",   "R","","","",
            "R","","","^R'1.3",   "+0","","","^R'1.3",
            "+0","","","^R'1.3",   "+0","","","",

            "(R)(LeftLine1)","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","","",   "D","","+0","",

            "(R)RightLine1","","","",   "D","","+2","",
            "+2","","+2","",   "","","(D)","",
            "","","(D)","",   "+2","","+2","",
            "+2","","+2","",   "+202{Tap}(Scale)","","+212{Tap}","",

            "+202{Tap}(RickRoll)(Scale)","","+212{Tap}","",   "+202{Tap}(RickRoll)(Scale)","","+212{Tap}","",
            "+202{Tap}(RickRoll)(Scale)","","+212{Tap}","",   "+202{Tap}(RickRoll)(ScaleBack)","","+212{Tap}","",
            "Mask","","","",   "","","","",
            "","","","",   "","","","",
            //
            "$0{Down,v}(Line1)","","$0{v}(Line1)","",   "$0{Up,v}(Line1)","","","",
            "","","","",   "$0{v}","","","",
            "$01{Down,v}(Line2)","","$01{v}(Line2)","",   "$01{Up,v}(Line2)","","","",
            "","","","",   "$01{v}","","","",

            "$21{Down,v}(Line1)","","$21{v}(Line1)","",   "$21{Up,v}(Line1)","","","",
            "","","","",   "","","","",
            "$21{v}(Line2)","","","",   "","","$21{v}(Line2)","",
            "","","","",   "","","","(90Arrow1)",

            "(#0.8#R)(CentreLine1)(Line3)(+0{901})","","","",   "","","","",
            "","","","",   "R","","","",
            "R","","","",   "+0","","","",
            "","","","",   "","","","(90Arrow2)",

            "(#0.8#R)(CentreLine2)(Line3)(+0{902})","","","",   "","","","",
            "","","","",   "R","","","",
            "R","","","",   "+0","","","",
            "","","","",   "","","","(90Arrow3)",
            //
            "(#0.8#R)(CentreLine1)(Line3)(+0{903})","","","",   "","","","",
            "","","","",   "R","","","",
            "R","","","",   "+0","","","",
            "","","","",   "","","","",

            "R","","","",   "R","","","",
            "R","","","",   "($3'1.3)(+01'1.3)(Flicker)","","(+0'1.3)(+01'1.3)(Flicker)","",
            "($0'1.3)(+21'1.3)(Flicker)","($0'1.3)(+21'1.3)(Flicker)","($0'1.3)(+21'1.3)(Flicker)","",   "($0'1.3)(+21'1.3)(Flicker)","","($0'1.3)(+21'1.3)(Flicker)","",
            "($0'1.3)(+21'1.3)(Flicker)","($0'1.3)(+21'1.3)(Flicker)","($0'1.3)(+21'1.3)(Flicker)","",   "($0'1.3)(+21'1.3)(Flicker)","","($0'1.3)(+21'1.3)(Flicker)","",

            "($0'1.3)(+21'1.3)(BoundOutQuad)(Flicker)(ScaleLerp)","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","","",
            "","","R","",   "R","","","",

            "","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",


                });
                CreateChart(BeatTime(4), BeatTime(1), 6f, new string[]
                {
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
            //
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "(^R02'1.3{Tap})(^+012'1.3{Tap})","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
                });
            }//Tlott's turn!
            void NorPart7()
            {
                RegisterFunctionOnce("H3", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 3, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.85f);
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("soulB", () =>
                {
                    HeartAttribute.Speed = 3.4f;
                    TextPrinter t = new(BeatTime(8), "$Press [;] or \n[Down] to \nDown!", new Vector2(30, 160), new TextAttribute[]
                    {

                new TextSpeedAttribute(114),/*
            })
            { sound = false };
            CreateEntity(t);
            SetSoul(2);
            InstantSetBox(new Vector2(320, 240), 180, 500);
            float y = 60;
            ForBeat(13 * 4, () =>
            {
                InstantTP(new(Heart.Centre.X, y));
                HeartAttribute.Gravity = 0;
                HeartAttribute.JumpTimeLimit = 0;
                if (GameStates.IsKeyDown(InputIdentity.MainDown))
                {
                    y += 0.3f;
                }
                if (GameStates.IsKeyDown(InputIdentity.MainUp))
                {
                    y -= 0.3f;
                }

            });
            ForBeat(13 * 4, () =>
            {
                StepSample.CentreX = Heart.Centre.X;
                StepSample.CentreY = Heart.Centre.Y;
            });
            ScreenDrawing.ScreenScale = 1f;
            StepSample.Intensity = 0.1f;
            HeartAttribute.Speed = 2.1f;
            splitter.Intensity = 1 + 2f;
        });
        RegisterFunctionOnce("Attack", () =>
        {
            float height1 = 0;
            float height2 = 0;
            ValueEasing.EaseBuilder hf = new();
            ValueEasing.EaseBuilder sinf = new();
            hf.Insert(0, ValueEasing.Stable(0));
            hf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
            hf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
            hf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
            hf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
            hf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
            hf.Insert(BeatTime(4), ValueEasing.SinWave(17, BeatTime(2), -1));
            hf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
            hf.Insert(0, ValueEasing.Stable(0));
            hf.Run((s) =>
            {
                height1 = s;
            });
            sinf.Insert(0, ValueEasing.Stable(0));
            sinf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
            sinf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
            sinf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
            sinf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
            sinf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
            sinf.Insert(BeatTime(4), ValueEasing.SinWave(17, BeatTime(2), 0));
            sinf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
            sinf.Insert(0, ValueEasing.Stable(0));
            sinf.Run((s) =>
            {
                height2 = s;
            });
            for (int i = 0; i < 14 * 4 * 14.2f; i++)
            {
                AddInstance(new InstantEvent(2 * i, () =>
                {
                    CreateBone(new LeftBone(true, 9, height1) { ColorType = 0, MarkScore = false });
                    CreateBone(new RightBone(true, 9, height2) { ColorType = 0, MarkScore = false });
                }));
            }
            DelayBeat(5 * 4, () =>
            {
                for (int i = 0; i < 8; i++)
                {
                    DelayBeat(i+0.5f, () =>
*/
                    })
                    { PlaySound = false };
                    CreateEntity(t);
                    SetSoul(2);
                    InstantSetBox(new Vector2(320, 240), 180, 500);
                    float y = 60;
                    ForBeat(13 * 4, () =>
                    {
                        InstantTP(new(Heart.Centre.X, y));
                        HeartAttribute.Gravity = 0;
                        HeartAttribute.JumpTimeLimit = 0;
                        if (GameStates.IsKeyDown(InputIdentity.MainDown))
                        {
                            y += 0.3f;
                        }
                        if (GameStates.IsKeyDown(InputIdentity.MainUp))
                        {
                            y -= 0.3f;
                        }

                    });
                    ForBeat(13 * 4, () =>
                    {
                        StepSample.CentreX = Heart.Centre.X;
                        StepSample.CentreY = Heart.Centre.Y;
                    });
                    ScreenDrawing.ScreenScale = 1f;
                    StepSample.Intensity = 0.1f;
                    HeartAttribute.Speed = 2.1f;
                    splitter.Intensity = 1 + 2f;
                });
                RegisterFunctionOnce("Attack", () =>
                {
                    float height1 = 0;
                    float height2 = 0;
                    ValueEasing.EaseBuilder hf = new();
                    ValueEasing.EaseBuilder sinf = new();
                    hf.Insert(0, ValueEasing.Stable(0));
                    hf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                    hf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                    hf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                    hf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                    hf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                    hf.Insert(BeatTime(4), ValueEasing.SinWave(17, BeatTime(2), -1));
                    hf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                    hf.Insert(0, ValueEasing.Stable(0));
                    hf.Run((s) =>
                    {
                        height1 = s;
                    });
                    sinf.Insert(0, ValueEasing.Stable(0));
                    sinf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                    sinf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                    sinf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                    sinf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                    sinf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                    sinf.Insert(BeatTime(4), ValueEasing.SinWave(17, BeatTime(2), 0));
                    sinf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                    sinf.Insert(0, ValueEasing.Stable(0));
                    sinf.Run((s) =>
                    {
                        height2 = s;
                    });
                    for (int i = 0; i < 14 * 4 * 14.2f; i++)
                    {
                        AddInstance(new InstantEvent(2 * i, () =>
                        {
                            CreateBone(new LeftBone(true, 9, height1) { ColorType = 0, MarkScore = false });
                            CreateBone(new RightBone(true, 9, height2) { ColorType = 0, MarkScore = false });
                        }));
                    }
                    DelayBeat(5 * 4, () =>
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(i + 0.5f, () =>
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
                    DelayBeat(4, () => { CreateEntity(new Boneslab(180, 140, BeatTime(7 * 4), BeatTime(5 * 4)) { ColorType = 0 }); });
                });
                RegisterFunctionOnce("LeftB", () =>
                {
                    float rd = Rand(1, 2);
                    if (rd == 1)
                    {
                        DrawingUtil.CrossBone(new Vector2(Rand(320 - 50, 320 + 10), 500), new Vector2(0, -8), 30, 1, 2);
                    }
                    else if (rd == 2)
                    {
                        CreateBone(new CustomBone(new Vector2(Rand(320 - 50, 320 + 10), 500), Motions.PositionRoute.linear, 0, 30)
                        {
                            PositionRouteParam = new float[] { 0, -7 },
                            ColorType = 0
                        });
                    }
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("Kick1", () =>
                {
                    float rot = Rand(9, 20);
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot)), rot, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270)), 180, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot)), -rot, 40) { ColorType = 0 });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("Kick2", () =>
                {
                    float rot = Rand(9, 20);
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot / 2)), rot / 2, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot / 2)), -rot / 2, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot * 1.5f)), rot * 1.5f, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot * 1.5f)), -rot * 1.5f, 40) { ColorType = 0 });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("RightB", () =>
                {
                    PlaySound(Sounds.pierce);
                    float rd = Rand(1, 2);
                    if (rd == 1)
                    {
                        DrawingUtil.CrossBone(new Vector2(Rand(320 - 10, 320 + 50), 500), new Vector2(0, -8), 30, 1, 2);
                    }
                    else if (rd == 2)
                    {
                        CreateBone(new CustomBone(new Vector2(Rand(320 - 10, 320 + 50), 500), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 0, -7 },
                            ColorType = 0
                        });
                    }
                });//REMEMBER to ADD these STRING INTO the STRING which under this WORD
                RegisterFunctionOnce("Sounds", () =>
                {
                    for (int a = 0; a < 2; a++) PlaySound(Sounds.destroy);
                });
                RegisterFunctionOnce("FirstBone", () =>
                {
                    for (int a = 0; a < 8; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 540 + a * 27));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -Rand(4.00f, 8.00f)), new(0, -Rand(0.040f, 0.090f))));
                        CreateBone(new CustomBone(new(0, 0), ce.GetResult(), 90 + Rand(-30, 30), BoxStates.Width + 80) { ColorType = 2 });
                    }

                });
                CreateChart(BeatTime(4), BeatTime(1), 6f, new string[]
                {
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","","",
            "","","R","",   "R","","","",

            "","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "($0)($2)(Flicker)","","","",   "","","($0)($2)(Flicker)","",
            "","","","",   "($0)($2)(Flicker)","","","",
            //HALLLLLLLLLLLLLLLLLLLLLL
            "(soulB)(Flicker)(Attack)(Sounds)(FirstBone)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(LeftB)","","","",   "(LeftB)","","","",
            "(LeftB)","","(LeftB)","",   "(LeftB)","","","",
            "(LeftB)","","","",   "(LeftB)","","(LeftB)","",
            "","","(LeftB)","",   "(LeftB)","","","",
            //
            "(LeftB)","","","",   "(LeftB)","","","",
            "(LeftB)","","","",   "(LeftB)","","","",
            "(LeftB)","","","",   "(LeftB)","","(LeftB)","",
            "","","(LeftB)","",   "(LeftB)","","(LeftB)","",

            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","(RightB)","",
            "","","(RightB)","",   "(RightB)","","","",

            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","(RightB)","",
            "","","(RightB)","",   "(RightB)","","(RightB)","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "(^R02'1.3{Tap})(^+012'1.3{Tap})","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            //
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "(^R02'1.3{Tap})(^+012'1.3{Tap})","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",

            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
                });
            }
            void NorPart8()
            {
                RegisterFunctionOnce("GB", () =>
                {

                    int non = Rand(1, 2);
                    CreateEntity(new NormalGB(new(Heart.Centre.X, 440), new(Heart.Centre.X, 440), new(0.75f, 0.375f), 270, BeatTime(4.5f), BeatTime(0.3f)) { AppearVolume = 0.01f });
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    float length = 0;
                    ValueEasing.EaseBuilder len = new();
                    len.Insert(0, ValueEasing.Stable(64));
                    len.Insert(BeatTime(0.5f), ValueEasing.Linear(80, 120, BeatTime(0.5f)));
                    len.Run((s) =>
                    {
                        length = s;
                    });
                    for (int i = 0; i < 1; i++)
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
                    len.Insert(0, ValueEasing.Stable(64));
                    len.Insert(BeatTime(0.5f), ValueEasing.Linear(80, 120, BeatTime(0.5f)));
                    len.Run((s) =>
                    {
                        length = s;
                    });
                    for (int i = 0; i < 1; i++)
                    {
                        AddInstance(new InstantEvent(i * 1, () =>
                        {
                            CreateBone(new RightBone(true, 12, length) { ColorType = 0 });
                        }));
                    }
                });
                RegisterFunctionOnce("ChangeA", () =>
                {
                    SetBox(120, 180, 500);
                    DelayBeat(0, () =>
                    {
                        HeartAttribute.Gravity = 9.8f;
                        HeartAttribute.JumpTimeLimit = 1;
                        Heart.GiveForce(0, 8);
                        DelayBeat(0.5f, () =>
                        {
                            DrawingUtil.CrossBone(new(320 - 90, 380), new(4, 0), 160, 2, 2);
                            DrawingUtil.CrossBone(new(320 + 90, 380), new(-4, 0), 160, 2, 2);
                            PlaySound(Sounds.pierce);
                        });
                    });
                    CreateEntity(new Boneslab(0, 30, BeatTime(1.4f), BeatTime(1)));
                    DrawingUtil.MaskSquare s = new(0, 0, 640, 480, BeatTime(0.9f), Color.Black, 1);
                    DelayBeat(1.5f, () => { TP(320, 240); CreateEntity(s); PlaySound(Sounds.change); DelayBeat(0.5f, () => { s.Dispose(); }); });
                });
                RegisterFunctionOnce("ChangeB", () =>
                {
                    float co1 = 0;
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(BeatTime(2), ValueEasing.Stable(0));
                    a.Insert(1, ValueEasing.Linear(0, 1, 1));
                    a.Run((s) => { co1 = s; });
                    InstantSetBox(new Vector2(320, 240), 160, 160);
                    SetSoul(0);
                    CreateBone(new CentreCircleBone(Rand(0, 359), 6.5f, 140, BeatTime(2)) { ColorType = 1 });
                    CreateBone(new CentreCircleBone(LastRand + 90, -5f, 140, BeatTime(2 * 3)) { ColorType = 1 });
                    for (int i = 0; i < 36; i++)
                    {
                        CreateBone(new SideCircleBone(i * 10, -6, 20, BeatTime(3 * 4 - 2)));
                    }
                    DelayBeat(6, () =>
                    {
                        float rotation = 0;
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(0, ValueEasing.Stable(30));
                        rot.Insert(BeatTime(4), ValueEasing.Linear(30, 390, BeatTime(4)));
                        rot.Run((s) => { rotation = s; });
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(i * 0.5f, () =>
                            {
                                CreateEntity(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(200, rotation), new Vector2(320, -20), new(0.875f, 0.625f), rotation + 180, BeatTime(4), BeatTime(1)));
                            });
                        }
                    });//不要让你的恶习复苏

                });
                RegisterFunctionOnce("SetSoulR", () =>
                {
                    splitter.Intensity = 0f;
                    StepSample.CentreX = 320;
                    StepSample.CentreY = 240;
                    StepSample.Intensity = 0.1f;
                    InstantSetBox(new Vector2(320, 240), 170, 170);
                    SetSoul(0);
                    Heart.Speed = 3f;
                    ScreenDrawing.ScreenScale = 1;
                });
                RegisterFunctionOnce("CentreBone", () =>
                {
                    CentreCircleBone c = new(Rand(0, 359), 4.5f, 180, BeatTime(9)) { ColorType = 1, IsMasked = true };
                    CreateBone(c);
                    DelayBeat(5, () =>
                    {
                        c.ColorType = 2;
                        PlaySound(Sounds.Ding);
                        c.RotateSpeed = 4;
                    });
                });
                float rot = 0;
                RegisterFunctionOnce("Value", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                    ve.Insert(BeatTime(5), ValueEasing.Linear(2));
                    ve.Insert(BeatTime(4), ValueEasing.Linear(-3.5f));
                    ve.Run((s) => { rot = s; });
                    ForBeat(13, () =>
                    {
                        Heart.Speed = GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainUp)
                            ? 3f * 1.414f
                            : GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainUp)
                                ? 3f * 1.414f
                                : GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainDown)
                                                            ? 3f * 1.414f
                                                            : GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight) ? 3f * 1.414f : 3f;
                    });
                });
                RegisterFunctionOnce("CreateGB1", () =>
                {
                    /*
                                    if (GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainUp))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainUp))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainDown))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else
                                    {
                                        Heart.Speed = 3f;
                                    }
                    */
                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(200, rot), new Vector2(320, 240) + MathUtil.GetVector2(300, rot), new(1, 0.5f), rot + 180, BeatTime(2), BeatTime(0.34f)) { AppearVolume = 0 });
                });
                RegisterFunctionOnce("CreateGB2", () =>
                {
                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(170, rot), new Vector2(320, 240) + MathUtil.GetVector2(300, rot), new(1, 0.5f), rot + 180, BeatTime(2), BeatTime(0.25f)) { AppearVolume = 0 });

                });
                RegisterFunctionOnce("Return", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0.1f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0.1f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    TP();
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "GB","","","",
            "","","","",   "GB","","","",
            "","","","",   "GB","","","",
            "","","","",   "GB","","","",

            "","","","",   "GB","","","",
            "","","","",   "GB","","","",
            "","","","",   "GB","","","",
            "","","","",   "GB","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",

            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "ChangeA","","","",   "","","","",
            "(Value)","","","",   "","","","",
            "SetSoulR(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB1)(CentreBone)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            //
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",

            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "(Return)","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","R","",
            "","","R","",   "R","","","",
            //
            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "R","","","",   "","","","",
            "R","","","",   "R","","","",
            "R","","R","",   "R","","R","",
            "","","R","",   "R","","R","",

            "","","","",   "","","","",

                });
            }//Tlott's turn!
            #endregion
            #region Easy
            void EasyPart1()
            {
                ScreenDrawing.UISettings.CreateUISurface();
                DelayBeat(4, () =>
                {
                    CentreEasing.EaseBuilder e1 = new();
                    CentreEasing.EaseBuilder e2 = new();
                    CentreEasing.EaseBuilder e3 = new();
                    CentreEasing.EaseBuilder e4 = new();
                    e1.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(320, 240 - 340), game.BeatTime(2)));
                    e2.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(320, 240 + 340), game.BeatTime(2)));
                    e3.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(660, 240), game.BeatTime(2)));
                    e4.Insert(game.BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(-20, 240), game.BeatTime(2)));
                    Line a = new(e1.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.55f };
                    Line b = new(e2.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.55f };
                    Line c = new(e3.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    Line d = new(e4.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    CreateEntity(c);
                    CreateEntity(d);
                    DelayBeat(4, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                        c.Dispose();
                        d.Dispose();
                    });
                    LineShadow(3, 0.9f, 9, a);
                    LineShadow(3, 0.9f, 9, b);
                    LineShadow(3, 0.9f, 9, c);
                    LineShadow(3, 0.9f, 9, d);
                });
                RegisterFunctionOnce("lineL", () =>
                {
                    CentreEasing.EaseBuilder ease = new();
                    ease.Insert(0, CentreEasing.Stable(new(840, 240)));
                    ease.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(840, 240), new(-20, 240), BeatTime(2)));
                    Line ce = new(ease.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(ce);

                    DelayBeat(4, () =>
                    {
                        ce.Dispose();
                    });
                });
                RegisterFunctionOnce("lineR", () =>
                {
                    CentreEasing.EaseBuilder ease = new();
                    ease.Insert(0, CentreEasing.Stable(new(-200, 240)));
                    ease.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(-200, 240), new(660, 240), BeatTime(2)));
                    Line ce = new(ease.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.55f };
                    CreateEntity(ce);

                    game.DelayBeat(4, () =>
                    {
                        ce.Dispose();
                    });
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(-3, game.BeatTime(2));
                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(3, game.BeatTime(2));
                });
                RegisterFunctionOnce("ShiningSoul", () => { SetSoul(1); });
                RegisterFunctionOnce("Bound", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ValueEasing.EaseBuilder vea = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    vea.Insert(game.BeatTime(32), ValueEasing.Stable(0.2f));
                    vea.Insert(game.BeatTime(16), ValueEasing.EaseOutSine(0.4f, 0.99f, game.BeatTime(16)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutCubic(0, 80, game.BeatTime(4)));
                    //ve.Insert(game.BeatTime(28), ValueEasing.Stable(28));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(80, 75, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseInSine(75, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(80, 85, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(85, 80, game.BeatTime(2)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(80, 120, game.BeatTime(4)));
                    ve.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(120, 160, game.BeatTime(4)));
                    for (int i = 0; i < 16; i++)
                    {
                        ve.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutBack(160 + i * 12, 172 + i * 12, game.BeatTime(0.5f)));
                    }
                    ve.Insert(0, ValueEasing.Stable(240));
                    ve.Run((s) =>
                    {
                        ScreenDrawing.UpBoundDistance = s;
                        ScreenDrawing.DownBoundDistance = s;
                    });
                    vea.Run((s) =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.DarkRed * 0.7f, s);
                    });
                });
                game.RegisterFunctionOnce("upload", () =>
                {
                    float z = Rand(0, 1);
                    if (z == 0)
                    {
                        CentreEasing.EaseBuilder v = new();
                        CentreEasing.EaseBuilder vb = new();
                        CentreEasing.EaseBuilder va = new();
                        v.Insert(0, CentreEasing.Stable(new(320, 500)));
                        va.Insert(0, CentreEasing.Stable(new(0, 820)));
                        vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                        v.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(2)));
                        va.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(2)));
                        vb.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(2)));
                        Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                        Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                        CreateEntity(a);
                        CreateEntity(b);
                        LineShadow(3, 0.9f, 4, a);
                        LineShadow(3, 0.9f, 4, b);
                        game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                        ValueEasing.EaseBuilder bl = new();
                        bl.Insert(0, ValueEasing.Stable(0));
                        bl.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 8, BeatTime(0.25f)));
                        bl.Insert(BeatTime(2.75f), ValueEasing.EaseOutCubic(8, 0, BeatTime(2.75f)));
                        bl.Run((x) =>
                        {
                            Blur.Sigma = x * 2;
                            splitter.Intensity = 1 + x * 0.1f;
                            StepSample.Intensity = 0.01f + x * 0.01f;
                        });
                    }
                    else if (z == 1)
                    {
                        CentreEasing.EaseBuilder v = new();
                        CentreEasing.EaseBuilder vb = new();
                        CentreEasing.EaseBuilder va = new();
                        v.Insert(0, CentreEasing.Stable(new(320, 500)));
                        va.Insert(0, CentreEasing.Stable(new(0, 820)));
                        vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                        v.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(2)));
                        va.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(2)));
                        vb.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(2)));
                        Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                        Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                        CreateEntity(a);
                        CreateEntity(b);
                        LineShadow(3, 0.9f, 4, a);
                        LineShadow(3, 0.9f, 4, b);
                        game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                        ValueEasing.EaseBuilder bl = new();
                        bl.Insert(0, ValueEasing.Stable(0));
                        bl.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 8, BeatTime(0.25f)));
                        bl.Insert(BeatTime(2.75f), ValueEasing.EaseOutCubic(8, 0, BeatTime(2.75f)));
                        bl.Run((x) =>
                        {
                            Blur.Sigma = x * 2;
                            splitter.Intensity = 1 - x * 0.1f;
                            StepSample.Intensity = 0.01f + x * 0.01f;
                        });
                    }
                    //写个色散+模糊的blur
                });
                RegisterFunctionOnce("soulR", () => { SetSoul(0); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                game.RegisterFunctionOnce("convL", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(2, game.BeatTime(1), false);
                });
                game.RegisterFunctionOnce("convR", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(2, game.BeatTime(1), true);
                });
                RegisterFunctionOnce("soulG", () => { SetSoul(1); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder v = new();
                    ValueEasing.EaseBuilder vb1 = new();
                    ValueEasing.EaseBuilder vb2 = new();
                    ValueEasing.EaseBuilder co = new();
                    v.Insert(game.BeatTime(1), ValueEasing.EaseOutSine(0, 2.4f, game.BeatTime(1)));
                    v.Insert(game.BeatTime(1), ValueEasing.EaseInSine(2.4f, 0, game.BeatTime(1)));
                    vb1.Insert(0, ValueEasing.Stable(240));
                    vb1.Insert(BeatTime(2), ValueEasing.EaseInQuad(240, 0, BeatTime(2)));
                    vb1.Insert(BeatTime(1), ValueEasing.Stable(0));
                    vb2.Insert(0, ValueEasing.Stable(240));
                    vb2.Insert(BeatTime(2), ValueEasing.EaseInQuad(240, 0, BeatTime(2)));
                    vb2.Insert(BeatTime(1), ValueEasing.Stable(0));
                    co.Insert(BeatTime(2), ValueEasing.Linear(0.1f, 0.99f, BeatTime(2)));
                    co.Insert(BeatTime(4), ValueEasing.Stable(0.99f));
                    v.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s * 0.2f;
                        splitter.Intensity = 1 + s;
                    });
                    vb1.Run((s) => { ScreenDrawing.UpBoundDistance = s; });
                    vb2.Run((s) => { ScreenDrawing.DownBoundDistance = s; });
                    co.Run((s) => { ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.DarkRed, s); });
                    ScreenDrawing.CameraEffect.SizeShrink(0.45f, BeatTime(2));
                });
                RegisterFunctionOnce("WaveR", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(0, ValueEasing.Stable(0));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0, 7.2f, BeatTime(1)));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(7.2f, -3.6f, BeatTime(1)));
                    a.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-3.6f, 0, BeatTime(1.5f)));
                    a.Insert(0, ValueEasing.Stable(0));
                    a.Run((s) => { ScreenDrawing.ScreenAngle = s * 0.3f; });
                });
                RegisterFunctionOnce("wLineL", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, Rand(100, 300)));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -1), new(0, 0.2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(-32, -20)));
                    CreateEntity(l);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 0.5f, () =>
                        {
                            Line ls = l.Split(false);
                            CreateEntity(ls);
                            ls.Alpha = 0.7f;
                            ls.AlphaDecrease(BeatTime(3));
                        });
                    }
                    LineShadow(6, 0.4f, 8, l);
                });
                RegisterFunctionOnce("WaveL", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    ValueEasing.EaseBuilder alp = new();
                    a.Insert(0, ValueEasing.Stable(0));
                    alp.Insert(0, ValueEasing.Stable(0.85f));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0, -7.2f, BeatTime(1)));
                    a.Insert(BeatTime(1), ValueEasing.EaseOutQuad(-7.2f, 3.6f, BeatTime(1)));
                    a.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(3.6f, 0, BeatTime(1.5f)));
                    a.Insert(0, ValueEasing.Stable(0));
                    alp.Insert(BeatTime(3), ValueEasing.EaseOutBounce(0.85f, 0, BeatTime(3)));
                    a.Run((s) => { ScreenDrawing.ScreenAngle = s * 0.3f; });
                });
                RegisterFunctionOnce("wLineR", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, Rand(100, 300)));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -1), new(0, 0.2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(180 + Rand(20, 32)));
                    CreateEntity(l);
                    for (int a = 0; a < 4; a++)
                    {
                        DelayBeat(a * 0.5f, () =>
                        {
                            Line ls = l.Split(false);
                            ls.Alpha = 0.7f;
                            CreateEntity(ls);
                            ls.AlphaDecrease(BeatTime(3));
                        });
                    }
                    LineShadow(6, 0.4f, 8, l);
                });
                RegisterFunctionOnce("RDcross", () =>
                {
                    float random = Rand(1, 4);
                    CentreEasing.EaseBuilder ce1 = new();
                    CentreEasing.EaseBuilder ce2 = new();
                    ValueEasing.EaseBuilder alp = new();
                    ce1.Insert(0, CentreEasing.Stable(new(320, 240)));
                    ce2.Insert(0, CentreEasing.Stable(new(320, 240)));
                    alp.Insert(0, ValueEasing.Stable(0.85f));
                    alp.Insert(BeatTime(2), ValueEasing.EaseOutSine(0.85f, 0, BeatTime(2)));
                    if (random == 1)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 0; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 0; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else if (random == 2)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else if (random == 3)
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240 + 60), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240 - 60), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                    else
                    {
                        ce1.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 60, 240), BeatTime(1.5f)));
                        ce2.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 60, 240), BeatTime(1.5f)));
                        Line a = new(ce1.GetResult(), (s) => { return 90; }) { Alpha = 0.85f };
                        Line b = new(ce2.GetResult(), (s) => { return 90; }) { Alpha = 0.85f };
                        Line[] l = { a, b };
                        foreach (Line L in l)
                        {
                            CreateEntity(L);
                            L.InsertRetention(new(3, 0.65f));
                            alp.Run((s) => { L.Alpha = s; });
                            DelayBeat(2, () =>
                            {
                                L.Dispose();
                            });
                        }
                    }
                });
                RegisterFunctionOnce("FakeNotes", () =>
                {
                    for (int a = 0; a < 5; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(6 + a * 0.1f, 0), new(-0.2f, 0.4f)));
                        FakeNote.LeftNote note = new(BeatTime(4), 6 + a * 0.1f, Rand(0, 1), 0, CentreEasing.Accerlating(new(9 + a * 1f, 0), new(-0.05f, 0.1f)), BeatTime(4));
                        note.Offset = new(Rand(-200, 0), Rand(-30, 30));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(4), ValueEasing.Stable(180));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(180, 180 + Rand(50, 90), BeatTime(4)));
                        ve.Run((s) => { note.Rotation = 180; });
                        CreateEntity(note);
                    }
                    for (int a = 0; a < 5; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(-6 + a * 0.1f, 0), new(0.2f, 0.4f)));
                        FakeNote.RightNote note = new(BeatTime(4), 6 + a * 0.1f, Rand(0, 1), 0, CentreEasing.Accerlating(new(-9 + a * 1f, 0), new(+0.05f, 0.1f)), BeatTime(4));
                        note.Offset = new(Rand(0, 200), Rand(-30, 30));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(4), ValueEasing.Stable(180));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(180, 180 + Rand(50, 90), BeatTime(4)));
                        ve.Run((s) => { note.Rotation = 0; });
                        CreateEntity(note);
                    }
                });
                CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                {
            "($3)","","","",   "($3)","","","",
            "($3)","","","",   "($3)","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R(WaveR)(wLineR)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R(WaveR)(wLineR)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R(WaveL)(wLineL)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "R(WaveL)(wLineL)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R(WaveR)(wLineR)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R(WaveR)(wLineR)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R(WaveL)(wLineL)","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","(RotateL)","",
            //
            "(R)(Bound)(lineL)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateR)","",
            "(+0)(lineR)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateR)","",

            "(R)(lineR)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateL)","",
            "(+0)(lineL)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateL)","",

            "(R)(lineL)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateR)","",
            "(+0)(lineR)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateR)","",

            "(R)(lineR)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateL)","",
            "(+0)(lineL)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateL)","",
            //
            "(R)(lineL)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateR)","",
            "(+0)(lineR)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateR)","",

            "(R)(lineR)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateL)","",
            "(+0)(lineL)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateL)","",

            "(R)(lineL)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "R","","(RotateR)","",
            "(+0)(lineR)","","+0","",    "+0","","","",
            "+0(RDcross)","","","",    "+0","","(RotateR)","",

            "(R)(lineR)","","","",    "+0","","+0","",
            "+0(RDcross)","","","",    "+0","","(RotateL)","",
            "(R'1.2)","","","",    "(R'1.2)","","","",
            "(R'1.2)(RDcross)","","","",    "(R'1.2)","","","",
            //
            "(upload)(soulR)(convL)","","","",   "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "(upload)(soulG)(convR)","","","",   "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",
            "","","","",    "","","","",

            "(upload)(^R)","","","",    "(^+0)","","","",
            "(^R)","","","",    "(^+0)","","","",
            "(^R)(FakeNotes)","","","",    "(^+0)","","","",
            "(^R)","","","",    "(^+0)","","","",

            "(upload)($0)","","(+0)","",    "(+0)","","+0","",
            "($1)","($1)","($1)","($1)",    "($1)","($1)","($1)","($1)",
            "Blur1","","","",   "","","","",
            "","","","",   "","","","",
                });
            }//zKronO's turn!
            void EasyPart2()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                RegisterFunctionOnce("soulR", () => { SetSoul(0); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("soulB", () => { SetSoul(2); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Change", () =>
                {
                    game.DelayBeat(3, () => { SetBox(new Vector2(320, 240), 240, 240); });
                });
                RegisterFunctionOnce("atk1", () =>
                {
                    game.DelayBeat(0, () =>
                    {
                        Heart.GiveForce(180, 8);
                        CreateEntity(new Boneslab(0, 160, game.BeatTime(0.5f), game.BeatTime(4)));
                        CreateEntity(new Boneslab(90, 100, game.BeatTime(0.5f), game.BeatTime(2)));
                        CreateEntity(new Boneslab(270, 100, game.BeatTime(0.5f), game.BeatTime(2)));
                    });
                    game.DelayBeat(2.5f, () =>
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(i * 0.5f, () =>
                            {
                                UpBone b1 = new(true, 4, 80) { ColorType = Rand(1, 2) };
                                UpBone b2 = new(false, 4, 80) { ColorType = Rand(1, 2) };
                                CreateBone(b1);
                                CreateBone(b2);
                                AddInstance(new TimeRangedEvent(1145, () =>
                                {
                                    b1.Speed -= 0.075f;
                                    b2.Speed -= 0.075f;
                                }));
                            });
                        }
                    });
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    Heart.GiveForce(0, 12);
                    for (int i = 0; i < 4; i++)
                    {
                        game.DelayBeat(i * 1, () =>
                        {
                            CreateEntity(new Boneslab(0, 15, game.BeatTime(2), game.BeatTime(0.23f)));
                            game.DelayBeat(2, () =>
                            {
                                PlaySound(Sounds.pierce);
                            });
                        });
                    }
                    game.DelayBeat(6, () =>
                    {
                        SetSoul(0);
                        ValueEasing.EaseBuilder v = new();
                        v.Insert(game.BeatTime(1.5f), ValueEasing.EaseInCubic(0, 110, game.BeatTime(1.5f)));
                        v.Insert(game.BeatTime(1.5f), ValueEasing.EaseOutQuart(110, 0, game.BeatTime(1.5f)));
                        v.Run((s) =>
                        {

                        });
                        for (int i = 0; i < 36; i++)
                        {
                            SideCircleBone b = new(i * 10, 8, 50, game.BeatTime(2.85f));
                            //CreateBone(b);
                        }

                    });
                    game.DelayBeat(9, () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(270, 12);
                        for (int i = 0; i < 2; i++)
                        {
                            game.DelayBeat(i * 4 + 2, () =>
                            {
                                CreateBone(new LeftBone(true, 3, 160 + 15));
                                CreateBone(new RightBone(true, 3, 30 - 15));
                                PlaySound(Sounds.pierce);
                            });
                            game.DelayBeat(i * 4 + 4, () =>
                            {
                                CreateBone(new LeftBone(false, 3, 160 + 15));
                                CreateBone(new RightBone(false, 3, 30 - 15));
                                PlaySound(Sounds.pierce);
                            });
                        }
                    });
                });
                RegisterFunctionOnce("atk3", () =>
                {
                    SetSoul(0);
                    Heart.GiveInstantForce(0, 0);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(game.BeatTime(1.5f), ValueEasing.EaseInCubic(0, 90, game.BeatTime(1.5f)));
                    v.Insert(game.BeatTime(1.5f), ValueEasing.EaseOutQuart(90, 0, game.BeatTime(1.5f)));
                    v.Run((s) =>
                    {
                        InstantSetBox(new Vector2(320, 240), 240 - s, 240 - s);
                    });
                    for (int i = 0; i < 36; i++)
                    {
                        SideCircleBone b = new(i * 10, 8, 50, game.BeatTime(3));
                        CreateBone(b);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        // CreateBone(new CentreCircleBone(90 * i + 140, 2f, 300, game.BeatTime(3)) { IsMasked = true }); ;

                    }
                    game.DelayBeat(6.75f, () =>
                    {
                        SetGreenBox();
                    });
                });
                RegisterFunctionOnce("Bones1", () =>
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new LeftBone(true, 6, 70));
                    CreateBone(new RightBone(false, 6, 70));
                });
                RegisterFunctionOnce("Scale+", () =>
                {
                    ScreenDrawing.ScreenScale += 0.05f;
                });
                RegisterFunctionOnce("Scale++", () =>
                {
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("ScaleRet", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(2), 1, 0.07f);
                });
                RegisterFunctionOnce("RandomSniperBone", () =>
                {

                    for (int a = 0; a < 1; a++)
                    {
                        float rot = Rand(10, 80);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(BoxStates.Left - 40, BoxStates.Up - 40));
                        ce.Insert(BeatTime(4), CentreEasing.Linear(MathUtil.GetVector2(5f, rot)));
                        CustomBone cb = new(new(0, 0), ce.GetResult(), rot + 90, 35);
                        CreateBone(cb);
                    }

                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("soulG", () => { SetSoul(1); TP(new(BoxStates.Centre.X, BoxStates.Centre.Y)); });
                RegisterFunctionOnce("Bound1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(0, 30, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(30, 10, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(10, 40, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(40, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(20, 50, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(50, 30, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(30, 60, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(60, 40, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(40, 70, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(70, 50, BeatTime(0.25f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = s + 90; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), false);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), false);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(0, 0), new(240, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(-240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(-120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(0, 0), new(380, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), true);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), true);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(640, 0), new(400, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(640, 0), new(480, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(640, 0), new(260, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("LeftLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(660, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.5f));
                    LineShadow(3, 0.4f, 2, l);
                });
                RegisterFunctionOnce("RightLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(-20, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.5f));
                    LineShadow(3, 0.4f, 2, l);
                });
                RegisterFunctionOnce("MidLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(0, 0), new(360, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1f));
                    LineShadow(3, 0.4f, 2, l);
                    l.TransverseMirror = true;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 1.1f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseInQuad(1.1f, 1f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                });
                RegisterFunctionOnce("MidOutLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(320, 0), new(660, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1f));
                    LineShadow(3, 0.1f, 3, l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("MakeF", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                });
                RegisterFunctionOnce("MakeF2", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
            "Change","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "soulB","","","",   "atk1","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "atk2","","","",
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

            "","","","",   "","","atk3","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "RandomSniperBone","","","",   "RandomSniperBone","","","",
            "RandomSniperBone","","RandomSniperBone","",   "","","RandomSniperBone","",
            "","","RandomSniperBone","",   "","","RandomSniperBone","",
            "","","RandomSniperBone","",   "RandomSniperBone","soulG","","",
            //green
            "(R)(LeftLine1)","","","",   "","","","",
            "","","","",   "R","","","",
            "+0","","","",   "","","","",
            "","","","",   "","","","",

            "(R)(RightLine1)","","","",   "","","","",
            "","","","",   "R","","","",
            "+0","","","",   "","","","",
            "","","","",   "(R)(LeftLine2)","","","",

            "(+0)(LeftLine2)","","","",   "","","","",
            "","","","",   "(R)(RightLine2)","","","",
            "(+0)(RightLine2)","","","",   "","","","",
            "","","","",   "","","","",

            "(+0)MidLine","","","",   "","","","",
            "","","","",   "","","","",
            "","MakeF","Scale+","MakeF",   "Scale+","MakeF","Scale+","MakeF",
            "Scale+","MakeF","Scale+","MakeF",   "Scale+","MakeF","Scale+","MakeF",
            //
            "+0(RightLine1)(ScaleRet)","","","",   "","","","",
            "","","","",   "R","","","",
            "+0","","","",   "","","","",
            "","","","",   "","","","",

            "(R)(LeftLine1)","","","",   "","","","",
            "","","","",   "R","","","",
            "+0","","","",   "","","","",
            "","","","",   "(R)(RightLine2)","","","",

            "(+0)(RightLine2)","","","",   "","","","",
            "","","","",   "(R)(LeftLine2)","","","",
            "+0(LeftLine2)","","","",   "","","","",
            "","","","",   "","","","",

            "(+0)MidLine","","","",   "","","","",
            "","","","",   "","","","MakeF2",
            "(MidOutLine)(Scale++)","","","MakeF2",   "(MidOutLine)(Scale++)","","","MakeF2",
            "(MidOutLine)(Scale++)","","","MakeF2",   "(MidOutLine)(Scale++)","","","",
            "(MidOutLine)(ScaleRet)(MakeF2)","","","",
                    //
                });
            }//ParaDOXXX's turn!
            void EasyPart3()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                ScreenDrawing.BoundColor = Color.DarkRed * 0.6f;
                RegisterFunctionOnce("BoundStart", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0, 60, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                });
                RegisterFunctionOnce("Bound1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(60));
                    for (int a = 0; a < 8 * 8 - 8; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(60 + a * 2, 120 + a * 2, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutSine(120 + a * 2, 60 + (a + 1) * 2, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(60 + 56 * 2, 0, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(56), ValueEasing.Linear(0, 1f, BeatTime(56)));
                    ve2.Run((s) => { ScreenDrawing.BoundColor = Color.Lerp(Color.DarkRed, Color.White * 0.75f, s) * 0.6f; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    Line l = new(CentreEasing.EaseOutCubic(new(0, 0), new(640, 0), BeatTime(1f)), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    l.AlphaDecrease(BeatTime(1));
                    CreateEntity(l);
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutCubic(new(640, 0), new(0, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    l.AlphaDecrease(BeatTime(1));
                    CreateEntity(l);
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("ConvL1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.6f), false);
                });
                RegisterFunctionOnce("ConvR1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.6f), true);
                });
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 10, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(10, 0, BeatTime(0.25f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.33f), ValueEasing.EaseOutQuart(0, 10, BeatTime(0.33f)));
                    ve.Insert(BeatTime(0.33f), ValueEasing.EaseOutQuad(10, 0, BeatTime(0.33f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(20, 0, BeatTime(0.75f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("ScaleIn", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(0.75f), ScreenDrawing.ScreenScale + 0.175f, 0.1f);
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(0.5f), 1, 0.15f);
                });
                RegisterFunctionOnce("FinalLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(0, 0), new(190, 0), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(190, 0), new(380, 0), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(380, 0), new(650, 0), BeatTime(0.75f)));
                    ValueEasing.EaseBuilder rot = new();
                    rot.Insert(0, ValueEasing.Stable(90));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(90, 80, BeatTime(0.75f)));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(80, 70, BeatTime(0.75f)));
                    rot.Insert(BeatTime(0.75f), ValueEasing.EaseOutQuad(70, 60, BeatTime(0.75f)));
                    Line l = new(ce.GetResult(), rot.GetResult());
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    LineShadow(10, l);
                    DelayBeat(1.5f, () => { l.AlphaDecrease(BeatTime(1f)); });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                });
                bool another = false;
                if (!another)
                    CreateChart(BeatTime(7.5f), BeatTime(0.5f), 5, new string[]
                    {

            "R","","","",   "+0","","","",
            "+0","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "R","","","",   "","","","",

            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "R","","","",   "+0","","","",
            "+0","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "R","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "R","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "R","","+0","",   "+0","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "R","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                    });
                if (!another)
                    CreateChart(BeatTime(4), BeatTime(1f), 6, new string[]
                    {
                "","","","",   "","","","",
                "","","","",   "","","","",

                "BoundStart(LeftLine1)(ConvL1)","","","",   "(Bound1)(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                //
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(RightLine1)(ConvR1)","","","",   "(RightLine1)(ConvR1)","","","",

                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)(Blur1)","","","",   "(LeftLine1)(ConvL1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","","","",

                "(RightLine1)(ConvR1)(Blur1)","","","",   "(RightLine1)(ConvR1)","","","",
                "(LeftLine1)(ConvL1)","","","",   "(LeftLine1)(ConvL1)","(Blur2)","","",
                "(ScaleIn)(FinalLine)(Flicker)","","","(Blur2)",   "","","(ScaleIn)(Flicker)","",
                "","(Blur2)","","",   "(ScaleBack)(Flicker)","","(Blur3)","",
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
                        //
                    });
            }//Tlottgodinf's turn!
            void EasyPart4()
            {

                RegisterFunctionOnce("soulG", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("crossbone1", () =>
                {
                    DrawingUtil.CrossBone(new Vector2(320 - 135, Heart.Centre.Y), new Vector2(4, 0), 30, 2, Rand(1, 2));
                    DrawingUtil.CrossBone(new Vector2(320 + 135, Heart.Centre.Y), new Vector2(-4, 0), 30, 2, LastRand);
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("crossbone2", () =>
                {
                    DrawingUtil.CrossBone(new Vector2(320, 120), new Vector2(0, 4), 30, 2, 2);
                    DrawingUtil.CrossBone(new Vector2(320, 360), new Vector2(0, -4), 30, 2, 2);
                    PlaySound(Sounds.pierce);

                });
                RegisterFunctionOnce("atk1", () =>
                {
                    SetSoul(0);
                    Heart.Speed = 3.25f;
                    SetBox(new Vector2(320, 240), 240, 180);
                    float t = 0;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(16), ValueEasing.Linear(360 / BeatTime(3.3f) / 2));
                    ve.Run((s) => { t = s; });
                    float t2 = 0;
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(16), ValueEasing.Linear(360 / BeatTime(3.5f) / 2));
                    ve2.Run((s) => { t2 = s; });
                    for (int i = 5; i < BeatTime(5); i++)
                    {
                        AddInstance(new InstantEvent(i * 3, () =>
                        {

                            CreateBone(new DownBone(true, 6, Sin(t) * 50f + 50) { MarkScore = false });
                            CreateBone(new UpBone(true, 6, -Sin(t) * 50f + 50) { MarkScore = false });
                            CreateBone(new DownBone(false, 5, Cos(t2 - 30) * 50f + 50) { MarkScore = false });
                            CreateBone(new UpBone(false, 5, -Cos(t2 - 30) * 50f + 50) { MarkScore = false });
                        }));
                    }
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    SetSoul(0);
                    Heart.Speed = 3.2f;
                    SetBox(new Vector2(320, 240), 180, 240);
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ScreenDrawing.BoundColor = Color.DarkRed;
                    ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = 0.1f;
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInCubic(0, 0.72f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0.72f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s * 0.2f;
                        StepSample.Intensity = 0.01f + s * 6f;
                        splitter.Intensity = 1f + 7f * s;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 8, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutSine(8, 0, BeatTime(0.25f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;

                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuart(0, 20, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(20, 0, BeatTime(0.5f)));
                    ve.Run((s) =>
                    {
                        StepSample.Intensity = s * 0.01f;
                        Blur.Sigma = s * 0.25f;
                    });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    Line[] ls = GetAll<Line>("A");
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1f), CentreEasing.EaseOutQuint(new(0, 0), new(380, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Tags = new string[] { "A" } };
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(1, () => { l.AlphaDecrease(BeatTime(1f)); });
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    Line[] ls = GetAll<Line>("A");
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(1f), CentreEasing.EaseOutQuint(new(320, 0), new(-15, 0), BeatTime(1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Tags = new string[] { "A" } };
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(1, () => { l.AlphaDecrease(BeatTime(1f)); });
                    LineShadow(5, l);
                });
                RegisterFunctionOnce("WarnLineBlue", () =>
                {
                    Line l = new(new Vector2(320, 260), 90) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(new Vector2(300, 240), 0) { DrawingColor = Color.CornflowerBlue };
                    CreateEntity(l);
                    CreateEntity(l2);
                    l.AlphaDecrease(BeatTime(0.35f));
                    l2.AlphaDecrease(BeatTime(0.35f));
                    l.TransverseMirror = true;
                    l2.VerticalMirror = true;
                });
                RegisterFunctionOnce("WarnLineRed", () =>
                {
                    Line l = new(new Vector2(320, 260), 90) { DrawingColor = Color.LightCoral };
                    Line l2 = new(new Vector2(300, 240), 0) { DrawingColor = Color.LightCoral };
                    CreateEntity(l);
                    CreateEntity(l2);
                    l.AlphaDecrease(BeatTime(0.35f));
                    l2.AlphaDecrease(BeatTime(0.35f));
                    l.TransverseMirror = true;
                    l2.VerticalMirror = true;
                });
                RegisterFunctionOnce("Scales", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutCubic(1, 1.15f, BeatTime(1)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuart(1.15f, 1.33f, BeatTime(1.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(1.33f, 1.15f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1.15f, 1, BeatTime(1)));
                    ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                });
                RegisterFunctionOnce("SmallFlicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.25f);
                });
                RegisterFunctionOnce("StepFollow", () =>
                {
                    ForBeat(16, () => { StepSample.CentreX = Heart.Centre.X; StepSample.CentreY = Heart.Centre.Y; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 0.08f, BeatTime(1)));
                    ve.Insert(BeatTime(14), ValueEasing.Stable(0.08f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0.08f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                });
                RegisterFunctionOnce("SmallBlur", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseInSine(0, 10, BeatTime(1)));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(10, 0, BeatTime(1)));
                    ve.Run((s) => { Blur.Sigma = s; });
                });
                RegisterFunctionOnce("StepFollow2", () =>
                {
                    ForBeat(8, () => { StepSample.CentreX = Heart.Centre.X; StepSample.CentreY = Heart.Centre.Y; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 0.08f, BeatTime(1)));
                    ve.Insert(BeatTime(6), ValueEasing.Stable(0.08f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0.08f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                });
                RegisterFunctionOnce("SuddenLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(0.5f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutCubic(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    LineShadow(6, l);
                    l.AlphaDecrease(BeatTime(0.75f));
                });
                RegisterFunctionOnce("SuddenLine2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(0.8f), false);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutCubic(new(0, 0), new(280, 0), BeatTime(0.75f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    LineShadow(6, l);
                    l.AlphaDecrease(BeatTime(0.75f));
                });
                float times = -7.5f;
                float count = 0.2f;
                RegisterFunctionOnce("GravityLine", () =>
                {
                    float randomnumber1 = Rand(-20, 20);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, Rand(200, 400)));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, times), new(0, count)));
                    Line l = new(ce, ValueEasing.Stable(randomnumber1)) { Alpha = 0.7f };
                    CreateEntity(l);
                    l.InsertRetention(new Line.RetentionEffect(BeatTime(0.125f), 0.5f));
                    times -= 1;
                    count += 0.03f;
                });
                RegisterFunctionOnce("FinalShake", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(4, BeatTime(0.125f), false);
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                    DelayBeat(0.125f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(0.125f), true);
                        ScreenDrawing.MakeFlicker(Color.Silver * 0.25f);
                    });
                });
                RegisterFunctionOnce("SilverShake", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.35f);
                    ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1f), RandBool());
                });
                RegisterFunctionOnce("Over", () =>
                {
                    Line[] ls = GetAll<Line>();
                    for (int i = 0; i < ls.Length; i++)
                    {
                        int x = i;
                        ls[x].Dispose();
                    }
                    ScreenDrawing.MakeFlicker(Color.Silver);
                });
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
            "(^R'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "R(WarnLineBlue)","","(WarnLineBlue)","",
            "+0(WarnLineBlue)","","","",   "","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "R(WarnLineBlue)","","(WarnLineBlue)","",
            "+0(WarnLineBlue)","","","",   "","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "R(WarnLineBlue)","","(WarnLineBlue)","",
            "+0(WarnLineBlue)","","","",   "","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "R(WarnLineBlue)","","(WarnLineBlue)","",   "(WarnLineBlue)","","(WarnLineBlue)","",
            "(WarnLineBlue)","","(WarnLineBlue)","",   "R(WarnLineBlue)","","(WarnLineBlue)","",
            "+0(WarnLineBlue)","","","",   "","","","",

            "(atk1)(StepFollow)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SuddenLine1)","","","",   "(SuddenLine2)soulG","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineRed)","","","",
            "R(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "R(WarnLineRed)","","(WarnLineRed)","",
            "+0(WarnLineRed)","","","",   "","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineRed)","","","",
            "R(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "R(WarnLineRed)","","(WarnLineRed)","",
            "+0(WarnLineRed)","","","",   "","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineRed)","","","",
            "R(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "R(WarnLineRed)","","(WarnLineRed)","",
            "+0(WarnLineRed)","","","",   "","","Blur3","",

            "(^R'1.4)","","","",   "(WarnLineRed)","","","",
            "R(WarnLineRed)","","(WarnLineRed)","",   "(WarnLineRed)","","(WarnLineRed)","",
            "(WarnLineRed)","","(WarnLineRed)","",   "R(WarnLineRed)","","(WarnLineRed)","",
            "+0(WarnLineRed)","","","",   "","","","",

            "(atk2)(StepFollow2)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "(SmallBlur)","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "soulG","","","",

            "(^R'1.2)(SilverShake)(GravityLine)","","","(^R'1.2)(SilverShake)(GravityLine)",   "","","(^R'1.2)(SilverShake)(GravityLine)","",
            "","","(^R'1.2)(SilverShake)(GravityLine)","",   "(^R'1.2)(SilverShake)(GravityLine)","","(^R'1.2)(SilverShake)(GravityLine)","",
            "(R'1.2)(GravityLine)(FinalShake)","+0'1.2","","(+0'1.2)(GravityLine)(FinalShake)",   "+0'1.2","","(R'1.2)(GravityLine)(FinalShake)","+0'1.2",
            "","(+0'1.2)(GravityLine)(FinalShake)","^+0'1.3","",      "!!3","(>^$0'1.5)(GravityLine)(FinalShake)","(>^+0'1.5)","(<^$0'1.5)(GravityLine)(FinalShake)",
            "(<^+0'1.5)(Over)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "Blur","","","",   "","","","",
                });
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
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
            //
            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",

            "Scales(Line1)(Flicker)","","","",   "","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
            "","","Blur2","",   "Line1(SmallFlicker)","","Blur2","",
            "Line2(SmallFlicker)","","","",   "","","","",
                });
            }//zKronO's turn!
            void EasyPart5()
            {
                float del = 50;
                ScreenDrawing.UISettings.RemoveUISurface();
                RegisterFunctionOnce("Blur1", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(2), ValueEasing.EaseInCubic(0, -0.32f, BeatTime(2)));
                    e1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(-0.32f, 0, BeatTime(2)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Polar.Intensity = s;
                    });
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;
                        ScreenDrawing.ScreenScale += s * 0.017f;
                    });
                });
                RegisterFunctionOnce("Blur2", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(1), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(1)));
                    e1.Insert(BeatTime(1), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(1)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("Blur3", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseInQuint(0, 0.2f, BeatTime(0.25f)));
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuint(0.2f, 0, BeatTime(0.25f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {

                        StepSample.Intensity = 0.01f + s;


                    });
                });
                RegisterFunctionOnce("Blur4", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseInQuint(0, 0.2f, BeatTime(0.5f)));
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuint(0.2f, 0, BeatTime(0.5f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        StepSample.Intensity = 0.01f + s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("Blur5", () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.5f), ValueEasing.EaseInQuint(0, 0.6f, BeatTime(0.5f)));
                    e1.Insert(BeatTime(3.5f), ValueEasing.EaseOutQuint(0.6f, 0, BeatTime(3.5f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.05f);
                });
                RegisterFunctionOnce("Bound", () =>
                {
                    ForBeat120(12, () =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Lerp(Color.White, Color.Red, 0.21f), 0.4f) * 0.5f;
                    });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(16), ValueEasing.Linear(0, 320, BeatTime(16)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(320, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.LeftBoundDistance = s; ScreenDrawing.RightBoundDistance = s; });
                });
                RegisterFunctionOnce("BlueSoul", () =>
                {
                    HeartAttribute.Gravity = 9f;
                    HeartAttribute.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(320, 260, 128);
                });
                RegisterFunctionOnce("GreenSoul", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("BlueSoul2", () =>
                {
                    HeartAttribute.Gravity = 9f;
                    HeartAttribute.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(150, 260, 128);
                });
                RegisterFunctionOnce("BoneSea", () =>
                {
                    for (int a = 0; a < 32 * 2 * 1.2f; a++)
                        CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 40) { MarkScore = false });

                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), CentreEasing.Stable(500, 335));
                    ve.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(500, 335), new(320, 335), BeatTime(2)));
                    ve.Insert(BeatTime(30), CentreEasing.XSinWave(128, BeatTime(8), 0));
                    Platform p = new(0, new(0, 0), ve.GetResult(), 0, 40);
                    CreateEntity(p);
                    DelayBeat(32, () => { p.Dispose(); });
                });
                RegisterFunctionOnce("BoneSea2", () =>
                {
                    for (int a = 0; a < 32 * 2 * 1.2f; a++)
                        CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 40) { MarkScore = false });

                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), CentreEasing.Stable(500, 165));
                    ve.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(500, 190 + 15), new(320, 190 + 15), BeatTime(2)));
                    ve.Insert(BeatTime(30), CentreEasing.XSinWave(128, BeatTime(8), 0));
                    Platform p = new(0, new(0, 0), ve.GetResult(), 0, 40);
                    CreateEntity(p);
                    DelayBeat(32, () => { p.Dispose(); });
                });
                RegisterFunctionOnce("BoomBone", () =>
                {
                    for (int a = 0; a < Rand(2, 3); a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(Rand(Heart.Centre.X - 10, Heart.Centre.X + 10), Rand(-60, -30)));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.010f, 0.010f), Rand(3.30f, 4.30f)), new(0, Rand(0.10f, 0.20f))));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                        ve.Insert(BeatTime(8), ValueEasing.Accerlating(0, Rand(0.10f, 0.30f) * Someway.Rand0or1()));
                        CustomBone b = new(new(0, 0), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 35 }, IsMasked = false };
                        CreateBone(b);
                    }
                });
                RegisterFunctionOnce("BoomBone2", () =>
                {
                    for (int a = 0; a < Rand(1, 2); a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(Rand(Heart.Centre.X - 10, Heart.Centre.X + 10), 640 + Rand(30, 60)));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-1.60f, 1.60f), Rand(-13.00f, -11.00f)), new(0, Rand(0.15f, 0.20f))));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                        ve.Insert(BeatTime(8), ValueEasing.Accerlating(0, Rand(0.10f, 0.20f) * Someway.Rand0or1()));
                        CustomBone b = new(new(0, 0), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 35 }, IsMasked = false };
                        CreateBone(b);
                    }
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    DrawingUtil.Shock();
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(0.125f), ValueEasing.EaseInQuint(0, 0.1f, BeatTime(0.125f)));
                    e1.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuint(0.1f, 0, BeatTime(0.25f)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {
                        Blur.Sigma = s;
                        splitter.Intensity = 1f + 60f * s;

                    });
                });
                RegisterFunctionOnce("upload", () =>
                {
                    CentreEasing.EaseBuilder v = new();
                    CentreEasing.EaseBuilder vb = new();
                    CentreEasing.EaseBuilder va = new();
                    v.Insert(0, CentreEasing.Stable(new(320, 500)));
                    va.Insert(0, CentreEasing.Stable(new(0, 820)));
                    vb.Insert(0, CentreEasing.Stable(new(640, 820)));
                    v.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(320, 500), new(320, -320), game.BeatTime(1f)));
                    va.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(0, 820), new(0, 0), game.BeatTime(1f)));
                    vb.Insert(game.BeatTime(1f), CentreEasing.EaseOutCubic(new(640, 820), new(640, 0), game.BeatTime(1f)));
                    Line a = new(v.GetResult(), va.GetResult()) { Alpha = 0.55f };
                    Line b = new(v.GetResult(), vb.GetResult()) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    LineShadow(3, 0.9f, 4, a);
                    LineShadow(3, 0.9f, 4, b);
                    game.DelayBeat(4, () => { a.Dispose(); b.Dispose(); });
                });
                RegisterFunctionOnce("ScreenRot+", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(2, BeatTime(2f));
                });
                RegisterFunctionOnce("ScreenRot-", () =>
                {
                    ScreenDrawing.CameraEffect.Rotate(-2, BeatTime(2f));
                });
                RegisterFunctionOnce("BoundEaseBack", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(30 * a, 40 * (a + 1), BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutCubic(40 * (a + 1), 30 * (a + 1), BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.Stable(210));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(210, 140, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(140, 170, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(170, 70, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(70, 100, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(100, 0, BeatTime(0.25f)));
                    ve.Run((s) => { ScreenDrawing.UpBoundDistance = s; ScreenDrawing.DownBoundDistance = s; });
                });
                RegisterFunctionOnce("Con", () => { ScreenDrawing.CameraEffect.Convulse(9, BeatTime(1f), true); });
                RegisterFunctionOnce("Con2", () => { ScreenDrawing.CameraEffect.Convulse(9, BeatTime(1f), false); });
                RegisterFunctionOnce("Line", () =>
                {
                    Line l = new(new Vector2(del * 1.3f, 0), new Vector2(0, del));
                    CreateEntity(l);
                    del += 16;
                    l.AlphaDecrease(BeatTime(0.5f));
                    l.ObliqueMirror = true;
                    l.TransverseMirror = true;
                    l.VerticalMirror = true;
                });
                RegisterFunctionOnce("Mask", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(18), Color.Black, 0.4f);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(BeatTime(16.5f), ValueEasing.Stable(0.4f));
                    v.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.4f, 0, BeatTime(1.5f)));
                    v.Run((s) => { m.alpha = s; });
                    DelayBeat(18, () => { m.Dispose(); });
                });
                CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                {
            "","","","",   "Mask","","","",
            //空拍
            "(Bound)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",
            //正片
            "(D{v})","","","",   "+0{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",

            "(+0{v})","","","",   "+0{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur)","",   "","","","",

            "(+0{v})","","","",   "+0{v}","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","(Blur2)","",   "","","","",

            "($0)(ScaleBack)(upload)","","","",   "+0","","+0","",
            "+0","","","",   "(+0)(Blur3)","","","",
            "(D)(ScreenRot+)","","+0","",   "+0","","","",
            "+0","","Blur3","",   "+0","","(Blur3)","",

            "(R)(ScreenRot-)","","","",   "+0","","+0","",
            "+0","","(Blur3)","",   "(+0)","","","",
            "(D)(ScreenRot-)","","+0","",   "+0","","","",
            "+0","","","",   "+0(Blur4)","","","",

            "R(upload)(ScreenRot+)(Line)","","","",   "","","","",
            "R(ScreenRot+)(Line)","","","",   "(Blur4)","","","",
            "R(upload)(ScreenRot-)(Line)","","","",   "+0(Line)","","","",
            "R(ScreenRot-)(Line)","","+0(Line)","",   "+0(Blur4)(Line)","","+0(Line)","",

            "(R)(upload)(ScreenRot+)(Line)","","","",   "(+0)(Line)","","(+0)(Line)","",
            "","","(+0)(Line)","",   "(+0)(Blur4)(Line)","","","",
            "(R)(ScreenRot+)(Line)","","","",   "(D)(Blur4)(Line)","","","",
            "(D)(Line)","","","",   "(D)(Blur5)(Line)","","","",

            "(D)(BlueSoul)(BoneSea)(upload)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","(Shake)","",

            "(Shake)","(Shake)","(Shake)","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)(GreenSoul)ScreenRot-","","","",   "","","","",

            "R(BoundEaseBack)(Con)","","+0","",   "+0(Con)","","+0","",
            "R(Con)","","+0","",   "+0(Con)","","+0","",
            "R(Con)","","+0","",   "+0(Con)","","+0","",
            "R(Con)","","+0","",   "+0(Con)","","+0","",

            "(BlueSoul2)(BoneSea2)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "ScreenRot-","","","",   "","","","",

            "(Shake)","","(Shake)","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","(Shake)","",

            "(Shake)","(Shake)","(Shake)","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)","","","",   "(Shake)","","","",
            "(Shake)(ScreenRot+)","","(Shake)","",   "(GreenSoul)(Shake)","","","",

            "R(BoundEaseBack)(Con2)","","+0","",   "+0(Con2)","","+0","",
            "+001(Con2)","","+001","",   "+001(Con2)","","+001","",
            "R(Con2)","","+0","",   "+0(Con2)","","+0","",
            "+001(Con2)","","+001","",   "+001(Con2)","","+001","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                });
            }//ParaDOXXX vs zKronO!
            void EasyPart6()
            {
                ScreenDrawing.UISettings.RemoveUISurface();
                Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                RegisterFunctionOnce("RickRoll", () =>
                {
                    Heart.FixArrow = true;
                    ScreenDrawing.ScreenAngle += 90;
                    Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                });
                RegisterFunctionOnce("Scale", () =>
                {
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("ScaleBack", () =>
                {
                    ScreenDrawing.ScreenScale = 1f;
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Rand(480 - 20, 480 + 20), 20));
                    ce.Insert(BeatTime(4f), CentreEasing.Accerlating(new(0, 0), new(0, Rand(0.08f, 0.12f))));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(20, 40)));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(3));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Rand(480 - 20, 480 + 20), 20));
                    ce.Insert(BeatTime(4f), CentreEasing.Accerlating(new(0, 0), new(0, Rand(0.08f, 0.12f))));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(Rand(-40, -20)));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(3));
                });
                RegisterFunctionOnce("CentreLine1", () =>
                {
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, 135, BeatTime(2)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(135, 135 + 45f / 2f, BeatTime(0.5f)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(135 + 45f / 2f, 135 + 45f, BeatTime(0.5f)));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, 360 + 45, BeatTime(2)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(360 + 45, 360 + 45 - 45f / 2f, BeatTime(0.5f)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(360 + 45 - 45f / 2f, 360 + 45 - 45f, BeatTime(0.5f)));
                    Line l1 = new(CentreEasing.Stable(320, 240), ve1.GetResult());
                    l1.Alpha = 0.4f;
                    l1.DrawingColor = Color.Red;
                    Line l2 = new(CentreEasing.Stable(320, 240), ve2.GetResult());
                    l2.Alpha = 0.4f;
                    l2.DrawingColor = Color.Red;
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(2f, () => { l1.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); l2.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); });
                    DelayBeat(2.5f, () => { l1.AlphaIncrease(BeatTime(0.125f), 0.6f); l2.AlphaIncrease(BeatTime(0.125f), 0.6f); });
                    DelayBeat(2.625f, () => { l1.AlphaDecrease(BeatTime(0.65f), 1f); l2.AlphaDecrease(BeatTime(0.65f), 1f); });
                });
                RegisterFunctionOnce("CentreLine2", () =>
                {
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, -135, BeatTime(2)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(-135, -135 - 45f / 2f, BeatTime(0.5f)));
                    ve1.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(-135 - 45f / 2f, -135 - 45f, BeatTime(0.5f)));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutBack(0, -360 - 45, BeatTime(2)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(-360 - 45, -360 - 45 + 45f / 2f, BeatTime(0.5f)));
                    ve2.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(-360 - 45 + 45f / 2f, -360 - 45 + 45f, BeatTime(0.5f)));
                    Line l1 = new(CentreEasing.Stable(320, 240), ve1.GetResult());
                    l1.Alpha = 0.4f;
                    l1.DrawingColor = Color.Red;
                    Line l2 = new(CentreEasing.Stable(320, 240), ve2.GetResult());
                    l2.Alpha = 0.4f;
                    l2.DrawingColor = Color.Red;
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(2f, () => { l1.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); l2.AlphaIncreaseAndDecrease(BeatTime(0.5f), 0.6f); });
                    DelayBeat(2.5f, () => { l1.AlphaIncrease(BeatTime(0.125f), 0.6f); l2.AlphaIncrease(BeatTime(0.125f), 0.6f); });
                    DelayBeat(2.625f, () => { l1.AlphaDecrease(BeatTime(0.65f), 1f); l2.AlphaDecrease(BeatTime(0.65f), 1f); });
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutSine(new(0, 0), new(150, 0), BeatTime(1)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseInSine(new(150, 0), new(-5, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    l.TransverseMirror = true;
                    CreateEntity(l);
                    DelayBeat(2, () => { l.Dispose(); });
                });
                RegisterFunctionOnce("90Arrow1", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("901");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90Arrow2", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("902");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90Arrow3", () =>
                {
                    ValueEasing.EaseBuilder easeBuilder = new();
                    easeBuilder.Insert(0, ValueEasing.Stable(90));
                    easeBuilder.Insert(BeatTime(3f), ValueEasing.EaseOutElastic(90, 0, BeatTime(3f)));
                    Arrow[] ars = GetAll<Arrow>("903");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        easeBuilder.Run((s) => { ars[x].CentreRotationOffset = s; });
                        ars[x].Delay(1 * BeatTime(1 - 0.125f));

                    }
                });
                RegisterFunctionOnce("90", () =>
                {
                    Arrow[] ars = GetAll<Arrow>("901");
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        ars[x].CentreRotationOffset = 90;
                    }
                    Arrow[] ars2 = GetAll<Arrow>("902");
                    for (int a = 0; a < ars2.Length; a++)
                    {
                        int x = a;
                        ars2[x].CentreRotationOffset = 90;
                    }
                    Arrow[] ars3 = GetAll<Arrow>("903");
                    for (int a = 0; a < ars3.Length; a++)
                    {
                        int x = a;
                        ars3[x].CentreRotationOffset = 90;
                    }
                });
                RegisterFunctionOnce("Mask", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(10), Color.Black, 0);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(0, ValueEasing.Stable(0));
                    v.Insert(BeatTime(2.25f), ValueEasing.EaseOutBack(0, 0.3f, BeatTime(2.25f)));
                    v.Insert(BeatTime(1.75f), ValueEasing.Stable(0.3f));
                    v.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(0.3f, 0.4f, BeatTime(0.5f)));
                    v.Insert(BeatTime(1.5f), ValueEasing.Stable(0.4f));
                    v.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(0.4f, 0.5f, BeatTime(0.5f)));
                    v.Insert(BeatTime(2), ValueEasing.Stable(0.5f));
                    v.Insert(BeatTime(0.75f), ValueEasing.EaseInQuad(0.5f, 0.99f, BeatTime(0.5f)));
                    v.Insert(BeatTime(0.4f), ValueEasing.Stable(0.99f));
                    v.Insert(BeatTime(0.35f), ValueEasing.EaseOutCubic(0.99f, 0, BeatTime(0.5f)));
                    v.Run((s) => { m.alpha = s; });
                    DelayBeat(10, () => { m.Dispose(); });
                    ValueEasing.EaseBuilder bd = new();
                    bd.Insert(BeatTime(2), ValueEasing.EaseInSine(ScreenDrawing.DownBoundDistance, 0, BeatTime(2)));
                    bd.Insert(0, ValueEasing.Stable(0));
                    bd.Run((x) => { ScreenDrawing.UpBoundDistance = x; ScreenDrawing.DownBoundDistance = x; });
                    ValueEasing.EaseBuilder scl = new();
                    scl.Insert(BeatTime(2), ValueEasing.Stable(1));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1, 1.04f, BeatTime(2)));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1.04f, 1.09f, BeatTime(2)));
                    scl.Insert(BeatTime(2), ValueEasing.EaseOutBack(1.09f, 1.15f, BeatTime(2)));
                    scl.Insert(BeatTime(0.5f), ValueEasing.Stable(1.15f));
                    scl.Insert(BeatTime(1.25f), ValueEasing.EaseOutSine(1.15f, 1.45f, BeatTime(1.25f)));
                    scl.Insert(BeatTime(1.5f), ValueEasing.EaseOutBack(1.45f, 1, BeatTime(1.5f)));
                    scl.Run((a) => { ScreenDrawing.ScreenScale = a; });
                });
                RegisterFunctionOnce("LeftLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), false);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), false);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), false);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(0, 0), new(240, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(-240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(0, 0), new(160, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(-120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(0, 0), new(380, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RightLine1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(1.5f), true);
                    DelayBeat(1.5f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3f, BeatTime(0.5f), true);
                    });
                    DelayBeat(2f, () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7.5f, BeatTime(2f), true);
                    });
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1.5f) - 1, CentreEasing.EaseOutCubic(new(640, 0), new(400, 0), BeatTime(1.5f)));
                    ce.Insert(1, CentreEasing.Linear(240));
                    ce.Insert(BeatTime(0.5f) - 1, CentreEasing.EaseOutQuad(new(640, 0), new(480, 0), BeatTime(0.5f)));
                    ce.Insert(1, CentreEasing.Linear(120));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuart(new(640, 0), new(260, 0), BeatTime(2f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(2)); });
                    CreateEntity(l);
                });

                RegisterFunctionOnce("H0", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 0, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("H2", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 2, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("H1", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 1, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });

                RegisterFunctionOnce("BoundOutQuad", () =>
                {
                    ScreenDrawing.BoundColor = Color.DarkRed * 0.7f;
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 100, BeatTime(1)));
                    for (int a = 0; a < 48 * 2 - 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(100, 180, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseOutQuad(180, 100, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuart(100, 0, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; });
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                    ScreenDrawing.CameraEffect.Convulse(3, BeatTime(0.25f), RandBool());
                    ScreenDrawing.ScreenScale += 0.02f;
                });

                RegisterFunctionOnce("ScaleLerp", () =>
                {
                    DrawingUtil.LerpScreenScale(BeatTime(1), 1, 0.09f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                {
            "!!3","R(90)","+0","+0",   "+0","","","",
            "R","","","",   "+0","","","",
            "R","","","^R'1.3",   "+0","","","^+0'1.3",
            "+0","","","^+0'1.3",   "+0","","","",

            "(R)(LeftLine1)","","","",   "D","","+0","",
            "+0","","+0","",   "","","(+0)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","","",   "D","","+0","",

            "(R)RightLine1","","","",   "D","","+0","",
            "+0","","+0","",   "","","(+0)","",
            "","","(D)","",   "+0","","+0","",
            "+0","","+0","",   "+0(Scale)","","+0","",

            "+0(RickRoll)(Scale)","","+0","",   "+0(RickRoll)(Scale)","","+0","",
            "+0(RickRoll)(Scale)","","+0","",   "+0(RickRoll)(ScaleBack)","","+0","",
            "Mask","","","",   "","","","",
            "","","","",   "","","","",
            //
            "$0{Down,v}(Line1)","","$0{v}(Line1)","",   "$0{Up,v}(Line1)","","","",
            "","","","",   "$0{v}","","","",
            "$0{Down,v}(Line2)","","$0{v}(Line2)","",   "$0{Up,v}(Line2)","","","",
            "","","","",   "$0{v}","","","",

            "$2{Down,v}(Line1)","","$2{v}(Line1)","",   "$2{Up,v}(Line1)","","","",
            "","","","",   "","","","",
            "$2{v}(Line2)","","","",   "","","$2{v}(Line2)","",
            "","","","",   "","","","(90Arrow1)",

            "(#0.8#R)(CentreLine1)(Line3)(+0{901})","","","",   "","","","",
            "","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","(90Arrow2)",

            "(#0.8#R)(CentreLine2)(Line3)(+0{902})","","","",   "","","","",
            "","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","(90Arrow3)",
            //
            "(#0.8#R)(CentreLine1)(Line3)(+0{903})","","","",   "","","","",
            "","","","",   "+0","","","",
            "+0","","","",   "+0","","","",
            "","","","",   "","","","",

            "R","","","",   "+1","","","",
            "+1","","","",   "($3'1.3)(Flicker)","","(+0'1.3)(Flicker)","",
            "($0'1.3)(Flicker)","($0'1.3)(Flicker)","($0'1.3)(Flicker)","",   "($0'1.3)(Flicker)","","($0'1.3)(Flicker)","",
            "($0'1.3)(Flicker)","($0'1.3)(Flicker)","($0'1.3)(Flicker)","",   "($0'1.3)(Flicker)","","($0'1.3)(Flicker)","",

            "($0'1.3)(BoundOutQuad)(Flicker)(ScaleLerp)","","","",   "","","","",


                });
                CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                {
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
            //
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "R","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
                });
            }//Tlott's turn!
            void EasyPart7()
            {
                RegisterFunctionOnce("H3", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        Arrow arr1 = MakeArrow(BeatTime(3.5f + 0.5f * i), 3, 6, 1, 0);
                        arr1.VoidMode = true;
                        arr1.JudgeType = Arrow.JudgementType.Hold;
                        CreateEntity(arr1);
                    }
                });
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White * 0.85f);
                    ScreenDrawing.ScreenScale += 0.1f;
                });
                RegisterFunctionOnce("soulB", () =>
                {
                    HeartAttribute.Speed = 3.4f;
                    TextPrinter t = new(BeatTime(8), "$Press [;] or \n[Down] to \nDown!", new Vector2(30, 160), new TextAttribute[]
                    {

                new TextSpeedAttribute(114),
                        /*
                                    })
                                    { sound = false };
                                    CreateEntity(t);
                                    SetSoul(2);
                                    InstantSetBox(new Vector2(320, 240), 180, 500);
                                    float y = 60;
                                    ForBeat(13 * 4, () =>
                                    {
                                        InstantTP(new(Heart.Centre.X, y));
                                        HeartAttribute.Gravity = 0;
                                        HeartAttribute.JumpTimeLimit = 0;
                                        if (GameStates.IsKeyDown(InputIdentity.MainDown))
                                        {
                                            y += 0.3f;
                                        }
                                        if (GameStates.IsKeyDown(InputIdentity.MainUp))
                                        {
                                            y -= 0.3f;
                                        }

                                    });
                                    ForBeat(13 * 4, () =>
                                    {
                                        StepSample.CentreX = Heart.Centre.X;
                                        StepSample.CentreY = Heart.Centre.Y;
                                    });
                                    ScreenDrawing.ScreenScale = 1f;
                                    StepSample.Intensity = 0.1f;
                                    HeartAttribute.Speed = 2.1f;
                                    splitter.Intensity = 1 + 2f;
                                });
                                RegisterFunctionOnce("Attack", () =>
                                {
                                    float height1 = 0;
                                    float height2 = 0;
                                    ValueEasing.EaseBuilder hf = new();
                                    ValueEasing.EaseBuilder sinf = new();
                                    hf.Insert(0, ValueEasing.Stable(0));
                                    hf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                                    hf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                                    hf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                                    hf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                                    hf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                                    hf.Insert(BeatTime(4), ValueEasing.SinWave(17, BeatTime(2), -1));
                                    hf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                                    hf.Insert(0, ValueEasing.Stable(0));
                                    hf.Run((s) =>
                                    {
                                        height1 = s;
                                    });
                                    sinf.Insert(0, ValueEasing.Stable(0));
                                    sinf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                                    sinf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                                    sinf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                                    sinf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                                    sinf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                                    sinf.Insert(BeatTime(4), ValueEasing.SinWave(17, BeatTime(2), 0));
                                    sinf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                                    sinf.Insert(0, ValueEasing.Stable(0));
                                    sinf.Run((s) =>
                                    {
                                        height2 = s;
                                    });
                                    for (int i = 0; i < 14 * 4 * 14.2f; i++)
                                    {
                                        AddInstance(new InstantEvent(2 * i, () =>
                                        {
                                            CreateBone(new LeftBone(true, 9, height1) { ColorType = 0, MarkScore = false });
                                            CreateBone(new RightBone(true, 9, height2) { ColorType = 0, MarkScore = false });
                                        }));
                                    }
                                    DelayBeat(5 * 4, () =>
                                    {
                                        for (int i = 0; i < 8; i++)
                                        {
                                            DelayBeat(i + 0.5f, () =>
                        */
                    })
                    { PlaySound = false };
                    CreateEntity(t);
                    SetSoul(2);
                    InstantSetBox(new Vector2(320, 240), 180, 500);
                    float y = 60;
                    ForBeat(13 * 4, () =>
                    {
                        InstantTP(new(Heart.Centre.X, y));
                        HeartAttribute.Gravity = 0;
                        HeartAttribute.JumpTimeLimit = 0;
                        if (GameStates.IsKeyDown(InputIdentity.MainDown))
                        {
                            y += 0.3f;
                        }
                        if (GameStates.IsKeyDown(InputIdentity.MainUp))
                        {
                            y -= 0.3f;
                        }

                    });
                    ForBeat(13 * 4, () =>
                    {
                        StepSample.CentreX = Heart.Centre.X;
                        StepSample.CentreY = Heart.Centre.Y;
                    });
                    ScreenDrawing.ScreenScale = 1f;
                    StepSample.Intensity = 0.1f;
                    HeartAttribute.Speed = 2.1f;
                    splitter.Intensity = 1 + 2f;
                });
                RegisterFunctionOnce("Attack", () =>
                {
                    float height1 = 0;
                    float height2 = 0;
                    ValueEasing.EaseBuilder hf = new();
                    ValueEasing.EaseBuilder sinf = new();
                    hf.Insert(0, ValueEasing.Stable(0));
                    hf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                    hf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                    hf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                    hf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                    hf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                    hf.Insert(BeatTime(4), ValueEasing.SinWave(0, BeatTime(2), -1));
                    hf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                    hf.Insert(0, ValueEasing.Stable(0));
                    hf.Run((s) =>
                    {
                        height1 = s;
                    });
                    sinf.Insert(0, ValueEasing.Stable(0));
                    sinf.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0, 40, BeatTime(4)));
                    sinf.Insert(BeatTime(4 * 4), ValueEasing.Stable(40));
                    sinf.Insert(BeatTime(2), ValueEasing.EaseInSine(40, 25, BeatTime(2)));
                    sinf.Insert(BeatTime(3 * 4 + 2), ValueEasing.Stable(25));
                    sinf.Insert(BeatTime(3 * 4), ValueEasing.Linear(25, 65, BeatTime(3 * 4)));
                    sinf.Insert(BeatTime(4), ValueEasing.SinWave(0, BeatTime(2), 0));
                    sinf.Insert(BeatTime(0.5f), ValueEasing.Linear(65, 0, BeatTime(0.5f)));
                    sinf.Insert(0, ValueEasing.Stable(0));
                    sinf.Run((s) =>
                    {
                        height2 = s;
                    });
                    for (int i = 0; i < 14 * 4 * 14.2f; i++)
                    {
                        AddInstance(new InstantEvent(2 * i, () =>
                        {
                            CreateBone(new LeftBone(true, 9, height1) { ColorType = 0, MarkScore = false });
                            CreateBone(new RightBone(true, 9, height2) { ColorType = 0, MarkScore = false });
                        }));
                    }
                    DelayBeat(5 * 4, () =>
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(i + 0.5f, () =>
                            {
                                PlaySound(Sounds.pierce);
                                float rd = Rand(40, 90);
                                CreateBone(new LeftBone(true, 6, rd) { ColorType = 0 });
                                CreateBone(new RightBone(true, 6, 130 - rd) { ColorType = 0 });
                                DelayBeat(0.125f, () =>
                                {
                                    CreateBone(new LeftBone(true, 6, rd) { ColorType = 0 });
                                    CreateBone(new RightBone(true, 6, 130 - rd) { ColorType = 0 });
                                });
                            });
                        }
                    });
                    DelayBeat(4, () => { CreateEntity(new Boneslab(180, 140, BeatTime(7 * 4), BeatTime(5 * 4)) { ColorType = 0 }); });
                });
                RegisterFunctionOnce("LeftB", () =>
                {
                    float rd = 1;
                    if (rd == 1)
                    {
                        DrawingUtil.CrossBone(new Vector2(Rand(320 - 50, 320 + 10), 500), new Vector2(0, -8), 30, 1, 2);
                    }
                    else if (rd == 2)
                    {
                        CreateBone(new CustomBone(new Vector2(Rand(320 - 50, 320 + 10), 500), Motions.PositionRoute.linear, 0, 30)
                        {
                            PositionRouteParam = new float[] { 0, -7 },
                            ColorType = 0
                        });
                    }
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("Kick1", () =>
                {
                    float rot = Rand(9, 20);
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot)), rot, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270)), 180, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot)), -rot, 40) { ColorType = 0 });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("Kick2", () =>
                {
                    float rot = Rand(9, 20);
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot / 2)), rot / 2, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot / 2)), -rot / 2, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 + rot * 1.5f)), rot * 1.5f, 40) { ColorType = 0 });
                    CreateBone(new CustomBone(new(Heart.Centre.X, 520), CentreEasing.Linear(MathUtil.GetVector2(7.5f, 270 - rot * 1.5f)), -rot * 1.5f, 40) { ColorType = 0 });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("RightB", () =>
                {
                    PlaySound(Sounds.pierce);
                    float rd = 1;
                    if (rd == 1)
                    {
                        DrawingUtil.CrossBone(new Vector2(Rand(320 - 10, 320 + 50), 500), new Vector2(0, -8), 30, 1, 2);
                    }
                    else if (rd == 2)
                    {
                        CreateBone(new CustomBone(new Vector2(Rand(320 - 10, 320 + 50), 500), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 0, -7 },
                            ColorType = 0
                        });
                    }
                });//REMEMBER to ADD these STRING INTO the STRING which under this WORD
                RegisterFunctionOnce("Sounds", () =>
                {
                    for (int a = 0; a < 2; a++) PlaySound(Sounds.destroy);
                });
                RegisterFunctionOnce("FirstBone", () =>
                {
                    for (int a = 0; a < 8; a++)
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 540 + a * 27));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, -Rand(4.00f, 8.00f)), new(0, -Rand(0.040f, 0.090f))));
                        CreateBone(new CustomBone(new(0, 0), ce.GetResult(), 90 + Rand(-30, 30), BoxStates.Width + 80) { ColorType = 2 });
                    }

                });
                CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                {
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
            //
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "($1)(Flicker)","","","",   "","","($1)(Flicker)","",
            "","","","",   "($1)(Flicker)","","","",
            //HALLLLLLLLLLLLLLLLLLLLLL
            "(soulB)(Flicker)(Attack)(Sounds)(FirstBone)","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "(LeftB)","","","",   "(LeftB)","","","",
            "(LeftB)","","(LeftB)","",   "(LeftB)","","","",
            "(LeftB)","","","",   "(LeftB)","","(LeftB)","",
            "","","(LeftB)","",   "(LeftB)","","","",
            //
            "(LeftB)","","","",   "(LeftB)","","","",
            "(LeftB)","","","",   "(LeftB)","","","",
            "(LeftB)","","","",   "(LeftB)","","(LeftB)","",
            "","","(LeftB)","",   "(LeftB)","","(LeftB)","",

            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","(RightB)","",
            "","","(RightB)","",   "(RightB)","","","",

            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","","",
            "(RightB)","","","",   "(RightB)","","(RightB)","",
            "","","(RightB)","",   "(RightB)","","(RightB)","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
                });
                CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                {
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "R","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            //
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "R","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",

            "","","","",   "R(TapEvent)","","","",
            "","","","",   "R(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
            "","","","",   "(TapEvent)","","","",
                });
            }
            void EasyPart8()
            {
                RegisterFunctionOnce("GB", () =>
                {

                    int non = Rand(1, 2);
                    CreateEntity(new NormalGB(new(Heart.Centre.X, 440), new(Heart.Centre.X, 440), new(0.75f, 0.375f), 270, BeatTime(4.5f), BeatTime(0.3f)) { AppearVolume = 0.01f });
                });
                RegisterFunctionOnce("atk2", () =>
                {
                    float length = 0;
                    ValueEasing.EaseBuilder len = new();
                    len.Insert(0, ValueEasing.Stable(64));
                    len.Insert(BeatTime(0.5f), ValueEasing.Linear(80, 120, BeatTime(0.5f)));
                    len.Run((s) =>
                    {
                        length = s;
                    });
                    for (int i = 0; i < 1; i++)
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
                    len.Insert(0, ValueEasing.Stable(64));
                    len.Insert(BeatTime(0.5f), ValueEasing.Linear(80, 120, BeatTime(0.5f)));
                    len.Run((s) =>
                    {
                        length = s;
                    });
                    for (int i = 0; i < 1; i++)
                    {
                        AddInstance(new InstantEvent(i * 1, () =>
                        {
                            CreateBone(new RightBone(true, 12, length) { ColorType = 0 });
                        }));
                    }
                });
                RegisterFunctionOnce("ChangeA", () =>
                {
                    SetBox(120, 180, 500);
                    DelayBeat(0, () =>
                    {
                        //HeartAttribute.Gravity = 9.8f;
                        HeartAttribute.JumpTimeLimit = 1;
                        Heart.GiveForce(0, 8);
                        DelayBeat(0.5f, () =>
                        {
                            PlaySound(Sounds.pierce);
                        });
                    });
                    CreateEntity(new Boneslab(0, 30, BeatTime(1.4f), BeatTime(1)));
                    DrawingUtil.MaskSquare s = new(0, 0, 640, 480, BeatTime(0.9f), Color.Black, 1);
                    DelayBeat(1.5f, () => { TP(320, 240); CreateEntity(s); PlaySound(Sounds.change); DelayBeat(0.5f, () => { s.Dispose(); }); });
                });
                RegisterFunctionOnce("ChangeB", () =>
                {
                    float co1 = 0;
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(BeatTime(2), ValueEasing.Stable(0));
                    a.Insert(1, ValueEasing.Linear(0, 1, 1));
                    a.Run((s) => { co1 = s; });
                    InstantSetBox(new Vector2(320, 240), 160, 160);
                    SetSoul(0);
                    CreateBone(new CentreCircleBone(Rand(0, 359), 6.5f, 140, BeatTime(2)) { ColorType = 1 });
                    CreateBone(new CentreCircleBone(LastRand + 90, -5f, 140, BeatTime(2 * 3)) { ColorType = 1 });
                    for (int i = 0; i < 36; i++)
                    {
                        CreateBone(new SideCircleBone(i * 10, -6, 20, BeatTime(3 * 4 - 2)));
                    }
                    DelayBeat(6, () =>
                    {
                        float rotation = 0;
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(0, ValueEasing.Stable(30));
                        rot.Insert(BeatTime(4), ValueEasing.Linear(30, 390, BeatTime(4)));
                        rot.Run((s) => { rotation = s; });
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(i * 0.5f, () =>
                            {
                                CreateEntity(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(200, rotation), new Vector2(320, -20), new(0.875f, 0.625f), rotation + 180, BeatTime(4), BeatTime(1)));
                            });
                        }
                    });//不要让你的恶习复苏

                });
                RegisterFunctionOnce("SetSoulR", () =>
                {
                    splitter.Intensity = 0f;
                    StepSample.CentreX = 320;
                    StepSample.CentreY = 240;
                    StepSample.Intensity = 0.1f;
                    InstantSetBox(new Vector2(320, 240), 170, 170);
                    SetSoul(0);
                    Heart.Speed = 3f;
                    ScreenDrawing.ScreenScale = 1;
                });
                RegisterFunctionOnce("CentreBone", () =>
                {
                    CentreCircleBone c = new(Rand(0, 359), 4.5f, 180, BeatTime(9)) { ColorType = 1, IsMasked = true };
                    CreateBone(c);
                    DelayBeat(5, () =>
                    {
                        c.ColorType = 2;
                        PlaySound(Sounds.Ding);
                        c.RotateSpeed = 4;
                    });
                });
                float rot = 0;
                RegisterFunctionOnce("Value", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(Rand(0, 359)));
                    ve.Insert(BeatTime(5), ValueEasing.Linear(1));
                    ve.Insert(BeatTime(4), ValueEasing.Linear(2f));
                    ve.Run((s) => { rot = s; });
                    ForBeat(13, () =>
                    {
                        Heart.Speed = GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainUp)
                            ? 3f * 1.414f
                            : GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainUp)
                                ? 3f * 1.414f
                                : GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainDown)
                                                            ? 3f * 1.414f
                                                            : GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight) ? 3f * 1.414f : 3f;
                    });
                });
                RegisterFunctionOnce("CreateGB1", () =>
                {
                    /*
                                    if (GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainUp))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainUp))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainLeft) && GameStates.IsKeyDown(InputIdentity.MainDown))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else if (GameStates.IsKeyDown(InputIdentity.MainDown) && GameStates.IsKeyDown(InputIdentity.MainRight))
                                    {
                                        Heart.Speed = 3f * 1.414f;
                                    }
                                    else
                                    {
                                        Heart.Speed = 3f;
                                    }
                    */
                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(200, rot), new Vector2(320, 240) + MathUtil.GetVector2(300, rot), new(1, 0.5f), rot + 180, BeatTime(2), BeatTime(0.34f)) { AppearVolume = 0 });
                });
                RegisterFunctionOnce("CreateGB2", () =>
                {
                    CreateGB(new NormalGB(new Vector2(320, 240) + MathUtil.GetVector2(170, rot), new Vector2(320, 240) + MathUtil.GetVector2(300, rot), new(1, 0.5f), rot + 180, BeatTime(2), BeatTime(0.25f)) { AppearVolume = 0 });

                });
                RegisterFunctionOnce("Return", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0.1f));
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0.1f, 0, BeatTime(1)));
                    ve.Run((s) => { StepSample.Intensity = s; });
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    TP();
                });
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "GB","","","",
            "","","","",   "","","","",
            "","","","",   "GB","","","",
            "","","","",   "","","","",

            "","","","",   "GB","","","",
            "","","","",   "","","","",
            "","","","",   "GB","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            //
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",

            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",
            "atk2","","","",   "atk3","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "ChangeA","","","",   "","","","",
            "(Value)","","","",   "","","","",
            "SetSoulR(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            //
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB1)","","(CreateGB1)","",   "(CreateGB1)","","(CreateGB1)","",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",

            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",   "(CreateGB2)","(CreateGB2)","(CreateGB2)","(CreateGB2)",
            "","","","",   "","","","",
            "","","","",   "","","","",

            "","","","",   "","","","",
            "","","","",   "","","","",
            "(Return)","","","",   "","","","",
            "","","","",   "","","","",

            "R","","","",   "","","","",
            "+0","","","",   "+0","","","",
            "+0","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",

            "+0","","","",   "","","","",
            "+0","","","",   "+0","","","",
            "+0","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",

            "+0","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","","",
            //
            "+0","","","",   "","","","",
            "+0","","","",   "+0","","","",
            "+0","","","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",

            "+0","","","",   "","","","",
            "+0","","","",   "+0","","","",
            "+0","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",

            "+0","","","",   "","","","",
            "+0","","","",   "+0","","","",
            "+0","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",

            "+0","","","",   "","","","",
            "+0","","","",   "+0","","","",
            "+0","","+0","",   "+0","","+0","",
            "","","+0","",   "+0","","+0","",

            "","","","",   "","","","",

                });
            }//Tlott's turn!
            #endregion
            public void SetOffset(Arrow arrow, float offset)
            {
                if (arrow.Way == 0) arrow.Offset = new(0, offset);
                if (arrow.Way == 1) arrow.Offset = new(offset, 0);
                if (arrow.Way == 2) arrow.Offset = new(0, offset);
                if (arrow.Way == 3) arrow.Offset = new(-offset, 0);
            }
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            #region non
            public void ExtremePlus()
            {
                if (Gametime < 0) return;
            }

            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }
            #endregion
            public void Easy()
            {

                Arrow[] ars = GetAll<Arrow>("Tap");
                for (int a = 0; a < ars.Length; a++)
                {
                    int x = a;
                    ars[x].JudgeType = Arrow.JudgementType.Tap;
                }
                Arrow[] ars2 = GetAll<Arrow>("Up");
                for (int a = 0; a < ars2.Length; a++)
                {
                    int x = a;
                    SetOffset(ars2[x], -12);
                }
                Arrow[] ars3 = GetAll<Arrow>("Down");
                for (int a = 0; a < ars3.Length; a++)
                {
                    int x = a;
                    SetOffset(ars3[x], 12);
                }
                Arrow[] ars4 = GetAll<Arrow>("v");
                for (int a = 0; a < ars4.Length; a++)
                {
                    int x = a;
                    ars4[x].VoidMode = true;
                }

                if (Heart.SoulType != 1)
                {
                    ScreenDrawing.ThemeColor = Color.Lerp(ScreenDrawing.ThemeColor, new(105, 0, 0), 0.08f);
                }
                if (Heart.SoulType == 1)
                {
                    ScreenDrawing.ThemeColor = Color.Lerp(ScreenDrawing.ThemeColor, Color.White, 0.08f);
                }
                if (InBeat(0)) EasyPart1();
                if (InBeat(76)) EasyPart2();
                if (InBeat(16 * 9 + 2f - 4)) EasyPart3();
                if (InBeat(44 * 4)) EasyPart4();
                if (InBeat(60 * 4 - 1)) EasyPart5();
                if (InBeat(76 * 4)) EasyPart6();
                if (InBeat(92 * 4)) EasyPart7();
                if (InBeat(108 * 4)) EasyPart8();
            }
            public void Normal()
            {

                Arrow[] ars = GetAll<Arrow>("Tap");
                for (int a = 0; a < ars.Length; a++)
                {
                    int x = a;
                    ars[x].JudgeType = Arrow.JudgementType.Tap;
                }
                Arrow[] ars2 = GetAll<Arrow>("Up");
                for (int a = 0; a < ars2.Length; a++)
                {
                    int x = a;
                    SetOffset(ars2[x], -12);
                }
                Arrow[] ars3 = GetAll<Arrow>("Down");
                for (int a = 0; a < ars3.Length; a++)
                {
                    int x = a;
                    SetOffset(ars3[x], 12);
                }
                Arrow[] ars4 = GetAll<Arrow>("v");
                for (int a = 0; a < ars4.Length; a++)
                {
                    int x = a;
                    ars4[x].VoidMode = true;
                }

                if (Heart.SoulType != 1)
                {
                    ScreenDrawing.ThemeColor = Color.Lerp(ScreenDrawing.ThemeColor, new(105, 0, 0), 0.08f);
                }
                if (Heart.SoulType == 1)
                {
                    ScreenDrawing.ThemeColor = Color.Lerp(ScreenDrawing.ThemeColor, Color.White, 0.08f);
                }
                if (InBeat(0)) NorPart1();
                if (InBeat(76)) NorPart2();
                if (InBeat(16 * 9 + 2f - 4)) NorPart3();
                if (InBeat(44 * 4)) NorPart4();
                if (InBeat(60 * 4 - 1)) NorPart5();
                if (InBeat(76 * 4)) NorPart6();
                if (InBeat(92 * 4)) NorPart7();
                if (InBeat(108 * 4)) NorPart8();
            }
            public void Extreme()
            {
                if (GameStates.IsKeyPressed120f(InputIdentity.Alternate)) EndSong();
                Arrow[] ars = GetAll<Arrow>("Tap");
                for (int a = 0; a < ars.Length; a++)
                {
                    int x = a;
                    ars[x].JudgeType = Arrow.JudgementType.Tap;
                }
                Arrow[] ars2 = GetAll<Arrow>("Up");
                for (int a = 0; a < ars2.Length; a++)
                {
                    int x = a;
                    SetOffset(ars2[x], -12);
                }
                Arrow[] ars3 = GetAll<Arrow>("Down");
                for (int a = 0; a < ars3.Length; a++)
                {
                    int x = a;
                    SetOffset(ars3[x], 12);
                }
                Arrow[] ars4 = GetAll<Arrow>("v");
                for (int a = 0; a < ars4.Length; a++)
                {
                    int x = a;
                    ars4[x].VoidMode = true;
                }

                if (Heart.SoulType != 1)
                {
                    ScreenDrawing.ThemeColor = Color.Lerp(ScreenDrawing.ThemeColor, new(105, 0, 0), 0.08f);
                }
                if (Heart.SoulType == 1)
                {
                    ScreenDrawing.ThemeColor = Color.Lerp(ScreenDrawing.ThemeColor, Color.White, 0.08f);
                }
                if (InBeat(0)) Part1();
                if (InBeat(76)) Part2();
                if (InBeat(16 * 9 + 2f - 4)) Part3();
                if (InBeat(44 * 4)) Part4();
                if (InBeat(60 * 4 - 1)) Part5();
                if (InBeat(76 * 4)) Part6();
                if (InBeat(92 * 4)) Part7();
                if (InBeat(108 * 4)) Part8();
            }
            public void Start()
            {
                game = this;
                StepSample = Shaders.StepSample;
                production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.51f);
                production2 = new ScreenDrawing.Shaders.Filter(Shaders.Polar, 0.8f);
                RenderProduction production3 = Blur = new ScreenDrawing.Shaders.Blur(0.8f);
                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                Polar = Shaders.Polar;

                Polar.Intensity = 0f;
                splitter.Intensity = 0.0f;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                ScreenDrawing.SceneRendering.InsertProduction(splitter);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(production2);
                GametimeDelta = BeatTime(-0.01f);
                //GametimeDelta = BeatTime(16 * 9 + 2f - 4) -4;
                //PlayOffset = BeatTime(16 * 9 + 2f - 4);
                //GametimeDelta = BeatTime(108 - 2 + 17 * 2)-4;
                //PlayOffset = BeatTime(108 - 2 + 17 * 2);
                Heart.Gravity = 10.7f;
                SetSoul(1);
                SetBox(new Vector2(320, 240), 84, 84);
                TP(new Vector2(320, 240));
                HeartAttribute.MaxHP = 92;
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 1.45f;
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.HPExistColor = Color.Lerp(Color.Red, Color.Black, 0.4f);
                ScreenDrawing.UIColor = new(190, 190, 190);
                RegisterFunction("TapEvent", () =>
                {
                    bool b = RandBool();
                    ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1f), b);
                    MakeLine(b);

                });
                int p = AnomalyExist();
                var scene = CurrentScene as SongFightingScene;
                if (scene.Mode != GameMode.None) return;

                if (p == 0) return;
                if (p == 2 && (int)CurrentDifficulty >= 4) {
                    HeartAttribute.KR = false; HeartAttribute.DamageTaken = 12; ScreenDrawing.HPBar.HPExistColor = Color.DarkMagenta;
                    AutoEnd = false;
                }
                else if (p >= 1 && (int)CurrentDifficulty >= 2)
                {
                    HeartAttribute.KR = false; HeartAttribute.DamageTaken = 12; ScreenDrawing.HPBar.HPExistColor = Color.DarkMagenta;
                    AutoEnd = false;
                }
                AdvanceFunctions.Interactive.AddEndEvent(() => {
                    SimplifiedEasing.RunEase(s => ScreenDrawing.MasterAlpha = s, SimplifiedEasing.Linear(BeatTime(4), 1, 0));
                    DelayBeat(4, () => {
                        GameStates.ResetScene(new Traveler_at_Sunset.Anomaly(p));
                    });
                });
            }
        }
    }
}