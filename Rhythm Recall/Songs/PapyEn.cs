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

namespace Rhythm_Recall.Waves
{
    public class PapyEn : IChampionShip
    {
        public PapyEn()
        {
            difficulties = new();
            //this.difficulties.Add("Pa5t Lv.&", Difficulty.Easy);
            //this.difficulties.Add("&rese^6 [u.1l", Difficulty.Normal);
            difficulties.Add("ALL JUST A GAME", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (122f / 60f)) { }
            public string Music => "Papyrus Encounter";

            public string FightName => "Papyrus Encounter";
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
                public override string BarrageAuthor => "[ParaDOXXX] Walking through the dust.";
                public override string AttributeAuthor => "[Always Dodger] - zKronO";
                public override string PaintAuthor => "Michele";
                public override string SongAuthor => "Nikolas";
            }
            public SongInformation Attributes => new ThisInformation();
            private bool notRegistered = true;
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
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
            #region Ex
            void Dust()
            {
                CreateEntity(new Particle(Color.White * Rand(0.3f, 0.9f), new Vector2(Rand(-5, -2), Rand(1, 4)), 3, new Vector2(Rand(80, 680), -10)) { Depth = Rand(0.6f, 0.9f) });
            }
            void ExPart1()
            {
                RegisterFunctionOnce("mid", () =>
                {
                    Line a = new(new Vector2(320, 240), 90) { Alpha = 0.75f };
                    Line b = new(new Vector2(320 - 18, 240), 90) { Alpha = 0.75f };
                    Line c = new(new Vector2(320 + 18, 240), 90) { Alpha = 0.75f };
                    Line[] all = { a, b, c };
                    foreach (Line l in all)
                    {
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1.35f), 0.8f);
                    }
                });
                RegisterFunctionOnce("TRline", () =>
                {
                    int rd = Rand(320 - 40, 320 + 80);
                    int rot = Rand(95, 140);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(3), CentreEasing.EaseInQuad(new(rd, 240), new(rd + 720, 240), BeatTime(3)));
                    Line l = new(c.GetResult(), (s) => { return rot; }) { Alpha = 0.6f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2.75f), 0.7f);
                });
                RegisterFunctionOnce("TLline", () =>
                {
                    int rd = Rand(320 - 80, 320 + 40);
                    int rot = Rand(40, 85);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(3), CentreEasing.EaseInQuad(new(rd, 240), new(rd - 720, 240), BeatTime(3)));
                    Line l = new(c.GetResult(), (s) => { return rot; }) { Alpha = 0.6f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2.75f), 0.7f);
                });
                RegisterFunctionOnce("rdline", () =>
                {
                    Line l = new(new Vector2(Rand(60, 580), 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.35f), 0.8f);
                });
                RegisterFunctionOnce("Move", () =>
                {
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(BeatTime(4), ValueEasing.EaseOutBack(600, 240, BeatTime(4)));
                    v.Run((s) => { InstantSetBox(new Vector2(320, s), 84, 84); InstantTP(new(320, s)); });
                });
                #region text
                RegisterFunctionOnce("txt1", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(8), "$ I DONT EXPECT YOU TO UNDERSTAND,SANS." +
                        "\n          NOR DO I WANT YOU TO.", new Vector2(40, 280), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("txt2", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(8), "$   ITS NOT THAT I WANT TO DO THIS..." +
                        "\n        ITS THAT I HAVE TO DO.", new Vector2(40, 280), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("txt3", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(8), "$I HAVE TO DO THIS TO SAVE US IN THE END."
                        , new Vector2(40, 280), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("txt4", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(8), "$    THEY'LL DESTROY US ALL IF I DONT."
                        , new Vector2(40, 280), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("txt5", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(8), "$      I WILL BE DEEMED THE SAVIOR OF" +
                        "\n        MONSTERKING FOR MY EFFORTS.", new Vector2(40, 280), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("txt6", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(8), "$  ISN'T THAT WHAT YOU WOULD HAVE WANTED," +
                        "\n                 SANS?", new Vector2(40, 280), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                #endregion
                BarrageCreate(BeatTime(0), BeatTime(2), 5.5f, new string[]
                {
                    "mid(TRline)(txt1)","","","",    "TRline","","","",    "TRline","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "TLline","","TLline","",
                    //
                    "TLline(mid)(txt2)","","","",    "","","","",    "TRline","","","",    "","","","",
                    "TRline","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "mid(TLline)(txt3)","","","",    "TLline","","","",    "TLline","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "TRline","","TRline","",
                    //
                    "TRline(mid)(txt4)","","","",    "","","","",    "TLline","","","",    "","","","",
                    "TRline","","","",    "","","","",    "TLline","","","",    "","","","",
                    //
                    "mid(TLline)(rdline)(txt5)","","","",    "TLline","","","",    "TLline(rdline)","","","",    "","","","",
                    "(rdline)","","","",    "","","","",    "(rdline)","","","",    "TRline","","TRline","",
                    //
                    "TRline(mid)(rdline)(txt6)","","","",    "(rdline)","","","",    "TLline(rdline)","","","",    "(rdline)","","","",
                    "TLline(rdline)","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "mid(TRline)(rdline)","","","",    "TRline","","","",    "TRline(rdline)","","","",    "","","","",
                    "(rdline)","","","",    "","","","",    "(rdline)","","","",    "TLline","","TLline","",
                    //
                    "TLline(mid)(rdline)","","","",    "(rdline)","","","",    "TRline(rdline)","","","",    "(rdline)","","","",
                    "TLline(rdline)(Move)","","","",    "","","","",    "TRline","","","",    "","","","",
                    //
                });
            }
            void ExPart2()
            {
                #region cross$stand line
                RegisterFunctionOnce("c1line", () =>
                {
                    Line l = new(new Vector2(320, 80), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c2line", () =>
                {
                    Line l = new(new Vector2(320, 80 * 2), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c3line", () =>
                {
                    Line l = new(new Vector2(320, 80 * 3), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c4line", () =>
                {
                    Line l = new(new Vector2(320, 80 * 4), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c5line", () =>
                {
                    Line l = new(new Vector2(320, 80 * 5), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c1aline", () =>
                {
                    Line l = new(new Vector2(320, 80 + 40), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c2aline", () =>
                {
                    Line l = new(new Vector2(320, 80 * 2 + 40), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c3aline", () =>
                {
                    Line l = new(new Vector2(320, 80 * 3 + 40), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("c4aline", () =>
                {
                    Line l = new(new Vector2(320, 80 * 4 + 40), 0) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s1line", () =>
                {
                    Line l = new(new Vector2(80 * 2, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s2line", () =>
                {
                    Line l = new(new Vector2(80 * 3, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s3line", () =>
                {
                    Line l = new(new Vector2(80 * 4, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s4line", () =>
                {
                    Line l = new(new Vector2(80 * 5, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s5line", () =>
                {
                    Line l = new(new Vector2(80 * 6, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s1aline", () =>
                {
                    Line l = new(new Vector2(80 * 2 + 40, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s2aline", () =>
                {
                    Line l = new(new Vector2(80 * 3 + 40, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s3aline", () =>
                {
                    Line l = new(new Vector2(80 * 4 + 40, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                RegisterFunctionOnce("s4aline", () =>
                {
                    Line l = new(new Vector2(80 * 5 + 40, 240), 90) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1.25f), 0.8f);
                });
                #endregion
                RegisterFunctionOnce("txt1", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(4), "$  LET'S JUST MAKE THIS QUICK."
                        , new Vector2(120, 340), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("txt2", () =>
                {
                    CreateEntity(new TextPrinter(BeatTime(4), "$      ...I'M SORRY,SANS."
                        , new Vector2(120, 340), new TextAttribute[]{ new TextSizeAttribute(0.85f),
                            new TextColorAttribute(Color.WhiteSmoke)})
                    { PlaySound = false });
                });
                RegisterFunctionOnce("Change", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.White);
                    PlaySound(Sounds.switchScene);
                    SetSoul(0);
                    ScreenDrawing.BoundColor = Color.White * 0.65f;
                    ScreenDrawing.UpBoundDistance = 80;
                });
                RegisterFunctionOnce("Dark", () =>
                {
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, BeatTime(4), Color.Black, 0.2f);
                    CreateEntity(m);
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(BeatTime(1), ValueEasing.Stable(0.2f));
                    v.Insert(1, ValueEasing.Linear(0.2f, 0.4f, 1));
                    v.Insert(BeatTime(1) - 1, ValueEasing.Stable(0.4f));
                    v.Insert(1, ValueEasing.Linear(0.4f, 0.7f, 1));
                    v.Insert(BeatTime(1) - 1, ValueEasing.Stable(0.7f));
                    v.Insert(1, ValueEasing.Linear(0.7f, 0.9f, 1));
                    v.Insert(BeatTime(1) - 2, ValueEasing.Stable(0.9f));
                    v.Insert(1, ValueEasing.Linear(0.9f, 0, 1));
                    v.Run(s => m.alpha = s);
                    DelayBeat(4, () => { m.Dispose(); });
                });
                BarrageCreate(BeatTime(4), BeatTime(2), 5.5f, new string[]
                {
                    "#4#$21(c5line)","(c4line)","(c3line)","(c2line)",    "(c1line)","(c2line)","(c3line)","(c4line)",
                    "(c5line)","(c4line)","(c3line)","(c2line)",    "(c1line)","(c2line)","(c3line)","(c4line)",
                    "#3#$21(c5line)","(c4line)","(c3line)","(c2line)",    "(c1line)","(c2line)","(c3line)","(c4line)",
                    "(c5line)","(c4line)","(c3line)","(c2line)",    "(c1line)","(c2line)","(c3line)","(c4line)",
                    //
                    "#4#$31(c1line)(c5line)","(c2line)","(c3line)","(c4line)",    "(c5line)","(c4line)","(c3line)","(c2line)",
                    "(c1line)","(c2line)","(c3line)","(c4line)",    "(c5line)","(c4line)","(c3line)","(c2line)",
                    "#3#$31(c1line)","(c2line)","(c3line)","(c4line)",    "(c5line)","(c4line)","(c3line)","(c2line)",
                    "(c1line)","(c2line)","(c3line)","(c4line)",    "(c5line)","(c4line)","(c3line)","(c2line)",
                    //
                    "#4#$0(s1line)(c1line)","(s2line)","(s3line)","(s4line)",   "(s5line)","(s4line)","(s3line)","(s2line)",
                    "(s1line)","(s2line)","(s3line)","(s4line)",    "(s5line)","(s4line)","(s3line)","(s2line)",
                    "#3#$0(s1line)","(s2line)","(s3line)","(s4line)",   "(s5line)","(s4line)","(s3line)","(s2line)",
                    "(s1line)","(s2line)","(s3line)","(s4line)",    "(s5line)","(s4line)","(s3line)","(s2line)",
                    //
                    "#4#$1(s5line)(s1line)","(s4line)","(s3line)","(s2line)",    "(s1line)","(s2line)","(s3line)","(s4line)",
                    "(s5line)","(s4line)","(s3line)","(s2line)",    "(s1line)","(s2line)","(s3line)","(s4line)",
                    "#3#$1(s5line)","(s4line)","(s3line)","(s2line)",    "(s1line)","(s2line)","(s3line)","(s4line)",
                    "(s5line)","(s4line)","(s3line)","(s2line)",    "(s1line)","(s2line)","(s3line)","(s4line)",
                    //
                    "(#4#$21)(#7#$0)(s1line)(s5line)","(s1aline)(s4aline)","(s2line)(s4line)","(s2aline)(s3aline)",    "(s3line)","(s2aline)(s3aline)","(s2line)(s4line)","(s1aline)(s4aline)",
                    "(s1line)(s5line)","(s1aline)(s4aline)","(s2line)(s4line)","(s2aline)(s3aline)",    "(s3line)","(s2aline)(s3aline)","(s2line)(s4line)","(s1aline)(s4aline)",
                    "(s1line)(s5line)","(s1aline)(s4aline)","(s2line)(s4line)","(s2aline)(s3aline)",    "(s3line)","(s2aline)(s3aline)","(s2line)(s4line)","(s1aline)(s4aline)",
                    "(s1line)(s5line)","(s1aline)(s4aline)","(s2line)(s4line)","(s2aline)(s3aline)",    "(s3line)","(s2aline)(s3aline)","(s2line)(s4line)","(s1aline)(s4aline)",
                    //
                    "(#7#$21)(#4#$0)(c5line)(c1line)(s1line)(s5line)","(c4aline)(c1aline)","(c4line)(c2line)","(c3aline)(c2aline)",    "(c3line)","(c3aline)(c2aline)","(c4line)(c2line)","(c4aline)(c1aline)",
                    "(c5line)(c1line)","(c4aline)(c1aline)","(c4line)(c2line)","(c3aline)(c2aline)",    "(c3line)","(c3aline)(c2aline)","(c4line)(c2line)","(c4aline)(c1aline)",
                    "(c5line)(c1line)","(c4aline)(c1aline)","(c4line)(c2line)","(c3aline)(c2aline)",    "(c3line)","(c3aline)(c2aline)","(c4line)(c2line)","(c4aline)(c1aline)",
                    "(c5line)(c1line)","(c4aline)(c1aline)","(c4line)(c2line)","(c3aline)(c2aline)",    "(c3line)","(c3aline)(c2aline)","(c4line)(c2line)","(c4aline)(c1aline)",
                    //
                    "(#3#$1)(#3#$31)(c5line)(c1line)(s1line)(s5line)","(c4aline)(c1aline)(s1aline)(s4aline)","(c4line)(c2line)(s2line)(s4line)","(c2aline)(c3aline)(s2aline)(s3aline)",    "(c3line)(s3line)","(c2aline)(c3aline)(s2aline)(s3aline)","(c4line)(c2line)(s2line)(s4line)","(c4aline)(c1aline)(s1aline)(s4aline)",
                    "(c5line)(c1line)(s1line)(s5line)","(c4aline)(c1aline)(s1aline)(s4aline)","(c4line)(c2line)(s2line)(s4line)","(c2aline)(c3aline)(s2aline)(s3aline)",    "(c3line)(s3line)","(c2aline)(c3aline)(s2aline)(s3aline)","(c4line)(c2line)(s2line)(s4line)","(c4aline)(c1aline)(s1aline)(s4aline)",
                    "(#4#$3)(#4#$11)(c5line)(c1line)(s1line)(s5line)","(c4aline)(c1aline)(s1aline)(s4aline)","(c4line)(c2line)(s2line)(s4line)","(c2aline)(c3aline)(s2aline)(s3aline)",    "(c3line)(s3line)","(c2aline)(c3aline)(s2aline)(s3aline)","(c4line)(c2line)(s2line)(s4line)","(c4aline)(c1aline)(s1aline)(s4aline)",
                    "(c5line)(c1line)(s1line)(s5line)","(c4aline)(c1aline)(s1aline)(s4aline)","(c4line)(c2line)(s2line)(s4line)","(c2aline)(c3aline)(s2aline)(s3aline)",    "(c3line)(s3line)","(c2aline)(c3aline)(s2aline)(s3aline)","(c4line)(c2line)(s2line)(s4line)","(c4aline)(c1aline)(s1aline)(s4aline)",
                    //
                    "($11)($3)(Change)(txt1)","","","",    "","","","",
                    "","","","",    "","","","",
                    "(txt2)(Dark)","","","",    "","","","",    "","","","",
                    "","","","",
                    //
                });
            }
            void ExPart3()
            {
                RegisterFunctionOnce("Reset", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.WhiteSmoke);
                    SetBox(new Vector2(320, 240), 240, 180);
                    SetSoul(2);
                    Heart.GiveForce(180, 7.2f);
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    v1.Insert(BeatTime(2), ValueEasing.EaseOutQuad(180, 80, BeatTime(2)));
                    v2.Insert(BeatTime(3), ValueEasing.EaseOutCirc(1.45f, 1f, BeatTime(3)));
                    v1.Run(s => ScreenDrawing.DownBoundDistance = s);
                    v2.Run(s => ScreenDrawing.ScreenScale = s);
                    CreateEntity(new Boneslab(0, 40, BeatTime(2), BeatTime(12)) { ColorType = 1 });
                });
                RegisterFunctionOnce("BoneA1", () =>
                {
                    int r = Rand(30, 70);
                    CreateBone(new UpBone(false, 5, r));
                    CreateBone(new DownBone(false, 5, 130 - r));
                    PlaySound(Sounds.pierce);
                    AddInstance(new InstantEvent(2, () =>
                    {
                        CreateBone(new UpBone(false, 5, r));
                        CreateBone(new DownBone(false, 5, 130 - r));
                    }));
                });
                RegisterFunctionOnce("BoneA2", () =>
                {
                    CreateBone(new CustomBone(new Vector2(320 + 130, 240), Motions.PositionRoute.linear, 0, 175)
                    {
                        PositionRouteParam = new float[] { -4, 0 },
                        ColorType = 2
                    });
                });
                RegisterFunctionOnce("BoneA3", () =>
                {
                    DrawingUtil.CrossBone(new(320 - 130, 240 - 90), new(3.5f, 0), 64, 2);
                });
                RegisterFunctionOnce("change1", () => { Heart.GiveForce(0, 12); });
                RegisterFunctionOnce("BoneB1", () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        game.DelayBeat(1.5f * i, () =>
                        {
                            CreateBone(new UpBone(true, 2.25f, 140));
                            CreateBone(new DownBone(false, 2.25f, 30));
                        });
                    }
                });
                RegisterFunctionOnce("BoneB2", () =>
                {
                    CreateBone(new CustomBone(new(320 + 130, 240 + 90), Motions.PositionRoute.linear, 20, 240)
                    {
                        PositionRouteParam = new float[] { -5, 0 },
                        ColorType = 1
                    });
                    PlaySound(Sounds.pierce);
                });
                RegisterFunctionOnce("change2", () =>
                {
                    SetSoul(0);
                    SetBox(240, 240, 240);
                    for (int i = 0; i < 240; i++)
                    {
                        game.DelayBeat(0.125f * i, () =>
                        {
                            CreateBone(new LeftBone(false, 6, 20) { MarkScore = false });
                            CreateBone(new RightBone(false, 6, 20) { MarkScore = false });
                        });
                    }
                    game.DelayBeat(0.25f, () =>
                    {
                        ValueEasing.EaseBuilder v = new();
                        v.Insert(BeatTime(30 - 0.25f), ValueEasing.Linear(240, 120, BeatTime(30 - 0.25f)));
                        v.Run((s) => { InstantSetBox(240, s, 240); });
                    });
                });
                RegisterFunctionOnce("BoneC1", () =>
                {
                    float x = Heart.Centre.X;
                    float y = Heart.Centre.Y;
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(2), CentreEasing.EaseInBack(new(x + 160, y), new(x + 17.5f, y), BeatTime(2)));
                    c2.Insert(BeatTime(2), CentreEasing.EaseInBack(new(x - 160, y), new(x - 17.5f, y), BeatTime(2)));
                    c1.Insert(BeatTime(2), CentreEasing.EaseInQuad(new(x + 17.5f, y), new(x + 50, y + 400), BeatTime(2)));
                    c2.Insert(BeatTime(2), CentreEasing.EaseInQuad(new(x - 17.5f, y), new(x - 50, y + 400), BeatTime(2)));
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    v1.Insert(BeatTime(2), ValueEasing.Stable(90));
                    v2.Insert(BeatTime(2), ValueEasing.Stable(90));
                    v1.Insert(BeatTime(2), ValueEasing.Linear(90, 170, BeatTime(1.55f)));
                    v2.Insert(BeatTime(2), ValueEasing.Linear(90, 10, BeatTime(1.55f)));
                    CreateBone(new CustomBone(new(0, 0), c1.GetResult(), (s) => { return 30; }, v1.GetResult()));
                    CreateBone(new CustomBone(new(0, 0), c2.GetResult(), (s) => { return 30; }, v2.GetResult()));
                    DelayBeat(2, () => { PlaySound(Sounds.slam); });
                });
                RegisterFunctionOnce("BoneC2", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2), CentreEasing.EaseInCubic(new(320, 120), new(320, 380), BeatTime(2)));
                    CreateBone(new CustomBone(new(0, 0), c.GetResult(), 90, 230) { ColorType = Rand(1, 2) });
                });
                RegisterFunctionOnce("change3", () =>
                {
                    SetBox(new Vector2(Heart.Centre.X, Heart.Centre.Y), 25, 25);
                    ResetBarrage();
                });
                BarrageCreate(BeatTime(4), BeatTime(2), 5.5f, new string[]
                {
                    "Reset","","","",    "","","","",    "BoneA1","","","",    "BoneA2","","","",
                    "BoneA3","","","",    "","","","",    "BoneA1","","","",    "BoneA2","","","",
                    //
                    "BoneA3","","","",    "","","","",    "BoneA1","","","",    "BoneA2","","","",
                    "BoneA3","","","",    "","","","",    "BoneA1","","","",    "BoneA2","","","",
                    //
                    "change1","","","",    "BoneB1","","","",    "BoneB2","","","",    "","","","",
                    "","","","",    "","","","",    "BoneB2","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "BoneB2","","","",    "","","","",
                    "","","","",    "","","","",    "BoneB2","","","",    "","","","",
                    //
                    "change2","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    "BoneC1","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    //
                    "BoneC1(BoneC2)","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    "BoneC1","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    //
                    "BoneC1(BoneC2)","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    "BoneC1(BoneC2)","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    //
                    "BoneC1(BoneC2)","","","",    "","","","",    "BoneC1","","","",    "","","","",
                    "BoneC1(BoneC2)","","","",    "","","","",    "change3","","","",    "","","","",
                    //
                });
            }
            void ExPart4()
            {
                RegisterFunctionOnce("boneA1", () =>
                {

                });
                RegisterFunctionOnce("boneA2", () =>
                {

                });
                RegisterFunctionOnce("boneA3", () =>
                {

                });
                BarrageCreate(BeatTime(4), BeatTime(2), 5.5f, new string[]
                {
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
                    //
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
                    //
                });
            }
            #endregion
            public void ExtremePlus()
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { PlaySound = false });
                #region turn a round
                Arrow[] ars3 = GetAll<Arrow>("Lm");
                for (int a = 0; a < ars3.Length; a++)
                {
                    int x = a;
                    ars3[x].Offset = new(-24, 0);
                }
                Arrow[] ars4 = GetAll<Arrow>("Li");
                for (int a = 0; a < ars4.Length; a++)
                {
                    int x = a;
                    ars4[x].Offset = new(-12, 0);
                }
                Arrow[] ars5 = GetAll<Arrow>("Ri");
                for (int a = 0; a < ars5.Length; a++)
                {
                    int x = a;
                    ars5[x].Offset = new(12, 0);
                }
                Arrow[] ars6 = GetAll<Arrow>("Rm");
                for (int a = 0; a < ars6.Length; a++)
                {
                    int x = a;
                    ars6[x].Offset = new(24, 0);
                }
                Arrow[] ars7 = GetAll<Arrow>("Lo");
                for (int a = 0; a < ars7.Length; a++)
                {
                    int x = a;
                    ars7[x].Offset = new(-32, 0);
                }
                Arrow[] ars8 = GetAll<Arrow>("Ro");
                for (int a = 0; a < ars8.Length; a++)
                {
                    int x = a;
                    ars8[x].Offset = new(32, 0);
                }
                #endregion
                if (InBeat(-56)) ExPart1();
                if (InBeat(0 + 4)) ExPart2();
                if (InBeat(8 * 8 + 4)) ExPart3();
                if (InBeat(8 * 16 + 4)) ExPart4();
                Dust();
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

                bool delayEnable = true;
                if (delayEnable)
                {
                    float delay = BeatTime(8 * 15);
                    PlayOffset = delay - BeatTime(4) + 3.1f;
                    GametimeDelta = delay - BeatTime(60);
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(new Vector2(320, 240));
                }
                else
                {
                    GametimeDelta = BeatTime(-56) - 0.2f;
                    PlayOffset = 0;
                    InstantSetBox(new Vector2(320, 600), 84, 84);
                    InstantTP(new Vector2(320, 600));
                }

                SetSoul(1);
                HeartAttribute.MaxHP = 15.4321f;
                HeartAttribute.DamageTaken = 1;
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.HPExistColor = Color.Lerp(Color.Red, Color.DarkRed, 0.4f);
            }
        }
    }
}