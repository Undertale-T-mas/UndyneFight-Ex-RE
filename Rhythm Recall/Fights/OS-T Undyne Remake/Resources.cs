using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        internal static class Resources
        {
            public static void Initialize()
            {
                starParticle = Loader.Load<Texture2D>("Fights\\OS-T Remake\\particle");
                eyeLaser = Loader.Load<Texture2D>("Fights\\OS-T Remake\\eye_laser");
            }
            public static Texture2D starParticle;
            public static Texture2D eyeLaser;
        }
    }
}