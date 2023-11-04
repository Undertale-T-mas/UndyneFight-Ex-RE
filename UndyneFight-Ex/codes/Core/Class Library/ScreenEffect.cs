using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.GlobalResources.Effects;

namespace UndyneFight_Ex.Fight
{
    public static partial class Functions
    {
        public static class ScreenDrawing
        {
            public static class UISettings
            {
                public static Vector2 NameShowerPos
                {
                    set => (GameStates.CurrentScene as FightScene).NameShow.Centre = value;
                    get => (GameStates.CurrentScene as FightScene).NameShow.Centre;
                }
                public static Vector2 HPShowerPos
                {
                    set
                    {
                        var v = (GameStates.CurrentScene as FightScene).HPBar;
                        CollideRect r = v.CurrentArea;
                        r.TopLeft = value;
                        v.ResetArea(r);
                    }
                    get => (GameStates.CurrentScene as FightScene).HPBar.CurrentArea.TopLeft;
                }
                public class UISurfaceDrawing : RenderProduction
                {
                    public Surface UISurface => uiSurf;
                    Surface uiSurf;
                    public UISurfaceDrawing(Surface uiSurf) : base(null, SpriteSortMode.Immediate, BlendState.AlphaBlend, 0.50001f)
                    {
                        this.uiSurf = uiSurf;
                    }
                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        MissionTarget = obj;
                        DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                        return MissionTarget;
                    }
                }
                private static void MoveSurface(Surface surface)
                {
                    SongFightingScene scene = GameStates.CurrentScene as SongFightingScene;
                    if (scene == null) return;
                    scene.NameShow.controlLayer = surface;
                    scene.HPBar.controlLayer = surface;
                    scene.Accuracy.controlLayer = surface;
                    scene.Time.controlLayer = surface;
                    scene.ScoreState.controlLayer = surface;
                }
                public static UISurfaceDrawing CreateUISurface()
                {
                    Surface surf;
                    Scene scene = GameStates.CurrentScene;
                    if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("UI"))
                        scene.CurrentDrawingSettings.surfaces.Add("UI", surf = new("UI")
                        {
                            BlendState = BlendState.AlphaBlend,
                            SpriteSortMode = SpriteSortMode.FrontToBack,
                            Transfer = Surface.TransferUse.ForceDefault,
                            DisableExpand = true
                        });
                    else surf = scene.CurrentDrawingSettings.surfaces["UI"];
                    MoveSurface(surf);
                    UISurfaceDrawing production;
                    SceneRendering.InsertProduction(production = new UISurfaceDrawing(surf));
                    bufferProduction.Add(production);
                    return production;
                }
                private static List<UISurfaceDrawing> bufferProduction = new();
                public static void RemoveUISurface()
                {
                    bufferProduction.ForEach(s => s.Dispose());
                    var surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
                    MoveSurface(surfaces["normal"]);
                    if (surfaces.ContainsKey("UI"))
                    {
                        surfaces["UI"].Dispose();
                        surfaces.Remove("UI");
                    }
                }
            }
            public static class HPBar
            {
                public static Color HPExistColor { get => (GameStates.CurrentScene as FightScene).HPBar.HPExistColor; set => (GameStates.CurrentScene as FightScene).HPBar.HPExistColor = value; }
                public static Color HPLoseColor { get=> (GameStates.CurrentScene as FightScene).HPBar.HPLoseColor; set => (GameStates.CurrentScene as FightScene).HPBar.HPLoseColor = value; }
                public static CollideRect AreaOccupied
                {
                    set => (GameStates.CurrentScene as FightScene).HPBar.ResetArea(value);
                    get => (GameStates.CurrentScene as FightScene).HPBar.CurrentArea;
                }
                public static bool Vertical { set => (GameStates.CurrentScene as FightScene).HPBar.Vertical = value; }
            }
            public static class CameraEffect
            {
                public static void Rotate180(float time)
                {
                    Rotate(180, time);
                }
                public static void Rotate(float rotation, float time)
                {
                    if (time < 0)
                        throw new ArgumentOutOfRangeException(nameof(time), string.Format("参数 {0} 必须为正数 或 0", nameof(time)));
                    float last = rotation;
                    float tick = 0;

                    float progress = 0;

                    AddInstance(new TimeRangedEvent(0, time + 1, () =>
                    {
                        tick += 0.5f;
                        if (tick < time)
                        {
                            float scale = tick / time;
                            float newRot = MathUtil.Sigmoid01(MathF.Pow(scale, 0.7f));
                            float del = newRot - progress;
                            progress = newRot;
                            last -= del * rotation;
                            ScreenAngle += del * rotation;
                        }
                        else
                        {
                            ScreenAngle += last;
                            last = 0;
                        }
                    })
                    { UpdateIn120 = true });
                }
                public static void RotateTo(float rotation, float time)
                {
                    float del = MathUtil.MinRotate(ScreenAngle, rotation);
                    Rotate(del, time);
                }
                /// <summary>
                /// 屏幕抽动
                /// </summary>  
                /// <param name="direction">抽动方向，false为先左后右，否则为先右后左</param>
                public static void Convulse(bool direction)
                {
                    Convulse(16, 8, direction);
                }
                /// <summary>
                /// 屏幕抽动
                /// </summary> 
                /// <param name="time">抽动持续时间</param>
                /// <param name="direction">抽动方向，false为先左后右，否则为先右后左</param>
                public static void Convulse(float time, bool direction)
                {
                    Convulse(25, time, direction);
                }
                /// <summary>
                /// 屏幕抽动
                /// </summary>
                /// <param name="intensity">抽动强度</param>
                /// <param name="time">抽动持续时间</param>
                /// <param name="direction">抽动方向，false为先右后左，否则为先左后右</param>
                public static void Convulse(float intensity, float time, bool direction)
                {
                    if (intensity <= 0)
                        throw new ArgumentOutOfRangeException(nameof(intensity), string.Format("参数 {0} 必须为正数", intensity));
                    if (time < 0)
                        throw new ArgumentOutOfRangeException(nameof(time), string.Format("参数 {0} 必须为正数 或 0", time));

                    intensity = MathF.Sqrt(intensity);
                    if (direction == false) intensity = -intensity;
                    float last = 0;
                    float tick = 0;

                    float progress = 0;

                    AddInstance(new TimeRangedEvent(0, time + 1, () =>
                    {
                        tick += 0.5f;
                        if (tick < time)
                        {
                            float scale = tick / time;
                            float newRot = AdvanceFunctions.Sin01(MathF.Pow(scale, 0.75f));
                            float del = newRot - progress;
                            progress = newRot;
                            last -= del * intensity;
                            ScreenAngle += del * intensity;
                        }
                        else
                        {
                            ScreenAngle += last;
                            last = 0;
                        }
                    })
                    { UpdateIn120 = true });
                }
                /// <summary>
                /// 将屏幕先扩大一下再缩小到原来大小。
                /// </summary>
                /// <param name="intensity">强度</param>
                /// <param name="time">持续时间</param>
                public static void SizeExpand(float intensity, float time)
                {
                    if (intensity <= 0)
                        throw new ArgumentOutOfRangeException(nameof(intensity), string.Format("参数 {0} 必须为正数", nameof(intensity)));
                    if (time < 0)
                        throw new ArgumentOutOfRangeException(nameof(time), string.Format("参数 {0} 必须为正数 或 0", nameof(time)));

                    intensity = 1 - MathF.Pow(0.98f, intensity);
                    float last = 0, tick = 0, progress = 0;

                    AddInstance(new TimeRangedEvent(0, time + 1, () =>
                    {
                        tick++;
                        if (tick < time)
                        {
                            float scale = tick / time;
                            float newRot = AdvanceFunctions.Sin01(MathF.Pow(scale, 0.75f));
                            float del = newRot - progress;
                            progress = newRot;
                            last -= del * intensity;
                            ScreenScale += del * intensity;
                        }
                        else
                        {
                            ScreenScale += last;
                            last = 0;
                        }
                    }));
                }
                /// <summary>
                /// 将屏幕先收缩一下再放大到原来大小。
                /// </summary>
                /// <param name="intensity">强度</param>
                /// <param name="time">持续时间</param>
                public static void SizeShrink(float intensity, float time)
                {
                    if (intensity <= 0)
                        throw new ArgumentOutOfRangeException(nameof(intensity), string.Format("参数 {0} 必须为正数", nameof(intensity)));
                    if (time < 0)
                        throw new ArgumentOutOfRangeException(nameof(time), string.Format("参数 {0} 必须为正数 或 0", nameof(time)));

                    intensity = MathF.Pow(0.98f, intensity) - 1;
                    float last = 0, tick = 0, progress = 0;

                    AddInstance(new TimeRangedEvent(0, time + 1, () =>
                    {
                        tick++;
                        if (tick < time)
                        {
                            float scale = tick / time;
                            float newRot = AdvanceFunctions.Sin01(MathF.Pow(scale, 0.75f));
                            float del = newRot - progress;
                            progress = newRot;
                            last -= del * intensity;
                            ScreenScale += del * intensity;
                        }
                        else
                        {
                            ScreenScale += last;
                            last = 0;
                        }
                    }));
                }
            }

