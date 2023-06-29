
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    internal class SmartInputer : TextInputer
    {
        private string[] _tips;
        public SmartInputer(string[] tips, ISelectChunk father, CollideRect area) : base(father, area)
        {
            this._tips = tips; 
        }
        float SimilarityChar(char a, char b)
        {
            if (a == b) return 1f;
            if (a >= 'a' && a <= 'z') a = (char)(a - ('a' - 'A'));
            if (b >= 'a' && b <= 'z') b = (char)(b - ('a' - 'A'));
            if (a == b) return 0.5f;
            return 0;
        }
        float SimilarityString(string origin, string str2)
        {
            if (origin == str2) return 1;
            int len1, len2, minLength, maxLength;
            len1 = origin.Length;
            len2 = str2.Length;
            minLength = Math.Min(len1, len2);
            maxLength = Math.Max(len1, len2);

            // The similarity of Head (0.3f)
            float total = 0;
            for(int i = 0; i < minLength; i++)
            {
                float v = SimilarityChar(origin[i], str2[i]);
                if (v == 0) break;
                total += v;
            }
            float result1 = total / (len2 > len1 ? maxLength : minLength);

            // The similarity of Distance (0.7f)
            float[,] dp = new float[len1 + 1, len2 + 1];
            for (int i = 1; i <= len1; i++) dp[i, 0] = i;
            for (int j = 1; j <= len2; j++) dp[0, j] = j;

            for (int i = 1; i <= len1; i++)
                for (int j = 1; j <= len2; j++)
                {
                    float flag = 1 - SimilarityChar(origin[i - 1], str2[j - 1]);
                    float temp1 = dp[i - 1, j] + 1;
                    temp1 = MathF.Min(temp1, dp[i, j - 1] + 1); 
                    temp1 = MathF.Min(temp1, dp[i - 1, j - 1] + flag);

                    dp[i, j] = temp1;
                }
            float result2 = (len1 - dp[len1, len2]) / len1;
            return result1 * 0.3f + result2 * 0.7f;
        }
    }
}