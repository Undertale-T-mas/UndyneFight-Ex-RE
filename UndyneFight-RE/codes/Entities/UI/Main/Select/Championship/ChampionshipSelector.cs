using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;

namespace UndyneFight_Ex.Remake.UI
{
    internal class ChampionshipSelector : SmartSelector
    {
        private class ParticleGenerater : GameObject
        {
            public override void Update()
            {
                if(Rand(0, 1f) < 0.765f)
                {
                    float speed = Rand(1.0f, 1.6f);
                    this.AddChild(new Particle(Color.White, new(0, -speed * 1.15f * 4f), Rand(16f, 24f) / MathF.Pow(speed, 1.42f) * 1.315f, new(Rand(0, 960), 730), FightResources.Sprites.square)
                    {
                        DarkingSpeed = 4,
                        AutoRotate = true
                    });
                }
            }
        }
        public ChampionshipSelector() {
            this.KeyEvent = KeyEventNormal;
        }
        public override void Start()
        {
            base.Start();
            this.AddChild(new MouseCursor());
            this.AddChild(new ParticleGenerater());
        }
        public override void Draw()
        {

        }
        Color bound = Color.Transparent;
        float height = 0;
        public override void Update()
        {
            bound = Color.Lerp(bound, Color.Aqua, 0.035f);
            height = MathHelper.Lerp(height, 200, 0.05f);
            
            DownBoundDistance = height;
            LeftBoundDistance = 0;
            RightBoundDistance = 0;
            UpBoundDistance = 0;
            BoundColor = bound;

            if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
            {
                this.Dispose();
                GameStates.InstanceCreate(new SelectUI());
            }

            base.Update();
        }
        public override void Dispose()
        {
            MasterAlpha = 1f;
            DownBoundDistance = 0f;
            base.Dispose();
        }
    }
}