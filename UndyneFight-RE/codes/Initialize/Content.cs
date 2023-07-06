﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Runtime.CompilerServices;
using UndyneFight_Ex.Remake.UI;

namespace UndyneFight_Ex.Remake
{
    public static class Resources
    {
        internal static void Initialize(ContentManager loader)
        {
            loader.RootDirectory = "Content\\ReEngine";
            UI.Cursor = loader.Load<Texture2D>("Mouse\\cursor");
            UI.Start = loader.Load<Texture2D>("UI\\start");
            UI.Tick = loader.Load<Texture2D>("UI\\tick");
            UI.ScrollArrow = loader.Load<Texture2D>("UI\\scrollArrow");

            Musics.DreamDiver_INTRO = new("ReEngine\\Musics\\Dream diver_INTRO.ogg");
            Musics.DreamDiver_LOOP = new("ReEngine\\Musics\\Dream diver_LOOP.ogg");

            Font.Normal = new GLFont("Font\\chinese", loader);

            Sounds.YellowShoot = loader.Load<SoundEffect>("Sounds\\shoot");

            FightSprites.SoulShoot = loader.Load<Texture2D>("FightSprites\\soulBullet");

            for(int i = 0; i < 18; i++)
            {
                FightSprites.ParasolMett[i] = loader.Load<Texture2D>("FightSprites\\Mettaton\\spr_parasolmett_" + i);
            }
            for(int i = 0; i < 2; i++)
            {
                FightSprites.MettBomb[i] = loader.Load<Texture2D>("FightSprites\\Mettaton\\spr_plusbomb_" + i);
            }
            for(int i = 0; i < 7; i++)
            {
                FightSprites.MettBombCoreBlast[i] = loader.Load<Texture2D>("FightSprites\\Mettaton\\spr_plusbomb_coreblast_" + i);
                FightSprites.MettBombHorBlast[i] = loader.Load<Texture2D>("FightSprites\\Mettaton\\spr_plusbomb_horblast_" + i);
                FightSprites.MettBombVerBlast[i] = loader.Load<Texture2D>("FightSprites\\Mettaton\\spr_plusbomb_verblast_" + i);
            }

            MainLoader = loader;

            SelectUI.Initialize();

            MouseSystem.Initialize();
        }
        public static ContentManager MainLoader { get; private set; }
        public static class UI
        {
            public static Texture2D Cursor { get; set; }
            public static Texture2D Start { get; set; }
            public static Texture2D Tick { get; set; }
            public static Texture2D ScrollArrow { get; set; }
        }
        public static class Musics
        {
            public static Audio DreamDiver_INTRO { get; set; }
            public static Audio DreamDiver_LOOP { get; set; }
        }
        public static class Sounds
        {
            public static SoundEffect YellowShoot { get; set; }
        }
        public static class FightSprites
        {
            public static Texture2D SoulShoot { get; set; }
            public static Texture2D[] ParasolMett { get; private set; } = new Texture2D[18];
            public static Texture2D[] MettBomb { get; private set; } = new Texture2D[2];
            public static Texture2D[] MettBombCoreBlast { get; private set; } = new Texture2D[7];
            public static Texture2D[] MettBombVerBlast { get; private set; } = new Texture2D[7];
            public static Texture2D[] MettBombHorBlast { get; private set; } = new Texture2D[7];
        }
        public static class Font
        {
            public static GLFont Normal { get; set; }
        }
    }
}