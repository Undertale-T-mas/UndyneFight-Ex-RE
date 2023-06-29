using static UndyneFight_Ex.FightResources.Font;
using Microsoft.Xna.Framework;
using System.Xml;
using System;
using System.Linq;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class UserUI 
    {
        internal class LoginUI : SmartSelector
        {
            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.5f);
            }
            public LoginUI()
            {
                //L = 65, R = 325
                NameInitialize();
                this.collidingBox.Size = new Vector2(135, 75);
                this.SetChild();
                this.OneSelectionOnly = true;
            }

            private void NameInitialize()
            {
                allNames = PlayerManager.playerInfos.Keys.ToArray();
            }

            private string[] allNames;
            private void SetChild()
            {
                ChildObjects.Clear();
                ChildObjects.Add(new SmartInputer(allNames, this, new CollideRect(new Vector2(571, 66), new Vector2(330, 50))) { FontScale = 1.2f });
                ChildObjects.Add(new TextInputer(this, new CollideRect(new Vector2(571, 156), new Vector2(330, 50))) { FontScale = 1.2f });
            }
            public override void Draw()
            {
                NormalFont.CentreDraw("Login", this.Centre, DrawingColor, 1.8f * _secondaryScale, 0.1f);

                NormalFont.CentreDraw("Account", new Vector2(480, 65), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("Name", new Vector2(480, 100), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 105), new(410, 125), Color.White);
                DrawLine(new(410, 125), new(550, 125), Color.White);

                NormalFont.CentreDraw("Pass", new Vector2(480, 155), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("code", new Vector2(480, 190), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 195), new(410, 215), Color.White);
                DrawLine(new(410, 215), new(550, 215), Color.White);
            }
            float _secondaryScale = 1.0f;

            public override void Update()
            { 
                this.Centre = new Vector2(204, 84);
                base.Update();
                if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    this._secondaryScale = MathHelper.Lerp(_secondaryScale, 1.1f, 0.1f);
                }
                else _secondaryScale = MathHelper.Lerp(_secondaryScale, 1.0f, 0.1f);
            } 
        }
    }
}