using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UndyneFight_Ex.Debugging
{
    internal partial class DebugWindow
    {
        internal enum WordType
        {
            Head = 0,
            KeyWord = 1,
            Constancy = 2,
            Operator = 3,
            Class = 4,
            Struct = 5,
            Function = 6,
            Variable = 7,
            Property = 8,
            Splitter = 9,
            Unknown = 10,
        }
        internal static class TypeMatchingLibrary
        {
            /// <summary>
            /// 指令头字库 0.1.0版本
            /// </summary>
            private static HashSet<string> headWords_0_1_0;
            /// <summary>
            /// 关键字字库 0.1.0版本
            /// </summary>
            private static HashSet<string> keyWords_0_1_0;
            /// <summary>
            /// 类型匹配库 0.1.0版本
            /// </summary>
            private static Dictionary<string, Type> typeMatches_0_1_0;
            private static HashSet<string> operators_0_1_0;
            private static HashSet<char> splitters_0_1_0;
            public static void Initialize()
            {
                headWords_0_1_0 = new HashSet<string>() { "InsCreate", "InsKill", "Variable", "Exit", "User" };
                keyWords_0_1_0 = new HashSet<string>() { "int", "float", "double", "string", "vector2", "color" };
                operators_0_1_0 = new HashSet<string>() { "+", "-", "*", "/", "+=", "-=", "*=", "/=", "=", "%" };
                splitters_0_1_0 = new HashSet<char>() { ' ', ',', ';', '{', '}', '[', ']', '(', ')', '\'', '\"' };

                typeMatches_0_1_0 = new Dictionary<string, Type>
                {
                    { @"^\d*$", typeof(int) },
                    { @"^\d*\.\d*f$", typeof(float) },
                    { @"^\d*\.\d*d$", typeof(double) },
                    { @"^"".*""$", typeof(string) }
                };

                HeadWords = headWords_0_1_0;
                KeyWords = keyWords_0_1_0;
                TypeMatches = typeMatches_0_1_0;
                Operators = operators_0_1_0;
                Splitters = splitters_0_1_0;
            }

            private static HashSet<string> HeadWords { get; set; }
            private static HashSet<string> KeyWords { get; set; }
            private static HashSet<string> Operators { get; set; }
            private static Dictionary<string, Type> TypeMatches { get; set; }
            public static HashSet<char> Splitters { get; private set; }
            public static WordType Check(string data)
            {
                if (HeadWords.Contains(data)) return WordType.Head;
                if (KeyWords.Contains(data)) return WordType.KeyWord;
                string[] patterns = TypeMatches.Keys.ToArray();
                foreach (var pat in patterns)
                {
                    if (Regex.IsMatch(data, pat))
                        return WordType.Constancy;
                }
                if (data.Length == 1)
                    if (Splitters.Contains(data[0])) return WordType.Splitter;
                return Operators.Contains(data) ? WordType.Operator : WordType.Unknown;
            }
        }
        internal static WordType TypeChecking(string data)
        {
            return TypeMatchingLibrary.Check(data);
        }
    }
}
