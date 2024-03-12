using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class LightSweepShader : Shader
            {
                /*
float iChunkHeight;
float iIntensity;
float iTime;
*/
                public float Intensity { get; set; } = 1;
                public float Width { get; set; } = 10;
                public float Direction { get; set; } = 0;
                public Vector2 Centre { get; set; } = new Vector2(320, 240);


                public LightSweepShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    { /*
                       * uniform float2 iCenter;//center中心
uniform float iDirection; 
uniform float iWidth;//光柱宽度
uniform float iSweepIntensity;//调整强度
                        */
                        x.Parameters["iSweepIntensity"].SetValue(Intensity);
                        x.Parameters["iDirection"].SetValue(MathUtil.GetRadian(Direction));
                        x.Parameters["iWidth"].SetValue(Width);
                        x.Parameters["iCenter"].SetValue(Centre);
                    };
                }
            }
        }
    }
}