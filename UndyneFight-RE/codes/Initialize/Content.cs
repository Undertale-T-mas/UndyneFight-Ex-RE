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
        }
        public static class Font
        {
            public static GLFont Normal { get; set; }
        }
    }
}