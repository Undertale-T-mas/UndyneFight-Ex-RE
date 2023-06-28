using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private abstract partial class SongList
            {
                protected class LeafSelection : Button
                {
                    public LeafSelection(RootSelection rootSelection, Vector2 centre, string text) : base(rootSelection.Father, centre, text)
                    {
                        this.SongName = text;
                        rootSelection.LinkLeaf(this);
                        this.CentreDraw = false;
                        this.SelectedScale = 1.1f; 
                        this.ColorDisabled = Color.Transparent;
                        this.State = SelectState.Disabled;
                        this.Root = rootSelection;

                        this.LeftClick += () => {
                            if (this.State == SelectState.Selected)
                            {
                                this.Root.State = SelectState.Disabled;
                            }
                            else this.Root.State = SelectState.Selected;
                        };
                    }

                    public Texture2D Illustration { get; init; }
                    public string SongName { get; init; }
                    public object FightObject { get; init; }
                    public RootSelection Root { get; init; }
                    public bool SongAvailable { get; init; } = true;

                    public override void Start()
                    {
                        base.Start();
                        this.State = SelectState.Disabled;
                        if (!SongAvailable) this.ColorSelected = Color.Red;
                    }
                    public override void Update()
                    {
                        base.Update();

                        this.PositionDelta = Root.PositionDelta;
                    }
                }
            }
        }
    }
}
