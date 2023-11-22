using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    internal class FightSelector : Selector
    {
        private class TextSelectionEx : Entity, ISelectAble
        {
            public static void Reset() { missionDelta = currentDelta = Vector2.Zero; }
            private static Vector2 missionDelta;
            private static Vector2 currentDelta;
            public static void MoveUp()
            {
                missionDelta += new Vector2(0, 52);
            }
            public static void MoveDown()
            {
                missionDelta += new Vector2(0, -52);
            }

            public static void Move()
            {
                currentDelta = currentDelta * 0.8f + missionDelta * 0.2f;
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
            }

            private Vector2 startLocation;
            protected string texts;
            private float alpha = 0.0f;
            private float currentSize = 1.0f;
            private float maxSize = 1.35f;
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
                FightResources.Font.NormalFont.Draw(texts, startLocation + currentDelta - new Vector2(0, size1 * 14 - 14), showingColor * alpha, size1, 0.9f);
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
                Centre = currentDelta + startLocation;
            }

            public virtual void SelectionEvent()
            {
                action?.Invoke();
                InstanceCreate(new ShinyTextEffect(this));
            }
        }

        public static bool IsRaceSong { get; private set; }
        public static SaveInfo RaceInfo { get; private set; }

        private float alpha = 0;

        private int currentPos = 1;

        public FightSelector(GameMode mode)
        {
            FightBox.boxs = new System.Collections.Generic.List<FightBox>();
            RectangleBox box = new(new CollideRect(60, 144, 520, 166));
            InstanceCreate(box);

            TextSelectionEx.Reset();
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp) || IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    currentSelect--;
                    if (currentPos == 3)
                        currentPos--;
                    else if (currentPos == 2 && currentSelect != 0)
                        TextSelectionEx.MoveUp();
                    else if (currentSelect != -1)
                        currentPos--;
                }
                else if (IsKeyPressed120f(InputIdentity.MainDown) || IsKeyPressed120f(InputIdentity.MainRight))
                {
                    currentSelect++;
                    if (currentPos == 1)
                        currentPos++;
                    else if (currentPos == 2 && currentSelect <= SelectionCount - 1)
                        TextSelectionEx.MoveDown();
                    else if (currentSelect != SelectionCount)
                        currentPos++;
                }
                if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                else if (currentSelect < 0) currentSelect = 0;
            };

            ResetSelect();
            PlaySound(select, 0.9f);
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };

            int y = 0;

            DateTime dt = DateTime.Now;

            foreach (var type in FightSystem.MainGameFights.Values)
            {
                //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                object tem = Activator.CreateInstance(type); TextSelectionEx selection;

                if (tem is Fight.IClassicFight)
                {
                    Fight.IClassicFight fight;

                    fight = tem as Fight.IClassicFight;

                    string name = fight.FightName;

                    selection = new TextSelectionEx(name, new Vector2(100, 160 + 52 * y))
                    {
                        SetSelectionAction = () =>
                        {
                            isRecord = false;
                            SelectBattle(fight, mode);
                        }
                    };
                }
                else if (tem is Fight.IExtraOption)
                {
                    Fight.IExtraOption opt;
                    opt = tem as Fight.IExtraOption;
                    string name = opt.OptionName;
                    selection = new TextSelectionEx(name, new Vector2(100, 160 + 52 * y))
                    {
                        SetSelectionAction = () =>
                        {
                            isRecord = false;
                            ResetScene(Activator.CreateInstance(opt.IntroScene) as Scene);
                        }
                    };
                }
                else throw new ArgumentOutOfRangeException(tem.GetType().Name);

                y++;
                PushSelection(selection);

            }
        }

        public override void Update()
        {
            TextSelectionEx.Move();
            if (alpha < 1)
                alpha += 0.025f;
            base.Update();
        }

        public override void Draw()
        {
            GlobalResources.Font.NormalFont.CentreDraw("Select a fight", new Vector2(320, 45), Color.White * alpha);
        }

        public override void Dispose()
        {
            FightBox.instance.Dispose();
            FightBox.boxs = new System.Collections.Generic.List<FightBox>();
            base.Dispose();
        }

        public override void Reverse()
        {
            FightBox.boxs = new System.Collections.Generic.List<FightBox>();
            FightBox box = new RectangleBox(new CollideRect(60, 144, 520, 166));
            InstanceCreate(box);

            base.Reverse();
        }
    }
}