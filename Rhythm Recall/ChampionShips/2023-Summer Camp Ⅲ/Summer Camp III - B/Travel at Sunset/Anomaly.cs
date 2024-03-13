using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Texts;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using col = Microsoft.Xna.Framework.Color;
using rect = UndyneFight_Ex.CollideRect;
using vec2 = Microsoft.Xna.Framework.Vector2;
using VPCT = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;

namespace Rhythm_Recall.Waves
{
    internal partial class Traveler_at_Sunset : IChampionShip
    {
        private static Texture2D anomalyImage0, anomalyImage1, anomalyImage2, anomalyImage3, anomalyImage4;
        public partial class Project
        {
            Glitching glitch;
            ImageEntity i0, i1, i2, i3, i4, i5, i6;
            public void Noob()
            {
                if (InBeat(0))
                {
                    int index = -1;
                    float v = GametimeF;
                    RegisterFunctionOnce("Image1", () =>
                    {
                        CreateEntity(i0 = new(anomalyImage0));
                        i0.Centre = new(420, 140);
                        i0.Scale = 0.45f;
                        i0.AngleMode = true;
                        i0.Depth = 0.2f;
                        RunEase(s => i0.Alpha = s,
                            Linear(BeatTime(8), 0.97f),
                            Linear(BeatTime(8), 0.97f, 0.13f)
                            );

                        RunEase(s => i0.Centre = s,
                            EaseOut(BeatTime(8), new(420, 140), new vec2(320, 240), EaseState.Cubic),
                            EaseInOut(BeatTime(8), new(320, 240), new vec2(-740, 110), EaseState.Cubic)
                            ); ;
                        RunEase(s => i0.Rotation = s,
                            EaseOut(BeatTime(8), 0, 15, EaseState.Cubic),
                            EaseOut(BeatTime(8), 15, 12, EaseState.Cubic)
                            ); ;
                    });
                    RegisterFunctionOnce("Image2", () =>
                    {
                        CreateEntity(i1 = new(anomalyImage1));
                        i1.Centre = new(1420, 340);
                        i1.Scale = 1.16f;
                        i1.AngleMode = true;
                        i1.Depth = 0.12f;
                        RunEase(s => i1.Alpha = s,
                            Linear(BeatTime(8), 0.04f, 0.7f)
                            );
                        RunEase(s => i1.Centre = s,
                            EaseOut(BeatTime(8), new(1420, 340), new vec2(320, 240), EaseState.Cubic)
                            ); ;
                    });
                    RegisterFunctionOnce("Move2", () =>
                    {
                        RunEase(s => i1.Scale = s,
                            EaseOut(BeatTime(16), 1.16f, 0.64f, EaseState.Cubic)
                            ); ;
                        RunEase(s => i1.Rotation = s,
                            EaseInOut(BeatTime(8), 0, 6, EaseState.Cubic),
                            EaseInOut(BeatTime(8), 6, 0, EaseState.Cubic)
                            ); ;
                    });
                    RegisterFunctionOnce("Fade", () =>
                    {
                        RunEase(s => i1.Alpha = s,
                            Linear(BeatTime(5), 0.7f, 0.63f),
                            Linear(BeatTime(3), 0.63f, 0.0f)
                            );

                    });
                    RegisterFunctionOnce("i3", () =>
                    {
                        CreateEntity(i2 = new(anomalyImage2));
                        i2.Centre = new(320, -450);
                        i2.Scale = 0.790774299f;
                        i2.AngleMode = true;
                        i2.Depth = 0.32f;
                        i2.Alpha = 1;
                        CreateEntity(i3 = new(anomalyImage3));
                        i3.Centre = new(320, 240);
                        i3.Scale = 1.0f;
                        i3.AngleMode = true;
                        i3.Depth = 0.37f;
                        i3.Alpha = 0.0f;

                        rect[] rects = {
                            new(0, 0, 300, 200),
                            new(270, 0, 370, 240),
                            new(0, 190, 320, 290),
                            new(270, 190, 370, 290),
                            new(0, 0, 640, 480)
                        };

                        i3.OnDraw += () =>
                        {
                            if (index < 0 || i3.Alpha < 0.01f) return;
                            vec2 tl = new(-107, 0), tr = new(640 + 107, 0), bl = new(-107, 480);
                            vec2[] tri = { tl, tr, bl };
                            rect cur = rects[index];
                            vec2 uvTL = cur.TopLeft, uvBR = cur.BottomRight;
                            vec2 uvTR = cur.TopRight, uvBL = cur.BottomLeft;
                            ScreenDrawing.SpriteBatch.DrawVertex(i3.Image, i3.Depth,
                                new VPCT(new(uvTL, 0), col.White * i3.Alpha, DrawingLab.UVPosition(tri, uvTL)),
                                new VPCT(new(uvTR, 0), col.White * i3.Alpha, DrawingLab.UVPosition(tri, uvTR)),
                                new VPCT(new(uvBR, 0), col.White * i3.Alpha, DrawingLab.UVPosition(tri, uvBR)),
                                new VPCT(new(uvBL, 0), col.White * i3.Alpha, DrawingLab.UVPosition(tri, uvBL))
                                );
                        };
                        RunEase(s => i2.Centre = s,
                            EaseOut(BeatTime(2), new(320, -450), new vec2(320, 240), EaseState.Elastic)
                            ); ;

                        ScreenDrawing.SceneRendering.InsertProduction(glitch = new Glitching());
                        glitch.AverageDelta = 1.5f;
                        glitch.Intensity = 7;
                        glitch.RGBSplitIntensity = 0;
                    });
                    RegisterFunctionOnce("Flash", () =>
                    {
                        RunEase(s => i3.Alpha = s, false,
                            Linear(BeatTime(0.25f), 0.21f, 0),
                            Linear(BeatTime(0.25f), 0.31f, 0),
                            Linear(BeatTime(0.25f), 0.41f, 0)
                            );
                        index++;
                    });
                    RegisterFunctionOnce("Screen", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1f), 1.15f, 1.0f, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("Lag", () =>
                    {
                        glitch.Intensity = 14;
                        glitch.RGBSplitIntensity = 5.5f;
                        glitch.AverageDelta = 0.5f;
                        Extends.DrawingUtil.LerpScreenScale(BeatTime(128), 2, 0.02f);
                    });

                    CreateChart(0, BeatTime(4), 0, new[] { 
                        //1
                        "Image1", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "Image2", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",     
                        //3
                        "Move2", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "Fade", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        //5
                        "i3", "", "", "",     "Flash", "", "Flash", "",
                        "Flash", "", "Flash", "",     "", "", "", "",
                        "Flash", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        //7
                        "Screen", "", "", "Screen",     "", "", "Screen", "",
                        "", "Screen", "", "",     "", "", "Lag", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                    });
                }
            }
            public void AnomalyStart()
            {
                GametimeDelta = -0.5f;
            }
        }
        public class Anomaly : Scene
        {
            MusicPlayer player;

