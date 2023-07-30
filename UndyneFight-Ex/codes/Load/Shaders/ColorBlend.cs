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
                        /*       x.Parameters["iSpeedX"].SetValue(Speed.X);
                               x.Parameters["iSpeedY"].SetValue(Speed.Y);
                               x.Parameters["iCoreSpeed"].SetValue(CoreSpeed);
                               x.Parameters["iTime"].SetValue(Time);
                               x.Parameters["iColorA"].SetValue(ColorA.ToVector4());
                               x.Parameters["iColorB"].SetValue(ColorB.ToVector4());*/
                    };
                } 
            }
        }
    }
}