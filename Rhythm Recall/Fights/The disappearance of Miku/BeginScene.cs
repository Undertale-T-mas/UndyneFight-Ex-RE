using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class MikuFight : IExtraOption
    {
        public string OptionName => "The disappearance of Miku";

        public Type IntroScene => typeof(MainScene);

        private class MainScene : Scene
        {
            Texture2D back;
            public MainScene()
            {
                back = Loader.Load<Texture2D>("Fights\\Miku\\back");
                SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Glitching());
                //this.InstanceCreate(new);
            }
            public override void Draw()
            {
                //初音 消失
                FightResources.Font.Japanese.CentreDraw("チュウインミクのシヤオシー", new(320, 320), Color.White, 1, 0.5f);
                base.Draw();
                FormalDraw(back, new(160, 100, 320, 200), Color.White * Rand(0.6f, 0.7f));
            }
            public override void Update()
            {
                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                {
                    GameStates.StartSong(new SongFightingScene.SceneParams(new MainFight(), null, 5, "Content\\Fights\\Miku\\song", JudgementState.Lenient, GameMode.None));
                }
                base.Update();
            }
        }
    }
}