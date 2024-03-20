using Microsoft.Xna.Framework;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;

namespace UndyneFight_Ex.Entities.Advanced
{
    public class ZaWarudo : GameObject
    {
        internal static bool exist = false;
        private int appearTime = 0;
        private readonly int totalTime;
        private readonly float scale;
        private readonly float oldScreenSize;
        private Color oldScreenColor;
        private Color oldBoundColor;
        private Color frozenColor = Color.Black;
        float frozenDepth = 60;

        public ZaWarudo(int time, float scale)
        {
            if (exist)
            {
                totalTime = -1;
                Dispose();
                return;
            }
            exist = true;
            oldScreenSize = ScreenScale;
            oldScreenColor = BackGroundColor;

            BackGroundColor = Color.DarkBlue * 0.27f;
            MakeFlicker(Color.LightBlue);
            totalTime = time;
            this.scale = scale;
            GameMain.GameSpeed = 0.5f;
        }
        public ZaWarudo(int time, float scale, Color frozenColor)
        {
            if (exist)
            {
                totalTime = -1;
                Dispose();
                return;
            }
            exist = true;
            oldBoundColor = BoundColor;
            BoundColor = frozenColor;
            this.frozenColor = frozenColor;
            oldScreenSize = ScreenScale;
            oldScreenColor = BackGroundColor;

            BackGroundColor = Color.DarkBlue * 0.27f;
            MakeFlicker(Color.LightBlue);
            totalTime = time;
            this.scale = scale;
            GameMain.GameSpeed = 0.5f;
        }

        public override void Update()
        {
            if (totalTime <= 0)
            {
                Dispose();
                return;
            }
            if (frozenColor != Color.Black)
            {
                BoundDistance = (appearTime <= totalTime)
                ? (BoundDistance * 0.87f) + (Vector4.One * frozenDepth * 0.13f)
                : BoundDistance *= 0.87f;
            }
            appearTime++;
            if (appearTime <= 16)
                ScreenScale -= (16 - appearTime) / 720f;
            if (appearTime >= 8 && appearTime <= 36)
            {
                GameMain.GameSpeed = (GameMain.gameSpeed * 0.8f) + (scale * 0.2f);
            }
            if (appearTime >= totalTime)
            {
                ScreenScale = (GameMain.CurrentDrawingSettings.screenScale * 0.91f) + (oldScreenSize * 0.09f);
                BackGroundColor = new Color((GameMain.CurrentDrawingSettings.backGroundColor.ToVector4() * 0.9f) + (oldScreenColor.ToVector4() * 0.1f));
                GameMain.GameSpeed = ((1 - scale) * (appearTime - totalTime) / 45f) + scale;
            }
            if (appearTime >= totalTime + 45)
            {
                Dispose();
            }
        }
        public override void Dispose()
        {
            if (totalTime <= 0)
                goto A;
            exist = false;
            if (frozenColor != Color.Black)
                BoundDistance = Vector4.One;
            GameMain.gameSpeed = 1.0f;
            ScreenScale = oldScreenSize;
            BackGroundColor = oldScreenColor;
            BoundColor = oldBoundColor;
        A: base.Dispose();
        }
    }
}
