using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Entities.Player;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.BSet;
using C = Microsoft.Xna.Framework.Color;
using ES = UndyneFight_Ex.Entities.SimplifiedEasing.EaseState;
using V = Microsoft.Xna.Framework.Vector2;
using V3 = Microsoft.Xna.Framework.Vector3;
using System.Text;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;
using System.Collections;
using static Rhythm_Recall.Resources.BadAppleRE;
using System.Runtime.Serialization.Formatters;
using Microsoft.Xna.Framework.Media;
namespace Rhythm_Recall.Waves
{
    internal partial class BadApple_RE : IChampionShip
    {
        partial class Game : WaveConstructor, IWaveSet
        {
            public static Game game;

            private GlobalResources.Effects.StepSampleShader StepSample;

            private Functions.ScreenDrawing.Shaders.RGBSplitting splitter;

            private GlobalResources.Effects.PolarShader Polar;

            private GlobalResources.Effects.StepSampleShader step = FightResources.Shaders.StepSample;

            private GlobalResources.Effects.BlurShader blur = FightResources.Shaders.Blur;

            ScreenDrawing.Shaders.Blur Blurs;

            private C CW = C.White;

            Shader shader1;
            Shader shader2;
            RenderProduction shaderProduction1, shaderProduction2, shaderProduction3, shaderProduction4, shaderProduction5;
            RenderProduction SidePro;
            RenderProduction Glitch;
            RenderProduction WaveformR = null;
            Shader Waveform;
            private static RenderProduction cameraProduction;

            public static float Bbpm = 62.5f / (138f / 60f);
            public static float bpm = 28.8461533f;
            public static float DT = 62.5f / (560/*bpm*/ / 60f);
            public static float FD = 16.8725f / 4f;
            public float hz = 62.5f / (60f) / 1;

            private RenderProduction production1;

            private RenderProduction production2;

            private GlobalResources.Effects.CameraShader Effect3D;

            GlobalResources.Effects.StepSampleShader DreadStep;

            RenderProduction Dreadpro1;

            ScreenDrawing.Shaders.RGBSplitting Dreadsplit = new();

            Shader BadShader;

            RenderProduction BadShaderrend;

            private Rainer rainer;

            public static Texture2D HandImage;

            private static GameObject hull1, hull2;

            Glitching BlurG;
            Blur BlurB;
            RGBS rgb;
            private class Ef : Entity
            {
                public bool scf { get; set; } = false;
                public bool scf2 { get; set; } = false;
                public bool shader { get; set; } = false;
                public Vector2 centre { set; get; }
                public Color Color { set; get; }
                public float Alpha { set; get; } = 1;
                public string Texts { get; set; }
                public float rot { set; get; }
                public bool Mirror { get; set; } = false;

                public bool AutoDispose = true;
                public bool edging { get; set; } = false;
                public float edgingsize { get; set; } = 15f;
                public C edgingColor { get; set; } = C.Black;
                int Type;
                public GLFont font = BadAppleFont;

                /// <summary>
                ///
                /// </summary>
                /// <param name="text">BadAppleで使っている文字のみ使用可能</param>
                public Ef(string text, Vector2 vec, Color col, float size)
                {
                    Color = col;
                    Texts = text;
                    centre = vec;
                    Size = new V(size / 8);
                    Depth = 1;
                    Centre = centre;

                }
                public Ef(string text, Vector2 vec, Color col, float size, float depth)
                {
                    Color = col;
                    Texts = text;
                    centre = vec;
                    Size = new V(size / 8);
                    Depth = depth;
                    Centre = centre;
                }
                public Ef(string text, Color col, float size)
                {
                    Color = col;
                    Texts = text;
                    centre = V.Zero;
                    Size = new V(size / 8);
                    Depth = 1;
                    Centre = centre;
                }

