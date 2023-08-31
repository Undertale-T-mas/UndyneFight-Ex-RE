using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;

namespace Rhythm_Recall.Waves
{
    public class Determination : IChampionShip
    {
        public Determination()
        {

            difficulties = new();
            difficulties.Add("DETERMINATION", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (165f / 60f))
            {

            }
            public string Music => "Determination";

            public string FightName => "Determination";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {

                            new(Difficulty.ExtremePlus, 19.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {

                            new(Difficulty.ExtremePlus, 19.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {

                        new(Difficulty.ExtremePlus, 23.5f),
                        }
                    );
                public override string BarrageAuthor => "Woem";
                public override string AttributeAuthor => "Woem";
                public override string SongAuthor => "Laur";
            }
            public SongInformation Attributes => new ThisInformation();
            private bool notRegistered = true;
            public static Determination game;
            public class Lyric : Entity
            {
                WaveConstructor wave = new(62.5f / (165f / 60f));
                public Speech[] lyrics =
                {
                 new Speech("XXにXをXらす",8),//永遠に血を鳴らす
                 new Speech("XるぎないXしみを",8),//揺るぎない憎しみを
                 new Speech("Xりかざして   XはXけXすの",10),//振りかざして　私は駆け出すの
                 new Speech("",4+32),
                 new Speech("XしくXをXばすXは",12),//卑しく影を伸ばす針は
                 new Speech("XのXXけをXしあう",20),//時の仕掛けを映しあう
                 new Speech("XきXXなXをXす",16),//幼き無力な胸を指す
                 new Speech("いのちXげた   Xすら",16),//いのち捧げた　愛すら
                 new Speech("XXのXXれに",16),//暴虐の道連れに
                 new Speech("XにXるXが",32),//鏡に煌る傷が
                 new Speech("まるでXのX",16),//まるで心の透視
                 new Speech("ああXXもXめた",16),//ああ何度も歪めた
                 new Speech("XXにさらわれた",16),//永遠にさらわれた
                 new Speech("XるぎないXしさを",16),//揺るぎない愛しさを
                 new Speech("XいXしてXがれるまどろみ",16),//思い出して焦がれるまどろみ
                 new Speech("XXかへXった",4),//何処かへ散った
                 new Speech("Xえてしまえ   うつつなんて",16),//消えてしまえ　うつつなんて
                 new Speech("もがいてもXになるだけ",16),//もがいても痣になるだけ
                 new Speech("XX   Xうなら   Xれずに",16),//再生　叶うなら　恐れずに
                 new Speech("XちXかうXXがXしい",16),//立ち向かう勇気が欲しい
                 new Speech("XらめくXをXすXは",16),//揺らめく影を隠す羽は
                 new Speech("XいXいをXかせた",16),//甘い誘いを響かせた
                 new Speech("XえばXのXから",16),//例えば私の心から
                 new Speech("XいXをXして",16),//重い枷を外して
                 new Speech("XをXせることはできるの?",16),//夢を魅せることはできるの？"
                 new Speech("XにXるXが",16),//鏡に煌る傷が
                 new Speech("まるでXのXX",16),//まるで心の天使
                 new Speech("ああXXもXんだ",16),//ああ何度も叫んだ
                 new Speech("XXにXXした",16),//永遠に支配した
                 new Speech("XるぎないXしさを",16),//揺るぎない悔しさを
                 new Speech("XにXえてXきXらぎ",16),//声に変えて尊き安らぎ
                 new Speech("XをXった",16),//姿を知った
                 new Speech("Xえてしまえ   Xいなど",16),//消えてしまえ　虚いなど
                 new Speech("XXなXXになるから",16),//残酷な要塞になるから
                 new Speech("XX   いまX   もうXX",16),//済生　いま私　もう一度
                 new Speech("このXXで   XいてXせる",16),//この場所で　輝いて魅せる
                 new Speech("XざしていたXをXけるのは",16),//閉ざしていた扉を開けるのは
                 new Speech("XXXXだった",16),//自分自身だった
                 new Speech("XのXXにXをXせ",16),//麗の虚空に灰を晒せ
                 new Speech("XXをXXりXせた",16),//鼓動を手繰り寄せた
                 new Speech("XXをXXにのせて",16),//音色を明日にのせて
                 new Speech("Xきていく",16),//生きていく
                 new Speech("XのXなXX",16),//悪魔の様な天使
                 new Speech("XXのXにXされ",16),//決意の槍に刺され
                 new Speech("Xちてく",16),//堕ちてく
                 new Speech("XXにXをXらす",16),//永遠に血を鳴らす
                 new Speech("XるぎないXしみを",16),//揺るぎない憎しみを
                 new Speech("XりかざしてXれたXXを",16),//振りかざして汚れた視界を
                 new Speech("XはXった",16),//時は巡った
                 new Speech("Xえないで   Xじたいの",16),//消えないで　信じたいの
                 new Speech("XかなXXのXXを",16),//僅かな希望の開花を
                 new Speech("Xかせた   Xいなき   そのXは",16),//咲かせた　迷いなき　その花は
                 new Speech("XをXし   XしくXつ",16),//火を宿し　逞しく育つ
                 new Speech("XめていたXをXうのは",16),//殺めていた心を救うのは
                 new Speech("XXXXだった",16),//自分自身だった
                 new Speech("XをXしてXをXうの",16),//麗を壊して善を乞うの

                 new Speech("Barrage:Tlottgodinf",1f),
            };
                float timer = 0;
                int lyriccount = 0;
                public override void Draw()
                {
                    FightResources.Font.Japanese.CentreDraw(lyrics[lyriccount].Lyric, new(320, 400), Color.Lerp(Color.White, Color.Red, 0.3f), 1f, 0.5f);

                }
                float beatcount = 0;
                public override void Update()
                {
                    timer++;
                    if (lyriccount == lyrics.Length - 1) Dispose();
                    if (timer == 1)
                    {
                        for (int a = 0; a < lyrics.Length; a++)
                        {
                            beatcount += lyrics[a].BeatCount;
                            wave.DelayBeat(beatcount, () => { lyriccount += 1; });
                        }
                    }

                }
            }

            public class Speech
            {
                WaveConstructor wave = new(62.5f / (165f / 60f));
                public string Lyric = "";
                public float BeatCount = 0;
                public Speech(string lyric, float beatcount)
                {
                    Lyric = lyric;
                    BeatCount = beatcount;//*wave.BeatTime(beatcount); 
                }
            }

            public void LineShadow(int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, 0.24f - 0.24f / times * t));

                }
            }
            public void LineShadow(float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, deep - deep / times * t));

                }
            }
            public void LineShadow(float delay, float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * delay, deep - deep / times * t));

                }
            }

            public void Easy()
            {
                if (Gametime < 0) return;
            }
            GlobalResources.Effects.BlurShader blur = FightResources.Shaders.Blur;
            GlobalResources.Effects.StepSampleShader step = FightResources.Shaders.StepSample;
            ScreenDrawing.Shaders.RGBSplitting splitter = null;
            void Part1()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Filter", () =>
                {
                    RenderProduction production = new ScreenDrawing.Shaders.Filter(blur, 0.99f);
                    SceneRendering.InsertProduction(production);
                    blur.Factor = new(0.4f, 0.4f);
                    SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(step, 0.98f));
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.97f) { Disturbance = false };
                    splitter.RandomDisturb = 0;
                    SceneRendering.InsertProduction(splitter);
                    //SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(RGB,0.9999f));
                    //SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(RGB2));
                });
                RegisterFunctionOnce("Sigma1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0, 8f, BeatTime(0.5f)));
                    ve.Insert(BeatTime(3f), ValueEasing.EaseInQuad(8, 0f, BeatTime(3f)));
                    ve.Run((s) => { blur.Sigma = s; splitter.Intensity = s * 2f; });
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(0, ValueEasing.Stable(1));
                    ve2.Insert(BeatTime(1f), ValueEasing.EaseOutQuart(1, 0.8f, BeatTime(1f)));
                    ve2.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0.8f, 1f, BeatTime(3f)));
                    ve2.Run((s) => { ScreenScale = s; });
                    CameraEffect.Convulse(30, BeatTime(3f), false);
                    ValueEasing.EaseBuilder ve3 = new();
                    ve3.Insert(0, ValueEasing.Stable(0));
                    ve3.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(0, 0.4f, BeatTime(1f)));
                    ve3.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0.4f, 0f, BeatTime(3f)));
                    ve3.Run((s) => { step.Intensity = s; });
                    ValueEasing.EaseBuilder ve4 = new();
                    ve4.Insert(0, ValueEasing.Stable(0));

                    //RGB.Radius = 10f;
                    //RGB.RadiusOut = 10f;
                });
                bool color = true;
                if (color) BarrageCreate(0, BeatTime(1), 6.2f, new string[]
                  {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "R","","","",   "R","","","",
                "R","","","",   "R","","","",
                "","","","",   "R","","","",
                "","","","",   "R","","R","",

                "R","","","",   "R1","","","",
                "R1","","","",   "R1","","","",
                "R1","","","",   "R1","","","",
                "R","","","",   "R","","","",

                "R","","","",   "R","","","",
                "","","","",   "R","","","",
                "","","","",   "R","","","",
                "","","","",   "R","","R","",
                //
                "R","","","",   "R","","","",
                "R","","","",   "R","","","",
                "R","","","",   "R","","","",
                "R","","","",   "R","","","",

                "(R)(R1)","","","",   "(R)","","","",
                "(R)(R1)","","","",   "(R)","","","",
                "(R)(R1)","","","",   "(R)","","","",
                "(R)(R1)","","","",   "(R)","","","",

                "(R)(R1)","","","",   "(R)(R1)","","","",
                "(R)(R1)","","","",   "(R)(R1)","","","",
                "","","","",   "(R)(R1)","","","",
                "","","","",   "(R)(R1)","","","",

                "(Sigma1)(Filter)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","R","",
                  });
                if (!color)
                    BarrageCreate(BeatTime(2), BeatTime(1), 6.2f, new string[]
                    {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                    });
            }
            void Part2()
            {

                bool color = false;
                if (!color) BarrageCreate(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                "R","","","",   "","","","",
                "","","","",   "R","","","",
                "R","","","",   "","","R","",
                "","","","",   "R","","","",

                "R","","","",   "","","","",
                "R","","","",   "R","","","",
                "","","","",   "R","","","",
                "","","","",   "R","","","",

                "","","","",   "","","","",
                "","","","",   "R","","R","",
                "R","","","",   "","","","",
                "R","","","",   "R","","","",

                "R","","","",   "","","","",
                "","","","",   "","","","",
                "R","","","",   "","","R","",
                "","","","",   "R","","","",
                //
                "R","","","",   "","","","",
                "","","","",   "R","","R","",
                "R","","","",   "R","","","",
                "R","","","",   "R","","","",

                "R","","","",   "R","","","",
                "R","","","",   "R","","","",
                "","","","",   "","","","",
                "","","","",   "R","","R","",

                "R","","","",   "","","","",
                "","","","",   "R","","","",
                "R","","","",   "","","R","",
                "","","","",   "R","","","",

                "R","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    //
                });
                if (!color)
                    BarrageCreate(BeatTime(4), BeatTime(1), 6.2f, new string[]
                    {
                "$01","","","",   "","","","",
                "","","","",   "","","","",
                "+21","","","",   "","","","",
                "","","","",   "","","","",

                "+21","","","",   "","","","",
                "","","","",   "","","","",
                "+21","","","",   "","","","",
                "","","","",   "","","","",

                "+21","","","",   "","","","",
                "","","","",   "","","","",
                "+21","","","",   "","","","",
                "","","","",   "","","","",

                "+21","","","",   "","","","",
                "","","","",   "+21","","","",
                "","","","",   "","","","",
                "+21","","","",   "+21","","","",
                //
                "+21","","","",   "","","","",
                "","","","",   "","","","",
                "+21","","","",   "+21","","","",
                "+21","","","",   "+21","","","",

                "+21","","","",   "","","","",
                "","","","",   "","","","",
                "+21","","","",   "","","","",
                "","","","",   "","","","",

                "+21","","","",   "","","","",
                "","","","",   "","","","",
                "+21","","","",   "","","","",
                "+21","","","",   "","","","",

                "+21","","","",   "","","","",
                "","","","",   "+21","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                        //

                    });
            }


            public void ExtremePlus()
            {
                if (InBeat(0)) Part1();
                if (InBeat(32 - 4)) Part2();
            }
            #region non
            public void Extreme()
            {
                if (Gametime < 0) return;
            }

            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }

            public void Normal()
            {
                if (Gametime < 0) return;
            }
            #endregion
            public void Start()
            {
                DelayBeat(1.4f, () => { CreateEntity(new Lyric()); });
                GametimeDelta = -0.5f;
                //GametimeDelta = 630 - 3f;
                //PlayOffset = 630;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(new Vector2(320, 240));
                HeartAttribute.MaxHP = 100;
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 1.45f;
            }
        }
    }
}