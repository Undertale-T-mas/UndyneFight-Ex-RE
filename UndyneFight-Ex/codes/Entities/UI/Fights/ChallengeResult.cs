using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class ChallengeResult : Entity
    {
        private class SingleResult : Entity
        {
            SongResult result;
            public SingleResult(SongResult result, int index)
            {
                this.index = index;
                collidingBox = new CollideRect(Vector2.Zero, new(575, 91));
                this.result = result;
                bool direction = index % 2 == 0;
                Centre = new Vector2(direction ? -320 : 960, index * 100 + 113);
                ValueEasing.EaseBuilder builder = new();
                builder.Insert(60, ValueEasing.EaseOutQuint(Centre.X, 320, 60));
                builder.Run(s => Centre = new Vector2(s, Centre.Y));
                InstanceCreate(new InstantEvent(61, () => Centre = new Vector2(320, Centre.Y)));
            }
            int index;
            public override void Start()
            {
                var tuple = (FatherObject as ChallengeResult).completedChallenge.Routes[index];
                songName = (Activator.CreateInstance(tuple.Item1) as IWaveSet).FightName;
                difficulty = tuple.Item2;
                difColor = (int)difficulty switch
                {
                    0 => Color.White,
                    1 => Color.LawnGreen,
                    2 => Color.LightBlue,
                    3 => Color.MediumPurple,
                    4 => Color.Orange,
                    _ => Color.Gray
                };
                mark = result.CurrentMark;
                remarkColor = mark switch
                {
                    SkillMark.Failed => Color.DarkRed,
                    SkillMark.Ordinary => Color.Green,
                    SkillMark.Acceptable => Color.SpringGreen,
                    SkillMark.Respectable => Color.LightSkyBlue,
                    SkillMark.Excellent => Color.MediumPurple,
                    SkillMark.Eminent => Color.OrangeRed,
                    SkillMark.Impeccable => Color.Goldenrod,
                    _ => throw new ArgumentException($"{nameof(mark)} has something wrong", nameof(mark))
                };
            }
            SkillMark mark;
            Difficulty difficulty;
            Color difColor;
            Color remarkColor;
            string songName;
            public override void Draw()
            {
                DrawingLab.DrawRectangle(CollidingBox, Color.White, 3.0f, 0.1f);
                NormalFont.Draw(songName, CollidingBox.TopLeft + new Vector2(18, 9), Color.White);
                float size = NormalFont.SFX.MeasureString(difficulty.ToString()).X;
                NormalFont.Draw(difficulty.ToString(), CollidingBox.TopRight - new Vector2(15 + size, -5), difColor);
                NormalFont.Draw(MathUtil.FloatToString(result.Accuracy * 100f, 2) + "%",
                    CollidingBox.TopLeft + new Vector2(18, 48), Color.Wheat, 1f, 0.1f);
                NormalFont.Draw(result.Score.ToString(), CollidingBox.TopLeft + new Vector2(158, 48), Color.White, 1f, 0.1f);
                NormalFont.CentreDraw(mark.ToString(), CollidingBox.TopLeft + new Vector2(438, 62), remarkColor, 1.32f, -0.025f, 0.1f);

            }

            public override void Update()
            {
            }
        }
        Challenge completedChallenge;
        public ChallengeResult(Challenge challenge)
        {
            completedChallenge = challenge;
        }
        public override void Start()
        {
            CreateResultUI(completedChallenge);
        }

        float totalAccuracy = 0;
        private void CreateResultUI(Challenge challenge)
        {
            var enumerator = challenge.ResultBuffer.GetEnumerator();
            curShowAccuracy = 0;
            totalAccuracy = 0;
            totalX = -120;
            appearTime = 0;
            float acc = 0;
            for (int i = 0; i < 3; i++)
            {
                float delay = i * 12;
                int t = i;
                InstanceCreate(new InstantEvent(delay, () =>
                {
                    enumerator.MoveNext();
                    AddChild(new SingleResult(enumerator.Current, t));
                    totalAccuracy += enumerator.Current.Accuracy;
                }));
                acc += challenge.ResultBuffer[i].Accuracy;
            }

            acc *= 100;
            alpha = 0;
            if (acc >= 300)
            {
                result = "Impeccable";
                resultColor = Color.Goldenrod;
            }
            else if (acc >= 297)
            {
                result = "Eminent";
                resultColor = Color.OrangeRed;
            }
            else if (acc >= 294)
            {
                result = "Excellent";
                resultColor = Color.MediumPurple;
            }
            else if (acc >= 288)
            {
                result = "Respectable";
                if (acc >= 291) result += "+";
                resultColor = Color.LightSkyBlue;
            }
            else if (acc >= 270)
            {
                result = "Acceptable";
                if (acc >= 279) result += "+";
                resultColor = Color.SpringGreen;
            }
            else
            {
                result = "Unaccepted";
                resultColor = Color.DarkRed;
            }
        }
        string result; Color resultColor;
        float curShowAccuracy;
        float totalX = -120;
        public override void Draw()
        {
            FightResources.Font.NormalFont.CentreDraw("Challenge Result", new(320, 31), Color.White);
            FightResources.Font.NormalFont.CentreDraw("Total:" + MathUtil.FloatToString(curShowAccuracy * 100, 1) + "%",
                new Vector2(totalX, 400), Color.Wheat);
            if (calcFinished && (appearTime % 60 < 30 || appearTime > 180))
                FightResources.Font.NormalFont.CentreDraw("press Z to return", new Vector2(320, 449),
                    Color.Lime);
            if (calcFinished)
            {
                FightResources.Font.NormalFont.CentreDraw(result, new Vector2(475, 398), resultColor * alpha, 1.5f, -0.03f, 0.5f);
            }
        }
        bool calcFinished = false;
        float alpha = 0;
        int appearTime = 0;
        public override void Update()
        {
            if (IsKeyPressed(InputIdentity.Confirm)) ResetScene(new GameMenuScene());
            curShowAccuracy = MathHelper.Lerp(curShowAccuracy, totalAccuracy + 0.00002f, 0.07f);
            calcFinished = MathF.Abs(curShowAccuracy - totalAccuracy) < 0.00003f;
            if (calcFinished)
            {
                totalX = MathHelper.Lerp(totalX, 140, 0.12f); appearTime++;
                if (alpha < 1) alpha += 0.05f;
            }
            else totalX = MathHelper.Lerp(totalX, 320, 0.12f);
        }
    }
}