                bool s = true;
                public override void Draw()
                {
                    if (scf)
                    {

                        controlLayer = UIS().UISurface;

                    }
                    if (scf2)
                    {

                        controlLayer = UIS2().UISurface;

                    }
                    if (shader)
                    {
                        controlLayer = TextShader().UISurface;
                    }
                    if (s)
                    {
                        if (edging)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.X * edgingsize, rot) + GetVector2((Size.Y * edgingsize) - (Size.Y * edgingsize) * i, rot + 90);
                                font.CentreDraw(Texts, centre + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.X * edgingsize, rot + 180) + GetVector2((Size.Y * edgingsize) - (Size.Y * edgingsize) * i, rot + 270);
                                font.CentreDraw(Texts, centre + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.Y * edgingsize, rot + 90) + GetVector2((Size.X * edgingsize) - (Size.X * edgingsize) * i, rot + 180);
                                font.CentreDraw(Texts, centre + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.Y * edgingsize, rot - 90) + GetVector2((Size.X * edgingsize) - (Size.X * edgingsize) * i, rot);
                                font.CentreDraw(Texts, centre + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                        }
                        font.CentreDraw(Texts, centre, Color * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth);
                        if (Mirror)
                        {
                            V Mvec = new V(640, 480) - centre;
                            font.CentreDraw(Texts, Mvec, Color * Alpha, Size, GetRadian(rot), Depth);
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.X * edgingsize, rot) + GetVector2((Size.Y * edgingsize) - (Size.Y * edgingsize) * i, rot + 90);
                                font.CentreDraw(Texts, Mvec + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.X * edgingsize, rot + 180) + GetVector2((Size.Y * edgingsize) - (Size.Y * edgingsize) * i, rot + 270);
                                font.CentreDraw(Texts, Mvec + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.Y * edgingsize, rot + 90) + GetVector2((Size.X * edgingsize) - (Size.X * edgingsize) * i, rot + 180);
                                font.CentreDraw(Texts, Mvec + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                V vec = GetVector2(Size.Y * edgingsize, rot - 90) + GetVector2((Size.X * edgingsize) - (Size.X * edgingsize) * i, rot);
                                font.CentreDraw(Texts, Mvec + vec, edgingColor * Alpha, Size, (rot % 360.0f) * (float)(Math.PI / 180), Depth - 0.01f);
                            }
                        }
                    }
                }
                float remove = 0;
                public override void Update()
                {
                    remove++;
                    if (AutoDispose)
                    {

                        if (remove > 3000)
                        { Dispose(); }
                    }
                    if (scf || scf2 || shader)
                    {
                        if (remove < 4) s = false;
                        else s = true;
                    }

                }
                public void AutoDis(float time)
                {
                    AddInstance(new InstantEvent(time, () => { Alpha = 0; Dispose(); }));
                }
                #region easing
                public void vecOut(V vec1, V vec2, float time, ES type)
                {
                    RunEase((s) => { centre = s; }, LinkEase(Stable(0, vec1), EaseOut(time, vec2 - vec1, type)));
                }
                public void vecOut(V vec1, V vec2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { centre = s; }, LinkEase(Stable(0, vec1), EaseOut(time, vec2 - vec1, type)));
                    }));
                }
                public void vecIn(V vec1, V vec2, float time, ES type)
                {
                    RunEase((s) => { centre = s; }, LinkEase(Stable(0, vec1), EaseIn(time, vec2 - vec1, type)));
                }
                public void vecInS(V vec1, float time, ES type)
                {
                    RunEase((s) => { centre = s; }, EaseIn(time, centre, centre + vec1, type));
                }
                public void vecIn(V vec1, V vec2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { centre = s; }, LinkEase(Stable(0, vec1), EaseIn(time, vec2 - vec1, type)));
                    }));
                }
                public void alphaOut(float al1, float al2, float time, ES type)
                {
                    RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, al1), EaseOut(time, al2 - al1, type)));
                }
                public void alphaOut(float al1, float al2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, al1), EaseOut(time, al2 - al1, type)));
                    }));
                }
                public void alphaIn(float al1, float al2, float time, ES type)
                {
                    RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, al1), EaseIn(time, al2 - al1, type)));
                }
                public void alphaIn(float al1, float al2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, al1), EaseIn(time, al2 - al1, type)));
                    }));
                }
                public void rotOut(float rot1, float rot2, float time, ES type)
                {
                    RunEase((s) => { rot = s; }, LinkEase(Stable(0, rot1), EaseOut(time, rot2 - rot1, type)));
                }
                public void rotOut(float rot1, float rot2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { rot = s; }, LinkEase(Stable(0, rot1), EaseOut(time, rot2 - rot1, type)));
                    }));
                }
                public void rotIn(float rot1, float rot2, float time, ES type)
                {
                    RunEase((s) => { rot = s; }, LinkEase(Stable(0, rot1), EaseIn(time, rot2 - rot1, type)));
                }
                public void rotIn(float rot1, float rot2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { rot = s; }, LinkEase(Stable(0, rot1), EaseIn(time, rot2 - rot1, type)));
                    }));
                }
                public void colOut(Color col1, Color col2, float time, ES type)
                {
                    RunEase((s) => { Color = Color.Lerp(col1, col2, s); }, LinkEase(Stable(0, 0), EaseOut(time, 1, type)));
                }
                public void colOut(Color col1, Color col2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Color = Color.Lerp(col1, col2, s); }, LinkEase(Stable(0, 0), EaseOut(time, 1, type)));
                    }));
                }
                public void colIn(Color col1, Color col2, float time, ES type)
                {
                    RunEase((s) => { Color = Color.Lerp(col1, col2, s); }, LinkEase(Stable(0, 0), EaseIn(time, 1, type)));
                }
                public void colIn(Color col1, Color col2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Color = Color.Lerp(col1, col2, s); }, LinkEase(Stable(0, 0), EaseIn(time, 1, type)));
                    }));
                }
                public void sizeOut(V size1, V size2, float time, ES type)
                {
                    RunEase((s) => { Size = s / 8; }, LinkEase(Stable(0, size1), EaseOut(time, size2 - size1, type)));
                }
                public void sizeOut(V size1, V size2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Size = s / 8; }, LinkEase(Stable(0, size1), EaseOut(time, size2 - size1, type)));
                    }));
                }
                public void sizeIn(V size1, V size2, float time, ES type)
                {
                    RunEase((s) => { Size = s / 8; }, LinkEase(Stable(0, size1), EaseIn(time, size2 - size1, type)));
                }
                public void sizeIn(V size1, V size2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Size = s / 8; }, LinkEase(Stable(0, size1), EaseIn(time, size2 - size1, type)));
                    }));
                }
                public void sizeOut(float size1, float size2, float time, ES type)
                {
                    RunEase((s) => { Size = new V(s / 8); }, LinkEase(Stable(0, size1), EaseOut(time, size2 - size1, type)));
                }
                public void sizeOut(float size1, float size2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Size = new V(s / 8); }, LinkEase(Stable(0, size1), EaseOut(time, size2 - size1, type)));
                    }));
                }
                public void sizeIn(float size1, float size2, float time, ES type)
                {
                    RunEase((s) => { Size = new V(s / 8); }, LinkEase(Stable(0, size1), EaseIn(time, size2 - size1, type)));
                }
                public void sizeIn(float size1, float size2, float delay, float time, ES type)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        RunEase((s) => { Size = new V(s / 8); }, LinkEase(Stable(0, size1), EaseIn(time, size2 - size1, type)));
                    }));
                }
                #endregion
                float T(float e)
                {
                    return e * (62.5f / (138f / 60) / 1);
                }
                public void TSEf(float time = 0)
                {
                    Alpha = 0;
                    AddInstance(new InstantEvent(time, () =>
                    {
                        alphaOut(0, 1, T(4), ES.Cubic);
                        alphaIn(1, 0, T(4), T(4), ES.Cubic);
                        vecIn(centre, centre + new V(100), T(4), T(4), ES.Cubic);
                        vecOut(centre + new V(-50), centre, T(4), ES.Cubic);
                        AutoDis(T(8));
                    }));
                }

                public override void Dispose()
                {
                    centre = new V(8000);
                    controlLayer = Surface.Normal;
                    base.Dispose();
                }
            }
            #region Lostmemoryの時計
            public static class DrawLine
            {
                public class NormalLine : Entity
                {
                    public float duration;

                    public float x1;

                    public float y1;

                    public float x2;

                    public float y2;

                    public Color color = Color.White;

                    public float depth;

                    public float alpha = 1f;

                    public int time;

                    public float speed = 1f;

                    public NormalLine(float x1, float y1, float x2, float y2, float duration, float alpha, Color color, float depth)
                    {
                        this.x1 = x1;
                        this.y1 = y1;
                        this.x2 = x2;
                        this.y2 = y2;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.depth = depth;
                    }

                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), 4f, color * alpha, depth);
                        base.Depth = 0.99f;
                    }

                    public override void Update()
                    {
                        time++;
                        if ((float)time == duration)
                        {
                            Dispose();
                        }
                    }
                }

                public class Linerotate : Entity
                {
                    public float duration;

                    public float xCenter;

                    public float yCenter;

                    public float rotate;

                    public Color color = Color.White;

                    public float depth;

                    public float alpha = 1f;

                    public int time;

                    public float speed = 1f;

                    public Linerotate(float xCenter, float yCenter, float rotate, float duration, float alpha, Color color, float depth)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.depth = depth;
                    }

                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new Vector2(xCenter - Functions.Tan(rotate) * yCenter, 0f), new Vector2(xCenter + Functions.Tan(rotate) * (480f - yCenter), 480f), 4f, color * alpha, depth);
                        base.Depth = depth;
                    }

                    public override void Update()
                    {
                        time++;
                        if ((float)time == duration)
                        {
                            Dispose();
                        }
                    }
                }

                public class Linerotatelong : Entity
                {
                    public float duration;

                    public float xCenter;

                    public float yCenter;

                    public float rotate;

                    public float length;

                    public float width = 4f;

                    public Color color = Color.White;

                    public float alpha = 1f;

                    public int time;

                    public float speed = 1f;

                    public Linerotatelong(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, Color color)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.length = length;
                        this.color = color;
                    }

                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new Vector2(xCenter, yCenter), new Vector2(xCenter + Functions.Cos(rotate) * length, yCenter + Functions.Sin(rotate) * length), width, color * alpha, 0.99f);
                        base.Depth = 0.99f;
                    }

                    public override void Update()
                    {
                        time++;
                        if ((float)time == duration)
                        {
                            Dispose();
                        }
                    }
                }

                public class Clock : Entity
                {
                    public float duration;

                    public float xCenter;

                    public float yCenter;

                    public float rotate;

                    public float length;

                    public float Anotherlength;

                    public Color color = Color.White;

                    public float alpha = 1f;

                    public int time;

                    public float speed = 1f;

                    public Clock(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, float Anotherlength, Color color)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.length = length;
                        this.color = color;
                        this.Anotherlength = Anotherlength;
                    }

                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new Vector2(xCenter, yCenter), new Vector2(xCenter + Functions.Cos(rotate) * length, yCenter + Functions.Sin(rotate) * length), 6f, color * alpha, 0.99f);
                        base.Depth = 0.99f;
                        DrawingLab.DrawLine(new Vector2(xCenter, yCenter), new Vector2(xCenter + Functions.Cos(rotate + 180f) * Anotherlength, yCenter + Functions.Sin(rotate + 180f) * Anotherlength), 6f, color * alpha, 0.99f);
                        base.Depth = 0.99f;
                    }

                    public override void Update()
                    {
                        time++;
                        if ((float)time == duration)
                        {
                            Dispose();
                        }
                    }
                }

                public class SolidPolygon : Entity
                {
                    public int accuracy;

                    public float duration;

                    public float xCenter;

                    public float yCenter;

                    public float radius;

                    public float rotate;

                    public float rotatec;

                    public Color color = Color.White;

                    public float alpha = 1f;

                    public int time;

                    public float speed = 1f;

                    public SolidPolygon(float xCenter, float yCenter, float radius, float duration, float alpha, float rotate, float rotatec, Color color, int accuracy)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.accuracy = accuracy;
                        this.radius = radius;
                        this.rotate = rotate;
                        this.rotatec = rotatec;
                    }

                    public override void Draw()
                    {
                        for (int i = 0; i < accuracy; i++)
                        {
                            int num = accuracy * 180 - 360;
                            DrawingLab.DrawLine(new Vector2(xCenter + Functions.Cos((float)(num / accuracy * i) + rotatec) * radius + Functions.Cos(rotate) * radius, yCenter + Functions.Sin((float)(num / accuracy * i) + rotatec) * radius + Functions.Sin(rotate) * radius), new Vector2(xCenter + Functions.Cos((float)(num / accuracy * (i + 1)) + rotatec) * radius + Functions.Cos(rotate) * radius, yCenter + Functions.Sin((float)(num / accuracy * (i + 1)) + rotatec) * radius + Functions.Sin(rotate) * radius), 4f, color * alpha, 0.99f);
                            base.Depth = 0.99f;
                        }
                    }

                    public override void Update()
                    {
                        time++;
                        if ((float)time == duration)
                        {
                            Dispose();
                        }
                    }
                }

                public class HollowPolygon : Entity
                {
                    public int accuracy;

                    public float duration;

                    public float xCenter;

                    public float yCenter;

                    public float radius;

                    public Color color = Color.White;

                    public float alpha = 1f;

                    public int time;

                    public float speed = 1f;

                    public HollowPolygon(float xCenter, float yCenter, float radius, float duration, float alpha, Color color, int accuracy)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.accuracy = accuracy;
                        this.radius = radius;
                    }

                    public override void Draw()
                    {
                        for (int i = 0; i < accuracy; i++)
                        {
                            int num = 360;
                            DrawingLab.DrawLine(new Vector2(xCenter + Functions.Cos(num / accuracy * i) * radius, yCenter + Functions.Sin(num / accuracy * i) * radius), new Vector2(xCenter + Functions.Cos(num / accuracy * (i + 1)) * radius, yCenter + Functions.Sin(num / accuracy * (i + 1)) * radius), 4f, color * alpha, 0.99f);
                            base.Depth = 0.99f;
                        }
                    }

                    public override void Update()
                    {
                        time++;
                        if ((float)time == duration)
                        {
                            Dispose();
                        }
                    }
                }
            }
            #endregion

            #region 雨演出2
            private class Rainer : Entity
            {
                private class WindParticle : Entity
                {
                    private readonly float randVal;

                    private readonly Rainer rainer;

                    private readonly float alpha;

                    private readonly float speed;

                    public WindParticle(Rainer rainer)
                    {
                        base.UpdateIn120 = true;
                        speed = Functions.Rand(0f, 1f) + rainer.Speed;
                        base.Rotation = Functions.Rand(-2f, 2f);
                        alpha = Functions.Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        base.Centre = new Vector2(Functions.Rand(-50, 690), -15f);
                        randVal = Functions.Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        _ = rainer.Intensity;
                        _ = randVal;
                    }

                    public override void Update()
                    {
                        Vector2 vector = MathUtil.GetVector2(speed / 1.5f, rainer.Rotation + base.Rotation + 90f);
                        base.Centre += vector;
                        if (base.Centre.Y > 499f)
                        {
                            Dispose();
                        }
                    }
                }

                private class RainDrop : Entity
                {
                    private readonly float length;

                    private readonly float randVal;

                    private readonly Rainer rainer;

                    private readonly float alpha;

                    private readonly float speed;

                    public RainDrop(Rainer rainer)
                    {
                        base.UpdateIn120 = true;
                        speed = Functions.Rand(0f, 1f) + rainer.Speed;
                        base.Rotation = Functions.Rand(-2f, 2f);
                        alpha = Functions.Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        base.Centre = new Vector2(Functions.Rand(-50, 690), -15f);
                        length = (float)Functions.Rand(6, 11) + rainer.Speed * 1f;
                        randVal = Functions.Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (!(rainer.Intensity <= randVal))
                        {
                            Vector2 vector = MathUtil.GetVector2(length / 2f, rainer.Rotation + base.Rotation + 90f);
                            DrawingLab.DrawLine(base.Centre + vector, base.Centre - vector, 2f, Color.LightBlue * alpha, 0f);
                        }
                    }

                    public override void Update()
                    {
                        Vector2 vector = MathUtil.GetVector2(speed / 1.5f, rainer.Rotation + base.Rotation + 90f);
                        base.Centre += vector;
                        if (base.Centre.Y > 499f)
                        {
                            Dispose();
                        }
                    }
                }

                public float Intensity { private get; set; }

                public float Speed { private get; set; } = 3f;


                public Rainer()
                {
                    base.Rotation = 10f;
                    base.UpdateIn120 = true;
                }

                public override void Draw()
                {

                }

                public override void Update()
                {
                    RainDrop obj = new RainDrop(this);
                    AddChild(obj);
                }
            }
            #endregion

            #region 円演出
            private class FireWorker : Entity
            {
                private class FireWorkBone : Bone
                {
                    private Vector2 speed;

                    public FireWorkBone(Vector2 centre, float speed, float rotation)
                    {
                        base.IsMasked = false;
                        base.Length = 30f;
                        base.Centre = centre;
                        this.speed = MathUtil.GetVector2(speed, rotation);
                        base.Rotation = rotation + 90f;
                    }

                    public override void Update()
                    {
                        base.Centre += speed / 2f;
                        if (alpha < 1f)
                        {
                            alpha += 0.1f;
                        }

                        base.Update();
                    }

                    public override void Draw()
                    {
                        base.Draw();
                    }
                }

                private int colorType;



                private int appearTime;

                private Color drawingColor;

                private readonly float[] rotations;

                private readonly float[] speeds;

                private readonly float[] sizes;

                private readonly int count;

                private readonly float speed;

                private readonly float duration;

                private float timeLeft;




                public int ColorType
                {
                    set
                    {
                        colorType = value;
                    }
                }

                public FireWorker(Vector2 centre, int count, float speed, float duration, Color color)
                {
                    base.Centre = centre;
                    this.count = count;
                    this.speed = speed;
                    this.duration = duration;
                    Depth = 1;
                    rotations = new float[count];
                    sizes = new float[count];
                    speeds = new float[count];
                    for (int i = 0; i < count; i++)
                    {
                        rotations[i] = Functions.Rand(0, 359);
                        speeds[i] = (float)Functions.Rand(20, 50) / 10f;
                        sizes[i] = (float)Functions.Rand(4, 9) / 10f;
                    }

                    drawingColor = color;
                }

                public override void Update()
                {
                    appearTime++;
                    timeLeft = duration - (float)appearTime;
                    if (timeLeft < 0f)
                    {
                        Dispose();
                    }
                }

                public override void Dispose()
                {

                    base.Dispose();
                }

                public override void Draw()
                {
                    for (int i = 0; i < count; i++)
                    {
                        FormalDraw(FightResources.Sprites.lightBallS, base.Centre + MathUtil.GetVector2(speeds[i] * timeLeft, rotations[i]), drawingColor * MathHelper.Min(0.7f, (float)appearTime / (duration / 1.3f)), sizes[i], 0, new Vector2(10f, 10f));
                    }
                }
            }
            #endregion

            #region 演出組み合わせ

            class MixProduction : RenderProduction
            {
                public MixProduction() : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.0f) { }

                public int Shaderss { get; private set; }

                public override RenderTarget2D Draw(RenderTarget2D obj)
                {

                    MissionTarget = HelperTarget;
                    this.Shader = FightResources.Shaders.Blur;
                    this.DrawTexture(obj, HelperTarget.Bounds);



                    return HelperTarget;
                }
            }
            #endregion

            #region 色反転シェーダー
            public static void Shaderblack(Vector2 center, float Width, float Height, float rot, Shader shader)
            {
                shader.Parameters["Width"].SetValue(Width / 640);
                shader.Parameters["Height"].SetValue(Height / 640);
                shader.Parameters["rot"].SetValue(rot * (float)(180 / 3.1415926f));
                shader.Parameters["centerX"].SetValue((center.X - 320) / 1280f);
                shader.Parameters["centerY"].SetValue((center.Y - 240) / 1280f);
                shader.Parameters["Type"].SetValue(0);
            }
            public static void Shaderblack(float rot, Shader shader)
            {
                rot %= 360.0f;
                shader.Parameters["rot"].SetValue(rot * (float)(Math.PI / 180));
                shader.Parameters["Type"].SetValue(0);
            }
            public static void Shaderblack(Vector2 center, float Width, float Height, Shader shader)
            {
                shader.Parameters["Width"].SetValue(Width / 960);
                shader.Parameters["Height"].SetValue(Height / 960);
                shader.Parameters["centerX"].SetValue((center.X - 320) / 1280f);
                shader.Parameters["centerY"].SetValue((center.Y - 240) / 1280f);
                shader.Parameters["Type"].SetValue(0);
            }
            public static void Shaderblack(Vector2 center, Shader shader)
            {

                shader.Parameters["centerX"].SetValue((center.X - 320) / 1280f);
                shader.Parameters["centerY"].SetValue((center.Y - 240) / 1280f);
                shader.Parameters["Type"].SetValue(0);
            }
            public static void Shaderblack(float Width, float Height, Shader shader)
            {

                shader.Parameters["Width"].SetValue(Width / 640 / 2);
                shader.Parameters["Height"].SetValue(Height / 640 / 2);
                shader.Parameters["Type"].SetValue(0);
            }
            public static void ShaderblackT2(float CentreX, Shader shader)
            {

                shader.Parameters["centerX"].SetValue((CentreX - 320) / 640f);
                shader.Parameters["centerX2"].SetValue(((CentreX * -1) + 320) / 640f);
                shader.Parameters["Type"].SetValue(2);
            }
            public static void ShaderblackBall(float size, Shader shader)
            {
                shader?.Parameters["size"].SetValue(size);
                shader?.Parameters["Type"].SetValue(3);
            }
            #endregion

            #region WhiteSoul 
            public class SoulOriginal
            {
                public static Player.MoveState WhiteSoul { get; private set; } = new Player.MoveState(Color.White, delegate (Player.Heart s)
                {
                    SoulMove(s);
                });





                private static float Project(Vector2 origin, Vector2 vector)
                {
                    return Vector2.Dot(origin, vector) / origin.Length();
                }

                private static void SoulMove(Player.Heart s)
                {
                    Vector2 centre = s.CollidingBox.GetCentre();
                    float num = s.Speed;
                    if (GameStates.IsKeyDown(InputIdentity.Cancel))
                    {
                        num *= 0.5f;
                    }

                    Vector2 zero = Vector2.Zero;
                    for (int i = 0; i < 4; i++)
                    {
                        if (GameStates.IsKeyDown(s.movingKey[i]))
                        {
                            zero += MathUtil.GetVector2(num * 0.5f, i * 90);
                        }
                    }

                    Vector2 vector = centre + zero;
                    BoxVertex[] vertexs = s.controlingBox.Vertexs;
                    _ = new Vector2[vertexs.Length];
                    for (int j = 0; j < vertexs.Length; j++)
                    {
                        Vector2 currentPosition = vertexs[j].CurrentPosition;
                        Vector2 currentPosition2 = vertexs[(j + 1) % vertexs.Length].CurrentPosition;
                        Vector2 vector2 = MathUtil.Rotate(currentPosition2 - currentPosition, 90f);
                        Vector2 vector3 = (currentPosition + currentPosition2) / 2f;
                        Vector2 vector4 = (currentPosition2 - currentPosition) / 2f;
                        Vector2 vector5 = centre - vector3;
                        Vector2 vector6 = vector - vector3;
                        float num2 = vector4.Length();
                        vector2.Normalize();
                        vector4.Normalize();
                        float x = Project(vector4, vector5);
                        float num3 = Project(vector4, vector6);
                        if (!(MathF.Abs(x) > num2 + 0.2f) || !(MathF.Abs(num3) > num2 + 0.2f))
                        {
                            float num4 = Project(vector2, vector5);
                            float num5 = Project(vector2, vector6);
                            if (num4 < 0f)
                            {
                                num4 = 0f - num4;
                                num5 = 0f - num5;
                                vector2 = -vector2;
                            }

                            if (num5 < 8f)
                            {
                                num5 = 8f;
                                vector = vector3 + vector4 * num3 + num5 * vector2;
                            }
                        }
                    }

                    s.Centre = vector;
                }
            }
            #endregion

            #region Undyne Effect
            private void CreateLine(float yCentre, float length)
            {
                CentreEasing.EaseBuilder builder = new();
                float time = BeatTime(2);
                builder.Insert(time, CentreEasing.Alternate(
                        3f,
                        CentreEasing.LerpTo(new(320, yCentre), 0.12f, CentreEasing.EaseOutQuad(new(320 - 31, yCentre), new(320, yCentre), time)),
                        CentreEasing.LerpTo(new(320, yCentre), 0.12f, CentreEasing.EaseOutQuad(new(320 + 31, yCentre), new(320, yCentre), time))
                    ));
                builder.Insert(time, CentreEasing.Accelerating(new(-5, -7), new(0, 0.7f)));
                Line follow, mid, line = new(builder.GetResult(), (s) => s.AppearTime > time ? (90 + (s.AppearTime - time) * 3.3f) : 90, (s) => length) { Depth = 0.5f };
                line.Width = 4;
                line.DrawingColor = Color.Red;
                follow = new((s) => line.Centre, (s) => 90);
                DelayBeat(2, () =>
                {
                    line.AlphaDecrease(time);
                    line.DrawingColor = Color.Red;

                    Line mid2 = new((s) => line.Centre, (s) => line.Rotation, (s) => 1000);
                    CreateEntity(mid2);
                    mid2.AlphaDecrease(time / 3);
                    mid2.DrawingColor = Color.Orange;

                    follow.Alpha += 0.2f;
                    follow.AlphaDecrease(time / 3, 0.67f);
                });
                ForBeat120(0, 2, () =>
                {

                    line.DrawingColor = Color.Lerp(line.DrawingColor, Color.White, 0.12f);
                });
                float col = 0;
                for (int i = 0; i < 20; i++)
                {
                    DelayBeat(i * 0.1f, () =>
                    {
                        SceneOut(Color.Black * col, 0.1f);
                        col += 0.1f;
                    });
                }


                mid = new(new Vector2(320, 0), new Vector2(320, 480));
                CreateEntity(line);
                CreateEntity(mid);
                CreateEntity(follow);
                mid.AlphaDecrease(time / 3);
                follow.AlphaDecrease(time / 3, 0.5f);
                follow.DrawingColor = Color.Orange;
                follow.Depth = 0.48f;
                mid.DrawingColor = Color.Orange;

                DelayBeat(16, () =>
                {
                    follow.Dispose();
                    mid.Dispose();
                    line.Dispose();
                });
            }
            #endregion

            #region GOODWORLD Line
            public class Linerotatee : Entity
            {
                public float duration = 0;
                public float xCenter = 0;
                public float yCenter = 0;
                public float rotate = 0;
                public float width = 4;
                public Color color = Color.White;
                public Linerotatee(float xCenter, float yCenter, float rotate, float duration, float alpha, Color color)
                {
                    this.xCenter = xCenter;
                    this.yCenter = yCenter;
                    this.rotate = rotate;
                    this.duration = duration;
                    this.alpha = alpha;
                    this.color = color;
                }
                public Linerotatee(float xCenter, float yCenter, float rotate, float duration, float alpha) : this(xCenter, yCenter, rotate, duration, alpha, Color.White) { }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public float depth = 0.99f;
                public override void Draw()
                {
                    if (rotate % 180 != 0)
                        DrawingLab.DrawLine(new(xCenter - 1f / Tan(rotate) * yCenter, -50), new(xCenter + 1f / Tan(rotate) * (480 - yCenter), 520), width, color * alpha, depth);
                    else
                        DrawingLab.DrawLine(new(0, yCenter), new(640, yCenter), width, color * alpha, depth);
                    Depth = 0.99f;
                }

                public override void Update()
                {
                    time++;
                    if (time >= duration)
                    {
                        Dispose();
                    }

                }
            }
            #endregion

            #region asgore hand
            public class Hand : UndyneFight_Ex.Remake.Entities.Barrage
            {
                public Hand(Func<ICustomMotion, Vector2> ce, Func<ICustomMotion, float> re)
                {
                    this.Image = HandImage;
                    this.PositionRoute = ce;
                    this.RotationRoute = re;
                    this.Depth = 0.5f;
                    this.UpdateIn120 = true;
                    this.Alpha = 1.0f;
                }
                public Hand(EaseUnit<Vector2> ce, EaseUnit<float> re) : this(ce.Easing, re.Easing) { }
                protected override float GetDistance(Player.Heart heart)
                {
                    return 10000;
                }
            }
            #endregion

            #region unknown
            private class BoxPiece : Entity
            {
                public BoxPiece(Vector2 centre)
                {
                    w = Rand(-10, 10) / 100f;
                    Centre = centre;
                    Rotation = 0;
                    Image = FightResources.Sprites.boxPiece;
                    speed = new Vector2(Rand(-20, 20) / 10f, Rand(-30, 20) / 10f);
                }

                private Vector2 speed;
                private readonly float w;

                public override void Draw()
                {
                    FormalDraw(Image, Centre, Color.White, 1, Rotation, ImageCentre);
                }

                public override void Update()
                {
                    Rotation += w;
                    speed.Y += 0.16f;
                    Centre += speed;
                }
            }
            #endregion

            #region GoodDrillBarrages
            public void A21()
            {
                SetSoul(2);
                CreateEntity(new Boneslab(0, 30, 30, BeatTime(8)));
                CreateEntity(new Boneslab(180, 30, 30, BeatTime(16)));
            }
            public void A22()
            {
                CreateEntity(new Platform(1, new(320, 280), (s) => { return new Vector2(Sin(s.AppearTime * 1.25f) * 140, 0); }, 0, 50, BeatTime(8)));
                CreateEntity(new Platform(1, new(320, 280), (s) => { return new Vector2(Sin(s.AppearTime * -1.25f) * 140, 0); }, 0, 50, BeatTime(8)));
                CreateEntity(new Platform(0, new(320, 200), (s) => { return new Vector2(Sin(s.AppearTime * 1.55f) * 120, 0); }, 0, 50, BeatTime(8)));
                CreateEntity(new Platform(0, new(320, 200), (s) => { return new Vector2(Sin(s.AppearTime * -1.55f) * 120, 0); }, 0, 50, BeatTime(8)));
                LeftBone rb1 = new(true, 5, 600) { ColorType = 2 };
                LeftBone rb2 = new(false, 5, 600) { ColorType = 2 };
                CreateBone(rb1);
                CreateBone(rb2);
                AddInstance(new TimeRangedEvent(30, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                AddInstance(new TimeRangedEvent(70, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                AddInstance(new TimeRangedEvent(110, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                AddInstance(new TimeRangedEvent(150, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                AddInstance(new TimeRangedEvent(190, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                AddInstance(new TimeRangedEvent(230, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                AddInstance(new TimeRangedEvent(270, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                AddInstance(new TimeRangedEvent(310, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                AddInstance(new TimeRangedEvent(350, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                AddInstance(new TimeRangedEvent(390, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                AddInstance(new TimeRangedEvent(430, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                AddInstance(new TimeRangedEvent(470, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                AddInstance(new TimeRangedEvent(510, 1500, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
            }
            public static void A23()
            {
                CreateBone(new CustomBone(new(580, 240), (s) => { return new Vector2(Sin(s.AppearTime * -2.15f) * 160, 0); }, 0, 200));
                CreateEntity(new NormalGB(new(Rand(40, 600), 120), new(320, -20), new(0.75f, 0.5f), 0, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 120), new(660, -20), new(0.75f, 0.5f), 60, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 120), new(660, 500), new(0.75f, 0.5f), 120, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 120), new(320, 500), new(0.75f, 0.5f), 180, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 120), new(-20, 500), new(0.75f, 0.5f), 240, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 120), new(-20, -20), new(0.75f, 0.5f), 300, 60, 20));
            }
            public static void A24()
            {
                CreateBone(new CustomBone(new(60, 240), (s) => { return new Vector2(Sin(s.AppearTime * 2.15f) * 160, 0); }, 0, 200));
                CreateEntity(new NormalGB(new(Rand(40, 600), 360), new(320, -20), new(0.75f, 0.5f), 0, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 360), new(660, -20), new(0.75f, 0.5f), 60, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 360), new(660, 500), new(0.75f, 0.5f), 120, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 360), new(320, 500), new(0.75f, 0.5f), 180, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 360), new(-20, 500), new(0.75f, 0.5f), 240, 60, 20));
                CreateEntity(new NormalGB(new(LastRand, 360), new(-20, -20), new(0.75f, 0.5f), 300, 60, 20));
            }
            public static void A25()
            {
                CreateBone(new CustomBone(new(40, 120), Motions.PositionRoute.linear, 40, 240) { PositionRouteParam = new float[] { 8, 0 }, ColorType = 2 });
                CreateBone(new CustomBone(new(40, 360), Motions.PositionRoute.linear, 130, 240) { PositionRouteParam = new float[] { 8, 0 }, ColorType = 2 });
                CreateBone(new CustomBone(new(600, 120), Motions.PositionRoute.linear, 130, 240) { PositionRouteParam = new float[] { -8, 0 }, ColorType = 2 });
                CreateBone(new CustomBone(new(600, 360), Motions.PositionRoute.linear, 40, 240) { PositionRouteParam = new float[] { -8, 0 }, ColorType = 2 });
            }
            public static void A26()
            {
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 80)
                {
                    PositionRouteParam = new float[] { 4, 0 }
                });
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 80)
                {
                    PositionRouteParam = new float[] { -4, 0 }
                });
            }
            public static void A27()
            {
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 60)
                {
                    PositionRouteParam = new float[] { 4, 0 }
                });
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 60)
                {
                    PositionRouteParam = new float[] { -4, 0 }
                });
            }
            public static void A28()
            {
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 45)
                {
                    PositionRouteParam = new float[] { 4, 0 }
                });
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 45)
                {
                    PositionRouteParam = new float[] { -4, 0 }
                });
            }
            public static void A29()
            {
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 34)
                {
                    PositionRouteParam = new float[] { 4, 0 }
                });
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 34)
                {
                    PositionRouteParam = new float[] { -4, 0 }
                });
            }
            public static void A30()
            {
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 26)
                {
                    PositionRouteParam = new float[] { 4, 0 }
                });
                CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 26)
                {
                    PositionRouteParam = new float[] { -4, 0 }
                });
            }
            #endregion

            #region DT2 Knife
            public static void Knife(Vector2 center, float rotate, bool trafic, float trafictime)
            {
                DrawingUtil.Linerotatelong head = new(center.X, center.Y, rotate + 180, trafictime, 1, 34, new(255, 61, 207));
                head.width = 7;
                DrawingUtil.Linerotatelong end = new(center.X + Cos(rotate) * 10, center.Y + Sin(rotate) * 10, rotate, trafictime, 1, 8, new(255, 61, 207));
                end.width = 7;
                CreateEntity(head);
                CreateEntity(end);
                AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                {
                    head.Dispose();
                    end.Dispose();
                }));
                if (!trafic)
                {
                    PlaySound(Sounds.Warning);
                }

                AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                {
                    DrawingUtil.Shock(1.2f, 1.3f, 3);
                    PlaySound(Sounds.largeKnife, 0.7f);
                    DrawingUtil.Linerotatelong Line = new(center.X + Cos(rotate) * 640 * 1.25f, center.Y + Sin(rotate) * 640 * 1.25f, rotate + 180, 100, 1, 640 * 2.5f, new(189, 44, 153));
                    Line.width = 0;
                    CreateEntity(Line);
                    AddInstance(new TimeRangedEvent(5, 30 * 9, () =>
                    {
                        Line.width = Line.width * 0.7f + 25 * 0.3f;
                        Line.alpha -= 0.05f;
                    }));
                    for (int a = 0; a < 4; a++)
                    {
                        CreateSpear(new Pike(center + new Vector2(Cos(rotate + 180) * (128 + a * 33), Sin(rotate + 180) * (128 + a * 33)), rotate, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        CreateSpear(new Pike(center + new Vector2(Cos(rotate) * (128 + a * 33), Sin(rotate) * (128 + a * 33)), rotate + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                    }
                }));

            }
            #endregion

            #region Endtime
            private static class MainEffect
            {
                public static void PlusGreenSoulRotate(float rot, float duration)
                {
                    float start = Functions.Heart.Rotation;
                    float end = rot;
                    float del = start - end;
                    float t = 0;
                    AddInstance(new TimeRangedEvent(0, duration, () =>
                    {
                        float x = t / (duration - 1);
                        float f = 2 * x - x * x;
                        Functions.Heart.InstantSetRotation(start - del * f);
                        t++;
                    }));
                }
                public static void BGshining()
                {
                    float alp = 0;
                    game.ForBeat(3, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.Black, new(95, 137, 154, 60), 0.2f) * alp;
                        alp += 0.0075f;
                    });
                    game.ForBeat(3, 3, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.Black, new(95, 137, 154, 60), 0.2f) * alp;
                        alp -= 0.0075f;
                    });
                }
                public static void SoftSetBox(Vector2 center, float width, float height, float duration, int SetMission, int type)
                {
                    float t = 0;
                    float startx = BoxStates.Centre.X;
                    float starty = BoxStates.Centre.Y;
                    float endx = center.X;
                    float endy = center.Y;
                    float delx = startx - endx;
                    float dely = starty - endy;
                    if (type == 1)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = 2 * x - x * x;
                            SetBoxMission(SetMission);
                            InstantSetBox(new Vector2(startx - delx * f, starty - dely * f), width, height);
                            t++;
                        }));
                    }
                    if (type == 2)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = x * x;
                            SetBoxMission(SetMission);
                            InstantSetBox(new Vector2(startx - delx * f, starty - dely * f), width, height);
                            t++;
                        }));
                    }
                }
                public static void SoftSetBox(Vector2 center, float width, float height, float duration, int type)
                {
                    float t = 0;
                    float startx = BoxStates.Centre.X;
                    float starty = BoxStates.Centre.Y;
                    float endx = center.X;
                    float endy = center.Y;
                    float delx = startx - endx;
                    float dely = starty - endy;
                    if (type == 1)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = 2 * x - x * x;
                            InstantSetBox(new Vector2(startx - delx * f, starty - dely * f), width, height);
                            t++;
                        }));
                    }
                    if (type == 2)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = x * x;
                            InstantSetBox(new Vector2(startx - delx * f, starty - dely * f), width, height);
                            t++;
                        }));
                    }
                }
                public static void SoftTP(Vector2 center, float duration, int SetMission, int type)
                {
                    float t = 0;
                    float startx = Functions.Heart.Centre.X;
                    float starty = Functions.Heart.Centre.Y;
                    float endx = center.X;
                    float endy = center.Y;
                    float delx = startx - endx;
                    float dely = starty - endy;
                    if (type == 1)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = 2 * x - x * x;
                            SetPlayerMission(SetMission);
                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                    if (type == 2)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = x * x;
                            SetPlayerMission(SetMission);
                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                }
                public static void SoftTP(Vector2 center, float duration, int type)
                {
                    float t = 0;
                    float startx = Functions.Heart.Centre.X;
                    float starty = Functions.Heart.Centre.Y;
                    float endx = center.X;
                    float endy = center.Y;
                    float delx = startx - endx;
                    float dely = starty - endy;
                    if (type == 1)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = 2 * x - x * x;

                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                    if (type == 2)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = x * x;

                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                }
                public static void Rain(float time, int DensityPerFrame)
                {
                    AddInstance(new TimeRangedEvent(time, () =>
                    {
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(game.BeatTime(24), CentreEasing.EaseInSine(new(Rand(0, 680), -50), new(LastRand - 50, 560), game.BeatTime(24)));
                        Line a = new(e.GetResult(), (s) => { return -87; }, (s) => { return Rand(8, 48); }) { Alpha = 0.4f, Width = 2.3f, DrawingColor = new(0, 0, 125) };
                        Line b = new(e.GetResult(), (s) => { return -87; }, (s) => { return Rand(8, 48); }) { Alpha = 0.1f, Width = 2.3f, DrawingColor = new(0, 0, 55) };
                        Line[] line = { a, b };
                        foreach (Line lines in line)
                        {
                            for (int i = 0; i < DensityPerFrame; i++)
                            {
                                CreateEntity(lines);
                                game.ForBeat(6, () =>
                                {
                                    if (lines.Centre.Y >= 480) lines.Dispose();
                                });//4k确实能打（（（（
                            }
                        }
                    }));
                }
            }
            private void Apply3D()
            {
                float camRotation = 48;
                DelayBeat(0, () =>
                {
                    ScreenDrawing.UpExtending = 1.75f;
                    ScreenDrawing.DownExtending = 0.25f;

                    CentreEasing.EaseBuilder screen = new();
                    screen.Insert(BeatTime(2), CentreEasing.EaseOutQuint(new(0, 0), new(0, -10), BeatTime(2)));
                    screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                    cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                    ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                    ValueEasing.EaseBuilder camera = new();
                    Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y * (1 + ScreenDrawing.UpExtending) / (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPoint = new(0, 0, 200);

                    ValueEasing.EaseBuilder posMove = new();

                    int beatCount = 2;

                    posMove.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(200, 60, BeatTime(beatCount)));
                    posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                    camera.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(0, camRotation, BeatTime(beatCount)));
                    camera.Run(rotation =>
                    {
                        Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                        Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                        Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
            private void To6KIn3D()
            {
                float camRotation = 48;
                DelayBeat(0, () =>
                {
                    ValueEasing.EaseBuilder camera = new();

                    int beatCount = 2;

                    camera.Insert(BeatTime(beatCount), ValueEasing.EaseInSine(0, 17, BeatTime(beatCount)));
                    camera.Run(position =>
                    {
                        Effect3D.CameraPosition = new(0, -240 + Sin(camRotation) * -2 * position, -position * 2);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
            #endregion

            #region Universal Collapse

            private class UNLine : Entity
            {
                public int duration = 0;
                public float x1 = 0;
                public float y1 = 0;
                public float x2 = 0;
                public float y2 = 0;
                public UNLine(float x1, float y1, float x2, float y2, int duration, float alpha)
                {
                    this.x1 = x1;
                    this.y1 = y1;
                    this.x2 = x2;
                    this.y2 = y2;
                    this.duration = duration;
                    this.alpha = alpha;
                }
                public float alpha = 0.5f;
                public int time = 0;
                public float speed = 1;
                public override void Draw()
                {
                    DrawingLab.DrawLine(new(x1, y1), new(x2, y2), 4, Color.Cyan * alpha, 0.5f);
                    Depth = 0.01f;
                }

                public override void Update()
                {
                    time++;
                    if (time == duration)
                    {
                        Dispose();
                    }

                }
            }

            #endregion

            #region  DreamBattle
            private void BallCreate(int cnt, float range)
            {
                Vector3[] xy = new Vector3[cnt];
                Vector3[] xz = new Vector3[cnt];
                Vector3[] yz = new Vector3[cnt];
                VertexHull[] hull = new VertexHull[3];
                for (int i = 0; i < cnt; i++)
                {
                    float rot = i * 360f / cnt;
                    xy[i] = new Vector3(Cos(rot), Sin(rot), 0);
                    yz[i] = new Vector3(0, Cos(rot), Sin(rot));
                    xz[i] = new Vector3(Cos(rot), 0, Sin(rot));
                }
                Vector3 rotatingSpeed = new(0.6f, 0, 0);
                hull[0] = new VertexHull(xy, rotatingSpeed);
                hull[1] = new VertexHull(yz, rotatingSpeed);
                hull[2] = new VertexHull(xz, rotatingSpeed);
                for (int i = 0; i <= 2; i++) AddInstance(hull[i]);
                AddInstance(new TimeRangedEvent(0, BeatTime(8), () =>
                {
                    float del = Sin01(Gametime / BeatTime(12)) * 0.2f;
                    for (int i = 0; i <= 2; i++) hull[i].Axises[2] = new Vector2(-Sin01(del), Cos01(del)) * 0.9f;
                }));
                Vector2 stalker = Functions.Heart.Centre;
                TimeRangedEvent e;
                AddInstance(e = new TimeRangedEvent(0, BeatTime(8), () =>
                {
                    stalker = stalker * 0.99f + Functions.Heart.Centre * 0.01f;
                }));
                for (int i = 0; i < 3; i++)
                {
                    hull[i].Axises[0] = new(1.25f, 0);
                    for (int j = 0; j < cnt; j++)
                    {
                        int x = i, y = j;
                        CreateBone(new CustomBone(new(0, 0), (s) => stalker + hull[x].Translated[y] * range, 3, 5) { IsMasked = false, AlphaIncrease = true });
                    }
                }
                AddInstance(new InstantEvent(BeatTime(8), () =>
                {
                    stalker = new(320, -500);
                }));
            }
            #endregion

            #region Line Bone

            public static void Rb(RBones b, Vector2 vec, Vector2 vec2, int col, float blen)
            {
                b.vec = vec;
                b.vec2 = vec2;
                b.ColorType = col;
                b.blen = blen;
            }
            public class RBones : Bone
            {
                public Vector2 vec { set; get; }
                public Vector2 vec2 { set; get; }
                public float blen { set; get; }
                public RBones() { }
                public override void Start()
                {
                    base.Start();
                    Alpha = 1;
                }
                public override void Draw()
                {
                    float len = (float)Math.Abs(Math.Sqrt(((vec2.Y - vec.Y) * (vec2.Y - vec.Y)) + ((vec2.X - vec.X) * (vec2.X - vec.X)))) - blen;
                    if (len < 0) { len = 0; }
                    Length = len;

                    Centre = new Vector2((vec2.X + vec.X) / 2f, (vec2.Y + vec.Y) / 2f);
                    Rotation = (float)(Math.Atan2(vec2.Y - vec.Y, vec2.X - vec.X) * 180 / Math.PI) + 90f;
                    base.Draw();
                }
                public override void Update() { }
            }
            #endregion

            #region fireball
            public class CustomBall : UndyneFight_Ex.Remake.Entities.Barrage
            {
                public V centre { get; set; }
                public float distance { get; set; }
                public float startrot { get; set; }
                public float rotspeed { get; set; }
                public float speed { get; set; }
                public float dir { get; set; }
                public float size { get; set; } = 1;
                public CustomBall(V vec, float dis, float speed, float startrot, float rotspeed, float dir, float size)
                {
                    centre = vec;
                    distance = dis;
                    this.speed = speed / 2;
                    this.startrot = startrot;
                    this.rotspeed = rotspeed / 2;
                    this.dir = dir;
                    this.UpdateIn120 = true;
                    this.size = size;
                }
                protected override float GetDistance(Player.Heart heart)
                {
                    return Vector2.Distance(heart.Centre, this.Centre);
                }
                public override void Start()
                {
                    Centre = centre;
                    Alpha = 1;
                    Depth = 1;
                    Image = UndyneFight_Ex.Remake.Resources.FightSprites.Fireball[0];
                    ColorType = 0;
                    Size = new V(size);
                    this.HitRadius = 4 * size;

                }

                float timer;
                public override void Update()
                {

                    Rotation = (float)((startrot + 90 + (rotspeed * timer)) / (180 / Math.PI));
                    //(float)(rotspeed * (180f / Math.PI))
                    Centre = centre + GetVector2(speed * timer, dir)
                           + GetVector2(distance, startrot + rotspeed * timer);
                    timer++;

                    base.Update();
                    if (timer > 2000) { Dispose(); }
                }

            }
            #endregion

            #region TaS shader
            GridShader shaderGrid;
            public class GridShader : Shader
            {
                public GridShader() : base(
                    UndyneFight_Ex.Fight.Functions.Loader.Load<Effect>("Musics\\Traveler at Sunset\\Grid")
                    )
                {
                    this.StableEvents = (t) =>
                    {
                        Time += 0.011f * TimeElapsed;
                        RegisterTexture(GlobalResources.Sprites.hashtex, 1);
                        this.Parameters["iTime"].SetValue(Time);
                        this.Parameters["iColor"].SetValue(BlendColor.ToVector3() * 0.8f);
                        this.Parameters["iSide"].SetValue(SideColor.ToVector3());
                        this.Parameters["iIntensity"].SetValue(Intensity1);
                        this.Parameters["iGlowDistance"].SetValue(GlowDistance);
                        this.Parameters["iGlowIntensity"].SetValue(GlowIntensity);
                        this.Parameters["iIntensity2"].SetValue(Intensity2);
                        this.Parameters["iIntensity3"].SetValue(Intensity3);
                    };
                }
                public Color BlendColor { get; set; }
                public Color SideColor { get; set; }
                public float Time { get; set; }
                public float Intensity1 { get; set; }
                public float Intensity2 { get; set; }
                public float Intensity3 { get; set; }
                public float GlowIntensity { get; set; }
                public float GlowDistance { get; set; }
            }
            private class Winder : Entity
            {
                public float Intensity { set; get; } = 10;
                private float Speed { set; get; } = 40f;
                public float Length { set; get; } = 300f;
                public float BasicSpeed { set; get; } = 1f;
                public Color DrawingColor { set; get; } = Color.White;
                public bool Direction { set; get; } = false;
                public float Width { get; set; } = 1.5f;

                public Winder()
                {
                    UpdateIn120 = true;
                }
                public float timer = 0;
                public override void Draw()
                {


                }

                public override void Update()
                {
                    timer++;
                    if (timer % Intensity < 1)
                    {
                        Speed = 40f * BasicSpeed;
                        Length = 150f * (BasicSpeed + 1);
                        CreateEntity(new Wind(Speed * Rand(0.8f, 1.4f), Length, DrawingColor, Width, Direction));
                    }
                }
                class Wind : Entity
                {
                    float Speed;
                    float Width = 1.5f;
                    Vector2 point1;
                    Vector2 point2;
                    Color color;
                    public Wind(float Speed, float length, Color color, float width, bool dir = false)
                    {
                        this.Width = width;
                        this.color = color;
                        this.Speed = Speed;
                        if (dir)
                        {
                            this.Speed = -Speed;
                            point1 = new(-20, Rand(10, 470));
                            point2 = new(-20 - length, LastRand);
                        }
                        else
                        {
                            point1 = new(660, Rand(10, 470));
                            point2 = new(660 + length, LastRand);
                        }
                    }
                    float timer = 0;
                    public float Colordepth = Rand(0.300f, 0.500f);
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(point1, point2, Width, color * Colordepth, 0.1f);
                    }

                    public override void Update()
                    {
                        timer++;
                        point1 += new Vector2(-Speed, 0);
                        point2 += new Vector2(-Speed, 0);
                        if (timer >= 900 / Speed + 30) this.Dispose();
                    }
                }
            }
            #endregion

            #region Break BoxFrame
            public class BoxFrame : Entity
            {

                public float rot { get; set; }
                public BoxFrame(V Centre, float rot)
                {
                    this.Centre = Centre;
                    this.rot = rot;
                }
                public override void Draw()
                {
                    DrawingLab.DrawLine(Centre + GetVector2(2, rot), Centre + GetVector2(-2, rot), 6, C.White * 0.3f, 1);
                }

                public override void Update()
                {

                }
            }
            #endregion

            #region BlockLine
            private class BlockLine : Entity
            {
                public BlockLine(V centre, float rot, float width, float height)
                {
                    Width = width;
                    Centre = centre;
                    this.rot = rot;
                    Height = height;
                }
                public float Width { get; set; }
                public float Height { get; set; }
                public float rot { get; set; }
                public C Color { get; set; } = C.White;
                public bool mirror { get; set; }
                public override void Draw()
                {
                    for (int i = 0; i < 4; i++)
                        DrawingLab.DrawLine(Centre + new V(Cos(rot + i * 90) * Width / 2, Sin(rot + 90 * i) * Height / 2), Centre + new V(Cos(rot + 90 + i * 90) * Width / 2, Sin(rot + 90 + i * 90) * Height / 2), 3, Color, Depth);
                    if (mirror)
                    {
                        for (int i = 0; i < 4; i++)
                            DrawingLab.DrawLine(
                                new V(640 - Centre.X, Centre.Y) + new V(Cos(-rot - i * 90) * Width / 2, Sin(-rot - 90 * i) * Height / 2),
                                new V(640 - Centre.X, Centre.Y) + new V(Cos(-rot - 90 - i * 90) * Width / 2, Sin(-rot - 90 - i * 90) * Height / 2), 3, Color, Depth);
                    }
                }
                public void DelayDispose(float time)
                {
                    AddInstance(new InstantEvent(time, () => Dispose()));
                }
                public override void Update()
                {
                }
            }
            #endregion

            #region ether strike
            public static void Rotate(float intensity, float time)
            {
                float start = ScreenDrawing.ScreenAngle;
                float end = start + intensity;
                float del = start - end;
                float t = 0;
                AddInstance(new TimeRangedEvent(0, time / 2f, () =>
                {
                    float x = t / (time / 2f - 1);
                    float f = 2 * x - x * x;
                    ScreenDrawing.ScreenAngle = start - del * f;
                    t++;
                }));
                float t2 = 0;
                float start2 = start + intensity;
                float end2 = start;
                float del2 = start2 - end2;
                AddInstance(new TimeRangedEvent(time / 2f + 1, time / 2f, () =>
                {
                    float x = t2 / (time / 2f - 1);
                    float f = x * x;
                    ScreenDrawing.ScreenAngle = start2 - del2 * f;
                    t2++;
                }));
            }
            #endregion
            private class RotationBone : Bone
            {
                float speed;
                public RotationBone(V vec, float SRot, float speed)
                {
                    Centre = vec;
                    Rotation = SRot;
                    this.speed = speed;
                }
                public override void Draw()
                {
                    base.Draw();
                }
                public override void Update()
                {
                    Rotation += speed;
                    Alpha = 1;
                    base.Update();
                }
            }
            private class CenB : Bone
            {
                float rot;
                float rotspeed;
                float dis;
                float speed;
                float len;

                public float Prot { get; set; }
                public float Srot { get; set; }
                public bool di = true;
                V pos;
                public CenB(V pos, float rot, float speed, float rotspeed, float dis, float len)
                {
                    this.rot = rot;
                    this.rotspeed = rotspeed;
                    this.len = len;
                    this.pos = pos;
                    this.dis = dis;
                    this.speed = speed;
                    if (speed < 0)
                    {
                        di = true;
                    }
                }
                public override void Draw()
                {
                    base.Draw();
                }
                int time = 0;
                public override void Update()
                {
                    if (di && time > 500)
                    {
                        base.Dispose();
                    }
                    time++;
                    Centre = pos + GetVector2(dis, rot);
                    base.Update();
                    dis -= speed;
                    rot += rotspeed;
                    Rotation = rot + 90 + Srot;
                    Srot += Prot;

                    if (dis < len)
                    {
                        Length = dis;
                        if (Length < 0)
                        {
                            base.Dispose();
                        }
                    }
                    else
                    {
                        Length = len;
                    }

                }
                public override void Start()
                {
                    autoDispose = false;
                    ColorType = 0;
                    Alpha = 1;
                    base.Start();
                }
            }
            public class EfEn : Entity
            {
                public float rot { get; set; }
                public Vector2 centre { get; set; }
                public float Alpha { get; set; } = 1;
                public float alphaspeed { get; set; } = 0;
                public Color Color { get; set; } = Color.White;
                public bool Bone { get; set; } = false;
                public float Len { get; set; } = 0;
                public V speed { get; set; } = V.Zero;
                public bool stop { get; set; } = false;
                int type;
                public EfEn(V centre, Texture2D image)
                {
                    this.centre = centre;
                    Size = V.One;
                    type = 0;
                    this.Image = image;
                }
                public EfEn(V centre, Texture2D image, V Size)
                {
                    this.centre = centre;
                    this.Size = Size;
                    this.Image = image;
                    type = 0;
                }
                public EfEn(Vector2 centre, Texture2D image, V Size, float rot)
                {
                    this.centre = centre;
                    this.Size = Size;
                    this.rot = rot;
                    this.Image = image;
                    type = 0;
                }
                public override void Draw()
                {
                    if (!Bone)
                        FormalDraw(Image, centre, Color * Alpha, Size, GetRadian(rot), ImageCentre);
                    else if (Bone)
                    {
                        float scale = (Size.X + Size.Y) / 2;
                        var spb = SpriteBatch;
                        CollideRect cl1 = new(0, 0, 6, Len - 2);
                        V detla = GetVector2((Len / 2) * scale, rot + 90);
                        spb.Draw(Sprites.boneBody, centre, cl1.ToRectangle(), Color * Alpha, GetRadian(rot), new Vector2(3, Len / 2), scale, SpriteEffects.None, Depth);
                        spb.Draw(Sprites.boneHead, centre - detla, null, Color * Alpha, GetRadian(rot), new Vector2(5, 3), scale, SpriteEffects.None, Depth);
                        spb.Draw(Sprites.boneTail, centre + detla, null, Color * Alpha, GetRadian(rot), new Vector2(5, 3), scale, SpriteEffects.None, Depth);
                    }

                }
                #region Easing
                public void vecEase(bool InOut, Vector2 Startcentre, Vector2 Endcentre, float Time, EaseState EasingType)
                {
                    if (InOut)
                        RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseIn(Time, Endcentre - Startcentre, EasingType)));
                    else
                        RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseOut(Time, Endcentre - Startcentre, EasingType)));
                }
                public void vecEase(bool InOut, Vector2 Startcentre, Vector2 Endcentre, float Delay, float Time, EaseState EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseIn(Time, Endcentre - Startcentre, EasingType)));
                        else
                            RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseOut(Time, Endcentre - Startcentre, EasingType)));
                    }));
                }
                public void vec2Ease(bool InOut, Vector2 Startcentre, Vector2 Endcentre, float Time, EaseState EasingType)
                {
                    if (InOut)
                        RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseIn(Time, Endcentre, EasingType)));
                    else
                        RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseOut(Time, Endcentre, EasingType)));
                }
                public void vec2Ease(bool InOut, Vector2 Startcentre, Vector2 Endcentre, float Delay, float Time, EaseState EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseIn(Time, Endcentre, EasingType)));
                        else
                            RunEase((s) => { centre = s; }, LinkEase(Stable(0, Startcentre), EaseOut(Time, Endcentre, EasingType)));
                    }));
                }
                public void rotEase(bool InOut, float Startrot, float Endrot, float Time, EaseState EasingType)
                {
                    if (InOut)
                        RunEase((s) => { rot = s; }, LinkEase(Stable(0, Startrot), EaseIn(Time, Endrot - Startrot, EasingType)));
                    else
                        RunEase((s) => { rot = s; }, LinkEase(Stable(0, Startrot), EaseOut(Time, Endrot - Startrot, EasingType)));
                }
                public void rotEase(bool InOut, float Startrot, float Endrot, float Delay, float Time, EaseState EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { rot = s; }, LinkEase(Stable(0, Startrot), EaseIn(Time, Endrot - Startrot, EasingType)));
                        else
                            RunEase((s) => { rot = s; }, LinkEase(Stable(0, Startrot), EaseOut(Time, Endrot - Startrot, EasingType)));
                    }));
                }
                public void SizeEase(bool InOut, V StartS, V EndS, float Time, EaseState EasingType)
                {
                    if (InOut)
                        RunEase((s) => { Size = s; }, LinkEase(Stable(0, StartS), EaseIn(Time, EndS - StartS, EasingType)));
                    else
                        RunEase((s) => { Size = s; }, LinkEase(Stable(0, StartS), EaseOut(Time, EndS - StartS, EasingType)));
                }
                public void SizeEase(bool InOut, V StartS, V EndS, float Delay, float Time, EaseState EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { Size = s; }, LinkEase(Stable(0, StartS), EaseIn(Time, EndS - StartS, EasingType)));
                        else
                            RunEase((s) => { Size = s; }, LinkEase(Stable(0, StartS), EaseOut(Time, EndS - StartS, EasingType)));
                    }));
                }
                public void AlphaEase(bool InOut, float Startal, float Endal, float Time, EaseState EasingType)
                {

                    if (InOut)
                        RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, Startal), EaseIn(Time, Endal - Startal, EasingType)));
                    else
                        RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, Startal), EaseOut(Time, Endal - Startal, EasingType)));

                }
                public void AlphaEase(bool InOut, float Startal, float Endal, float Delay, float Time, EaseState EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, Startal), EaseIn(Time, Endal - Startal, EasingType)));
                        else
                            RunEase((s) => { Alpha = s; }, LinkEase(Stable(0, Startal), EaseOut(Time, Endal - Startal, EasingType)));
                    }));
                }
                public void ColorEase(bool InOut, C StartC, C EndC, float Time, ES EasingType)
                {
                    if (InOut)
                        RunEase((s) => { Color = C.Lerp(StartC, EndC, s); }, LinkEase(Stable(0, 0), EaseIn(Time, 1, EasingType)));
                    else
                        RunEase((s) => { Color = C.Lerp(StartC, EndC, s); }, LinkEase(Stable(0, 0), EaseOut(Time, 1, EasingType)));
                }
                public void ColorEase(bool InOut, C StartC, C EndC, float Delay, float Time, ES EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { Color = C.Lerp(StartC, EndC, s); }, LinkEase(Stable(0, 0), EaseIn(Time, 1, EasingType)));
                        else
                            RunEase((s) => { Color = C.Lerp(StartC, EndC, s); }, LinkEase(Stable(0, 0), EaseOut(Time, 1, EasingType)));
                    }));
                }
                public void LenEase(bool InOut, float Startal, float Endal, float Time, EaseState EasingType)
                {

                    if (InOut)
                        RunEase((s) => { Len = s; }, LinkEase(Stable(0, Startal), EaseIn(Time, Endal - Startal, EasingType)));
                    else
                        RunEase((s) => { Len = s; }, LinkEase(Stable(0, Startal), EaseOut(Time, Endal - Startal, EasingType)));

                }
                public void LenEase(bool InOut, float StartLen, float EndLen, float Delay, float Time, EaseState EasingType)
                {
                    AddInstance(new InstantEvent(Delay, () =>
                    {
                        if (InOut)
                            RunEase((s) => { Len = s; }, LinkEase(Stable(0, StartLen), EaseIn(Time, EndLen - StartLen, EasingType)));
                        else
                            RunEase((s) => { Len = s; }, LinkEase(Stable(0, StartLen), EaseOut(Time, EndLen - StartLen, EasingType)));
                    }));
                }
                #endregion
                public void AutoDis(float Time)
                {

                    AddInstance(new InstantEvent(Time, () =>
                    {
                        if (!stop)
                        {
                            Dispose();
                        }
                    }));

                }
                public void SetTag(string tag)
                {
                    Tags = new string[] { tag };
                }
                public void SetTag(string[] tag)
                {
                    Tags = tag;
                }
                public override void Update()
                {
                    if (!stop)
                    {
                        centre += speed;
                        Alpha += alphaspeed;
                    }
                    Centre = centre;
                }

                public override void Start()
                {
                    base.Start();
                }
            }
            #region 3dEffect
            public class Bone3d : Entity
            {
                public Vector3 回転 { set; get; }
                public Vector2 centre { set; get; }
                public Vector3 length { set; get; }
                public float len { set; get; }
                public int DrawingColor { set; get; }
                private bool way { set; get; } = false;
                private RBones a1 = new();
                private RBones a2 = new();
                private RBones a3 = new();
                private RBones a4 = new();
                private RBones a5 = new();
                private RBones a6 = new();
                private RBones a7 = new();
                private RBones a8 = new();
                private RBones a9 = new();
                private RBones a10 = new();
                private RBones a11 = new();
                private RBones a12 = new();
                public Bone3d(Vector2 vec1, Vector3 len, bool type)
                {
                    centre = vec1;
                    length = len;
                    way = type;
                }
                public override void Start()
                {
                    if (way)
                    {
                        RBones[] bones = { a1, a2, a3, a4, a5, a6, a7, a8, a9 };
                        foreach (Bone Bones in bones)
                        {
                            CreateBone(Bones);
                        }
                    }
                    else
                    {
                        RBones[] bones = { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12 };
                        foreach (Bone Bones in bones)
                        {
                            CreateBone(Bones);
                        }
                    }
                }
                private Vector2 計算3d(float x, float y, float z)
                {
                    float x1 = Cos(回転.X) * y - Sin(回転.X) * z;
                    float x2 = Sin(回転.X) * y + Cos(回転.X) * z;
                    float y1 = Sin(回転.Y) * x2 + Cos(回転.Y) * x;
                    float z1 = Cos(回転.Z) * y1 - Sin(回転.Z) * x1;
                    float z2 = Sin(回転.Z) * y1 + Cos(回転.Z) * x1;
                    return new Vector2(z1, z2);
                }
                public override void Draw()
                {
                    if (way)
                    {
                        float rot = 60;
                        Rb(a1, centre + 計算3d(0, 0, length.Z * -2),
                              centre + 計算3d(Sin(rot) * length.X, Cos(rot) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a2, centre + 計算3d(0, 0, length.Z * -2),
                              centre + 計算3d(Sin(rot + 120) * length.X, Cos(rot + 120) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a3, centre + 計算3d(0, 0, length.Z * -2),
                              centre + 計算3d(Sin(rot - 120) * length.X, Cos(rot - 120) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a4, centre + 計算3d(Sin(rot) * length.X, Cos(rot) * length.Y, length.Z * 0),
                              centre + 計算3d(Sin(rot - 120) * length.X, Cos(rot - 120) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a5, centre + 計算3d(Sin(rot) * length.X, Cos(rot) * length.Y, length.Z * 0),
                             centre + 計算3d(Sin(rot + 120) * length.X, Cos(rot + 120) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a6, centre + 計算3d(Sin(rot - 120) * length.X, Cos(rot - 120) * length.Y, length.Z * 0),
                             centre + 計算3d(Sin(rot + 120) * length.X, Cos(rot + 120) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a7, centre + 計算3d(0, 0, length.Z * 2),
                              centre + 計算3d(Sin(rot) * length.X, Cos(rot) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a8, centre + 計算3d(0, 0, length.Z * 2),
                              centre + 計算3d(Sin(rot + 120) * length.X, Cos(rot + 120) * length.Y, length.Z * 0), DrawingColor, len);
                        Rb(a9, centre + 計算3d(0, 0, length.Z * 2),
                              centre + 計算3d(Sin(rot - 120) * length.X, Cos(rot - 120) * length.Y, length.Z * 0), DrawingColor, len);

                    }
                    else
                    {
                        Rb(a1, centre + 計算3d(length.X * 1, length.Y * 1, length.Z * 1),
                                 centre + 計算3d(length.X * -1, length.Y * 1, length.Z * 1), DrawingColor, len);

                        Rb(a2, centre + 計算3d(length.X * -1, length.Y * -1, length.Z * 1),
                                 centre + 計算3d(length.X * 1, length.Y * -1, length.Z * 1), DrawingColor, len);

                        Rb(a3, centre + 計算3d(length.X * 1, length.Y * -1, length.Z * 1),
                                 centre + 計算3d(length.X * 1, length.Y * -1, length.Z * -1), DrawingColor, len);

                        Rb(a4, centre + 計算3d(length.X * 1, length.Y * 1, length.Z * -1),
                                 centre + 計算3d(length.X * 1, length.Y * 1, length.Z * 1), DrawingColor, len);

                        Rb(a5, centre + 計算3d(length.X * 1, length.Y * 1, length.Z * 1),
                                 centre + 計算3d(length.X * 1, length.Y * -1, length.Z * 1), DrawingColor, len);

                        Rb(a6, centre + 計算3d(length.X * -1, length.Y * -1, length.Z * -1),
                                 centre + 計算3d(length.X * 1, length.Y * -1, length.Z * -1), DrawingColor, len);

                        Rb(a7, centre + 計算3d(length.X * 1, length.Y * -1, length.Z * -1),
                                 centre + 計算3d(length.X * 1, length.Y * 1, length.Z * -1), DrawingColor, len);

                        Rb(a8, centre + 計算3d(length.X * 1, length.Y * 1, length.Z * -1),
                                 centre + 計算3d(length.X * -1, length.Y * 1, length.Z * -1), DrawingColor, len);

                        Rb(a9, centre + 計算3d(length.X * -1, length.Y * 1, length.Z * 1),
                                 centre + 計算3d(length.X * -1, length.Y * -1, length.Z * 1), DrawingColor, len);

                        Rb(a10, centre + 計算3d(length.X * -1, length.Y * -1, length.Z * 1),
                                 centre + 計算3d(length.X * -1, length.Y * -1, length.Z * -1), DrawingColor, len);

                        Rb(a11, centre + 計算3d(length.X * -1, length.Y * -1, length.Z * -1),
                                 centre + 計算3d(length.X * -1, length.Y * 1, length.Z * -1), DrawingColor, len);

                        Rb(a12, centre + 計算3d(length.X * -1, length.Y * 1, length.Z * -1),
                                 centre + 計算3d(length.X * -1, length.Y * 1, length.Z * 1), DrawingColor, len);
                    }
                }
                public override void Update() { }
                public override void Dispose()
                {
                    if (way)
                    {
                        RBones[] bones = { a1, a2, a3, a4, a5, a6, a7, a8, a9 };
                        foreach (Bone Bones in bones)
                        {
                            Bones.Dispose();
                        }
                    }
                    else
                    {
                        RBones[] bones = { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12 };
                        foreach (Bone Bones in bones)
                        {
                            Bones.Dispose();
                        }
                    }
                }
                public static void Rb(RBones b, Vector2 vec, Vector2 vec2, int col, float blen)
                {
                    b.vec = vec;
                    b.vec2 = vec2;
                    b.ColorType = col;
                    b.blen = blen;

                }
                public class RBones : Bone
                {
                    public Vector2 vec { set; get; }
                    public Vector2 vec2 { set; get; }
                    public float blen { set; get; }
                    public RBones() { }
                    public override void Draw()
                    {
                        float len = (float)Math.Abs(Math.Sqrt(((vec2.Y - vec.Y) * (vec2.Y - vec.Y)) + ((vec2.X - vec.X) * (vec2.X - vec.X)))) - blen;
                        if (len < 0) { len = 0; }
                        Length = len;
                        Centre = new Vector2((vec2.X + vec.X) / 2f, (vec2.Y + vec.Y) / 2f);
                        Rotation = (float)(Math.Atan2(vec2.Y - vec.Y, vec2.X - vec.X) * 180 / Math.PI) + 90f;
                        Alpha = 1;
                        base.Draw();
                    }
                    public override void Update() { }
                }
            }

            public class Line3d : Entity
            {
                public Vector2 centre { set; get; }
                public Vector3 length { set; get; }
                public Color DrawingColor { set; get; } = Color.White;
                public float Width { set; get; } = 1;
                public Line3d(Vector2 vec1, Vector3 len)
                {
                    centre = vec1;
                    length = len;
                }
                public Vector3 回転 { set; get; }

                public Vector2 計算3d(float x, float y, float z)
                {
                    float x1 = Cos(回転.X) * y - Sin(回転.X) * z;
                    float x2 = Sin(回転.X) * y + Cos(回転.X) * z;
                    float y1 = Sin(回転.Y) * x2 + Cos(回転.Y) * x;
                    float z1 = Cos(回転.Z) * y1 - Sin(回転.Z) * x1;
                    float z2 = Sin(回転.Z) * y1 + Cos(回転.Z) * x1;
                    return new Vector2(z1, z2);
                }
                public override void Draw()
                {
                    DrawingLab.DrawLine(centre + 計算3d(length.X * 1, length.Y * 1, length.Z * 1),
                             centre + 計算3d(length.X * -1, length.Y * 1, length.Z * 1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * -1, length.Y * -1, length.Z * 1),
                             centre + 計算3d(length.X * 1, length.Y * -1, length.Z * 1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * 1, length.Y * -1, length.Z * 1),
                             centre + 計算3d(length.X * 1, length.Y * -1, length.Z * -1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * 1, length.Y * 1, length.Z * -1),
                             centre + 計算3d(length.X * 1, length.Y * 1, length.Z * 1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * 1, length.Y * 1, length.Z * 1),
                             centre + 計算3d(length.X * 1, length.Y * -1, length.Z * 1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * -1, length.Y * -1, length.Z * -1),
                             centre + 計算3d(length.X * 1, length.Y * -1, length.Z * -1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * 1, length.Y * -1, length.Z * -1),
                             centre + 計算3d(length.X * 1, length.Y * 1, length.Z * -1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * 1, length.Y * 1, length.Z * -1),
                             centre + 計算3d(length.X * -1, length.Y * 1, length.Z * -1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * -1, length.Y * 1, length.Z * 1),
                             centre + 計算3d(length.X * -1, length.Y * -1, length.Z * 1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * -1, length.Y * -1, length.Z * 1),
                             centre + 計算3d(length.X * -1, length.Y * -1, length.Z * -1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * -1, length.Y * -1, length.Z * -1),
                             centre + 計算3d(length.X * -1, length.Y * 1, length.Z * -1), Width, DrawingColor, Depth);

                    DrawingLab.DrawLine(centre + 計算3d(length.X * -1, length.Y * 1, length.Z * -1),
                             centre + 計算3d(length.X * -1, length.Y * 1, length.Z * 1), Width, DrawingColor, Depth);
                }
                public override void Update()
                {

                }
            }
            #endregion
            public float FDBeat(float x)
            { return x * FD; }
            public void BadBreak()
            {
                for (int b = 0; b < 100; b++)
                {
                    Texture2D image = Sprites.GBShooting[0];

                    int rand = Rand(0, 5);
                    if (rand == 0)
                    { }
                    if (rand == 1)
                    {
                        image = Sprites.spear;
                    }
                    if (rand == 2)
                    {
                        image = Sprites.spider;
                    }
                    if (rand == 3)
                    {
                        image = Sprites.fireball[0];
                    }
                    if (rand == 5)
                    {
                        image = Sprites.lightBall;

                    }
                    if (rand == 4)
                    { image = Sprites.shield; }
                    AutoEntity l = new ImageEntity(image);
                    l.Depth = 1;
                    l.Alpha = 1;
                    if (rand == 5)
                    {
                        //l.BlendColor = Color.Gold;
                        l.Size = new(0.5f);
                    }
                    if (rand == 0)
                    {
                        l.Size = new(1, 0.5f);
                    }
                    CreateEntity(l);
                    l.Centre = new Vector2(320, 240);
                    float rot = Rand(0, 360);
                    float e = 0;
                    float e2 = Rand(50, 600);
                    l.Rotation = Rand(0, 360);
                    AddInstance(new TimeRangedEvent(BeatTime(16), () =>
                    {
                        l.Centre = new V(320, 240) + GetVector2(e, rot);
                        e += (e2 - e) * 0.01f;
                        l.Rotation += 3;
                        l.Alpha -= 0.01f;
                    }));
                }
            }
            private class RGBS : RenderProduction
            {
                public C Blend1 { get; set; } = new(0, 255, 0);
                public C Blend2 { get; set; } = new(255, 0, 0);
                public C Blend3 { get; set; } = new(0, 0, 255);
                public float rot { get; set; }
                public float intensity { get; set; }
                public RGBS(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Additive, depth)
                { }
                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    MissionTarget = HelperTarget;
                    DrawTexture(obj, V.Zero, Blend1);
                    DrawTexture(obj, GetVector2(intensity, rot), Blend2);
                    DrawTexture(obj, GetVector2(intensity, rot + 180), Blend3);
                    return HelperTarget;
                }
            }
            #region easing
            private ES Li = ES.Linear;
            private ES Qd = ES.Quad;
            private ES Cu = ES.Cubic;
            private ES Qa = ES.Quart;
            private ES Qi = ES.Quint;
            private ES Ex = ES.Expo;
            private ES El = ES.Elastic;
            private ES Ci = ES.Circ;
            private ES Ba = ES.Back;
            #endregion
            private V WCentre = new V(320, 240);
            class Entitycount : Entity
            {
                public override void Draw()
                {
                    Depth = 1;
                    Font.NormalFont.Draw("Entity:" + $"{GetAll<Entity>().Length}", new V(100, 100), C.White);
                }

                public override void Update()
                {

                }
            }
            class Auroras : RenderProduction
            {
                public Auroras(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Additive, depth)
                {
                }

                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    MissionTarget = HelperTarget;
                    this.Shader = FightResources.Shaders.Aurora;
                    DrawTexture(obj, HelperTarget.Bounds);
                    Shader = null;
                    DrawTexture(obj, HelperTarget.Bounds);
                    return HelperTarget;
                }
            }
            private class RotateLine : Entity
            {
                public RotateLine(V vec, float st, int count, float speed, float dis)
                {
                    float s = 360f / count;
                    for (int i2 = 0; i2 < 10; i2++)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            CreateEntity(new rotLine(vec, st + i * s - i2 * 0.5f, speed, dis, count) { Alpha = 1 - i2 * 0.09f });
                        }
                    }
                }
                public override void Draw()
                { }
                public override void Update()
                { }
                public override void Dispose()
                {
                    base.Dispose();
                }
                public class rotLine : Entity
                {
                    public float rot { get; set; }
                    public float speed { get; set; }
                    public float dis { get; set; }
                    public V vec { get; set; }
                    private float count;
                    public float Alpha { get; set; } = 1;
                    public rotLine(V vec, float st, float speed, float dis, float count)
                    {
                        this.vec = vec;
                        this.speed = speed;
                        this.rot = st;
                        this.dis = dis;
                        this.count = count;
                    }
                    public override void Draw()
                    {
                        float len = 0;
                        if (count < 6)
                            len = dis * Sin(360 / count) * 2;
                        else
                            len = dis * Tan(360 / count);
                        V v = vec + GetVector2(dis, rot) + GetVector2(len, rot + 90);
                        V v2 = vec + GetVector2(dis, rot) + GetVector2(len, rot - 90);
                        DrawingLab.DrawLine(v, v2, 3, C.White * Alpha, 0.2f);
                    }

                    public override void Update()
                    {
                        rot += speed;
                    }
                }
            }
            #region Rrhar'il clock
            public class Clock : Entity
            {
                public float duration = 0;
                public float xCenter = 0;
                public float yCenter = 0;
                public float rotate = 0;
                public float length = 0;
                public float Anotherlength = 0;
                public Color color = Color.White;
                public Clock(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, float Anotherlength, Color color)
                {
                    this.xCenter = xCenter;
                    this.yCenter = yCenter;
                    this.rotate = rotate;
                    this.duration = duration;
                    this.alpha = alpha;
                    this.length = length;
                    this.color = color;
                    this.Anotherlength = Anotherlength;
                }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public override void Draw()
                {
                    DrawingLab.DrawLine(
                        new Vector2(xCenter, yCenter),
                        new Vector2(
                            xCenter + Cos(rotate) * length,
                            yCenter + Sin(rotate) * length),
                        6,
                        color * alpha,
                        Depth);

                    DrawingLab.DrawLine(
                        new Vector2(xCenter, yCenter),
                        new Vector2(
                            xCenter + Cos(rotate + 180) * Anotherlength,
                            yCenter + Sin(rotate + 180) * Anotherlength),
                        6,
                        color * alpha,
                        Depth);
                }

                public override void Update()
                {
                    time++;
                    if (time == duration)
                    {
                        Dispose();
                    }

                }
            }
            #endregion
            #region Luminescent Effect
            private static BlurPlus1 btest;
            private static BlurPlus2 btest2;
            public class BlurPlus1 : Shader
            {
                public float sigma { get; set; }
                public float intensity { get; set; } = 1;
                public float factor { get; set; } = 1;
                public BlurPlus1() : base(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\BlurX"))
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iSigma2"].SetValue(sigma);
                        x.Parameters["light"].SetValue(intensity);
                        x.Parameters["factor"].SetValue(factor);
                    };
                }

            }
            public class BlurPlus2 : Shader
            {
                public float sigma { get; set; }
                public float intensity { get; set; } = 1;
                public float factor { get; set; } = 1;
                public BlurPlus2() : base(Loader.Load<Effect>("Musics\\BadAppleRE\\shader\\BlurY"))
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iSigma2"].SetValue(sigma);
                        x.Parameters["light"].SetValue(intensity);
                        x.Parameters["factor"].SetValue(factor);
                    };
                }

            }
            private class SC : RenderProduction
            {
                public static Shader wat1 { get; set; }
                public static Shader wat2 { get; set; }
                public SC() : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.723f)
                {
                    //this.depth = depth;
                }
                public SC(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Additive, depth)
                {
                    //this.depth = depth;
                }
                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    MissionTarget = HelperTarget;
                    Shader = btest;
                    DrawTexture(obj, Vector2.Zero, Color.White);
                    Shader = btest2;
                    DrawTexture(Surface.HelperTarget, Vector2.Zero, Color.White);
                    Shader = null;
                    DrawTexture(obj, Vector2.Zero, Color.White);
                    return MissionTarget;
                }
            }
            #endregion
            #region Noise Effect
            public class Noise : Shader
            {
                public bool circmode { get; set; } = false;
                public float intensity { get; set; } = 0.01f;
                float time = 0;
                public Noise() : base(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\Noise"))
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["time"].SetValue(time);
                        x.Parameters["intensity"].SetValue(intensity);
                        x.Parameters["circmode"].SetValue(circmode ? 1 : 0);
                        time++;
                    };

                }

            }
            #endregion
            Noise n = new();
            #region EndText(Special Thanks)
            bool texter = true;
            int textnum = 0;
            bool ContainKey = false;
            bool completed = false;
            bool delay = true;
            float brot;
            V boxvec = new V(320, 240);
            V BoxSize;
            void Ender(string tex)
            {
                delay = false;
                ContainKey = false;
                Ef text = new(tex, WCentre, C.White * 2, 0.7f) { AutoDispose = false, scf = true };
                Ef text2 = new(tex, WCentre, C.White * 2, 0.7f) { AutoDispose = false, scf2 = true };
                CreateEntity(text);
                CreateEntity(text2);
                text.alphaOut(0, 1, T(16), ES.Cubic);
                text2.alphaOut(0, 1, T(16), ES.Cubic);
                DelayBeat(18, () =>
                {

                    string endtext = "Press Z for Credits.";
                    text = new(endtext, new V(320, 465), C.White * 2, 0.5f) { AutoDispose = false, scf = true };
                    text2 = new(endtext, new V(320, 465), C.White * 2, 0.5f) { AutoDispose = false, scf2 = true };
                    CreateEntity(text);
                    CreateEntity(text2);
                    text.alphaOut(0, 1, T(2), ES.Cubic);
                    text2.alphaOut(0, 1, T(2), ES.Cubic);
                });
                DelayBeat(20, () =>
                {
                    delay = true;
                });
            }
            void Ender(string tex, float size)
            {
                delay = false;
                ContainKey = false;
                Ef text = new(tex, WCentre, C.White * 2, size) { AutoDispose = false, scf = true };
                Ef text2 = new(tex, WCentre, C.White * 2, size) { AutoDispose = false, scf2 = true };
                CreateEntity(text);
                CreateEntity(text2);
                text.alphaOut(0, 1, T(16), ES.Cubic);
                text2.alphaOut(0, 1, T(16), ES.Cubic);
                DelayBeat(18, () =>
                {

                    string endtext = "Press Z for Next.";
                    text = new(endtext, new V(320, 465), C.White * 2, 0.5f) { AutoDispose = false, scf = true };
                    text2 = new(endtext, new V(320, 465), C.White * 2, 0.5f) { AutoDispose = false, scf2 = true };
                    CreateEntity(text);
                    CreateEntity(text2);
                    text.alphaOut(0, 1, T(2), ES.Cubic);
                    text2.alphaOut(0, 1, T(2), ES.Cubic);
                });
                DelayBeat(20, () =>
                {
                    delay = true;
                });
            }
            bool NextText(int num)
            {
                return ContainKey && textnum == num;
            }
            #endregion
            bool complete;
            #region FinalPart item
            RenderProduction Gray = null;
            private void Flash()
            {
                var t = new Shader(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\flash"));
                if (Gray == null)
                    Gray = ActivateShader(t, 0.995297838937592873985f);
                RunEase(s => t.Parameters["intensity"].SetValue(s), EaseOut(T(0.5f), 0, 1, ES.Linear));
            }
            public void CustomBox()
            {
                var box = BoxUtils.VertexBoxInstance;
                V vec = boxvec;
                V v = BoxSize;
                box.InstantSetPosition(0, vec + GetVector2((float)(Math.Sqrt((v.X / 2) * (v.X / 2) + (v.Y / 2) * (v.Y / 2))),
                                                brot + (float)(Math.Atan2(v.Y / 2, v.X / 2) * 180 / Math.PI)));
                box.InstantSetPosition(1, vec + GetVector2((float)(Math.Sqrt((v.X / 2) * (v.X / 2) + (v.Y / 2) * (v.Y / 2))),
                                                brot + (float)(Math.Atan2(v.Y / 2, v.X / -2) * 180 / Math.PI)));
                box.InstantSetPosition(2, vec + GetVector2((float)(Math.Sqrt((v.X / 2) * (v.X / 2) + (v.Y / 2) * (v.Y / 2))),
                                                brot + (float)(Math.Atan2(v.Y / -2, v.X / -2) * 180 / Math.PI)));
                box.InstantSetPosition(3, vec + GetVector2((float)(Math.Sqrt((v.X / 2) * (v.X / 2) + (v.Y / 2) * (v.Y / 2))),
                                                brot + (float)(Math.Atan2(v.Y / -2, v.X / 2) * 180 / Math.PI)));
            }

            public void BreakBone(float number, Vector2 vec, float speed, float len, float startrot)
            {
                float roted = 360f / number;
                for (int i = 0; i < number; i++)
                {
                    CreateBone(new CustomBone(vec,
                    CentreEasing.Linear(MathUtil.GetVector2(speed, startrot + (i * roted))), i * roted + 90 + startrot, len)
                    { ColorType = 0 });
                }
            }
            public void BreakBone(float time, float number, Vector2 vec, float speed, float len, float startrot)
            {
                AddInstance(new InstantEvent(time, () =>
                {
                    float roted = 360f / number;
                    for (int i = 0; i < number; i++)
                    {
                        CreateBone(new CustomBone(vec,
                        CentreEasing.Linear(MathUtil.GetVector2(speed, startrot + (i * roted))), i * roted + 90 + startrot, len)
                        { ColorType = 0 });
                    }
                }));
            }
            public void BreakFire(float num, Vector2 vec, float speed, float startrot, float rotspeed, float dis, float dir, float size)
            {
                float roted = 360f / num;
                for (int i = 0; i < num; i++)
                {
                    CreateEntity(new CustomBall(vec, dis, speed, startrot + (roted * i), rotspeed, dir, size));
                }
            }
            #endregion

            #region test item
            public int Sig(float t)
            { return t == 0 ? -1 : 1; }
            #endregion
            #region
            class GlitchPro : RenderProduction
            {
                private static RenderTarget2D tl;
                public static V BoxSize;
                public static int intensity;
                public static Color Color;
                public static V BoxLength;
                private static float Depth;
                public GlitchPro(float depth) : base(null, SpriteSortMode.Immediate, BlendState.AlphaBlend, depth)
                {
                    for (int i = 0; i < 5; i++)
                        GameStates.InstanceCreate(new box());
                    BoxSize = new V(2000, 50);
                    intensity = 40;
                    Color = Color.White;
                    BoxLength = new V(5);
                    Depth = depth;
                }

                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    MissionTarget = HelperTarget;
                    DrawTexture(obj, V.Zero, Color.White);
                    tl = obj;
                    return MissionTarget;
                }
                public override void Dispose()
                {
                    foreach (var t in GetAll<box>())
                        t.Dispose();
                    base.Dispose();
                }
                class box : Entity
                {
                    float timer = 0;
                    float set = 0;
                    CollideRect vec;
                    V rand;
                    public override void Draw()
                    {
                        if (timer > 50)
                        {
                            V[] t = new[] { new V(0, 0), new V(640, 0), new V(0, 480) };
                            V t1, t2, t3, t4;
                            t1 = vec.TopLeft;
                            t2 = vec.TopRight;
                            t3 = vec.BottomRight;
                            t4 = vec.BottomLeft;
                            SpriteBatch.DrawVertex(tl, 1, new[]
                            {
                               new VertexPositionColorTexture(new(t1+rand,0),Color,DrawingLab.UVPosition(t,t1)),
                               new VertexPositionColorTexture(new(t2+rand,0),Color,DrawingLab.UVPosition(t,t2)),
                               new VertexPositionColorTexture(new(t3+rand,0),Color,DrawingLab.UVPosition(t,t3)),
                               new VertexPositionColorTexture(new(t4+rand,0),Color,DrawingLab.UVPosition(t,t4)),
                        });
                        }
                    }

                    public override void Update()
                    {
                        timer++;
                        if (timer > set)
                        {
                            vec = new CollideRect(0, Rand(0, 480), BoxSize.X, BoxSize.Y);
                            rand = new V(Rand(-BoxLength.X, BoxLength.X), Rand(-BoxLength.Y, BoxLength.Y));
                            set = Rand(5f, 25f);
                        }
                    }
                }
            }
            #endregion
            #region useful item
            public float T(float time)
            {
                return BeatTime(time);
            }

            public float R(float left, float right)
            {
                return Rand(left, right);
            }
            public V GV(float lt, float rt)
            {
                return MathUtil.GetVector2(lt, rt);
            }
            public void SideLerpEase(Color cl, float Time)
            {
                Color color = BSet.SideColor;
                RunEase((s) => { BSet.SideColor = Color.Lerp(color, cl, s); }, LinkEase(Stable(0, 0), EaseOut(Time, 1, Cu)));
            }
            Shader nega;
            #endregion
            private class Side : RenderProduction
            {
                public Side(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Additive, depth)
                {
                    spr = new(WindowDevice);
                }
                SpriteBatchEX spr;
                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    var vec = new CollideRect(-50, 0, 1920, 1920 * 3 / 4);
                    MissionTarget = obj;
                    V[] t = new[] { new V(-107, -250), new V(1920, -250), new V(-107, 1920 * 3 / 4 - 250) };
                    V t1, t2, t3, t4;
                    t1 = vec.TopLeft;
                    t2 = vec.TopRight;
                    t3 = vec.BottomRight;
                    t4 = vec.BottomLeft;
                    Shader blur = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\SideBlur"));
                    blur.Parameters["Iintensity"].SetValue(0.02f);
                    Shader = blur;
                    for (int i = 0; i < 50; i++)
                    {
                        C color = C.White * 1;
                        SpriteBatch.DrawVertex(obj, 1, new[]
                        {
                               new VertexPositionColorTexture(new(t1+new V(i,0),0),color,DrawingLab.UVPosition(t,t1)),
                               new VertexPositionColorTexture(new(t2+new V(i,0),0),color,DrawingLab.UVPosition(t,t2)),
                               new VertexPositionColorTexture(new(t3+new V(i,0),0),color,DrawingLab.UVPosition(t,t3)),
                               new VertexPositionColorTexture(new(t4+new V(i,0),0),color,DrawingLab.UVPosition(t,t4)),
                    });
                    }
                    return MissionTarget;
                }
            }
            inyouex inyouT = new();
            private class inyouex : Entity
            {
                public inyouex()
                {
                    Alpha = 1;
                    UpdateIn120 = true;
                }
                public float Alpha { get; set; } = 0;
                public int count { get; set; } = 12;
                public bool stop { get; set; } = false;
                public override void Draw()
                {
                    var rot = 360f / count;
                    var t = new VertexPositionColor[count];
                    for (int i = 0; i < count; i++)
                        t[i] = new(new(Centre+GetVector2(200,i*rot+Rotation),0),C.Black*Alpha);
                    SpriteBatch.DrawVertex(Depth-0.1f, t);
                    t = new VertexPositionColor[180];
                    for (int i = 0; i < 180; i++)
                        t[i] = new(new(Centre + GetVector2(110, i + (stop ? -135 : -45) + Rotation), 0),(stop?C.White:C.Black)*Alpha);
                    SpriteBatch.DrawVertex(Depth, t);
                    for (int i = 0; i < 180; i++)
                        t[i] = new(new(Centre + GetVector2(110, i +(stop?-135:-45)+180+Rotation), 0), (stop ? C.Black : C.White) * Alpha);
                    SpriteBatch.DrawVertex(Depth, t);
                    Image = inyou;
                    FormalDraw(Image, Centre, null, C.White * Alpha, new V(0.5f), GetRadian(Rotation), ImageCentre, stop ? SpriteEffects.None:SpriteEffects.FlipHorizontally);
                    for (int i = 0; i < count; i++) 
                    {
                        DrawingLab.DrawLine(Centre+GetVector2(110, i * rot + Rotation),Centre+GetVector2(200,i*rot+Rotation),3,C.White*Alpha,Depth);
                        DrawingLab.DrawLine(Centre + GetVector2(200, i * rot+Rotation), Centre + GetVector2(200, (i+1) * rot+Rotation), 3, C.White*Alpha, Depth);
                        SpriteBatch.DrawVertex(Depth - 0.2f, new[] 
                        {
                            new VertexPositionColor(new(Centre,0),C.White*Alpha),
                            new VertexPositionColor(new(Centre+GetVector2(200,i*rot+Rotation),0),C.White*Alpha),
                            new VertexPositionColor(new(Centre + GetVector2(200, (i+1) * rot+Rotation),0),C.White*Alpha),
                        });
                    }
                }

                public override void Update()
                {
                    if(!stop)
                    Centre = new V(320, 240) + new V(Sin(4.3217f * Gametime / 10f) * 20, Sin(5.5526f * Gametime / 10f) * 20);
                }
                public void AlphaM(EaseUnit<float> alp) 
                {
                    VEaseBuilder t = new();
                    t.Insert(alp);
                    t.Run(s=>Alpha=s);
                }
                public VEaseBuilder RotM(EaseUnit<float> Rot)
                {
                    VEaseBuilder t = new();
                    t.Insert(Rot);
                    t.Run(s => Rotation = s);
                    return t;
                }
            }
            public class VertexDictionary : GameObject
            {
                public V3[] Shield1 { get; set; }
                public V3[] Shield2 { get; set; }
                public V[] Arrow { get; set; }
                public VertexDictionary()
                {
                    StartP();
                    size = 1;
                    Arrow = new V[]
                    {
                            new V(Px(-4), Py(-3)),
                            new V(Px(0), Py(-2)),
                            new V(Px(-4), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(-2), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(-1), Py(0f)),
                            new V(Px(0), Py(1)),
                            new V(Px(-1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(-1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(-1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(1), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(2), Py(0)),
                            new V(Px(0), Py(1)),
                            new V(Px(4), Py(0)),
                            new V(Px(0), Py(-2)),
                            new V(Px(13), Py(0)),
                            new V(Px(0), Py(-7)),
                            new V(Px(-13), Py(0)),
                            new V(Px(0), Py(3))
                    };
                    size = 1;
                    StartP();
                    Shield1 = new V3[]
                    {
                        new V3(Px(18.5f),Py(9.5f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(4f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(4f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(4f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(4f),0),
                        new V3(Px(1f),Py(0f),0),
                        new V3(Px(0f),Py(3f),0),
                        new V3(Px(4f),Py(0f),0),
                        new V3(Px(0),Py(-60),0),
                        new V3(Px(-3f),Py(0f),0)
                    };
                    StartP();
                    size = 1f;
                    Shield2 = new[] {
                    new V3(Px(18.5f), Py(8.5f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(-1f), 0),
                        new V3(Px(2f), Py(0f), 0),
                        new V3(Px(0f), Py(1f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(4f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(3f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(3f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(4f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(3f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(3f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(4f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(3f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(4f), 0),
                        new V3(Px(0f), Py(0f), 0),
                        new V3(Px(1f), Py(0f), 0),
                        new V3(Px(0f), Py(-50f), 0),
                    };
                    
                }

                public override void Update()
                {
                }
            }

        }
    }
}


