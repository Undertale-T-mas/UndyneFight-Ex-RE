using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                    private static string Abbreviation(string origin)
                    {
                        if (origin.Length > 17) return origin[..7] + "..." + origin[^7..];
                        else return origin;
                    }
                    public LeafSelection(RootSelection rootSelection, Vector2 centre, string text) : base(rootSelection.Father, centre, Abbreviation(text))
                    {
                        this.SongName = text;
                        rootSelection.LinkLeaf(this);
                        this.CentreDraw = false;
                        this.SelectedScale = 1.07f; 
                        this.ColorDisabled = Color.Transparent;
                        this.State = SelectState.Disabled;
                        this.Root = rootSelection;

                        this.LeftClick += () => {
                            Root.State = State == SelectState.Selected ? SelectState.Disabled : SelectState.Selected;
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

                        this._lineScale = MathHelper.Lerp(_lineScale, _mouseOn ? 1 : 0, 0.1f);

                        this.PositionDelta = Root.PositionDelta;
                    }

                    float _lineScale = 0.0f;

                    public float DrawingY { get ; private set; }
                    public override void Draw()
                    { 
                        base.Draw();
                        if(!this._father.DrawEnabled) { return; }

                        float y = (_centre + PositionDelta).Y + 42;
                        DrawingY = y - 25;
                        DrawingLab.DrawLine(new Vector2(272, y), new Vector2(MathHelper.Lerp(525, 541, _lineScale), y), 3, _drawingColor * 0.8f, 0.1f);
                    }
                }
            }
        }
    }
}
