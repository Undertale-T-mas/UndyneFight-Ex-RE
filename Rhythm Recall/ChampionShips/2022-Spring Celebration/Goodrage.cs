using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Goodrage : IChampionShip
    {
        public Goodrage()
        {
            Game.instance = new Game();

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Normal);
            difficulties.Add("div.1", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 13.0f),
                            new(Difficulty.ExtremePlus, 19.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.3f),
                            new(Difficulty.ExtremePlus, 20.7f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 21.5f),
                            new(Difficulty.ExtremePlus, 21.8f),
                        }
                    );
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "IceAgeDOT";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "EBIMAYO";
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(4.2235f) { }

            public static Game instance;

            public string Music => "GOODRAGE";
            //  public string Music => "Brain Power";
            public string FightName => "GOODRAGE";

            private static class NormalBarrage
            {
                public static Game game;
                private static Player.Heart playerIns2;

                public static void Intro0SE1()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(30), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(55), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(61), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(65), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(72), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(87), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(93), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(99), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(4));
                    }));
                    float[] beats1 =
                    {
                        game.BeatTime(102),
                        game.BeatTime(110),
                        game.BeatTime(118),
                        game.BeatTime(122),
                        game.BeatTime(126),
                        game.BeatTime(128),
                        game.BeatTime(130),
                        game.BeatTime(132),
                    };
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i] + game.BeatTime(2), () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                            ScreenDrawing.CameraEffect.Convulse(RandBool());
                        }));
                }
                public static void Intro0SE2()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(30), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(55), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(61), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(65), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(72), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(87), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(93), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(99), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(4));
                    }));
                    float[] beats1 =
                    {
                        game.BeatTime(102),
                        game.BeatTime(110),
                        game.BeatTime(118),
                        game.BeatTime(122),
                        game.BeatTime(126),
                        game.BeatTime(128),
                        game.BeatTime(130),
                    };
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i] + game.BeatTime(2), () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                            ScreenDrawing.CameraEffect.Convulse(RandBool());
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(127), () =>
                    {
                        TextAttribute A = new TextSpeedAttribute(45);
                        TextAttribute B = new TextMoveAttribute((s) =>
                        {
                            return s <= 24 ? new(0, 0) : new(-42 * MathF.Pow(s - 24, 0.9f), Sin((s - 24) * 8) * 55);
                        });
                        CreateEntity(new TextPrinter((int)game.BeatTime(12), "$$Whaaaat?!", new Vector2(100, 300), A, B));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(132), () =>
                    {
                        ScreenDrawing.ScreenAngle = 15;
                        for (int i = 0; i < 4; i++)
                            AddInstance(new InstantEvent(game.BeatTime(i), () =>
                            {
                                ScreenDrawing.CameraEffect.Convulse(game.BeatTime(4f / 8f), RandBool());
                            }));
                    }));
                }
                public static void Intro3SE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    float[] beats1 =
                    {
                        game.BeatTime(8),
                        game.BeatTime(12),
                        game.BeatTime(16),
                        game.BeatTime(20)
                    };
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i], () =>
                        {
                            ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(4);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(28), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(140 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(30), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(6);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(140 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    float[] beats2 =
                    {
                        game.BeatTime(40),
                        game.BeatTime(44),
                        game.BeatTime(48),
                        game.BeatTime(52)
                    };
                    for (int i = 0; i < beats2.Length; i++)
                        AddInstance(new InstantEvent(beats2[i], () =>
                        {
                            ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(4));
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(56), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(4);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(340 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(62), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(6);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(340 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(70), () =>
                    {
                        TP();
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(70), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(32));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(70), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(32));
                    }));
                    float beats3 = game.BeatTime(102);
                    for (int i = 0; i < 16; i++)
                        AddInstance(new InstantEvent(game.BeatTime(i) + beats3, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(RandBool());
                            ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                        }));
                }

                private class ScreenSplit : RenderProduction
                {
                    public int count;
                    public ScreenSplit(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Opaque, depth) { }
                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        MissionTarget = HelperTarget;
                        for (int i = 0; i < count; i++)
                            for (int j = 0; j < count; j++)
                                DrawTexture(obj, new CollideRect(AdaptedSize.X / count * i, AdaptedSize.Y / count * j, AdaptedSize.X / count, AdaptedSize.Y / count).ToRectangle());
                        return MissionTarget;
                    }
                }
                public static void Area1ASE()
                {
                    // PlaySound(Sounds.switchScene);
                    float[] beats1 =
                    {
                        game.BeatTime(12),
                        game.BeatTime(20),
                        game.BeatTime(28),
                    };
                    ScreenSplit SE = new ScreenSplit(0.4f);
                    SE.count = 1;
                    ScreenDrawing.SceneRendering.InsertProduction(SE);
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i], () =>
                        {
                            SE.count++;
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        SE.count = 1;
                        SE.Dispose();
                    }));
                }
                public static void GreenSoulBoxMove(int MoveType, Vector2 delta, float time)
                {
                    if (MoveType == 1) // f(x) = 2 * x - x * x
                    {
                        float t = 0;
                        Vector2 position = BoxStates.Centre;
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            SetBox(position + delta * f, 84, 84);
                            t++;
                        }));
                    }
                }

                public static void Area4ASE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(8));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        float t = 0;
                        float stx = 320;
                        float sty = 295;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx - 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(28), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty - 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx + 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty + 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        ScreenDrawing.CameraEffect.Convulse(20, game.BeatTime(2), false);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = 2 * x - x * x;
                            InstantSetBox(new Vector2(stx + 100 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(68), () =>
                    {
                        float stx = 420;
                        float sty = 295;
                        ScreenDrawing.CameraEffect.Convulse(20, game.BeatTime(2), true);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = 2 * x - x * x;
                            InstantSetBox(new Vector2(stx - 100 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                }
                public static void Area4BSE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(8));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        float t = 0;
                        float stx = 320;
                        float sty = 295;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx - 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(28), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty - 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx + 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty + 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(56), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(68), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                    }));
                }
                public static void Area5ASE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        float x = 320, y = 240, t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(253), () =>
                        {
                            y = 15 * Sin(4 * 180 * t / (game.BeatTime(96) - 1)) + 240;
                            x = 32 * Sin(180 * t / (game.BeatTime(128) - 1)) + 320;
                            InstantSetBox(new Vector2(x, y), BoxStates.Width, BoxStates.Height);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                        for (int i = 0; i < 7; i++)
                            AddInstance(new InstantEvent(game.BeatTime(8 + 256 - 24 + 2 * i), () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.White * 0.4f);
                                ScreenDrawing.CameraEffect.Convulse(RandBool());
                            }));
                    }));
                }

                public static void Intro0()
                {
                    string[] ways = {
                        "R(R1)", "/", "/", "/", "D", "/", "/", "/",
                        "R", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                        "R1", "/", "/", "/", "/", "/","R1", "/",
                         "/", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)", "/", "/", "/", "D", "/", "/", "/",
                        "R", "+0", "+0", "+0", "+201", "+0", "+0", "+0",
                        "R(D1)", "/", "/", "/", "/", "/","R", "/",
                         "/", "/", "/", "/", "/", "/", "/", "/",
                    };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    string[] ways = {
                        "R(R1)", "/", "/", "/", "D", "/", "/", "/",
                        "R", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                        "R1", "/", "/", "/", "/", "/","R1", "/",
                         "/", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)",  "/", "/", "/", "/", "/", "/", "/",
                        "R(D1)", "/", "/", "/", "R", "/", "/", "/",
                         "R", "/", "R", "/", "R", "/", "R", "/",
                    };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    string[] ways = {
                        "R(R1)", "/", "/", "/", "D", "/", "/", "/",
                        "R", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                        "R1", "/", "/", "/", "/", "/","R1", "/",
                         "/", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)",  "/", "/", "/", "/", "/", "/", "/",
                        "R(D1)", "/", "/", "/", "R", "/", "/", "/",
                         "R", "/", "R", "/", "R", "/", "/", "/",
                    };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Intro3()
                {
                    string[] ways = {
                         "$0($2)", "/", "/", "/", "$0($2)", "/", "/", "/",
                         "$0($2)", "/", "/", "/", "$0($2)", "/", "/", "/",
                         "$1($3)", "/", "/", "/", "$1($3)", "/", "/", "/",
                         "$1($3)", "/", "/", "/", "$1($3)", "/", "/", "/",
                         "$01($21)", "/", "/", "/", "$01($21)", "/", "/", "/",
                         "$01($21)", "/", "/", "/", "$01($21)", "/", "/", "/",
                         "$11($31)", "/", "/", "/", "$11($31)", "/", "/", "/",
                         "$11($31)", "/", "/", "/", "/", "/", "/", "/",
                    };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Intro4()
                {
                    string[] ways = {
                        "$0", "/", "$1", "/", "$2", "/", "$3", "/",
                        "$0", "/", "$1", "/", "$2", "/", "$3", "/",
                        "$0", "/", "$1", "/", "$2", "/", "$3", "/",
                        "$0", "/", "$1", "/", "$2", "/", "$3", "/",
                        "R(R1)", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)",  "/", "/", "/", "/", "/", "/", "/",
                    };

                    float time = game.BeatTime(16);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                    AddInstance(new InstantEvent(game.BeatTime(16 + 48), () =>
                    {
                        TextPrinter text = null;
                        AddInstance(new InstantEvent(game.BeatTime(6), () =>
                        {
                            ScreenDrawing.SceneOut(Color.White * 0.7f, game.BeatTime(5.5f));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(12), () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White);
                            SetSoul(0);
                            InstantSetBox(300, 125, 125);

                            Player.Heart p = Heart.Split();
                            playerIns2 = p;

                            SetPlayerMission(1);
                            InstantTP(320, -250);
                            InstantSetBox(-150, 84, 84);
                            SetSoul(1);
                        }));
                        TextAttribute A = new TextSpeedAttribute(45);
                        TextAttribute B = new TextMoveAttribute((s) =>
                        {
                            return s <= 50 ? new(0, 0) : new(-42 * MathF.Pow(s - 50, 0.9f), Sin((s - 50) * 8) * 55);
                        });
                        AddInstance(new InstantEvent(50, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(64, game.BeatTime(1.4f), false);
                            ScreenDrawing.CameraEffect.Convulse(64, game.BeatTime(0.7f), true);
                            AddInstance(new InstantEvent(50, () => { text.Dispose(); }));
                        }));
                        CreateEntity(text = new TextPrinter("$$Right   \nafter   \nthe   \nbreak", new Vector2(60, 150), A, B));
                    }));
                }

                public static void Area1A()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(4),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(90, 50, game.BeatTime(4), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 80, game.BeatTime(12), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(4);
                            HeartAttribute.PurpleLineCount = 2;
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            TP(320, 100);
                            SetBox(100, 84, 84);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(0);
                            Heart.RotateTo(0);
                            SetPlayerMission(0);
                            TP(320, 300);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(8),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);

                            float curTime = game.BeatTime(16);
                            string[] bway = {
                                "$2", "/", "/", "/", "$2", "/", "$2", "/",
                                "/", "/", "$0", "/", "$0", "/", "$0", "/",
                            };
                            for (int i = 0; i < bway.Length; i++)
                            {
                                if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                                else if (bway[i] == "H") { CreateArrow(curTime, 0, 6, 0, 0); CreateArrow(curTime, 2, 6, 0, 0); }
                                else if (bway[i] == "V") { CreateArrow(curTime, 1, 6, 0, 0); CreateArrow(curTime, 3, 6, 0, 0); }
                                else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                                curTime += game.BeatTime(1);
                            }

                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(4), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(14), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            SetBox(-100, 84, 84);
                            TP(320, -100);
                        }
                    ));
                }
                public static void Area1A0()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(4),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(270, 50, game.BeatTime(4), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 80, game.BeatTime(12), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(4);
                            HeartAttribute.PurpleLineCount = 2;
                            Heart.RotateTo(0);
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            TP(320, 100);
                            SetBox(100, 84, 84);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(0);
                            Heart.RotateTo(0);
                            TP(320, 300);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(8),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);

                            float curTime = game.BeatTime(16);
                            string[] bway = {
                                "$2", "/", "/", "/", "$2", "/", "$2", "/",
                                "/", "/", "$0", "/", "$0", "/", "/", "/",
                            };
                            for (int i = 0; i < bway.Length; i++)
                            {
                                if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                                else if (bway[i] == "H") { CreateArrow(curTime, 0, 6, 0, 0); CreateArrow(curTime, 2, 6, 0, 0); }
                                else if (bway[i] == "V") { CreateArrow(curTime, 1, 6, 0, 0); CreateArrow(curTime, 3, 6, 0, 0); }
                                else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                                curTime += game.BeatTime(1);
                            }

                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(4), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(14), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(16), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            SetBox(-100, 84, 84);
                            TP(320, -100);
                        }
                    ));
                }
                public static void Area1B()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(4),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(90, 50, game.BeatTime(4), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 80, game.BeatTime(12), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(4);
                            HeartAttribute.PurpleLineCount = 2;
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            TP(320, 100);
                            SetBox(100, 84, 84);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(2);
                            Heart.GiveForce(180, 10);
                            TP(320, 300);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(8),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);

                            float curTime = game.BeatTime(16);
                            string[] rway = {
                                "$0", "/", "$0", "$0", "$0", "/", "$0", "/",
                                "$2", "/", "$2", "/", "$2", "/", "/", "/",
                            };
                            for (int i = 0; i < rway.Length; i++)
                            {
                                if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                                else if (rway[i] == "H") { CreateArrow(curTime, 0, 6, 1, 0); CreateArrow(curTime, 2, 6, 1, 0); }
                                else if (rway[i] == "V") { CreateArrow(curTime, 1, 6, 1, 0); CreateArrow(curTime, 3, 6, 1, 0); }
                                else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                                curTime += game.BeatTime(1);
                            }
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(4), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(12), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            SetBox(-100, 84, 84);
                            TP(320, -100);
                        }
                    ));
                }
                public static void Area1C()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetBoxMission(0);
                            SetPlayerMission(0);
                            SetSoul(3);
                            SetBox(240, 125, 125);
                        }
                    ));
                    for (int x0 = 1; x0 <= 4; x0++)
                    {
                        int x = x0;
                        AddInstance(new InstantEvent(
                            game.BeatTime(8 * x),
                            () =>
                            {
                                SetBoxMission(0);
                                SetPlayerMission(0);
                                for (int i = 0; i <= 3; i++)
                                    CreateBone(new SideCircleBone(90 * i - 2 * game.BeatTime(8 * x), 4, 24 + 12 * x, game.BeatTime(36 - x * 8)));
                            }
                        ));
                    }
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(0);
                            SetPlayerMission(0);
                            SetSoul(0);
                            SetBox(300, 125, 125);
                        }
                    ));
                }

                public static void Area2A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(5.8f), () =>
                    {
                        SetPlayerMission(1);
                        SetSoul(0);
                        SetPlayerMission(0);
                        playerIns2.Merge(Heart);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(6.9f), () =>
                    {
                        SetBox(300, 250, 160);
                        SetSoul(2);
                        BoxStates.BoxMovingScale = 0.24f;
                        Heart.GiveForce(0, 12);
                        TP(320, 480);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(8f), () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.15f;
                        SetBox(300, 160, 160);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        CentreCircleBone a;
                        CreateBone(a = new CentreCircleBone(45, 180 / game.BeatTime(24), 240, game.BeatTime(124)) { IsMasked = true, ColorType = 0 });
                        AddInstance(new InstantEvent(game.BeatTime(64), () =>
                        {
                            a.RotateSpeed = -180 / game.BeatTime(16);
                            a.ColorType = 2;
                            PlaySound(Sounds.Ding);
                        }));
                    }));
                    for (int i = 2; i <= 8; i += 1)
                        AddInstance(new InstantEvent(game.BeatTime(8 * i), () =>
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
                            AddInstance(new TimeRangedEvent(game.BeatTime(6), 15, () =>
                            {
                                bones.ForEach(s => s.Speed += 0.4f);
                            })
                            { UpdateIn120 = true });
                            bones.ForEach(s => CreateBone(s));
                            PlaySound(Sounds.pierce);
                        }));
                    for (int i = 9; i <= 13; i++)
                        AddInstance(new InstantEvent(game.BeatTime(8 * i), () =>
                        {
                            CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)) - new Vector2(0, 50), Heart.Centre + GetVector2(120, Rand(0, 359)), new Vector2(1, 0.45f) * 0.95f, game.BeatTime(16), game.BeatTime(1)));
                        }));

                    float curSpeed = 0;
                    AddInstance(new TimeRangedEvent(game.BeatTime(126), game.BeatTime(10), () =>
                    {
                        curSpeed += 5f * 0.0072f;
                        InstantSetBox(BoxStates.Centre - new Vector2(0, curSpeed), BoxStates.Width, BoxStates.Height);
                    })
                    { UpdateIn120 = true }); ;
                    AddInstance(new TimeRangedEvent(game.BeatTime(136), game.BeatTime(8), () =>
                    {
                        curSpeed += 5f * 0.04f;
                        InstantSetBox(BoxStates.Centre - new Vector2(0, curSpeed), BoxStates.Width, BoxStates.Height);
                    })
                    { UpdateIn120 = true }); ;
                }

                public static void Area3A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(5 + 32), () =>
                    {
                        InstantSetBox(600, 84, 84);
                        InstantTP(BoxStates.Centre);
                        BoxStates.BoxMovingScale = 0.05f;
                        SetGreenBox();
                        SetSoul(1);
                        AddInstance(new TimeRangedEvent(0, 500, () =>
                        {
                            InstantTP(BoxStates.Centre);
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        float curTime = game.BeatTime(8);
                        string[] way = {
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "+0", "+0", "+0", "R", "/", "R", "/",
                            "/", "/", "R", "/", "D", "/", "D", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",

                            "D", "+0", "D", "+0", "D", "+0", "D", "+0",
                            "D", "+0", "D", "+0", "D", "+0", "D", "+0",
                            "D", "/", "/", "/", "R", "/", "D", "/",
                            "D", "/", "/", "/", "$1", "+2", "+2", "+2",
                        };
                        for (int i = 0; i < way.Length; i++)
                        {
                            if (way[i] != "/") game.CreateArrows(curTime, 7.5f, way[i]);
                            curTime += game.BeatTime(1);
                        }
                    }));
                }
                public static void Area3B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(0), () =>
                    {
                        float curTime = game.BeatTime(8);
                        string[] way = {
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "+0", "+0", "+0", "R", "/", "R", "/",
                            "/", "/", "R", "/", "D", "/", "D", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",

                            "R", "/", "R", "/", "R", "/", "D", "/",
                            "/", "/", "R", "/", "R", "/", "D", "/",
                            "/", "/", "R", "/", "R", "/", "D", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                        };
                        for (int i = 0; i < way.Length; i++)
                        {
                            if (way[i] != "/") game.CreateArrows(curTime, 7.5f, way[i]);
                            curTime += game.BeatTime(1);
                        }
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        float curTime = game.BeatTime(8);
                        string[] way = {
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "+0", "+0", "+0", "R", "/", "R", "/",
                            "/", "/", "R", "/", "D", "/", "D", "/",
                            "R", "/", "D", "/", "R", "/", "D", "/",

                            "D", "+0", "D", "+0", "D", "+0", "D", "+0",
                            "D", "+0", "D", "+0", "D", "+0", "D", "+0",
                            "D", "/", "/", "/", "R", "/", "D", "/",
                            "D", "/", "/", "/", "R(R1)", "/", "/", "/",
                        };
                        for (int i = 0; i < way.Length; i++)
                        {
                            if (way[i] != "/") game.CreateArrows(curTime, 7.5f, way[i]);
                            curTime += game.BeatTime(1);
                        }
                    }));
                }

                public static void Area4A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(4.5f), () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Lime * 0.2f);
                        ScreenDrawing.CameraEffect.SizeExpand(50, game.BeatTime(4f));
                        ScreenDrawing.CameraEffect.Convulse(30, game.BeatTime(4f), false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(7f), () =>
                    {
                        SetSoul(3);
                        SetBox(295, 150, 150);
                        BoxStates.BoxMovingScale = 0.2f;
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SideBone a;
                        PlaySound(Sounds.spearAppear);
                        CreateBone(a = new DownBone(false, 5, 71));
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                        {
                            a.Speed *= 0.81f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            a.Speed = 5.6f;
                            PlaySound(Sounds.pierce);
                        }));
                    }));
                    for (int x = 0; x < 7; x += 4)
                    {
                        int i = x;
                        AddInstance(new InstantEvent(game.BeatTime(24 + 2f * i), () =>
                        {
                            float dist = 75 + (i % 2 == 1 ? 33 : 0);
                            CreateBone(new CustomBone(new Vector2(320, 295) + GetVector2(dist, i * 45), (s) =>
                            {
                                return GetVector2(AdvanceFunctions.Sin01(s.AppearTime / game.BeatTime(8)) * dist * 1.1f, i * 45 + 180);
                            }, Motions.LengthRoute.autoFold, (s) => { return i * 45; })
                            { LengthRouteParam = new float[] { 140.0f + (i % 2 == 1 ? 33 : 0), game.BeatTime(4) } });
                            PlaySound(Sounds.pierce);
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            SideBone a;
                            PlaySound(Sounds.spearAppear);
                            CreateBone(a = new LeftBone(false, 5, 71));
                            AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                            {
                                a.Speed *= 0.81f;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                a.Speed = 5.6f;
                                PlaySound(Sounds.pierce);
                            }));
                        }));
                        for (int x = 0; x < 7; x += 2)
                        {
                            int i = x;
                            if (x == 2)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(16 + 2f * i), () =>
                                {
                                    for (int c = 0; c < 3; c++)
                                        CreateGB(new NormalGB(new(100, 295 - 75 + 60 * c), new(0, 480), new(1.0f, 0.5f), 0, game.BeatTime(8), game.BeatTime(1f)));
                                }));
                            }
                            else if (x == 6)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(16 + 2f * i), () =>
                                {
                                    for (int c = 0; c < 3; c++)
                                        CreateGB(new NormalGB(new(540, 295 - 45 + 60 * c), new(640, 480), new(1.0f, 0.5f), 180, game.BeatTime(8), game.BeatTime(1f)));
                                }));
                            }
                        }
                    }));
                }
                public static void Area4B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SideBone b;
                        PlaySound(Sounds.spearAppear);
                        CreateBone(b = new UpBone(false, 5, 71));
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                        {
                            b.Speed *= 0.81f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            b.Speed = 5.6f;
                            PlaySound(Sounds.pierce);
                        }));
                    }));
                    for (int x = 0; x < 7; x += 4)
                    {
                        int i = x;
                        AddInstance(new InstantEvent(game.BeatTime(24 + 2f * i), () =>
                        {
                            float dist = 75 + (i % 2 == 1 ? 33 : 0);
                            CreateBone(new CustomBone(new Vector2(320, 295) + GetVector2(dist, i * 45), (s) =>
                            {
                                return GetVector2(AdvanceFunctions.Sin01(s.AppearTime / game.BeatTime(8)) * dist * 1.1f, i * 45 + 180);
                            }, Motions.LengthRoute.autoFold, (s) => { return i * 45; })
                            { LengthRouteParam = new float[] { 140.0f + (i % 2 == 1 ? 33 : 0), game.BeatTime(4) } });
                            PlaySound(Sounds.pierce);
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            SideBone b;
                            PlaySound(Sounds.spearAppear);
                            CreateBone(b = new RightBone(false, 5, 71));
                            AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                            {
                                b.Speed *= 0.81f;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                b.Speed = 5.6f;
                                PlaySound(Sounds.pierce);
                            }));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(16), () =>
                        {
                            for (int x = 0; x < 4; x++) CreateEntity(new Boneslab(x * 90, 12, game.BeatTime(8), game.BeatTime(14)));
                            CreateGB(new NormalGB(new(320 - 75, 295 - 75), new(0, 0), Vector2.One, 45, game.BeatTime(8), game.BeatTime(1f)));
                            CreateGB(new NormalGB(new(320 + 75, 295 - 75), new(640, 0), Vector2.One, 135, game.BeatTime(8), game.BeatTime(1f)));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(24), () =>
                        {
                            for (int x = 0; x < 4; x++) CreateEntity(new Boneslab(x * 90, 42, game.BeatTime(8), game.BeatTime(6)) { ColorType = 2 });
                            CreateGB(new NormalGB(new(100, 295), new(0, 295), Vector2.One, 0, game.BeatTime(8), game.BeatTime(1f)));
                            CreateGB(new NormalGB(new(320, 100), new(320, 0), Vector2.One, 90, game.BeatTime(8), game.BeatTime(1f)));
                        }));
                    }));
                }
                public static void Area4C()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SideBone a;
                        PlaySound(Sounds.spearAppear);
                        CreateBone(a = new DownBone(true, 5, 71));
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                        {
                            a.Speed *= 0.81f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            a.Speed = 5.6f;
                            PlaySound(Sounds.pierce);
                        }));
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        SetBox(240, 150, 150);
                        playerIns2 = Heart.Split();
                        SetPlayerMission(1); SetBoxMission(1);
                        InstantTP(new(320, 580));
                        AddInstance(new InstantEvent(1, () =>
                        {
                            FightBox box = BoxStates.CurrentBox;
                            InstantSetBox(580, 84, 84);
                            SetSoul(1);
                            AddInstance(new TimeRangedEvent(game.BeatTime(128), () =>
                            {
                                SetPlayerMission(playerIns2);
                                SetBoxMission(box);
                                InstantTP(BoxStates.Centre);
                            })
                            { UpdateIn120 = true });
                            BoxStates.BoxMovingScale = 0.05f;
                            SetGreenBox();
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(34), () =>
                    {
                        SetPlayerMission(0); SetBoxMission(0);
                        FightBox box = BoxStates.CurrentBox;
                        float speed = 1.0f;
                        AddInstance(new TimeRangedEvent(game.BeatTime(32), () =>
                        {
                            SetBoxMission(box);
                            speed += 0.04f;
                            InstantSetBox(BoxStates.Centre.Y - speed, 150, 150);
                        })
                        { UpdateIn120 = true });
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(54), () =>
                    {
                        SetBoxMission(0); SetPlayerMission(0);
                        BoxStates.CurrentBox.Dispose();
                        Heart.Dispose();
                    }));
                }
                public static void Area4D()
                {
                    AddInstance(new InstantEvent(game.BeatTime(2), () =>
                    {
                        Regenerate(24);
                        PlaySound(Sounds.heal);
                        SetPlayerMission(playerIns2);
                        string[] ways = {
                            "$1", "", "", "", "$3", "", "", "",
                            "$101", "", "", "", "$301", "", "", "",
                            "$1", "", "", "", "$3", "", "", "",
                            "$101", "", "$101", "", "$301", "", "$301", "",
                        };

                        float time = game.BeatTime(6);
                        for (int i = 0; i < ways.Length; i++)
                        {
                            game.CreateArrows(time, 6.5f, ways[i]);
                            time += game.BeatTime(1);
                        }
                    }));
                }

                public static void Area5A()
                {
                    string[] ways = {
                            "(R1)", "", "$2", "", "(R1)", "", "$2(R1)", " ",
                            "(R1)", "", "$2(R1)", "", "(R1)", "", "$2(R1)", "",
                            "", "", "$2", "", "(R1)", "", "$2", "",
                            "(R1)", "", "$2(R1)", "", "(R1)", "", "$2(R1)", "",
                            "(R1)", "", "$2(R1)", "", "", "", "$2(R1)", "",
                            "", "", "$2(R1)", "", "(R1)", "", "$2(R1)", "",
                            "", "", "$2(R1)", "", "(R1)", "", "$2", "",
                            "", "", "$2(R1)", "", "(R1)", "", "$2", "",
                        };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]))
                        {
                            GameObject[] arrows = game.MakeArrows(time, 7.5f, ways[i]);
                            foreach (Arrow arr in arrows)
                            {
                                CreateEntity(arr);
                            }
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Area5B()
                {
                    string[] ways = {
                            "$0(R1)", "", "", "", "$0(R1)", "", "(R1)", " ",
                            "$0(R1)", "", "(R1)", "", "$0(R1)", "", "(R1)", "",
                            "$0", "", "", "", "$0(R1)", "", "", "",
                            "$0(R1)", "", "(R1)", "", "$0(R1)", "", "(R1)", "",
                            "R", "", "R", "", "R", "", "R", "",
                            "R", "", "R", "", "R", "", "R", "",
                            "$11", "(R)", "$11", "(R)", "$11", "(R)", "$11", "(R)",
                            "$31", "(R)", "$31", "(R)", "$31", "(R)", " ", "",
                        };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]))
                        {
                            GameObject[] arrows = game.MakeArrows(time, 7.5f, ways[i]);
                            foreach (Arrow arr in arrows)
                            {
                                CreateEntity(arr);
                            }
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Area5C()
                {
                    string[] ways = {
                            "(R1)", "", "$2", "", "(R1)", "", "$2(R1)", " ",
                            "(R1)", "", "$2(R1)", "", "(R1)", "", "$2(R1)", "",
                            "", "", "$2", "", "(R1)", "", "$2", "",
                            "(R1)", "", "$2(R1)", "", "(R1)", "", "$2(R1)", "",
                            "(R1)", "", "$2", "", "(R1)", "", "$2", " ",
                            "(R1)", "", "$2", "", "(R1)", "", "$2", "",
                            "($0)($2)","",
                            "$11","",
                            "($0)($2)","",
                            "$11","",
                            "($0)($2)","",
                            "$11","",
                            "($0)($2)","",
                        };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]))
                        {
                            GameObject[] arrows = game.MakeArrows(time, 7.5f, ways[i]);
                            foreach (Arrow arr in arrows)
                            {
                                CreateEntity(arr);
                            }
                        }
                        time += game.BeatTime(1);
                    }
                }

                public static void FinalA()
                {
                    AddInstance(new InstantEvent(game.BeatTime(6.5f), () =>
                    {
                        SetBox(295, 250, 180);
                        SetSoul(2);
                        PlayerInstance.hpControl.GiveProtectTime(30);
                        BoxStates.BoxMovingScale = 0.2f;

                        SideBone a, b;
                        CreateBone(a = new UpBone(false, 320, 0, 1));
                        CreateBone(b = new DownBone(true, 320, 0, 126));

                        AddInstance(new TimeRangedEvent(game.BeatTime(64), () =>
                        {
                            a.MissionLength += 0.45f;
                            b.MissionLength -= 0.45f;
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(56), game.BeatTime(16), () =>
                        {
                            a.Speed += 0.2f;
                            b.Speed += 0.2f;
                        }));
                    }));
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        AddInstance(new InstantEvent(game.BeatTime(7.5f + i * 8), () =>
                        {
                            Heart.GiveForce(x * 180 + 90, 16);
                        }));
                    }
                }
                public static void FinalB()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SetSoul(1);
                        Heart.RotateTo(0);
                        SetGreenBox();
                        TP();
                    }));
                    string[] ways = {
                         "$0($2)", "/", "/", "/", "$0($2)", "/", "/", "/",
                         "$0($2)", "/", "/", "/", "$0($2)", "/", "/", "/",
                         "$1($3)", "/", "/", "/", "$1($3)", "/", "/", "/",
                         "$1($3)", "/", "/", "/", "$1($3)", "/", "/", "/",
                        "$0", "/", "$1", "/", "$2", "/", "$3", "/",
                        "$0", "/", "$1", "/", "$2", "/", "$3", "/",
                        "R(R1)", "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)",  "/", "/", "/", "/", "/", "/", "/",
                        "R(R1)",  "/", "/", "/", "/", "/", "/", "/",
                    };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                }
            }
            private static class ExBarrage
            {
                public static Game game;
                private static Player.Heart playerIns2;

                public static void Intro0SE1()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(30), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(55), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(61), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(65), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(72), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(87), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(93), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(99), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(4));
                    }));
                    float[] beats1 =
                    {
                        game.BeatTime(102),
                        game.BeatTime(110),
                        game.BeatTime(118),
                        game.BeatTime(122),
                        game.BeatTime(126),
                        game.BeatTime(128),
                        game.BeatTime(130),
                        game.BeatTime(132),
                    };
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i] + game.BeatTime(2), () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                            ScreenDrawing.CameraEffect.Convulse(RandBool());
                        }));
                }
                public static void Intro0SE2()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(30), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-5, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(55), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(61), () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-5, 5);
                        ScreenDrawing.ScreenScale = Rand(0.8f, 1);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(65), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(72), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(87), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(93), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-10, game.BeatTime(6));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(99), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(5, game.BeatTime(4));
                    }));
                    float[] beats1 =
                    {
                        game.BeatTime(102),
                        game.BeatTime(110),
                        game.BeatTime(118),
                        game.BeatTime(122),
                        game.BeatTime(126),
                        game.BeatTime(128),
                        game.BeatTime(130),
                    };
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i] + game.BeatTime(2), () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                            ScreenDrawing.CameraEffect.Convulse(RandBool());
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(127), () =>
                    {
                        TextAttribute A = new TextSpeedAttribute(45);
                        TextAttribute B = new TextMoveAttribute((s) =>
                        {
                            return s <= 24 ? new(0, 0) : new(-42 * MathF.Pow(s - 24, 0.9f), Sin((s - 24) * 8) * 55);
                        });
                        CreateEntity(new TextPrinter((int)game.BeatTime(12), "$$Whaaaat?!", new Vector2(100, 300), A, B));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(132), () =>
                    {
                        ScreenDrawing.ScreenAngle = 15;
                        for (int i = 0; i < 4; i++)
                            AddInstance(new InstantEvent(game.BeatTime(i), () =>
                            {
                                ScreenDrawing.CameraEffect.Convulse(game.BeatTime(4f / 8f), RandBool());
                            }));
                    }));
                }
                public static void Intro3SE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    float[] beats1 =
                    {
                        game.BeatTime(8),
                        game.BeatTime(12),
                        game.BeatTime(16),
                        game.BeatTime(20)
                    };
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i], () =>
                        {
                            ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(4);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(28), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(140 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(30), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(6);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(140 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    float[] beats2 =
                    {
                        game.BeatTime(40),
                        game.BeatTime(44),
                        game.BeatTime(48),
                        game.BeatTime(52)
                    };
                    for (int i = 0; i < beats2.Length; i++)
                        AddInstance(new InstantEvent(beats2[i], () =>
                        {
                            ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(4));
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(56), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(4);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(340 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(62), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(2);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(240 + 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        float t = 0;
                        float time = game.BeatTime(6);
                        PlaySound(Sounds.Ding);
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            InstantSetBox(340 - 100 * f, 84, 84);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(70), () =>
                    {
                        TP();
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(70), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(32));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(70), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(32));
                    }));
                    float beats3 = game.BeatTime(102);
                    for (int i = 0; i < 16; i++)
                        AddInstance(new InstantEvent(game.BeatTime(i) + beats3, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(RandBool());
                            ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                        }));
                }

                private class ScreenSplit : RenderProduction
                {
                    public int count;
                    public ScreenSplit(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Opaque, depth) { }
                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        MissionTarget = HelperTarget;
                        for (int i = 0; i < count; i++)
                            for (int j = 0; j < count; j++)
                                DrawTexture(obj, new CollideRect(AdaptedSize.X / count * i, AdaptedSize.Y / count * j, AdaptedSize.X / count, AdaptedSize.Y / count).ToRectangle());
                        return MissionTarget;
                    }
                }
                public static void Area1ASE()
                {
                    // PlaySound(Sounds.switchScene);
                    float[] beats1 =
                    {
                        game.BeatTime(12),
                        game.BeatTime(20),
                        game.BeatTime(28),
                    };
                    ScreenSplit SE = new ScreenSplit(0.4f);
                    SE.count = 1;
                    ScreenDrawing.SceneRendering.InsertProduction(SE);
                    for (int i = 0; i < beats1.Length; i++)
                        AddInstance(new InstantEvent(beats1[i], () =>
                        {
                            SE.count++;
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        SE.count = 1;
                        SE.Dispose();
                    }));
                }
                public static void GreenSoulBoxMove(int MoveType, Vector2 delta, float time)
                {
                    if (MoveType == 1) // f(x) = 2 * x - x * x
                    {
                        float t = 0;
                        Vector2 position = BoxStates.Centre;
                        AddInstance(new TimeRangedEvent(0, time, () =>
                        {
                            float x = t / time;
                            float f = 2 * x - x * x;
                            SetBox(position + delta * f, 84, 84);
                            t++;
                        }));
                    }
                }

                public static void Area4ASE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(8));
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(8));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        float t = 0;
                        float stx = 320;
                        float sty = 295;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx - 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(28), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty - 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx + 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty + 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        ScreenDrawing.CameraEffect.Convulse(20, game.BeatTime(2), false);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = 2 * x - x * x;
                            InstantSetBox(new Vector2(stx + 100 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(68), () =>
                    {
                        float stx = 420;
                        float sty = 295;
                        ScreenDrawing.CameraEffect.Convulse(20, game.BeatTime(2), true);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = 2 * x - x * x;
                            InstantSetBox(new Vector2(stx - 100 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                }
                public static void Area4BSE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-360, game.BeatTime(8));
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(8));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        float t = 0;
                        float stx = 320;
                        float sty = 295;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx - 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(28), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty - 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx + 50 * y, sty), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(36), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float y = -x * x + x;
                            InstantSetBox(new Vector2(stx, sty + 50 * y), BoxStates.Width, BoxStates.Height);
                            t++;
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(40), () =>
                    {
                        float stx = 320;
                        float sty = 295;
                        InstantSetBox(new Vector2(stx, sty), BoxStates.Width, BoxStates.Height);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(56), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.7f);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.7f);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.7f);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(68), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(10, game.BeatTime(4));
                        ScreenDrawing.MakeFlicker(Color.White * 0.7f);
                    }));
                }
                public static void Area5ASE()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        float x = 320, y = 240, t = 0;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(253), () =>
                        {
                            y = 15 * Sin(4 * 180 * t / (game.BeatTime(96) - 1)) + 240;
                            x = 32 * Sin(180 * t / (game.BeatTime(128) - 1)) + 320;
                            InstantSetBox(new Vector2(x, y), BoxStates.Width, BoxStates.Height);
                            InstantTP(BoxStates.Centre);
                            t++;
                        }));
                        for (int i = 0; i < 7; i++)
                            AddInstance(new InstantEvent(game.BeatTime(8 + 256 - 24 + 2 * i), () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.White * 0.4f);
                                ScreenDrawing.CameraEffect.Convulse(RandBool());
                            }));
                    }));
                }

                public static void Intro0()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "G2", "/", "/", "/", "$3", "/", "/", "/",
                            "1", "0", "/", "/", "/", "0", "1", "0",
                            "G0", "/", "/", "/", "/", "/", "GR", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",

                            "G2", "/", "/", "/", "$3", "/", "/", "/",
                            "1", "0", "/", "/", "3", "0", "/", "/",
                            "G0", "/", "/", "/", "/", "/", "GR", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(8);
                    string[] bway = {
                            "G0", "/", "/", "/", "$3", "/", "/", "/",
                            "/", "/", "2", "1", "2", "/", "/", "/",
                            "/", "/", "/", "/", "D", "/", "/", "/",
                            "/", "/", "/", "D", "/", "/", "/", "/",

                            "G0", "/", "/", "/", "$3", "/", "/", "/",
                            "/", "/", "1", "2", "/", "/", "3", "2",
                            "/", "/", "/", "/", "D", "/", "/", "/",
                            "/", "/", "/", "D", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "G2", "/", "/", "/", "$3", "/", "/", "/",
                            "1", "0", "/", "/", "/", "0", "1", "0",
                            "G0", "/", "/", "/", "/", "/", "GR", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",

                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "H", "/", "H", "/", "H", "/", "H", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                        else if (rway[i] == "H") { CreateArrow(curTime, 0, 10, 1, 0); CreateArrow(curTime, 2, 10, 1, 0); }
                        else if (rway[i] == "V") { CreateArrow(curTime, 1, 10, 1, 0); CreateArrow(curTime, 3, 10, 1, 0); }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(8);
                    string[] bway = {
                            "G0", "/", "/", "/", "$3", "/", "/", "/",
                            "/", "/", "2", "1", "2", "/", "/", "/",
                            "/", "/", "/", "/", "D", "/", "/", "/",
                            "/", "/", "/", "D", "/", "/", "/", "/",

                            "H", "/", "/", "/", "/", "/", "/", "/",
                            "H", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                        else if (bway[i] == "H") { CreateArrow(curTime, 0, 10, 0, 0); CreateArrow(curTime, 2, 10, 0, 0); }
                        else if (bway[i] == "V") { CreateArrow(curTime, 1, 10, 0, 0); CreateArrow(curTime, 3, 10, 0, 0); }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "G2", "/", "/", "/", "$3", "/", "/", "/",
                            "1", "0", "/", "/", "/", "0", "1", "0",
                            "G0", "/", "/", "/", "/", "/", "GR", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",

                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "H", "/", "H", "/", "H", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                        else if (rway[i] == "H") { CreateArrow(curTime, 0, 10, 1, 0); CreateArrow(curTime, 2, 10, 1, 0); }
                        else if (rway[i] == "V") { CreateArrow(curTime, 1, 10, 1, 0); CreateArrow(curTime, 3, 10, 1, 0); }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(8);
                    string[] bway = {
                            "G0", "/", "/", "/", "$3", "/", "/", "/",
                            "/", "/", "2", "1", "2", "/", "/", "/",
                            "/", "/", "/", "/", "D", "/", "/", "/",
                            "/", "/", "/", "D", "/", "/", "/", "/",

                            "H", "/", "/", "/", "/", "/", "/", "/",
                            "H", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                        else if (bway[i] == "H") { CreateArrow(curTime, 0, 10, 0, 0); CreateArrow(curTime, 2, 10, 0, 0); }
                        else if (bway[i] == "V") { CreateArrow(curTime, 1, 10, 0, 0); CreateArrow(curTime, 3, 10, 0, 0); }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro3()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "V", "/", "/", "/", "V", "/", "/", "/",
                            "V", "/", "/", "/", "V", "/", "/", "/",
                            "H", "/", "/", "/", "H", "/", "/", "/",
                            "H", "/", "/", "/", "H", "/", "H", "/",

                            "V", "/", "/", "/", "V", "/", "/", "/",
                            "V", "/", "/", "/", "V", "/", "/", "/",
                            "H", "/", "/", "/", "H", "/", "/", "/",
                            "H", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                        else if (rway[i] == "H") { CreateArrow(curTime, 0, 8, 1, 0); CreateArrow(curTime, 2, 8, 1, 0); }
                        else if (rway[i] == "V") { CreateArrow(curTime, 1, 8, 1, 0); CreateArrow(curTime, 3, 8, 1, 0); }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(8);
                    string[] bway = {
                            "$1", "/", "/", "/", "$3", "/", "/", "/",
                            "$1", "/", "/", "/", "$3", "/", "/", "/",
                            "$0", "/", "/", "/", "$2", "/", "/", "/",
                            "$0", "/", "/", "/", "$2", "/", "/", "/",

                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                        else if (bway[i] == "H") { CreateArrow(curTime, 0, 6, 0, 0); CreateArrow(curTime, 2, 6, 0, 0); }
                        else if (bway[i] == "V") { CreateArrow(curTime, 1, 6, 0, 0); CreateArrow(curTime, 3, 6, 0, 0); }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro4()
                {
                    float curTime = game.BeatTime(16);
                    string[] rway = {
                            "H", "/", "H", "/", "H", "/", "H", "/",
                            "H", "/", "H", "/", "H", "/", "H", "/",
                            "H", "/", "H", "/", "H", "/", "H", "/",
                            "H", "/", "H", "/", "/", "/", "/", "/",

                            "$0", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                        else if (rway[i] == "H") { CreateArrow(curTime, 0, 8, 0, 1); CreateArrow(curTime, 2, 8, 0, 1); }
                        else if (rway[i] == "V") { CreateArrow(curTime, 1, 8, 1, 0); CreateArrow(curTime, 3, 8, 1, 0); }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(16);
                    string[] bway = {
                            "$1", "/", "$3", "/", "$1", "/", "$3", "/",
                            "$1", "/", "$3", "/", "$1", "/", "$3", "/",
                            "$1", "/", "$3", "/", "$1", "/", "$3", "/",
                            "$1", "/", "$3", "/", "/", "/", "/", "/",

                            "/", "$2", "/", "-1", "/", "-1", "/", "-1",
                            "/", "-1", "/", "-1", "/", "-1", "/", "-1",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                        else if (bway[i] == "H") { CreateArrow(curTime, 0, 6, 0, 0); CreateArrow(curTime, 2, 6, 0, 0); }
                        else if (bway[i] == "V") { CreateArrow(curTime, 1, 6, 0, 0); CreateArrow(curTime, 3, 6, 0, 0); }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                    AddInstance(new InstantEvent(game.BeatTime(16 + 48), () =>
                    {
                        TextPrinter text = null;
                        AddInstance(new InstantEvent(game.BeatTime(6), () =>
                        {
                            ScreenDrawing.SceneOut(Color.White * 0.7f, game.BeatTime(5.5f));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(12), () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White);
                            SetSoul(0);
                            InstantSetBox(300, 125, 125);

                            Player.Heart p = Heart.Split();
                            playerIns2 = p;

                            SetPlayerMission(1);
                            InstantTP(320, -250);
                            InstantSetBox(-150, 84, 84);
                            SetSoul(1);
                        }));
                        TextAttribute A = new TextSpeedAttribute(45);
                        TextAttribute B = new TextMoveAttribute((s) =>
                        {
                            return s <= 50 ? new(0, 0) : new(-42 * MathF.Pow(s - 50, 0.9f), Sin((s - 50) * 8) * 55);
                        });
                        AddInstance(new InstantEvent(50, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(64, game.BeatTime(1.4f), false);
                            ScreenDrawing.CameraEffect.Convulse(64, game.BeatTime(0.7f), true);
                            AddInstance(new InstantEvent(50, () => { text.Dispose(); }));
                        }));
                        CreateEntity(text = new TextPrinter("$$Right   \nafter   \nthe   \nbreak", new Vector2(60, 150), A, B));
                    }));
                }

                public static void Area1A()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(4),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(90, 50, game.BeatTime(4), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 60, game.BeatTime(8), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 70, game.BeatTime(10), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 40, game.BeatTime(12), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 50, game.BeatTime(14), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 60, game.BeatTime(16), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(4);
                            HeartAttribute.PurpleLineCount = 2;
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            TP(320, 100);
                            SetBox(100, 84, 84);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(0);
                            Heart.RotateTo(0);
                            SetPlayerMission(0);
                            TP(320, 300);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(8),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);

                            float curTime = game.BeatTime(16);
                            string[] bway = {
                                "$2", "/", "/", "/", "$0", "/", "$2", "/",
                                "/", "/", "$0", "/", "$2", "/", "$0", "/",
                            };
                            for (int i = 0; i < bway.Length; i++)
                            {
                                if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                                else if (bway[i] == "H") { CreateArrow(curTime, 0, 6, 0, 0); CreateArrow(curTime, 2, 6, 0, 0); }
                                else if (bway[i] == "V") { CreateArrow(curTime, 1, 6, 0, 0); CreateArrow(curTime, 3, 6, 0, 0); }
                                else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                                curTime += game.BeatTime(1);
                            }

                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(4), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 50, game.BeatTime(8), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(10), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(14), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(16), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(18), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            SetBox(-100, 84, 84);
                            TP(320, -100);
                        }
                    ));
                }
                public static void Area1A0()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(4),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(90, 50, game.BeatTime(4), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 60, game.BeatTime(8), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 70, game.BeatTime(10), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 40, game.BeatTime(12), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 50, game.BeatTime(14), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 60, game.BeatTime(16), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(4);
                            HeartAttribute.PurpleLineCount = 2;
                            Heart.RotateTo(0);
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            TP(320, 100);
                            SetBox(100, 84, 84);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(0);
                            Heart.RotateTo(0);
                            TP(320, 300);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(8),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);

                            float curTime = game.BeatTime(16);
                            string[] bway = {
                                "$2", "/", "/", "/", "$0", "/", "$2", "/",
                                "/", "/", "$0", "/", "$2", "/", "/", "/",
                            };
                            for (int i = 0; i < bway.Length; i++)
                            {
                                if (bway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(bway[i].Substring(1)), 0, 7));
                                else if (bway[i] == "H") { CreateArrow(curTime, 0, 6, 0, 0); CreateArrow(curTime, 2, 6, 0, 0); }
                                else if (bway[i] == "V") { CreateArrow(curTime, 1, 6, 0, 0); CreateArrow(curTime, 3, 6, 0, 0); }
                                else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                                curTime += game.BeatTime(1);
                            }

                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(4), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(8), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(10), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(14), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(16), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            SetBox(-100, 84, 84);
                            TP(320, -100);
                        }
                    ));
                }
                public static void Area1B()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(4),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(90, 50, game.BeatTime(4), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 60, game.BeatTime(8), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(90, 70, game.BeatTime(10), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 40, game.BeatTime(12), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 50, game.BeatTime(14), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(270, 60, game.BeatTime(16), 4) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(4);
                            HeartAttribute.PurpleLineCount = 2;
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            TP(320, 100);
                            SetBox(100, 84, 84);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetPlayerMission(0);
                            SetSoul(2);
                            Heart.GiveForce(180, 10);
                            TP(320, 300);
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(8),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);

                            float curTime = game.BeatTime(16);
                            string[] rway = {
                                "$2", "/", "$0", "$0", "$0", "/", "$2", "/",
                                "$0", "/", "$2", "/", "$0", "/", "/", "/",
                            };
                            for (int i = 0; i < rway.Length; i++)
                            {
                                if (rway[i][0] == 'G') CreateGB(new GreenSoulGB(curTime, GetWayFromTag(rway[i].Substring(1)), 1, 7));
                                else if (rway[i] == "H") { CreateArrow(curTime, 0, 6, 1, 0); CreateArrow(curTime, 2, 6, 1, 0); }
                                else if (rway[i] == "V") { CreateArrow(curTime, 1, 6, 1, 0); CreateArrow(curTime, 3, 6, 1, 0); }
                                else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                                curTime += game.BeatTime(1);
                            }
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(20),
                        () =>
                        {
                            SetBoxMission(0);
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(4), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(6), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(8), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(10), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(12), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(180, 47, game.BeatTime(14), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                            CreateEntity(new Boneslab(0, 47, game.BeatTime(16), 2) { BoneProtruded = () => { PlaySound(Sounds.pierce); } });
                        }
                    ));
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(1);
                            SetPlayerMission(1);
                            SetBox(-100, 84, 84);
                            TP(320, -100);
                        }
                    ));
                }
                public static void Area1C()
                {
                    AddInstance(new InstantEvent(
                        game.BeatTime(7),
                        () =>
                        {
                            SetBoxMission(0);
                            SetPlayerMission(0);
                            SetSoul(3);
                            SetBox(240, 125, 125);
                        }
                    ));
                    for (int x0 = 1; x0 <= 4; x0++)
                    {
                        int x = x0;
                        AddInstance(new InstantEvent(
                            game.BeatTime(8 * x),
                            () =>
                            {
                                SetBoxMission(0);
                                SetPlayerMission(0);
                                for (int i = 0; i <= 3; i++)
                                    CreateBone(new SideCircleBone(90 * i - 2 * game.BeatTime(8 * x), 4, 25 + 20 * x, game.BeatTime(36 - x * 8)));
                            }
                        ));
                    }
                    AddInstance(new InstantEvent(
                        game.BeatTime(38),
                        () =>
                        {
                            SetBoxMission(0);
                            SetPlayerMission(0);
                            SetSoul(0);
                            SetBox(300, 125, 125);
                        }
                    ));
                }

                public static void Area2A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(5.8f), () =>
                    {
                        SetPlayerMission(1);
                        SetSoul(0);
                        SetPlayerMission(0);
                        playerIns2.Merge(Heart);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(6.9f), () =>
                    {
                        SetBox(300, 250, 160);
                        SetSoul(2);
                        BoxStates.BoxMovingScale = 0.24f;
                        Heart.GiveForce(0, 12);
                        TP(320, 480);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(8f), () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.15f;
                        SetBox(300, 160, 160);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        CentreCircleBone a, b;
                        CreateBone(a = new CentreCircleBone(45, 180 / game.BeatTime(16), 240, game.BeatTime(124)) { IsMasked = true, ColorType = 1 });
                        CreateBone(b = new CentreCircleBone(135, 180 / game.BeatTime(16), 240, game.BeatTime(124)) { IsMasked = true });
                        AddInstance(new InstantEvent(game.BeatTime(64), () =>
                        {
                            a.RotateSpeed = -180 / game.BeatTime(16);
                            b.RotateSpeed = 180 / game.BeatTime(8);
                            a.ColorType = 0;
                            b.ColorType = 2;
                            PlaySound(Sounds.Ding);

                            for (int i = 0; i < 4; i++) CreateBone(new SideCircleBone(i * 90, 90 / game.BeatTime(16), 18, game.BeatTime(50)) { ColorType = 1 });
                        }));
                    }));
                    for (int i = 2; i <= 8; i += 1)
                        AddInstance(new InstantEvent(game.BeatTime(8 * i), () =>
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
                            AddInstance(new TimeRangedEvent(game.BeatTime(6), 15, () =>
                            {
                                bones.ForEach(s => s.Speed += 0.4f);
                            })
                            { UpdateIn120 = true });
                            bones.ForEach(s => CreateBone(s));
                            PlaySound(Sounds.pierce);
                        }));
                    for (int i = 9; i <= 15; i++)
                        AddInstance(new InstantEvent(game.BeatTime(8 * i), () =>
                        {
                            CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)) - new Vector2(0, 50), Heart.Centre + GetVector2(120, Rand(0, 359)), new Vector2(1, 0.5f) * 0.95f, game.BeatTime(16), game.BeatTime(2)));
                        }));

                    float curSpeed = 0;
                    AddInstance(new TimeRangedEvent(game.BeatTime(126), game.BeatTime(10), () =>
                    {
                        curSpeed += 5f * 0.0072f;
                        InstantSetBox(BoxStates.Centre - new Vector2(0, curSpeed), BoxStates.Width, BoxStates.Height);
                    })
                    { UpdateIn120 = true }); ;
                    AddInstance(new TimeRangedEvent(game.BeatTime(136), game.BeatTime(8), () =>
                    {
                        curSpeed += 5f * 0.04f;
                        InstantSetBox(BoxStates.Centre - new Vector2(0, curSpeed), BoxStates.Width, BoxStates.Height);
                    })
                    { UpdateIn120 = true }); ;
                }

                public static void Area3A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(5 + 32), () =>
                    {
                        InstantSetBox(600, 84, 84);
                        InstantTP(BoxStates.Centre);
                        BoxStates.BoxMovingScale = 0.05f;
                        SetGreenBox();
                        SetSoul(1);
                        AddInstance(new TimeRangedEvent(0, 500, () =>
                        {
                            InstantTP(BoxStates.Centre);
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        float curTime = game.BeatTime(8);
                        string[] way = {
                            "R(R1)", "/", "/", "/", "R(R1)", "/", "R", "/",
                            "R1", "+01", "+01", "+01", "R", "/", "R1", "/",
                            "/", "/", "R1", "/", "D", "/", "D1", "D",
                            "R1", "/", "D1", "D", "R1", "/", "D1", "D",

                            "D1", "+01", "D", "+0", "D1", "+01", "D", "+0",
                            "D1", "+01", "D", "+0", "D1", "+01", "D", "+0",
                            "D", "/", "/", "/", "R1", "/", "D", "/",
                            "D(R1)", "/", "D(R1)", "/", "$1", "+201", "+2", "+201",
                        };
                        for (int i = 0; i < way.Length; i++)
                        {
                            if (way[i] != "/") game.CreateArrows(curTime, 7.5f, way[i]);
                            curTime += game.BeatTime(1);
                        }
                    }));
                }
                public static void Area3B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(0), () =>
                    {
                        float curTime = game.BeatTime(8);
                        string[] way = {
                            "$1", "/", "/", "/", "R1", "/", "R", "/",
                            "R1", "+01", "+01", "+01", "R", "/", "R1", "/",
                            "/", "/", "R", "/", "D", "/", "D1", "D",
                            "R", "/", "D1", "D", "R", "/", "D1", "D",

                            "D", "/", "D1", "/", "D", "/", "D(D1)", "/",
                            "/", "/", "D1", "/", "D", "/", "D(D1)", "/",
                            "/", "/", "D1", "/", "D", "/", "D(D1)", "/",
                            "/", "/", "D1", "/", "D(D1)", "/", "/", "/",
                        };
                        for (int i = 0; i < way.Length; i++)
                        {
                            if (way[i] != "/") game.CreateArrows(curTime, 7.5f, way[i]);
                            curTime += game.BeatTime(1);
                        }
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(64), () =>
                    {
                        float curTime = game.BeatTime(8);
                        string[] way = {
                            "R(R1)", "/", "/", "/", "R(R1)", "/", "R", "/",
                            "R1", "+01", "+01", "+01", "R", "/", "R1", "/",
                            "/", "/", "R1", "/", "D", "/", "D1", "D",
                            "R1", "/", "D1", "D", "R1", "/", "D1", "D",

                            "D1", "+01", "D", "+0", "D1", "+01", "D", "+0",
                            "D1", "+01", "D", "+0", "D1", "+01", "D", "+0",
                            "D", "/", "/", "/", "R1", "/", "D", "/",
                            "R1", "/", "D", "/","D(R1)(D)", "/", "/", "/",
                        };
                        for (int i = 0; i < way.Length; i++)
                        {
                            if (way[i] != "/") game.CreateArrows(curTime, 7.5f, way[i]);
                            curTime += game.BeatTime(1);
                        }
                    }));
                }

                public static void Area4A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(4.5f), () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Lime * 0.2f);
                        ScreenDrawing.CameraEffect.SizeExpand(50, game.BeatTime(4f));
                        ScreenDrawing.CameraEffect.Convulse(30, game.BeatTime(4f), false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(7f), () =>
                    {
                        SetSoul(3);
                        SetBox(295, 150, 150);
                        BoxStates.BoxMovingScale = 0.2f;
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SideBone a, b;
                        PlaySound(Sounds.spearAppear);
                        CreateBone(a = new DownBone(false, 5, 71));
                        CreateBone(b = new UpBone(true, 5, 71));
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                        {
                            a.Speed *= 0.81f;
                            b.Speed *= 0.81f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            a.Speed = 5.6f;
                            b.Speed = 5.6f;
                            PlaySound(Sounds.pierce);
                        }));
                    }));
                    for (int x = 0; x < 7; x += 2)
                    {
                        int i = x;
                        AddInstance(new InstantEvent(game.BeatTime(24 + 2f * i), () =>
                        {
                            float dist = 75 + (i % 2 == 1 ? 33 : 0);
                            CreateBone(new CustomBone(new Vector2(320, 295) + GetVector2(dist, i * 45), (s) =>
                            {
                                return GetVector2(AdvanceFunctions.Sin01(s.AppearTime / game.BeatTime(8)) * dist * 1.1f, i * 45 + 180);
                            }, Motions.LengthRoute.autoFold, (s) => { return i * 45; })
                            { LengthRouteParam = new float[] { 140.0f + (i % 2 == 1 ? 33 : 0), game.BeatTime(4) } });
                            PlaySound(Sounds.pierce);
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            SideBone a, b;
                            PlaySound(Sounds.spearAppear);
                            CreateBone(a = new LeftBone(false, 5, 71));
                            CreateBone(b = new RightBone(true, 5, 71));
                            AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                            {
                                a.Speed *= 0.81f;
                                b.Speed *= 0.81f;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                a.Speed = 5.6f;
                                b.Speed = 5.6f;
                                PlaySound(Sounds.pierce);
                            }));
                        }));
                        for (int x = 0; x < 7; x += 2)
                        {
                            int i = x;
                            if (x % 4 == 0)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(24 + 2f * i), () =>
                                {
                                    PlaySound(Sounds.pierce);
                                    for (int x = 1; x <= 3; x++)
                                        CreateBone(new DownBone(i == 0, 5 + 2 * x, 145) { ColorType = 2 });
                                }));
                            }
                            else if (x == 2)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(16 + 2f * i), () =>
                                {
                                    for (int c = 0; c < 3; c++)
                                        CreateGB(new NormalGB(new(100, 295 - 75 + 60 * c), new(0, 480), new(1.0f, 0.5f), 0, game.BeatTime(8), game.BeatTime(1f)));
                                }));
                            }
                            else if (x == 6)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(16 + 2f * i), () =>
                                {
                                    for (int c = 0; c < 3; c++)
                                        CreateGB(new NormalGB(new(540, 295 - 45 + 60 * c), new(640, 480), new(1.0f, 0.5f), 180, game.BeatTime(8), game.BeatTime(1f)));
                                }));
                            }
                        }
                    }));
                }
                public static void Area4B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SideBone a, b;
                        PlaySound(Sounds.spearAppear);
                        CreateBone(a = new DownBone(true, 5, 71));
                        CreateBone(b = new UpBone(false, 5, 71));
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                        {
                            a.Speed *= 0.81f;
                            b.Speed *= 0.81f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            a.Speed = 5.6f;
                            b.Speed = 5.6f;
                            PlaySound(Sounds.pierce);
                        }));
                    }));
                    for (int x = 0; x < 7; x += 2)
                    {
                        int i = x;
                        AddInstance(new InstantEvent(game.BeatTime(24 + 2f * i), () =>
                        {
                            float dist = 75 + (i % 2 == 1 ? 33 : 0);
                            CreateBone(new CustomBone(new Vector2(320, 295) + GetVector2(dist, i * 45), (s) =>
                            {
                                return GetVector2(AdvanceFunctions.Sin01(s.AppearTime / game.BeatTime(8)) * dist * 1.1f, i * 45 + 180);
                            }, Motions.LengthRoute.autoFold, (s) => { return i * 45; })
                            { LengthRouteParam = new float[] { 140.0f + (i % 2 == 1 ? 33 : 0), game.BeatTime(4) } });
                            PlaySound(Sounds.pierce);
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            SideBone a, b;
                            PlaySound(Sounds.spearAppear);
                            CreateBone(a = new LeftBone(true, 5, 71));
                            CreateBone(b = new RightBone(false, 5, 71));
                            AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                            {
                                a.Speed *= 0.81f;
                                b.Speed *= 0.81f;
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                a.Speed = 5.6f;
                                b.Speed = 5.6f;
                                PlaySound(Sounds.pierce);
                            }));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(16), () =>
                        {
                            for (int x = 0; x < 4; x++) CreateEntity(new Boneslab(x * 90, 12, game.BeatTime(8), game.BeatTime(14)));
                            CreateGB(new NormalGB(new(320 - 75, 295 - 75), new(0, 0), Vector2.One, 45, game.BeatTime(8), game.BeatTime(1f)));
                            CreateGB(new NormalGB(new(320 + 75, 295 - 75), new(640, 0), Vector2.One, 135, game.BeatTime(8), game.BeatTime(1f)));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(20), () =>
                        {
                            for (int c = 0; c < 3; c++)
                            {
                                CreateGB(new NormalGB(new(100, 295 - 75 + 60 * c), new(0, 480), new(1.0f, 0.5f), 0, game.BeatTime(8), game.BeatTime(1f)));
                                CreateGB(new NormalGB(new(320 - 75 + 60 * c, 100), new(0, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), game.BeatTime(1f)));
                            }
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(24), () =>
                        {
                            for (int x = 0; x < 4; x++) CreateEntity(new Boneslab(x * 90, 42, game.BeatTime(8), game.BeatTime(6)) { ColorType = 2 });
                            CreateGB(new NormalGB(new(100, 295), new(0, 295), Vector2.One, 0, game.BeatTime(8), game.BeatTime(1f)));
                            CreateGB(new NormalGB(new(320, 100), new(320, 0), Vector2.One, 90, game.BeatTime(8), game.BeatTime(1f)));
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(28), () =>
                        {
                            for (int c = 0; c < 3; c++)
                            {
                                CreateGB(new NormalGB(new(540, 295 - 45 + 60 * c), new(640, 480), new(1.0f, 0.5f), 180, game.BeatTime(8), game.BeatTime(1f)));
                                CreateGB(new NormalGB(new(320 - 45 + 60 * c, 100), new(640, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), game.BeatTime(1f)));
                            }
                        }));
                    }));
                }
                public static void Area4C()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SideBone a, b;
                        PlaySound(Sounds.spearAppear);
                        CreateBone(a = new DownBone(true, 5, 71));
                        CreateBone(b = new UpBone(false, 5, 71));
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(8), () =>
                        {
                            a.Speed *= 0.81f;
                            b.Speed *= 0.81f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            a.Speed = 5.6f;
                            b.Speed = 5.6f;
                            PlaySound(Sounds.pierce);
                        }));
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(32), () =>
                    {
                        SetBox(240, 150, 150);
                        playerIns2 = Heart.Split();
                        SetPlayerMission(1); SetBoxMission(1);
                        InstantTP(new(320, 580));
                        AddInstance(new InstantEvent(1, () =>
                        {
                            FightBox box = BoxStates.CurrentBox;
                            InstantSetBox(580, 84, 84);
                            SetSoul(1);
                            AddInstance(new TimeRangedEvent(game.BeatTime(128), () =>
                            {
                                SetPlayerMission(playerIns2);
                                SetBoxMission(box);
                                InstantTP(BoxStates.Centre);
                            })
                            { UpdateIn120 = true });
                            BoxStates.BoxMovingScale = 0.05f;
                            SetGreenBox();
                        }));
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(34), () =>
                    {
                        SetPlayerMission(0); SetBoxMission(0);
                        FightBox box = BoxStates.CurrentBox;
                        float speed = 1.0f;
                        AddInstance(new TimeRangedEvent(game.BeatTime(32), () =>
                        {
                            SetBoxMission(box);
                            speed += 0.04f;
                            InstantSetBox(BoxStates.Centre.Y - speed, 150, 150);
                        })
                        { UpdateIn120 = true });
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(54), () =>
                    {
                        SetBoxMission(0); SetPlayerMission(0);
                        BoxStates.CurrentBox.Dispose();
                        Heart.Dispose();
                    }));
                }
                public static void Area4D()
                {
                    AddInstance(new InstantEvent(game.BeatTime(2), () =>
                    {
                        Regenerate(24);
                        PlaySound(Sounds.heal);
                        SetPlayerMission(playerIns2);
                        string[] ways = {
                            "$1($31)", "", "", "", "$3($11)", "", "", "",
                            "$101($311)", "", "", "", "$301($111)", "", "", "",
                            "$1($31)", "", "", "", "$3($11)", "", "", "",
                            "$101($311)", "", "$101($311)", "", "$301($111)", "", "$301($111)", "",
                        };

                        float time = game.BeatTime(6);
                        for (int i = 0; i < ways.Length; i++)
                        {
                            game.CreateArrows(time, 6.5f, ways[i]);
                            time += game.BeatTime(1);
                        }
                    }));
                }

                public static void Area5A()
                {
                    string[] ways = {
                            "$0($2)(R1)", "", "$0($2)", "", "$0($2)(R1)", "", "$0($2)(R1)", " ",
                            "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)(R1)", "",
                            "$0($2)", "", "$0($2)", "", "$0($2)(R1)", "", "$0($2)", "",
                            "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)(R1)", "",
                            "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)", "", "$0($2)(R1)", "",
                            "$0($2)", "", "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)(R1)", "",
                            "$0($2)", "", "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)", "",
                            "$0($2)", "", "$0($2)(R1)", "", "$0($2)(R1)", "", "$0($2)", "",
                        };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]))
                        {
                            GameObject[] arrows = game.MakeArrows(time, 7.5f, ways[i]);
                            foreach (Arrow arr in arrows)
                            {
                                if (arr.ArrowColor == 0) { arr.Speed = 9.5f; arr.Offset = new Vector2(0, 8); }
                                else if (arr.ArrowColor == 1 && arr.Way % 2 == 0) { arr.Offset = new Vector2(0, -8); }
                                CreateEntity(arr);
                            }
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Area5B()
                {
                    string[] ways = {
                            "$01($21)(R)", "", "$01($21)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)", "", "$01($21)", "", "$01($21)(R)", "", "$01($21)", "",
                            "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)", "(R)", "$01($21)", "(R)", "$01($21)", "(R)", "$01($21)", "(R)",
                            "$01($21)", "(R)", "$01($21)", "(R)", "$01($21)", "(R)", "$01($21)", "",
                        };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]))
                        {
                            GameObject[] arrows = game.MakeArrows(time, 7.5f, ways[i]);
                            foreach (Arrow arr in arrows)
                            {
                                if (arr.ArrowColor == 1) { arr.Speed = 9.5f; arr.Offset = new Vector2(0, 8); }
                                else if (arr.ArrowColor == 0 && arr.Way % 2 == 0) { arr.Offset = new Vector2(0, -8); }
                                CreateEntity(arr);
                            }
                        }
                        time += game.BeatTime(1);
                    }
                }
                public static void Area5C()
                {
                    string[] ways = {
                            "$01($21)(R)", "", "$01($21)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)", "", "$01($21)", "", "$01($21)(R)", "", "$01($21)", "",
                            "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "", "$01($21)(R)", "",
                            "$01($21)(R)", "", "$01($21)", "", "$01($21)(R)", "", "$01($21)", "",
                            "$01($21)(R)", "", "$01($21)", "", "$01($21)(R)", "", "$01($21)", "",
                            "(($0*i)($2*i))[i:0..1]","",
                            "(($0*i)($2*i))[i:0..1]","",
                            "(($0*i)($2*i))[i:0..1]","",
                            "(($0*i)($2*i))[i:0..1]","",
                            "(($0*i)($2*i))[i:0..1]","",
                            "(($0*i)($2*i))[i:0..1]","",
                            "(($0*i)($2*i))[i:0..1]","",
                        };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]))
                        {
                            GameObject[] arrows = game.MakeArrows(time, 7.5f, ways[i]);
                            foreach (Arrow arr in arrows)
                            {
                                if (arr.ArrowColor == 1) { arr.Speed = 9.5f; arr.Offset = new Vector2(0, 8); }
                                else if (arr.ArrowColor == 0 && arr.Way % 2 == 0) { arr.Offset = new Vector2(0, -8); }
                                CreateEntity(arr);
                            }
                        }
                        time += game.BeatTime(1);
                    }
                }

                public static void FinalA()
                {
                    AddInstance(new InstantEvent(game.BeatTime(6.5f), () =>
                    {
                        SetBox(295, 250, 180);
                        SetSoul(2);
                        PlayerInstance.hpControl.GiveProtectTime(30);
                        BoxStates.BoxMovingScale = 0.2f;

                        SideBone a, b;
                        CreateBone(a = new UpBone(false, 320, 0, 1));
                        CreateBone(b = new DownBone(true, 320, 0, 136));

                        AddInstance(new TimeRangedEvent(game.BeatTime(64), () =>
                        {
                            a.MissionLength += 0.49f;
                            b.MissionLength -= 0.49f;
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(56), game.BeatTime(16), () =>
                        {
                            a.Speed += 0.2f;
                            b.Speed += 0.2f;
                        }));
                    }));
                    for (int i = 0; i < 16; i++)
                    {
                        int x = i;
                        AddInstance(new InstantEvent(game.BeatTime(7.5f + i * 4), () =>
                        {
                            Heart.GiveForce(x * 180 + 90, 16);
                        }));
                    }
                }
                public static void FinalB()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        SetSoul(1);
                        Heart.RotateTo(0);
                        SetGreenBox();
                        TP();
                    }));
                    string[] ways = {
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "($*i0)[i:0..2]", "/", "($*i1)[i:0..2]", "/",
                        "$11", "$01", "$10", "$2",
                        "$11", "$01", "$10", "$20",
                        "$11", "$01", "$10", "$20",
                        "$11", "$01", "$10", "$20",
                        "$3($31)", "/", "/", "/",
                        "$301($311)", "/", "$3($31)", "/",
                        "$3($311)", "/", "/", "/",
                        "$3($311)", "/", "/", "/",
                        "$0($2)($011)($211)"
                    };

                    float time = game.BeatTime(8);
                    for (int i = 0; i < ways.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(ways[i]) && ways[i] != "/")
                        {
                            game.CreateArrows(time, 7.5f, ways[i]);
                        }
                        time += game.BeatTime(1);
                    }
                }
            }

            #region Non-ChampionShip
            public void Noob()
            {
                throw new System.NotImplementedException();
            }
            public void Hard()
            {
                throw new System.NotImplementedException();
            }
            public void Extreme()
            {
                throw new System.NotImplementedException();
            }
            public void Easy() { }
            #endregion

            public void Normal()
            {
                if (Gametime < 0) return;
                if (InBeat(0)) NormalBarrage.Intro0();
                if (InBeat(0)) NormalBarrage.Intro0SE1();
                if (InBeat(64)) NormalBarrage.Intro1();
                if (InBeat(128)) NormalBarrage.Intro0();
                if (InBeat(128)) NormalBarrage.Intro0SE2();
                if (InBeat(192)) NormalBarrage.Intro2();
                if (InBeat(256)) NormalBarrage.Intro3();
                if (InBeat(256)) NormalBarrage.Intro3SE();
                if (InBeat(320 - 8)) NormalBarrage.Intro4();
                if (InBeat(384 + 0.1f))
                {
                    NormalBarrage.Area1A();
                    HeartAttribute.KRDamage = 4;
                }
                if (InBeat(416 + 0.1f)) NormalBarrage.Area1B();
                if (InBeat(448 + 0.1f)) NormalBarrage.Area1A0();
                if (InBeat(480 + 0.1f)) NormalBarrage.Area1C();
                if (InBeat(384 + 128 + 0.1f))
                {
                    NormalBarrage.Area1A();
                }
                if (InBeat(416 + 128 + 0.1f)) NormalBarrage.Area1B();
                if (InBeat(448 + 128 + 0.1f)) NormalBarrage.Area1A0();
                if (InBeat(480 + 128 + 0.1f)) NormalBarrage.Area1C();
                if (InBeat(640 + 0.1f))
                {
                    NormalBarrage.Area1A();
                }
                if (InBeat(484 + 128 + 0.1f))
                    NormalBarrage.Area1ASE();
                if (InBeat(640 + 32 + 0.1f)) NormalBarrage.Area1B();
                if (InBeat(640 + 64 + 0.1f)) NormalBarrage.Area1A0();
                if (InBeat(640 + 96 + 0.1f)) NormalBarrage.Area1C();
                if (InBeat(740 + 0.1f))
                    NormalBarrage.Area1ASE();
                if (InBeat(768))
                {
                    NormalBarrage.Area2A();
                    HeartAttribute.KRDamage = 8;
                }
                if (InBeat(896)) NormalBarrage.Area3A();
                if (InBeat(1024)) NormalBarrage.Area3B();
                if (InBeat(1152)) NormalBarrage.Area4A();
                if (InBeat(1152)) NormalBarrage.Area4ASE();
                if (InBeat(1216)) NormalBarrage.Area4B();
                if (InBeat(1216)) NormalBarrage.Area4BSE();
                if (InBeat(1280)) NormalBarrage.Area4C();
                if (InBeat(1312)) NormalBarrage.Area4D();
                if (InBeat(1344)) NormalBarrage.Area5A();
                if (InBeat(1344)) NormalBarrage.Area5ASE();
                if (InBeat(1408)) NormalBarrage.Area5B();
                if (InBeat(1472)) NormalBarrage.Area5A();
                if (InBeat(1536)) NormalBarrage.Area5C();
                if (InBeat(1600)) NormalBarrage.FinalA();
                if (InBeat(1664)) NormalBarrage.FinalB();
            }

            public void ExtremePlus()
            {
                if (Gametime < 0) return;
                if (InBeat(0)) ExBarrage.Intro0();
                if (InBeat(0)) ExBarrage.Intro0SE1();
                if (InBeat(64)) ExBarrage.Intro1();
                if (InBeat(128)) ExBarrage.Intro0();
                if (InBeat(128)) ExBarrage.Intro0SE2();
                if (InBeat(192)) ExBarrage.Intro2();
                if (InBeat(256)) ExBarrage.Intro3();
                if (InBeat(256)) ExBarrage.Intro3SE();
                if (InBeat(320 - 8)) ExBarrage.Intro4();
                if (InBeat(384 + 0.3f))
                {
                    ExBarrage.Area1A();
                    HeartAttribute.KRDamage = 4;
                }
                if (InBeat(416 + 0.3f)) ExBarrage.Area1B();
                if (InBeat(448 + 0.3f)) ExBarrage.Area1A0();
                if (InBeat(480 + 0.3f)) ExBarrage.Area1C();
                if (InBeat(384 + 128 + 0.3f))
                {
                    ExBarrage.Area1A();
                }
                if (InBeat(416 + 128 + 0.3f)) ExBarrage.Area1B();
                if (InBeat(448 + 128 + 0.3f)) ExBarrage.Area1A0();
                if (InBeat(480 + 128 + 0.3f)) ExBarrage.Area1C();
                if (InBeat(640 + 0.3f))
                {
                    ExBarrage.Area1A();
                }
                if (InBeat(484 + 128 + 0.3f))
                    ExBarrage.Area1ASE();
                if (InBeat(640 + 32 + 0.3f)) ExBarrage.Area1B();
                if (InBeat(640 + 64 + 0.3f)) ExBarrage.Area1A0();
                if (InBeat(640 + 96 + 0.3f)) ExBarrage.Area1C();
                if (InBeat(740 + 0.3f))
                    ExBarrage.Area1ASE();
                if (InBeat(768))
                {
                    ExBarrage.Area2A();
                    HeartAttribute.KRDamage = 8;
                }
                if (InBeat(896)) ExBarrage.Area3A();
                if (InBeat(1024)) ExBarrage.Area3B();
                if (InBeat(1152)) ExBarrage.Area4A();
                if (InBeat(1152)) ExBarrage.Area4ASE();
                if (InBeat(1216)) ExBarrage.Area4BSE();
                if (InBeat(1216)) ExBarrage.Area4B();
                if (InBeat(1280)) ExBarrage.Area4C();
                if (InBeat(1312)) ExBarrage.Area4D();
                if (InBeat(1344)) ExBarrage.Area5A();
                if (InBeat(1344)) ExBarrage.Area5ASE();
                if (InBeat(1408)) ExBarrage.Area5B();
                if (InBeat(1472)) ExBarrage.Area5A();
                if (InBeat(1536)) ExBarrage.Area5C();
                if (InBeat(1600)) ExBarrage.FinalA();
                if (InBeat(1664)) ExBarrage.FinalB();
            }

            public void Start()
            {
                //ScreenDrawing.UISettings.CreateUISurface();
                NormalBarrage.game = this;
                ExBarrage.game = this;
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 8;
                HeartAttribute.MaxHP = 100;
                HeartAttribute.Speed = 3.46f;
                HeartAttribute.SoftFalling = true;
                SetGreenBox();
                TP();
                SetSoul(1);
                GametimeDelta = -68.5f + 8 * 4.22297f;
                //   GametimeDetla = 4300;

                //   GametimeDelta = BeatTime(1599);
                // SetSoul(0); 
            }
        }
    }
}