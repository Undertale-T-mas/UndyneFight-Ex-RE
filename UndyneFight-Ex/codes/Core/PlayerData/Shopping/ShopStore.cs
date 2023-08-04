using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public enum ItemRarity
    {
        Ordinary = 1,
        Uncommon = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5
    }
    public abstract class StoreItem
    {
        protected StoreItem(string name, string fullName, bool stackable, string description, ItemRarity rarity, int count)
        {
            Name = name;
            FullName = fullName;
            Stackable = stackable;
            Description = description;
            Rarity = rarity;
            Count = count;
        }

        public abstract bool InShop { get; }

        public string Name { get; init; }
        public string FullName { get; init; }
        public bool Stackable { get; init; }

        public string Description { get; init; }
        public ItemRarity Rarity { get; init; }

        public Texture2D Image { get; set; }

        /// <summary>
        /// count of the item you have if it's stackable
        /// </summary>
        public int Count { get; set; }

        public virtual void Selected()
        {

        }
        public virtual void DeSelected()
        {

        }
        public virtual void Used()
        {

        }
    }
    public class StoreData : ISaveLoad
    {
        private static Dictionary<string, StoreItem> allItems = new();
        public static Dictionary<string, StoreItem> AllItems => allItems;

        public List<ISaveLoad> Children => null;

        public Dictionary<string, StoreItem> userItems = new();

        internal static void AddToItemList(StoreItem item)
        {
            allItems.Add(item.FullName, item);
        }
        public static void StoreItemLoad()
        {

        }
        public bool ConsumeItem(string itemName, int count)
        {
            if (!userItems.ContainsKey(itemName)) return false;
            StoreItem item = userItems[itemName];
            if (item.Count < count) return false;
            item.Count -= count;
            return true;
        }

        public void Load(SaveInfo info)
        {
            foreach (SaveInfo itemInfo in info.Nexts.Values)
            {
                StoreItem item = allItems[itemInfo.Title];
                userItems.Add(item.FullName, item);
                item.Count = itemInfo.IntValue;
            }
        }

        public SaveInfo Save()
        {
            SaveInfo result = new("StoreData{");

            foreach (StoreItem item in userItems.Values)
                result.PushNext(new SaveInfo(item.FullName + ":value=" + item.Count));

            return result;
        }
    }
}