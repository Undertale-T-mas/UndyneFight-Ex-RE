using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static System.MathF;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sprites;
using static UndyneFight_Ex.GameMain;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class HPShower : Entity
    {
        private CollideRect KRRect;
        private CollideRect FullRect;

        public bool Vertical { set; private get; } = false;
        private bool Buffed => ((CurrentScene as FightScene).Mode & GameMode.Buffed) != 0;
        public Color HPExistColor { get => hpExistColor; set => hpExistColor = hpExistCurrent = value; }
        public Color HPLoseColor { get => hpExistColor; set => hpLoseColor = hpExistCurrent = value; }
        public Color HPKRColor { set => hpKRColor = hpKRCurrent = value; }
        private Color hpExistColor, hpExistCurrent;
        private Color hpLoseColor, hpLoseCurrent;
        private Color hpKRColor, hpKRCurrent;

        public static HPShower instance;

        public HPShower()
        {
            instance = this;
            Image = hpText;
            collidingBox = new CollideRect(320, 455 - 12, 100, 24);
            KRRect.Height = 24;
            KRRect.Y = 458 - 12;

            HPKRColor = new(255, 0, 255, 255);
            HPLoseColor = Color.Red;
            HPExistColor = new(0, 255, 0, 255);
        }

        private CollideRect fullarea = new(320, 443, 100, 24);
        public void ResetArea(CollideRect rect)
        {
            fullarea = rect;
        }
        public CollideRect CurrentArea => fullarea;
        public override void Draw()
        {
            Vector2 hpPos = Vertical ? new Vector2(CollidingBox.GetCentre().X, FullRect.Down + 45) : new Vector2(CollidingBox.X - 30, CollidingBox.GetCentre().Y);
            FormalDraw(Image, hpPos, CurrentDrawingSettings.UIColor, 1.1f, 0.0f, ImageCentre);
            if (HeartAttribute.KR && PlayerInstance.hpControl.KRHPExist)
            {
                NormalFont.CentreDraw("*KR*", FullRect.GetCentre(), Color.Purple, 1.0f, 0.1f);
                Depth = 0.06f;
                FormalDraw(hpBar, KRRect.ToRectangle(), hpKRCurrent);
            }
            Depth = 0.05f;
            FormalDraw(hpBar, collidingBox.ToRectangle(), hpExistCurrent);
            Depth = 0.0f;
            FormalDraw(hpBar, FullRect.ToRectangle(), hpLoseCurrent);
            if (HeartAttribute.BuffedLevel > 0)
            {
                //FightFont.Draw($"B.LV. {HeartAttribute.BuffedLevel:F2}", new Vector2(FullRect.Left, collidingBox.Y - 10), Color.White, 0.5f, 0);
            }

            string hpString;
            HeartAttribute.HP = Max(0, HeartAttribute.HP);
            var RoundHP = Round(HeartAttribute.HP, 2);
            var CeilHP = Ceiling(HeartAttribute.HP);
            if (((CurrentScene as FightScene).Mode & GameMode.Practice) != 0) hpString = "inf";
            else
            {
                if (((CurrentScene as FightScene).Mode & GameMode.Buffed) == 0 && HeartAttribute.BuffedLevel == 0)
                    hpString = $"{CeilHP} / {Ceiling(HeartAttribute.MaxHP)}";
                else if (HeartAttribute.BuffedLevel != 0)
                {
                    hpString = $"{RoundHP:F2} / {Ceiling(HeartAttribute.MaxHP)}";
                }
                else
                {
                    float hp = HeartAttribute.HP, max = HeartAttribute.MaxHP;
                    float scale = 20 / max;
                    string hptext = string.Format("{0:N2}", hp * scale);
                    if (hptext.Length == 1) hptext += "0";
                    hpString = hptext + " / 20.00";
                }
                if (Heart.Shields != null && Heart.Shields.Circle.Consumption > 1)
                {
                    hpString += $"/ {(Heart.Shields.Circle.Consumption * 8) - 8:F2}";
                }
            }
            if (!Vertical)
            {
                FightFont.Draw(hpString, new Vector2(FullRect.Right + 20, collidingBox.Y + 1), Buffed ? Color.Gold : CurrentDrawingSettings.UIColor);
            }
            else
            {
                if (((CurrentScene as FightScene).Mode & GameMode.Practice) != 0)
                {
                    FightFont.Draw(hpString, new Vector2(FullRect.Right + 1, collidingBox.Y + 20), CurrentDrawingSettings.UIColor, 1, 0, 0);
                }
                else
                {
                    Vector2 pos = new(FullRect.GetCentre().X, FullRect.Down + 18);
                    FightFont.CentreDraw(RoundHP.ToString(), pos, CurrentDrawingSettings.UIColor, 1, 0, 0);
                    pos = new Vector2(FullRect.GetCentre().X, FullRect.Up - 16);
                    FightFont.CentreDraw(HeartAttribute.MaxHP.ToString(), pos, CurrentDrawingSettings.UIColor, 1, 0, 0);
                }
            }
        }

        public override void Update()
        {
            CalculatePosition();

            float scale = 1;
            if (Buffed)
                scale = MathHelper.Clamp(1.25f - (PlayerInstance.hpControl.LostSpeed * 0.5f), 0.1f, 1.0f);
            scale = 1 - scale;
            hpExistCurrent = Color.Lerp(hpExistColor, new(128, 32, 32), scale);
            hpLoseCurrent = Color.Lerp(hpLoseColor, new(128, 32, 32), scale);
            hpKRCurrent = Color.Lerp(hpKRColor, new(128, 32, 32), scale);
        }

        private void CalculatePosition()
        {
            FullRect = fullarea;
            collidingBox = fullarea;

            if (Vertical)
            {
                collidingBox.Height = HeartAttribute.HP * fullarea.Height / HeartAttribute.MaxHP;
                collidingBox.Y += fullarea.Height - collidingBox.Height;
            }
            else
            {
                collidingBox.Width = HeartAttribute.HP * fullarea.Width / HeartAttribute.MaxHP;
            }

            float KRSize = Min(PlayerInstance.hpControl.KRHP, HeartAttribute.HP) * 100.0f / HeartAttribute.MaxHP;
            if (!Vertical)
            {
                KRRect.X = Math.Max(collidingBox.X + 1, collidingBox.Right - KRSize);
                KRRect.Y = collidingBox.Y;
                KRRect.Width = collidingBox.Right - KRRect.X + 1;
                KRRect.Height = collidingBox.Height;
            }
            else
            {
                KRRect.Y = Math.Max(collidingBox.Y + 1, collidingBox.Down - KRSize);
                KRRect.X = collidingBox.X;
                KRRect.Height = collidingBox.Down - KRRect.Y + 1;
                KRRect.Width = collidingBox.Width;
            }
        }
    }
}