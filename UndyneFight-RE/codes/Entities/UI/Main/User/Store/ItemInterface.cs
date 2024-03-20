using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    public class ItemUsingUI : SmartSelector
    {
        public ItemUsingUI()
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
        }
    }
}