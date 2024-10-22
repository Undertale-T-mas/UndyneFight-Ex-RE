﻿using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal partial class Traveler_at_Sunset
    {
        public partial class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (226f / 60f)) { }

            public SongInformation Attributes => new Information();
            Blur Blur;
            RenderProduction production, production1, production2, production3, production4, production5;
            GlobalResources.Effects.StepSampleShader StepSample;
            RGBSplitting splitter = new();
            #region disused

            public void Hard()
            {
                throw new NotImplementedException();
            }
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }

            #endregion
            Winder r = new(), s = null;
            static Arrow.UnitEasing easeA = null, easeB = null, easeC = null, easeD, easeE, easeF, easeG, easeH, easeI, easeJ, easeK;
            static Arrow.EnsembleEasing easeX = null, easeY = null, easeZ = null, easeU, easeV, easeW, easeS2, easeT2, easeS1, easeT1;
            static Arrow.ClassicApplier easeK1;

            GridShader shaderGrid;

            Sans sans;
            public void Start()
            {
                Loader.RootDirectory = "Content";
                shaderGrid = new();
                if (CurrentDifficulty == Difficulty.Noob)
                {
                    DelayBeat(1, () =>
                    {
                        GameStates.ResetScene(new Anomaly());
                    });
                }
                RegisterFunction("Drum", () =>
                {
                    ScreenDrawing.ScreenAngle = 0;
                    float time = Arguments[0];
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    },
                    EaseOut(BeatTime(time) / 4f, Arguments[1], EaseState.Quad),
                    EaseIn(BeatTime(time) / 4f * 3, -Arguments[1], EaseState.Quad)
                    );
                });
                RegisterFunction("SetScreenAngle", () =>
                {
                    ScreenDrawing.ScreenAngle = Arguments[0];
                });
                RegisterFunction("SetScreenScale", () =>
                {
                    ScreenDrawing.ScreenScale = Arguments[0];
                });
                #region Easing
                AddInstance(easeA = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.75f),
                    RotationEase = LinkEase(
                        Stable(BeatTime(0.5f), 0),
                        EaseOut(BeatTime(2.2f), 0, -45, EaseState.Sine))
                });
                AddInstance(easeB = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.75f),
                    RotationEase = LinkEase(
                        Stable(BeatTime(0.5f), -90),
                        EaseOut(BeatTime(2.2f), -90, -45, EaseState.Sine))
                });
                AddInstance(easeC = new Arrow.UnitEasing());
                AddInstance(easeD = new Arrow.UnitEasing());
                AddInstance(easeE = new Arrow.UnitEasing());
                AddInstance(easeF = new Arrow.UnitEasing());
                AddInstance(easeG = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.5f),
                    PositionEase = LinkEase(
                        Stable(BeatTime(1.4f), new Vector2(0, -245)),
                        EaseOut(BeatTime(1.1f), new Vector2(0, -245), new Vector2(0), EaseState.Elastic))
                });
                AddInstance(easeH = new Arrow.UnitEasing());
                AddInstance(easeI = new Arrow.UnitEasing());
                AddInstance(easeJ = new Arrow.UnitEasing());
                AddInstance(easeK = new Arrow.UnitEasing());

                easeX = new();
                AddInstance(easeX);
                easeY = new();
                AddInstance(easeY);
                easeZ = new();
                AddInstance(easeZ);
                easeU = new();
                AddInstance(easeU);
                easeV = new();
                AddInstance(easeV);
                easeW = new();
                AddInstance(easeW);
                easeS1 = new();
                AddInstance(easeS1);
                easeT1 = new();
                AddInstance(easeT1);
                easeS2 = new();
                AddInstance(easeS2);
                easeT2 = new();
                AddInstance(easeT2);
                #endregion
                production = Blur = new Blur(0.505f);
                production1 = new Filter(Shaders.StepSample, 0.51f);
                splitter = new RGBSplitting(0.9f) { Disturbance = false };
                StepSample = Shaders.StepSample;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                splitter.Intensity = 0.0f;
                ScreenDrawing.BoundColor = Color.White;
                ScreenDrawing.SceneRendering.InsertProduction(production);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(splitter);
                CreateEntity(r);
                GametimeDelta = -3.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(320, 240);
                ScreenDrawing.MasterAlpha = 0f;
                ScreenDrawing.ScreenScale = 2f;
                CreateEntity(sans = new Sans(Loader));
                bool jump = false;
                if (GameStates.difficulty == 0) jump = false;
                if (jump)
                {
                    float beat;
                    beat = 1020;
                    sans.Alpha = 0.0f;
                    GametimeDelta += BeatTime(beat);

                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                    Settings.GreenTap = true;
                }
                else sans.Alpha = 1.0f;
                if (GameStates.difficulty == 2)
                    sans.Visible = false;
            }
        }
    }
}