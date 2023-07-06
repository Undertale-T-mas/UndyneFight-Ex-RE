using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake
{
    public partial class Souls
    {
        public static Player.MoveState YellowSoul { get; private set; } = new(Color.Yellow, (s) => {
            SoulMove(s);
            if (GameStates.IsKeyPressed120f(InputIdentity.Confirm)) GameStates.InstanceCreate(new Entities.SoulBullet(s));
        });
    }
}