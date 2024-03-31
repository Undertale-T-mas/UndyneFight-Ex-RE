using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Remake.UI;
using UndyneFight_Ex.Remake.Effects;
using UndyneFight_Ex.Remake.Texts;
using static UndyneFight_Ex.Remake.Resources.UI;
using static UndyneFight_Ex.Remake.Resources.Musics;
using static UndyneFight_Ex.Remake.Resources.Font;
using static UndyneFight_Ex.Remake.Resources.Sounds;
using static UndyneFight_Ex.Remake.Resources.FightSprites;
using static UndyneFight_Ex.FightResources.Font;

namespace UndyneFight_Ex.Remake
{
    public static class Resources
    {
        internal static void Initialize(ContentManager loader)
        {
            loader.RootDirectory = "Content\\ReEngine";
            Cursor = loader.Load<Texture2D>("Mouse\\cursor");
            Start = loader.Load<Texture2D>("UI\\start");
            Tick = loader.Load<Texture2D>("UI\\tick");
            ScrollArrow = loader.Load<Texture2D>("UI\\scrollArrow");
            Gear = loader.Load<Texture2D>("UI\\gear");
            Mail = loader.Load<Texture2D>("UI\\mail");
            IntroStart = loader.Load<Texture2D>("UI\\introStart");
            IntroAccount = loader.Load<Texture2D>("UI\\account");
            IntroSetting = loader.Load<Texture2D>("UI\\introSetting");

            DreamDiver_INTRO = new("ReEngine\\Musics\\Dream diver_INTRO.ogg");
            DreamDiver_LOOP = new("ReEngine\\Musics\\Dream diver_LOOP.ogg");

            Normal = new GLFont("Font\\chinese", loader);

            YellowShoot = loader.Load<SoundEffect>("Sounds\\shoot");
            TargetBurst = loader.Load<SoundEffect>("Sounds\\objBurst");
            Bomb = loader.Load<SoundEffect>("Sounds\\bomb");

            SoulShoot = loader.Load<Texture2D>("FightSprites\\soulBullet");
            MettBlockA = loader.Load<Texture2D>("FightSprites\\Mettaton\\blockA");
            MettBlockB = loader.Load<Texture2D>("FightSprites\\Mettaton\\blockB");
            MettBullet = loader.Load<Texture2D>("FightSprites\\Mettaton\\bullet");
            Spider = loader.Load<Texture2D>("FightSprites\\spider");

            for (int i = 0; i < 18; i++)
            {
                ParasolMett[i] = loader.Load<Texture2D>($"FightSprites\\Mettaton\\spr_parasolmett_{i}");
                if (i < 7)
                {
                    MettBombCoreBlast[i] = loader.Load<Texture2D>($"FightSprites\\Mettaton\\spr_plusbomb_coreblast_{i}");
                    MettBombHorBlast[i] = loader.Load<Texture2D>($"FightSprites\\Mettaton\\spr_plusbomb_horblast_{i}");
                    MettBombVerBlast[i] = loader.Load<Texture2D>($"FightSprites\\Mettaton\\spr_plusbomb_verblast_{i}");
                    if (i < 2)
                    {
                        MettBomb[i] = loader.Load<Texture2D>($"FightSprites\\Mettaton\\spr_plusbomb_{i}");
                        Fireball[i] = loader.Load<Texture2D>($"FightSprites\\FireBall\\spr_{i}");
                    }
                }
            }

            MainLoader = loader; UIShaders.Load(loader);

            SelectUI.Initialize();
            FontPatched.Initialize();

            MouseSystem.Initialize();
        }
        public static ContentManager MainLoader { get; private set; }
        
        public static class FontPatched { 
            public static SmartFont Default { get; private set; }

            public static void Initialize()
            {
                Default = new SmartFont();
                Default.Insert(Normal, 0.0f);
                Default.Insert(Japanese, 0.5f);
                Default.Insert(NormalFont, 1.0f);
            }
        }

        public static class UI
        {
            public static Texture2D Cursor { get; set; }
            public static Texture2D Start { get; set; }
            public static Texture2D Tick { get; set; }
            public static Texture2D ScrollArrow { get; set; }
            public static Texture2D Gear { get; set; }
            public static Texture2D Mail { get; internal set; }
            public static Texture2D IntroStart { get; internal set; }
            public static Texture2D IntroAccount { get; internal set; }
            public static Texture2D IntroSetting { get; internal set; }
        }
        public static class Musics
        {
            public static Audio DreamDiver_INTRO { get; set; }
            public static Audio DreamDiver_LOOP { get; set; }
        }
        public static class Sounds
        {
            public static SoundEffect YellowShoot { get; set; }
            public static SoundEffect TargetBurst { get; set; }
            public static SoundEffect Bomb { get; internal set; }
        }
        public static class FightSprites
        {
            public static Texture2D[] Fireball { get; private set; } = new Texture2D[2];

            public static Texture2D Spider { get; set; }
            public static Texture2D SoulShoot { get; set; }
            public static Texture2D MettBlockA { get; set; }
            public static Texture2D MettBlockB { get; set; }
            public static Texture2D[] ParasolMett { get; private set; } = new Texture2D[18];
            public static Texture2D[] MettBomb { get; private set; } = new Texture2D[2];
            public static Texture2D[] MettBombCoreBlast { get; private set; } = new Texture2D[7];
            public static Texture2D[] MettBombVerBlast { get; private set; } = new Texture2D[7];
            public static Texture2D[] MettBombHorBlast { get; private set; } = new Texture2D[7];
            public static Texture2D MettBullet { get; internal set; }
        }
        public static class Font
        {
            public static GLFont Normal { get; set; }
        }
        public static class UIShaders
        {
            public static void Load(ContentManager loader)
            {
                string _path = loader.RootDirectory;

                loader.RootDirectory = "Content\\ReEngine\\Effects\\NonFight";

                Background = new(loader.Load<Effect>("BackGround"));

                loader.RootDirectory = _path;
            }

            public static BackgroundShader Background { get; set; }

        }
    }
}