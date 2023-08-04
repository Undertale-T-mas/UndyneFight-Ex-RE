using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.UserService;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall
{
    internal class Souvenir : StoreItem
    {
        public Souvenir(string name, string fullName, string description, ItemRarity rarity) : base(name, fullName, false, description, rarity, 1)
        {
        }

        public override bool InShop => false;
    }
}