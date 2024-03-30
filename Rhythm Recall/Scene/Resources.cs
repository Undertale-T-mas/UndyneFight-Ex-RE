using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.UserService;
using static Rhythm_Recall.Resources.Championships;
using static UndyneFight_Ex.BSet;
using static UndyneFight_Ex.GameInterface.GameStartUp;

namespace Rhythm_Recall
{
    public static class Resources
    {
        private static readonly char[] _loads = { 'A', 'C', 'E', 'H', 'L', 'M', 'R', 'T', 'Y' };
        public static void Initialize(ContentManager cm)
        {
            //   UndyneFight_Ex.ChampionShips.LicenseMaker.GetScore(Waves.Memory2022.GetChampionShip);
            BadAppleRE.Final = new("Musics\\BadAppleRE\\Font\\Final", cm);
            BadAppleRE.Final = new("Musics\\BadAppleRE\\Font\\BadAppleFont", cm);
            BadAppleRE.inyou = cm.Load<Texture2D>("Musics\\BadAppleRE\\inyou");
            BadAppleRE.bubble = cm.Load<Texture2D>("Musics\\BadAppleRE\\bubble");
            foreach (char ch in _loads)
            {
                charTextures[ch] = cm.Load<Texture2D>($"FontTexture\\{ch}\\{ch}");
            }
            for (int i = 0; i < BadAppleRE.BreakScreen.Length; i++)
            {
                string st = "0";
                char[] s = $"{i}".ToCharArray();
                for (int i2 = 0; i2 < 4 - s.Length; i2++) st += "0";
                st += $"{i}";
                BadAppleRE.BreakScreen[i] = cm.Load<Texture2D>($"Musics\\BadAppleRE\\BreakScreen\\BlackMove_{st}");
            }
            List<int> temp = new();
            for (int i = 0; i < 835; i++) temp.Add(i);
            foreach (var t in temp)
            {
                string st = "";
                if (t < 10)
                {
                    st = "0000" + t.ToString();
                }
                else if (t < 100)
                {
                    st = "000" + t.ToString();
                }
                else
                {
                    st = "00" + t.ToString();
                }
                BadAppleRE.anomalyVideo[t] = cm.Load<Texture2D>($"Musics\\BadAppleRE\\anomaly\\Screen_{st}");
            }
            block = cm.Load<Texture2D>("Sprites\\Blocks\\block");
            blockTmas = cm.Load<Texture2D>("Sprites\\Blocks\\blockTmas");
            blockOtokp = cm.Load<Texture2D>("Sprites\\Blocks\\blockOtokp");
            blockTail = cm.Load<Texture2D>("Sprites\\Blocks\\blockMini");
            BackGround = cm.Load<Texture2D>("FontTexture\\BackGround");

            Memory2022.Flash0 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash0");
            Memory2022.Flash1 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash1");
            Memory2022.Flash2 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash2");
            Memory2022.Flash3 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash3");
            Memory2022.Flash4 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash4");
            Memory2022.Flash5 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash5");
            //Memory2022.Flash1 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash1");

            TKSpecial.AprilFools2023.FlanHand = cm.Load<Texture2D>("TK2023\\FlanHand");

            rander = new Random();

            SouvenirLoad(cm);
        }

        private static void SouvenirLoad(ContentManager cm)
        {
            cm.RootDirectory = "Content\\Shop";
            PushStoreItem(
                new Souvenir("Gold Trophy", "2023MEMTroGol", "The gold trophy of Memory-2023. It symbolizes your strength.", ItemRarity.Legendary)
                {
                    Image = cm.Load<Texture2D>("Souvenir\\MemoryGold")
                }); ;
            PushStoreItem(
                new Souvenir("Silver Trophy", "2023MEMTroSil", "The silver trophy of Memory-2023. It symbolizes your strength.", ItemRarity.Epic)
                {
                    Image = cm.Load<Texture2D>("Souvenir\\MemorySilver")
                }); ;
            PushStoreItem(
                new Souvenir("Bronze Trophy", "2023MEMTroBrz", "The bronze trophy of Memory-2023. It symbolizes your strength.", ItemRarity.Epic)
                {
                    Image = cm.Load<Texture2D>("Souvenir\\MemoryBronze")
                }); ;
        }
        public static class BadAppleRE
        {
            public static Texture2D[] BreakScreen = new Texture2D[106];
            public static Texture2D[] anomalyVideo = new Texture2D[835];
            public static GLFont Final;
            public static GLFont BadAppleFont;
            public static Texture2D inyou;
            public static Texture2D bubble;
        }
        static Random rander;
        public static Dictionary<char, Texture2D> charTextures = new();
        private static Texture2D blockTail, block, blockTmas, blockOtokp;
        public static Texture2D BlockTexture
        {
            get
            {
                double v = rander.NextDouble();
                return v < 0.9 ? block : v < 0.95 ? blockTmas : blockOtokp;
            }
        }
        public static Texture2D BlockTail => blockTail;

        public static Texture2D BackGround { get; internal set; }
        public static class Championships
        {
            public static class Memory2022
            {
                public static Texture2D Flash0 { get; internal set; }
                public static Texture2D Flash1 { get; internal set; }
                public static Texture2D Flash2 { get; internal set; }
                public static Texture2D Flash3 { get; internal set; }
                public static Texture2D Flash4 { get; internal set; }
                public static Texture2D Flash5 { get; internal set; }
            }
        }
        public static class TKSpecial
        {
            public static class AprilFools2023
            {
                public static Texture2D FlanHand { get; internal set; }
            }
        }
    }
}