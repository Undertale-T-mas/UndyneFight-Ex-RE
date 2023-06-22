using Microsoft.Xna.Framework;
using System;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    internal class TimeShower : Entity
    {
        public TimeShower()
        {
            UpdateIn120 = true;
        }
        public override void Draw()
        {
            Color col = GameMain.CurrentDrawingSettings.UIColor;
            if (del)
            {
                col.R = (byte)Math.Max(col.R - 2, 0);
                col.G = (byte)Math.Max(col.G - 2, 0);
                col.B = (byte)Math.Max(col.B - 2, 0);
            }
            int d = (int)(GameMain.gameTime / 62.5f * 60f);
            int min = d / 3600, sec = (d / 60) % 60, ms = d % 60;
            FightResources.Font.NormalFont.CentreDraw($"{min}:{(sec < 10 ? "0" : "") + sec}:{(ms < 10 ? "0" : "") + ms}",
                new Vector2(100, 20), col);
#if DEBUG
            FightResources.Font.NormalFont.CentreDraw($"{FloatToString(deltaMin, 1)}/" +
                $"{FloatToString(deltaCur, 1)}/{FloatToString(deltaMax, 1)}",
                new Vector2(100, 110), col, 0.9f, 0.3f);
#endif
        }
#if DEBUG
        float deltaCur = -1;
        float deltaMax = -999, deltaMin = 999;
#endif
        int appearTime = 0;
        Color drawingColor = Color.White;
        bool del = false;
        public override void Update()
        {
#if DEBUG
            if (appearTime > 35)
            {
                deltaCur = (CurrentScene as SongFightingScene).GlobalDelta;
                deltaMax = MathF.Max(deltaCur, deltaMax);
                deltaMin = MathF.Min(deltaMin, deltaCur);
            }
#endif
            appearTime++;
            if (appearTime % 62 == 0)
            {
                del = Fight.Functions.RandBool();
            }
        }
    }
}