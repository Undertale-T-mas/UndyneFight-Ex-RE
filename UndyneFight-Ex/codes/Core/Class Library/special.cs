using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using System;
namespace UndyneFight_Ex
{

    public static class BSet
    {

        public static bool final { get; set; } = false;
        public static bool again { get; set; } = true;
        public static bool col { get; set; } = false;
        public static bool LastShield { get; set; } = false;
        public static string Windowname { set; get; } = $"Rhythm Recall v0.2.2 (UF-Ex [V{ModDynamic.UFExVersion}])";
        public static bool timestop { get; set; } = false;
        public static bool FullScreen { get; set; } = false;
        public static Color SideColor { get; set; } = Color.Black;
        public static Color windowcolor { get; set; } = Color.White;
        public static bool pause { get; set; } = true;
        public static bool problem { get; set; } = false;
        private static string ESD = "Eternal Spring Dream", NOK = "Night Of Knights", DB = "Dream Battle";
        public static bool BAAD 
        { 
            get =>
#if DEBUG
                DateTime.Now.Day == 14;
#else
                true
#endif
        }
        public static void GetACC(float ACC, string songname, Difficulty dif)
        {
            if (PlayerManager.CurrentUser != null&&BAAD)
            {
                var songs = PlayerManager.CurrentUser.SongManager;
                Dictionary<string, SongData> dic = new();
                foreach (var v in songs.AllDatas)
                {
                    dic.Add(v.SongName, v);
                }
                if (dic.ContainsKey("Bad apple"))
                {
                    if (
                    dic["Bad apple"].CurrentSongStates.ContainsKey(Difficulty.Extreme)
                    )
                    {
                        if (dic["Bad apple"].CurrentSongStates[Difficulty.Extreme].Accuracy !=0)
                        {
                            var data = PlayerManager.CurrentUser.Custom;
                            int count = -1;
                            if (data.Nexts.ContainsKey("BadApple%"))
                                count = data.Nexts["BadApple%"].IntValue;
                            if
                                (ACC > 0.95f && dif == Difficulty.Extreme &&
                                songname == ESD && !data.Nexts.ContainsKey("BadApple%"))
                            {
                                data.PushNext(new("BadApple%:value=0"));
                            }
                            else if
                                (ACC > 0.95f && dif == Difficulty.Hard &&
                                songname == NOK && count == 0)
                                data.Nexts["BadApple%"]["value"] = "1";
                            else if
                                (ACC > 0.95f && dif == Difficulty.Extreme &&
                                songname == DB && count == 1)
                            {
                                data.Nexts["BadApple%"]["value"] = "2";
                                problem = true;
                            }
                            PlayerManager.Save();
                        }
                    }
                }
            }
        }
        public static void ResetALL()
        {
            Windowname = $"Rhythm Recall v0.2.2 (UF-Ex [V{ModDynamic.UFExVersion}])";
            SideColor = Color.Black;
            final = false;
            again = true;
            timestop = false;
            FullScreen = false;
            col = false;
            LastShield = false;
            pause = true;
            RemoveUI1();
            RemoveUI2();
            RemoveUI3();

        }
        private static List<UISS> bufferProduction = new();
        private static List<UISS2> bufferProduction2 = new();
        private static List<TextLumi> bufferProduction3 = new();
        private static List<sp> bufferProduction4 = new();
        private static List<Hidden> Hiddens = new();
        public class UISS : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public UISS(Surface uiSurf) : base(null, SpriteSortMode.Immediate, BlendState.AlphaBlend, 0.54f)
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
        public static UISS UIS()
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("Entity"))
                scene.CurrentDrawingSettings.surfaces.Add("Entity", surf = new("Entity")
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces["Entity"];
            MoveSurface(surf);
            UISS production;
            Functions.ScreenDrawing.SceneRendering.InsertProduction(production = new UISS(surf));
            bufferProduction.Add(production);
            return production;
        }
        public class Hidden : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public Hidden(Surface uiSurf) : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.35f)
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
        public static Hidden HiddenS()
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("HiddenT"))
                scene.CurrentDrawingSettings.surfaces.Add("HiddenT", surf = new("HiddenT")
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces["HiddenT"];
            MoveSurface(surf);
            Hidden production;
            Functions.ScreenDrawing.BackGroundRendering.InsertProduction(production = new Hidden(surf));
            Hiddens.Add(production);
            return production;
        }

        private static void MoveSurface(Surface surface)
        {
            SongFightingScene scene = GameStates.CurrentScene as SongFightingScene;

        }
        public class UISS2 : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public UISS2(Surface uiSurf) : base(null, SpriteSortMode.Immediate, BlendState.AlphaBlend, 1f)
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
        public static UISS2 UIS2()
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("Entity2"))
                scene.CurrentDrawingSettings.surfaces.Add("Entity2", surf = new("Entity2")
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces["Entity2"];
            MoveSurface(surf);
            UISS2 production;
            Functions.ScreenDrawing.SceneRendering.InsertProduction(production = new UISS2(surf));
            bufferProduction2.Add(production);
            return production;
        }
        public class sp : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public sp(Surface uiSurf) : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.98536f)
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
        public static sp sps()
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("Entity6"))
                scene.CurrentDrawingSettings.surfaces.Add("Entity6", surf = new("Entity6")
                {
                    BlendState = BlendState.Additive,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces["Entity6"];
            MoveSurface(surf);
            sp production;
            Functions.ScreenDrawing.SceneRendering.InsertProduction(production = new sp(surf));
            bufferProduction4.Add(production);
            return production;
        }
        public static void RemoveUI1()
        {
            bufferProduction.ForEach(s => s.Dispose());
            var surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
            MoveSurface(surfaces["normal"]);
            if (surfaces.ContainsKey("Entity"))
            {
                surfaces["Entity"].Dispose();
                surfaces.Remove("Entity");
            }
        }
        public static void RemoveUI2()
        {
            bufferProduction2.ForEach(s => s.Dispose());
            var surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
            MoveSurface(surfaces["normal"]);
            if (surfaces.ContainsKey("Entity2"))
            {
                surfaces["Entity2"].Dispose();
                surfaces.Remove("Entity2");
            }
        }
        public static void RemoveUI3()
        {
            bufferProduction2.ForEach(s => s.Dispose());
            var surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
            MoveSurface(surfaces["normal"]);
            if (surfaces.ContainsKey("Text"))
            {
                surfaces["Text"].Dispose();
                surfaces.Remove("Text");
            }
        }
        public static void Resetball()
        {
            ballR.ForEach(s => s.Dispose());
            var surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
            MoveSurface(surfaces["ball"]);
            if (surfaces.ContainsKey("ball"))
            {
                surfaces["ball"].Dispose();
                surfaces.Remove("ball");
            }
            Hiddens.ForEach(s => s.Dispose());
            if (surfaces.ContainsKey("HiddenT"))
            {
                surfaces["HiddenT"].Dispose();
                surfaces.Remove("HiddenT");
            }
        }
        public class TextLumi : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public TextLumi(Surface surface) : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.995f)
            {
                this.uiSurf = surface;
            }
            public override RenderTarget2D Draw(RenderTarget2D obj)
            {
                MissionTarget = obj;
                Shader = null;
                DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                Shader = null;
                DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                return MissionTarget;
            }
        }
        public static TextLumi TextShader()
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("Text"))
                scene.CurrentDrawingSettings.surfaces.Add("Text", surf = new("Text")
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces["Text"];
            MoveSurface(surf);
            TextLumi production;
            Functions.ScreenDrawing.SceneRendering.InsertProduction(production = new TextLumi(surf));
            bufferProduction3.Add(production);
            return production;
        }
        public class ballRender : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public ballRender(Surface uiSurf) : base(null, SpriteSortMode.Immediate, BlendState.Additive, 0.4f)
            {
                this.uiSurf = uiSurf;
            }

            public override RenderTarget2D Draw(RenderTarget2D obj)
            {
                MissionTarget = obj;
                Shader = FightResources.Shaders.Cos1Ball;
                DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                return MissionTarget;
            }
        }
        private static List<ballRender> ballR = new();
        public static ballRender ballL()
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey("ball"))
                scene.CurrentDrawingSettings.surfaces.Add("ball", surf = new("ball")
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces["ball"];
            ballRender production;
            Functions.ScreenDrawing.BackGroundRendering.InsertProduction(production = new ballRender(surf));
            ballR.Add(production);
            return production;
        }
        public class Custom : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public Custom(Surface uiSurf, Shader shader) : base(shader, SpriteSortMode.Immediate, BlendState.AlphaBlend, 0.4f)
            {
                this.uiSurf = uiSurf;
            }

            public override RenderTarget2D Draw(RenderTarget2D obj)
            {
                MissionTarget = obj;
                Shader = FightResources.Shaders.Blur;
                DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                return MissionTarget;
            }
        }
        public class Custom2 : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public Custom2(Surface uiSurf, Shader shader) : base(shader, SpriteSortMode.Immediate, BlendState.AlphaBlend, 0.45f)
            {
                this.uiSurf = uiSurf;
            }

            public override RenderTarget2D Draw(RenderTarget2D obj)
            {
                MissionTarget = obj;
                Shader = FightResources.Shaders.Blur;
                DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                return MissionTarget;
            }
        }
        public class Custom3 : RenderProduction
        {
            public Surface UISurface => uiSurf;
            Surface uiSurf;
            public Custom3(Surface uiSurf, Shader shader) : base(shader, SpriteSortMode.Immediate, BlendState.AlphaBlend, 0.47f)
            {
                this.uiSurf = uiSurf;
            }

            public override RenderTarget2D Draw(RenderTarget2D obj)
            {
                MissionTarget = obj;
                Shader = FightResources.Shaders.Blur;
                DrawTexture(uiSurf.RenderPaint, Vector2.Zero, Color.White);
                return MissionTarget;
            }
        }
        private static List<Custom> CustomRenders1 = new();
        private static List<Custom2> CustomRenders2 = new();
        private static List<Custom3> CustomRenders3 = new();
        public static Custom CustomRender1(RenderingManager rend, string path, Shader shader = null)
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey(path))
                scene.CurrentDrawingSettings.surfaces.Add(path, surf = new(path)
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces[path];
            var production = new Custom(surf, shader);
            rend.InsertProduction(production);
            CustomRenders1.Add(production);
            return production;
        }
        public static Custom2 CustomRender2(RenderingManager rend, string path, Shader shader = null)
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey(path))
                scene.CurrentDrawingSettings.surfaces.Add(path, surf = new(path)
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces[path];
            var production = new Custom2(surf, shader);
            rend.InsertProduction(production);
            CustomRenders2.Add(production);
            return production;
        }
        public static Custom3 CustomRender3(RenderingManager rend, string path, Shader shader = null)
        {
            Surface surf;
            Scene scene = GameStates.CurrentScene;
            if (!scene.CurrentDrawingSettings.surfaces.ContainsKey(path))
                scene.CurrentDrawingSettings.surfaces.Add(path, surf = new(path)
                {
                    BlendState = BlendState.AlphaBlend,
                    SpriteSortMode = SpriteSortMode.FrontToBack,
                    Transfer = Surface.TransferUse.ForceDefault,
                    DisableExpand = true
                });
            else surf = scene.CurrentDrawingSettings.surfaces[path];
            var production = new Custom3(surf, shader);
            rend.InsertProduction(production);
            CustomRenders3.Add(production);
            return production;
        }
        public static void RemoveCustom(string path)
        {
            if(path=="inyou")CustomRenders1.ForEach(s => s.Dispose());
            CustomRenders2.ForEach(s => s.Dispose());
            CustomRenders3.ForEach(s => s.Dispose());

            var surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
            if (surfaces.ContainsKey(path))
            {
                surfaces[path].Dispose();
                surfaces.Remove(path);
            }
        }

    }

}