using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.File;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;
using static UndyneFight_Ex.ChampionShips.ChampionShip;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    internal class ModeSelector : Selector
    {
        private float alpha = 0;

        public ModeSelector()
        {
            selectedMode = last;
            ResetSelect();
            PlaySound(select, 0.9f);
            isRecord = true;
            isReplay = false;
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp))
                {
                    currentSelect--;
                    if (currentSelect == 1) currentSelect--;
                }
                else if (IsKeyPressed120f(InputIdentity.MainDown))
                {
                    if (currentSelect == 0) currentSelect++;
                    currentSelect++;
                }
                else if (IsKeyPressed120f(InputIdentity.MainRight))
                {
                    currentSelect++;
                }
                else if (IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    currentSelect--;
                }
                if (currentSelect >= SelectionCount) currentSelect -= SelectionCount;
                else if (currentSelect < 0) currentSelect = SelectionCount - 1;
            };
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };
            Selected += () =>
            {
                PlaySound(select, 0.9f);
            };
            PushSelection(new SelectEnd(this));
            PushSelection(new ChallengeMode(this));
            PushSelection(new Buffed(this));
            PushSelection(new NoHitter(this));
            PushSelection(new APer(this));
            PushSelection(new Practice(this));
            PushSelection(new NoGreenSoul(this));
            PushSelection(new Autoplay(this));

            AutoDispose = false;
        }

        public override void Dispose()
        {
            last = selectedMode;
            base.Dispose();
        }

        private static GameMode last;
        private GameMode selectedMode;

        public class SelectEnd : TextSelection
        {
            private readonly ModeSelector selector;
            public SelectEnd(ModeSelector selector) : base("Start Game", new Vector2(190, 160 - 23 - 30))
            {
                this.selector = selector; Size = 0.9f;
            }
            public override void SelectionEvent()
            {
                InstanceCreate(new SongSelector(selector.selectedMode));
                selector.Dispose();
                base.SelectionEvent();
            }
        }
        public class ChallengeMode : TextSelection
        {
            private readonly ModeSelector selector;
            public ChallengeMode(ModeSelector selector) : base("Start Challenge", new Vector2(450, 160 - 21 - 30))
            {
                this.selector = selector; Size = 0.9f;
            }
            public override void SelectionEvent()
            {
                if (selector.selectedMode != 0) return;
                InstanceCreate(new ChallengeSelector());
                selector.Dispose();
                base.SelectionEvent();
            }
            public override void Update()
            {
                TextColor = selector.selectedMode == 0 ? Color.Aqua : Color.Red;
                base.Update();
            }
        }
        public class Buffed : TextSelection
        {
            private readonly ModeSelector selector;
            public Buffed(ModeSelector selector) : base("Buffed", new Vector2(320, 230 - 21))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.Buffed) != 0 ? Color.Red : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.Buffed;
                TextColor = (selector.selectedMode & GameMode.Buffed) != 0 ? Color.Red : Color.Gray;
                base.SelectionEvent();
            }
        }
        public class NoHitter : TextSelection
        {
            private readonly ModeSelector selector;
            public NoHitter(ModeSelector selector) : base("No Hit", new Vector2(320, 275 - 21))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.NoHit) != 0 ? Color.Yellow : Color.Gray;
            }
            bool Interrupted => (selector.selectedMode & GameMode.Practice) != 0;
            public override void SelectionEvent()
            {
                if (!Interrupted)
                {
                    selector.selectedMode ^= GameMode.NoHit;
                    TextColor = (selector.selectedMode & GameMode.NoHit) != 0 ? Color.Yellow : Color.Gray;
                }
                base.SelectionEvent();
            }
            public override void Update()
            {
                if (Interrupted) TextColor = Color.DarkRed;
                if (!Interrupted && TextColor == Color.DarkRed) TextColor = Color.Gray;
                base.Update();
            }
        }
        public class APer : TextSelection
        {
            private readonly ModeSelector selector;
            public APer(ModeSelector selector) : base("Perfect Only", new Vector2(320, 320 - 21))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.PerfectOnly) != 0 ? Color.Yellow : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.PerfectOnly;
                TextColor = (selector.selectedMode & GameMode.PerfectOnly) != 0 ? Color.Yellow : Color.Gray;
                base.SelectionEvent();
            }
        }
        public class Practice : TextSelection
        {
            private readonly ModeSelector selector;
            public Practice(ModeSelector selector) : base("Practice", new Vector2(320, 365 - 21))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.Practice) != 0 ? Color.Lime : Color.Gray;
            }
            bool Interrupted => (selector.selectedMode & GameMode.NoHit) != 0;
            public override void SelectionEvent()
            {
                if (!Interrupted)
                {
                    selector.selectedMode ^= GameMode.Practice;
                    TextColor = (selector.selectedMode & GameMode.Practice) != 0 ? Color.Lime : Color.Gray;
                }
                base.SelectionEvent();
            }
            public override void Update()
            {
                if (Interrupted) TextColor = Color.DarkRed;
                if (!Interrupted && TextColor == Color.DarkRed) TextColor = Color.Gray;
                base.Update();
            }
        }
        public class NoGreenSoul : TextSelection
        {
            private readonly ModeSelector selector;
            public NoGreenSoul(ModeSelector selector) : base("No green soul", new Vector2(320, 410 - 21))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.NoGreenSoul) != 0 ? Color.Beige : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.NoGreenSoul;
                TextColor = (selector.selectedMode & GameMode.NoGreenSoul) != 0 ? Color.Bisque : Color.Gray;
                base.SelectionEvent();
            }
        }
        public class Autoplay : TextSelection
        {
            private readonly ModeSelector selector;
            public Autoplay(ModeSelector selector) : base("Autoplay", new Vector2(320, 455 - 21))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.Autoplay) != 0 ? Color.Gold : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.Autoplay;
                TextColor = (selector.selectedMode & GameMode.Autoplay) != 0 ? Color.Gold : Color.Gray;
                base.SelectionEvent();
            }
        }

        public override void Update()
        {
            if (alpha < 1)
                alpha += 0.025f;
            base.Update();
        }

        public override void Draw()
        {
            GlobalResources.Font.NormalFont.CentreDraw("Select your gamemode", new Vector2(320, 45), Color.White * alpha);
            //GlobalResources.Font.NormalFont.CentreDraw("Extra Settings", new Vector2(320, 155), Color.White * alpha);
            base.Draw();
        }
    }

    internal class SongSelector : Selector
    {
        private class TextSelectionEx : Entity, ISelectAble
        {
            public static void Reset() { missionDetla = currentDetla = Vector2.Zero; }
            private static Vector2 missionDetla;
            private static Vector2 currentDetla;
            public static void MoveUp()
            {
                missionDetla += new Vector2(0, 52);
            }
            public static void MoveDown()
            {
                missionDetla += new Vector2(0, -52);
            }

            public static void Move()
            {
                currentDetla = currentDetla * 0.8f + missionDetla * 0.2f;
            }

            public class ShinyTextEffect : Entity
            {
                private readonly string texts;
                private float alpha = 0.0f, size;
                private Color showingColor;

                public ShinyTextEffect(TextSelectionEx s)
                {
                    Centre = s.Centre;
                    size = s.size * s.currentSize;
                    alpha = s.alpha;
                    texts = s.texts;
                    showingColor = Color.Lerp(s.showingColor, Color.Gold, 0.5f);
                }
                public override void Draw()
                {
                    FightResources.Font.NormalFont.Draw(texts, Centre - new Vector2(0, size * 14 - 14), showingColor * alpha, size, 0.9f);
                }

                public override void Update()
                {
                    collidingBox.Y -= 0.1f + alpha * 0.4f;
                    alpha *= 0.9f;
                    alpha -= 0.03f;
                    size += ((2 - alpha) / 40f + 0.04f) / 1.6f;
                    if (alpha < 0) Dispose();
                }
            }

            public TextSelectionEx(string texts, Vector2 Location)
            {
                controlLayer = Surface.Hidden;
                this.texts = texts;
                startLocation = Location;
                controlLayer = Surface.Hidden;
            }

            private Vector2 startLocation;
            protected string texts;
            private float alpha = 0.0f, currentSize = 1.0f, maxSize = 1.35f;
            private const float sizeChangeSpeed = 0.18f;
            private bool isSelected;

            public float MaxSize
            {
                set
                {
                    maxSize = value;
                }
            }
            public float Size
            {
                set
                {
                    size = value;
                }
            }
            public Color SetColor
            {
                set
                {
                    showingColor = value;
                }
            }
            private float size = 0.8f;
            private Color showingColor = Color.White;

            private Action action;
            public Action SetSelectionAction
            {
                set => action = value;
            }

            public void DeSelected()
            {
                isSelected = false;
            }

            public override void Draw()
            {
                float size1 = currentSize * size;
                FightResources.Font.NormalFont.Draw(texts, startLocation + currentDetla - new Vector2(0, size1 * 14 - 14), showingColor * alpha, size1, 0.9f);
            }

            public void Selected()
            {
                isSelected = true;
            }

            public override void Update()
            {
                if (alpha < 1f) alpha += 0.025f;
                else alpha = 1f;
                currentSize = isSelected
                    ? currentSize * (1 - sizeChangeSpeed) + maxSize * sizeChangeSpeed
                    : currentSize * (1 - sizeChangeSpeed) + sizeChangeSpeed;
                Centre = currentDetla + startLocation;
            }

            public virtual void SelectionEvent()
            {
                action?.Invoke();
                InstanceCreate(new ShinyTextEffect(this));
            }
        }

        public static bool IsRaceSong { get; private set; }
        public static IChampionShip SelectedChampionShip { get; private set; }

        private float alpha = 0;
        private int appearTime = 0;
        private int currentPos = 1;

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

        private readonly Camera camera;
        private readonly List<BackGround> backs = new();
        private readonly GameMode mode;
        private bool customsExist;
        private int songNum;
        private float RectPos = 0;
        private int RectHeight = 25;
        private float RectPosTarget = 0;
        private float RectPosChange = 0;

        public SongSelector(GameMode mode)
        {
            Last ??= new IntroUI();
            customsExist = FightSystem.MainGameFights.Values.Length > 0;

            this.mode = mode;

            ResetSelect();

            TextSelectionEx.Reset();
            SelectChanger += () =>
            {
                RectPosChange = (109f / (songNum - 1));
                if (IsKeyPressed120f(InputIdentity.MainUp) || IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    currentSelect--;

                    if (currentPos == 3)
                    {
                        currentPos--;
                        RectPosTarget -= RectPosChange;
                    }
                    else if (currentPos == 2 && currentSelect > 0)
                    {
                        TextSelectionEx.MoveUp();
                        RectPosTarget -= RectPosChange;
                    }
                    else if (currentSelect != -1)
                    {
                        currentPos--;
                        RectPosTarget -= RectPosChange;
                    }
                }
                else if (IsKeyPressed120f(InputIdentity.MainDown) || IsKeyPressed120f(InputIdentity.MainRight))
                {
                    currentSelect++;
                    if (currentPos == 1)
                    {
                        currentPos++;
                        RectPosTarget += RectPosChange;
                    }
                    else if (currentPos == 2 && currentSelect <= SelectionCount - 1)
                    {
                        TextSelectionEx.MoveDown();
                        RectPosTarget += RectPosChange;
                    }
                    else if (currentSelect != SelectionCount)
                    {
                        currentPos++;
                        RectPosTarget += RectPosChange;
                    }
                }
                if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                else if (currentSelect < 0) currentSelect = 0;
            };
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };

            camera = new Camera();
            AddChild(camera);
            DateTime dt = DateTime.Now;
        }

        public override void Start()
        {
            FightBox.boxs = new List<FightBox>();
            RectangleBox box = new(new CollideRect(60, 144, 520, 166));
            InstanceCreate(box);
            int y = 0;
            songNum = 0;
            foreach (var type in FightSystem.CurrentSongs.Values)
            {
                songNum++;
                //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                object tem = Activator.CreateInstance(type);

                IWaveSet wave;
                bool res1 = tem is IChampionShip;
                bool available = true;

                wave = res1 ? (tem as IChampionShip).GameContent : (tem as IWaveSet);
                if (wave.Attributes != null && wave.Attributes.Hidden) continue;

                string name = wave.FightName;

                string filePath = $"Content\\Musics\\{wave.Music}";

                Texture2D songImage = null;

                if (!Exists(filePath + "\\song.xnb"))
                {
                    if (!Exists(filePath + ".xnb"))
                    {
                        available = false;
                        name = $"({name})";
                    }
                }
                else
                {
                    if (Exists(filePath + "\\paint.xnb"))
                    {
                        string tmp = Scene.Loader.RootDirectory;
                        Scene.Loader.RootDirectory = "";
                        BackGround v;
                        AddChild(v = new BackGround(songImage = Scene.Loader.Load<Texture2D>($"{filePath}\\paint"), camera, new(320, 240 + y * 480)));
                        v.Alpha = 0.3f;
                        backs.Add(v);
                        Scene.Loader.RootDirectory = tmp;
                    }
                    filePath += "\\song";
                }

                TextSelectionEx selection = new(name, new Vector2(100, 160 + 52 * y))
                {
                    SetSelectionAction = () =>
                    {
                        if (!available)
                        {
                            ResetScene(new GameMenuScene());
                            return;
                        }
                        if (res1)
                            SelectedChampionShip = tem as IChampionShip;
                        IsRaceSong = res1;
                        isRecord &= (!res1);
                        InstanceCreate(new DifficultySelector(songImage, wave, filePath, mode));
                    }
                };

                y++;
                PushSelection(selection);

            }
            base.Start();
        }

        public override void Update()
        {
            RectPos = MathHelper.Lerp(RectPos, RectPosTarget, 0.16f);
            if (IsKeyPressed(InputIdentity.Special) && customsExist && FightSystem.CurrentSongs == FightSystem.MainGameSongs)
            {
                Dispose();
                InstanceCreate(new FightSelector(mode));
            }
            appearTime++;
            TextSelectionEx.Move();
            if (alpha < 1)
                alpha += 0.025f;
            base.Update();
        }

        public override void Draw()
        {
            if (FightSystem.CurrentSongs == FightSystem.MainGameSongs && customsExist)
                if (appearTime % 60 <= 29 || appearTime >= 180)
                    GlobalResources.Font.NormalFont.CentreDraw("Press C for extras", new Vector2(320, 435), Color.Yellow * alpha, 1, 0.1f);
            GlobalResources.Font.NormalFont.CentreDraw("Select your music", new Vector2(320, 45), Color.White * alpha, 1, 0.1f);
            DrawingLab.DrawRectangle(new CollideRect(new(75, 160), new(10, 134)), Color.White * 0.6f, 2, 1);
            DrawingLab.DrawLine(new(80, 160 + RectPos), new(80, 160 + RectPos + RectHeight), 20, Color.White * 0.9f, 1);
        }

        public override void Dispose()
        {
            FightBox.instance.Dispose();
            FightBox.boxs = new List<FightBox>();
            base.Dispose();
        }

        public override void Reverse()
        {
            FightBox.boxs = new List<FightBox>();
            RectangleBox box = new(new CollideRect(60, 144, 520, 166));
            InstanceCreate(box);

            foreach (var v in backs)
            {
                AddChild(v); v.Reverse();
            }
            camera.Reverse();
            AddChild(camera);

            base.Reverse();
        }
    }

    internal class DifficultySelector : Selector
    {
        private float alpha = 0;
        private int appearTime = 0;
        private readonly IWaveSet wave;
        private readonly GameMode mode;
        private readonly string filePath;

        private static bool InChampionShip =>
            FightSystem.CurrentChampionShip != null &&
            FightSystem.CurrentChampionShip.CheckTime() == ChampionShipStates.Starting;

        private class JudgeSelector : Entity
        {
            public JudgeSelector()
            {
                UpdateIn120 = true;
            }

            public bool Enabled { get; set; } = false;

            private float alpha => (FatherObject as DifficultySelector).alpha;

            private JudgementState judgeState = JudgementState.Balanced;
            public override void Draw()
            {
                GlobalResources.Font.NormalFont.CentreDraw("Judge Type:", new Vector2(530, 412), Color.White, 0.9f, 0.1f);
                Vector2 Position = new(530, 442);
                string JudgeText = "Balanced";
                Color JudgeColor = Color.Yellow;
                string PerPos = "+3.3", PerNeg = "-3.3";

                switch (judgeState)
                {
                    case JudgementState.Strict:
                        JudgeText = "Strict";
                        JudgeColor = Color.Red;
                        PerPos = "+2";
                        PerNeg = "-2";
                        break;
                    case JudgementState.Lenient:
                        JudgeText = "Lenient";
                        JudgeColor = Color.LawnGreen;
                        PerPos = "+4.5";
                        PerNeg = "-4";
                        break;
                }
                GlobalResources.Font.NormalFont.CentreDraw(JudgeText, Position, JudgeColor, 0.9f, 0.1f);
                GlobalResources.Font.NormalFont.CentreDraw($"({PerPos} {PerNeg})", Position + new Vector2(0, 25), JudgeColor, 0.8f, 0.1f);
            }

            public override void Update()
            {
                if (!Enabled)
                {
                    judgeState = JudgementState.Strict;
                    return;
                }
                if (IsKeyPressed120f(InputIdentity.Alternate))
                {
                    PlaySound(Ding);
                    switch (judgeState)
                    {
                        case JudgementState.Strict:
                            judgeState = JudgementState.Balanced;
                            break;
                        case JudgementState.Balanced:
                            judgeState = JudgementState.Lenient;
                            break;
                        case JudgementState.Lenient:
                            judgeState = JudgementState.Strict;
                            break;
                    }
                }
            }
            public JudgementState JudgeState => judgeState;
        }

        private readonly JudgeSelector judgeSelector;
        Texture2D _songIllustration;

        public DifficultySelector(IWaveSet wave)
        {
            this.wave = wave;
            judgeSelector = new JudgeSelector();
            AddChild(judgeSelector);
            int del = SongSelector.IsRaceSong ? 1 : 2;
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp))
                    currentSelect -= del;
                else if (IsKeyPressed120f(InputIdentity.MainDown))
                    currentSelect += del;
                else if (IsKeyPressed120f(InputIdentity.MainRight))
                    currentSelect++;
                else if (IsKeyPressed120f(InputIdentity.MainLeft))
                    currentSelect--;
                if (currentSelect >= SelectionCount) currentSelect -= SelectionCount;
                else if (currentSelect < 0) currentSelect = SelectionCount - 1;
            };
            ResetSelect();
            PlaySound(select, 0.9f);
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };

