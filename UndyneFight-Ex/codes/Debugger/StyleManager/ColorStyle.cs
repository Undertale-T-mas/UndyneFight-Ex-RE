using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static UndyneFight_Ex.Debugging.DebugWindow;

namespace UndyneFight_Ex.Debugging
{
    internal interface ISetAble
    {
        void SetTo(string settings);
    }

    internal abstract class IColorStyle
    {
        protected abstract Color ClassColor { get; }
        protected abstract Color ConstancyColor { get; }
        protected abstract Color FunctionColor { get; }
        protected abstract Color HeadColor { get; }
        protected abstract Color KeyWordColor { get; }
        protected abstract Color OperatorColor { get; }
        protected abstract Color PropertyColor { get; }
        protected abstract Color SplitterColor { get; }
        protected abstract Color StructColor { get; }
        protected abstract Color VariableColor { get; }
        protected abstract Color UnKnownColor { get; }

        public Color GetColor(DebugWindow.WordType type) => type switch
        {
            WordType.Head => HeadColor,
            WordType.KeyWord => KeyWordColor,
            WordType.Constancy => ConstancyColor,
            WordType.Operator => OperatorColor,
            WordType.Class => ClassColor,
            WordType.Struct => StructColor,
            WordType.Function => FunctionColor,
            WordType.Variable => VariableColor,
            WordType.Property => PropertyColor,
            WordType.Splitter => SplitterColor,
            WordType.Unknown => UnKnownColor,
            _ => throw new System.NotImplementedException(),
        };
    }
    internal class ColorStyleManager : ISetAble
    {
        private class DotnetStyle : IColorStyle
        {
            protected override Color ClassColor => Color.LimeGreen;

            protected override Color ConstancyColor => Color.Silver;

            protected override Color FunctionColor => Color.Yellow;

            protected override Color HeadColor => Color.Magenta;

            protected override Color KeyWordColor => Color.LightBlue;

            protected override Color OperatorColor => Color.Goldenrod;

            protected override Color PropertyColor => Color.PaleGoldenrod;

            protected override Color SplitterColor => Color.Gold;

            protected override Color StructColor => Color.Lime;

            protected override Color VariableColor => Color.White;

            protected override Color UnKnownColor => Color.Red;
        }

        public ColorStyleManager()
        {
            allColors.Add("DotnetStyle", new DotnetStyle());
            currentStyle = allColors["DotnetStyle"];
        }
        private readonly Dictionary<string, IColorStyle> allColors = new();
        public Color GetColor(WordType type)
        {
            return currentStyle.GetColor(type);
        }

        public void SetTo(string settings)
        {
            throw new System.NotImplementedException();
        }

        private readonly IColorStyle currentStyle;

    }
}