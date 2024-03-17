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
            for (int i = 0; i < len; i++) a += '*';

            return a;
        }

        public override void Update()
        {
            base.Update();
            if (!this.ModuleSelected) return;
            if (IsKeyPressed120f(InputIdentity.Tab))
            {
                drawingString = _inProtected ? Result : GenerateProtect();
                _inProtected = !_inProtected;
            }
        }
    }
}