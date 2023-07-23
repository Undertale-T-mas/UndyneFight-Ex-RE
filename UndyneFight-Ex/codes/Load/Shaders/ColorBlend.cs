using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Fight;
using System.Runtime.CompilerServices;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class ColorBlendShader : Shader
            { 
                public ColorBlendShader(Effect eff) : base(eff)
                { 
                    StableEvents = (x) =>
                    {
                        RegisterTexture(Sprites.hashtex2, 1);
                        /*       x.Parameters["iSpeedX"].SetValue(Speed.X);
                               x.Parameters["iSpeedY"].SetValue(Speed.Y);
                               x.Parameters["iCoreSpeed"].SetValue(CoreSpeed);
                               x.Parameters["iTime"].SetValue(Time);
                               x.Parameters["iColorA"].SetValue(ColorA.ToVector4());
                               x.Parameters["iColorB"].SetValue(ColorB.ToVector4());*/
                    };
                }
                public float Time { get; set; } = 0;
                public Vector2 Speed { get; set; } = new Vector2(1, 1.5f);
                public Vector2 CoreSpeed { get; set; } = new Vector2(1, 1.5f);
                public Color ColorA { get; set; } = Color.Red;
                public Color ColorB { get; set; } = Color.Orange;
            }
        }
    }
}