using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;
using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static Extends.LineMoveLibrary;
using static UndyneFight_Ex.Entities.Line;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.ComponentModel.Design;
using UndyneFight_Ex.Entities.Advanced;

namespace Rhythm_Recall.Waves
{
    internal static class Monochrome
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Monochrome");

                fightSet.Push(typeof(BadApple_RE));

                return new ChampionShip(fightSet)
                {
                    Title = "",
                    SubTitle = "",
                    EditorName = "",
                    Introduce = "The deepest memory",
                    IconPath = "ChampionShips\\TCS",

                    CheckTime = () =>
                    {
#if DEBUG
                        return ChampionShip.ChampionShipStates.End;
#endif        
                        if (PlayerManager.CurrentUser != null)
                            return PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reBadApple") ? ChampionShip.ChampionShipStates.End : ChampionShip.ChampionShipStates.NotStart;
                        else
                            return ChampionShip.ChampionShipStates.NotStart;
                    }

                };
            }
        }
    }
    
}