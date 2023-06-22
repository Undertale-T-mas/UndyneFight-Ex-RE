using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {

        public static partial class Effects
        {
            public class MainShader : Shader
            {
                public MainShader(Effect eff) : base(eff)
                {
                    PartEvents = GetDictionary(new string[] { "SplitColorDrawing" }, new Action<Effect>[] { (s) => {
                        if (Fight.Functions.Gametime % 2 == 0)
                        {
                            var screenSize = GameMain.ScreenSize;
                            Vector2 screenDiv = new Vector2((0.7f + splitDegree) / screenSize.X, (0.7f + splitDegree) / screenSize.Y);
                            s.Parameters["v1"].SetValue(new Vector2(GetRandom(-2, 2), GetRandom(-2, 2)) * screenDiv);
                            s.Parameters["v2"].SetValue(new Vector2(GetRandom(-2, 2), GetRandom(-2, 2)) * screenDiv);
                            s.Parameters["v3"].SetValue(new Vector2(GetRandom(-2, 2), GetRandom(-2, 2)) * screenDiv);
                        }
                    } });
                }

                public float splitDegree { private get; set; } = 1;
            }
        }
    }
}