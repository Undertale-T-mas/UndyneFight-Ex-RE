using System.Linq;
using UFData;
using UndyneFight_Ex.ChampionShips;
using col = Microsoft.Xna.Framework.Color;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class ChampionshipSelector
    {
        private class DivisionSelector : Entity
        {
            ChampionshipInfo _info;
            public DivisionSelector(ChampionshipInfo info) {
                this._info = info; 
            }
            public void LockDivision()
            {
                type = true;
            }
            bool type = false;

            public override void Draw()
            {
                //70, 420
                GLFont font = FightResources.Font.NormalFont;
                /*DrawingLab.DrawLine(new(70, 420), new vec2(70, 330), 3f, col.White, 0.3f);
                DrawingLab.DrawLine(new(70, 360), new vec2(100, 390), 3f, col.White, 0.3f);
                DrawingLab.DrawLine(new(870, 390), new vec2(100, 390), 3f, col.White, 0.3f);*/
                font.Draw(type ? "Current Division" : "Select Division:", new(100, 339), col.White, 1.35f, 0.3f);
            }

            public override void Update()
            { 
            }

            internal string[] GetDivisions(ChampionShip c)
            {
                    return
                    c.DivisionExist == null ?  _info.Divisions.Keys.ToArray() : (
                      from v in _info.Divisions.Keys
                      where c.DivisionExist.Contains(v)
                      select v
                      ).ToArray();
                      
                      
            }
        }
    }
}