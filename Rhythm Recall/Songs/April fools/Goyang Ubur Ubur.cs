using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using UndyneFight_Ex.Entities.Advanced;
namespace Rhythm_Recall.Waves
{
    public class Goyang : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Goyang()
        {
            dif.Add("div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (152f / 60f)) { }
            public string Music => "Goyang Ubur Ubur";

            public string FightName => "Goyang Ubur Ubur";

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public Information() { MusicOptimized = true; }
                public override string SongAuthor => "Diego Takupaz";
                public override string BarrageAuthor => "TK";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                    {

                    }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                        {

                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                        {

                        }
                    );
            }
            #region
            public void Noob() { }
            public void Easy() { }
            public void Normal() { }
            public void Hard() { }
            public void ExtremePlus() { }
            #endregion
            public void Extreme()
            {
                #region Intro
                if (InBeat(1))
                {
                    CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
                    {
                        "", "R", "", "", "R", "", "", "",
                        "", "", "", "", "", "", "R", "",
                        "R", "", "R", "", "R", "R", "R", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "R", "", "", "R", "", "R",
                        "", "", "R", "", "", "", "", "",
                        "", "", "", "", "", "R", "", "R",
                        "", "R", "", "", "", "", "", "",
                        "", "", "", "R", "", "R", "", "R",
                        "", "R", "", "R", "", "R", "", "",
                        "", "", "", "", "", "", "", "",
                        "R", "", "R", "", "R", "", "R", "",
                        "R", "", "R", "", "R", "", "", "",
                    });
                }
                if (InBeat(33.25f))
                {
                    foreach (Line l in GetAll<Line>())
                    {
                        RunEase((s) => l.Alpha = s,
                            EaseOut(BeatTime(4), 0.3f, 0, EaseState.Linear));
                        DelayBeat(4, () => l.Dispose());
                    }
                }
                #endregion
                #region Part 1 Blue
                if (InBeat(36))
                {
                    SetSoul(0);
                    SetBox(240, 180, 180);
                }
                if (InBeat(36, 72) && AtKthBeat(3, BeatTime(1.5f)))
                {
                    SetSoul(2);
                    int dir = Rand(0, 3) * 90;
                    Heart.GiveInstantForce(dir, 5);
                    CustomBone bone;
                    int AppearCol = Rand(0, 2);
                    CreateEntity(new Boneslab(dir, 30, BeatTime(1.5f), BeatTime(1)));
                    CreateEntity(new Boneslab(dir, 60, BeatTime(2.5f), BeatTime(0.25f)));
                    PlaySound(Sounds.boneSlabSpawn);
                    DelayBeat(1.5f, () => PlaySound(Sounds.pierce));
                    DelayBeat(2.5f, () => PlaySound(Sounds.pierce));
                    for (int i = -10; i <= 10; i++)
                    {
                        if (Posmod(i, 3) != AppearCol) continue;

                        bone = new(Stable(0, new Vector2(320, 240) + GetVector2(95, dir) + GetVector2(i * 10, dir + 90)), EaseOut(BeatTime(3), dir + 90, dir, EaseState.Quad), 0);
                        bone.LengthRoute = Motions.LengthRoute.sin;
                        bone.LengthRouteParam = new float[] { 60, BeatTime(4), 0, BeatTime(2) };
                        bone.ColorType = Posmod(i, 3);
                        bone.AlphaIncrease = true;
                        CreateBone(bone);

                        bone = new(Stable(0, new Vector2(320, 240) - GetVector2(95, dir) + GetVector2(i * 10, dir - 90)), EaseOut(BeatTime(3), dir - 90, dir, EaseState.Quad), 0);
                        bone.LengthRoute = Motions.LengthRoute.sin;
                        bone.LengthRouteParam = new float[] { 60, BeatTime(4), 0, BeatTime(2) };
                        bone.ColorType = Posmod(i, 3);
                        bone.AlphaIncrease = true;
                        CreateBone(bone);
                    }
                    dir -= 90;
                    Vector2 Start = GetVector2(100, dir) + new Vector2(320, 240),
                            Displace = GetVector2(120, dir);
                    bone = new(LinkEase(Stable(BeatTime(1), Start),
                        EaseOut(BeatTime(1), -Displace, EaseState.Quad),
                        EaseOut(BeatTime(1), Displace, EaseState.Quad)),
                        Stable(0, dir), 175);
                    CreateBone(bone);
                    DelayBeat(3, () => bone.Dispose());
                }
                #endregion
                #region Part 2 Blue
                if (InBeat(74))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                    Heart.InstantSetRotation(0);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new SwarmBone(70, BeatTime(12), BeatTime(i * 3), BeatTime(38)) { ColorType = (i % 2) + 1, Extras = 0 });
                }
                if (InBeat(74, 110))
                {
                    if (At0thBeat(3))
                    {
                        EaseUnit<Vector2>[] ease = new EaseUnit<Vector2>[12];
                        for (int i = 0; i < 12; i++)
                            ease[i] = EaseOut(BeatTime(2.5f), new Vector2(-140, 0), EaseState.Quad);
                        CreatePlatform(new(1, new Vector2(500, 320), LinkEase(ease), 15, 50));
                    }
                    if (GametimeF > BeatTime(75) && At0thBeat(0.1f))
                        CreateBone(new DownBone(true, 4, Rand(10f, 40f)) { Rotation = Rand(-10f, 10f), MarkScore = false });
                    if (AtKthBeat(2.35f, BeatTime(1.35f)))
                    {
                        CreateGB(new NormalGB(new Vector2(Rand(100, 540), 180), Heart.Centre, new(1, 0.5f), BeatTime(2), BeatTime(0.4f)));
                    }
                }
                if (InBeat(111)) SetSoul(0);
                if (InBeat(110, 145))
                {
                    bool[] TimeCheck =
                    {
                        GametimeF <= BeatTime(125) && At0thBeat(4.5f),
                        GametimeF >= BeatTime(125) && AtKthBeat(1.125f, BeatTime(0.25f)) && GametimeF <= BeatTime(135),
                        GametimeF >= BeatTime(135) && At0thBeat(0.5f) && GametimeF <= BeatTime(140),
                        GametimeF >= BeatTime(140) && At0thBeat(0.25f)
                    };
                    if (TimeCheck[0] || TimeCheck[1] || TimeCheck[2] || TimeCheck[3])
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, TimeCheck[3] ? 4 : 2, 154) { ColorType = TimeCheck[1] ? Rand(1, 2) : 1 });
                    }
                    if (GametimeF <= BeatTime(125) && At0thBeat(2))
                    {
                        CreateBone(new UpBone(false, 3, 75));
                        CreateBone(new DownBone(true, 3, 75));
                    }
                }
                if (InBeat(145)) SetBox(270, 240, 120);
                if (InBeat(146)) SetBox(260, 180, 100);
                if (InBeat(147)) SetBox(250, 120, 90);
                if (InBeat(148)) { SetGreenBox(); TP(); }
                if (InBeat(148.5f)) SetSoul(1);
                if (InBeat(145, 148) && At0thBeat(1))
                {
                    float Cur = ScreenDrawing.ScreenScale;
                    RunEase((s) => ScreenDrawing.ScreenScale = s, EaseOut(BeatTime(1), Cur, Cur + 0.3f, EaseState.Back));
                }
                if (InBeat(148.75f))
                {
                    RunEase((s) => ScreenDrawing.ScreenScale = s, EaseOut(BeatTime(1), ScreenDrawing.ScreenScale, 1, EaseState.Quad));
                }
                #endregion
                #region Part 3 Green
                if (InBeat(147))
                {
                    string R2 = "(R)(R1)", D2 = "(D)(D1)";
                    CreateChart(BeatTime(1.5f), BeatTime(1), 7, new string[]
                    {
                        "", "", R2 + D2, "",
                        "", "", "", "", "", "", "", R2,
                        "", "", "", "", "", "", "", D2,
                        "", "", "", "", D2, "", "", "",
                        "", D2, "", "", "", "", D2, "",
                        "", "", D2, "", "", "", "", D2,
                        "", "", "", "", D2, "", "", "",
                        "", D2, "", "", "", "", D2, "",
                        "", "", D2, "", "", "", "", D2,
                        "", "", "", "", D2, "", "", "",
                        "", D2, "", "", R2, "", D2, "",
                        "", "", D2, "", "", "", "", "",
                        "", "", R2, "", "", "", D2, "",
                        R2, "", D2, "", "", "", "", D2,
                        "", "", "", D2, "", "", "", D2,
                        "", "", "", "", "", "", D2, "",
                        "", "", D2, "", "", "", D2, "",
                        "", "", D2, "", "", "R", "", "R1",
                        "", "", D2, "", "", "", D2, "",
                        "", "", D2, "", "", "", D2, "",
                    });
                }
                if (InBeat(164))
                {
                    string R2 = "(R)(R1)", D2 = "(D)(D1)";
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "", "", R2 + D2, "",
                        "", "", "", R2, "", "", "", R2,
                        "", "", "", R2, "", "", R2, "",
                        "", "", "", R2, "", "", "", R2,
                        "", "", "", R2, "", "", "", R2,
                        "", "", "", R2, "", "", "", "",
                        "", R2, "", R2, "", "", "", R2,
                        "", "", "", R2, "", "", "", R2,
                        "", "", "", R2, "", "", "", R2,
                        "", "", "", "", "", D2, "", D2,
                        "", D2, "", "", "", "", D2, "",
                        "", "", D2, "", "", "", "", D2,
                        "", "", D2, "", D2, "", "", "",
                        "", "", D2, "", "", "", D2, "",
                        "", "", D2, "", "", "", "", "",
                    });
                }
                if (InBeat(178.5f))
                {
                    string B = "($0)($2)", R = "($01)($21)";
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        B + R, "", "", "", R, "", "", "", "", "",
                        B + R, "", "", "", B, "", "", "",
                        B + R, "", "", "", R, "", "", "",
                        B + R, "", R, "", B, "", R, "",
                    });
                }
                #endregion
                #region Part 4 Red
                if (InBeat(187))
                {
                    Heart.InstantSplit(new(320, 240, 190, 190));
                    SetSoul(Souls.RedSoul);
                    BoxUtils.Vertexify();
                    var box = BoxUtils.VertexBoxInstance;
                    box.SetPosition(0, new(400, 160));
                    box.SetPosition(1, new(400, 320));
                    box.SetPosition(2, new(240, 320));
                    box.SetPosition(3, new(240, 160));
                }
                if (InBeat(189))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(0);
                        CreateBone(new SideCircleBone(i * 90, 3, 60, BeatTime(90)) { IsMasked = false });
                    }
                }
                if (InBeat(187, 999))
                {
                    if (At0thBeat(2.25f))
                    {
                        var box = FightBox.boxs[0] as VertexBox;
                        for (int i = 0; i < 4; i++)
                        {
                            float Rot = i * 90 + GametimeF * 0.7f;
                            box.SetPosition(i, new Vector2(320, 240) + GetVector2(80 * 1.41f, Rot));
                        }
                    }
                }
                #endregion
            }
            public void Start()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
                bool jump = true;
                if (!jump)
                {
                    Extends.DrawingUtil.BlackScreen(0, 0, BeatTime(12));
                    //Lines
                    RegisterFunctionOnce("L", () =>
                    {
                        Vector2 Start = new(320, Rand(20, 620));
                        float AngStart = Rand(-15, 15), Dur = BeatTime(Rand(2f, 4f));
                        Line l = new(
                            LinkEase(false, Stable(BeatTime(33) - GametimeF, Start),
                            EaseIn(Dur, Start, new Vector2(Rand(0, 640), Rand(700, 800)), EaseState.Back)),

                            LinkEase(false, Stable(BeatTime(33) - GametimeF, AngStart),
                            EaseIn(Dur, AngStart, -AngStart * 2, EaseState.Linear)));
                        l.AlphaDecrease(BeatTime(2), 0.8f);
                        CreateEntity(l);
                    });
                    for (int i = 0; i < 7; i++)
                        CreateChart(BeatTime(i * 4.75f), BeatTime(1), 0, new string[]
                        {
                            "L", "", "", "", "L", "", "", "",
                            "", "", "", "", "", "", "L", "",
                            "", "", "L", "", "", "", "L", "",
                            "", "", "L", "", "", "", "L", "",
                        });
                }
                else
                {
                    GametimeDelta = BeatTime(186);
                    PlayOffset = GametimeDelta;
                }
            }
        }
    }
}