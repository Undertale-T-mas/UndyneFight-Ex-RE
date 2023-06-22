using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static UndyneFight_Ex.Entities.StartingShower;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Sprites;

namespace UndyneFight_Ex.Entities
{
    internal static class StartingShower
    {
        public static Type TitleSetUp;
        public static Type TitleShower;
    }
    internal class IntroUI : Selector
    {
        private readonly float[] texturesAlpha = { 1.0f, 1.0f, 0.5f, 0.5f, 0.5f, 0.5f };

        private static bool hasCreated = false;
        private const int selectionCount = 6;
        private const int availableCount = 6;

        private readonly Texture2D[] selectionTextures = new Texture2D[selectionCount];
        private readonly string[] selectionIntroduction = new string[selectionCount];
        private readonly Color[] selectionBack = new Color[selectionCount];

        private Entity UpdateingEntity;

        private float alpha = 0;
        private int appearTime = 110;

        public static RatingShowing RatingShowing { get; private set; }

        public override void Start()
        {
            InstanceCreate(RatingShowing = new RatingShowing());
            base.Start();
        }

        public void CreateBackground()
        {
            if (hasCreated)
            {
                if (TitleShower != null)
                    UpdateingEntity = Activator.CreateInstance(TitleShower) as Entity;
                appearTime = 1;
            }
            else
            {
                if (TitleSetUp != null)
                    UpdateingEntity = Activator.CreateInstance(TitleSetUp) as Entity;
                hasCreated = true;
            }
            if (UpdateingEntity != null) AddChild(UpdateingEntity);
        }
        public IntroUI()
        {
            CreateBackground();
            AutoDispose = false;
            IsCancelAvailable = false;

            ImageCentre = new Vector2(72, 72);
            selectionTextures[0] = login;
            selectionTextures[1] = mainGame;
            selectionTextures[5] = achieveMents;
            selectionTextures[4] = options;
            selectionTextures[3] = championShip;
            selectionTextures[2] = record;
            selectionBack[0] = Color.Lime;
            selectionBack[1] = Color.Red;
            selectionBack[5] = Color.Pink;
            selectionBack[4] = Color.Silver;
            selectionBack[3] = Color.Gold;
            selectionBack[2] = Color.LightSkyBlue;
            selectionIntroduction[0] = "Login";
            selectionIntroduction[1] = "Start game";
            selectionIntroduction[5] = "My achievements";
            selectionIntroduction[4] = "Options";
            selectionIntroduction[3] = "Championships";
            selectionIntroduction[2] = "Records";

            if (PlayerManager.CurrentUser != null) currentSelect = 1;

            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp) || IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    currentSelect--;
                }
                else if (IsKeyPressed120f(InputIdentity.MainDown) || IsKeyPressed120f(InputIdentity.MainRight))
                {
                    currentSelect++;
                }
                if (currentSelect >= SelectionCount) currentSelect -= SelectionCount;
                else if (currentSelect < 0) currentSelect = SelectionCount - 1;
            };
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };
            PushSelection(new SelectionEntities.User(this));
            PushSelection(new SelectionEntities.MainGame(this));

            if (GameInterface.ClassicalGUI.MainMenuSettings.RecordEnabled)
                PushSelection(new SelectionEntities.Record(this));

            PushSelection(new SelectionEntities.Championship(this));
            PushSelection(new SelectionEntities.SettingsIntro(this));

            if (GameInterface.ClassicalGUI.MainMenuSettings.AchievementsEnabled)
                PushSelection(new SelectionEntities.AchievementsIntro(this));
        }

        private static class SelectionEntities
        {
            public class User : Model
            {
                public User(IntroUI ui) : base(ui, 0, (ui) =>
                {
                    if (string.IsNullOrEmpty(PlayerManager.currentPlayer))
                    {
                        InstanceCreate(PlayerManager.playerInfos.Count != 0
                            ? new LoginUI() : new RegisterUI());
                    }
                    else InstanceCreate(new AccountManager());
                    ui.Dispose();
                })
                { }
            }
            public class MainGame : Model
            {
                public MainGame(IntroUI ui) : base(ui, 1, (ui) =>
                {
                    FightSystem.SelectMainSet();
                    InstanceCreate(new ModeSelector());
                    ui.Dispose();
                })
                { }
            }
            public class Record : Model
            {
                public Record(IntroUI ui) : base(ui, 2, (ui) =>
                {
                    int count = System.IO.Directory.GetFiles("Datas\\Records").Length;
                    if (count == 0)
                    {
                        return;
                    }
                    InstanceCreate(new RecordSelector());
                    ui.Dispose();
                })
                { }
            }
            public class Championship : Model
            {
                public Championship(IntroUI ui) : base(ui, 3, (ui) =>
                {
                    InstanceCreate(new ChampionShipSelector());
                    ui.Dispose();
                })
                { }
            }
            public class SettingsIntro : Model
            {
                public SettingsIntro(IntroUI ui) : base(ui, 4, (ui) =>
                {
                    InstanceCreate(new Settings.SettingsShower());
                    ui.Dispose();
                })
                { }
            }
            public class AchievementsIntro : Model
            {
                public AchievementsIntro(IntroUI ui) : base(ui, 5, (ui) =>
                {
                    InstanceCreate(new Achievements.AchievementShower());
                    ui.Dispose();
                })
                { }
            }
            public class Model : Entity, ISelectAble
            {
                private readonly Selector father;
                protected Model(IntroUI sel, int id, Action<Selector> select)
                {
                    Image = sel.selectionTextures[id];
                    father = sel;
                    Centre = new Vector2(320, 304);
                    introduction = sel.selectionIntroduction[id];
                    back = sel.selectionBack[id];
                    this.select = select;
                    UpdateIn120 = true;
                }

                private Color back;
                private readonly Action<Selector> select;
                private readonly string introduction;
                private bool enabled = false;
                private float showingScale = 0;
                public void DeSelected()
                {
                    enabled = false;
                    Update();
                    Depth += 0.02f;
                }

                public override void Draw()
                {
                    if (showingScale <= 0.01f) return;
                    CollideRect area = new(0, 0, Image.Width, Image.Height * showingScale);
                    FormalDraw(Image, Centre, area.ToRectangle(), Color.White, 0, ImageCentre);
                    area.SetCentre(Centre - new Vector2(0, Image.Height * (1 - showingScale) * 0.5f));
                    Depth -= 0.005f;
                    Color c = back * 0.2f;
                    c.A = 135;
                    FormalDraw(FightResources.Sprites.pixiv, area.ToRectangle(), c);
                    Depth += 0.005f;

                    if (enabled)
                    {
                        FightResources.Font.NormalFont.CentreDraw(introduction, new(320, 410), Color.Lerp(Color.White, back, 0.2f));
                    }
                }

                public void Selected()
                {
                    enabled = true;
                    showingScale = 1.0f;
                    Update();
                    Depth -= 0.02f;
                }

                public void SelectionEvent()
                {
                    select.Invoke(father);
                }

                public override void Update()
                {
                    Depth = 0.6f - showingScale * 0.3f - (enabled ? 0.1f : 0);
                    if (!enabled) showingScale *= 0.85f;
                }
            }
        }

        public override void Draw()
        {
            DrawingLab.DrawRectangle(new CollideRect(new Vector2(320 - 145 / 2f, 304 - 145 / 2f), new(145, 145)), Color.White, 3, 0.9f);
            base.Draw();
        }

        public override void Update()
        {
            if (appearTime >= 100f)
            {
                if (alpha < 1)
                    alpha += 0.025f;
            }

            base.Update();
        }
        public override void Reverse()
        {
            CreateBackground();
            base.Reverse();
        }
    }
}