            internal static void Reset()
            {
                BackGroundColor = Color.Black;
                ThemeColor = Color.White;
                UIColor = Color.White;
                ScreenAngle = 0;
                ScreenScale = 1;
                ScreenPositionDelta = Vector2.Zero;
                BoundDistance = Vector4.One;
                BoundColor = Color.Black;
                whiteOutRest = 0;
            }
            internal static float whiteOutRest = 0;
            internal static Vector4 BoundDistance = Vector4.One;
            internal static Color flinkerColor = Color.White;

            /// <summary>
            /// 以特定颜色淡出
            /// </summary>
            /// <param name="col">颜色</param>
            /// <param name="time">时间</param>
            public static void SceneOut(Color col, float time)
            {
                flinkerColor = col;
                whiteOutRest = time;
            }
            /// <summary>
            /// 以白色淡出
            /// </summary>
            /// <param name="time">淡出时长</param>
            public static void WhiteOut(float time)
            {
                flinkerColor = Color.White;
                whiteOutRest = time;
            }
            public static Color UIColor
            {
                set => GameMain.CurrentDrawingSettings.UIColor = value;
                get => GameMain.CurrentDrawingSettings.UIColor;
            }
            public static Color BoundColor { get; set; }
            public static Color ThemeColor
            {
                set => GameMain.CurrentDrawingSettings.themeColor = value;
                get => GameMain.CurrentDrawingSettings.themeColor;
            }
            public static Color BoxBackColor
            {
                set => Surface.Hidden.BackGroundColor = value;
                get => Surface.Hidden.BackGroundColor;
            }

