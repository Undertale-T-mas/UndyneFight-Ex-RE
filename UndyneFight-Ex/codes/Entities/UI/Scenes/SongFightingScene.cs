using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using System.Net.NetworkInformation;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.DebugState;
using static UndyneFight_Ex.GameStates;
using Microsoft.Xna.Framework.Audio;

namespace UndyneFight_Ex.Entities
{
    public class SongFightingScene : FightScene
    {
        private class SongConditionOptimizer : Entity
        {
            float asyncTime = 0.0f;
            SongFightingScene fatherScene;
            public SongConditionOptimizer(SongFightingScene scene)
            {
                UpdateIn120 = true;
                fatherScene = scene;
            }
            const float deltaDured = 3.1f;

            int index = 0;
            float[] data = new float[55];
            float[] cur = new float[10];
            float timer = 0.0f;
            float avg = 0.0f;

            public float GlobalAVG = 0.0f;

            public override void Draw()
            {
#if DEBUG
                FightResources.Font.NormalFont.CentreDraw(GameMain.UpdateCost.ToString("F3"), new(100, 150), Color.White, 0.7f, 0.1f); 
                FightResources.Font.NormalFont.CentreDraw(KeyCheckTime1.ToString("F3"), new(50, 200), Color.White, 0.7f, 0.1f); 
                FightResources.Font.NormalFont.CentreDraw(KeyCheckTime2.ToString("F3"), new(150, 200), Color.White, 0.7f, 0.1f); 
#endif
            }
            
            public override void Update()
            {
                timer += 0.5f;

                float del2 = GameMain.gameSpeed;
                del2 *= 15;
                del2 -= 15;

                bool result = false;
                Audio music = fatherScene.music;
                float curTime = music.TryGetPosition(out result);

                fatherScene.GlobalDelta = curTime - Functions.GametimeF;

                if (!result) fatherScene.GlobalDelta = 0f;

                if (timer > 15f)
                {
                    if (index < data.Length)
                    {
                        data[index] = fatherScene.GlobalDelta;
                        index++;
                        if(index == data.Length)
                        {
                            for (int i = 0; i < data.Length; i++) avg += data[i] / data.Length;
                        }
                    }
                }
                if (index > data.Length / 2f && timer > 25f)
                {
                    // avg = curDelta - asyncTime - Functions.GametimeDelta;
                    bool result2;
                    float realTime = fatherScene.music.TryGetPosition(out result2);
                    if (!result2) return;
                    cur[^1] = realTime - Functions.GametimeF;
                    for (int i = 0; i < cur.Length - 1; i++) cur[i] = cur[i + 1];
                    GlobalAVG = 0;
                    for (int i = 0; i < cur.Length; i++) GlobalAVG += cur[i] / cur.Length; 
                }
                if(index >= data.Length)
                {
                    if (MathF.Abs(GlobalAVG - avg + del2) > deltaDured)
                        fatherScene.music.TrySetPosition(avg + Functions.GametimeF + del2);
                }
            }
        }

        public void SetSongPosition(float position)
        {
            this.music.TrySetPosition(position);
        }

        public class SceneParams
        {
            public IWaveSet Waveset => Activator.CreateInstance(wavesetType) as IWaveSet;

            private readonly Type wavesetType;
            public int difficulty;
            private readonly string musicPath;
            public GameMode mode;

            public JudgementState JudgeState;

            private Audio musicIns;
            private float musicDuration;

            public bool MusicOptimized { get; set; } = false;

            public void LoadMusic()
            {
                string temp = Loader.RootDirectory;
                Loader.RootDirectory = "";
                if (!MusicOptimized)
                    musicIns = new(musicPath, Loader);
                else musicIns = new(musicPath + ".ogg", Loader);
                if (SongIllustration != null && SongIllustration.IsDisposed)
                {
                    SongIllustration = Loader.Load<Texture2D>(SongIllustration.Name);
                }
                Loader.RootDirectory = temp;
                musicDuration = (float)musicIns.SongDuration.TotalSeconds * 62.5f;
            }
            public Audio Music => musicIns;
            public bool MusicLoaded => musicIns != null;
            public float MusicDuration => musicDuration;

            public Texture2D SongIllustration { get; set; }
            public bool IsUnload { get; private set; }

            public SceneParams(IWaveSet waveset, Texture2D songIllustration, int difficulty, string musicPath, JudgementState judgeState, GameMode mode = GameMode.None, bool unload = true)
            {
                IsUnload = unload;
                SongIllustration = songIllustration;
                JudgeState = judgeState;
                wavesetType = waveset.GetType();
                this.difficulty = difficulty;
                this.musicPath = musicPath;
                this.mode = mode;
            }
        }

        private IWaveSet waveset;
        private readonly SceneParams currentParam;
        private int appearTime = 0;
        public SongFightingScene(SceneParams _params)
        {
            currentParam = _params;
        }
        public SongFightingScene(SceneParams _params, Challenge _challenge, int progress)
        {
            this._challenge = _challenge;
            currentParam = _params;
            _challengeProgress = progress;
            difficulty = _params.difficulty;
        }
        private Challenge _challenge = null;
        private int _challengeProgress;
        public AccuracyBar Accuracy { get; set; }
        internal StateShower ScoreState { get; set; }
        internal TimeShower Time { get; set; }
        public JudgementState JudgeState => currentParam.JudgeState;
        public Difficulty CurrentDifficulty => (Difficulty)currentParam.difficulty;

        private GameMode mode;
        public override GameMode Mode => mode;

        private volatile bool songLoaded = false;
        private Audio music;
        private bool forceEnd = false;
        public float PlayOffset { get; set; } = 0;

