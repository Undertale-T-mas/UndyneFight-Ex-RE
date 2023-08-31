using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Settings;
using static UndyneFight_Ex.Settings.SettingsManager.DataLibrary;

namespace UndyneFight_Ex.UserService
{
    public class Settings : ISaveLoad
    {
        public List<ISaveLoad> Children => throw new NotImplementedException();

        int masterVolume, spearBlockingVolume, reduceBlueAmount, drawingQuality;
        float arrowDelay, arrowSpeed, arrowScale, fps;
        bool dialogAvailable, perciseWarning, mirror;
        string samplerState;

        public void Load(SaveInfo info)
        {
            masterVolume = info.Nexts.ContainsKey("masterVolume")
                ? info.Nexts["masterVolume"].IntValue : SettingsManager.DataLibrary.masterVolume;

            spearBlockingVolume = info.Nexts.ContainsKey("spearBlockingVolume")
                ? info.Nexts["spearBlockingVolume"].IntValue
                : SpearBlockingVolume;

            reduceBlueAmount = info.Nexts.ContainsKey("reduceBlueAmount")
                ? info.Nexts["reduceBlueAmount"].IntValue
                : SettingsManager.DataLibrary.reduceBlueAmount;

            arrowDelay = info.Nexts.ContainsKey("arrowDelay") ? info.Nexts["arrowDelay"].FloatValue : ArrowDelay;

            arrowScale = info.Nexts.ContainsKey("arrowScale") ? info.Nexts["arrowScale"].FloatValue : ArrowScale;

            arrowSpeed = info.Nexts.ContainsKey("arrowSpeed") ? info.Nexts["arrowSpeed"].FloatValue : ArrowSpeed;

            fps = info.Nexts.ContainsKey("fps") ? info.Nexts["fps"].FloatValue : DrawFPS;

            mirror = info.Nexts.ContainsKey("mirror") ? info.Nexts["mirror"].BoolValue : Mirror;

            dialogAvailable = info.Nexts.ContainsKey("dialogAvailable")
                ? info.Nexts["dialogAvailable"].BoolValue
                : SettingsManager.DataLibrary.dialogAvailable;

            perciseWarning = info.Nexts.ContainsKey("perciseWarning") ? info.Nexts["perciseWarning"].BoolValue : SettingsManager.DataLibrary.perciseWarning;

            drawingQuality = info.Nexts.ContainsKey("drawingQuality")
                ? info.Nexts["drawingQuality"].IntValue
                : (int)SettingsManager.DataLibrary.drawingQuality;

            samplerState = info.Nexts.ContainsKey("samplerState")
                ? info.Nexts["samplerState"].StringValue
                : SamplerState;
        }
        public void Apply()
        {
            SettingsManager.DataLibrary.masterVolume = masterVolume;
            SpearBlockingVolume = spearBlockingVolume;
            SettingsManager.DataLibrary.reduceBlueAmount = reduceBlueAmount;
            ArrowDelay = arrowDelay;
            ArrowSpeed = arrowSpeed;
            ArrowScale = arrowScale;
            Mirror = mirror;
            DrawFPS = MathUtil.Clamp(25, fps, 125);
            SettingsManager.DataLibrary.perciseWarning = perciseWarning;
            SettingsManager.DataLibrary.dialogAvailable = dialogAvailable;
            SettingsManager.DataLibrary.drawingQuality = (DrawingQuality)drawingQuality;
            SamplerState = samplerState;
            SettingsResetInterface.ApplySettings();
        }

        public SaveInfo Save()
        {
            masterVolume = SettingsManager.DataLibrary.masterVolume;
            spearBlockingVolume = SpearBlockingVolume;
            reduceBlueAmount = SettingsManager.DataLibrary.reduceBlueAmount;
            arrowDelay = ArrowDelay;
            arrowSpeed = ArrowSpeed;
            arrowScale = ArrowScale;
            fps = DrawFPS;
            mirror = Mirror;
            perciseWarning = SettingsManager.DataLibrary.perciseWarning;
            dialogAvailable = SettingsManager.DataLibrary.dialogAvailable;
            drawingQuality = (int)SettingsManager.DataLibrary.drawingQuality;
            samplerState = SamplerState;
            
            SaveInfo info = new("Settings{");
            info.PushNext(new SaveInfo("masterVolume:" + masterVolume));
            info.PushNext(new SaveInfo("spearBlockingVolume:" + spearBlockingVolume));
            info.PushNext(new SaveInfo("reduceBlueAmount:" + reduceBlueAmount));
            info.PushNext(new SaveInfo("arrowDelay:" + MathUtil.FloatToString(arrowDelay, 3)));
            info.PushNext(new SaveInfo("arrowSpeed:" + MathUtil.FloatToString(arrowSpeed, 3)));
            info.PushNext(new SaveInfo("arrowScale:" + MathUtil.FloatToString(arrowScale, 3)));
            info.PushNext(new SaveInfo("fps:" + MathUtil.FloatToString(fps, 3)));
            info.PushNext(new SaveInfo("mirror:" + (mirror ? "true" : "false")));
            info.PushNext(new SaveInfo("perciseWarning:" + (perciseWarning ? "true" : "false")));
            info.PushNext(new SaveInfo("dialogAvailable:" + (dialogAvailable ? "true" : "false")));
            info.PushNext(new SaveInfo("drawingQuality:" + drawingQuality));
            info.PushNext(new SaveInfo("samplerState:" + samplerState));
            return info;
        }
    }

    public class Statistic : ISaveLoad
    {
        public List<ISaveLoad> Children => null;

        public int DeathCount { get; private set; } = 0;
        public int PlayedTime
        {
            get
            {
                UpdateTime();
                return (int)playedTime;
            }
        }
        float playedTime = 0;
        private void UpdateTime()
        {
            TimeSpan del = DateTime.Now - span;
            playedTime += (float)del.TotalSeconds;
            span = DateTime.Now;
        }

        public void AddDeath()
        {
            DeathCount++;
        }
        DateTime span = DateTime.Now;
        public void Load(SaveInfo info)
        {
            DeathCount = info.Nexts["DeathCount"].IntValue;
            playedTime = info.Nexts["PlayedTime"].FloatValue;
        }

        public SaveInfo Save()
        {
            SaveInfo info = new("Statistic{");
            info.PushNext(new SaveInfo("DeathCount:" + DeathCount));
            info.PushNext(new SaveInfo("PlayedTime:" + MathUtil.FloatToString(playedTime, 4)));
            return info;
        }
    }
}