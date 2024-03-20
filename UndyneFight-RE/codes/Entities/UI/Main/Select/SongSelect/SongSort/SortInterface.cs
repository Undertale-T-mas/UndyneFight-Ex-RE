using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        partial class SongSelector
        { 
            private class SortInterface : AlternateButton
            {
                public SortInterface(SongSelector father) : base(father, new Vector2(797, 551),
                    "Chart sortkey:", "Default", "Clear diff", "Complex diff", "Initial")
                {
                    this._father = father;
                    this.TipScale = 1.2f;
                    this.DefaultScale = 1.35f;
                    this.TipDistance = 50f;
                    this.LeftClick += SortInterface_LeftClick;
                }
                public override void Start()
                {
                    this._lastActivated = _father._currentSongList;
                }

                SongList _lastActivated;
                private void SortInterface_LeftClick()
                {
                    this.TryActivate(this.Result switch
                    {
                        "Default" => _father._packMode,
                        "Clear diff" => _father._diffClearMode,
                        "Complex diff" => _father._diffComplexMode,
                        "Initial" => _father._letterMode,
                    });
                }

                public void TryActivate(SongList list)
                {
                    if(list == _lastActivated) return;
                    this._lastActivated.Deactivate();
                    list.Activate();
                    _father._currentSongList = list;
                    this._lastActivated = list;
                }

                new SongSelector _father;
                public override void Update()
                {
                    bool temp = this.Visible = _father._virtualFather.SongSelected == null ;
                    if (temp)
                        base.Update();
                }
            }
        }
    }
}
