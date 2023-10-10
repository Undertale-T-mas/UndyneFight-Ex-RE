using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Engine
{
    internal class EnemyComponment
    {
        public EnemyComponment(string resourcePath)
        {
            Texture = Loader.Load<Texture2D>(resourcePath);
            ImageCentre = Texture.Bounds.Size.ToVector2() / 2;
        }
        public EnemyComponment(string resourcePath, Vector2 imageCentre)
        {
            ImageCentre = imageCentre;
            Texture = Loader.Load<Texture2D>(resourcePath);
        }
        public EnemyComponment(Texture2D tex, Vector2 imageCentre)
        {
            ImageCentre = imageCentre;
            Texture = tex;
        }

        public Texture2D Texture { get; private set; }
        public Vector2 ImageCentre { get; private set; }
    }
}