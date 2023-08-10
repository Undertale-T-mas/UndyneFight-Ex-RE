using System.Drawing;

namespace UndyneFight_Ex.Server
{
    public static class UFConsole
    {
        public static void WriteLine(string msg)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            string[] res = msg.Split('\0');
            SetStyle();
            Console.Write(res[0]);
            for (int i = 1; i < res.Length; i++)
            {
                string cur = res[i];
                int j;
                string control = string.Empty;
                for (j = 0; cur[j] != ']'; j++) control += cur[j];
                SetStyle(control);
                Console.Write(cur[(j + 1)..]); //, foreGroundColor);
            }
            Console.WriteLine();
        }
        static Color foreGroundColor;
        private static void SetStyle(string style = "#White")
        { 
            if (style[0] == '#')
            {
                foreGroundColor = style[1..] switch
                {
                    "White" => Color.White,
                    "Red" => Color.Red,
                    "Magenta" => Color.Magenta,
                    "Cyan" => Color.Cyan,
                    "Green" => Color.Lime,
                    "Blue" => Color.Blue,
                    "Yellow" => Color.Yellow,
                    _ => Color.Black //throw new ArgumentException($"{nameof(style)} input is in incorrect format.")
                };
                Console.ForegroundColor = style[1..] switch { 
                    "White" => ConsoleColor.White,
                    "Red" => ConsoleColor.Red,
                    "Magenta" => ConsoleColor.Magenta,
                    "Cyan" => ConsoleColor.Cyan,
                    "Green" => ConsoleColor.Green,
                    "Blue" => ConsoleColor.Blue,
                    "Yellow" => ConsoleColor.Yellow,
                    _ => ConsoleColor.Black // throw new ArgumentException($"{nameof(style)} input is in incorrect format.")
                };
            }
        }
    }
}