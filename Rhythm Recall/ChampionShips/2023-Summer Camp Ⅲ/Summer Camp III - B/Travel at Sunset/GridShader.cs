using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic; 
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex;

namespace Rhythm_Recall.Waves
{
    internal partial class Traveler_at_Sunset
    {
        internal partial class GridShader : UndyneFight_Ex.Shader
        {
            public GridShader() : base(UndyneFight_Ex.Fight.Functions.Loader.Load<Effect>("Musics\\Traveler at Sunset\\Grid"))
            {
                this.StableEvents = (t) => {
                    Time += 0.011f * TimeElapsed;
                    RegisterTexture(GlobalResources.Sprites.hashtex, 1);
                    this.Parameters["iTime"].SetValue(Time);
                    this.Parameters["iColor"].SetValue(BlendColor.ToVector3() * 0.8f);
                    this.Parameters["iSide"].SetValue(SideColor.ToVector3());
                    this.Parameters["iIntensity"].SetValue(Intensity1);
                    this.Parameters["iGlowDistance"].SetValue(GlowDistance);
                    this.Parameters["iGlowIntensity"].SetValue(GlowIntensity);
                    this.Parameters["iIntensity2"].SetValue(Intensity2);
                    this.Parameters["iIntensity3"].SetValue(Intensity3);
                };
            }
            public Color BlendColor { get; set; }
            public Color SideColor { get; set; }
            public float Time { get; set; }
            public float Intensity1 { get; set; }
            public float Intensity2 { get; set; }
            public float Intensity3 { get; set; }
            public float GlowIntensity { get; set; }
            public float GlowDistance { get; set; }
        }
    }
}