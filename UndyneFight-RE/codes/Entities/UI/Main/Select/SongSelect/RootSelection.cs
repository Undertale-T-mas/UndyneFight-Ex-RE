using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private abstract partial class SongList
            {
                protected class RootSelection : Button
                {
                    public RootSelection(SongList father, Vector2 centre, string text) : base(father, centre, text)
                    {
                        this._father = father;
                        this.SelectedScale = 1.1f;
                        this.CentreDraw = false;
                        this.LeftClick += () => lerpRate = 0f;
                        UpdateIn120 = true;
                    }

                    private new SongList _father;
                    public SongList Father => _father;

                    public RootSelection Last { private get; set; }

                    private float lerpRate = 0.0f;
                    private float maxHeight = 0f;
                    private float currentHeight = 0f;

                    private float sumLast = 0f;
                    public float SumHeight => sumLast + this.currentHeight + 65;
                     
                    public override void Update()
                    {
                        if(Last != null) { this.sumLast = Last.sumLast + Last.currentHeight + 65; }
                        this.PositionDelta = _father._positionDelta + new Vector2(0, sumLast);
                        base.Update();

                        currentHeight = MathHelper.Lerp(currentHeight, State is SelectState.Selected or SelectState.Disabled ? maxHeight : 0, lerpRate);
                        lerpRate = MathHelper.Lerp(lerpRate, 0.19f, 0.06f);
                        if(currentHeight > 2 && currentHeight < maxHeight - 2)
                        {
                            int id = 0;
                            foreach (LeafSelection button in this._leave)
                            {
                                float y = id * 55f + (this.State == SelectState.Selected ? -25f : 75f);
                                if (!button.ModuleSelected)
                                    button.State = y < currentHeight ? SelectState.False : SelectState.Disabled;
                                id++;
                            }
                        }
                    }

                    private List<LeafSelection> _leave = new();
                    public void LinkLeaf(LeafSelection leafSelection)
                    {
                        _leave.Add(leafSelection);
                        maxHeight += 55f;
                    }

                    public override void Draw()
                    {
                        base.Draw();
                        if(!_father.DrawEnabled) { return; }
                        Vector2 pos1 = _centre + this.PositionDelta;
                        pos1.Y += 50;
                        //276, 60
                        DrawingLab.DrawLine(new Vector2(272, pos1.Y), new(246, pos1.Y - 26), 3f, Color.Silver, 0.4f);
                        DrawingLab.DrawLine(new Vector2(272, pos1.Y), new(611, pos1.Y), 3f, Color.Silver, 0.4f);
                        DrawingLab.DrawLine(new Vector2(272, pos1.Y), new(272, pos1.Y + currentHeight), 3f, Color.Silver, 0.4f);
                    }

                    public override void Start()
                    { 
                    }
                }
            }
        }
    }
}