            public static void MakeFlicker(Color color)
            {
                if (whiteOutRest > 0) return;
                flinkerColor = color;
                GameStates.MakeFlicker();
            }
            public static float DownBoundDistance { get => BoundDistance.X; set => BoundDistance.X = value; }
            public static float LeftBoundDistance { get => BoundDistance.Y; set => BoundDistance.Y = value; }
            public static float UpBoundDistance { get => BoundDistance.Z; set => BoundDistance.Z = value; }
            public static float RightBoundDistance { get => BoundDistance.W; set => BoundDistance.W = value; }

            /// <summary>
            /// 游戏的背景颜色
            /// </summary>
            public static Color BackGroundColor
            {
                set => GameMain.CurrentDrawingSettings.backGroundColor = value;
                get => GameMain.CurrentDrawingSettings.backGroundColor;
            }

            /// <summary>
            /// 屏幕旋转的角度
            /// </summary>
            public static float ScreenAngle
            {
                get => GameMain.CurrentDrawingSettings.screenAngle * 180f / MathUtil.PI;
                set => GameMain.CurrentDrawingSettings.screenAngle = value / 180f * MathUtil.PI;
            }

            /// <summary>
            /// 屏幕的偏移
            /// </summary>
            public static Vector2 ScreenPositionDelta
            {
                get => GameMain.CurrentDrawingSettings.screenDetla;
                set => GameMain.CurrentDrawingSettings.screenDetla = value;
            }

