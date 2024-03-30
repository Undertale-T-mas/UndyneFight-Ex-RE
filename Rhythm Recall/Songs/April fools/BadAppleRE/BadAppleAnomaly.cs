using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static Rhythm_Recall.Waves.Determination.Game;
using static UndyneFight_Ex.Entities.Player;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using static Extends.DrawingUtil;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.Entities.Advanced;
using V = Microsoft.Xna.Framework.Vector2;
using ES = UndyneFight_Ex.Entities.SimplifiedEasing.EaseState;
using C = Microsoft.Xna.Framework.Color;
using System.Threading.Tasks;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Texts;
using Microsoft.Xna.Framework.Media;
using UndyneFight_Ex.IO;
using Microsoft.Xna.Framework.Input;
using static Rhythm_Recall.Resources.BadAppleRE;

namespace Rhythm_Recall.Waves
{
    internal partial class BadApple_RE : IChampionShip
    {

        partial class Game
        {

            class anomalyScreen : Entity
            {
                public override void Draw()
                {
                    Centre = new V(320, 240);
                    FormalDraw(anomalyVideo[time], V.Zero, C.White, 0.4444444f, 0, ImageCentre);
                }
                public anomalyScreen()
                {
                    UpdateIn120 = true;
                }
                bool start = false;
                public override void Start()
                {
                    AddInstance(new InstantEvent(85, () => PlaySound(Loader.Load<SoundEffect>("Musics\\BadAppleRE\\anomaly\\anomaly_sound"))));
                    AddInstance(new InstantEvent(100, () =>
                    {
                        CreateEntity(new AppleSongInfo());
                        base.Start();
                        start = true;
                        for (int i = 0; i < 834; i++)
                        {
                            AddInstance(new InstantEvent(62.5f / 60 * i, () =>
                            {
                                time++;
                            }));
                        }
                        AddInstance(new InstantEvent(62.5f / 60 * 834, () =>
                        {
                            time = 0;
                            AddInstance(new InstantEvent(150, () =>
                            {
                                BadAppleSet();
                            }));
                        }));
                    }));
                }
                int time = 0;
                int time2 = 0;
                public override void Update()
                {
                }
                void BadAppleSet()
                {
                    SongFightingScene.SceneParams @params =
                            new(new BadApple_RE.Game(), null, 5, "Content\\Musics\\BadAppleRE\\song",
                            GameStates.CurrentScene is SongFightingScene
                        ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                        : JudgementState.Strict, GameMode.RestartDeny | GameMode.Practice | GameMode.PauseDeny);
                    GameStates.ResetScene(new SongLoadingScene(@params));
#if !DEBUG
                    var customData = PlayerManager.CurrentUser.Custom;
                    if (!customData.Nexts.ContainsKey("reBadApple"))
                        customData.PushNext(new("reBadApple:value=0"));
                    PlayerManager.Save();
#endif
                }
            }
            public class AppleSongInfo : Entity
            {
                public int difficulty { get; set; } = 0;
                private float NameX = 0;
                private Color MainCol;
                float s = 0;
                public override void Update()
                {
                    MainCol = C.White;
                }
                public override void Start()
                {
                    base.Start();
                    RunEase((s) => { NameX = MathHelper.Lerp(0, 340, s); },
                        LinkEase(Stable(0, 0), EaseOut(150, 1, EaseState.Sine), Stable(450, 0), EaseIn(70, -1.5f, ES.Sine)));
                }
                public override void Draw()
                {

                    C nameBoxColor = C.Black;
                    //Name
                    SpriteBatch.DrawVertex(0, new[] {
                    new VertexPositionColor(new(0, 30, 0), C.White),
                    new VertexPositionColor(new(NameX+2.5f, 30, 0), C.White),
                    new VertexPositionColor(new(NameX + 37.5f, 60, 0), C.White),
                    new VertexPositionColor(new(NameX+2.5f, 90, 0), C.White),
                    new VertexPositionColor(new(NameX-42.5f, 90, 0), C.White),
                    new VertexPositionColor(new(NameX-57.5f, 120, 0), C.White),
                    new VertexPositionColor(new(0, 120, 0), C.White),
                    });
                    SpriteBatch.DrawVertex(0.0001f, new[] {
                    new VertexPositionColor(new(0, 35, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX, 35, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX + 30, 60, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX, 85, 0), nameBoxColor),
                    new VertexPositionColor(new(0, 85, 0), nameBoxColor),
                    });
                    //Desc
                    SpriteBatch.DrawVertex(0.0001f, new[] {
                    new VertexPositionColor(new(0, 115, 0), C.DarkGray),
                    new VertexPositionColor(new(NameX - 60, 115, 0), C.DarkGray),
                    new VertexPositionColor(new(NameX - 45, 85, 0), C.DarkGray),
                    new VertexPositionColor(new(0, 85, 0), C.DarkGray),
                });
                    var text = "ExtremePlus:21.5";
                    Font.NormalFont.LimitDraw(text, new(10, 85), MainCol, NameX - 50, 999999, 1, 0.2f);
                    Font.NormalFont.LimitDraw("Bad Apple!!", new V(10, 35), MainCol, NameX - 15, 99999, 1.5f, 0.2f);
                }
            }
            public void Hard()
            {
                if (InBeat(0))
                {
                    CreateEntity(new anomalyScreen());

                }
            }
            public void AnomalyStart()
            {
                GametimeDelta = -1.5f;
            }

            int debug = 2;
            public void Normal()
            {
               
            }
            public void Easy() { }
            public void Extreme() { }
        }
        private class anomaly : Scene
        {
            Game game;
            public anomaly()
            {
                this.AddChild(game = new Game());
            }


            public override void Draw()
            {
                base.Draw();
            }
            bool start = true;
            public override void Update()
            {
                if (start)
                {
                    start = false;
                    game.AnomalyStart();
                }
                game.Hard();
                base.Update();

#if DEBUG
                if (GameStates.IsKeyDown(InputIdentity.Reset))
                {
                    SongFightingScene.SceneParams @params =
                        new(new BadApple_RE.Game(), null, 5, "Content\\Musics\\BadAppleRE\\song",
                        GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Strict, GameMode.RestartDeny);
                    GameStates.ResetScene(new SongLoadingScene(@params));
                }

#endif
            }

        }

        public static void IntoUnlockScene()
        {
            GameStates.ResetScene(new anomaly());
        }
    }
}
