
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics; 

namespace UndyneFight_Ex.Remake
{
    public static class Resources
    {
        internal static void Initialize(ContentManager loader)
        {
            loader.RootDirectory = "Content\\ReEngine";
            UI.Cursor = loader.Load<Texture2D>("Mouse\\cursor");
        }
        public static class UI
        {
            public static Texture2D Cursor { get; set; }
        }
    }
}