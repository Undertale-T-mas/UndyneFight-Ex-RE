using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Rhythm_Recall
{
    public static class Resources
    {
        private static readonly char[] _loads = { 'A', 'C', 'E', 'H', 'L', 'M', 'R', 'T', 'Y' };
        public static void Initialize(ContentManager cm)
        {
            //   UndyneFight_Ex.ChampionShips.LicenseMaker.GetScore(Waves.Memory2022.GetChampionShip);
            foreach (char ch in _loads)
            {
                charTextures[ch] = cm.Load<Texture2D>("FontTexture\\" + ch + "\\" + ch);
            }

            block = cm.Load<Texture2D>("Sprites\\Blocks\\block");
            blockTmas = cm.Load<Texture2D>("Sprites\\Blocks\\blockTmas");
            blockOtokp = cm.Load<Texture2D>("Sprites\\Blocks\\blockOtokp");
            blockTail = cm.Load<Texture2D>("Sprites\\Blocks\\blockMini");
            BackGround = cm.Load<Texture2D>("FontTexture\\BackGround");

            Championships.Memory2022.Flash0 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash0");
            Championships.Memory2022.Flash1 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash1");
            Championships.Memory2022.Flash2 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash2");
            Championships.Memory2022.Flash3 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash3");
            Championships.Memory2022.Flash4 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash4");
            Championships.Memory2022.Flash5 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash5");
            //  Championships.Memory2022.Flash1 = cm.Load<Texture2D>("ChampionShips\\Memory2022\\Flash1");

            TKSpecial.AprilFools2023.FlanHand = cm.Load<Texture2D>("TK2023\\FlanHand");

            rander = new Random();
        }
        static Random rander;
        public static Dictionary<char, Texture2D> charTextures = new Dictionary<char, Texture2D>();
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