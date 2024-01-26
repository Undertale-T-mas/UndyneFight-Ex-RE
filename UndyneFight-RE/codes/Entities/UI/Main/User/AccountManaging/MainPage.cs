using System;
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
using static UndyneFight_Ex.UserService.RatingCalculater.RatingList;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.GameStates;
using UndyneFight_Ex.Remake.UI.DEBUG;
using static UndyneFight_Ex.PlayerManager;
using static UndyneFight_Ex.GlobalResources.Sprites;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Remake.UI
{
    public class AccountManager : SmartSelector
    {
        private static void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
        {
            DrawingLab.DrawLine(start, end, size, color, 0.5f);
        }

        User AccountData;
        SingleSong[] data = new SingleSong[9];
        int datacount=0;
        RatingCalculater.RatingList list;
        public AccountManager()
        {
            AccountData = CurrentUser;
            list = CurrentUser.GenerateList();
        }

        public override void Start()
        {
            base.Start();

            // Do initializes
            this.InitializeColor();
            this.InitializeMedal();

            data[0] = list.APDonor;
            data[1] = list.FCDonor;
            data[2] = list.CompleteDonor;
            for (int i = 2; i < 9; i++)
            {
                if (list.StrictDonors.Count == 0) break;
                data[i] = list.StrictDonors.Max;
                list.StrictDonors.Remove(list.StrictDonors.Max);
                datacount=i;
                
            }
        }
        Color[] vertexColors;

        Texture2D[] medalTextures;
        int[] medalStates;

        void InitializeMedal()
        {
            float skill = AccountData.Skill;
            medalTextures = new[] { brimMedal, medal, starMedal };
            medalStates = new[] { 0, 0, 0 };
            if (skill >= 60) medalStates = new[] { 1, 0, 0 };
            if (skill >= 70) medalStates = new[] { 1, 1, 0 };
            if (skill >= 80) medalStates = new[] { 1, 1, 1 };
            if (skill >= 90) medalStates = new[] { 2, 1, 1 };
            if (skill >= 92.5f) medalStates = new[] { 2, 2, 1 };
            if (skill >= 95) medalStates = new[] { 2, 2, 2 };
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
            if (id == 0)
            {
                lerp1 = Color.Lerp(ratingLevels[0], ratingLevels[1], skill / 20f);
            }
            else
            {
                lerp1 = Color.Lerp(ratingLevels[id], ratingLevels[id + 1], skill % 10f / 10f);
            }
            vertexColors[0] = ratingLevels[id];
            vertexColors[1] = Color.Lerp(vertexColors[0], lerp1, 0.35f);
            vertexColors[2] = Color.Lerp(vertexColors[0], lerp1, 0.65f);
            vertexColors[4] = Color.Lerp(vertexColors[0], lerp1, 0.5f);
            vertexColors[3] = lerp1;

        }

        public override void Draw()
        {
            #region User UI
            DrawLines();

            // show the basic statistic of account
            GLFont font = NormalFont;
            font.CentreDraw(AccountData.PlayerName, new(480, 268), Color.White, 1.5f, 0.21f);

            float alp = 0.3f;
            SpriteBatch.DrawVertex(0.1f, new[] {
                new VertexPositionColor(new(300, 240, 0.1f), vertexColors[0] * alp),
                new VertexPositionColor(new(630, 240, 0.1f), vertexColors[1] * alp),
                new VertexPositionColor(new(660, 270, 0.1f), vertexColors[2] * alp),
                new VertexPositionColor(new(660, 300, 0.1f), vertexColors[3] * alp),
                new VertexPositionColor(new(300, 300, 0.1f), vertexColors[4] * alp),
            });

            font.CentreDraw(FloatToString(AccountData.Skill, 2), new(370, 328), Color.Wheat, 1.3f, 0.1f);
            var digit = playTimeHour < 1000 ? 1 : 0;
            font.CentreDraw(FloatToString(playTimeHour, digit) + "h", new(595, 328), Color.Wheat, 1.23f, 0.1f);

            for (int i = 0; i < 3; i++)
            {
                this.Image = medalTextures[medalStates[i]];

                this.FormalDraw(this.Image, new(450 + i * 30, 330), Color.White, 0.0f, ImageCentre);
            }
            font.CentreDraw("Death count:" + AccountData.PlayerStatistic.DeathCount, new(492, 427), Color.White, 1.23f, 0.1f);
            font.CentreDraw($"Abs.Rating:{AccountData.AbsoluteSkill:F2}", new(480, 385), Color.White, 1.2f, 0.1f);
            #endregion

            #region Rating List
            //Box
            float BoxDepth = 1f;
            DrawingLab.DrawLine(new(480, BoxYTop + 2.5f), new(480, 721), 952.5f, Color.Black, BoxDepth - 0.1f);
            DrawingLab.DrawLine(new(0, BoxYTop), new(960, BoxYTop), 5, Color.White, BoxDepth);
            DrawingLab.DrawLine(new(2.5f, BoxYTop), new(2.5f, BoxYTop + 600), 5, Color.White, BoxDepth);
            DrawingLab.DrawLine(new(957.5f, BoxYTop), new(957.5f, BoxYTop + 600), 5, Color.White, BoxDepth);

            DrawingLab.DrawLine(new(451, BoxYTop + ArrowY), new(481, BoxYTop - 80), 5, Color.White, BoxDepth);
            DrawingLab.DrawLine(new(509, BoxYTop + ArrowY), new(479, BoxYTop - 80), 5, Color.White, BoxDepth);

            font.CentreDraw("Rating Data", new(480, BoxYTop + 40), Color.White, 1.5f, BoxDepth);

            Color TextColor;
            string RatingText, DifficultyText, SongName;
            for (int i = 0; i <= datacount; i++)
            {
                TextColor = i switch
                {
                    0 => Color.Gold,
                    1 => Color.Orange,
                    2 => Color.Lime,
                    _ => Color.White,
                };
                DifficultyText = (int)data[i].difficulty switch
                {
                    0 => "NB",
                    1 => "EZ",
                    2 => "NR",
                    3 => "HD",
                    4 => "EX",
                    5 => "EX+",
                    _ => ""
                };
                string RealSongName = data[i].name;
                SongName = RealSongName.Length > 16 ? RealSongName[0..7] + "..." + RealSongName[(RealSongName.Length - 7)..] : RealSongName;
                RatingText = $"{SongName} ({DifficultyText}, LV.{data[i].threshold})";
                font.Draw(RatingText, new(50, BoxYTop + 60 + i * 30), TextColor, 1, BoxDepth);
                font.Draw($"ACC = {(data[i].accuracy * 100):F2}%", new(570, BoxYTop + 60 + i * 30), TextColor, 1, BoxDepth);
                font.Draw($"{data[i].scoreResult:F2}", new(790, BoxYTop + 60 + i * 30), TextColor, 1, BoxDepth);
            }
            DrawingLab.DrawLine(new(0, BoxYTop + 350), new(960, BoxYTop + 350), 5, Color.White, BoxDepth);
            font.Draw("Song Name", new(50, BoxYTop + 360), Color.White, 1.3f, BoxDepth);
            font.Draw("Accuracy", new(570, BoxYTop + 360), Color.White, 1.3f, BoxDepth);
            font.Draw("Rating", new(790, BoxYTop + 360), Color.White, 1.3f, BoxDepth);
            #endregion
        }

        private static readonly Color[] ratingLevels = { Color.Green, Color.Lime, Color.LawnGreen, Color.Blue,
                Color.MediumPurple, Color.Red, Color.OrangeRed, Color.Orange, Color.Gold, Color.Gold};

        private void DrawLines()
        {
            // The centre area, showing the data of player!
            //cool rgb
            DrawLine(new(490, 240), new(630, 240), new(DrawingLab.HsvToRgb(timer, 255, 255, 255)));
            DrawLine(new(630, 240), new(630 + 30, 240 + 30), new(DrawingLab.HsvToRgb(timer + 10, 255, 255, 255)));
            DrawLine(new(630 + 30, 270), new(630 + 30, 460), new(DrawingLab.HsvToRgb(timer + 20, 255, 255, 255)));

            DrawLine(new(330 - 30, 240), new(300, 430), new(DrawingLab.HsvToRgb(timer + 30, 255, 255, 255)));
            DrawLine(new(330, 460), new(300, 430), new(DrawingLab.HsvToRgb(timer + 40, 255, 255, 255)));
            DrawLine(new(330, 460), new(660, 460), new(DrawingLab.HsvToRgb(timer + 50, 255, 255, 255)));
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
        int timer = 0;

        bool active = false;
        int RatingTimer = 0;
        float BoxYTop = 480, ArrowY = -50;
        public override void Update()
        {
            timer++;
            playTimeSec = AccountData.PlayerStatistic.PlayedTime;
            playTimeHour = playTimeSec / 3600.0f;
            base.Update();

            if (IsKeyPressed(InputIdentity.Cancel))
            {
                this.Dispose();
                InstanceCreate(new IntroUI());
            }


            if (active)
            {
                if (RatingTimer < 60) RatingTimer++;
            }
            else
            {
                if (RatingTimer > 0) RatingTimer--;
            }

            if (IsKeyPressed120f(InputIdentity.Alternate))
            {
                active = !active;
            }
            BoxYTop = TKValueEasing.EaseInOutCirc(RatingTimer, 720, -420, 60);
            ArrowY = TKValueEasing.EaseInOutBack(RatingTimer, -50, -60, 60);
        }
    }
}