using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class BrainPower : IChampionShip
    {
        private class UnlockScene : Scene
        {
            SoundEffectInstance intro;
            public UnlockScene() : base()
            {
                System.IO.File.CreateText("Datas\\Global\\Unlock1.Tmpf");
                intro = Loader.Load<SoundEffect>("Musics\\ad,!U)~ZRr]\\intro").CreateInstance();
                Image = Loader.Load<Texture2D>("Musics\\ad,!U)~ZRr]\\paint");
                intro.Play();
            }
            int appearTime = 0;
            Vector2 pos = new(320, 10000);
            public override void Draw()
            {
                FormalDraw(Image, pos, Color.White * Rand(0.3f, 0.4f), 0, ImageCentre);
                base.Draw();
            }
            public override void Update()
            {
                pos = pos * 0.98f + new Vector2(320, 240) * 0.02f;
                if (appearTime == 1)
                {
                    Player.hearts.Clear();
                    var v = GetAll<GameObject>();
                    for (int i = 0; i < v.Length; i++) v[i].Dispose();
                }
                if (appearTime == 245)
                {
                    InstanceCreate(new UndyneFight_Ex.Fight.TextPrinter(
                        "$Let the bass kick\n" +
                        "$O - oooooooooooo\n" +
                        "AAAAE-A-A-I-A-U-\n" +
                        "JO- oooooooooooo\n" +
                        "AAE-O-A-A-U-U-A-\n" +
                        "E - eeeee-ee-eee\n" +
                        "AAAAE-A-E-I-E-A-\n" +
                        "JO- ooo-oo-oo-oo\n" +
                        "AAAAE-A-A-I-A-U-\n" +
                        "O - oooooooooooo\n" +
                        "AAAAE-A-A-I-A-U-\n" +
                        "JO- oooooooooooo\n" +
                        "AAE-O-A-A-U-U-A-\n" +
                        "E - eeeee-ee-eee\n" +
                        "AAAAE-A-E-I-E-A-\n" +
                        "JO- ooo-oo-oo-oo\n" +
                        "AAAAE-A-A-I-A-U-\n", new Vector2(160, 50),
                        new UndyneFight_Ex.Fight.TextSpeedAttribute(9), new UndyneFight_Ex.Fight.TextSpeedAttribute(11.1f)));
                }
                appearTime++;

                if (intro.State == SoundState.Stopped)
                {
                    //throw new Exception();

                    SongFightingScene.SceneParams @params =
                        new(new BrainPower.Game(), null, 5, "Content\\Musics\\ad,!U)~ZRr]\\song",
                        GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Strict, GameMode.RestartDeny);
                    GameStates.ResetScene(new SongLoadingScene(@params));
                }
                base.Update();
            }
        }
        public static void IntoUnlockScene()
        {
            GameStates.ResetScene(new UnlockScene());
        }
    }
}