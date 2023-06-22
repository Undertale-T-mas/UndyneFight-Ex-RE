using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            internal static Shader Light0 { get; private set; }
            internal static Shader Light1 { get; private set; }
            internal static Shader Light2 { get; private set; }
            internal static Shader Light3 { get; private set; }

            internal static void LoadInternals(ContentManager loader)
            {
                loader.RootDirectory = "Content\\Global\\Shaders\\Internal Effect\\";
                Light0 = new Shader(loader.Load<Effect>("Light0"));
            }
        }
    }
}