        public bool AutoEnd { get; set; } = true;
        public Texture2D SongIllustration { get; set; } = null;
        private bool endRunned = false;

        private int restartTimer = 0;

        internal void ForceEnd()
        {
            forceEnd = true;
        }

        private float lastDelta = -1;
        public float GlobalDelta { get; private set; }

        public override void Update()
        {
            //debug
            //  if (this._challenge != null) this.TempIntro();
            if (waveset != null)
            {
                restartTimer = (IsKeyDown(InputIdentity.Reset)) ? restartTimer + 1 : 0;
                if (restartTimer >= 60 || (IsKeyPressed120f(InputIdentity.Reset) && IsKeyDown(InputIdentity.Alternate)))
                {
                    PlayDeath();
                    return;
                }
            }

            appearTime++;

            if (appearTime == 2)
            {
                music = currentParam.Music;
                GameStates.InstanceCreate(new InstantEvent(30, () =>
                {
                    SetSongFight();
                }));
            }
            if (songLoaded)
            {
                if (waveset != null)
                {
                    UpdateSong();
                }

                bool needEnd = waveset != null && music != null && appearTime > currentParam.MusicDuration * 2 && music.IsEnd;

                if (needEnd)
                {
                    if (!endRunned)
                    {
                        StateShower.instance.EndAction?.Invoke();
                        endRunned = true;
                    }
                }
                if ((needEnd && AutoEnd) || forceEnd)
                {
                    Surface.Normal.drawingAlpha -= 0.015f;
                    if (Surface.Normal.drawingAlpha < 0)
                    {
                        if (_challenge == null)
                            WinFight();
                        else
                        {
                            ChallengeSave();
                        }
                    }
                }
            }
            mode = currentParam.mode;
            base.Update();

            void WinFight()
            {
                StateShower ss = StateShower.instance;
                if (isPaused)
                {
                    ss.PauseTime = this.pauseTime;
                }
                ResetFightState(false);
                ResetScene(new WinScene(ss, PlayerInstance.GameAnalyzer));
            }
            void ChallengeSave()
            {
                SongResult result;
                result = StateShower.instance.GenerateResult();
                PlayerManager.RecordMark(currentParam.Waveset.FightName, currentParam.difficulty,
                    result.CurrentMark, result.Score, result.AC, result.AP, result.Accuracy, 0);
                PlayerManager.Save();
                ResetFightState(false);
                _challenge.ResultBuffer.Add(result);
                ResetScene((_challenge.Routes.Length == _challengeProgress + 1)
                    ? new ChallengeWinScene(_challenge)
                    : new SongLoadingScene(_challenge, _challengeProgress + 1));
            }
        }

        private void SetSongFight()
        {
            bool auto = (mode & GameMode.Autoplay) != 0;
            otherAuto = redShieldAuto = blueShieldAuto = greenShieldAuto = purpleShieldAuto = auto;

            MathUtil.rander = new Random(seed);
            ResetTime();
            isInBattle = true;
            songLoaded = true;
            GameStates.InstanceCreate(Accuracy = new AccuracyBar());
            InstanceCreate(HPBar = new HPShower());
            waveset = currentParam.Waveset;
            InstanceCreate(ScoreState = new StateShower(waveset, currentParam.difficulty, JudgeState, currentParam.mode, currentParam.MusicDuration));
            InstanceCreate(Time = new TimeShower());
            StartBattle();
            if ((mode & GameMode.PauseDeny) == 0)
                this.Pausable = true;
            PlayerInstance = new Player();
            InstanceCreate(PlayerInstance);
            waveset.Start();
            //music.PlayPosition = (PlayOffset != 0) ? PlayOffset : 0;
            //怎么不就 = PlayOffset(  ~TK
            if (PlayOffset != 0)
                music.PlayPosition = PlayOffset;
            music.Play();

            SongIllustration = currentParam.SongIllustration;

            if (waveset is GameObject) InstanceCreate(waveset as GameObject);
#if DEBUG
            InstanceCreate(new SongConditionOptimizer(this));
#endif
        }

        private void UpdateSong()
        {
            switch (currentParam.difficulty)
            {
                case 0: waveset.Noob(); break;
                case 1: waveset.Easy(); break;
                case 2: waveset.Normal(); break;
                case 3: waveset.Hard(); break;
                case 4: waveset.Extreme(); break;
                case 5: waveset.ExtremePlus(); break;
            }
        }

        //debug
        private void TempIntro()
        {
            // debug
            _challenge.ResultBuffer.Add(new SongResult(SkillMark.Impeccable, 0, 0.99f, false, false, 0));
            _challenge.ResultBuffer.Add(new SongResult(SkillMark.Excellent, 0, 0.98f, true, false, 0));
            _challenge.ResultBuffer.Add(new SongResult(SkillMark.Acceptable, 0, 0.96f, true, true, 0));
            ResetScene(new ChallengeWinScene(_challenge));
        }
        public override void Dispose()
        {
            music?.Stop();
            base.Dispose();
            waveset = null;
        }
        protected override void PlayerDied()
        {
            ResetScene(!isReplay ? new TryAgainScene(StateShower.instance)
                : new TryAgainScene(new RecordSelector()));
        }
        bool isPaused = false;
        float pauseTime = 0.0f;
        public override void WhenPaused()
        {
            pauseTime += 0.5f;
        }
        public override void AlternatePause()
        {
            if (isPaused)
            {
                music.Resume();
                isPaused = false;
            } 
            else
            {
                this.ScoreState.PauseUsed();
                isPaused = true;
                music.Pause();
            }
        }
    }
}