            /// <summary>
            /// 屏幕的缩放比例
            /// </summary>
            public static float ScreenScale
            {
                get => GameMain.CurrentDrawingSettings.screenScale;
                set => GameMain.CurrentDrawingSettings.screenScale = value;
            }

            public static float SceneOutScale
            {
                get => GameMain.CurrentDrawingSettings.sceneOutScale;
                set => GameMain.CurrentDrawingSettings.sceneOutScale = value;
            }

            public static float OutFadeScale
            {
                get => GameMain.CurrentDrawingSettings.outFadeScale;
                set => GameMain.CurrentDrawingSettings.outFadeScale = value;
            }
            public static float MasterAlpha
            {
                get => GameMain.CurrentDrawingSettings.masterAlpha;
                set => GameMain.CurrentDrawingSettings.masterAlpha = value;
            }

            public static RenderingManager SceneRendering => GameStates.missionScene.SceneRendering;
            public static RenderingManager BackGroundRendering => GameStates.missionScene.BackgroundRendering;

            public static Vector4 ScreenExtending
            {
                get => GameMain.CurrentDrawingSettings.Extending;
                set => GameMain.CurrentDrawingSettings.Extending = value;
            }
            public static float UpExtending
            {
                get => ScreenExtending.W;
                set => ScreenExtending = new(ScreenExtending.X, ScreenExtending.Y, ScreenExtending.Z, value);
            }
            public static float DownExtending
            {
                get => ScreenExtending.Y;
                set => ScreenExtending = new(ScreenExtending.X, value, ScreenExtending.Z, ScreenExtending.W);
            }
            public static SpriteBatchEX SpriteBatch => GameMain.MissionSpriteBatch;

            public static Shaders.Filter ActivateShader(Shader shader, float depth = 0.5f)
            {
                Shaders.Filter textureFilter = new(shader, depth);
                SceneRendering.InsertProduction(textureFilter);
                return textureFilter;
            }
            public static Shaders.Filter ActivateShaderBack(Shader shader, float depth = 0.5f)
            {
                Shaders.Filter textureFilter = new(shader, depth);
                BackGroundRendering.InsertProduction(textureFilter);
                return textureFilter;
            }

            public static class Shaders
            {
                public class Converging : RenderProduction
                {
                    public Converging(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Additive, depth)
                    {
                    }

                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        this.MissionTarget = HelperTarget;
                        //          this.DrawTextures(obj, )
                        return obj;
                    }
                }
                public class Lighting : RenderProduction
                {
                    private static bool Initialized = false;
                    public static RenderTarget2D[] lightSources { get; private set; } = new RenderTarget2D[4];
                    const int lightSize = 100;

                    public enum LightMode
                    {
                        Limit = 1,
                        Additive = 2,
                        ShaderMul = 3,
                    }
                    public LightMode LightingMode { private get; set; } = LightMode.Limit;
                    public Color AmbientColor { 
                        private get; 
                        set; 
                    }
                        = Color.Transparent;

                    public class Light
                    {
                        public Vector2 position;
                        public Vector2 scale = Vector2.One;
                        public Color color;
                        public float size;
                    }
                    public List<Light> Lights { get; private set; } = new();
                    public Lighting(float depth) : base(null, SpriteSortMode.Immediate, BlendState.Opaque, depth)
                    {
                    }

