using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Entities
{
    public interface ISelectAble
    {
        void Selected();
        void DeSelected();
        void SelectionEvent();
    }

    internal class OKCancelSelector : Selector
    {
        public OKCancelSelector() : base()
        {
            AutoDispose = false;
            IsCancelAvailable = false;
        }

        public override void Update()
        {
            if (playTick == 3)
            {
                PushSelection(new OK(this));
                PushSelection(new Cancel(this));
            }
            base.Update();
        }

        public event Func<bool> OKAction;

        private class OK : TextSelection
        {
            public OK(OKCancelSelector selector) : base("OK", new Vector2(320, 330))
            {
                Size = 1.0f;
                this.selector = selector;
            }

            private readonly OKCancelSelector selector;
            private bool correctOperation = true;

            public override void SelectionEvent()
            {
                if (selector.OKAction != null)
                    correctOperation = selector.OKAction.Invoke();
                if (correctOperation)
                {
                    selector.Dispose();
                    base.SelectionEvent();
                }
            }
        }

        private class Cancel : TextSelection
        {
            public Cancel(OKCancelSelector selector) : base("Cancel", new Vector2(320, 380))
            {
                Size = 1.0f;
                this.selector = selector;
            }

            private readonly OKCancelSelector selector;

            public override void SelectionEvent()
            {
                Back();
                base.SelectionEvent();
            }
        }
    }

    internal class Selector : Entity
    {
        public Selector() : this(true)
        {
            UpdateIn120 = true;
        }
        public Selector(bool enableMemory)
        {
            EnabledMemory = enableMemory;
            if (EnabledMemory)
            {
                Last = current;
                current = this;
            }
            UpdateIn120 = true;
        }

        protected bool EnabledMemory { get; set; } = true;

        private static Selector current
        {
            get => GameStates.CurrentScene.BaseSelector;
            set => GameStates.CurrentScene.BaseSelector = value;
        }
        protected Selector Last { get; set; }

        /// <summary>
        /// 将选择设置成0项。
        /// </summary>
        public void ResetSelect()
        {
            isReseted = true;
        }

        protected List<ISelectAble> Selections { get; private set; } = new List<ISelectAble>();
        public int currentSelect = 0;
        public int SelectionCount => Selections.Count;
        private int lastSelect = -1;
        private bool isReseted = false;
        /// <summary>
        /// 是否在选择选项之后自动关闭
        /// </summary>
        protected bool AutoDispose { get; set; } = true;
        protected int playTick = 0;

        /// <summary>
        /// 是否可以按X退出
        /// </summary>
        public bool IsCancelAvailable { get; set; } = true;

        public delegate void ChangeSelect();

        public event ChangeSelect SelectChanger;
        public event Action SelectChanged;
        public event Action Selected;

        public override void Draw()
        {
        }

        public override void Update()
        {
            if (Selections.Count == 0) goto A;
            if (isReseted)
            {
                if (Selections[0] is ISelectAble)
                {
                    if (lastSelect != -1)
                        Selections[lastSelect].DeSelected();
                    Selections[0].Selected();
                    currentSelect = lastSelect = 0;
                }
                isReseted = false;
            }
            playTick++;
            SelectChanger();
            if (lastSelect != currentSelect)
            {
                SelectChanged?.Invoke();
                ISelectAble selection = Selections[currentSelect];
                selection.Selected();
                if (lastSelect >= 0)
                {
                    Selections[lastSelect].DeSelected();
                }
                lastSelect = currentSelect;
            }
        A: if (SelectionCount > 0 && playTick >= 2 && GameStates.IsKeyPressed120f(InputIdentity.Confirm))
            {
                if (AutoDispose)
                    Dispose();
                Selections[lastSelect].SelectionEvent();
                Selected?.Invoke();
            }
            else if (IsCancelAvailable && GameStates.IsKeyPressed120f(InputIdentity.Cancel))
            {
                Back();
            }
        }

        public void PushSelection(ISelectAble Selection)
        {
            if (Selection is GameObject)
                AddChild(Selection as GameObject);
            Selections.Add(Selection);
        }

        protected void InstanceSelect(int p)
        {
            Selections[p].SelectionEvent();
            Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// 退回上一级选项卡
        /// </summary>
        public static void Back()
        {
            if (current == null) return;
            Selector last = current.Last;
            if (last != null)
            {
                GameStates.InstanceCreate(last);
                current.Dispose();
                last.Reverse();
                current = last;
            }
            else
            {
                current = null;
                GameStates.ResetScene(new GameMenuScene()); //already is the root 
            }
        }
        public override void Reverse()
        {
            Selections.ForEach(s =>
            {
                if (s is GameObject) AddChild(s as GameObject);
            });
            base.Reverse();
        }
        public static void BackToRoot()
        {
            Selector last = current.Last;
            if (last == null) return; //already on the root.
            while (last.Last != null)
            {
                last = last.Last; //recursion to the root.
            }
            last.Reverse();
            current.Dispose();
            current = last;
            GameStates.InstanceCreate(last);
        }
    }

    public class TextSelection : Entity, ISelectAble
    {
        public class ShinyTextEffect : Entity
        {
            private readonly string texts;
            private float alpha = 0.0f, size;
            private Color showingColor;

            public ShinyTextEffect(TextSelection s)
            {
                Centre = s.Centre;
                size = s.size * s.currentSize;
                alpha = s.alpha;
                texts = s.texts;
                showingColor = Color.Lerp(s.showingColor, Color.Gold, 0.5f);
            }
            public override void Draw()
            {
                FightResources.Font.NormalFont.CentreDraw(texts, Centre, showingColor * alpha, size, 0.9f);
            }

            public override void Update()
            {
                collidingBox.Y -= 0.1f + (alpha * 0.4f);
                alpha *= 0.9f;
                alpha -= 0.03f;
                size += (((2 - alpha) / 40f) + 0.04f) / 1.6f;
                if (alpha < 0) Dispose();
            }
        }

        public TextSelection(string texts, Vector2 Centre)
        {
            this.texts = texts;
            this.Centre = Centre;
        }
        protected string texts;
        public string subText { private get; set; }
        protected float alpha = 0.0f;
        private float currentSize = 1.0f, maxSize = 1.35f;
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
        public Color TextColor
        {
            set
            {
                showingColor = value;
            }
            get
            {
                return showingColor;
            }
        }
        private float size = 0.8f;
        protected float GetSize => size * currentSize;
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
            var FinalText = texts;
            if (!string.IsNullOrEmpty(subText))
                FinalText += ":" + subText;
            FightResources.Font.NormalFont.CentreDraw(FinalText, Centre, showingColor * alpha, currentSize * size, 0.9f);

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
                ? (currentSize * (1 - sizeChangeSpeed)) + (maxSize * sizeChangeSpeed)
                : (currentSize * (1 - sizeChangeSpeed)) + sizeChangeSpeed;
        }

        public virtual void SelectionEvent()
        {
            action?.Invoke();
            GameStates.InstanceCreate(new ShinyTextEffect(this));
        }
    }
}