#if !DEBUG 
            if (InChampionShip)
            {
                string _type;
                if (PlayerManager.CurrentUser != null)
                {
                    _type = PlayerManager.CurrentUser.ChampionShipDiv(FightSystem.CurrentChampionShip.Title);
                }
                else throw new NotImplementedException();
                try
                { 
                    _type.TrimEnd('\n');
                    var panel = SongSelector.SelectedChampionShip.DifficultyPanel;
                    int type;
                    if (panel.ContainsKey("div" + _type))
                        type = (int)panel["div" + _type];
                    else type = (int)panel["div." + _type];
                    this.PushSelection(new TextSelection("Start!", new Vector2(320, 240))
                    {
                        SetSelectionAction = () =>
                        {
                            GameStates.StartSong(this.wave, _songIllustration, this.filePath, type, JudgementState.Strict, this.mode);
                        }
                    });
                    this.divNames = SongSelector.SelectedChampionShip.DifficultyPanel.Keys.ToArray();
                }
                catch
                { 
                    this.Dispose();
                    GameStates.InstanceCreate(new IntroUI());
                    return;
                }
            }
            else
            {
#endif
            if ((mode & GameMode.Buffed) != GameMode.Buffed)
                judgeSelector.Enabled = true;
            if (!SongSelector.IsRaceSong)
            {
                if (this.wave.Attributes == null)
                {
                    PushSelection(new Noob());
                    PushSelection(new Easy());
                    PushSelection(new Normal());
                    PushSelection(new Hard());
                    PushSelection(new Extreme());
                }
                else
                {
                    HashSet<Difficulty> unlocked = this.wave.Attributes.UnlockedDifficulties;
                    if (unlocked.Contains(Difficulty.Noob))
                        PushSelection(new Noob());
                    if (unlocked.Contains(Difficulty.Easy))
                        PushSelection(new Easy());
                    if (unlocked.Contains(Difficulty.Normal))
                        PushSelection(new Normal());
                    if (unlocked.Contains(Difficulty.Hard))
                        PushSelection(new Hard());
                    if (unlocked.Contains(Difficulty.Extreme))
                        PushSelection(new Extreme());
                }
            }
            else
            {
                var current = SongSelector.SelectedChampionShip;
                float height = 0;
                HashSet<Difficulty> unlocked = null;
                if (this.wave.Attributes != null) unlocked = this.wave.Attributes.UnlockedDifficulties;
                foreach (var v in current.DifficultyPanel)
                {
                    if (unlocked != null && !unlocked.Contains(v.Value)) continue;
                    PushSelection(new DivisionInformation(v.Key, height, (int)v.Value));
                    height += 40;
                }
                divNames = current.DifficultyPanel.Keys.ToArray();
            }
            Selected += () =>
            {
                StartSong(this.wave, _songIllustration, filePath, (Selections[currentSelect] as IGetDifficulty).ThisDifficulty, judgeSelector.JudgeState, mode);
            };
#if !DEBUG
            }
