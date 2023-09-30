using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using Extends;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class NightofKnights : IChampionShip
    {
        public NightofKnights()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new()
            {
                { "div.3", Difficulty.Noob },
                { "div.2", Difficulty.Easy },
                { "div.1", Difficulty.Hard },
                { "div.0", Difficulty.Extreme },
            };
        }


        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class KickCounter : Entity
            {
                public override void Draw()
                {
                    Font.NormalFont.CentreDraw(count + 1 + "", new Microsoft.Xna.Framework.Vector2(320, 80), Color.White, GameStates.SpriteBatch);
                    if (time > 0)
                    {
                        Font.NormalFont.CentreDraw("Time = " + (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 120), Color.White, GameStates.SpriteBatch);
                        Font.NormalFont.CentreDraw("Frame = " + 60 * (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 160), Color.White, GameStates.SpriteBatch);
                    }
                }

                private int count = -1;
                private float time = 0;

                public override void Update()
                {
                    if (GameStates.IsKeyPressed(InputIdentity.Alternate) && time == 0)
                    {
                        count = 0;
                        time += 0.001f;
                        return;
                    }
                    if (time == 0) return;
                    time++;
                    if (GameStates.IsKeyPressed(InputIdentity.Alternate))
                    {
                        count++;
                        PlaySound(Sounds.pierce);
                    }
                }
            }

            public Game() : base(5.208f) { }

            public static Game instance;

            public string Music => "Night Of Knights";

            public string FightName => "Night Of Knights";

            class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => GameStates.difficulty == 4 ? "T-mas, modded by TK" : "T-mas";
                public override string SongAuthor => "BeatMario";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 1.5f),
                            new(Difficulty.Easy, 7.5f),
                            new(Difficulty.Hard, 15.6f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 2.0f),
                            new(Difficulty.Easy, 8.5f),
                            new(Difficulty.Hard, 16.4f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 8.5f),
                            new(Difficulty.Easy, 12.5f),
                            new(Difficulty.Hard, 19.8f),
                        }
                    );
                public override HashSet<Difficulty> UnlockedDifficulties
                {
                    get
                    {
                        HashSet<Difficulty> difficulties = new();
                        for (int i = 0; i <= 3; i++) difficulties.Add((Difficulty)i);
                        return difficulties;
                    }
                }
            }
            public SongInformation Attributes => new ThisInformation();

            private static class NoobBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(0);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(0);
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$0", "/", "/", "/", "/", "/", "/","/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                            "+0", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int T = 0; T < 1; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(96);
                    string[] bway = {
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "+0", "/", "+0", "/", "+0", "/",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "/", "+0", "/", "/", "+0", "/",
                            "R", "/", "/", "+0", "/", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "$0", "/", "/", "/", "+0", "/", "/", "/",
                            "$2", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "/", "+0", "/", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(0);
                    }
                    if (game.InBeat(start, start + 116))
                        if (game.At0thBeat(16))
                        {
                            CreateEntity(new NormalGB(new(Rand(200, 440), 100), new(320, 0), new(1, 1f), 90, game.BeatTime(16), game.BeatTime(1)));
                        }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        float curt = Gametime - game.BeatTime(start + 64);
                        InstantSetBox(260 + Sin01(0.5f + curt / game.BeatTime(32)) * 60, 240, 105);
                    }
                }
                public static void Area2B(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(4);
                    }
                    if (game.InBeat(start, start + 108))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(Sounds.pierce);
                        }
                        if (game.AtKthBeat(16, game.BeatTime(0)))
                        {
                            CreateBone(new CustomBone(new(200, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 12)
                            {
                                PositionRouteParam = new float[] { 2, 0 }
                            });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateBone(new CustomBone(new(200, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 12)
                            {
                                PositionRouteParam = new float[] { 2, 0 }
                            });
                        }
                    }
                }
                public static void Area2C(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 220, 155);
                        SetSoul(2);
                        Heart.GiveForce(0, 12);
                    }
                    if (game.InBeat(start + 64))
                    {
                        Heart.GiveForce(180, 12);
                    }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 4, 4));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(8)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 4, 4));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(24)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 3.9f, 116) { ColorType = 1 });
                        }

                    }
                    if (game.InBeat(start, start + 63))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 4, 3));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(8)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 4, 3));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(24)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 3.9f, 116) { ColorType = 1 });
                        }

                    }
                }
                public static void Area3A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(310, 300, 120);
                        SetSoul(4);
                        Heart.RotateTo(0);
                    }
                    if (game.InBeat(start, start + 112) && game.At0thBeat(16))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(false, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(false, 2f, 40) { ColorType = 0 });
                        AddInstance(new InstantEvent(game.BeatTime(4), () =>
                        {
                            PlaySound(Sounds.Ding);
                            bone1.ColorType = 1;
                            bone2.ColorType = 1;
                            AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                            {
                                bone1.ColorType = 0;
                                bone2.ColorType = 0;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                            {
                                int s = 1;
                                bone1.MissionLength -= 30 * s;
                                bone2.MissionLength += 30 * s;
                            }));
                        }));
                    }
                    if (game.InBeat(start, start + 112) && game.AtKthBeat(16, game.BeatTime(8)))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(true, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(true, 2f, 40) { ColorType = 0 });
                    }
                }

                public static void Area4A()
                {
                    float curTime = game.BeatTime(32);
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "/", "/", "/", "+0", "+0", "+0", "/",
                            "$2", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$0", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$2", "/", "+0", "/", "+0", "/", "+0", "/",
                            "$2", "/", "/", "/", "+0", "+0", "+0", "/",
                            "$0", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$2", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 1, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "/", "/", "/", "+0", "+0", "+0", "/",
                            "$2", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$0", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$2", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "/", "+0", "/", "/", "+0", "/",
                            "R", "/", "/", "+0", "/", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "/", "/", "/", "+0", "+0", "+0", "/",
                            "$2", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$0", "/", "/", "/", "+0", "+0", "+0", "+0",
                            "$2", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "/", "+0", "/", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
            }
            private static class EasyBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(0);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(0);
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "D", "/", "R", "/", "+0", "+0", "/", "R",
                            "/", "+0", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "+0", "+0", "/", "R",
                            "/", "+0", "/", "R", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$0", "/", "/", "/", "/", "/", "/","/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                            "+2", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(96);
                    string[] bway = {
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "+0", "+0", "+0", "+0",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "+0", "+0", "+0", "+0",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "+0", "+0", "+0", "+0",
                            "D", "/", "+0", "/", "+0", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "/", "/", "$0", "+0", "+0", "/",
                            "/", "/", "/", "/", "$0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "$0", "+0", "+0", "+0",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                            "/", "/", "/", "/", "$0", "+0", "+0", "/",
                            "/", "/", "/", "/", "$0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "$0", "+0", "+0", "+0",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "+0", "+0", "/",
                            "/", "/", "/", "/", "$2", "+0", "+0", "+0",
                            "/", "/", "/", "/", "$2", "+0", "+0", "+0",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "N3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "+0", "/", "+0", "/", "N3", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "+0", "+0", "/",
                            "/", "/", "/", "/", "$2", "+0", "+0", "+0",
                            "/", "/", "/", "/", "$2", "+0", "+0", "+0",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(0);
                        CreateEntity(new Boneslab(0, 12, (int)game.BeatTime(4), (int)game.BeatTime(128)));
                        CreateEntity(new Boneslab(180, 12, (int)game.BeatTime(4), (int)game.BeatTime(128)));
                    }
                    if (game.InBeat(start, start + 116))
                        if (game.At0thBeat(4))
                        {
                            CreateEntity(new NormalGB(new(Rand(200, 440), 100), new(320, 0), new(1, 0.5f), 90, game.BeatTime(16), game.BeatTime(1)));
                        }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        float curt = Gametime - game.BeatTime(start + 64);
                        InstantSetBox(260 + Sin01(0.5f + curt / game.BeatTime(32)) * 60, 240, 105);
                    }
                }
                public static void Area2B(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(4);
                    }
                    if (game.InBeat(start, start + 108))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(Sounds.pierce);
                        }
                        if (game.AtKthBeat(16, game.BeatTime(0)))
                        {
                            CreateBone(new CustomBone(new(200, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 20)
                            {
                                PositionRouteParam = new float[] { 2, 0 }
                            });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateBone(new CustomBone(new(440, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 20)
                            {
                                PositionRouteParam = new float[] { -2, 0 }
                            });
                        }
                    }
                }
                public static void Area2C(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 220, 155);
                        SetSoul(2);
                        Heart.GiveForce(0, 12);
                    }
                    if (game.InBeat(start + 64))
                    {
                        Heart.GiveForce(180, 12);
                    }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 4, 14));
                            CreateBone(new DownBone(true, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(8)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 4, 14));
                            CreateBone(new DownBone(false, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(24)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 3.9f, 116) { ColorType = 1 });
                        }

                    }
                    if (game.InBeat(start, start + 63))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 4, 13));
                            CreateBone(new UpBone(true, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(8)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 4, 13));
                            CreateBone(new UpBone(false, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(24)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 3.9f, 116) { ColorType = 1 });
                        }

                    }
                }
                public static void Area3A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(310, 300, 120);
                        SetSoul(4);
                        Heart.RotateTo(0);
                    }
                    if (game.InBeat(start, start + 112) && game.At0thBeat(16))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(false, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(false, 2f, 40) { ColorType = 0 });
                        AddInstance(new InstantEvent(game.BeatTime(4), () =>
                        {
                            PlaySound(Sounds.Ding);
                            bone1.ColorType = 1;
                            bone2.ColorType = 1;
                            AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                            {
                                bone1.ColorType = 0;
                                bone2.ColorType = 0;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                            {
                                int s = 1;
                                bone1.MissionLength -= 30 * s;
                                bone2.MissionLength += 30 * s;
                            }));
                        }));
                    }
                    if (game.InBeat(start, start + 112) && game.AtKthBeat(16, game.BeatTime(8)))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(true, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(true, 2f, 40) { ColorType = 0 });
                        AddInstance(new InstantEvent(game.BeatTime(4), () =>
                        {
                            PlaySound(Sounds.Ding);
                            bone1.ColorType = 1;
                            bone2.ColorType = 1;
                            AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                            {
                                bone1.ColorType = 0;
                                bone2.ColorType = 0;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                            {
                                int s = 1;
                                bone1.MissionLength += 30 * s;
                                bone2.MissionLength -= 30 * s;
                            }));
                        }));
                    }
                }
                public static void Area4A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$1", "/", "/", "/", "$1", "/", "+0", "/",
                            "/", "/", "$3", "/", "/", "$3", "/", "+0",
                            "$1", "/", "/", "/", "$1", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "/", "/", "$1", "/", "+0", "/",
                            "/", "/", "$3", "/", "/", "$3", "/", "+0",
                            "$1", "/", "/", "/", "$1", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "$1", "/", "/", "$1", "/", "/",
                            "$3", "/", "/", "/", "$3", "/", "+0", "/",
                            "/", "/", "$1", "/", "/", "$1", "/", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "/", "/", "$1", "/", "/", "$1", "/", "/",
                            "$3", "/", "/", "/", "$3", "/", "+0", "/",
                            "/", "/", "$1", "/", "/", "$1", "/", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "$2", "/", "/", "$2", "/", "/",
                            "$0", "/", "/", "/", "$0", "/", "+0", "/",
                            "/", "/", "$2", "/", "/", "$2", "/", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "$2", "/", "/", "/", "$2", "/", "+0", "/",
                            "/", "/", "$0", "/", "/", "$0", "/", "+0",
                            "$2", "/", "/", "/", "$2", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "$2", "/", "/", "$2", "/", "/",
                            "$0", "/", "/", "/", "$0", "/", "+0", "/",
                            "/", "/", "$2", "/", "/", "$2", "/", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "$2", "/", "/", "/", "$2", "/", "+0", "/",
                            "/", "/", "$0", "/", "/", "$0", "/", "+0",
                            "$2", "/", "/", "/", "$2", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
            }
            private static class HardBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(0);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(0);
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "/", "/", "+1", "/", "/","/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(96);
                    string[] bway = {
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "/", "/", "$0", "+2", "+2", "/",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                            "/", "/", "/", "/", "$0", "+2", "+2", "/",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "+2", "+2", "/",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "+2", "+2", "/",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(3);
                        CreateEntity(new Boneslab(0, 12, (int)game.BeatTime(4), (int)game.BeatTime(128)));
                        CreateEntity(new Boneslab(180, 12, (int)game.BeatTime(4), (int)game.BeatTime(128)));
                    }
                    if (game.InBeat(start, start + 116))
                        if (game.At0thBeat(4))
                        {
                            CreateEntity(new NormalGB(new(Rand(200, 440), 100), new(320, 0), new(1, 0.5f), 90, game.BeatTime(16), game.BeatTime(1)));
                        }
                    if (game.InBeat(start, start + 54))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(Sounds.pierce);
                        }
                        if (game.At0thBeat(16))
                        {
                            CreateBone(new DownBone(false, 3, 50) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateBone(new UpBone(false, 3, 50) { ColorType = 1 });
                        }
                    }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        float curt = Gametime - game.BeatTime(start + 64);
                        InstantSetBox(260 + Sin01(0.5f + curt / game.BeatTime(16)) * 60, 240, 105);
                    }
                }
                public static void Area2B(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(4);
                    }
                    if (game.InBeat(start, start + 112))
                    {
                        if (game.At0thBeat(4))
                        {
                            PlaySound(Sounds.pierce);
                        }
                        if (game.At0thBeat(16))
                        {
                            CreateBone(new DownBone(false, 3, 50) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateBone(new UpBone(false, 3, 50) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(4)))
                        {
                            CreateBone(new CustomBone(new(200, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 20)
                            {
                                PositionRouteParam = new float[] { 2, 0 }
                            });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(12)))
                        {
                            CreateBone(new CustomBone(new(440, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 20)
                            {
                                PositionRouteParam = new float[] { -2, 0 }
                            });
                        }
                    }
                }
                public static void Area2C(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 220, 155);
                        SetSoul(2);
                        Heart.GiveForce(0, 12);
                    }
                    if (game.InBeat(start + 64))
                    {
                        Heart.GiveForce(180, 12);
                    }
                    if (game.InBeat(start + 72, start + 127))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 4, 14));
                            CreateBone(new DownBone(true, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(4)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 4, 14));
                            CreateBone(new DownBone(false, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(20)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 3.9f, 116) { ColorType = 1 });
                        }

                    }
                    if (game.InBeat(start, start + 63))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 4, 13));
                            CreateBone(new UpBone(true, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(4)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 4, 13));
                            CreateBone(new UpBone(false, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(20)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 3.9f, 116) { ColorType = 1 });
                        }

                    }
                }
                public static void Area3A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(310, 300, 120);
                        SetSoul(4);
                        Heart.RotateTo(0);
                    }
                    if (game.InBeat(start, start + 112) && game.At0thBeat(16))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(false, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(false, 2f, 40) { ColorType = 0 });
                        if (RandBool())
                            AddInstance(new InstantEvent(game.BeatTime(4), () =>
                            {
                                PlaySound(Sounds.Ding);
                                bone1.ColorType = 1;
                                bone2.ColorType = 1;
                                AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                                {
                                    bone1.ColorType = 0;
                                    bone2.ColorType = 0;
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                                {
                                    int s = RandSignal();
                                    bone1.MissionLength -= 30 * s;
                                    bone2.MissionLength += 30 * s;
                                }));
                            }));
                    }
                    if (game.InBeat(start, start + 112) && game.AtKthBeat(16, game.BeatTime(8)))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(true, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(true, 2f, 40) { ColorType = 0 });
                        if (RandBool())
                            AddInstance(new InstantEvent(game.BeatTime(4), () =>
                            {
                                PlaySound(Sounds.Ding);
                                bone1.ColorType = 1;
                                bone2.ColorType = 1;
                                AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                                {
                                    bone1.ColorType = 0;
                                    bone2.ColorType = 0;
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                                {
                                    int s = RandSignal();
                                    bone1.MissionLength += 30 * s;
                                    bone2.MissionLength -= 30 * s;
                                }));
                            }));
                    }
                }
                public static void Area4A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
            }
            private static class ExBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(0);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "$2", "/", "$0", "$2", "$0", "$2", "$0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(0);
                    string[] bway = {
                            "$0", "/", "$0", "/", "$2", "$2", "/", "$0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                            "/", "/", "/", "/", "$2", "/", "/", "/",
                            "/", "/", "/", "/", "$0", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                            "D", "/", "R", "/", "R", "D", "/", "R",
                            "/", "R", "/", "R", "+0", "D", "+0", "D",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "/", "/", "+1", "/", "/","/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(96);
                    string[] bway = {
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                            "D", "/", "R", "/", "R", "+0", "/", "R",
                            "/", "R", "/", "R", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "D", "/", "D", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "D", "/", "D", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "/", "/", "$0", "+2", "+2", "/",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "$0", "/", "D", "/", "D", "/", "D", "/",
                            "/", "/", "/", "/", "$0", "+2", "+2", "/",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$0", "+2", "+2", "+2",
                            "$0", "/", "D", "/", "D", "/", "D", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "D", "/", "D", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "+2", "+2", "/",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "$2", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "$1", "/", "+0", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "D", "/", "D", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "$2", "+2", "+2", "/",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "$2", "+2", "+2", "+2",
                            "$2", "/", "/", "/", "D", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(3);
                        CreateEntity(new Boneslab(0, 24, (int)game.BeatTime(4), (int)game.BeatTime(128)));
                        CreateEntity(new Boneslab(180, 24, (int)game.BeatTime(4), (int)game.BeatTime(128)));
                    }
                    if (game.InBeat(start, start + 116))
                        if (game.At0thBeat(4))
                        {
                            CreateEntity(new NormalGB(new(Rand(200, 440), 100), new(320, 0), new(1, 0.5f), 90, game.BeatTime(16), game.BeatTime(1)));
                        }
                    if (game.InBeat(start, start + 54))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(Sounds.pierce);
                        }
                        if (game.At0thBeat(16))
                        {
                            CreateBone(new DownBone(false, 3, 50));
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateBone(new UpBone(false, 3, 50));
                        }
                    }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        float curt = Gametime - game.BeatTime(start + 64);
                        InstantSetBox(260 + Sin01(0.5f + curt / game.BeatTime(16)) * 60, 240, 105);
                    }
                }
                public static void Area2B(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 240, 105);
                        SetSoul(4);
                    }
                    if (game.InBeat(start, start + 112))
                    {
                        if (game.At0thBeat(4))
                        {
                            PlaySound(Sounds.pierce);
                        }
                        if (game.At0thBeat(16))
                        {
                            CreateBone(new DownBone(false, 3, 50));
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateBone(new UpBone(false, 3, 50));
                        }
                        if (game.AtKthBeat(16, game.BeatTime(4)) || game.AtKthBeat(16, game.BeatTime(12)))
                        {
                            CreateBone(new CustomBone(new(200, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 20)
                            {
                                PositionRouteParam = new float[] { 3, 0 }
                            });
                            CreateBone(new CustomBone(new(440, 320 + 105 / 4f * Rand(-1, 1)), Motions.PositionRoute.linear, 0, 20)
                            {
                                PositionRouteParam = new float[] { -3, 0 }
                            });
                        }
                    }
                }
                public static void Area2C(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(320, 220, 155);
                        SetSoul(2);
                        Heart.GiveForce(0, 12);
                    }
                    if (game.InBeat(start + 64))
                    {
                        Heart.GiveForce(180, 12);
                    }
                    if (game.InBeat(start + 64, start + 127))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 4, 14));
                            CreateBone(new DownBone(true, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(4)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(2)))
                        {
                            CreateBone(new UpBone(true, 3.9f, 116) { ColorType = 2 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 4, 14));
                            CreateBone(new DownBone(false, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(20)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new UpBone(false, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(18)))
                        {
                            CreateBone(new UpBone(false, 3.9f, 116) { ColorType = 2 });
                        }

                    }
                    if (game.InBeat(start, start + 63))
                    {
                        if (game.At0thBeat(32))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 4, 13));
                            CreateBone(new UpBone(true, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(4)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(true, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(2)))
                        {
                            CreateBone(new DownBone(true, 3.9f, 116) { ColorType = 2 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(16)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 4, 13));
                            CreateBone(new UpBone(false, 4, 100));
                        }
                        if (game.AtKthBeat(32, game.BeatTime(20)))
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new DownBone(false, 3.9f, 116) { ColorType = 1 });
                        }
                        if (game.AtKthBeat(32, game.BeatTime(18)))
                        {
                            CreateBone(new DownBone(false, 3.9f, 116) { ColorType = 2 });
                        }
                    }
                }
                public static void Area3A(float start)
                {
                    if (game.InBeat(start))
                    {
                        SetBox(310, 300, 120);
                        SetSoul(4);
                        Heart.RotateTo(0);
                    }
                    if (game.InBeat(start, start + 112) && game.At0thBeat(16))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(false, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(false, 2f, 40) { ColorType = 0 });
                        AddInstance(new InstantEvent(game.BeatTime(4), () =>
                        {
                            PlaySound(Sounds.Ding);
                            bone1.ColorType = 1;
                            bone2.ColorType = 1;
                            AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                            {
                                bone1.ColorType = 0;
                                bone2.ColorType = 0;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                            {
                                int s = RandBool() ? RandSignal() : 0;
                                bone1.MissionLength -= 30 * s;
                                bone2.MissionLength += 30 * s;
                            }));
                        }));
                    }
                    if (game.InBeat(start, start + 112) && game.AtKthBeat(16, game.BeatTime(8)))
                    {
                        PlaySound(Sounds.pierce);
                        DownBone bone1;
                        UpBone bone2;
                        CreateBone(bone1 = new DownBone(true, 2f, 40) { ColorType = 0 });
                        CreateBone(bone2 = new UpBone(true, 2f, 40) { ColorType = 0 });
                        if (RandBool())
                            AddInstance(new InstantEvent(game.BeatTime(4), () =>
                            {
                                PlaySound(Sounds.Ding);
                                bone1.ColorType = 1;
                                bone2.ColorType = 1;
                                AddInstance(new InstantEvent(game.BeatTime(1f), () =>
                                {
                                    bone1.ColorType = 0;
                                    bone2.ColorType = 0;
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(4f), () =>
                                {
                                    int s = RandSignal();
                                    bone1.MissionLength += 30 * s;
                                    bone2.MissionLength -= 30 * s;
                                }));
                            }));
                    }
                }
                public static void Area4A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, bway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area4C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$1", "/", "+2", "/", "+2", "+0", "+0", "+0",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "$3", "/", "+0", "/", "+0", "/", "+0", "/",
                            "+0", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "$0", "/", "+2", "/", "+2", "+2", "+2", "+2",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "N3", "/",
                            "$3", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, rway[i] == "+2" ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
            }

            #region Non-ChampionShip
            public void Normal()
            {
                throw new System.NotImplementedException();
            }
            public void Extreme()
            {
                Effect();
                if (Gametime < 0) return;

                if (InBeat(0)) ExBarrage.Intro0();
                if (InBeat(32)) ExBarrage.Intro1();
                if (InBeat(96)) ExBarrage.Intro2();
                if (InBeat(224)) ExBarrage.Area1A();
                if (InBeat(288)) ExBarrage.Area1B();
                if (InBeat(352)) ExBarrage.Area1A();
                if (InBeat(416)) ExBarrage.Area1C();
                if (InBeat(512, 512 + 129)) ExBarrage.Area2A(512);
                if (InBeat(640, 640 + 129)) ExBarrage.Area2B(640);
                if (InBeat(768, 768 + 129)) ExBarrage.Area2C(768);
                if (InBeat(896, 896 + 129)) ExBarrage.Area3A(896);
                if (InBeat(1021))
                {
                    SetSoul(1);
                    TP(); SetGreenBox();
                }
                if (InBeat(1024 - 32)) ExBarrage.Area1A();
                if (InBeat(1088 - 32)) ExBarrage.Area1B();
                if (InBeat(1152 - 32)) ExBarrage.Area1A();
                if (InBeat(1216 - 32)) ExBarrage.Area1C();
                if (InBeat(1280 - 32)) ExBarrage.Intro2();
                if (InBeat(1408 - 32)) ExBarrage.Intro2();
                for (int i = 0; i <= 1; i++)
                {
                    if (InBeat(1536 + i * 128 - 32)) ExBarrage.Area1A();
                    if (InBeat(1600 + i * 128 - 32)) ExBarrage.Area1B();
                }
                if (InBeat(1792 - 32)) ExBarrage.Area4A();
                if (InBeat(1856 - 32)) ExBarrage.Area4B();
                if (InBeat(1920 - 32)) ExBarrage.Area4A();
                if (InBeat(1984 - 32)) ExBarrage.Area4C();
            }
            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }
            #endregion

            public void Noob()
            {
                if (Gametime < 0) return;

                if (InBeat(0)) NoobBarrage.Intro0();
                if (InBeat(32)) NoobBarrage.Intro1();
                if (InBeat(96)) NoobBarrage.Intro2();
                if (InBeat(224)) NoobBarrage.Area1A();
                if (InBeat(288)) NoobBarrage.Area1B();
                if (InBeat(352)) NoobBarrage.Area1A();
                if (InBeat(416)) NoobBarrage.Area1C();
                if (InBeat(512, 512 + 129)) NoobBarrage.Area2A(512);
                if (InBeat(640, 640 + 129)) NoobBarrage.Area2B(640);
                if (InBeat(768, 768 + 129)) NoobBarrage.Area2C(768);
                if (InBeat(896, 896 + 129)) NoobBarrage.Area3A(896);
                if (InBeat(1024 - 3))
                {
                    SetSoul(1);
                    TP(); SetGreenBox();
                }
                if (InBeat(1024 - 32)) NoobBarrage.Area1A();
                if (InBeat(1088 - 32)) NoobBarrage.Area1B();
                if (InBeat(1152 - 32)) NoobBarrage.Area1A();
                if (InBeat(1216 - 32)) NoobBarrage.Area1C();
                if (InBeat(1280 - 32)) NoobBarrage.Intro2();
                if (InBeat(1408 - 32)) NoobBarrage.Intro2();
                for (int i = 0; i <= 1; i++)
                {
                    if (InBeat(1536 + i * 128 - 32)) NoobBarrage.Area1A();
                    if (InBeat(1600 + i * 128 - 32)) NoobBarrage.Area1B();
                }
                if (InBeat(1792 - 32)) NoobBarrage.Area4A();
                if (InBeat(1856 - 32)) NoobBarrage.Area4B();
                if (InBeat(1920 - 32)) NoobBarrage.Area4A();
                if (InBeat(1984 - 32)) NoobBarrage.Area4C();
            }

            public void Easy()
            {
                if (Gametime < 0) return;

                if (InBeat(0)) EasyBarrage.Intro0();
                if (InBeat(32)) EasyBarrage.Intro1();
                if (InBeat(96)) EasyBarrage.Intro2();
                if (InBeat(224)) EasyBarrage.Area1A();
                if (InBeat(288)) EasyBarrage.Area1B();
                if (InBeat(352)) EasyBarrage.Area1A();
                if (InBeat(416)) EasyBarrage.Area1C();
                if (InBeat(512, 512 + 129)) EasyBarrage.Area2A(512);
                if (InBeat(640, 640 + 129)) EasyBarrage.Area2B(640);
                if (InBeat(768, 768 + 129)) EasyBarrage.Area2C(768);
                if (InBeat(896, 896 + 129)) EasyBarrage.Area3A(896);
                if (InBeat(1024 - 3))
                {
                    SetSoul(1);
                    TP(); SetGreenBox();
                }
                if (InBeat(1024 - 32)) EasyBarrage.Area1A();
                if (InBeat(1088 - 32)) EasyBarrage.Area1B();
                if (InBeat(1152 - 32)) EasyBarrage.Area1A();
                if (InBeat(1216 - 32)) EasyBarrage.Area1C();
                if (InBeat(1280 - 32)) EasyBarrage.Intro2();
                if (InBeat(1408 - 32)) EasyBarrage.Intro2();
                for (int i = 0; i <= 1; i++)
                {
                    if (InBeat(1536 + i * 128 - 32)) EasyBarrage.Area1A();
                    if (InBeat(1600 + i * 128 - 32)) EasyBarrage.Area1B();
                }
                if (InBeat(1792 - 32)) EasyBarrage.Area4A();
                if (InBeat(1856 - 32)) EasyBarrage.Area4B();
                if (InBeat(1920 - 32)) EasyBarrage.Area4A();
                if (InBeat(1984 - 32)) EasyBarrage.Area4C();
            }

            public void Hard()
            {
                if (Gametime < 0) return;

                if (InBeat(0)) HardBarrage.Intro0();
                if (InBeat(32)) HardBarrage.Intro1();
                if (InBeat(96)) HardBarrage.Intro2();
                if (InBeat(224)) HardBarrage.Area1A();
                if (InBeat(288)) HardBarrage.Area1B();
                if (InBeat(352)) HardBarrage.Area1A();
                if (InBeat(416)) HardBarrage.Area1C();
                if (InBeat(512, 512 + 129)) HardBarrage.Area2A(512);
                if (InBeat(640, 640 + 129)) HardBarrage.Area2B(640);
                if (InBeat(768, 768 + 129)) HardBarrage.Area2C(768);
                if (InBeat(896, 896 + 129)) HardBarrage.Area3A(896);
                if (InBeat(1024 - 3))
                {
                    SetSoul(1);
                    TP(); SetGreenBox();
                }
                if (InBeat(1024 - 32)) HardBarrage.Area1A();
                if (InBeat(1088 - 32)) HardBarrage.Area1B();
                if (InBeat(1152 - 32)) HardBarrage.Area1A();
                if (InBeat(1216 - 32)) HardBarrage.Area1C();
                if (InBeat(1280 - 32)) HardBarrage.Intro2();
                if (InBeat(1408 - 32)) HardBarrage.Intro2();
                for (int i = 0; i <= 1; i++)
                {
                    if (InBeat(1536 + i * 128 - 32)) HardBarrage.Area1A();
                    if (InBeat(1600 + i * 128 - 32)) HardBarrage.Area1B();
                }
                if (InBeat(1792 - 32)) HardBarrage.Area4A();
                if (InBeat(1856 - 32)) HardBarrage.Area4B();
                if (InBeat(1920 - 32)) HardBarrage.Area4A();
                if (InBeat(1984 - 32)) HardBarrage.Area4C();
            }


            private void Effect()
            {
                if (
                    InBeat(224 + 56) || InBeat(224 + 89) ||
                    InBeat(288 + 56) ||
                    InBeat(352 + 56) || InBeat(352 + 89) || InBeat(352 + 89 + 32) ||
                    InBeat(1024 - 32 + 56) || InBeat(1024 - 32 + 89) ||
                    InBeat(1088 - 32 + 56) ||
                    InBeat(1152 - 32 + 56) || InBeat(1152 - 32 + 89) || InBeat(1152 + 89) ||
                    InBeat(1536 - 32 + 56) || InBeat(1536 - 32 + 89) ||
                    InBeat(1600 - 32 + 56) ||
                    InBeat(1536 + 128 - 32 + 56) || InBeat(1536 + 128 - 32 + 89) || InBeat(1536 + 128 + 89) ||
                    InBeat(1792 - 32 + 56) || InBeat(1792 - 32 + 89) ||
                    InBeat(1856 - 32 + 56) ||
                    InBeat(1920 - 32 + 56) || InBeat(1920 - 32 + 89)
                    )
                {
                    DelayBeat(0, () => { ScreenDrawing.ScreenAngle = 25; });
                    DelayBeat(2, () => { ScreenDrawing.ScreenAngle = -25; });
                    DelayBeat(4, () => { ScreenDrawing.ScreenAngle = 25; });
                    DelayBeat(6, () => { ScreenDrawing.ScreenAngle = -25; });
                    DelayBeat(8, () => { ScreenDrawing.ScreenAngle = 0; });
                }
                if (
                    InBeat(288 + 88) ||
                    InBeat(1088 - 32 + 89) ||
                    InBeat(1600 - 32 + 89) ||
                    InBeat(1600 + 128 - 32 + 89) ||
                    InBeat(1856 - 32 + 88)
                    )
                {
                    DrawingUtil.ScreenAngle(360, BeatTime(6));
                }
                if (InBeat(352 + 89 + 54) || InBeat(1152 - 32 + 89 + 55))
                {
                    DrawingUtil.ScreenAngle(720, BeatTime(12));
                }
                if (InBeat(1984 - 32 + 80))
                    DrawingUtil.ScreenAngle(2520, BeatTime(42));
                if (
                    InBeat(288 + 89 + 6) ||
                    InBeat(353 + 89 + 54 + 12) ||
                    InBeat(1088 - 32 + 89 + 6) ||
                    InBeat(1152 - 32 + 89 + 54 + 12) ||
                    InBeat(1600 - 32 + 89 + 6) ||
                    InBeat(1600 + 128 - 32 + 89 + 6) ||
                    InBeat(1856 - 32 + 89 + 6) ||
                    InBeat(1984 - 32 + 80 + 42)
                    )
                {
                    ScreenDrawing.ScreenAngle = 0;
                }
                if (InBeat(1792))
                {
                    DrawingUtil.LerpScreenScale(BeatTime(240), 3, 0.0001f);
                }
                if (InBeat(1984 - 32 + 80))
                {
                    DrawingUtil.LerpScreenScale(BeatTime(6), 1, 0.16f);
                }
            }
            public void Start()
            {
                NoobBarrage.game = this;
                EasyBarrage.game = this;
                HardBarrage.game = this;
                ExBarrage.game = this;
                HeartAttribute.MaxHP = 8;
                HeartAttribute.Speed = 2.86f;
                HeartAttribute.SoftFalling = true;
                SetGreenBox();
                TP();
                SetSoul(1);
                GametimeDelta = -6.5f;
                //GametimeDetla = 4300;

                //GametimeDetla = this.BeatTime(1532);
                //GametimeDelta += BeatTime(1758);
                PlayOffset = GametimeDelta + 6.5f;
                // SetSoul(0); 
            }
        }
    }
}