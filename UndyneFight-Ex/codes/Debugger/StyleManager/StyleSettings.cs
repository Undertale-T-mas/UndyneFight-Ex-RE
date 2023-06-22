namespace UndyneFight_Ex.Debugging
{
    internal class StyleManager : ISetAble
    {
        internal static ColorStyleManager ColorStyleManager { get; } = new ColorStyleManager();

        public void SetTo(string settings)
        {
            throw new System.NotImplementedException();
        }
    }
}