#endif
        }

        public DifficultySelector(Texture2D songIllustration, IWaveSet wave, string filePath, GameMode mode) : this(wave)
        {
            _songIllustration = songIllustration;
            this.mode = mode;
            this.filePath = filePath;
        }

        private static Color GetColor(int v)
        {
            return v switch
            {
                0 => Color.White,
                1 => Color.LawnGreen,
                2 => Color.LightBlue,
                3 => Color.MediumPurple,
                4 => Color.Gold,
                5 => Color.Gray,
                _ => throw new NotImplementedException(),
            };
        }

        private interface IGetDifficulty
        {
            int ThisDifficulty { get; }
        }
        private class DivisionInformation : TextSelection, IGetDifficulty
        {
            private readonly bool colorfulDraw = false;
            private readonly int dif;

            public int ThisDifficulty => dif;
            public DivisionInformation(string name, float delta, int difficulty) : base(name, new Vector2(320, 220 + delta))
            {
                dif = difficulty;
                Size = 1.0f; TextColor = GetColor(difficulty);
                if (difficulty == 5)
                {
                    colorfulDraw = true;
                }
            }

            private int appearTime = 0;
            public override void Update()
            {
                appearTime++;
                base.Update();
            }
            public override void Draw()
            {
                base.Draw();
                if (colorfulDraw)
                {
                    for (int i = 0; i < 3; i++)
                        FightResources.Font.NormalFont.CentreDraw(texts, Centre + MathUtil.GetVector2(20, appearTime * 1.4f + i * 120),
                            new Color(DrawingLab.HsvToRgb(i * 120 + appearTime, 125, 125, 255)) * alpha * 0.8f, GetSize, 0.9f);
                }
            }
            public override void SelectionEvent()
            {
                base.SelectionEvent();
            }
        }

        private class Extreme : TextSelection, IGetDifficulty
        {
            public int ThisDifficulty => 4;
            public Extreme() : base("Extreme", new Vector2(320, 280)) { Size = 1.0f; }
        }
        private class Noob : TextSelection, IGetDifficulty
        {
            public int ThisDifficulty => 0;
            public Noob() : base("Noob", new Vector2(200, 160)) { Size = 1.0f; }
        }
        private class Easy : TextSelection, IGetDifficulty
        {
            public int ThisDifficulty => 1;
            public Easy() : base("Easy", new Vector2(440, 160)) { Size = 1.0f; }
        }
        private class Normal : TextSelection, IGetDifficulty
        {
            public int ThisDifficulty => 2;
            public Normal() : base("Normal", new Vector2(200, 220)) { Size = 1.0f; }
        }
        private class Hard : TextSelection, IGetDifficulty
        {
            public int ThisDifficulty => 3;
            public Hard() : base("Hard", new Vector2(440, 220)) { Size = 1.0f; }
        }

        public override void Update()
        {
            try
            {
                appearTime++;
                if (alpha < 1)
                    alpha += 0.025f;
                base.Update();
            }
            catch
            {
                Dispose();
            }
        }

        private static readonly string[] difficultyNames = { "noob", "easy", "normal", "hard", "extreme", "extreme+" };
        private readonly string[] divNames;

        public override void Draw()
        {
            if (!string.IsNullOrEmpty(PlayerManager.currentPlayer))
            {
                string score = "0", curFight = wave.FightName;
                float acc = 0;

                int dif = currentSelect;

                if (SongSelector.IsRaceSong)
                {
                    dif = ChampionShipDifficulty();
                }

                if (PlayerManager.CurrentUser.SongPlayed(curFight))
                {
                    if (PlayerManager.CurrentUser.GetSongData(curFight).CurrentSongStates.ContainsKey((Difficulty)dif))
                    {
                        SongData.SongState info = PlayerManager.CurrentUser.GetSongData(curFight).CurrentSongStates[(Difficulty)dif];
                        score = info.Score.ToString();
                        acc = (float)Math.Round(info.Accuracy * 100, 2);
                        DrawACAP(curFight, dif, info);
                    }
                }
                var f = GlobalResources.Font.NormalFont;
                var Percentage = (acc > 0 ? (acc.ToString() + "%") : "N/A");
                string extraacctxt = $"({Percentage})";
                f.CentreDraw($"Max score: {score + extraacctxt}", new Vector2(320, 381), Color.White * alpha);
                f.CentreDraw("Select difficulty", new Vector2(320, 45), Color.White * alpha);

                DrawInformation();
            }

            if (_songIllustration != null)
            {
                FormalDraw(_songIllustration, new(320, 240), Color.White * 0.3f, 0, new(320, 240));
            }
            base.Draw();
        }

        private void DrawInformation()
        {
            if (!SongSelector.IsRaceSong)
            {
                GlobalResources.Font.NormalFont.CentreDraw(wave.FightName + " " + difficultyNames[currentSelect], new Vector2(320, 345), Color.White * alpha);
            }
            else
            {
                if (FightSystem.CurrentChampionShip != null && FightSystem.CurrentChampionShip.CheckTime() == ChampionShipStates.Starting)
                {
                    string text = "";
                    if (PlayerManager.CurrentUser != null)
                    {
                        text = PlayerManager.CurrentUser.ChampionShipDiv(FightSystem.CurrentChampionShip.Title);
                    }
                    GlobalResources.Font.NormalFont.CentreDraw(wave.FightName + " div." + text, new Vector2(320, 345), Color.White * alpha);
                }
                else
                    GlobalResources.Font.NormalFont.CentreDraw(wave.FightName + " " + divNames[currentSelect], new Vector2(320, 345), Color.White * alpha);
            }
        }

        private int ChampionShipDifficulty()
        {
            int dif;
            string _type;
            if (PlayerManager.CurrentUser != null)
            {
                if (FightSystem.CurrentChampionShip != null && FightSystem.CurrentChampionShip.CheckTime() == ChampionShipStates.Starting)
                    _type = PlayerManager.CurrentUser.ChampionShipDiv(FightSystem.CurrentChampionShip.Title);
                else
                    return (Selections[currentSelect] as IGetDifficulty).ThisDifficulty;
            }
            else throw new NotImplementedException();

            var panel = SongSelector.SelectedChampionShip.DifficultyPanel;

            var divText = "div";
            if (panel.ContainsKey("div" + _type))
                divText += ".";
            dif = (int)panel[divText + _type];
            return dif;
        }

        private Color MarkColor(SkillMark mark)
        {
            return mark switch
            {
                SkillMark.Impeccable => Color.Goldenrod,
                SkillMark.Eminent => Color.OrangeRed,
                SkillMark.Excellent => Color.MediumPurple,
                SkillMark.Respectable => Color.LightSkyBlue,
                SkillMark.Acceptable => Color.SpringGreen,
                SkillMark.Ordinary => Color.Transparent,
                SkillMark.Failed => Color.Transparent,
                _ => Color.White
            };
        }

        private void DrawACAP(string curFight, int dif, SongData.SongState info)
        {
            if (info.AP)
            {
                var TextY = 417 + Sin(appearTime) * 10;
                var Angle = Sin(appearTime * 0.07f) * 0.25f;
                GlobalResources.Font.NormalFont.CentreDraw("All Perfect", new Vector2(320, TextY), Color.Gold * alpha, 1.0f, Angle, 0.1f);
                for (int i = 0; i < 4; i++)
                {
                    GlobalResources.Font.NormalFont.CentreDraw("All Perfect",
                        new Vector2(320, TextY) + MathUtil.GetVector2(Cos(appearTime) * 12, appearTime * 1.0f + i * 90),
                        new Color(DrawingLab.HsvToRgb(appearTime * 1.3f + i * 60, 255, 255, 255)) * alpha * 0.75f, 1.0f, Angle, 0.1f);
                }
            }
            else if (info.AC)
            {
                GlobalResources.Font.NormalFont.CentreDraw(
                    "No Hit",
                    new Vector2(320, 417 + Sin(appearTime) * 10),
                    MarkColor((SkillMark)Convert.ToInt32(info.Mark)) * alpha,
                    GameMain.MissionSpriteBatch
                );
            }
        }
    }
}