using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex.GameInterface;
using static UndyneFight_Ex.GlobalResources.Font;
using static UndyneFight_Ex.GlobalResources.Effects;
using static UndyneFight_Ex.GlobalResources.Sprites;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        internal static Dictionary<T, Q> GetDictionary<T, Q>(IEnumerable<T> ts, IEnumerable<Q> qs)
        {
            List<T> ts1 = new(ts);
            List<Q> qs1 = new(qs);
            T[] ts2 = ts1.ToArray();
            Q[] qs2 = qs1.ToArray();

            KeyValuePair<T, Q>[] res = new KeyValuePair<T, Q>[ts2.Length];
            for (int i = 0; i < ts2.Length; i++)
            {
                res[i] = new KeyValuePair<T, Q>(ts2[i], qs2[i]);
            }

            return new Dictionary<T, Q>(res);
        }
        internal static void Initialize(ContentManager loader)
        {
            loader.RootDirectory = "Content";
            //Content\FontTexture\Title.png
            string root = GameStartUp.LoadingSettings.TitleTextureRoot;
            if (System.IO.File.Exists($"Content\\{root}.xnb"))
            {
                loadingTexture = loader.Load<Texture2D>(root);
            }

            NormalFont =    new GLFont("Sprites\\font\\normal", loader);
            FightFont =     new GLFont("Sprites\\font\\menu", loader);
            SansFont =      new GLFont("Sprites\\font\\sans", loader);
            DamageFont =    new GLFont("Sprites\\font\\DamageShow", loader);
            Japanese =      new GLFont("Sprites\\font\\Japanese", loader);
            //Font.Chinese = new GLFont("Sprites\\font\\Chinese", loader);

            loader.RootDirectory = "Content\\Global";

            //championShip =      loader.Load<Texture2D>("UI\\cup");
            championShip =      loader.Load<Texture2D>("UI\\cup_highres");
            hashtex =           loader.Load<Texture2D>("Shaders\\Effect Library\\hashtex");
            hashtex2 =          loader.Load<Texture2D>("Shaders\\Effect Library\\hashtex2");
            mainGame =          loader.Load<Texture2D>("UI\\maingame");
            achieveMents =      loader.Load<Texture2D>("UI\\stars");
            options =           loader.Load<Texture2D>("UI\\options");
            cursor =            loader.Load<Texture2D>("UI\\PlaceCheck");
            login =             loader.Load<Texture2D>("UI\\login");
            debugArrow =        loader.Load<Texture2D>("UI\\debugArrow");
            record =            loader.Load<Texture2D>("UI\\record");
            medal =             loader.Load<Texture2D>("UI\\medal1");
            starMedal =         loader.Load<Texture2D>("UI\\medal2");
            brimMedal =         loader.Load<Texture2D>("UI\\medal0");
            loadingText =       loader.Load<Texture2D>("Loading\\Loading");
            progressArrow =     loader.Load<Texture2D>("Loading\\ProgressArrow");

            backGroundShader = new Shader(loader.Load<Effect>("Shaders\\BackGroundShader"))
            {
            };
            reduceBlueShader = new Shader(loader.Load<Effect>("Shaders\\reduceBlue"))
            {
                StableEvents = (s) =>
                {
                    s.Parameters["reduceBlueAmount"].SetValue(Settings.SettingsManager.DataLibrary.reduceBlueAmount / 200f);
                }
            }; 

            UsingShader.BackGround = backGroundShader;

            ReadCustomShaders(loader);
            // Fight.Functions.EffectName = "SplitColorDrawing";
            // Fight.Functions.ShaderParam = 0;
            // Fight.Functions.BackGroundColor = Color.White;
        }
        internal static void ReadCustomShaders(ContentManager loader)
        {
            loader.RootDirectory = "Content\\Global\\Shaders\\Effect Library\\";
            CustomShaders.Aurora =      new AuroraShader(loader.Load<Effect>("Aurora")) { };
            CustomShaders.Sinwave =     new Shader(loader.Load<Effect>("Sinwave")) { };
            CustomShaders.ColorBlend =  new ColorBlendShader(loader.Load<Effect>("ColorBlend")) { };
            CustomShaders.NeonLine =    new NeonLineShader(loader.Load<Effect>("NeonLine")) { };
            CustomShaders.Camera =      new CameraShader(loader.Load<Effect>("CameraSurface")) { };
            CustomShaders.Cos1Ball =    new BallShapingShader(loader.Load<Effect>("Cos1Ball")) { };
            CustomShaders.StepSample =  new StepSampleShader(loader.Load<Effect>("StepSample")) { };
            CustomShaders.Scale =       new ScaleShader(loader.Load<Effect>("Scale"));
            CustomShaders.Swirl =       new SwirlShader(loader.Load<Effect>("Swirl"));
            CustomShaders.Blur =        new BlurShader(loader.Load<Effect>("Blur"));
            CustomShaders.FastBlur =    new BlurShader(loader.Load<Effect>("BlurLow"));
            CustomShaders.BlurKawase =    new BlurKawaseShader(loader.Load<Effect>("BlurKawase"));
            CustomShaders.Lens =        new LensShader(loader.Load<Effect>("Lens"));
            CustomShaders.Polar =       new PolarShader(loader.Load<Effect>("Polar"));
            CustomShaders.Gray =        new GrayShader(loader.Load<Effect>("Gray"));
            CustomShaders.Seismic =     new SeismicShader(loader.Load<Effect>("Seismic"));
            CustomShaders.Scatter =     new ScatterShader(loader.Load<Effect>("Scatter"));
            CustomShaders.Mosaic =      new MosaicShader(loader.Load<Effect>("Mosaic"));
            CustomShaders.BlockTile =   new BlockTileShader(loader.Load<Effect>("BlockTile"));

            CustomShaders.Tyndall =     new TyndallShader(loader.Load<Effect>("Tyndall"));
            CustomShaders.Spiral =      new SpiralShader(loader.Load<Effect>("Sprial3D"));
            CustomShaders.Wrong =       new WrongShader(loader.Load<Effect>("Wrong"));
            CustomShaders.Fire =        new FireShader(loader.Load<Effect>("NoiseFire"));
            LoadInternals(loader);
            //Effects.CustomShaders.ShaderTiler = new Effects.ShaderTiler(loader.Load<Effect>("ShaderTiler"));
            //Effects.CustomShaders.Smear = new Effects.Smear(loader.Load<Effect>("Smear"));
        }

        public static partial class Effects
        {
            internal static Shader backGroundShader;
            internal static Shader reduceBlueShader;

            public static class CustomShaders
            {
                public static Shader Sinwave { get; set; }
                public static AuroraShader Aurora { get; set; }
                public static TyndallShader Tyndall { get; set; }
                public static NeonLineShader NeonLine { get; set; }
                public static ColorBlendShader ColorBlend { get; set; }
                public static SpiralShader Spiral { get; set; }
                
                public static BallShapingShader Cos1Ball { get; set; }
                public static ScaleShader Scale { get; set; }
                public static CameraShader Camera { get; set; }
                public static StepSampleShader StepSample { get; set; }
                public static SwirlShader Swirl { get; set; }
                public static BlurShader Blur { get; set; }
                public static BlurShader FastBlur { get; set; }
                public static BlurKawaseShader BlurKawase { get; set; }
                public static LensShader Lens { get; set; }
                public static PolarShader Polar { get; set; }
                public static GrayShader Gray { get; set; }
                public static SeismicShader Seismic { get; set; }
                public static ScatterShader Scatter { get; set; }
                public static ShaderTiler ShaderTiler { get; set; }
                public static Smear Smear { get; set; }
                public static MosaicShader Mosaic { get; set; }
                public static BlockTileShader BlockTile { get; set; }
                public static WrongShader Wrong { get; set; }
                public static FireShader Fire { get; set; }
            }
        }

        internal static class Font
        {
            public static GLFont FightFont { get; internal set; }
            public static GLFont NormalFont { get; internal set; }
            public static GLFont SansFont { get; internal set; }
            public static GLFont DamageFont { get; internal set; }
            public static GLFont Japanese { get; internal set; }
            //public static GLFont Chinese { get; internal set; }
        }

        public static class Sprites
        {
            public static Texture2D cursor;
            public static Texture2D login;
            public static Texture2D championShip;
            public static Texture2D hashtex;
            public static Texture2D hashtex2;
            public static Texture2D mainGame;
            public static Texture2D options;
            public static Texture2D achieveMents;
            public static Texture2D record;
            public static Texture2D debugArrow;
            public static Texture2D loadingText;
            public static Texture2D progressArrow;
            public static Texture2D medal;
            public static Texture2D starMedal;
            public static Texture2D brimMedal;
            public static Texture2D loadingTexture;
        }
    }
    public static class FightResources
    {
        public static void Initialize(ContentManager loader)
        {
            loader.RootDirectory = "Content";

            //Font.fightFont = new TFont("Content\\Sprites\\font\\menu.ttf", 16f, GameMain.graphics, System.Drawing.FontStyle.Regular);
            //Font.normalFont = new TFont("Content\\Sprites\\font\\normal.ttf", 16f, GameMain.graphics, System.Drawing.FontStyle.Regular); 
            Font.FightFont =    FightFont;
            Font.SansFont =     SansFont;
            Font.DamageFont =   DamageFont;
            Font.Japanese =     Japanese;
            Font.NormalFont =   NormalFont;
            //Font.Chinese = GlobalResources.Font.Chinese;

            Sprites.player =        loader.Load<Texture2D>("Sprites\\SOUL\\original");
            Sprites.brokenHeart =   loader.Load<Texture2D>("Sprites\\SOUL\\break");
            Sprites.leftHeart =     loader.Load<Texture2D>("Sprites\\SOUL\\leftSoul");
            Sprites.rightHeart =    loader.Load<Texture2D>("Sprites\\SOUL\\rightSoul");
            Sprites.soulCollide =   loader.Load<Texture2D>("Sprites\\SOUL\\collide");
            for (int i = 0; i < 6; i++)
            {
                Sprites.arrowShards[0, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\white\\01-" + (i + 1));
                Sprites.arrowShards[1, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\white\\02-" + (i + 1));
                Sprites.arrowShards[0, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\yellow\\01-" + (i + 1));
                Sprites.arrowShards[1, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\yellow\\02-" + (i + 1));
                Sprites.arrowShards[0, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\green\\01-" + (i + 1));
                Sprites.arrowShards[1, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\green\\02-" + (i + 1));
                Sprites.arrowShards[0, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\purple\\01-" + (i + 1));
                Sprites.arrowShards[1, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\purple\\02-" + (i + 1));
                Sprites.arrowShards[2, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\white\\01-" + (i + 1));
                Sprites.arrowShards[3, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\white\\02-" + (i + 1));
                Sprites.arrowShards[2, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\yellow\\01-" + (i + 1));
                Sprites.arrowShards[3, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\yellow\\02-" + (i + 1));
                Sprites.arrowShards[2, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\green\\01-" + (i + 1));
                Sprites.arrowShards[3, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\green\\02-" + (i + 1));
                Sprites.arrowShards[2, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\purple\\01-" + (i + 1));
                Sprites.arrowShards[3, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\Shards\\purple\\02-" + (i + 1));

                FightSprites.slides[i] = loader.Load<Texture2D>("Sprites\\FightSprites\\frames\\frame_" + i);
                if (i < 5)
                {
                    Sprites.heartPieces[i] = loader.Load<Texture2D>("Sprites\\SOUL\\shard" + i);
                    Sprites.GBStart[i] = loader.Load<Texture2D>("Sprites\\GB\\s\\frame_" + i);
                    if (i < 4)
                    {
                        Sprites.explodes[i] = loader.Load<Texture2D>("Sprites\\Explodes\\smallExplode" + (i + 1));

                        Sprites.arrow[0, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\blue" + i);
                        Sprites.arrow[1, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\red" + i);
                        Sprites.arrow[0, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\circle_blue" + i);
                        Sprites.arrow[1, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\circle_red" + i);
                        Sprites.arrow[0, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\rot_blue" + i);
                        Sprites.arrow[1, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\rot_red" + i);
                        Sprites.arrow[0, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\tran_blue" + i);
                        Sprites.arrow[1, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\tran_red" + i);
                        Sprites.arrow[2, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\green" + i);
                        Sprites.arrow[3, 0, i] = loader.Load<Texture2D>("Sprites\\bullet\\purple" + i);
                        Sprites.arrow[2, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\circle_green" + i);
                        Sprites.arrow[3, 1, i] = loader.Load<Texture2D>("Sprites\\bullet\\circle_purple" + i);
                        Sprites.arrow[2, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\rot_green" + i);
                        Sprites.arrow[3, 2, i] = loader.Load<Texture2D>("Sprites\\bullet\\rot_purple" + i);
                        Sprites.arrow[2, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\tran_green" + i);
                        Sprites.arrow[3, 3, i] = loader.Load<Texture2D>("Sprites\\bullet\\tran_purple" + i);
                        if (i < 2)
                        {
                            Sprites.GBShooting[i] = loader.Load<Texture2D>("Sprites\\GB\\p\\frame_" + i);
                            FightSprites.fight[i] = loader.Load<Texture2D>("Sprites\\FightSprites\\atk_" + i);
                            FightSprites.act[i] = loader.Load<Texture2D>("Sprites\\FightSprites\\act_" + i);
                            FightSprites.item[i] = loader.Load<Texture2D>("Sprites\\FightSprites\\itm_" + i);
                            FightSprites.mercy[i] = loader.Load<Texture2D>("Sprites\\FightSprites\\mry_" + i);
                            Sprites.deadlaser[i] = loader.Load<Texture2D>("Sprites\\OtherBarrages\\deadlaser\\deadlaser_" + i);
                            Sprites.fireball[i] = loader.Load<Texture2D>("Sprites\\OtherBarrages\\fireball\\fireball_" + i);
                        }
                    }
                }
            }

            Sprites.spear =         loader.Load<Texture2D>("Sprites\\bullet\\spear");
            Sprites.spike =         loader.Load<Texture2D>("Sprites\\Bone\\bone_spike");
            Sprites.spider =        loader.Load<Texture2D>("Sprites\\OtherBarrages\\spider");
            Sprites.pixiv =         loader.Load<Texture2D>("Sprites\\others\\pixiv");
            Sprites.firePartical =  loader.Load<Texture2D>("Sprites\\others\\firePartical");
            Sprites.lightBall =     loader.Load<Texture2D>("Sprites\\others\\lightBall");
            Sprites.lightBallS =    loader.Load<Texture2D>("Sprites\\others\\lightBallS");
            Sprites.square =        loader.Load<Texture2D>("Sprites\\others\\square");
            Sprites.boxPiece =      loader.Load<Texture2D>("Sprites\\others\\boxPiece");

            /*
                        Sprites.star = loader.Load<Texture2D>("Sprites\\others\\star");
                        Sprites.deadwarn = loader.Load<Texture2D>("Sprites\\others\\deadwarn");
                        for (int a = 0; a < 2; a++)
                        {
                            Sprites.Fireball[a] = loader.Load<Texture2D>("Sprites\\others\\fireball\\" + a);
                        }
                        for (int a = 0; a < 2; a++)
                        {
                            Sprites.deadlaser[a] = loader.Load<Texture2D>("Sprites\\others\\deadlaser\\" + a);
                        }

            Sprites.star = loader.Load<Texture2D>("Sprites\\others\\star");
            Sprites.deadwarn = loader.Load<Texture2D>("Sprites\\others\\deadwarn");
            Sprites.deadlaser = loader.Load<Texture2D>("Sprites\\others\\deadlaser"); */


            Sprites.stuck1 = loader.Load<Texture2D>("Sprites\\others\\GBStuck1");
            Sprites.stuck2 = loader.Load<Texture2D>("Sprites\\others\\GBStuck2");

            Sprites.voidarrow[0] = loader.Load<Texture2D>("Sprites\\bullet\\voidarrow\\blue0");
            Sprites.voidarrow[1] = loader.Load<Texture2D>("Sprites\\bullet\\voidarrow\\red0");
            Sprites.voidarrow[2] = loader.Load<Texture2D>("Sprites\\bullet\\voidarrow\\green0");
            Sprites.voidarrow[3] = loader.Load<Texture2D>("Sprites\\bullet\\voidarrow\\purple0");

            Sprites.target =        loader.Load<Texture2D>("Sprites\\bullet\\target");
            Sprites.bullet =        loader.Load<Texture2D>("Sprites\\bullet\\gunBullet");
            Sprites.goldenBrim =    loader.Load<Texture2D>("Sprites\\bullet\\golden_tip");

            Sprites.shield =             loader.Load<Texture2D>("Sprites\\SOUL\\shield");
            Sprites.shinyShield =        loader.Load<Texture2D>("Sprites\\SOUL\\shield_shiny");
            Sprites.ShieldCircle =       loader.Load<Texture2D>("Sprites\\SOUL\\circle");
            Sprites.ConsumptionCrystal = loader.Load<Texture2D>("Sprites\\SOUL\\crystal");

            Sprites.hpText =    loader.Load<Texture2D>("Sprites\\hp_show\\hp");
            Sprites.hpBar =     loader.Load<Texture2D>("Sprites\\hp_show\\hp_bar");

            Sprites.boneBody =      loader.Load<Texture2D>("Sprites\\Bone\\bone_body");
            Sprites.boneTail =      loader.Load<Texture2D>("Sprites\\Bone\\bone_down");
            Sprites.boneHead =      loader.Load<Texture2D>("Sprites\\Bone\\bone_up");
            Sprites.boneSlab =      loader.Load<Texture2D>("Sprites\\Bone\\bone_slab");
            Sprites.warningLine =   loader.Load<Texture2D>("Sprites\\Bone\\warning_line");

            Sprites.GBLaser = loader.Load<Texture2D>("Sprites\\GB\\laser");

            Sprites.explodeTrigger =        loader.Load<Texture2D>("Sprites\\Explodes\\explodeTrigger");
            Sprites.allPerfectText =        loader.Load<Texture2D>("Sprites\\others\\allPerfect");
            Sprites.accuracyBar =           loader.Load<Texture2D>("Sprites\\Pointer\\accuracyBar");
            Sprites.accuracyPointers[0] =   loader.Load<Texture2D>("Sprites\\Pointer\\accuracyPointerL");
            Sprites.accuracyPointers[1] =   loader.Load<Texture2D>("Sprites\\Pointer\\accuracyPointerM");
            Sprites.accuracyPointers[2] =   loader.Load<Texture2D>("Sprites\\Pointer\\accuracyPointerR");

            Sprites.platform[0] =       loader.Load<Texture2D>("Sprites\\Platform\\platform_body");
            Sprites.platform[1] =       loader.Load<Texture2D>("Sprites\\Platform\\platform_body2");
            Sprites.platformSide[0] =   loader.Load<Texture2D>("Sprites\\Platform\\platform_side");
            Sprites.platformSide[1] =   loader.Load<Texture2D>("Sprites\\Platform\\platform_side2");

            Sounds.playerSlice =        loader.Load<SoundEffect>("Sounds\\slice");
            Sounds.printWord =          loader.Load<SoundEffect>("Sounds\\word_sound");
            Sounds.sansWord =           loader.Load<SoundEffect>("Sounds\\sans_sound");
            Sounds.Ding =               loader.Load<SoundEffect>("Sounds\\hit");
            Sounds.playerHurt =         loader.Load<SoundEffect>("Sounds\\hurt");
            Sounds.spearAppear =        loader.Load<SoundEffect>("Sounds\\spawn");
            Sounds.spearShoot =         loader.Load<SoundEffect>("Sounds\\toss");
            Sounds.pierce =             loader.Load<SoundEffect>("Sounds\\pierce");
            Sounds.select =             loader.Load<SoundEffect>("Sounds\\choose_2");
            Sounds.changeSelection =    loader.Load<SoundEffect>("Sounds\\choose_1");
            Sounds.change =             loader.Load<SoundEffect>("Sounds\\change");
            Sounds.damaged =            loader.Load<SoundEffect>("Sounds\\damaged");
            Sounds.die1 =               loader.Load<SoundEffect>("Sounds\\die_1");
            Sounds.die2 =               loader.Load<SoundEffect>("Sounds\\die_2");
            Sounds.GBSpawn =            loader.Load<SoundEffect>("Sounds\\L_GB_summon");
            Sounds.GBShoot =            loader.Load<SoundEffect>("Sounds\\S_GB_shot");
            Sounds.heal =               loader.Load<SoundEffect>("Sounds\\heal");
            Sounds.explode =            loader.Load<SoundEffect>("Sounds\\exploding1");
            Sounds.destroy =            loader.Load<SoundEffect>("Sounds\\exploding2");
            Sounds.gunTargeting =       loader.Load<SoundEffect>("Sounds\\targeting");
            Sounds.gunShot =            loader.Load<SoundEffect>("Sounds\\gunShot");
            Sounds.boneSpawnLarge =     loader.Load<SoundEffect>("Sounds\\spawn2");
            Sounds.slam =               loader.Load<SoundEffect>("Sounds\\slam");
            Sounds.largeKnife =         loader.Load<SoundEffect>("Sounds\\knife");
            Sounds.boneSlabSpawn =      loader.Load<SoundEffect>("Sounds\\boneslab_spawn");
            Sounds.switchScene =        loader.Load<SoundEffect>("Sounds\\switch");
            Sounds.Warning =            loader.Load<SoundEffect>("Sounds\\warning");
            Sounds.giga =               loader.Load<SoundEffect>("Sounds\\giga");
            Sounds.ArrowStuck =         loader.Load<SoundEffect>("Sounds\\arrowStuck");
            Sounds.sparkles =           loader.Load<SoundEffect>("Sounds\\sparkles");
            Sounds.deadwarn =           loader.Load<SoundEffect>("Sounds\\deadwarn");
            Sounds.star0 =              loader.Load<SoundEffect>("Sounds\\star0");
            Sounds.star1 =              loader.Load<SoundEffect>("Sounds\\star1");

            Sprites.gunbolt =           loader.Load<Texture2D>("Sprites\\OtherBarrages\\gunbolt");
            Sprites.star =              loader.Load<Texture2D>("Sprites\\OtherBarrages\\star");
            Sprites.deadwarn =          loader.Load<Texture2D>("Sprites\\OtherBarrages\\deadwarn");

            FightSprites.aimer =        loader.Load<Texture2D>("Sprites\\FightSprites\\aimer");
            FightSprites.dialogBox =    loader.Load<Texture2D>("Sprites\\FightSprites\\dialogBox");
            FightSprites.stopBar =      loader.Load<Texture2D>("Sprites\\FightSprites\\stop_bar");
            FightSprites.movingBar =    loader.Load<Texture2D>("Sprites\\FightSprites\\moving_bar");

            Sprites.knife =             loader.Load<Texture2D>("Sprites\\OtherBarrages\\Knife\\Knife");
            Sprites.KnifeWarn =         loader.Load<Texture2D>("Sprites\\OtherBarrages\\Knife\\Warn");
        }

        public static class Font
        {
            public static GLFont FightFont { get; internal set; }
            public static GLFont SansFont { get; internal set; }
            public static GLFont DamageFont { get; internal set; }
            public static GLFont NormalFont { get; internal set; }
            public static GLFont Japanese { get; internal set; }
            //public static GLFont Chinese { get; internal set; }
        }
        public static class Sprites
        {
            /// <summary>
            /// undyne的箭头。第一维数组表示箭头颜色(3紫2绿1红0蓝)，第二维表示状态(0不变1黄2转3瞬移)，第三维表示损坏程度
            /// </summary>
            public static Texture2D[,,] arrow = new Texture2D[4, 4, 4];
            /// <summary>
            /// undyne的箭头碎片。第一维数组表示箭头颜色(3紫2绿1红0蓝)，第二维表示状态(0不变1黄2转3瞬移)，第三维表示碎片种类
            /// </summary>
            public static Texture2D[,,] arrowShards { get; } = new Texture2D[4, 4, 6];
            public static Texture2D[] voidarrow = new Texture2D[4];

            public static Texture2D player;
            public static Texture2D soulCollide;
            public static Texture2D brokenHeart;

            public static Texture2D star;

            public static Texture2D knife;
            public static Texture2D KnifeWarn;

            public static Texture2D[] deadlaser = new Texture2D[2];
            public static Texture2D deadwarn;
            public static Texture2D[] fireball = new Texture2D[2];
            public static Texture2D gunbolt;

            public static Texture2D leftHeart, rightHeart;
            public static Texture2D warningLine, boneSlab;
            public static Texture2D[] heartPieces = new Texture2D[5];

            /// <summary>
            /// 一个像素点
            /// </summary>
            public static Texture2D pixiv;
            public static Texture2D firePartical;
            public static Texture2D bullet;
            public static Texture2D target;
            /// <summary>
            /// 一个光球
            /// </summary>
            public static Texture2D lightBall;
            public static Texture2D lightBallS;
            public static Texture2D square;

            /// <summary>
            /// 盾牌
            /// </summary>
            public static Texture2D shield;
            public static Texture2D shinyShield;
            public static Texture2D ShieldCircle { get; internal set; }
            public static Texture2D ConsumptionCrystal { get; internal set; }

            /// <summary>
            /// 矛
            /// </summary>
            public static Texture2D spear;
            public static Texture2D spike;
            /// <summary>
            /// 蜘蛛
            /// </summary>
            public static Texture2D spider;
            public static Texture2D boxPiece;

            public static Texture2D stuck1;
            public static Texture2D stuck2;

            public static Texture2D hpText;
            public static Texture2D hpBar;

            public static Texture2D boneHead;
            public static Texture2D boneTail;
            public static Texture2D boneBody;

            public static Texture2D[] platform = new Texture2D[2];
            public static Texture2D[] platformSide = new Texture2D[2];

            public static Texture2D[] GBStart = new Texture2D[5];
            public static Texture2D[] GBShooting = new Texture2D[2];
            public static Texture2D GBLaser;

            public static Texture2D[] explodes = new Texture2D[4];
            public static Texture2D explodeTrigger;
            public static Texture2D goldenBrim;
            internal static Texture2D accuracyBar;
            internal static Texture2D allPerfectText;
            internal static Texture2D[] accuracyPointers = new Texture2D[3];
        }
        /// <summary>
        /// 提供了声音库
        /// </summary>
        public static class Sounds
        {
            /// <summary>
            /// 大型场面切换使用的音效
            /// </summary>

            public static SoundEffect switchScene;

            /// <summary>
            /// 大型壮观骨头召唤使用的音效
            /// </summary>
            public static SoundEffect boneSpawnLarge;
            /// <summary>
            /// 摔框音效
            /// </summary>
            public static SoundEffect slam;
            /// <summary>
            /// 玩家用刀攻击
            /// </summary>
            public static SoundEffect playerSlice;
            /// <summary>
            /// 打字机声音
            /// </summary>
            public static SoundEffect printWord;
            /// <summary>
            /// sans说话声音
            /// </summary>
            public static SoundEffect sansWord;
            /// <summary>
            /// 警告声音
            /// </summary>
            public static SoundEffect Warning { get; internal set; }
            /// <summary>
            /// 旧版箭头被格挡的"叮~"声
            /// </summary>
            public static SoundEffect Ding { get; internal set; }
            /// <summary>
            /// 新版打击音效
            /// </summary>
            public static SoundEffect ArrowStuck { get; internal set; }
            /// <summary>
            /// 吃药回血的声音
            /// </summary>
            public static SoundEffect heal;
            /// <summary>
            /// 玩家受伤的声音
            /// </summary>
            public static SoundEffect playerHurt;
            /// <summary>
            /// 矛等物品被召唤出来的声音
            /// </summary>
            public static SoundEffect spearAppear;
            /// <summary>
            /// 矛或骨刺被发射出去的声音
            /// </summary>
            public static SoundEffect spearShoot;
            /// <summary>
            /// 穿透声效，它常用来做骨头出现的声音
            /// </summary>
            public static SoundEffect pierce;
            /// <summary>
            /// 选项选择的音效(通常不在战斗使用)
            /// </summary>
            public static SoundEffect select;
            /// <summary>
            /// 改变选择的音效(通常不在战斗使用)
            /// </summary>
            public static SoundEffect changeSelection;
            /// <summary>
            /// sans转换场景时产生黑屏和消除黑屏的声音，降低音量后也可用于小型骨头召唤
            /// </summary>
            public static SoundEffect change;
            /// <summary>
            /// 怪物受伤的声音
            /// </summary>
            public static SoundEffect damaged;
            /// <summary>
            /// 决心分成两半的声音
            /// </summary>
            public static SoundEffect die1;
            /// <summary>
            /// 决心碎裂的声音
            /// </summary>
            public static SoundEffect die2;
            /// <summary>
            /// GB出现的声音
            /// </summary>
            public static SoundEffect GBSpawn;
            /// <summary>
            /// GB射击的声音
            /// </summary>
            public static SoundEffect GBShoot;
            /// <summary>
            /// 爆炸声音
            /// </summary>
            public static SoundEffect explode;
            /// <summary>
            /// 巨大物品毁灭的声音
            /// </summary>
            public static SoundEffect destroy;
            /// <summary>
            /// 枪支瞄准声音
            /// </summary>
            public static SoundEffect gunTargeting;
            /// <summary>
            /// 枪支瞄准声音
            /// </summary>
            public static SoundEffect gunShot;
            /// <summary>
            /// dt2刀子音效
            /// </summary>
            public static SoundEffect largeKnife;
            /// <summary>
            /// 骨墙召唤音效，也等同于敌人出现的音效
            /// </summary>
            public static SoundEffect boneSlabSpawn;
            /// <summary>
            /// dt2吼声
            /// </summary>
            public static SoundEffect giga;
            /// <summary>
            /// 星星0号声音
            /// </summary>
            public static SoundEffect star0;
            /// <summary>
            /// 星星1号声音
            /// </summary>
            public static SoundEffect star1;
            /// <summary>
            /// 类似闪烁的声音
            /// </summary>
            public static SoundEffect sparkles;
            /// <summary>
            /// 小羊闪电的预警
            /// </summary>
            public static SoundEffect deadwarn;
        }

        public static class FightSprites
        {
            public static Texture2D[] fight = new Texture2D[2];
            public static Texture2D[] act = new Texture2D[2];
            public static Texture2D[] item = new Texture2D[2];
            public static Texture2D[] mercy = new Texture2D[2];
            public static Texture2D aimer;
            public static Texture2D stopBar;
            public static Texture2D movingBar;

            public static Texture2D[] slides = new Texture2D[6];
            public static Texture2D dialogBox;

        }
        public static class Shaders
        {
            public static Shader Sinwave => CustomShaders.Sinwave;
            public static AuroraShader Aurora => CustomShaders.Aurora; 
            public static NeonLineShader NeonLine => CustomShaders.NeonLine;
            public static ColorBlendShader ColorBlend => CustomShaders.ColorBlend;
            public static BallShapingShader Cos1Ball => CustomShaders.Cos1Ball;
            public static StepSampleShader StepSample => CustomShaders.StepSample;
            public static ScaleShader Scale => CustomShaders.Scale;
            public static ScatterShader Scatter => CustomShaders.Scatter;
            public static CameraShader Camera => CustomShaders.Camera;
            public static SwirlShader Swirl => CustomShaders.Swirl;
            public static BlurShader Blur => CustomShaders.Blur;
            public static BlurShader FastBlur => CustomShaders.FastBlur;
            public static BlurKawaseShader BlurKawase => CustomShaders.BlurKawase;
            public static LensShader Lens => CustomShaders.Lens;
            public static PolarShader Polar => CustomShaders.Polar;
            public static GrayShader Gray => CustomShaders.Gray;
            public static SeismicShader Seismic => CustomShaders.Seismic;
            public static ShaderTiler ShaderTiler => CustomShaders.ShaderTiler;
            public static Smear Smear => CustomShaders.Smear;
            public static MosaicShader Mosaic => CustomShaders.Mosaic;
            public static BlockTileShader BlockTile => CustomShaders.BlockTile;
            public static TyndallShader Tyndall => CustomShaders.Tyndall;
            public static SpiralShader Spiral => CustomShaders.Spiral;
            public static WrongShader Wrong => CustomShaders.Wrong;
            public static FireShader Fire => CustomShaders.Fire;
        }
    }
}