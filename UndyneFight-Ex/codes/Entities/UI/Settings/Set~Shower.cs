using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.Settings.SettingLibrary;

namespace UndyneFight_Ex.Settings
{
    internal class SettingsShower : Selector
    {
        private static SettingsShower instance;
        private class MenuSelections
        {
            private class BackButton : TextSelection
            {
                private readonly SettingsShower shower;

                public BackButton(SettingsShower shower) : base("Save", new Vector2(320, 440))
                {
                    Size = 0.9f;
                    this.shower = shower;
                }

                public override void SelectionEvent()
                {
                    shower.Save();
                    Back();
                    instance.Dispose();
                }
            }
            private class CancelButton : TextSelection
            {
                public CancelButton() : base("Cancel", new Vector2(160, 440))
                {
                    Size = 0.9f;
                }

                public override void SelectionEvent()
                {
                    Back();
                    instance.Dispose();
                }
            }
            private class ApplyButton : TextSelection
            {
                private readonly SettingsShower shower;

                public ApplyButton(SettingsShower shower) : base("Apply", new Vector2(480, 440))
                {
                    Size = 0.9f;
                    this.shower = shower;
                }

                public override void SelectionEvent()
                {
                    shower.Save();
                }
            }

            public static List<TextSelection> GetMenuSelections(SettingsShower shower)
            {
                return new List<TextSelection>() { new CancelButton(), new BackButton(shower), new ApplyButton(shower) };
            }
        }

        private void Save()
        {
            IEnumerable<Setting> e = from selection in Selections where selection is Setting select selection as Setting;

            SettingsResetInterface.SaveSettings(e);
        }

        public SettingsShower()
        {
            SettingsResetInterface.EnterSettings();
            instance = this;
            AutoDispose = false;
            IsCancelAvailable = false;

            Vector2 startPosition = new Vector2(320, 50);
            foreach (var v in SettingsManager.Settings)
            {
                PushSelection(Activator.CreateInstance(v, startPosition) as Setting);
                startPosition.Y += 40;
            }
            foreach (var v in MenuSelections.GetMenuSelections(this))
                PushSelection(v);

            ResetSelect();
            PlaySound(select, 0.9f);
            Selected += () => { PlaySound(select, MasterVolume.Value / 100f); };
            SelectChanged += () => { PlaySound(changeSelection, MasterVolume.Value / 100f); };
            SelectChanger += () =>
            {
                if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                {
                    if (currentSelect == 0) currentSelect = SelectionCount - 2;
                    else if (currentSelect >= SelectionCount - 3) currentSelect = SelectionCount - 4;
                    else currentSelect--;
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    if (currentSelect == SelectionCount - 4) currentSelect = SelectionCount - 2;
                    else currentSelect++;
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.MainLeft) && currentSelect >= SelectionCount - 2) currentSelect--;
                if (GameStates.IsKeyPressed120f(InputIdentity.MainRight) && currentSelect >= SelectionCount - 3) currentSelect++;

                if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                else if (currentSelect < 0) currentSelect = 0;
            };
        }
    }
}