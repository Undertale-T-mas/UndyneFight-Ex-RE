using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.SongSystem;
 
namespace UndyneFight_Ex
{
    public class LoadingScene : Scene
    {
        private class ProgressArrow : Entity
        {
            public ProgressArrow(Vector2 centre, float beginTime)
            {
                Image = GlobalResources.Sprites.progressArrow;
                Centre = centre;
                appearTime = beginTime;
            }

            private float appearTime;
            private float alpha;

            public override void Draw()
            {
                if (alpha > 0)
                    FormalDraw(Image, Centre, Color.White * alpha * 0.8f, 0, ImageCentre);
            }

            public override void Update()
            {
                appearTime++;
                alpha = Functions.Sin(appearTime * 7.5f) * 0.9f + 0.1f;
            }
        }
        private const int loadingCentreY = 430;
        private class LoadingTexture : Entity
        {
            private Rectangle fullBound;
            float alpha = 0;
            public LoadingTexture()
            {
                Image = GlobalResources.Sprites.loadingText;
                fullBound = Image.Bounds;
            }
            public override void Draw()
            {
                Rectangle v = fullBound;
                v.X = 280 - v.Width / 2;
                v.Y = loadingCentreY - v.Height / 2;
                FormalDraw(Image, v, Color.White * alpha);
            }

            public override void Update()
            {
                if (alpha < 1)
                    alpha += 0.05f;
            }
        }
        private class TitleShower : Entity
        {
            private Rectangle fullBound;
            private readonly float scale;
            float alpha = 0;
            public TitleShower(Texture2D texture)
            {
                Centre = GameStartUp.LoadingSettings.TitleCentrePosition;
                Image = texture;
                fullBound = Image.Bounds;
                scale = MathHelper.Min(1, 640f / fullBound.Width);
            }
            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White * alpha, scale, 0, ImageCentre);
            }

