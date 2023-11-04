using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using Color = Microsoft.Xna.Framework.Color;

namespace Rhythm_Recall.Waves
{
    public class AFleetingOath : IChampionShip
    {
        public AFleetingOath()
        {
            difficulties = new();
            //this.difficulties.Add("Pa5t Lv.&", Difficulty.Easy);
            //this.difficulties.Add("&rese^6 [u.1l", Difficulty.Normal);
            difficulties.Add("?", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (162f / 60f)) { }
            public string Music => "A Fleeting Oath";

            public string FightName => "A Fleeting Oath";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            //new(Difficulty.Easy, 0),
                            //new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            //new(Difficulty.Easy, 0),
                            //new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            //new(Difficulty.Easy, 0),
                            //new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override string BarrageAuthor => "Fleeting Toki";
                public override string AttributeAuthor => "zKronO's Oath";
                public override string PaintAuthor => "from TONIKAKUKAWAII";
                public override string SongAuthor => "Neko Hacker feat. Tsukasa";
            }
            public SongInformation Attributes => new ThisInformation();
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            private bool notRegistered = true;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production, production1, production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            #region non

            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }
            public void Extreme()
            {
                if (Gametime < 0) return;
            }
            #endregion
            private static class NormalBarrage { }
            public void Easy()
            {
                if (Gametime < 0) return;
            }
            public void Normal()
            {
                if (Gametime < 0) return;
            }
            public void ExtremePlus()
            {
                if (InBeat(0))
                {
                    Arrow.UnitEasing easeRl = new();
                    Arrow.UnitEasing easeRr = new();
                    AddInstance(easeRr);
                    AddInstance(easeRl);
                    easeRr.ApplyTime = BeatTime(2);
                    easeRr.RotationEase = LinkEase(EaseOut(BeatTime(1), 0, 10, EaseState.Quad), EaseIn(BeatTime(1), 10, 0, EaseState.Quad));
                    easeRl.ApplyTime = BeatTime(2);
                    easeRl.RotationEase = LinkEase(EaseOut(BeatTime(1), 0, -10, EaseState.Quad), EaseIn(BeatTime(1), -10, 0, EaseState.Quad));
                    easeRl.TagApply("L");
                    easeRr.TagApply("R");
                    #region easeA
                    Arrow.UnitEasing easeA1 = new();
                    Arrow.UnitEasing easeA2 = new();
                    Arrow.UnitEasing easeA3 = new();
                    Arrow.UnitEasing easeA4 = new();
                    Arrow.UnitEasing easeA5 = new();
                    Arrow.UnitEasing easeA6 = new();
                    Arrow.UnitEasing easeA7 = new();
                    Arrow.UnitEasing easeA8 = new();
                    AddInstance(easeA1);
                    AddInstance(easeA2);
                    AddInstance(easeA3);
                    AddInstance(easeA4);
                    AddInstance(easeA5);
                    AddInstance(easeA6);
                    AddInstance(easeA7);
                    AddInstance(easeA8);
                    easeA1.ApplyTime = BeatTime(4);
                    easeA1.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(4) * 6.5f), new Vector2(0, 42));
                    easeA2.ApplyTime = BeatTime(4);
                    easeA2.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(3.5f) * 6.5f), new Vector2(0, 5.25f * 7f));
                    easeA3.ApplyTime = BeatTime(4);
                    easeA3.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(3) * 6.5f), new Vector2(0, 5.25f * 6f));
                    easeA4.ApplyTime = BeatTime(4);
                    easeA4.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(2.5f) * 6.5f), new Vector2(0, 5.25f * 5f));
                    easeA5.ApplyTime = BeatTime(4);
                    easeA5.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(2) * 6.5f), new Vector2(0, 5.25f * 4f));
                    easeA6.ApplyTime = BeatTime(4);
                    easeA6.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(1.5f) * 6.5f), new Vector2(0, 5.25f * 3f));
                    easeA7.ApplyTime = BeatTime(4);
                    easeA7.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(1) * 6.5f), new Vector2(0, 5.25f * 2f));
                    easeA8.ApplyTime = BeatTime(4);
                    easeA8.PositionEase = Linear(BeatTime(4), new Vector2(0, BeatTime(0.5f) * 6.5f), new Vector2(0, 5.25f));
                    easeA1.TagApply("A1");
                    easeA2.TagApply("A2");
                    easeA3.TagApply("A3");
                    easeA4.TagApply("A4");
                    easeA5.TagApply("A5");
                    easeA6.TagApply("A6");
                    easeA7.TagApply("A7");
                    easeA8.TagApply("A8");
                    #endregion
                    #region easeB
                    Arrow.UnitEasing easeB1 = new();
                    Arrow.UnitEasing easeB2 = new();
                    Arrow.UnitEasing easeB3 = new();
                    Arrow.UnitEasing easeB4 = new();
                    Arrow.UnitEasing easeB5 = new();
                    Arrow.UnitEasing easeB6 = new();
                    Arrow.UnitEasing easeB7 = new();
                    Arrow.UnitEasing easeB8 = new();
                    AddInstance(easeB1);
                    AddInstance(easeB2);
                    AddInstance(easeB3);
                    AddInstance(easeB4);
                    AddInstance(easeB5);
                    AddInstance(easeB6);
                    AddInstance(easeB7);
                    AddInstance(easeB8);
                    easeB1.ApplyTime = BeatTime(4);
                    easeB1.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(4) * 6.5f, 0), new Vector2(42, 0));
                    easeB2.ApplyTime = BeatTime(4);
                    easeB2.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(3.5f) * 6.5f, 0), new Vector2(5.25f * 7f, 0));
                    easeB3.ApplyTime = BeatTime(4);
                    easeB3.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(3) * 6.5f, 0), new Vector2(5.25f * 6f, 0));
                    easeB4.ApplyTime = BeatTime(4);
                    easeB4.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(2.5f) * 6.5f, 0), new Vector2(5.25f * 5f, 0));
                    easeB5.ApplyTime = BeatTime(4);
                    easeB5.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(2) * 6.5f, 0), new Vector2(5.25f * 4f, 0));
                    easeB6.ApplyTime = BeatTime(4);
                    easeB6.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(1.5f) * 6.5f, 0), new Vector2(5.25f * 3f, 0));
                    easeB7.ApplyTime = BeatTime(4);
                    easeB7.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(1) * 6.5f, 0), new Vector2(5.25f * 2f, 0));
                    easeB8.ApplyTime = BeatTime(4);
                    easeB8.PositionEase = Linear(BeatTime(4), new Vector2(BeatTime(0.5f) * 6.5f, 0), new Vector2(5.25f, 0));
                    easeB1.TagApply("B1");
                    easeB2.TagApply("B2");
                    easeB3.TagApply("B3");
                    easeB4.TagApply("B4");
                    easeB5.TagApply("B5");
                    easeB6.TagApply("B6");
                    easeB7.TagApply("B7");
                    easeB8.TagApply("B8");
                    #endregion
                    #region easeC
                    Arrow.UnitEasing easeC1 = new();
                    Arrow.UnitEasing easeC2 = new();
                    Arrow.UnitEasing easeC3 = new();
                    Arrow.UnitEasing easeC4 = new();
                    Arrow.UnitEasing easeC5 = new();
                    Arrow.UnitEasing easeC6 = new();
                    Arrow.UnitEasing easeC7 = new();
                    Arrow.UnitEasing easeC8 = new();
                    AddInstance(easeC1);
                    AddInstance(easeC2);
                    AddInstance(easeC3);
                    AddInstance(easeC4);
                    AddInstance(easeC5);
                    AddInstance(easeC6);
                    AddInstance(easeC7);
                    AddInstance(easeC8);
                    easeC1.ApplyTime = BeatTime(4);
                    easeC1.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(4) * 6.5f, 0), new Vector2(-42, 0));
                    easeC2.ApplyTime = BeatTime(4);
                    easeC2.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(3.5f) * 6.5f, 0), new Vector2(-5.25f * 7f, 0));
                    easeC3.ApplyTime = BeatTime(4);
                    easeC3.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(3) * 6.5f, 0), new Vector2(-5.25f * 6f, 0));
                    easeC4.ApplyTime = BeatTime(4);
                    easeC4.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(2.5f) * 6.5f, 0), new Vector2(-5.25f * 5f, 0));
                    easeC5.ApplyTime = BeatTime(4);
                    easeC5.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(2) * 6.5f, 0), new Vector2(-5.25f * 4f, 0));
                    easeC6.ApplyTime = BeatTime(4);
                    easeC6.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(1.5f) * 6.5f, 0), new Vector2(-5.25f * 3f, 0));
                    easeC7.ApplyTime = BeatTime(4);
                    easeC7.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(1) * 6.5f, 0), new Vector2(-5.25f * 2f, 0));
                    easeC8.ApplyTime = BeatTime(4);
                    easeC8.PositionEase = Linear(BeatTime(4), new Vector2(-BeatTime(0.5f) * 6.5f, 0), new Vector2(-5.25f, 0));
                    easeC1.TagApply("C1");
                    easeC2.TagApply("C2");
                    easeC3.TagApply("C3");
                    easeC4.TagApply("C4");
                    easeC5.TagApply("C5");
                    easeC6.TagApply("C6");
                    easeC7.TagApply("C7");
                    easeC8.TagApply("C8");
                    #endregion
                    RegisterFunctionOnce("LineA", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1.25f), new Vector2(640, 420), new Vector2(180, 420), EaseState.Quad),
                            EaseIn(BeatTime(0.75f), new Vector2(180, 420), new Vector2(320, 420), EaseState.Quad),
                            EaseOut(BeatTime(2), new Vector2(320, 420), new Vector2(360, 420), EaseState.Sine)),
                            LinkEase(Stable(BeatTime(2), 90),
                            EaseOut(BeatTime(2), 90, 100, EaseState.Sine)))
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(2, () => { a.AlphaDecrease(BeatTime(2)); });
                        DelayBeat(4, () => { a.Dispose(); });
                        DelayBeat(1, () =>
                        {
                            Line a = new(LinkEase(EaseOut(BeatTime(0.5f), new Vector2(640, 420), new Vector2(280, 420), EaseState.Quad),
                                EaseIn(BeatTime(0.5f), new Vector2(280, 420), new Vector2(320, 420), EaseState.Quad),
                                EaseOut(BeatTime(2), new Vector2(320, 420), new Vector2(420, 420), EaseState.Sine)),
                                LinkEase(Stable(BeatTime(1), 90),
                                EaseOut(BeatTime(2), 90, 115, EaseState.Sine)))
                            { Alpha = 0.6f };
                            CreateEntity(a);
                            DelayBeat(1, () => { a.AlphaDecrease(BeatTime(2)); });
                            DelayBeat(3, () => { a.Dispose(); });
                        });
                        DelayBeat(2, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(2), false);
                            Line a = new(EaseOut(BeatTime(2), new Vector2(320, 420), new Vector2(500, 420), EaseState.Sine),
                                EaseOut(BeatTime(2), 90, 130, EaseState.Sine))
                            { Alpha = 0.5f };
                            CreateEntity(a);
                            a.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { a.Dispose(); });
                        });
                    });
                    RegisterFunctionOnce("LineB", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1.25f), new Vector2(0, 420), new Vector2(640 - 180, 420), EaseState.Quad),
                            EaseIn(BeatTime(0.75f), new Vector2(640 - 180, 420), new Vector2(320, 420), EaseState.Quad),
                            EaseOut(BeatTime(2), new Vector2(320, 420), new Vector2(640 - 360, 420), EaseState.Sine)),
                            LinkEase(Stable(BeatTime(2), 90),
                            EaseOut(BeatTime(2), 90, 80, EaseState.Sine)))
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(2, () => { a.AlphaDecrease(BeatTime(2)); });
                        DelayBeat(4, () => { a.Dispose(); });
                        DelayBeat(1, () =>
                        {
                            Line a = new(LinkEase(EaseOut(BeatTime(0.5f), new Vector2(0, 420), new Vector2(640 - 280, 420), EaseState.Quad),
                                EaseIn(BeatTime(0.5f), new Vector2(640 - 280, 420), new Vector2(320, 420), EaseState.Quad),
                                EaseOut(BeatTime(2), new Vector2(320, 420), new Vector2(640 - 420, 420), EaseState.Sine)),
                                LinkEase(Stable(BeatTime(1), 90),
                                EaseOut(BeatTime(2), 90, 65, EaseState.Sine)))
                            { Alpha = 0.6f };
                            CreateEntity(a);
                            DelayBeat(1, () => { a.AlphaDecrease(BeatTime(2)); });
                            DelayBeat(3, () => { a.Dispose(); });
                        });
                        DelayBeat(2, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(2), true);
                            Line a = new(EaseOut(BeatTime(2), new Vector2(320, 420), new Vector2(640 - 500, 420), EaseState.Sine),
                                EaseOut(BeatTime(2), 90, 50, EaseState.Sine))
                            { Alpha = 0.5f };
                            CreateEntity(a);
                            a.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { a.Dispose(); });
                        });
                    });
                    CreateChart(BeatTime(1), BeatTime(1), 6.5f, new string[]
                    {
                        "(LineA)d@L","","~_!+0@L","",    "~_+0@L","","~_!+0@L","",    "+0@L","","","",    "","","","",
                        "*d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "(LineB)d@R","","~_!+0@R","",    "~_+0@R","","~_!+0@R","",    "+0@R","","","",    "","","","",
                        "*d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        ////
                        "(LineB)d1@R","","~_!+01@R","",    "~_+01@R","","~_!+01@R","",    "+01@R","","","",    "","","","",
                        "*d1","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "(LineA)d1@L","","~_!+01@L","",    "~_+01@L","","~_!+01@L","",    "+01@L","","","",    "","","","",
                        "*d1","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d1","","","",    "","","","",    "d1","","","",    "","","","",
                        "d1","","","",    "","","","",    "d1","","","",    "","","","",
                        //
                        "d1","","","",    "","","","",    "d1","","","",    "","","","",
                        "d1","","","",    "","","","",    "","","","",    "","","","",
                        ////
                        "$21","","~_!$21","",    "~_$21","","~_!$21","",    "(~_$21)($0)","","(~_!$21)(~_!$0)","",    "(~_$21)(~_$0)","","(~_!$21)(~_!$0)","",
                        "(~_!$21)(~_!$21@A1)(~_!$21@A2)(~_!$21@A3)(~_!$21@A4)(~_!$21@A5)(~_!$21@A6)(~_!$21@A7)(~_!$21@A8)" +
                        "(~_!$0)(~_!$0@A1)(~_!$0@A2)(~_!$0@A3)(~_!$0@A4)(~_!$0@A5)(~_!$0@A6)(~_!$0@A7)(~_!$0@A8)" +
                        "($11)(~_!$11@C1)(~_!$11@C2)(~_!$11@C3)(~_!$11@C4)(~_!$11@C5)(~_!$11@C6)(~_!$11@C7)(~_!$11@C8)" +
                        "($1)(~_!$1@B1)(~_!$1@B2)(~_!$1@B3)(~_!$1@B4)(~_!$1@B5)(~_!$1@B6)(~_!$1@B7)(~_!$1@B8)","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "d","","","",    "","","","",    "*d","","","",
                        //
                        "*+0","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "*d","","","",
                        //
                        "*+0","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "N0","","","",    "","","","",    "$0","","$1","",
                        ////
                        "$2","","","",    "","","","",    "","","","",    "","","","",
                        "d1","","","",    "","","","",    "","","","",    "*d1","","","",
                        //
                        "*+01","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "N21","","","",    "","","","",    "$21","","$11","",
                        //
                        "$01","","","",    "","","","",    "","","","",    "","","","",
                        "d1","","","",    "","","","",    "d1","","","",    "d1","","","",
                        //
                        "d1","","","",    "","","","",    "d1","","","",    "","","","",
                        "","","","",    "(*$01)(*$2)","","","",    "*>$011","","*>$201","",    "*>$011","","*>$201","",
                        ////
                        "*>$011","","*>$201","",    "*>$011","","*>$201","",    "*$01","","","",    "*$2","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                    });
                }
                if (InBeat(4 * 15))
                {
                    Arrow.UnitEasing easeL = new();
                    Arrow.UnitEasing easeR = new();
                    AddInstance(easeL);
                    AddInstance(easeR);
                    easeL.ApplyTime = BeatTime(5);
                    easeR.ApplyTime = BeatTime(5);
                    easeL.PositionEase = Stable(BeatTime(5), new Vector2(-16, 0));
                    easeR.PositionEase = Stable(BeatTime(5), new Vector2(16, 0));
                    easeL.TagApply("L");
                    easeR.TagApply("R");
                    Arrow.UnitEasing easeL1 = new();
                    Arrow.UnitEasing easeR1 = new();
                    AddInstance(easeL1);
                    AddInstance(easeR1);
                    easeL1.ApplyTime = BeatTime(5);
                    easeR1.ApplyTime = BeatTime(5);
                    easeL1.PositionEase = Stable(BeatTime(5), new Vector2(-16, 0));
                    easeR1.PositionEase = Stable(BeatTime(5), new Vector2(16, 0));
                    easeR1.RotationEase = Linear(BeatTime(5), -90, -30);
                    easeL1.RotationEase = Linear(BeatTime(5), 90, 30);
                    easeL1.TagApply("L1");
                    easeR1.TagApply("R1");
                    RegisterFunctionOnce("Step", () =>
                    {
                        RunEase(s => StepSample.Intensity = s,
                            EaseOut(BeatTime(1.2f), 0, 0.4f, EaseState.Quad),
                            EaseIn(BeatTime(0.8f), 0.4f, 0, EaseState.Quad));
                    });
                    CreateChart(BeatTime(5), BeatTime(1), 6.5f, new string[]
                    {
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "$2","","","",    "+2","","","",    "+2","","","",    "+2","","","",
                        //
                        "+2","","","",    "+2","","","",    "+2","","","",    "","","-1@R","",
                        "+01@L","","+0@R","",    "","","+21","",    "","","","",    "+21","","","",
                        //
                        "+21","","+11","",    "-11","","","",    "+21","","","",    "","","","",
                        "$2","","$1","",    "$0","","$1","",    "$2","","","",    "+2","","","",
                        //
                        "+2","","","",    "+2","","","",    "+2","","","",    "","","+1","",
                        "$01","","+11","",    "+11","","","",    "+21","","","",    "+21","","","",
                        ////
                        "+21","","","",    "+21","","","",    "+21","","","",    "","","","",
                        "d1","","+11","",    "-11","","+11","",    "-11","","","",    "d","","-1","",
                        //
                        "+1","","-1","",    "+1","","","",    "d1","","-11","",    "+11","","-11","",
                        "+11","","","",    "d","","+1","",    "-1","","+1","",    "-1","","","",
                        //
                        "*$3@R","","*$31@L","",    "*$3@R","","*$31@L","",    "*$3@R","~_$3@R'4.2","(~_$3@R)(~_$3@R'4.2)","~_$3@R'4.2",    "(~_$3@R'4.2)","~_$3@R'4.2","","",
                        "($0'1.2)($31'1.2)","","","",    "+11'1.2","","","",    "($2'1.2)($31'1.2)","","","",    "-11'1.2","","","",
                        //
                        "($01'1.2)($1'1.2)","","","",    "+1'1.2","","","",    "($21'1.2)($1'1.2)","","","",    "-1'1.2","","","",
                        "(*$2'1.4)(*$11'1.4)","","","",    "(*$2'1.4)(*$31'1.4)","","","",    "(*$01'1.4)(*$1'1.4)","","","",    "(*$01'1.4)(*$3'1.4)","","","",
                        ////
                        "*$21'1.6","","*$0'1.6","",    "*$21'1.6","","*$0'1.6","",    "*$1'1.6@R1","","*$11'1.6@L1","",    "*$1'1.6@R1","","*$11'1.6@L1","",
                        "(Step)(#1.7#$2)(#1.7#$01)","","","",    "","","","",    "","","","",    "","","","",
                        ////
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////
                    });
                }
                if (InBeat(4 * 24))
                {
                    CreateChart(BeatTime(5), BeatTime(1), 6.5f, new string[]
                    {
                        "$0","","$3","",    "$21","","$31","",    "($2)($01)","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////
                    });
                }
            }
            public void Start()
            {
                game = this;
                production = Blur = new Blur(0.505f);
                production1 = new Filter(FightResources.Shaders.StepSample, 0.51f);
                splitter = new RGBSplitting(0.9f) { Disturbance = false };
                StepSample = FightResources.Shaders.StepSample;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                splitter.Intensity = 0.0f;
                ScreenDrawing.SceneRendering.InsertProduction(production);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(splitter);

                SetSoul(1);
                InstantTP(new(320, 240));
                SetGreenBox();
                HeartAttribute.MaxHP = 16;
                ScreenDrawing.HPBar.HPLoseColor = Color.DarkRed;
                ScreenDrawing.HPBar.HPExistColor = Color.Blue * 0.7f;
                Settings.GreenTap = true;
                HeartAttribute.ArrowFixed = true;

                GametimeDelta = BeatTime(-0.65f);
                bool jump = true;
                if (jump)
                {
                    int beat = 0;
                    //int beat = 4 * 15;
                    GametimeDelta = BeatTime(-0.65f + beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}