using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class DislocationShaderX : Shader
            {
                /*
float iChunkHeight;
float iIntensity;
float iTime;
*/
                public float Intensity { get; set; } = 0;
                public float Speed { get; set; } = 0;
                public float Time { get; set; } = 0;
                public float ChunkHeight { get; set; } = 5;
                public bool RGBSplitEnabled { get; set; } = false;

                private Effect _eff;

                public DislocationShaderX(Effect eff) : base(eff)
                {
                    _eff = eff;
                    StableEvents = (x) =>
                    {
                        if(RGBSplitEnabled)
                        {
                            _eff.CurrentTechnique = _eff.Techniques[1];
                        }
                        else
                        {
                            _eff.CurrentTechnique = _eff.Techniques[0];
                        }
                        this.Time += this.Speed;
                        x.Parameters["iIntensity"].SetValue(Intensity / 480f); 
                        x.Parameters["iTime"].SetValue(Time / 480f);
                        x.Parameters["iChunkHeight"].SetValue(ChunkHeight / 480f);
                    };
                }
            }
            public class DislocationShaderY : Shader
            {
                /*
float iChunkHeight;
float iIntensity;
float iTime;
*/
                public float Intensity { get; set; } = 0;
                public float Speed { get; set; } = 0;
                public float Time { get; set; } = 0;
                public float ChunkHeight { get; set; } = 5;

                public bool RGBSplitEnabled { get; set; } = false;

                private Effect _eff;

                public DislocationShaderY(Effect eff) : base(eff)
                {
                    _eff = eff;
                    StableEvents = (x) =>
                    {
                        if (RGBSplitEnabled)
                        {
                            _eff.CurrentTechnique = _eff.Techniques[1];
                        }
                        else
                        {
                            _eff.CurrentTechnique = _eff.Techniques[0];
                        }
                        this.Time += this.Speed;
                        x.Parameters["iIntensity"].SetValue(Intensity);
                        x.Parameters["iIntensity"].SetValue(Intensity);
                        x.Parameters["iTime"].SetValue(Intensity / 640f);
                        x.Parameters["iChunkHeight"].SetValue(Intensity / 640f);
                    };
                }
            }
        }
    }
}