using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Astronomia : IChampionShip
    {
        public Astronomia()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("time:9.5,12"));
            divisionInformation.PushNext(new SaveInfo("date:2,9"));
            divisionInformation.PushNext(new SaveInfo("dif:1,2"));

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Easy);
            difficulties.Add("div.1", Difficulty.Normal);
        }

        private Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        private SaveInfo divisionInformation;

        IWaveSet IChampionShip.GameContent => Game.instance;

        public class Game : IWaveSet
        {
            private float lastX = 0, lastY = 0;

            public const float SingleBeat = 7.44f;
            public static float BeatTime(float x) => x * SingleBeat;

            public static bool InBeat(float beat) => Gametime >= BeatTime(beat) && Gametime < BeatTime(beat) + 1;
            public static bool InBeat(float leftBeat, float rightBeat) => Gametime >= BeatTime(leftBeat) && Gametime <= BeatTime(rightBeat) + 1;

            public static bool At0thBeat(float beatCount) => (int)(Gametime % BeatTime(beatCount)) == 0;
            public static bool AtKthBeat(float beatCount, float K) => (int)(Gametime % BeatTime(beatCount)) == (int)K;

            public static Game instance;

            public string FightName => "Astronomia";

            public string Music => "Astronomia";

            class ThisInformation : SongInformation
            {
                public override string SongAuthor => "Vicetone + Tony Igy";
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "TK";
                public override string PaintAuthor => "Vicetone + Tony Igy";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Normal, 11.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Normal, 11.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 10.0f),
                            new(Difficulty.Normal, 16f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            public void Noob()
            {
                throw new System.NotImplementedException();
            }

            public void Extreme()
            {
                throw new System.NotImplementedException();
            }

            public void Hard()
            {
                throw new System.NotImplementedException();
            }

            public void Normal()
            {
                if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
                if (InBeat(8))
                {
                    CreateEntity(new Boneslab(90, 30, 20, (int)BeatTime(156) - 20
                        , Motions.LengthRoute.sin, new float[] { 23, BeatTime(16), 0, 36 }));
                    CreateEntity(new Boneslab(270, 30, 20, (int)BeatTime(156) - 20
                        , Motions.LengthRoute.sin, new float[] { 23, BeatTime(16), BeatTime(8), 36 }));
                }
                if (InBeat(4, 160 - 4) && AtKthBeat(16, 0))
                {
                    // CreateBone(new CustomBone(new Vector2(140, 290), Motions.PositionRoute.linear, 45, 100) { PositionRouteParam = new float[] { 3, 0 } });
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3.0f, 33));
                    CreateBone(new UpBone(false, 3.0f, 80));
                }
                if (InBeat(0, 156) && At0thBeat(16))
                {
                    Extends.DrawingUtil.BetterBlackScreen(BeatTime(8), 0, BeatTime(8), Color.Black);
                }
                if (InBeat(4, 160 - 4) && AtKthBeat(16, BeatTime(8f)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 3.0f, 33));
                    CreateBone(new DownBone(false, 3.0f, 80));
                }
                if (InBeat(96, 160 - 4) && AtKthBeat(8, 0))
                {
                    CreateGB(new NormalGB(GetVector2(120, Rand(0, 359)) + Heart.Centre, Heart.Centre, new Vector2(1.0f, 0.5f), BeatTime(8), BeatTime(2)));
                }
                if (InBeat(160))
                {
                    CreateEntity(new Boneslab(0, 50, 20, (int)BeatTime(128) - 20));
                    CreateEntity(new Boneslab(180, 50, 20, (int)BeatTime(128) - 20));
                }
                if (InBeat(160, 288 - 16) && At0thBeat(16))
                {
                    CreateGB(new NormalGB(new Vector2(200, 273), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(8), 12));
                    CreateGB(new NormalGB(new Vector2(200, 307), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(12), 12));
                }
                if (InBeat(160, 288))
                {
                    int way = 1;
                    for (int i = 0; i < 16; i += 4)
                    {
                        way *= -1;
                        if (AtKthBeat(16, BeatTime(i)))
                        {
                            Extends.DrawingUtil.LerpScreenPos(BeatTime(4), new(0, 20 * way), 0.12f);
                        }
                    }
                }
                if (InBeat(160, 288 - 16) && AtKthBeat(16, BeatTime(8)))
                {
                    CreateGB(new NormalGB(new Vector2(420, 273), Heart.Centre, new Vector2(1.0f, 0.5f), 180, BeatTime(8), 12));
                    CreateGB(new NormalGB(new Vector2(420, 307), Heart.Centre, new Vector2(1.0f, 0.5f), 180, BeatTime(12), 12));
                }
                if (InBeat(288))
                {
                    Extends.DrawingUtil.LerpScreenPos(BeatTime(8), new(0, 0), 0.12f);
                    SetSoul(2);
                    SetBox(290, 200, 120);
                    CreateEntity(new Boneslab(90, 30, 20, (int)BeatTime(128) - 20
                        , Motions.LengthRoute.sin, new float[] { 58, BeatTime(16), 0, 66 }));
                    CreateEntity(new Boneslab(270, 30, 20, (int)BeatTime(128) - 20
                        , Motions.LengthRoute.sin, new float[] { 58, BeatTime(16), BeatTime(8), 66 }));
                }
                if (InBeat(288 + 4, 416 - 8) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 4.0f, 36));
                    CreateBone(new DownBone(true, 4.0f, 36));
                }
                if (InBeat(328 + 24, 416) && AtKthBeat(8, 4))
                {
                    Extends.DrawingUtil.PlusScreenScale(0.2f, BeatTime(4));
                    Extends.DrawingUtil.RotateWithBack(BeatTime(4), RandBool() ? -15 : 15);
                }
                if (InBeat(416))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.24f);
                    SetSoul(0);
                    SetBox(290, 160, 160);
                }
                if (InBeat(416 + 4, 544 - 4) && At0thBeat(8))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1.5f, 0.16f);
                    Extends.DrawingUtil.LerpScreenPos(BeatTime(4), new Vector2(320, 240) - FightBox.instance.Centre, 0.16f);
                    for (int k = 0; k < 24; k++)
                        CreateBone(new SideCircleBone(k * 15, 2.0f, 120, BeatTime(2f)));
                }
                if (InBeat(416 + 4, 544 - 4) && AtKthBeat(8, BeatTime(4)))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1f, 0.16f);
                    Extends.DrawingUtil.LerpScreenPos(BeatTime(4), Vector2.Zero, 0.16f);
                }
                if (InBeat(416 + 4, 544 - 8) && AtKthBeat(8, BeatTime(3.7f)))
                {
                    float centreX, centreY;
                    while (true)
                    {
                        centreX = Rand(-45, 45) + 320; centreY = Rand(-45, 45) + 290;
                        if (GetDistance(new Vector2(lastX, lastY), new Vector2(centreX, centreY)) >= 70) break;
                    }
                    lastY = centreY; lastX = centreX;
                    SetBox(centreX - 80, centreX + 80, centreY - 80, centreY + 80);
                }
                if (InBeat(544))
                {
                    SetSoul(2);
                    SetBox(290, 160, 160);
                }
                if (InBeat(544 + 4, 608) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5, 50));
                    CreateBone(new DownBone(false, 5, 50));
                }
                if (InBeat(544 + 4, 608) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(true, 5, 50));
                    CreateBone(new DownBone(true, 5, 50));
                }

                if (InBeat(610))
                {
                    Extends.DrawingUtil.BetterBlackScreen(0, BeatTime(10), BeatTime(40), Color.Black);
                }
                if (InBeat(615))
                {
                    SetBox(240, 90, 48);
                    TP(320, 240);
                    ScreenDrawing.ThemeColor = Color.Brown;
                }
                if (InBeat(737, 960) && At0thBeat(4))
                {
                    ScreenDrawing.CameraEffect.Convulse(BeatTime(2), RandBool());
                }
                if (InBeat(870, 959))
                {
                    if (At0thBeat(4))
                        Extends.DrawingUtil.LerpScreenScale(BeatTime(2), 1.3f, 0.16f);
                    if (AtKthBeat(4, BeatTime(2)))
                        Extends.DrawingUtil.LerpScreenScale(BeatTime(2), 1f, 0.16f);
                }

                float detla = 960;
                if (InBeat(detla))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(2), 1f, 0.16f);
                    SetSoul(2);
                    SetBox(290, 160, 160);
                    Extends.DrawingUtil.BetterBlackScreen(0, BeatTime(1), BeatTime(8), Color.Black);
                    ScreenDrawing.ThemeColor = Color.White;
                }
                if (InBeat(160 + detla))
                {
                    CreateEntity(new Boneslab(90, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 33, BeatTime(16), 0, 36 }));
                    CreateEntity(new Boneslab(270, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 33, BeatTime(16), BeatTime(8), 36 }));
                }
                if (InBeat(4 + detla, 160 - 4 + detla) && AtKthBeat(16, 0))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3.0f, 33));
                    CreateBone(new UpBone(false, 3.0f, 70));
                }
                if (InBeat(4 + detla, 160 - 4 + detla) && AtKthBeat(16, BeatTime(8f)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 3.0f, 33));
                    CreateBone(new DownBone(false, 3.0f, 70));
                }
                if (InBeat(0 + detla, 156 + detla) && At0thBeat(16))
                {
                    Extends.DrawingUtil.BetterBlackScreen(BeatTime(8), 0, BeatTime(8), Color.Black);
                }
                if (InBeat(96 + detla, 160 - 4 + detla) && AtKthBeat(8, 0))
                {
                    CreateGB(new NormalGB(GetVector2(120, Rand(0, 359)) + Heart.Centre, Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(8), BeatTime(1)));
                }
                if (InBeat(160 + detla))
                {
                    SetSoul(0);
                    CreateEntity(new Boneslab(0, 50, 20, (int)BeatTime(128) - 20));
                    CreateEntity(new Boneslab(180, 50, 20, (int)BeatTime(128) - 20));
                }
                if (InBeat(160 + detla, 288 - 32 + detla) && At0thBeat(16))
                {
                    CreateGB(new NormalGB(new Vector2(200, 273), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(8), 12));
                    CreateGB(new NormalGB(new Vector2(200, 307), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(12), 12));
                }
                if (InBeat(160 + detla, 288 - 32 + detla) && AtKthBeat(16, BeatTime(8)))
                {
                    CreateGB(new NormalGB(new Vector2(420, 273), Heart.Centre, new Vector2(1.0f, 0.5f), 180, BeatTime(8), 12));
                    CreateGB(new NormalGB(new Vector2(420, 307), Heart.Centre, new Vector2(1.0f, 0.5f), 180, BeatTime(12), 12));
                }
                if (InBeat(160 + detla, 288 + detla))
                {
                    int way = 1;
                    for (int i = 0; i < 16; i += 4)
                    {
                        way *= -1;
                        if (AtKthBeat(16, BeatTime(i)))
                        {
                            Extends.DrawingUtil.LerpScreenPos(BeatTime(4), new(0, 20 * way), 0.12f);
                        }
                    }
                }
                if (InBeat(288 + detla))
                {
                    SetSoul(2);
                    SetBox(290, 190, 110);
                    CreateEntity(new Boneslab(90, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 58, BeatTime(16), 0, 66 }));
                    CreateEntity(new Boneslab(270, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 58, BeatTime(16), BeatTime(8), 66 }));
                }
                if (InBeat(288 + 4 + detla, 416 - 8 + detla) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 4.0f, 36));
                    CreateBone(new DownBone(true, 4.0f, 36));
                }
                if (InBeat(328 + 24 + detla, 416 + detla) && AtKthBeat(8, 4))
                {
                    Extends.DrawingUtil.PlusScreenScale(0.2f, BeatTime(4));
                    Extends.DrawingUtil.RotateWithBack(BeatTime(4), RandBool() ? -15 : 15);
                }
                if (InBeat(416 + detla))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.24f);
                    SetSoul(0);
                    SetBox(290, 160, 160);
                }
                if (InBeat(416 + 4 + detla, 544 - 4 + detla) && At0thBeat(8))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1.5f, 0.16f);
                    Extends.DrawingUtil.LerpScreenPos(BeatTime(4), new Vector2(320, 240) - FightBox.instance.Centre, 0.16f);
                    for (int k = 0; k < 24; k++)
                        CreateBone(new SideCircleBone(k * 15, 2.0f, 120, BeatTime(2f)));
                }
                if (InBeat(416 + 4 + detla, 544 - 4 + detla) && AtKthBeat(8, BeatTime(4)))
                {
                    Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1f, 0.16f);
                    Extends.DrawingUtil.LerpScreenPos(BeatTime(4), Vector2.Zero, 0.16f);
                }
                if (InBeat(416 + 4 + detla, 544 - 8 + detla) && AtKthBeat(8, BeatTime(3.7f)))
                {
                    float centreX, centreY;
                    while (true)
                    {
                        centreX = Rand(-45, 45) + 320; centreY = Rand(-45, 45) + 290;
                        if (GetDistance(new Vector2(lastX, lastY), new Vector2(centreX, centreY)) >= 70) break;
                    }
                    lastY = centreY; lastX = centreX;
                    SetBox(centreX - 80, centreX + 80, centreY - 80, centreY + 80);
                }
                if (InBeat(544 + detla))
                {
                    SetSoul(2);
                    SetBox(290, 160, 160);
                }
                if (InBeat(544 + 4 + detla, 608 + detla) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5, 50));
                    CreateBone(new DownBone(false, 5, 50));
                }
                if (InBeat(544 + 4 + detla, 608 + detla) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(true, 5, 50));
                    CreateBone(new DownBone(true, 5, 50));
                }
                if (InBeat(608 + detla))
                {
                    SetSoul(0);
                }
                if (InBeat(608 + 4 + detla, 672 + detla) && At0thBeat(4))
                {
                    CreateGB(new NormalGB(GetVector2(120, Rand(0, 359)) + Heart.Centre, Heart.Centre, new Vector2(1.0f, 0.5f), BeatTime(8), BeatTime(2)));
                }
            }

            public void Easy()
            {
                if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
                if (InBeat(4, 160 - 4) && AtKthBeat(16, 0))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3.0f, 33));
                    CreateBone(new UpBone(false, 3.0f, 70));
                }
                if (InBeat(4, 160 - 4) && AtKthBeat(16, BeatTime(8f)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 3.0f, 33));
                    CreateBone(new DownBone(false, 3.0f, 70));
                }
                if (InBeat(96, 160 - 4) && AtKthBeat(8, 0))
                {
                    CreateGB(new NormalGB(GetVector2(120, Rand(0, 359)) + Heart.Centre, Heart.Centre, new Vector2(1.0f, 0.5f), BeatTime(8), BeatTime(1)));
                }
                if (InBeat(160))
                {
                    CreateEntity(new Boneslab(0, 50, 20, (int)BeatTime(128) - 20));
                    CreateEntity(new Boneslab(180, 50, 20, (int)BeatTime(128) - 20));
                }
                if (InBeat(160, 288 - 24) && At0thBeat(16))
                {
                    CreateGB(new NormalGB(new Vector2(200, 273), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(8), 12));
                    CreateGB(new NormalGB(new Vector2(200, 307), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(16), 12));
                }
                if (InBeat(288))
                {
                    SetSoul(2);
                    SetBox(290, 200, 120);
                }
                if (InBeat(288 + 4, 416 - 8) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 4.0f, 36));
                    CreateBone(new DownBone(true, 4.0f, 36));
                }
                if (InBeat(416))
                {
                    SetSoul(0);
                    SetBox(290, 160, 160);
                }
                if (InBeat(416 + 4, 544 - 4) && At0thBeat(8))
                {
                    for (int k = 0; k < 24; k++)
                        CreateBone(new SideCircleBone(k * 15, 2.0f, 80, BeatTime(2f)));
                }
                if (InBeat(416 + 4, 544 - 8) && AtKthBeat(8, BeatTime(3.7f)))
                {
                    float centreX, centreY;
                    while (true)
                    {
                        centreX = Rand(-35, 35) + 320; centreY = Rand(-35, 35) + 290;
                        if (GetDistance(new Vector2(lastX, lastY), new Vector2(centreX, centreY)) >= 50) break;
                    }
                    lastY = centreY; lastX = centreX;
                    SetBox(centreX - 80, centreX + 80, centreY - 80, centreY + 80);
                }
                if (InBeat(544))
                {
                    SetSoul(2);
                    SetBox(290, 160, 160);
                }
                if (InBeat(544 + 4, 608) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5, 50));
                    CreateBone(new DownBone(false, 5, 50));
                }

                float detla = 960;
                if (InBeat(160 + detla))
                {
                    CreateEntity(new Boneslab(90, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 33, BeatTime(16), 0, 36 }));
                    CreateEntity(new Boneslab(270, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 33, BeatTime(16), BeatTime(8), 36 }));
                }
                if (InBeat(4 + detla, 160 - 4 + detla) && AtKthBeat(16, 0))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3.0f, 33));
                    CreateBone(new UpBone(false, 3.0f, 50));
                }
                if (InBeat(4 + detla, 160 - 4 + detla) && AtKthBeat(16, BeatTime(8f)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 3.0f, 33));
                    CreateBone(new DownBone(false, 3.0f, 50));
                }
                if (InBeat(96 + detla, 160 - 4 + detla) && AtKthBeat(8, 0))
                {
                    CreateGB(new NormalGB(GetVector2(120, Rand(0, 359)) + Heart.Centre, Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(8), BeatTime(1)));
                }
                if (InBeat(160 + detla))
                {
                    SetSoul(0);
                    CreateEntity(new Boneslab(0, 50, 20, (int)BeatTime(128) - 20));
                    CreateEntity(new Boneslab(180, 50, 20, (int)BeatTime(128) - 20));
                }
                if (InBeat(160 + detla, 288 - 32 + detla) && At0thBeat(16))
                {
                    CreateGB(new NormalGB(new Vector2(200, 273), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(8), 12));
                    CreateGB(new NormalGB(new Vector2(200, 307), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(16), 12));
                }
                if (InBeat(288 + detla))
                {
                    SetSoul(2);
                    SetBox(290, 190, 110);
                    CreateEntity(new Boneslab(90, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 48, BeatTime(16), 0, 56 }));
                    CreateEntity(new Boneslab(270, 30, 20, (int)BeatTime(120) - 20
                        , Motions.LengthRoute.sin, new float[] { 48, BeatTime(16), BeatTime(8), 56 }));
                }
                if (InBeat(288 + 4 + detla, 416 - 8 + detla) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 4.0f, 26));
                    CreateBone(new DownBone(true, 4.0f, 26));
                }
                if (InBeat(416 + detla))
                {
                    SetSoul(0);
                    SetBox(290, 160, 160);
                }
                if (InBeat(416 + 4 + detla, 544 - 4 + detla) && At0thBeat(8))
                {
                    for (int k = 0; k < 24; k++)
                        CreateBone(new SideCircleBone(k * 15, 2.0f, 100, BeatTime(2f)));
                }
                if (InBeat(416 + 4 + detla, 544 - 8 + detla) && AtKthBeat(8, BeatTime(3.7f)))
                {
                    float centreX, centreY;
                    while (true)
                    {
                        centreX = Rand(-35, 35) + 320; centreY = Rand(-35, 35) + 290;
                        if (GetDistance(new Vector2(lastX, lastY), new Vector2(centreX, centreY)) >= 50) break;
                    }
                    lastY = centreY; lastX = centreX;
                    SetBox(centreX - 80, centreX + 80, centreY - 80, centreY + 80);
                }
                if (InBeat(544 + detla))
                {
                    SetSoul(2);
                    SetBox(290, 160, 160);
                }
                if (InBeat(544 + 4 + detla, 608 + detla) && At0thBeat(8))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5, 20));
                    CreateBone(new DownBone(false, 5, 20));
                }
                if (InBeat(544 + 4 + detla, 608 + detla) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(true, 5, 20));
                    CreateBone(new DownBone(true, 5, 20));
                }
                if (InBeat(608 + detla))
                {
                    SetSoul(0);
                }
                if (InBeat(608 + 4 + detla, 672 + detla) && At0thBeat(8))
                {
                    CreateGB(new NormalGB(GetVector2(120, Rand(0, 359)) + Heart.Centre, Heart.Centre, new Vector2(1.0f, 1f), BeatTime(8), BeatTime(2)));
                }
            }

            public void Start()
            {
                GametimeDelta = 2 + BeatTime(4);
                Heart.Speed = 3.2f; SetBox(290, 160, 160);
                SetSoul(0);
                HeartAttribute.MaxHP = 9;
                TP();
            }

            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}