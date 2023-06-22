using System.Collections.Generic;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public partial class ShopData : ISaveLoad
    {
        public ShopData()
        {
            CashManager = new();
            StoreManager = new();
        }

        public List<ISaveLoad> Children => null;

        public ShopCash CashManager { get; private set; }
        public StoreData StoreManager { get; private set; }

        public void Load(SaveInfo info)
        {
            if (!info.Nexts.ContainsKey("ShopCash"))
                info.PushNext(new SaveInfo("ShopCash{"));
            if (!info.Nexts.ContainsKey("StoreData"))
                info.PushNext(new SaveInfo("StoreData{"));
            CashManager.Load(info.Nexts["ShopCash"]);
            StoreManager.Load(info.Nexts["StoreData"]);
        }

        public SaveInfo Save()
        {
            SaveInfo result = new("ShopData{");
            result.PushNext(CashManager.Save());
            result.PushNext(StoreManager.Save());
            return result;
        }
    }
}