                    private void Initialize()
                    {
                        for (int i = 0; i < lightSources.Length; i++)
                        {
                            lightSources[i] = new RenderTarget2D(WindowDevice, lightSize * 2, lightSize * 2);
                        }
                        MissionTarget = lightSources[0];
                        ResetTargetColor(Color.Transparent);
                        Shader = Light0;
                        DrawTexture(FightResources.Sprites.pixUnit, lightSources[0].Bounds);
                        Initialized = true;
                    }

                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        if (!Initialized) Initialize();
                        if (Lights == null || Lights.Count == 0) return obj;
                        MissionTarget = HelperTarget;
                        Shader = null;
                        BlendState = BlendState.Additive;
                        ResetTargetColor(AmbientColor);

                        foreach (var v in Lights)
                        {
                            Vector2 trueSize = new Vector2(v.size, v.size) * v.scale;
                            DrawTexture(lightSources[0], (v.position - trueSize) * AdaptingScale, v.color, trueSize * AdaptingScale / lightSize);
                        }

                        return LightingMode switch {
                            LightMode.Limit => LightLimitRender(obj),
                            LightMode.Additive => LightAddRender(obj),
                            LightMode.ShaderMul => LightMulRender(obj),
                            _ => throw new ArgumentOutOfRangeException($"{LightingMode} is not a valid render mode")
                        };
                    }

