
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    internal class PasswordInputer : TextInputer
    {
        public PasswordInputer(ISelectChunk father, CollideRect area) : base(father, area)
        {
            this.ResultChanged += PasswordProtect; UpdateIn120 = true;
        }

        private bool _inProtected = true;
        private void PasswordProtect()
        {
            if (!_inProtected) { return; }
            string a = GenerateProtect();
            this.drawingString = a;
        }

        private string GenerateProtect()
        {
            int len = this.drawingString.Length;
            string a = "";
            for (int i = 0; i < len; i++)
            {
                a += '*';
            }

            return a;
        }

        public override void Update()
        {
            base.Update();
            if (!this.ModuleSelected) return;
            if (IsKeyPressed120f(InputIdentity.Tab))
            {
                if (_inProtected) this.drawingString = Result;
                else this.drawingString = GenerateProtect();
                _inProtected = !_inProtected;
            }
        }
    }
}