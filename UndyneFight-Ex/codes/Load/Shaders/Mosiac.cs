using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class MosaicShader : Shader
            {
                public Vector2 MosiacSize { get; set; } = new Vector2(4, 4);
                public MosaicShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iBlockSize"].SetValue(MosiacSize);
                    };
                }
            }
        }
    }
}