            Project project;

            bool isStarted = false;
            bool isPrepared = false;
            int dif;
            public Anomaly(int dif = 0)
            {
                this.dif = dif;
                GameStates.ResetTime();
                this.AddChild(project = new Project());
                UpdateIn120 = true;
                Task.Run(() =>
                {
                    Loader.RootDirectory = "Content\\Musics\\Traveler at Sunset";
                    player = new(new Audio("anomaly.ogg"));
                    player.Play();
                    anomalyImage0 = Loader.Load<Texture2D>("paint");
                    anomalyImage1 = Loader.Load<Texture2D>("Anomoly03");
                    anomalyImage2 = Loader.Load<Texture2D>("Anomoly01");
                    anomalyImage3 = Loader.Load<Texture2D>("Anomoly02");
                    isPrepared = true;
                    GameStates.ResetTime();
                });
            }

            public override void Update()
            {
#if DEBUG
                if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
                {
                    player.Stop();
                    GameStates.ResetScene(new GameMenuScene()); ;
                }
#endif
                if (isPrepared)
                {
                    if (!isStarted)
                    {
                        isStarted = true;
                        project.AnomalyStart();
                        CreateEntity(new TASSongInfo()
                        {
                            difficulty = dif
                        });
                    }
                    project.Noob();
                    if (GametimeF > 1175)
                    {
                        int d = dif == 1 ? 2 : 5;
                        GameStates.difficulty = d;
                        GameStates.ResetScene(new SongLoadingScene(new(
                            Activator.CreateInstance(typeof(Project)) as IWaveSet, anomalyImage0, d,
                            "Content\\Musics\\Traveler at Sunset\\song", JudgementState.Strict, GameMode.PauseDeny | GameMode.RestartDeny | GameMode.Practice, false
                            )));

                        var customData = PlayerManager.CurrentUser.Custom;
                        if (!customData.Nexts.ContainsKey("TaSAnomaly"))
                            customData.PushNext(new("TasAnomaly:value=0"));
                        customData.Nexts["TaSAnomaly"]["value"] = dif.ToString();
                        PlayerManager.Save();
                    }
                }
                base.Update();
            }
        }
        public class TASSongInfo : Entity
        {
            public int difficulty { get; set; } = 0;
            private float NameX = 0;
            private Color MainCol;
            public override void Update()
            {
                MainCol = difficulty == 1 ? col.LightBlue : col.Red;
                if (NameX == 0)
                {
                    Text text = TextUtils.DrawText(4, "$Traveller $At $Sunset", new vec2(10, 45), true,
                        new TextColorEffect(MainCol, 1),
                        new TextColorEffect(MainCol, 1),
                        new TextColorEffect(MainCol, 1)
                        );
                    AddInstance(text);
                }
                if (NameX < 340)
                    NameX = MathHelper.Lerp(NameX, 340, MathF.Min(GametimeF / 240, 1));
            }
            public override void Draw()
            {
                col nameBoxColor = col.Lerp(col.Green, col.Yellow, AdvanceFunctions.Sin01(GametimeF / 90));
                //Name
                SpriteBatch.DrawVertex(0, new[] {
                    new VertexPositionColor(new(0, 35, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX, 35, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX + 30, 60, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX, 85, 0), nameBoxColor),
                    new VertexPositionColor(new(0, 85, 0), nameBoxColor),
                });
                //Desc
                SpriteBatch.DrawVertex(0, new[] {
                    new VertexPositionColor(new(0, 115, 0), col.White),
                    new VertexPositionColor(new(NameX - 60, 115, 0), col.White),
                    new VertexPositionColor(new(NameX - 45, 85, 0), col.White),
                    new VertexPositionColor(new(0, 85, 0), col.White),
                });
                var text = difficulty == 1 ? "div 2 : 12.0" : "div 1 : 20.6";
                Font.NormalFont.LimitDraw(text, new(10, 85), MainCol, NameX - 15, 999999, 1, 0);
            }
        }
    }
}