            public override void Update()
            {
                if (alpha < 1)
                    alpha += 0.05f;
            }
        }

        public LoadingScene(Action loadingFinished, Action loadingAction)
        {
            Loader.Unload();
            this.loadingFinished = loadingFinished;
            if (GlobalResources.Sprites.loadingTexture != null)
                InstanceCreate(new TitleShower(GlobalResources.Sprites.loadingTexture));
            InstanceCreate(new LoadingTexture());
            for (int i = 0; i < 6; i++)
                InstanceCreate(new ProgressArrow(new(395 + i * 20, loadingCentreY), -i * 6 - 20));

            Thread thread = new(new ThreadStart(() =>
            {
                loadingAction.Invoke();
                finishedLoad = true;
            }));
            loadingThread = thread;
        }

        private readonly Action loadingFinished;
        private readonly Thread loadingThread;
        private int appearTime = 0;

        private bool finishedLoad = false;
        protected int LeastLoadingTime { get; set; } = 120;

        bool eventInvoked = false;

        public override void Update()
        {
            appearTime++;
            if (appearTime == 2) loadingThread.Start();
            if (appearTime >= LeastLoadingTime && finishedLoad && !eventInvoked)
            {
                eventInvoked = true;
                loadingFinished.Invoke();
            }
            base.Update();
        }
        public override void Draw()
        {
            GlobalResources.Font.NormalFont.CentreDraw("Initalizing Game\nPlease Stand By...", new(320, 120), Color.White, 0.8f, 0f);
        }
    }
    public class SongLoadingScene : LoadingScene
    {
        private class Camera : Entity
        {
            private SongSelector sel;
            public Camera()
            {
            }
            public override void Draw()
            {
            }

            public override void Update()
            {
                sel ??= FatherObject as SongSelector;
                Vector2 mission = new(320, 480 * sel.currentSelect + 240);
                Centre = Centre * 0.85f + mission * 0.15f;
            }
        }
        SongInformation Information;
        private static SongFightingScene.SceneParams songParams;
        private readonly List<BackGround> backs = new();
        private readonly Camera camera;
        public SongLoadingScene(SongFightingScene.SceneParams songParams) : base(() =>
        {
            // loadingFinished
            GameStates.InstanceCreate(new InstantEvent(30, () =>
            {
                GameStates.ResetScene(new SongFightingScene(songParams));
            }));
        }, () =>
        {
            // loadingAction
            if (songParams.Waveset.Attributes != null && songParams.Waveset.Attributes.MusicOptimized)
                songParams.MusicOptimized = true;
            SongLoadingScene.songParams.LoadMusic();
        })
        {
            //SongLoadingScene(){...}
            SongLoadingScene.songParams = songParams;
            Information = songParams.Waveset.Attributes;
            tipID = MathUtil.GetRandom(0, additions.Length - 1);
        }

        public SongLoadingScene(Challenge challenge, int progress) : base(() =>
        {
            // loadingFinished
            GameStates.InstanceCreate(new InstantEvent(30, () =>
            {
                GameStates.ResetScene(new SongFightingScene(songParams, challenge, progress));
            }));
        }, () =>
        {
            // loadingAction
            songParams.LoadMusic();
        })
        {
            //SongLoadingScene(){...}
            string path;
            IWaveSet cur = Activator.CreateInstance(challenge.Routes[progress].Item1) as IWaveSet;
            path = "Content\\Musics\\" + cur.Music;
            if (!System.IO.File.Exists($"Content\\Musics\\{cur.Music}.xnb"))
                path += "\\song";
            songParams = new(cur, null, (int)challenge.Routes[progress].Item2, path, JudgementState.Strict, GameMode.RestartDeny);
            tipID = MathUtil.GetRandom(0, additions.Length - 1);
            Information = songParams.Waveset.Attributes;
        }
        float alpha = 0;

        string[] additions = {
            "Every character worth your attention",
            "Do not bite off more than you can chew",
            "Practise mode starts with 99HP instead of Infinite HP formerly",
            "2021 Spring Celebration is the first and only championship with two segments",
            "2023 Memory is the first championship with 2 secret charts",
            "Indihome Paket Phoenix was in Extreme difficulty before upgraded to Extreme plus",
            "Freedom Dive div 1 final part has twice the blue arrows before the nerf",
            "Undyne Extreme was nerfed several times with difficulty drastically dropped",
            "Did you notice that arrows have reduced speed between the SOUL and shields?"
        };

        public override void Draw()
        {
            Depth = 0.1f;
            if (songParams.SongIllustration != null)
            {
                FormalDraw(songParams.SongIllustration, new(320, 240), Color.White * 0.3f, 0, new(320, 240));
            }
            if (Information != null)
            {
                var CurPos = 300;
                if (Information.BarrageAuthor != "Unknown")
                {
                    GlobalResources.Font.NormalFont.CentreDraw("Barrage: " + Information.BarrageAuthor, new(320, CurPos), Color.White * alpha, 0.8f, 0.5f);
                    CurPos += 30;
                }

                if (Information.SongAuthor != "Unknown")
                {
                    GlobalResources.Font.NormalFont.CentreDraw("Song from: " + Information.SongAuthor, new(320, CurPos), Color.White * alpha, 0.8f, 0.5f);
                    CurPos += 30;
                }

                if (Information.PaintAuthor != "Unknown")
                {
                    GlobalResources.Font.NormalFont.CentreDraw("Paint: " + Information.PaintAuthor, new(320, CurPos), Color.White * alpha, 0.8f, 0.5f);
                    CurPos += 30;
                }

                if (Information.AttributeAuthor != "Unknown")
                {
                    GlobalResources.Font.NormalFont.CentreDraw("Effect: " + Information.AttributeAuthor, new(320, CurPos), Color.White * alpha, 0.8f, 0.5f);
                }

                GlobalResources.Font.NormalFont.Draw(Information.Extra, Information.ExtraPosition, Information.ExtraColor * alpha, 0.75f, 0.5f);
            }
            GlobalResources.Font.NormalFont.Draw("Tips: ", new(12, 437), Color.White * alpha, 0.6f, 0.5f);
            GlobalResources.Font.NormalFont.Draw(additions[tipID], new(28, 456), Color.White * alpha, 0.48f, 0.5f);
            base.Draw();
        }
        int tipID;
        public override void Update()
        {
            if (GameStates.IsKeyPressed120f(InputIdentity.Alternate))
            {
                Functions.PlaySound(FightResources.Sounds.Ding);
                tipID = MathUtil.GetRandom(0, additions.Length - 1);
            }
            if (alpha < 1) alpha += 0.05f;
            base.Update();
        }
    }
    internal class ResourcesLoadingScene : LoadingScene
    {
        private static ContentManager loader;
        private static void MainResourcesLoad()
        {
            FightResources.Initialize(loader);
            GameStartUp.Initialize?.Invoke(loader);
        }
        public ResourcesLoadingScene(ContentManager loader) : base(() => GameStates.ResetScene(new GameMenuScene()), MainResourcesLoad)
        {
            ResourcesLoadingScene.loader = loader;
        }
    }
}