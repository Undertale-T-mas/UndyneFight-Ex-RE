using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class WaveShader : Shader
            {
                Effect _eff;
                public WaveShader(Effect eff) : base(eff)
                {
                    _eff = eff;
                    StableEvents = (x) =>
                    {
                        Time += 0.5f * Speed * TimeElapsed / 480f;

                        x.Parameters["iTime"].SetValue(Time);

                        x.Parameters["iIntensity1"].SetValue(Intensity1 / 640f);
                        x.Parameters["iIntensity2"].SetValue(Intensity2 / 640f);
                        x.Parameters["iIntensity3"].SetValue(Intensity3 / 640f);

                        x.Parameters["iFrequency1"].SetValue(Frequency1 * MathF.PI * 960f);
                        x.Parameters["iFrequency2"].SetValue(Frequency2 * MathF.PI * 960f);
                        x.Parameters["iFrequency3"].SetValue(Frequency3 * MathF.PI * 960f);

                        _eff.CurrentTechnique = MathF.Abs(Intensity3) < 0.1f ? _eff.Techniques[0] : _eff.Techniques[1];
                    };
                }

                public float Time { private get; set; } = 0.0f;
                public float Speed { private get; set; } = 1.0f;
                public float Intensity1, Intensity2, Intensity3;
                public float Frequency1, Frequency2, Frequency3;
            }
        }
    }
}