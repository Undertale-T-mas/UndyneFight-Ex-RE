using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.Remake.Texts;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Remake.TextUtils;
using static UndyneFight_Ex.MathUtil;
using System.Net.Mail;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace Rhythm_Recall.Waves
{
    public class Spider_Dance : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Spider_Dance()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f / (230f / 60f)) { }
            public string Music => "Spider Dance";

            public string FightName => "Spider Dance";

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "Toby Fox";
                public override string BarrageAuthor => "zKronO";
                public override string AttributeAuthor => "ParaDOXXX";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,11.0f),
                    new(Difficulty.Extreme,20.4f)
                    }
                    );
            }
            static Arrow.UnitEasing easeA, easeB, easeC, easeD;
            static Arrow.EnsembleEasing easeY;
            Blur Blur;
            RenderProduction production, production1, production2, production3, production4;
            GlobalResources.Effects.StepSampleShader StepSample;
            RGBSplitting splitter = new();
            #region disused
            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                throw new NotImplementedException();
            }
            public void Hard()
            {
                throw new NotImplementedException();
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Extreme()
            {
                if (InBeat(0))
                {
                    easeC.TagApply("C");
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(1), 2, 1.75f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.75f, 1.5f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.5f, 1.25f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.25f, 1, EaseState.Back)));
                    });
                    BarrageCreate(BeatTime(0), BeatTime(2), 6.2f, new string[]
                    { 
                        //1
                        "Shrink", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",//pre
                        "", "", "d", "",    "d", "", "", "",
                        "d", "", "+01", "",    "d", "", "+01", "",
                        //2
                        "", "", "d", "",    "d1", "", "+0", "",
                        "d1", "", "+0", "",    "d1", "", "+0", "",
                        "(d)(+0)", "", "(d)(+0)", "",    "(d)(+0)", "", "(d)(+0)", "",
                        "", "", "d", "",    "$2", "+11", "+1", "",
                        //3
                        "(d1)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        "(-11)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        //4
                        "*d'1.2", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "*+1'1.2", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "*+1'1.2", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "(*+1)(*+21)", "", "/", "",    "/", "", "/", "",
                    });
                }
                if (InBeat(24))
                {
                    easeA.TagApply("A");
                    easeB.TagApply("B");
                    easeD.TagApply("D");
                    RegisterFunctionOnce("ScPos", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenPositionDetla = new(s, 0),
                            LinkEase(EaseIn(BeatTime(3), 0, -20, EaseState.Sine),
                            EaseOut(BeatTime(4.5f), -20, 40, EaseState.Quad),
                            EaseIn(BeatTime(2.5f), 40, 0, EaseState.Sine)));
                    });

                    RegisterFunctionOnce("Kick", () =>
                    {
                        //这里可以使用mirror，就不用那么麻烦了
                        Line a = new(60, 90) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line b = new(90, 90) { Alpha = 0.4f, DrawingColor = Color.Purple * 0.9f };
                        Line c = new(640 - 60, 90) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line d = new(640 - 90, 90) { Alpha = 0.4f, DrawingColor = Color.Purple * 0.9f };
                        Line a1 = new(0, 40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line b1 = new(0, -40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line c1 = new(640, 40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line d1 = new(640, -40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line[] lines = { a, b, c, d, a1, b1, c1, d1 };
                        foreach(Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(0.75f));
                            DelayBeat(0.8f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("ConvR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(0.85f), false);
                    });
                    RegisterFunctionOnce("ConvL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(0.85f), true);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "Kick", "",    "Kick", "", "Kick", "",
                        //1
                        "(d)(+21)", "", "+2", "",    "(d)(+21)", "", "+01", "",
                        "(d)(+21)", "", "+2", "",    "(d)(+21)", "", "+01", "",
                        "(~_$2'1.2@B)(ConvL)", "", "(~_$2'1.2@A)(*N21)", "",    "(*+01)(~_$2'1.2@B)(ConvL)", "", "~_$2'1.2@A", "",
                        "(~_$2'1.2@B)(*N21)(ConvL)", "", "(*+01)(~_$2'1.2@A)", "",    "(~_$2'1.2@B)(*N21)(ConvL)", "", "(*+01)(~_$2'1.2@A)", "",
                        //2
                        "(~_$01'1.2@B)(~_$2'1.2@B)(ConvR)", "", "(N0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(N0)(ConvR)", "", "(N0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)(N0)(ConvR)", "", "(N0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(N0)(ConvR)", "", "(N0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)(N0)(ConvR)", "", "(N0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(N0)(ConvR)", "", "(N0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)", "", "$3", "",    "$01", "+1", "+11", "",
                        //3
                        "(#1.75#$0)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#$1)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#$2)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#$3)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        //4
                        "*d'1.2", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "*-1'1.2", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "*-1'1.2", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "(*-1)(*-21)", "", "/", "",    "/", "", "/", "",
                    });
                }
                if (InBeat(56))
                {
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetSoul(Souls.RedSoul);
                        BoxUtils.Vertexify();
                        var box = BoxUtils.VertexBoxInstance;

                        //     320,160
                        //240,240    400,240
                        //     320,320

                        box.SetPosition(0, new Vector2(320, 240 - 80));
                        box.SetPosition(1, new Vector2(320, 240 - 80));
                        box.SetPosition(2, new Vector2(320+Sin(60)*80, 240+Cos(60)* 80));
                        box.SetPosition(3, new Vector2(320 - Sin(60) * 80, 240 + Cos(60) * 80)); 
                        BoxUtils.Rotate(new(320, 240), 180);
                        DelayBeat(0.5f, () => { });
                        ScreenDrawing.BoxBackColor = Color.Transparent;
                        DelayBeat(1, () =>
                        {
                            box.SetPosition(3, new Vector2(240, 240 - 80));
                            box.SetPosition(0, new Vector2(320 + 80, 240 - 80));
                            box.SetPosition(1, new Vector2(320 + 80, 240 + 80));
                            box.SetPosition(2, new Vector2(320 - 80, 240 + 80));
                        });
                    });
                    RegisterFunctionOnce("Purple", () =>
                    {
                        SetSoul(4);
                        BoxUtils.DeVertexify(new(320 - 80, 240 - 80, 160, 160));
                        Heart.PurpleLineCount = 6;
                    });
                    RegisterFunctionOnce("ChangeA", () =>
                    {
                        SetBox(240, 240, 160);
                    });
                    #region Spiders Func
                    RegisterFunctionOnce("Sp1", () =>
                    {
                        CreateEntity(new Spider.LineSpider(1, true, 5));
                    });
                    RegisterFunctionOnce("Sp2", () =>
                    {
                        CreateEntity(new Spider.LineSpider(2, true, 5));
                    });
                    RegisterFunctionOnce("Sp3", () =>
                    {
                        CreateEntity(new Spider.LineSpider(3, true, 5));
                    });
                    RegisterFunctionOnce("Sp4", () =>
                    {
                        CreateEntity(new Spider.LineSpider(4, true, 5));
                    });
                    RegisterFunctionOnce("Sp5", () =>
                    {
                        CreateEntity(new Spider.LineSpider(5, true, 5));
                    });
                    RegisterFunctionOnce("Sp6", () =>
                    {
                        CreateEntity(new Spider.LineSpider(6, true, 5));
                    });
                    RegisterFunctionOnce("tSp1", () =>
                    {
                        CreateEntity(new Spider.LineSpider(1, false, 5));
                    });
                    RegisterFunctionOnce("tSp2", () =>
                    {
                        CreateEntity(new Spider.LineSpider(2, false, 5));
                    });
                    RegisterFunctionOnce("tSp3", () =>
                    {
                        CreateEntity(new Spider.LineSpider(3, false, 5));
                    });
                    RegisterFunctionOnce("tSp4", () =>
                    {
                        CreateEntity(new Spider.LineSpider(4, false, 5));
                    });
                    RegisterFunctionOnce("tSp5", () =>
                    {
                        CreateEntity(new Spider.LineSpider(5, false, 5));
                    });
                    RegisterFunctionOnce("tSp6", () =>
                    {
                        CreateEntity(new Spider.LineSpider(6, false, 5));
                    });
                    #endregion
                    RegisterFunctionOnce("SpA", () =>
                    {

                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "SetBox", "",    "", "", "", "",
                        //1
                        "", "", "Purple", "",    "ChangeA", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "Sp1", "", "Sp2", "",     "Sp3", "", "Sp4","",
                        "Sp5", "", "Sp6", "",     "Sp5", "", "Sp4","",
                        //2
                        "Sp3", "", "Sp2", "",     "Sp1", "", "", "",
                        "tSp6", "", "tSp5", "",     "tSp4", "", "tSp3", "",
                        "tSp2", "", "tSp1", "",     "tSp2", "", "tSp3", "",
                        "tSp4", "", "tSp5", "",     "tSp6", "", "", "",
                        //3
                        "(Sp1)(Sp2)(Sp3)", "", "", "",    "", "", "", "",
                        "(Sp6)(Sp5)(Sp4)", "", "", "",    "", "", "", "",
                        "(Sp1)(Sp2)(Sp3)", "", "", "",    "", "", "", "",
                        "(Sp6)(Sp5)(Sp4)", "", "", "",    "", "", "", "",
                        //4
                        "(tSp1)(tSp6)", "", "(tSp1)(tSp6)", "",     "(tSp2)(tSp5)", "", "(tSp2)(tSp5)", "",
                        "(tSp3)(tSp4)", "", "(tSp3)(tSp4)", "",     "(tSp3)(tSp4)", "", "(tSp3)(tSp4)", "",
                        "(tSp2)(tSp5)", "", "(tSp2)(tSp5)", "",     "(tSp1)(tSp6)", "", "(tSp1)(tSp6)", "",
                        "(Sp1)(Sp6)", "", "", "",    "", "", "", "",
                        //5
                        "(tSp1)(tSp6)", "", "", "",    "(Sp2)(Sp5)", "", "", "",
                        "(tSp3)(tSp4)", "", "", "",    "(Sp1)(Sp6)", "", "", "",
                        "tSp1", "", "tSp2", "",     "tSp3", "", "tSp4","",
                        "tSp5", "", "tSp6", "",     "tSp5", "", "tSp4","",
                        //6
                        "tSp3", "", "tSp2", "",     "tSp1", "", "", "",
                        "Sp6", "", "Sp5", "",     "Sp4", "", "Sp3", "",
                        "Sp2", "", "Sp1", "",     "Sp2", "", "Sp3", "",
                        "Sp4", "", "Sp5", "",     "Sp6", "", "", "",
                        //7
                        "(tSp1)(tSp2)(tSp3)", "", "", "",    "", "", "", "",
                        "(tSp6)(tSp5)(tSp4)", "", "", "",    "", "", "", "",
                        "(tSp1)(tSp2)(tSp3)", "", "", "",    "", "", "", "",
                        "(tSp6)(tSp5)(tSp4)", "", "", "",    "", "", "", "",
                        //8
                        "(Sp1)(Sp6)", "", "(Sp1)(Sp6)", "",     "(Sp2)(Sp5)", "", "(Sp2)(Sp5)", "",
                        "(Sp3)(Sp4)", "", "(Sp3)(Sp4)", "",     "(Sp3)(Sp4)", "", "(Sp3)(Sp4)", "",
                        "(Sp2)(Sp5)", "", "(Sp2)(Sp5)", "",     "(Sp1)(Sp6)", "", "(Sp1)(Sp6)", "",
                        "(tSp1)(tSp6)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(56+ 64))
                {
                    RegisterFunctionOnce("Green", () =>
                    {
                        SetBox(240, 84, 84);
                        SetSoul(1);
                        TP();
                    });
                    easeA.TagApply("A");
                    easeB.TagApply("B");
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "Green", "",    "", "", "", "",
                        //1
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "", "",
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "", "",
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        //2
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "", "", "*d1", "",    "*+0", "", "*+01", "",
                        //3
                        "(*d)(*+21)", "", "", "",    "+0", "", "", "",
                        "(*d1)(*+2)", "", "", "",    "+01", "", "", "",
                        "(d)(+2)", "", "", "",    "(d11)(+201)", "", "(d01)(+211)", "",
                        "", "", "", "",    "", "", "", "",
                        //4
                        "(d1)(+21)", "", "", "",    "(d11)(+201)", "", "(d01)(+211)", "",
                        "", "", "", "",    "", "", "", "",
                        "(d)(+2)", "", "", "",    "(d11)(+201)", "", "(d01)(+211)", "",
                        "", "", "*d", "",    "*+01", "", "*+0", "",
                        //5
                        "(d1)(*D)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01)", "", "", "",
                        "(d1)(*D)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01)", "", "", "",
                        "(d1)(*D)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01)", "", "d1", "",
                        "(*d)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)", "", "", "",
                        //6
                        "(d1)(*D)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01)", "", "d1", "",
                        "(*d)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)", "", "", "",
                        "(d1)(*D)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01)", "", "d1", "",
                        "", "", "*d", "",    "*+01", "", "*+0", "",
                        //7
                        "(*d)(*+21)", "", "", "",    "+0", "", "", "",
                        "(*d1)(*+2)", "", "", "",    "+01", "", "", "",
                        "d", "", "+01", "",    "", "", "(d)(+01)", "",
                        "", "", "", "",    "d1", "", "+0", "",
                        //8
                        "d1", "", "+0", "",    "+21", "", "", "",
                        "(d1)(+21)", "", "", "",    "(d11)(+201)", "", "", "",
                        "(d)(+2)", "", "", "",    "(d11)(+201)", "", "", "",
                        "(d01)(+211)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(184))
                {
                    easeA.TagApply("A");
                    easeB.TagApply("B");
                    easeC.TagApply("C");

                    Arrow.UnitEasing easeX = new();//创建箭头缓动的实例，其实这里也可以这么写  new(){ApplyTime=...},下面的ApplyTime就可以省略
                    AddInstance(easeX);//添加事件
                    float RunTime = BeatTime(3);//这里创建变量只是为了说明清楚ApplyTime和缓动的Time的关系，即最常见的是相等，相等的时候意味着
                    easeX.ApplyTime= RunTime;//会在标记箭头的x时间之前使用缓动，经过x时间后缓动正好完成，箭头标准地击中盾牌
                    //同样，缓动的使用也有更高级的用法，例如可以实现“偏移入眼，后转正常”的效果
                    //例如我在rotateEase中Stable了2beat，quad了2beat,ApplyTime是4beat（2beat+2beat）
                    //在实现缓动的前4beat中观众会先看到偏移的箭头运作，然后看到箭头变换到正常（不正常也可以，但不推荐）的位置
                    //ApplyTime和缓动Time不等的情况很少，因为这会导致两者时间轴不同，例如缓动没运行完箭头已经dispose了，是否有用这点需要看有什么花招了
                    easeX.PositionEase= LinkEase(Stable(0,new Vector2(0,400)),EaseOut(BeatTime(3), new Vector2(0,-400), EaseState.Elastic));
                    easeX.TagApply("X");//注意的是，极坐标变换开启后(默认关闭)的，从下到上的0轨箭头的缓动，用在2轨箭头会变成从上到下过来，用在1轨箭头会变成从左向右
                    //我忘了是啥，也忘了写过没，但我和master提过也讨论过，如果没有这个方法，记得问一下master，让他写一下

                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //1
                        "#1#d", "", "", "",    "+2", "", "", "",
                        "#1#d1", "", "", "",    "+21", "", "", "",
                        "#1#d", "", "", "",    "+2", "", "", "",
                        "#1#d1", "", "", "",    "+21", "", "", "",
                        //2
                        "#1#d", "", "", "",    "+2", "", "", "",
                        "#1#d1", "", "", "",    "+21", "", "", "",
                        "(*$1)(+0@C)", "", "", "",    "(*$3)(+0@C)", "", "", "",
                        "(*$0)($01@X)", "", "", "",    "(*$21)($2@X)", "", "", "",
                        //3
                        "d", "", "+01", "",    "d", "", "+01", "",
                        "d", "", "+01", "",    "d", "", "+01", "",
                        "d", "", "+01", "",    "d", "", "$21", "$11",
                        "$01", "", "+0", "",    "d1", "", "+0", "",
                        //4
                        "#1#d1", "", "", "",    "+21", "", "", "",
                        "#1#d", "", "", "",    "+2", "", "", "",
                        "d1", "", "", "",    "(d01)(+211)", "", "(+101)(+211)", "",
                        "(+101)(+211)", "", "", "",    "", "", "", "",
                        //5
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "", "",
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "", "",
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        //6
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        "(d)(*D1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "", "", "*d", "",    "*+01", "", "*+0", "",
                        //7
                        "(*d)(*+21)", "", "", "",    "+01", "", "", "",
                        "(*d1)(*+2)", "", "", "",    "+0", "", "", "",
                        "d1", "", "+0", "",    "", "", "(d)(+01)", "",
                        "", "", "", "",    "d", "", "+01", "",
                        //8
                        "d", "", "+01", "",    "+2", "", "", "",
                        "(d)(+2)", "", "", "",    "(d11)(+201)", "", "", "",
                        "(d1)(+21)", "", "", "",    "(d11)(+201)", "", "", "",
                        "(d01)(+211)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(248))
                {
                    RegisterFunctionOnce("Change0", () =>
                    {
                        SetBox(240, 340, 180);
                        SetSoul(4);
                        Heart.PurpleLineCount = 5;
                    });
                    RegisterFunctionOnce("Change1", () =>
                    {
                        SetBox(320 - 170, 320 + 170, 240 - 90, 490);
                        Heart.PurpleLineCount = 12;
                    });
                    RegisterFunctionOnce("Change2", () =>
                    {
                        SetGreenBox();
                        SetSoul(1);
                        TP();
                    });
                    RegisterFunctionOnce("SpUR", () =>
                    {
                        float r = Rand(320 - 160, 320 + 160);
                        if (r <= Heart.Centre.X + 5 && r >= Heart.Centre.X - 5) { return ; }
                        Spider sp = new(LinkEase(EaseIn(BeatTime(2), new Vector2(r, 240 - 100), new Vector2(r, 240 - 10), EaseState.Quad),
                            EaseOut(BeatTime(2), new Vector2(r, 240 - 10), new Vector2(r, 240 + 70), EaseState.Quad),
                            EaseIn(BeatTime(4), new Vector2(r, 240 + 70), new Vector2(r, 240 - 100), EaseState.Quad)))
                        { Rotation = 90 };
                        Line l = new(Stable(BeatTime(8), new Vector2(r, 240 - 90)).Easing,
                            LinkEase(EaseIn(BeatTime(2), new Vector2(r, 240 - 90), new Vector2(r, 240 - 10), EaseState.Quad),
                            EaseOut(BeatTime(2), new Vector2(r, 240 - 10), new Vector2(r, 240 + 70), EaseState.Quad),
                            EaseIn(BeatTime(4), new Vector2(r, 240 + 70), new Vector2(r, 240 - 90), EaseState.Quad)).Easing)
                        { Alpha = 0.8f, Depth = 0.9f };
                        CreateEntity(sp, l);
                        DelayBeat(8, () => { l.Dispose(); sp.Dispose(); });
                    });
                    RegisterFunctionOnce("SpInMess", () =>
                    {
                        for (int i = 0; i < 96; i++)
                        {
                            DelayBeat(i * 0.25f, () =>
                            {
                                float h = Rand(60, 240);
                                float start = Rand(320 - 160, 320 + 160);
                                float end = Rand(320 - 220, 320 + 220);
                                Spider sp = new(
                                    LinkEase(EaseIn(BeatTime(h / 60), start, (start + end) / 2, EaseState.Sine),
                                    EaseOut(BeatTime(h / 60), (start + end) / 2, end, EaseState.Sine)),
                                    LinkEase(EaseOut(BeatTime(h / 60), 490, 490 - h, EaseState.Quad),
                                    EaseIn(BeatTime(h / 60), 490 - h, 490, EaseState.Quad)))
                                { Rotation = -90, MarkScore = false };
                                CreateEntity(sp); 
                                DelayBeat(h / 30, () => { sp.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        ForBeat(4, () =>
                        {
                            Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(cur, new(320, 110), 0.23f);
                        });
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        ForBeat(4, () =>
                        {
                            Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(cur, new(320, 443), 0.23f);
                        });
                    });
                    #region Transverse Line Spiders ( Return )
                    RegisterFunctionOnce("TL1", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 1f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL1", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 1f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL2", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 2f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL2", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 2f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL3", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 3f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL3", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 3f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL4", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 4f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL4", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 4f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL5", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 5f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL5", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 5f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    #endregion
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "Change0", "", "", "",    "", "", "", "",
                        //1
                        "(SpUR)(SpUR)(TL1)(TL2)(TL3)(TL4)(TL5)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL1)(TTL2)(TTL3)(TTL4)(TTL5)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //2
                        "(SpUR)(SpUR)(TL3)", "(TL2)(TL4)", "(TL1)(TL5)", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL3)", "(TTL2)(TTL4)", "(TTL1)(TTL5)", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //3
                        "(SpUR)(SpUR)(TL3)", "", "(TL2)(TL4)", "",    "(TL1)(TL5)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL3)", "", "(TTL2)(TTL4)", "",    "(TTL1)(TTL5)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //4
                        "(SpUR)(SpUR)(TL3)", "", "", "(TL2)(TL4)",    "", "", "(TL1)(TL5)", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL3)", "", "", "(TTL2)(TTL4)",    "", "", "(TTL1)(TTL5)", "",
                        "", "", "", "",    "", "", "Move", "",
                        //5
                        "Change1(SpInMess)(SpInMess)(TL1)", "", "TL4", "",    "TL2", "", "TL3", "",
                        "", "", "", "",    "", "", "", "",
                        "TTL1", "", "TTL4", "",    "TTL2", "", "TTL3", "",
                        "", "", "", "",    "", "", "", "",
                        //6
                        "TL2(TTL3)", "", "", "",    "TL1(TTL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "TTL1(TL3)", "", "", "",    "TTL2(TL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //7
                        "TTL2(TL3)", "", "", "",    "TTL1(TL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "TL1(TTL3)", "", "", "",    "TL2(TTL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //8
                        "TTL1", "", "TTL4", "",    "TL2", "", "TL3", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "Back", "",    "Change2", "", "", "",
                    });
                }
                if (InBeat(248 + 64))
                {
                    Arrow.UnitEasing easeX = new();
                    AddInstance(easeX);
                    easeX.ApplyTime = BeatTime(4);
                    easeX.RotationEase = LinkEase(Stable(0,-70),
                        EaseOut(BeatTime(3), -70, 0, EaseState.Quad), 
                        Stable(BeatTime(1), 0));
                    easeX.PositionEase = LinkEase(Stable(0, new Vector2(0, 0)), 
                        EaseOut(BeatTime(3), new Vector2(0, 0), new Vector2(24, 0), EaseState.Linear),
                        Stable(BeatTime(1), new Vector2(24, 0)));
                    easeX.TagApply("X");
                    Arrow.UnitEasing easeY = new();
                    AddInstance(easeY);
                    easeY.ApplyTime = BeatTime(4);
                    easeY.RotationEase = LinkEase(Stable(0, 70),
                        EaseOut(BeatTime(3), 70, 0, EaseState.Quad),
                        Stable(BeatTime(1), 0));
                    easeY.PositionEase = LinkEase(Stable(0, new Vector2(0, 0)),
                        EaseOut(BeatTime(3), new Vector2(0, 0), new Vector2(-24, 0), EaseState.Linear),
                        Stable(BeatTime(1), new Vector2(24, 0)));
                    easeY.TagApply("Y");
                    Arrow.ClassicApplier easeK = new();
                    AddInstance(easeK);
                    DelayBeat(3.75f, () =>
                    {
                        easeK.ApplyDelay(BeatTime(4));
                    });
                    easeK.TagApply("K");
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "(d1@K)(+21@K)", "", "", "",    "D@K", "", "+01@K", "",
                        "", "", "D@K", "",    "+01@K", "", "", "",
                        //1
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(d)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        //2
                        "$0", "$1", "$2", "",    "+01", "", "", "",
                        "(d1)(+2)", "", "", "",    "D1", "", "#2#+0", "",
                        "", "", "", "",    "", "", "", "",
                        "+011", "+111", "+111", "+111",    "+111", "+111", "+111", "+111",
                        //3
                        "(d)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        "(d1)(+21)", "", "", "",    "D", "", "+01", "",
                        "", "", "D", "",    "+01", "", "", "",
                        //4
                        "(d)(+01)", "", "", "",    "(D)(+01)", "", "", "",
                        "D", "", "+01", "",    "", "", "D", "",
                        "#3.75#+01", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //5
                        "(d)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        "(d1)(+21)", "", "", "",    "D", "", "+01", "",
                        "", "", "D", "",    "+01", "", "", "",
                        //6
                        "$21", "$11", "$01", "",    "+0", "", "", "",
                        "(d)(+21)", "", "", "",    "D", "", "#2#+01", "",
                        "", "", "", "",    "", "", "", "",
                        "+011", "+301", "+301", "+301",    "+301", "+301", "+301", "+301",
                        //7
                        "(d1)(+21)", "", "", "",    "D", "", "+01", "",
                        "", "", "D", "",    "+01", "", "", "",
                        "(d)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        //8
                        "(*$2)(+0)", "", "", "",    "(*$01)(+01)", "", "", "",
                        "($0)($2)", "", "($01)($21)", "",    "", "", "($011)($201)", "",
                        "*$3'0.7", "*$31@X", "*$3'0.85", "*$31@Y",    "*$3", "*$31@X", "*$3'1.15", "*$31@Y",
                        "*$3'1.3", "*$31@X", "*$3'1.45", "*$31@Y",    "*$3'1.6", "*$31@X", "*$3'1.75", "*$31@Y",
                    });
                }
                if (InBeat(376))
                {
                    easeC.TagApply("C");
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //1
                        "d@C", "", "", "",    "+11@C", "", "", "",
                        "+1@C", "", "", "",    "+11@C", "", "", "",
                        "D", "", "D", "",    "D", "", "", "",
                        "d1", "", "+0", "",    "D", "", "+01", "",
                        //2
                        "", "", "$2", "",    "~$2", "", "~$2", "",
                        "~$2", "", "~$2", "",    "~$2", "", "~$2", "",
                        "~$2", "", "~$2", "",    "~$2", "", "~$2", "",
                        "", "", "", "",    "", "", "", "",
                        //end
                    });
                }
            }
            public void Easy()
            {

            }
            public void Start()
            {
                Settings.GreenTap = true;
                AddInstance(easeA = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(4.25f),
                    RotationEase = LinkEase(EaseOut(BeatTime(3.75f), 60, 0, EaseState.Quad),
                    Stable(BeatTime(0.25f), 0))
                });
                AddInstance(easeB = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(4.25f),
                    RotationEase = LinkEase(EaseOut(BeatTime(3.75f), -60, 0, EaseState.Quad),
                    Stable(BeatTime(0.25f), 0))
                });
                AddInstance(easeC = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.5f),
                    RotationEase = LinkEase(EaseOut(BeatTime(2.5f), -90, 0, EaseState.Sine))
                });
                AddInstance(easeD = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.5f),
                    RotationEase = LinkEase(EaseOut(BeatTime(2.5f), 90, 0, EaseState.Sine))
                });
                production = Blur = new Blur(0.505f);
                production1 = new Filter(Shaders.StepSample, 0.51f);
                splitter = new RGBSplitting(0.9f) { Disturbance = false };
                StepSample = Shaders.StepSample;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                splitter.Intensity = 0.0f;
                ScreenDrawing.SceneRendering.InsertProduction(production);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(splitter);
                GametimeDelta = -2.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(320, 240);
                ScreenDrawing.ScreenScale = 2;
                HeartAttribute.MaxHP = 8;
                bool jump = true;
                if (jump)
                {
                    //int beat = 118;
                    int beat = 54 + 128 + 64 + 64;
                    //int beat = 54;
                    GametimeDelta = -3.5f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}