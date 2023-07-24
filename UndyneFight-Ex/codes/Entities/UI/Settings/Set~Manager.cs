using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Settings.SettingLibrary;

namespace UndyneFight_Ex.Settings
{
    internal abstract class Setting : Entity, ISelectAble
    {
        protected Setting(string settingTitle, Vector2 centre)
        {
            _textSelection = new TextSelection(settingTitle, centre);
            _centre = centre;
            _settingTitle = settingTitle;
        }

        private TextSelection _textSelection;
        private string _settingTitle;
        private Vector2 _centre;
        protected string showingValue { set; private get; }
        protected bool IsSelected { private set; get; } = false;

        public override void Draw()
        {
            _textSelection.Draw();
        }

        public void DeSelected()
        {
            _textSelection.DeSelected();
            IsSelected = false;
        }

        public void Selected()
        {
            _textSelection.Selected();
            IsSelected = true;
        }

        public abstract void SelectionEvent();
        public abstract void Save();

        public override void Update()
        {
            _textSelection.subText = showingValue;
            _textSelection.Update();
        }
    }
    public static class SettingsManager
    {
        public static Type[] Settings { get; private set; }

        internal static void Initialize()
        {
            Settings = new Type[]
            {
                typeof(MasterVolume),
                typeof(SpearBlockedVolume),
                typeof(DrawingQualitySetter),
                typeof(ArrowSpeed),
                typeof(ArrowScale),
                typeof(IsMirror),
                typeof(ArrowDelay),
                typeof(DialogAvailable),
                typeof(PerciseWarning),
                typeof(ReduceBlue),
            };
        }

        public static class DataLibrary
        {
            public enum DrawingQuality
            {
                Low = 0,
                Normal = 1,
                High = 2
            }
            public static int masterVolume = 100;
            public static bool debugMessage = false;
            public static bool dialogAvailable = true;
            public static bool perciseWarning = false;
            public static int reduceBlueAmount = 0;
            public static DrawingQuality drawingQuality = DrawingQuality.High;

            public static int SpearBlockingVolume { get; set; } = 100;
            public static float ArrowSpeed { get; set; } = 1.0f;
            public static float ArrowDelay { get; set; } = 0.0f;
            public static float ArrowScale { get; set; } = 1.0f;
            public static bool Mirror { get; set; } = false;
            public static bool PauseCheating { get; set; } = true;
            public static float DrawFPS { get; set; } = 60f;
            public static string SamplerState { get; set; } = "Nearest";
        }
    }
}