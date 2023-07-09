using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex;

namespace UndyneFight_Ex.Remake.Effects
{
    public class BackGenerater : RenderProduction
    {
        public BackGenerater(float depth = 0.5f ) : base(Resources.UIShaders.Background, SpriteSortMode.Immediate, BlendState.Additive, depth)
        { 
        }
        float time0 = 0.0f;
        BackgroundShader _shader { get; set; }
        public override void Update()
        {
            if (_shader == null) return;
            _shader.Time += 0.35f;
            time0 += 0.5f;

            if ((time0 + 1) % 15 < 0.5f)
            {
                particles.Add(new Particle(
                    Color.White * MathUtil.GetRandom(0.4f, 0.8f) * 0.8f,
                    new Vector2(MathUtil.GetRandom(-1.0f, 1.0f) * 0.5f, -MathUtil.GetRandom(3f, 4f)),
                    MathUtil.GetRandom(40f, 70f) * 1.2f,
                    new Vector2(MathUtil.GetRandom(-10f, 970f), 960),
                    Fight.Functions.ScreenDrawing.Shaders.Lighting.lightSources[0]
                    )
                { DarkingSpeed = 0.27f });
            }
            this.particles.ForEach(s => s.Update());
            this.particles.RemoveAll(s => s.Centre.Y < -150);
        }
        List<Entity> particles = new();
        static bool LightSourceUpdated = false;
        public override RenderTarget2D Draw(RenderTarget2D obj)
        {
            if (!LightSourceUpdated)
            {
                var v = new Fight.Functions.ScreenDrawing.Shaders.Lighting(0.4f);
                v.Draw(obj);
                LightSourceUpdated = true;
            }
            this.Shader = this._shader  = Resources.UIShaders.Background;
            this.BlendState = BlendState.Additive;
            this.TransForm = Matrix.Identity;
            float position = MathF.Sin(time0 / 215f) * 1;
            float position2 = MathF.Sin(time0 / 205f) * 1;

            if(obj != HelperTarget3)
            {
                CopyRenderTarget(HelperTarget3, obj);
            }
            this.MissionTarget = HelperTarget;
            _shader.DeltaPosition = new Vector2(0.5f, 0.5f);

            this.DrawTextures(new Texture2D[] { HelperTarget3 }, obj.Bounds, null, new Color[] { Color.White * 0.21f * 0.8f });

            this.MissionTarget = HelperTarget2;
            _shader.DeltaPosition = new Vector2(1f, position + 0.5f); 

            this.DrawTextures(new Texture2D[] { HelperTarget3, HelperTarget }, obj.Bounds, null, new Color[] { Color.Pink * 0.32f * 0.8f, Color.White });

            this.MissionTarget = HelperTarget;
            _shader.DeltaPosition = new Vector2(0.5f + position2, 0f); 

            this.DrawTextures(new Texture2D[] { HelperTarget3, HelperTarget2 }, obj.Bounds, null, new Color[] { Color.Aqua * 0.30f * 0.8f, Color.White });
            this.Shader = null;
            Entity.DrawOptimize = false;
            this.TransForm = GameStates.ResizeMatrix;
            this.DrawEntities(this.particles.ToArray());



            return HelperTarget;

           /* VertexPositionColor[] vertexs = new VertexPositionColor[6];
            vertexs[0].Position = new(0, 0, 0);
            vertexs[1].Position = new(640, 0, 0);
            vertexs[2].Position = new(0, 480, 0);
            vertexs[3].Position = new(640, 480, 0); 

            for(int i = 0; i < 6; i++) vertexs[i].Color = Color.White;

            MissionTarget = obj;

            var passes = ((Effect)this._shader).CurrentTechnique.Passes;
            foreach (var pass in passes)
            {
                pass.Apply();

                // Whatever happens in pass.Apply, make sure the texture being drawn
                // ends up in Textures[0]. 

                WindowDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertexs, 0, 6, new int[]{ 0, 1, 2, 1, 2, 3 }, 0, 2);
            }

            return obj;*/
        }
    }
}