using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public partial class MikuFight
    {
        private class MainFight : IWaveSet
        {
            public SongInformation Attributes => null;
            public MainFight()
            {
            }

            public string Music => "";

            public string FightName => "";

            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }

            public void ExtremePlus()
            {
                ballDegree = Sin(Gametime) * 0.3f + 0.35f;
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                throw new NotImplementedException();
            }

            class DelProgress : Entity
            {
                float curTime = 0;
                float totalTime = (4 * 60 + 46) * 62.5f;
                float prog;
                public override void Draw()
                {
                    Font.Japanese.CentreDraw(prog.ToString() + " %", new(550, 440), Color.DimGray * 0.7f, 0.7f, 0.0f);
                }

                public override void Update()
                {
                    curTime += 1f;
                    prog = MathF.Round(curTime / totalTime * 100, 1);
                }
            }

            static float ballDegree = 0.0f;

            public void Start()
            {
                GameStates.InstanceCreate(new DelProgress());
                ScreenDrawing.BackGroundRendering.InsertProduction(new EffectLibrary.BackGround());
                ScreenDrawing.Shaders.Filter p1, p2;
                ScreenDrawing.SceneRendering.InsertProduction(p1 = new ScreenDrawing.Shaders.Filter(Shaders.NeonLine, 0.5f));
                (p1.CurrentShader as GlobalResources.Effects.NeonLineShader).DrawingColor = Color.White * 0.4f;
                (p1.CurrentShader as GlobalResources.Effects.NeonLineShader).Speed = 0.4f;
                ScreenDrawing.SceneRendering.InsertProduction(p2 = new ScreenDrawing.Shaders.Filter(Shaders.Cos1Ball, 0.6f));
                AddInstance(new TimeRangedEvent(0, 10000, () =>
                {
                    var v = (p2.CurrentShader as GlobalResources.Effects.BallShapingShader);
                    v.Intensity = ballDegree;
                    v.ScreenScale = 1 + (ballDegree / 2) * (ballDegree / 2) / 2;
                }));
                LyricSystem.RunShow();
                SetGreenBox();
            }
        }
    }
}