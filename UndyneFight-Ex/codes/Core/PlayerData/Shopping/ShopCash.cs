using System.Collections.Generic;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public partial class ShopData
    {
        public class ShopCash : ISaveLoad
        {
            public int Coins { get; set; }
            public int Energy { get; set; }
            public int Resonance { get; set; }
            public List<ISaveLoad> Children => null;

            public void Load(SaveInfo info)
            {
                if (info.Nexts.ContainsKey("Coins"))
                    Coins = info.Nexts["Coins"].IntValue;
                if (info.Nexts.ContainsKey("Energy"))
                    Energy = info.Nexts["Energy"].IntValue;
                if (info.Nexts.ContainsKey("Resonance"))
                    Resonance = info.Nexts["Resonance"].IntValue;

            }

            public SaveInfo Save()
            {
                SaveInfo info = new("ShopCash{");
                info.PushNext(new SaveInfo("Coins:value=" + Coins));
                info.PushNext(new SaveInfo("Energy:value=" + Energy));
                info.PushNext(new SaveInfo("Resonance:value=" + Resonance));
                return info;
            }
        }
    }
}