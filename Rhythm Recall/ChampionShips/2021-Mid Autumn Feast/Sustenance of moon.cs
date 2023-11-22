using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class SustenanceOfMoon : IChampionShip
    {
        public SustenanceOfMoon()
        {
            Game.instance = new Game();

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Easy);
            difficulties.Add("div.1", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {

            class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string SongAuthor => "S.I.N.G";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Extreme, 16.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 5.5f),
                            new(Difficulty.Extreme, 16.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 10.0f),
                            new(Difficulty.Extreme, 20.5f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (560f / 60f))
            { }

            public static Game instance;

            public string Music => "sustenance of moon";

            public string FightName => "Sustenance of Moon";

            #region Non-ChampionShip
            public void Normal()
            {
                throw new System.NotImplementedException();
            }
            public void Noob()
            {
                throw new System.NotImplementedException();
            }
            public void Hard()
            {
                throw new System.NotImplementedException();
            }
            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }
            #endregion

            private void CirssCrossGB(Vector2 centre, int count, float rotation, float waitingBeat)
            {
                float rot = rotation;
                for (int i = 0; i < count; i++)
                {
                    Vector2 pos = centre + GetVector2(165, rotation + i * 360f / count);
                    CreateGB(new NormalGB(pos, pos + GetVector2(50, Rand(0, 359)), new(1, 0.5f), BeatTime(waitingBeat), BeatTime(1)));
                }
            }

            private Vector2 screenMission = Vector2.Zero;
            private float scaleMission = 1f;

            public void Easy()
            {
                if (Gametime < 0) return;
                if (InBeat(16))
                {
                    CirssCrossGB(Heart.Centre, 8, 0, 12);
                }
                if (InBeat(32))
                {
                    CirssCrossGB(Heart.Centre, 8, 22.5f, 12);
                }
                if (InBeat(48))
                {
                    CirssCrossGB(Heart.Centre, 8, -22.5f, 12);
                }
                if (InBeat(64))
                {
                    CirssCrossGB(Heart.Centre, 8, 0, 12);
                }
                if (InBeat(80 + 1, 80 + 64 - 1) && At0thBeat(8)) PlaySound(Sounds.pierce);
                if (InBeat(80, 80 + 64 - 9) && AtKthBeat(16, 0))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i <= 4; i++)
                        CreateBone(new CustomBone(new(200 - i * 9, 300), (s) =>
                        {
                            return s.AppearTime < BeatTime(8)
                                ? s.CentrePosition + new Vector2((BeatTime(8) - s.AppearTime) * 0.025f / 2f, 0)
                                : s.CentrePosition + new Vector2(9 / 2f, 0);
                        }, 0, 155)
                        { ColorType = ctype });
                }
                if (InBeat(80, 80 + 64 - 9) && AtKthBeat(16, BeatTime(8)))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i <= 4; i++)
                        CreateBone(new CustomBone(new(440 + i * 9, 300), (s) =>
                        {
                            return s.AppearTime < BeatTime(8)
                                ? s.CentrePosition - new Vector2((BeatTime(8) - s.AppearTime) * 0.025f / 2f, 0)
                                : s.CentrePosition - new Vector2(9 / 2f, 0);
                        }, 0, 155)
                        { ColorType = ctype });
                }

                if (InBeat(144 - 4))
                {
                    SetBox(300, 240, 175);
                    SetSoul(2);
                    Heart.GiveForce(0, 11);
                    CreateEntity(new Boneslab(0, 32, (int)BeatTime(6), (int)BeatTime(124)));
                }
                if (InBeat(144 - 2))
                {
                    CreatePlatform(new Platform(1, new(320, 342), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                    {
                        PositionRouteParam = new float[] { 0, 75, BeatTime(32), 0 },
                        RotationRouteParam = new float[] { 32, BeatTime(16), 0, 0 },
                        LengthRouteParam = new float[] { 52, BeatTime(124) }
                    });
                }
                if (InBeat(144, 144 + 128 - 24) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateEntity(new CustomBone(new(320, 200), (s) => { return s.CentrePosition + new Vector2(0, 1.2f / 2f); }, 0, 20));
                }
                if (InBeat(272 - 6))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(272 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","","",

                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","R1","",
                    });
                }
                if (InBeat(336 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(400 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 5.2f, 0, 0);
                        if (x % 8 == 0) CreateArrow(time, x / 4, 5.1f, 1, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "(^D1)","","","",   "R1","","","",
                        "R","","","",   "","","","",
                        "(^R1)","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "(^R1)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "(^R1)","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "(^R1)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "(^R1)","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "(^R1)","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(^R1)","","","",   "R","","","",
                        "","","","",   "","","","",

                        "(^R1)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "(^R1)","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "(^R1)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "(^R1)","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "","","","",

                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "","","","",   "(R)(+01)","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(464 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                        {
                            CreateArrow(time, "R", 5.2f, 0, 0);
                            if (x >= 29)
                                CreateArrow(time, "+0", 5.2f, 1, 0);
                        }
                        if (x % 8 == 0 && x <= 28) CreateArrow(time, x / 4, 4.1f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(528 - 4))
                {
                    SetSoul(2);
                }
                if (InBeat(528 - 3, 528 + 108) && AtKthBeat(16, BeatTime(15.6f)))
                {
                    float dx = Rand(-120, 120);
                    float dy = Rand(-30, 60);
                    float x1, x2, y1, y2;
                    SetBox(x1 = 320 + dx - 75, x2 = 320 + dx + 75, y1 = 240 + dy - 75, y2 = 240 + dy + 75);
                    screenMission = new(dx, dy);

                    float rot = Rand(0, 3) * 90;
                    Heart.GiveForce(rot, 10);
                    CreateEntity(new Boneslab(rot, 40, (int)BeatTime(8), 20));

                    scaleMission = Rand(100, 160) * 0.01f;
                }
                if (InBeat(656 - 12))
                {
                    scaleMission = 1f;
                    screenMission = Vector2.Zero;
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    Heart.RotateTo(0);
                }
                if (InBeat(528 - 1, 528 + 136))
                {
                    ScreenDrawing.ScreenPositionDelta = ScreenDrawing.ScreenPositionDelta * 0.8f + screenMission * 0.2f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + scaleMission * 0.15f;
                }
                if (InBeat(656 - 16))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                    });
                }
                if (InBeat(720 - 20))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(784 - 12))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
 
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(4), BeatTime(4), 6.5f, new string[]
                    {
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",
                    });
                }
                if (InBeat(848 - 20))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {

                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(912 - 4))
                {
                    SetBox(300, 240, 175);
                    SetSoul(2);
                    Heart.GiveForce(0, 11);
                    CreateEntity(new Boneslab(0, 32, (int)BeatTime(6), (int)BeatTime(124)));
                }
                if (InBeat(912 - 2))
                {
                    CreatePlatform(new Platform(1, new(320, 342), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                    {
                        PositionRouteParam = new float[] { 0, 75, BeatTime(32), 0 },
                        RotationRouteParam = new float[] { 32, BeatTime(16), 0, 0 },
                        LengthRouteParam = new float[] { 52, BeatTime(124) }
                    });
                }
                if (InBeat(912, 912 + 128 - 24) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateEntity(new CustomBone(new(320, 200), (s) =>
                    {
                        return new Vector2(Cos01(Gametime / BeatTime(16)) * 80, s.CentrePosition.Y + 1.2f / 2f);
                    }, 0, 20)
                    { Tags = new string[] { "Sweeper" } });
                }
                if (InBeat(1040 - 6))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(1040 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","","",
                        //
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","R1","",
                        ///
                        "(D)","","","",   "R1","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","","",
                        ///
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "","","","",

                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "","","","",   "(R)(+01)","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(1104 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {

                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(1168 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {

                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 5.2f, 0, 0);
                        if (x % 8 == 0) CreateArrow(time, x / 4, 5.1f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(1232 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {

                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                        {
                            CreateArrow(time, "R", 5.2f, 0, 0);
                            if (x >= 29)
                                CreateArrow(time, "+0", 5.2f, 1, 0);
                        }
                        if (x % 8 == 0 && x <= 28) CreateArrow(time, x / 4, 4.1f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(1296 - 4))
                {
                    SetSoul(0);
                }
                if (InBeat(1296 - 3, 1296 + 108) && AtKthBeat(16, BeatTime(15.6f)))
                {
                    float dx = Rand(-120, 120);
                    float dy = Rand(-30, 60);
                    float x1, x2, y1, y2;
                    SetBox(x1 = 320 + dx - 75, x2 = 320 + dx + 75, y1 = 240 + dy - 75, y2 = 240 + dy + 75);
                    screenMission = new(dx, dy);
                    scaleMission = Rand(100, 160) * 0.01f;
                }
                if (InBeat(1296 - 3, 1296 + 108) && AtKthBeat(16, BeatTime(4.6f)))
                {
                    for (int i = 0; i < 355; i += 90)
                    {
                        CreateBone(new SideCircleBone(i, 2.8f, 120, BeatTime(4)));
                    }
                }

                if (InBeat(1424 - 16))
                {
                    scaleMission = 1f;
                    screenMission = Vector2.Zero;
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    Heart.RotateTo(0);
                }
                if (InBeat(1296 - 1, 1296 + 136))
                {
                    ScreenDrawing.ScreenPositionDelta = ScreenDrawing.ScreenPositionDelta * 0.8f + screenMission * 0.2f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + scaleMission * 0.15f;
                }
                if (InBeat(1424 - 12))
                {
                    CreateChart(BeatTime(4), BeatTime(4), 6.5f, new string[]
                 {
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                     //
                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                 });
                }
                if (InBeat(1680 - 12))
                {
                    CreateChart(BeatTime(4), BeatTime(4), 6.5f, new string[]
                 {
                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                     //
                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "(R)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",

                        "(R)","","","",   "R","","","",
                        "(R)","","","",   "R","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                 });
                }
                if (InBeat(1938 - 4))
                {
                    SetBox(300, 240, 175);
                    SetSoul(2);
                    Heart.GiveForce(0, 11);
                    CreateEntity(new Boneslab(0, 32, (int)BeatTime(6), (int)BeatTime(124)));
                }
                if (InBeat(1938 - 2))
                {
                    CreatePlatform(new Platform(1, new(320, 342), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                    {
                        PositionRouteParam = new float[] { 0, 75, BeatTime(32), 0 },
                        RotationRouteParam = new float[] { 32, BeatTime(16), 0, 0 },
                        LengthRouteParam = new float[] { 52, BeatTime(124) }
                    });
                }
                if (InBeat(1938, 1938 + 128 - 24) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateEntity(new DownBone(false, 2.6f, 54));
                }
                if (InBeat(1938, 1938 + 128 - 24) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(Sounds.pierce);
                    CreateEntity(new UpBone(true, 4, 50));
                }
            }

            public void Extreme()
            {
                if (Gametime < 0) return;
                if (InBeat(16))
                {
                    CirssCrossGB(Heart.Centre, 12, 0, 12);
                }
                if (InBeat(32))
                {
                    CirssCrossGB(Heart.Centre, 12, 180 / 12f, 12);
                }
                if (InBeat(48))
                {
                    CirssCrossGB(Heart.Centre, 12, 0, 12);
                }
                if (InBeat(64))
                {
                    CirssCrossGB(Heart.Centre, 12, 180 / 12f, 12);
                }
                if (InBeat(80 + 1, 80 + 64 - 1) && At0thBeat(4)) PlaySound(Sounds.pierce);
                if (InBeat(80, 80 + 64 - 9) && AtKthBeat(8, 0))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i <= 4; i++)
                        CreateBone(new CustomBone(new(210 - i * 9, 300), (s) =>
                        {
                            return s.AppearTime < BeatTime(4)
                                ? s.CentrePosition + new Vector2((BeatTime(4) - s.AppearTime) * 0.055f / 2f, 0)
                                : s.CentrePosition + new Vector2(9 / 2f, 0);
                        }, 0, 155)
                        { ColorType = ctype });
                }
                if (InBeat(80, 80 + 64 - 9) && AtKthBeat(8, BeatTime(4)))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i <= 4; i++)
                        CreateBone(new CustomBone(new(430 + i * 9, 300), (s) =>
                        {
                            return s.AppearTime < BeatTime(4)
                                ? s.CentrePosition - new Vector2((BeatTime(4) - s.AppearTime) * 0.055f / 2f, 0)
                                : s.CentrePosition - new Vector2(9 / 2f, 0);
                        }, 0, 155)
                        { ColorType = ctype });
                }

                if (InBeat(144 - 4))
                {
                    SetBox(300, 240, 175);
                    SetSoul(2);
                    Heart.GiveForce(0, 11);
                    CreateEntity(new Boneslab(0, 32, (int)BeatTime(6), (int)BeatTime(124)));
                }
                if (InBeat(144 - 2))
                {
                    CreatePlatform(new Platform(1, new(320, 360), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                    {
                        PositionRouteParam = new float[] { 0, 75, BeatTime(32), 0 },
                        RotationRouteParam = new float[] { 36, BeatTime(16), 0, 0 },
                        LengthRouteParam = new float[] { 58, BeatTime(124) }
                    });
                }
                if (InBeat(144, 144 + 128 - 24) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateEntity(new CustomBone(new(320, 200), (s) => { return s.CentrePosition + new Vector2(0, 1.4f / 2f); }, 0, 30));
                }
                if (InBeat(272 - 6))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(272 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","","",

                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","R1","",
                    });
                }
                if (InBeat(400 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {

                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "(R)(^D1'1.3)","","","",   "R1","","","",
                        "R","","","",   "","","","",
                        "R(^R1'1.3)","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "(^R1'1.3)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "(^R1'1.3)","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "(^R1'1.3)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(^R1'1.3)","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R(^R1'1.3)","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(^R1'1.3)","","","",   "R","","","",
                        "","","","",   "","","","",

                        "R(^R1'1.3)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(^R1'1.3)","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "(^R1'1.3)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "(^R1'1.3)","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "","","","",

                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "","","","",   "(R)(+01)","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(400 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 5.2f, 0, 0);
                        if (x % 8 == 0) CreateArrow(time, "R", 11.1f, 1, 0, ArrowAttribute.SpeedUp);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(464 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                        {
                            CreateArrow(time, "R", 5.2f, 0, 0);
                            if (x >= 29)
                                CreateArrow(time, "+0", 5.2f, 1, 0);
                            else if (x % 8 == 0) CreateArrow(time, "R", 11.1f, 1, 0, ArrowAttribute.SpeedUp);
                        }
                        time += BeatTime(1);
                    });
                }
                if (InBeat(528 - 4))
                {
                    SetSoul(2);
                }
                if (InBeat(528 - 3, 528 + 108) && AtKthBeat(16, BeatTime(15.6f)))
                {
                    float dx = Rand(-120, 120);
                    float dy = Rand(-30, 60);
                    float x1, x2, y1, y2;
                    SetBox(x1 = 320 + dx - 75, x2 = 320 + dx + 75, y1 = 240 + dy - 75, y2 = 240 + dy + 75);
                    screenMission = new(dx, dy);

                    float rot = Rand(0, 3) * 90;
                    Heart.GiveForce(rot, 10);
                    CreateEntity(new Boneslab(rot, 80, (int)BeatTime(8), 40));

                    scaleMission = Rand(100, 160) * 0.01f;
                }
                if (InBeat(656 - 16))
                {
                    scaleMission = 1f;
                    screenMission = Vector2.Zero;
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    Heart.RotateTo(0);
                }
                if (InBeat(528, 528 + 136))
                {
                    ScreenDrawing.ScreenPositionDelta = ScreenDrawing.ScreenPositionDelta * 0.8f + screenMission * 0.2f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + scaleMission * 0.15f;
                }
                if (InBeat(656 - 16))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2 || x % 4 == 0) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                    });
                }
                if (InBeat(720 - 20))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2 || x % 4 == 0) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(784 - 12))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2 || x % 4 == 0) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(4), BeatTime(4), 6.5f, new string[]
                    {
                        "","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",
                    });
                }
                if (InBeat(848 - 20))
                {
                    float time = BeatTime(12.5f);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (rhythm[x] == 2 || (x % 4 == 0 && rhythm[x] != 3)) CreateArrow(time, "R", 6.5f, 1, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(912 - 4))
                {
                    SetBox(300, 240, 175);
                    SetSoul(2);
                    Heart.GiveForce(0, 11);
                    CreateEntity(new Boneslab(0, 32, (int)BeatTime(6), (int)BeatTime(124)));
                }
                if (InBeat(912 - 2))
                {
                    CreatePlatform(new Platform(1, new(320, 350), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                    {
                        PositionRouteParam = new float[] { 0, 75, BeatTime(32), 0 },
                        RotationRouteParam = new float[] { 36, BeatTime(16), 0, 0 },
                        LengthRouteParam = new float[] { 58, BeatTime(124) }
                    });
                }
                if (InBeat(912, 912 + 128 - 24) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateEntity(new CustomBone(new(320, 200), (s) =>
                    {
                        return new Vector2(Cos01(Gametime / BeatTime(16)) * 80, s.CentrePosition.Y + 1.1f / 2f);
                    }, 0, 20)
                    { Tags = new string[] { "Sweeper" } });
                }
                if (InBeat(1040 - 6))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(1040 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                    CreateChart(BeatTime(8), BeatTime(4), 6.5f, new string[]
                    {
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","","",
                        //
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "","","","",   "","","R1","",
                        ///
                        "(D)(D1)","","","",   "R1","","","",
                        "R","","","",   "","","","",
                        "R(<D1)","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "(<D1)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "(<D1)","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "(<D1)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(<D1)","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R(<D1)","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "(<D1)","","","",   "R","","","",
                        "","","","",   "","","","",
                        ///
                        "R(<D1)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(<D1)","","","",   "","","","",
                        "R","","","",   "R","","","",

                        "(<D1)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "(<D1)","","","",   "R","","","",
                        "","","","",   "R","","","",

                        "(<D1)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "","","","",

                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "(R)(+01)","","","",   "(R)(+01)","","","",
                        "","","","",   "(R)(+01)","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(1104 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 6.2f, 0, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(1168 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                            CreateArrow(time, "R", 5.2f, 0, 0);
                        if (x % 4 == 0) CreateArrow(time, "R", 6.1f, 1, 0, x % 8 == 0 ? ArrowAttribute.RotateL : ArrowAttribute.RotateR);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(1232 - 12))
                {
                    float time = BeatTime(8);
                    int[] rhythm = {
                    };
                    Fortimes(rhythm.Length, (x) =>
                    {
                        if (rhythm[x] == 1)
                        {
                            CreateArrow(time, "R", 5.2f, 0, 0);
                            if (x >= 29)
                                CreateArrow(time, "+0", 5.2f, 1, 0);
                            else if (x % 4 == 0) CreateArrow(time, "R", 6.1f, 1, 0, x % 8 == 0 ? ArrowAttribute.RotateL : ArrowAttribute.RotateR);
                        }
                        time += BeatTime(1);
                    });
                }
                if (InBeat(1296 - 4))
                {
                    SetSoul(0);
                }
                if (InBeat(1296 - 3, 1296 + 108) && AtKthBeat(16, BeatTime(15.6f)))
                {
                    float dx = Rand(-120, 120);
                    float dy = Rand(-30, 60);
                    float x1, x2, y1, y2;
                    SetBox(x1 = 320 + dx - 75, x2 = 320 + dx + 75, y1 = 240 + dy - 75, y2 = 240 + dy + 75);
                    screenMission = new(dx, dy);
                    scaleMission = Rand(100, 160) * 0.01f;
                }
                if (InBeat(1296 - 3, 1296 + 108) && AtKthBeat(16, BeatTime(4.6f)))
                {
                    for (int i = 0; i < 355; i += 90)
                    {
                        CreateBone(new SideCircleBone(i, 3.2f, 140, BeatTime(4)));
                    }
                }

                if (InBeat(1424 - 16))
                {
                    scaleMission = 1f;
                    screenMission = Vector2.Zero;
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    Heart.RotateTo(0);
                }
                if (InBeat(1296 - 1, 1296 + 136))
                {
                    ScreenDrawing.ScreenPositionDelta = ScreenDrawing.ScreenPositionDelta * 0.8f + screenMission * 0.2f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + scaleMission * 0.15f;
                }
                if (InBeat(1424-12))
                {
                    CreateChart(BeatTime(4), BeatTime(4), 6.5f, new string[]
                 {
                        "","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                     //
                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                 });
                }
                if (InBeat(1680 - 12))
                {
                    CreateChart(BeatTime(4), BeatTime(4), 6.5f, new string[]
                 {
                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                     //
                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "R1","","","",
                        "R","","","",   "R1","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(+01)","","","",   "R","","","",
                        "(R1)","","","",   "R","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",

                        "(R)(D1)","","","",   "R","","","",
                        "(R)(D1)","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",
                        "R1","","","",   "R1","","","",

                 });
                }

                if (InBeat(1938 - 4))
                    {
                        SetBox(300, 240, 175);
                        SetSoul(2);
                        Heart.GiveForce(0, 11);
                        CreateEntity(new Boneslab(0, 32, (int)BeatTime(6), (int)BeatTime(124)));
                    }
                    if (InBeat(1938 - 2))
                    {
                        CreatePlatform(new Platform(1, new(320, 342), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                        {
                            PositionRouteParam = new float[] { 0, 75, BeatTime(32), 0 },
                            RotationRouteParam = new float[] { 32, BeatTime(16), 0, 0 },
                            LengthRouteParam = new float[] { 52, BeatTime(124) }
                        });
                    }
                    if (InBeat(1938, 1938 + 128 - 24) && At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new DownBone(false, 2.6f, 64));
                    }
                    if (InBeat(1938, 1938 + 128 - 24) && AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateEntity(new UpBone(true, 4, 64));
                    }
                }
            

                public void Start()
                {
                    ScreenDrawing.UIColor = Color.Snow;

                    HeartAttribute.Speed = 2.86f;
                    SetSoul(0);
                    SetBox(300, 200, 160);
                    GametimeDelta = -70;
                    bool jump = false;
                    if (jump)
                    {
                        GametimeDelta = this.BeatTime(500) - 61f;
                        PlayOffset = this.BeatTime(500);
                    }


                    HeartAttribute.MaxHP = 6;
                }
            }
        }
    }
