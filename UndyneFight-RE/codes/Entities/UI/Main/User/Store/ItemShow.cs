using Microsoft.Xna.Framework;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    public class ItemShowUI : SmartSelector
    {
        public ItemShowUI()
        {

            this.KeyEvent = () => {
                if (IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = FocusID;
                    if (id + 1 < all.Length)
                    {
                        currentFocus.OffFocus();
                        all[id + 1].OnFocus();
                    }
                }
                else if (IsKeyPressed120f(InputIdentity.MainUp))
                {
                    int id = FocusID;
                    if (id > 1)
                    {
                        currentFocus.OffFocus();
                        all[id - 1].OnFocus();
                    }
                }
                if (IsKeyPressed120f(InputIdentity.Confirm))
                {
                    currentFocus?.ConfirmKeyDown();
                }
            };
            this.OnSelected += ItemUsingUI_OnSelected;
        }

        private void ItemUsingUI_OnSelected()
        {
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Draw()
        {
            GLFont font = FightResources.Font.NormalFont;
            font.CentreDraw("Souvenir Collection", new(480, 60), Color.White, 1.4f, 0.1f);
        }
    }
}