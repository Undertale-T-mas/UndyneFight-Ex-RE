using System;
using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex
{
    public class Challenge
    {
        public Challenge(string iconPath, string title, Tuple<Type, Difficulty>[] routes)
        {
            IconPath = iconPath;
            Title = title;
            Routes = routes;
        }

        public Challenge(string title, Tuple<Type, Difficulty>[] routes)
        {
            IconPath = "";
            Title = title;
            Routes = routes;
        }

        public static void Initialize()
        {

        }

        public string IconPath;
        public string Title;

        public Tuple<Type, Difficulty>[] Routes;

        public List<SongResult> ResultBuffer { get; init; } = new();

        public void Reset()
        {
            ResultBuffer.Clear();
        }
    }
}