using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Net.Sockets;
using UndyneFight_Ex.SongSystem;
using System.ComponentModel.Design.Serialization;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake.UI
{
    public class ItemUsingUI : SmartSelector
    {
        public ItemUsingUI()
        {

            this.KeyEvent = () => {
                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = FocusID;
                    if (id + 1 < all.Length)
                    {
                        currentFocus.OffFocus();
                        all[id + 1].OnFocus();
                    }
                }
                else if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                {
                    int id = FocusID;
                    if (id > 1)
                    {
                        currentFocus.OffFocus();
                        all[id - 1].OnFocus();
                    }
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
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