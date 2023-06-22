using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class ChallengeSelector : Selector
    {
        private class ChallengeCard : Entity, ISelectAble
        {
            public readonly Challenge challenge;
            private bool isSelected = false;
            private Vector2 startPosition;
            private readonly ChallengeSelector selector;
            public ChallengeCard(Challenge challenge, Vector2 Centre, ChallengeSelector selector)
            {
                this.selector = selector;
                collidingBox = new CollideRect(0, 0, 560, 115);
                startPosition = Centre;
                this.challenge = challenge;
                try
                {
                    Image = Scene.Loader.Load<Texture2D>(challenge.IconPath);
                }
                catch
                {
                    Image = Sprites.championShip;
                }
                songs = new Tuple<string, Difficulty>[challenge.Routes.Length];
                int i = 0;
                foreach (var v in challenge.Routes)
                {
                    IWaveSet wave = Activator.CreateInstance(v.Item1) as IWaveSet;
                    string s = wave.FightName;
                    string text = s.Length > 17 ? s[0..7] + ".." + s[(s.Length - 8)..] : s;
                    songs[i] = new(text, v.Item2);
                    i++;
                }
            }

            public override void Update()
            {
                Centre = startPosition - new Vector2(XPositionDelta, 0);

                if (PlayerManager.CurrentUser != null && result == null)
                {
                    var data = PlayerManager.CurrentUser.ChallengeData;
                    if (!data.AllData.ContainsKey(challenge.Title)) return;
                    float acc = data.AllData[challenge.Title].TripleAccuracy;
                    acc *= 100;
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
            }
            Tuple<string, Difficulty>[] songs;

            public Color resultColor;
            public string result = null;
            Color DifficultyColor(Difficulty difficulty)
            {
                return difficulty switch
                {
                    Difficulty.Noob => Color.White,
                    Difficulty.Easy => Color.LawnGreen,
                    Difficulty.Normal => Color.LightBlue,
                    Difficulty.Hard => Color.MediumPurple,
                    Difficulty.Extreme => Color.Orange,
                    _ => Color.Gray
                };
            }
            public override void Draw()
            {
                GameMain.MissionSpriteBatch.Draw(Image, new Rectangle((int)collidingBox.X + 5, (int)collidingBox.Y + 7, 100, 100), null, Color.White,
                    0.0f, Vector2.Zero, SpriteEffects.None, 0.92f);
                var BoxMiddle = collidingBox.Height / 2;
                DrawingLab.DrawLine(new(collidingBox.Left, collidingBox.Y + BoxMiddle), new(collidingBox.Right, collidingBox.Y + BoxMiddle), BoxMiddle * 2, Color.Black, 0.1f);
                DrawingLab.DrawRectangle(collidingBox, isSelected ? Color.Gold : Color.White, 4, 0.5f);

                NormalFont.CentreDraw(challenge.Title, Centre + new Vector2(30, -35), Color.White, 0.92f, 0.53f);
                int x = 0, y = 0;
                for (int i = 0; i < songs.Length; i++)
                {
                    NormalFont.Draw("* " + songs[i].Item1,
                        Centre + new Vector2(x * 224 - 166, y * 30 - 13), DifficultyColor(songs[i].Item2),
                        0.69f, 0.94f);

                    x++;
                    if (x == 2)
                    {
                        x = 0;
                        y++;
                    }
                }
                if (result != null)
                    NormalFont.CentreDraw(result, Centre + new Vector2(153, 28), resultColor, 1.35f, -0.03f, 0.4f);
            }

            public void DeSelected()
            {
                isSelected = false;
            }

            public void Selected()
            {
                isSelected = true;
            }

            public void SelectionEvent()
            {
                challenge.Reset();
                ResetScene(new SongLoadingScene(challenge, 0));
            }
        }
        private static float XPositionDelta;
        public ChallengeSelector()
        {
            XPositionDelta = 0;
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainDown) && currentSelect != SelectionCount - 1)
                {
                    currentSelect++;
                    if (currentSelect % 3 == 0) currentSelect--;
                }
                if (IsKeyPressed120f(InputIdentity.MainUp) && currentSelect != 0)
                {
                    currentSelect--;
                    if (currentSelect % 3 == 2) currentSelect++;
                }
                if (IsKeyPressed120f(InputIdentity.MainLeft) && curPage != 0)
                {
                    curPage--;
                    currentSelect -= 3;
                    XPositionDelta -= 640;
                }
                if (IsKeyPressed120f(InputIdentity.MainRight) && curPage != totalPage)
                {
                    curPage++;
                    currentSelect += 3;
                    if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                    XPositionDelta += 640;
                }
            };
            SelectChanged += () =>
            {
                PlaySound(FightResources.Sounds.changeSelection, 0.9f);
            };
            Selected += () =>
            {
                PlaySound(FightResources.Sounds.select, 0.9f);
            };

            Vector2 Position0 = new Vector2(320, 128);
            int yCnt = 0, pages = 0;
            FightSystem.challenges.ForEach(challenge =>
            {
                PushSelection(new ChallengeCard(challenge, Position0, this));
                Position0.Y += 125;
                yCnt++;
                if (yCnt == 3)
                {
                    pages++;
                    yCnt = 0;
                    Position0.Y = 128;
                    Position0.X += 640;
                }
            });
            if (yCnt == 0) pages--;
            ResetSelect();
            totalPage = pages;
        }
        int totalPage, curPage;

        public override void Draw()
        {
            NormalFont.CentreDraw("Challenges", new Vector2(320, 34), Color.White);
            NormalFont.CentreDraw("Page: " + (curPage + 1) + " / " + (totalPage + 1), new Vector2(320, 460), Color.White, 1.0f, 0.5f);
        }
    }
}