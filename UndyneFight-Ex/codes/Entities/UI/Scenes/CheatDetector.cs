using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace UndyneFight_Ex.Entities
{
    internal class CheatDetector : Entity
    {
        class FrameDetector : Entity
        {
            public FrameDetector()
            {
                cur = DateTime.Now;
                UpdateIn120 = true;
            }
            DateTime cur;
            int count = 0;
            float frameAverage = 0;
            List<int> frames = new List<int>();
            int timeSustain0 = 0, timeSustain1 = 0, timeSustain2 = 0;
            public override void Draw()
            {
#if DEBUG
                GLFont font = GlobalResources.Font.FightFont;
                Color color = Color.White;
                if (timeSustain2 > 1) color = Color.DarkRed;
                else if (timeSustain1 > 1) color = Color.Red;
                else if (timeSustain0 > 1) color = Color.Orange;
                else if (timeSustain0 >= 1) color = Color.Yellow;

                font.Draw(frameAverage.ToString("F1"), new(0, 0), color, 0.6f, 0.5f);
#endif
            }

            public override void Update()
            {
                count++;
                DateTime time = DateTime.Now;
                if (time.Second != cur.Second)
                {
                    cur = time;
                    frames.Add(count);
                    if (frames.Count > 5)
                    {
                        frames.RemoveAt(0);
                    }
                    frameAverage = 0;
                    frames.ForEach(s => { frameAverage += s / frames.Count; });

                    if (count < 120 * GameMain.GameSpeed) timeSustain0++; else timeSustain0 = 0;
                    if (count < 115 * GameMain.GameSpeed) timeSustain1++; else timeSustain1 = 0;
                    if (count < 110 * GameMain.GameSpeed) timeSustain2++; else timeSustain2 = 0;
                    if (timeSustain0 > 10 || timeSustain1 > 6 || timeSustain2 > 3)
                    {
#if !DEBUG
                        (this.CurrentScene as FightScene).PlayDeath();
#endif
                    }

                    count = 0;
                }
            }
        }
        class ProcessDetector : GameObject
        {
            int appearTime = 0;
            public override void Update()
            {
                appearTime++;
                if (appearTime == 1)
                {
                    Process[] all = Process.GetProcesses();
                    for (int i = 0; i < all.Length; i++)
                    {
                        Process process = all[i];
                        string name = process.ProcessName;
                        if (name.Contains("Cheat Engine") || name.Contains("cheatengine"))
                        {
                            process.Kill();
                        }
                    }
                }
                if (appearTime >= 1500 || appearTime < 0)
                {
                    appearTime = 0;
                }
            }
        }
        public override void Start()
        {
            AddChild(new FrameDetector());
            AddChild(new ProcessDetector());
            base.Start();
        }
        public override void Draw()
        {
        }

        public override void Update()
        {
        }
        public override void Dispose()
        {
            // Unable to dispose!
        }
    }
}