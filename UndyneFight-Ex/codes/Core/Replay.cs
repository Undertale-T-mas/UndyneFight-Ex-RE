using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex
{
    internal class Recorder : Entity
    {
        public Recorder()
        {
            records = new List<KeyboardState>();
        }
        public override void Draw()
        {
            GlobalResources.Font.NormalFont.CentreDraw("RECORD", new Vector2(320, 60), (playTime % 120 > 59) ? Color.Red : Color.Yellow);
        }

        private int playTime = 0;
        private readonly List<KeyboardState> records;

        public override void Update()
        {
            records.Add(GameStates.currentKeyState2);
            playTime++;
        }

        private static char FromKeyState(Keys key, KeyboardState state)
        {
            return state.IsKeyDown(key) ? 'T' : 'F';
        }

        private static string ToInfo(KeyboardState state, int id)
        {
            string info = id + ":";
            info += FromKeyState(Keys.W, state);
            info += FromKeyState(Keys.A, state);
            info += FromKeyState(Keys.S, state);
            info += FromKeyState(Keys.D, state);
            info += FromKeyState(Keys.Up, state);
            info += FromKeyState(Keys.Left, state);
            info += FromKeyState(Keys.Right, state);
            info += FromKeyState(Keys.Down, state);
            info += FromKeyState(Keys.X, state);
            info += FromKeyState(Keys.LeftShift, state);
            info += FromKeyState(Keys.Z, state);
            info += FromKeyState(Keys.Space, state);
            return info;
        }

        private static readonly string[] difficultyName = { "noob", "easy", "normal", "hard", "extreme" };

        public void Flush()
        {
            List<string> strs = new()
            {
                "typeName:" + GameStates.waveSet.GetType().Name,
                "difficulty:" + GameStates.difficulty,
                "seed:" + GameStates.seed,
                "mode:" + 0
            };

            var v = records.GetEnumerator();
            int id = 0;
            while (v.MoveNext())
            {
                string rec = ToInfo(v.Current, id);
                strs.Add(rec);
                id++;
            }
            DateTime date = DateTime.Now;
            FlushArea.fileName = GameStates.waveSet.FightName + "_" + difficultyName[GameStates.difficulty] + "_" + /* + date.Month + "_" + date.Day + "_" */+date.Hour + "_" + date.Minute + "_" + date.Second;
            FlushArea.fileDatas = IO.IOEvent.StringToByte(strs);
        }

        public static void Save()
        {
            FlushArea.Record();
        }

        private static class FlushArea
        {
            public static string fileName;
            public static List<byte> fileDatas;
            public static void Record()
            {
                IO.IOEvent.WriteCustomFile("Datas\\Records\\" + fileName + ".Tmpf", fileDatas);
            }
        }
    }
    internal class Replayer : Entity
    {
        private static KeyboardState[] states;

        public static bool FromInfo(string str, int pos)
        {
            return str[pos] == 'T';
        }

        public static KeyboardState GetState(string val)
        {
            List<Keys> keys = new();
            if (FromInfo(val, 0)) keys.Add(Keys.W);
            if (FromInfo(val, 1)) keys.Add(Keys.A);
            if (FromInfo(val, 2)) keys.Add(Keys.S);
            if (FromInfo(val, 3)) keys.Add(Keys.D);
            if (FromInfo(val, 4)) keys.Add(Keys.Up);
            if (FromInfo(val, 5)) keys.Add(Keys.Left);
            if (FromInfo(val, 6)) keys.Add(Keys.Right);
            if (FromInfo(val, 7)) keys.Add(Keys.Down);
            if (FromInfo(val, 8)) keys.Add(Keys.X);
            if (FromInfo(val, 9)) keys.Add(Keys.LeftShift);
            if (FromInfo(val, 10)) keys.Add(Keys.Z);
            if (FromInfo(val, 11)) keys.Add(Keys.Space);
            KeyboardState state = new(keys.ToArray());
            return state;
        }

        public static IO.SaveInfo Info
        {
            set
            {
                int count = value.Nexts.Count - 4;
                states = new KeyboardState[count];
                for (int i = 0; i < count; i++)
                    states[i] = GetState(value.Nexts[i.ToString()].StringValue);
            }
        }

        private int appearTime = 0;
        public override void Update()
        {
            int det = 0;
            if (appearTime < 0) return;
            if (appearTime + det >= states.Length) { Dispose(); return; }
            GameStates.currentKeyState2 = states[appearTime + det];
            appearTime++;
        }

        public override void Draw()
        {

        }
    }
}