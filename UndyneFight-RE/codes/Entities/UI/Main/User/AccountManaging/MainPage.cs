﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Net.Sockets;
using UndyneFight_Ex.SongSystem;
using System.ComponentModel.Design.Serialization;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.UserService;
using UndyneFight_Ex.Remake.Texts; 

namespace UndyneFight_Ex.Remake.UI
{ 
    public class AccountManager : SmartSelector
    {
        private static void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
        {
            DrawingLab.DrawLine(start, end, size, color, 0.5f);
        } 

        User AccountData;
        public AccountManager() {
            AccountData = PlayerManager.CurrentUser;
        }

        public override void Start()
        {
            base.Start();

            // Do initializes
            this.InitializeColor();
            this.InitializeMedal();
        }
        Color[] vertexColors;

        Texture2D[] medalTextures;
        int[] medalStates;

        void InitializeMedal()
        {
            float skill = AccountData.Skill;
            this.medalTextures = new[] { GlobalResources.Sprites.brimMedal, GlobalResources.Sprites.medal, GlobalResources.Sprites.starMedal };
            if (skill >= 95) this.medalStates = new[] { 2, 2, 2 };
            else if (skill >= 92.5f) this.medalStates = new[] { 2, 2, 1 };
            else if (skill >= 90) this.medalStates = new[] { 2, 1, 1 };
            else if (skill >= 80) this.medalStates = new[] { 1, 1, 1 };
            else if (skill >= 70) this.medalStates = new[] { 1, 1, 0 };
            else if (skill >= 60) this.medalStates = new[] { 1, 0, 0 };
            else this.medalStates = new[] { 0, 0, 0 };
        }

        void InitializeColor()
        {
            vertexColors = new Color[5];
            float skill = AccountData.Skill;
            int id = 0;
            if (skill >= 20)
                for (int i = 9; i >= 2; i--)
                {
                    if (skill >= i * 10)
                    {
                        id = i - 1;
                        break;
                    }
                }
            Color lerp1;
            if(id == 0)
            {
                lerp1 = Color.Lerp(ratingLevels[0], ratingLevels[1], skill / 20f);
            }
            else
            {
                lerp1 = Color.Lerp(ratingLevels[id], ratingLevels[id + 1], (skill % 10f) / 10f);
            }
            vertexColors[0] = ratingLevels[id];
            vertexColors[1] = Color.Lerp(vertexColors[0], lerp1, 0.35f);
            vertexColors[2] = Color.Lerp(vertexColors[0], lerp1, 0.65f);
            vertexColors[4] = Color.Lerp(vertexColors[0], lerp1, 0.5f);
            vertexColors[3] = lerp1;
             
        }

        public override void Draw()
        {
            DrawLines();

            // show the basic statistic of account
            GLFont font = FightResources.Font.NormalFont;
            font.CentreDraw(AccountData.PlayerName, new(480, 268), Color.White, 1.5f, 0.21f);

            float alp = 0.3f;
            SpriteBatch.DrawVertex(0.1f, new[] { 
                new VertexPositionColor(new(300, 240, 0.1f), vertexColors[0] * alp),
                new VertexPositionColor(new(630, 240, 0.1f), vertexColors[1] * alp),
                new VertexPositionColor(new(660, 270, 0.1f), vertexColors[2] * alp),
                new VertexPositionColor(new(660, 300, 0.1f), vertexColors[3] * alp),
                new VertexPositionColor(new(300, 300, 0.1f), vertexColors[4] * alp),
            });

            font.CentreDraw(MathUtil.FloatToString(AccountData.Skill, 2), new(370, 328), Color.Wheat, 1.3f, 0.1f);
            if (playTimeHour < 1000)
                font.CentreDraw(MathUtil.FloatToString(playTimeHour, 1) + "h", new(595, 328), Color.Wheat, 1.23f, 0.1f);
            else
                font.CentreDraw(MathUtil.FloatToString(playTimeHour, 0) + "h", new(595, 328), Color.Wheat, 1.23f, 0.1f);

            for (int i = 0; i < 3; i++)
            {
                this.Image = medalTextures[medalStates[i]];

                this.FormalDraw(this.Image, new(450 + i * 30, 330), Color.White, 0.0f, ImageCentre);
            }
            font.CentreDraw("Death count:" + AccountData.PlayerStatistic.DeathCount, new(492, 427), Color.White, 1.23f, 0.1f);
            font.CentreDraw("Abs.Rating:" + MathUtil.FloatToString(AccountData.AbsoluteSkill, 2), new(480, 385), Color.White, 1.2f, 0.1f);
        }

        private static readonly Color[] ratingLevels = { Color.Green, Color.Lime, Color.LawnGreen, Color.Blue,
                Color.MediumPurple, Color.Red, Color.OrangeRed, Color.Orange, Color.Gold, Color.Gold};

        private void DrawLines()
        {
            // The centre area, showing the data of player!
            DrawLine(new(490 , 240), new(630, 240), Color.White);
            DrawLine(new(630, 240), new(630 + 30, 240 + 30), Color.White);
            DrawLine(new(630 + 30, 270), new(630 + 30, 460), Color.White);
            
            DrawLine(new(330 - 30, 240), new(300, 430), Color.White);
            DrawLine(new(330, 460), new(300, 430), Color.White);
            DrawLine(new(330, 460), new(660, 460), Color.White);

            DrawLine(new(304, 350), new(430, 350), vertexColors[0]);
            DrawLine(new(656, 350), new(530, 350), Color.White);

            // Draw the line of all selections

            // TL
            DrawLine(new(270, 65), new(270, 205), Color.White);
            DrawLine(new(270, 65), new(245, 40), Color.White);
            DrawLine(new(70, 205), new(270, 205), Color.White);
            DrawLine(new(70, 205), new(40, 175), Color.White);

            // TR
            DrawLine(new(670, 65), new(670, 180), Color.White);
            DrawLine(new(670, 65), new(670, 45), Color.White);
            DrawLine(new(670, 180), new(700, 210), Color.White);
            DrawLine(new(920, 210), new(700, 210), Color.White);

            // BL
            DrawLine(new(40, 480), new(255, 480), Color.White);
            DrawLine(new(285, 510), new(255, 480), Color.White);
            DrawLine(new(285, 510), new(285, 650), Color.White);

            // BR
            DrawLine(new(680 + 30, 665), new(680, 635), Color.White);
            DrawLine(new(680, 490), new(680, 630), Color.White);
            DrawLine(new(680, 490), new(900, 490), Color.White);
            DrawLine(new(900, 490), new(930, 520), Color.White);
        }

        int playTimeSec;
        float playTimeHour;

        public override void Update()
        {
            playTimeSec = AccountData.PlayerStatistic.PlayedTime;
            playTimeHour = playTimeSec / 3600.0f;
            base.Update();

            if (GameStates.IsKeyPressed(InputIdentity.Cancel))
            {
                this.Dispose();
                GameStates.InstanceCreate(new IntroUI());
            }
        }
    }
}