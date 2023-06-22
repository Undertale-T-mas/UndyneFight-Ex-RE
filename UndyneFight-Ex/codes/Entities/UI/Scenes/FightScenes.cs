using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.DebugState;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    public abstract class FightScene : Scene
    {
        internal Player PlayerInstance { get; set; }
        internal HPShower HPBar { get; set; }
        internal NameShower NameShow { get; set; }

        private bool playerAlive = true;
        private CheatDetector Detector = new();

        public abstract GameMode Mode { get; }
        public override void Dispose()
        {
            ResetFightState(!playerAlive);
            base.Dispose();
        }
        public FightScene()
        {
            UpdateIn120 = true;
            InstanceCreate(NameShow = new NameShower());
            InstanceCreate(new CheatDetector());
        }

        public void PlayDeath()
        {
            playerAlive = false;
            PlayerDied();

            Achievements.AchievementManager.CheckUserAchievements();
            GameStates.InstanceCreate(new Player.BrokenHeart());
        }
        protected abstract void PlayerDied();

        public override void Start()
        {
        }

        public override void Update()
        {
            if (stopTime <= 0.01f)
            {
                GasterBlaster.shootSoundPlayed = GasterBlaster.spawnSoundPlayed =
                Pike.shootSoundPlayed = Pike.spawnSoundPlayed = false;

                foreach (Player.Heart heart in Player.hearts)
                {
                    if (heart.SoulType == 1) continue;
                    foreach (GameObject entity in Objects)
                    {
                        if (entity is ICollideAble collideAble) collideAble.GetCollide(heart);
                    }
                }
            }

            base.Update();
        }
    }
    internal class NormalFightingScene : FightScene
    {
        private int appearTime = 0, restartTimer = 0;
        private readonly Fight.IClassicFight current;
        private readonly GameMode mode;
        public override GameMode Mode => mode;

        public NormalFightingScene(Fight.IClassicFight obj, GameMode mode)
        {
            this.mode = mode;
            Type type = obj.GetType();
            current = (Fight.IClassicFight)Activator.CreateInstance(type);
        }
        protected override void PlayerDied()
        {
            ResetScene(new TryAgainScene(current, mode));
        }
        public override void Update()
        {
            if (appearTime == 0)
                Fight.ClassicFightEnterance.CreateClassicFight(current);
            appearTime++;
            base.Update();

            restartTimer = (IsKeyDown(InputIdentity.Reset)) ? restartTimer + 1 : 0;
            if (restartTimer >= 45)
            {
                ResetFightState(true);
                GameStates.InstanceCreate(new Player.BrokenHeart());
                ResetScene(new TryAgainScene(Fight.ClassicFightEnterance.currentFight, mode));
            }
        }
    }
    public class SongFightingScene : FightScene
    {
        public class SceneParams
        {
            public IWaveSet Waveset => Activator.CreateInstance(wavesetType) as IWaveSet;

            private readonly Type wavesetType;
            public int difficulty;
            private readonly string musicPath;
            public readonly GameMode mode;

            public JudgementState JudgeState;

            private Audio musicIns;
            private float musicDuration;

            public void LoadMusic()
            {
                string temp = Loader.RootDirectory;
                Loader.RootDirectory = "";
                musicIns = new(musicPath, Loader);
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

            public SceneParams(IWaveSet waveset, Texture2D songIllustration, int difficulty, string musicPath, JudgementState judgeState, GameMode mode)
            {
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
        internal AccuracyBar Accuracy { get; set; }
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
        float asyncTime = 0.0f;

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
                    asyncTime += 0.5f;
                    UpdateSong();

                    bool result;
                    if (lastDelta == -1)
                        lastDelta = music.TryGetPosition(out result);
                    else
                    {
                        float curDelta = music.TryGetPosition(out result);
                        if (MathF.Abs(curDelta - lastDelta) > 0.1f)
                        {
                            lastDelta = curDelta;
                            GlobalDelta = curDelta - asyncTime;
                        }
                    }
                    if (!result) GlobalDelta = 0f;
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
                ResetFightState(false);
                ResetScene(new WinScene(ss, PlayerInstance.GameAnalyzer));
            }
            void ChallengeSave()
            {
                SongResult result;
                result = StateShower.instance.GenerateResult();
                PlayerManager.RecordMark(currentParam.Waveset.FightName, currentParam.difficulty,
                    result.CurrentMark, result.Score, result.AC, result.AP, result.Accuracy);
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
            _challenge.ResultBuffer.Add(new SongResult(SkillMark.Impeccable, 0, 0.99f, false, false));
            _challenge.ResultBuffer.Add(new SongResult(SkillMark.Excellent, 0, 0.98f, true, false));
            _challenge.ResultBuffer.Add(new SongResult(SkillMark.Acceptable, 0, 0.96f, true, true));
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
    }
}