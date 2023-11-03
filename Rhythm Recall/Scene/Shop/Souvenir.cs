using UndyneFight_Ex.UserService;

namespace Rhythm_Recall
{
    internal class Souvenir : StoreItem
    {
        public Souvenir(string name, string fullName, string description, ItemRarity rarity) : base(name, fullName, false, description, rarity, 1) { }

        public override bool InShop => false;
    }
}