                    private RenderTarget2D LightAddRender(RenderTarget2D obj)
                    {
                        BlendState = BlendState.Additive;

                        MissionTarget = HelperTarget2;
                        ResetTargetColor(Color.Transparent);
                        DrawTextures(new[] { HelperTarget, obj }, MissionTarget.Bounds);

                        return MissionTarget;
                    }
                    private RenderTarget2D LightMulRender(RenderTarget2D obj)
                    { 
                        MissionTarget = HelperTarget2;

                        this.Shader = FightResources.Shaders.ColorBlend;
                        FightResources.Shaders.ColorBlend.RegisterTexture(HelperTarget, 1);
                        DrawTexture(obj, MissionTarget.Bounds);
                        
                        return MissionTarget;
                    }
                    private RenderTarget2D LightLimitRender(RenderTarget2D obj)
                    {
                        BlendState = new BlendState()
                        {
                            Name = "LightingBlend",

                            AlphaBlendFunction = BlendFunction.Min,
                            AlphaSourceBlend = Blend.DestinationAlpha,
                            AlphaDestinationBlend = Blend.One,

                            ColorBlendFunction = BlendFunction.Min,
                            ColorSourceBlend = Blend.DestinationColor,
                            ColorDestinationBlend = Blend.One,

                        };

                        MissionTarget = HelperTarget2;
                        ResetTargetColor(Color.White);
                        DrawTextures(new[] { HelperTarget, obj }, MissionTarget.Bounds);

                        return MissionTarget;
                    }
                }
                public class RGBSplitting : RenderProduction
                {
                    public override void WindowSizeChanged(Vector2 vec)
                    {
                        screen = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
                    }
                    public RGBSplitting(float dep) : base(null, SpriteSortMode.FrontToBack, BlendState.Additive, dep)
                    {
                    }
                    public RGBSplitting() : base(null, SpriteSortMode.FrontToBack, BlendState.Additive, 0.50f)
                    {
                    }

                    private static RenderTarget2D screen;

                    public float Intensity { get; 
                        set; } = 1f;
                    public float RandomDisturb { get; set; } = 0.2f;
                    public bool Disturbance { get; set; } = true;
                    public Shader DisturbShader { get; set; } = CustomShaders.Sinwave;

                    private float time1 = 0, time2 = 0, time3 = 0;

                    public Color SplitColor1 { get; set; } = Color.Red;
                    public Color SplitColor2 { get; set; } = Color.Blue;
                    public Color MainColor { get; set; } = Color.Lime;

                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        RandomDisturb = Rand(-0.2f, 0.2f);
                        if (Shader == null && MathF.Abs(RandomDisturb + Intensity) * AdaptingScale < 0.8f) return obj;
                        MissionTarget = screen;

                        if (Disturbance)
                        {
                            Shader = DisturbShader;
                            Shader.Parameters["time1"].SetValue(time1 += Rand(0.08f, 0.15f));
                            Shader.Parameters["time2"].SetValue(time2 += Rand(0.18f, 0.35f));
                            Shader.Parameters["time3"].SetValue(time3 += Rand(0.38f, 0.55f));
                            Shader.Parameters["sin1"].SetValue(0.0005f);
                            Shader.Parameters["sin2"].SetValue(0.0007f);
                            Shader.Parameters["sin3"].SetValue(0.0010f);
                        }
                        else Shader = null;
                        DrawTexture(obj, new Vector2((Rand(0.5f - RandomDisturb, 0.5f + RandomDisturb) + Intensity) * AdaptingScale, 0), SplitColor1);
                        DrawTexture(obj, new Vector2((Rand(-0.5f - RandomDisturb, -0.5f + RandomDisturb) - Intensity) * AdaptingScale, 0), SplitColor2);

                        Shader = null;

                        DrawTexture(obj, new Vector2(0, Rand(-0.3f, 0.3f) * AdaptingScale), MainColor);

                        return MissionTarget;
                    }
                }
                public class Glitching : RenderProduction
                {
                    public int Intensity { get; set; } = 1;
                    public int AverageInterval { get; set; } = 4;
                    public float AverageDelta { get; set; } = 1f;
                    public float RGBSplitIntensity = 0.0f;
                    public float BlockScale = 1.0f;
                    private class Updater : GameObject
                    {
                        Glitching father;
                        public Updater(Glitching father)
                        {
                            this.father = father;
                        }
                        private class MoveBlock : GameObject
                        {
                            public Vector2 Delta { get; private set; }
                            public Rectangle Area { get; private set; }
                            public Vector2 RGBDelta { get; private set; }
                            public MoveBlock(Glitching father)
                            {
                                Area = new Rectangle(Rand(0, (int)AdaptedSize.X), Rand(0, (int)AdaptedSize.Y),
                                        (int)Rand(10 * AdaptingScale * father.BlockScale, 30 * AdaptingScale * father.BlockScale),
                                        (int)Rand(10 * AdaptingScale * father.BlockScale, 30 * AdaptingScale * father.BlockScale)
                                    );
                                ColorType = Rand(0, 2);
                                switch (Rand(0, 2))
                                {
                                    case 0:
                                        Delta = new Vector2(Rand(6, 12) * RandSignal(), 0); break;
                                    case 1:
                                        Delta = new Vector2(0, Rand(6, 12) * RandSignal()); break;
                                    case 2:
                                        Delta = new Vector2(Rand(-6, 6), Rand(-6, 6)); break;
                                }
                                Delta *= father.AverageDelta;
                                lastTime = Rand(18, 40);
                            }
                            int lastTime;
                            public int ColorType { get; private set; }
                            public override void Update()
                            {
                                lastTime--;
                                if (lastTime <= 0) Dispose();
                            }
                        }
                        public override void Update()
                        {
                            if (Rand(0, father.AverageInterval) == 0) for (int i = 0; i < father.Intensity; i++) AddChild(new MoveBlock(father));
                        }
                        public Tuple<Rectangle, Vector2, int>[] GetMoves()
                        {
                            List<Tuple<Rectangle, Vector2, int>> res = new();
                            ChildObjects.ForEach(s => res.Add(new Tuple<Rectangle, Vector2, int>((s as MoveBlock).Area, (s as MoveBlock).Delta, (s as MoveBlock).ColorType)));
                            return res.ToArray();
                        }
                    }
                    public Glitching(float dep) : base(null, SpriteSortMode.Immediate, BlendState.Opaque, dep)
                    {
                        updater = new Updater(this);
                        GameStates.InstanceCreate(updater);
                    }
                    public Glitching() : this(0.5f)
                    {
                    }
                    Updater updater;
                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        if (!updater.BeingUpdated) GameStates.InstanceCreate(updater);
                        CopyRenderTarget(HelperTarget, obj);
                        MissionTarget = obj;
                        DrawTexture(obj, Vector2.Zero);

                        Tuple<Rectangle, Vector2, int>[] all = updater.GetMoves();
                        for (int i = 0; i < all.Length; i++)
                        {
                            if (MathF.Abs(RGBSplitIntensity) > 0.1f)
                            {
                                DrawTexture(HelperTarget, new CollideRect(all[i].Item1) + all[i].Item2 + new Vector2(-RGBSplitIntensity, 0), all[i].Item1, Color.Red);
                                DrawTexture(HelperTarget, new CollideRect(all[i].Item1) + all[i].Item2 + new Vector2(RGBSplitIntensity, 0), all[i].Item1, new Color(0, 0, 1f));
                            } DrawTexture(HelperTarget, (new CollideRect(all[i].Item1) + all[i].Item2).ToRectangle(), all[i].Item1, Color.White);
                        }
                        return MissionTarget;
                    }
                    public override void Dispose()
                    {
                        updater.Dispose();
                        base.Dispose();
                    }
                }
                /// <summary>
                /// using the helper channel of 3
                /// </summary>
                public class Filter : RenderProduction
                {
                    public override void WindowSizeChanged(Vector2 vec)
                    {
                        if (screen == null || screen.Bounds.Size != vec.ToPoint())
                            screen = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
                    }
                    public Filter(Shader shader, float dep) : base(shader, SpriteSortMode.Immediate, BlendState.Opaque, dep)
                    {
                    }
                    public Filter(Shader shader) : base(shader, SpriteSortMode.Immediate, BlendState.Opaque, 0.50f)
                    {
                    }

                    private static RenderTarget2D screen;

                    public Shader CurrentShader => Shader;

                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        if (MissionTarget == screen) MissionTarget = HelperTarget3;
                        else MissionTarget = screen;

                        DrawTexture(obj, obj.Bounds);

                    /*    VertexPositionColorTexture[] vertexs = new VertexPositionColorTexture[4];
                        vertexs[0] = new(new Vector3(0, 0, 0.5f), Color.White, new(0, 0));
                        vertexs[1] = new(new Vector3(MissionTarget.Width, 0, 0.5f), Color.White, new(1, 0));
                        vertexs[2] = new(new Vector3(0, MissionTarget.Height, 0.5f), Color.White, new(0, 1));
                        vertexs[3] = new(new Vector3(MissionTarget.Width, MissionTarget.Height, 0.5f), Color.White, new(1, 1));

                        SpriteBatch.Begin();
                        SpriteBatch.DrawVertex(obj, 0.0f, vertexs);
                        SpriteBatch.End();*/

                        return MissionTarget;
                    }
                }
                public class Blur : RenderProduction
                {
                    private bool pendingClear = false;
                    public override void WindowSizeChanged(Vector2 vec)
                    {
                        screen = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
                    }
                    public Blur(float dep) : base(null, SpriteSortMode.Immediate, BlendState.Additive, dep)
                    {
                        pendingClear = true;
                    }
                    public Blur() : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.50f)
                    {
                        pendingClear = true;
                    } 

                    public BlurShader BlurShader { get; set; }  = CustomShaders.Blur;
                    private static RenderTarget2D screen;
                    public float Sigma { get => BlurShader.Sigma; set => BlurShader.Sigma = value; }

                    public bool Glittering { get; set; } = false;
                    public float GlitterScale { get; set; } = 0.0f;

                    public bool KawaseMode { get; set; } = true;

                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        if (Sigma <= 0.05f) return obj;
                        this.SamplerState = null;
                        if(Glittering && GlitterScale > 0.05f)
                        {
                            if (pendingClear) { 
                                this.pendingClear = false;
                                this.MissionTarget = screen;
                                this.ResetTargetColor(Color.Transparent);
                            }
                            this.BlendState = BlendState.Opaque;
                            CopyRenderTarget(HelperTarget, obj); 
                        }
                        Shader = BlurShader = CustomShaders.Blur;

                        float scale = Glittering ? 1.1f : 1;

                        if (!KawaseMode && Settings.SettingsManager.DataLibrary.drawingQuality == Settings.SettingsManager.DataLibrary.DrawingQuality.High)
                        {
                            this.SamplerState = null;
                            this.BlendState = BlendState.Additive;
                            MissionTarget = HelperTarget2;
                            BlurShader.Factor = new Vector2(1, 0) * scale;
                            DrawTexture(obj, Vector2.Zero);

                            MissionTarget = obj;
                            BlurShader.Factor = new Vector2(0, 1) * scale;
                            DrawTexture(HelperTarget2, Vector2.Zero);

                            MissionTarget = HelperTarget2;
                            BlurShader.Factor = new Vector2(1, 1) * scale;
                            DrawTexture(obj, Vector2.Zero);

                            MissionTarget = obj;
                            BlurShader.Factor = new Vector2(1, -1) * scale;
                            DrawTexture(HelperTarget2, Vector2.Zero);
                        }
                        else
                        {
                            //use kawase algorithm
                            this.SamplerState = SamplerState.LinearClamp;
                            BlurKawaseShader kawase = CustomShaders.BlurKawase;
                            this.Shader = kawase;
                            this.BlendState = BlendState.Additive;

                            Vector2 OriginToFactor(Vector2 delta) => delta / new Vector2(640f, 480f);

                            float sigma2 = Sigma * 0.75f;

                            MissionTarget = HelperTarget2;
                            kawase.Factor = OriginToFactor(new Vector2(0.5f, 0.5f) * sigma2) * scale;
                            DrawTexture(obj, Vector2.Zero);

                            MissionTarget = obj;
                            kawase.Factor = OriginToFactor(new Vector2(1.5f, 1.5f) * sigma2) * scale;
                            DrawTexture(HelperTarget2, Vector2.Zero);

                            MissionTarget = HelperTarget;
                            kawase.Factor = OriginToFactor(new Vector2(2.5f, 2.5f) * sigma2) * scale;
                            DrawTexture(obj, Vector2.Zero);
                        }

                        if (Glittering && GlitterScale > 0.05f)
                        {
                            this.SamplerState = null;
                            this.Shader = null;
                            this.MissionTarget = screen;
                            this.BlendState = BlendState.Additive;
                            //  this.DrawTextures(new Texture2D[] { HelperTarget, obj }, HelperTarget.Bounds, null, new Color[] { Color.White * (1 - GlitterScale * 0.35f), Color.White * GlitterScale});
                            this.DrawTextures(new Texture2D[] { HelperTarget, MissionTarget }, HelperTarget.Bounds, null, new Color[] { Color.White, Color.White * GlitterScale});

                            return MissionTarget;
                        }
                        return MissionTarget;
                    }
                }
                public class ScreenShot : RenderProduction
                {
                    public override void WindowSizeChanged(Vector2 vec)
                    {
                        if (screen == null || screen.Bounds.Size != vec.ToPoint())
                        {
                            screen = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
                            screen.Name = "screenShot";
                        }
                    }

                    public void MakeScreenShot()
                    {
                        screenShotEnabled = true;
                    }

                    public bool DrawLastShot { get; set; } = false;

                    bool screenShotEnabled = false;
                    RenderTarget2D Result => screen;
                    public override RenderTarget2D Draw(RenderTarget2D obj)
                    {
                        if (screenShotEnabled)
                        {
                            screenShotEnabled = false;
                            MissionTarget = screen;
                            BlendState = BlendState.Opaque;
                            ResetTargetColor(Color.Transparent);
                            DrawTexture(obj, Vector2.Zero);
                        }
                        if (DrawLastShot)
                        {
                            MissionTarget = obj;
                            BlendState = BlendState.Additive;
                            ResetTargetColor(Color.Transparent);
                            DrawTexture(Result, Vector2.Zero);
                        }
                        return obj;
                    }

                    public ScreenShot(float dep) : base(null, SpriteSortMode.Immediate, BlendState.Opaque, dep)
                    {
                    }

                    private static RenderTarget2D screen;
                }
            }
        }
    }
}