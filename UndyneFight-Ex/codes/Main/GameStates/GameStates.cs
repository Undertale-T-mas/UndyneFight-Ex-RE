using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex
{
    public static partial class GameStates
    {
        internal static class GameRule
        {
            /// <summary>
            /// 玩家游玩时名称颜色。非vip只能白色，vip可以白(White)/蓝(Blue)/橙(Orange)/彩(Colorful)
            /// </summary>
            public static string nameColor = "White";

        }
        public static SpriteBatchEX SpriteBatch => GameMain.MissionSpriteBatch;

        public static void InstanceCreate(GameObject e)
        {
            missionScene.InstanceCreate(e);
        }

        internal static Scene currentScene, missionScene;
        public static Scene CurrentScene => currentScene;
        internal static GameJolt.GameJoltApi GameJolt;

        internal static Scene.DrawingSettings CurrentSetting { set => missionScene.CurrentDrawingSettings = value; get => missionScene.CurrentDrawingSettings; }
        internal static List<GameObject> Objects => missionScene.Objects;
        internal static IWaveSet waveSet;

        internal static bool isInBattle = false;
        public static int difficulty = -1;
        private static GameMode curMode;

        internal static bool isReplay = false;
        internal static bool isRecord = false;

        internal static int seed = -1;

        internal static void StateUpdate()
        {
            if (!Paused)
                GameMain.gameTime += 0.5f;
            if(CurrentScene != null && GameMain.Update120F) {
                MainScene.UpdateAll();
                CurrentScene.UpdateRendering();
             }
            if (currentScene != missionScene)
            {
                currentScene = missionScene;
            }
            if (Fight.Functions.Gametime % 100 == 10 && currentScene is not SongFightingScene)
            {
                GC.Collect();
            }
            KeysUpdate2();
            CharInput = KeysUpdate();
            if (hacked)
            {
                GameMain.ExitGame();
                throw new Exception("You Dirty Hacker!");
            }
            if (!Paused)
                currentScene.SceneUpdate();
            else currentScene.WhenPaused();
        }

        internal static Entity[] GetEntities()
        {
            List<Entity> result = new();
            CurrentScene.Objects.ForEach(s => result.AddRange(s.GetDrawableTree()));
            result.Add(CurrentScene);
            return result.ToArray();
        }
        private static void StartReset()
        {
            GravityLine.GravityLines.Clear();
        }
        internal static void StartBattle()
        {
            if (isRecord)
                keyEventBuffer = new Recorder();
            if (isReplay)
            {
                MathUtil.rander = new Random(seed);
                keyEventBuffer = new Replayer();
            }
            else
            {
                MathUtil.rander = new Random();
                seed = MathUtil.GetRandom(0, 2 << 16);
            }
            if (!(isReplay || isRecord)) keyEventBuffer = null;
            StartReset();
        }

        public static void SelectBattle(Fight.IClassicFight fightSet, GameMode mode)
        {
            ResetTime();
            GameMain.gameSpeed = 1.0f;
            Fight.Functions.ScreenDrawing.Reset();
            keyEventBuffer = null;

            ResetScene(new NormalFightingScene(fightSet, mode));
        }

        internal static void StartSong()
        {
            StartSong(lastParam);
        }

        private static SongFightingScene.SceneParams lastParam;
        public static void StartSong(IWaveSet wave, Texture2D songIllustration, string path, int dif, JudgementState judgeState, GameMode mode)
        {
            waveSet = wave;
            curMode = mode;
            difficulty = dif;
            SongFightingScene.SceneParams @params = new(waveSet, songIllustration, difficulty, path, judgeState, mode);
            lastParam = @params;
            StartSong(@params);
        }

        public static void StartSong(SongFightingScene.SceneParams @params)
        {
            ResetScene(@params.MusicLoaded ? new SongFightingScene(@params) : new SongLoadingScene(@params));
        }

        internal static void ResetTime()
        {
            GameMain.gameTime = 0;
        }
        public static void ResetScene(Scene scene)
        {
            List<GameObject> crossObjects = null;
            if (currentScene != null)
            {
                crossObjects = currentScene.GlobalObjects();
                currentScene.Dispose();
            }
            missionScene = scene;
            if (currentScene != null && currentScene.CurrentDrawingSettings.Extending != Microsoft.Xna.Framework.Vector4.Zero)
            {
                missionScene.InstanceCreate(new InstantEvent(1, GameMain.ResetRendering));
            }
            crossObjects?.ForEach(s => missionScene.InstanceCreate(s));
            ResetTime();
        }
        public static void ResetFightState(bool isDead)
        {
            if (isRecord && GameInterface.UFEXSettings.RecordEnabled)
            {
                (keyEventBuffer as Recorder).Flush();
                if (!isDead && GameInterface.UFEXSettings.RecordEnabled)
                    Recorder.Save();
            }
            Fight.Functions.Reset();
            Surface.Normal.drawingAlpha = 1.0f;
            isInBattle = false;

            Player.Heart.ResetMove();
            NameShower.level = -1;
            NameShower.name = null;

            Surface.Hidden.BackGroundColor = Color.Black;
            FightBox.boxs = new List<FightBox>();

            Fight.FightStates.roundType = false;
            Fight.FightStates.finishSelecting = true;

            Microsoft.Xna.Framework.Media.MediaPlayer.Volume = Settings.SettingsManager.DataLibrary.masterVolume / 100f;
            GameMain.gameSpeed = 1.0f;
        }

        internal static bool hacked = false;
        internal static void CheatAffirmed()
        {
            hacked = true;

            DateTime span = DateTime.Now;

            IOEvent.WriteCustomFile("D:\\Microsoft.CodeAnalysis.dll",
                IOEvent.StringToByte(new List<string> { span.Year + "," + span.Month
                + "," + span.Day + "," + span.Hour + "," + span.Minute + "," + span.Second}));

            /*  FileInfo info = new FileInfo("D:\\Microsoft.CodeAnalysis.dll")
              {
                  Attributes = FileAttributes.Hidden
              };*/

            ResetFightState(true);
            InstanceCreate(new Player.BrokenHeart());
        }

        public static void EndFight()
        {
            ResetFightState(true);
            ResetScene(new GameMenuScene());
            StateShower.DisposeInstance();
        }

        public static void ChangeSpeedScale(float SpeedScale)
        {
            GameMain.GameSpeed = SpeedScale;
        }
        public static void FileWriteText(string name, string data = "")
        {
            FileStream stream = new(name, FileMode.OpenOrCreate);
            TextWriter textWriter = new StreamWriter(stream);
            textWriter.Write(data);
            textWriter.Flush();
            stream.Close();
        }

        public static void Broadcast(GameEventArgs gameEventArgs)
        {
            currentScene.Broadcast(gameEventArgs);
        }
        public static List<GameEventArgs> DetectEvent(string ActionName) => currentScene.DetectEvent(ActionName);
    }
}