using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RecallCharter
{
    public static class Resources
    {
        public static Texture2D XImage { get ; private set;}
        public static void Initialize(ContentManager loader)
        {
            loader.RootDirectory = "Content\\RecallCharter";
            XImage = loader.Load<Texture2D>("x");
        }
    }
}