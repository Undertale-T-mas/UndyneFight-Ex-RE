using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        partial class SongSelector
        { 
            private class SortOrder : AlternateButton
            {
                public SortOrder(SongSelector father) : base(father, new Vector2(801, 641),
                    "", "Normal order", "Reverse order")
                {
                    this._father = father; 
                    this.DefaultScale = 1.25f;
                    this.TipDistance = 50f;
                    this.LeftClick += SortOrder_LeftClick;
                }

                public bool IsReverse { get; private set; } = false;

                public override void Start()
                { 
                }
                 
                private void SortOrder_LeftClick()
                {
                    bool res = this.Result == "Reverse order";
                    this.IsReverse = res;
                    this._father.OrderChanged();
                } 

                new SongSelector _father;

                public override void Update()
                {
                    bool temp = this.Visible = (_father._virtualFather.SongSelected == null) && (_father._currentSongList is DiffMode);
                    if (temp)
                        base.Update();
                }
            }
        }
    }
}
