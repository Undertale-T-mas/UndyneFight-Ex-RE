using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall
{
    public class FacingCharactor : Scene
    {
        private class Question
        {
            public string QuestionInformation { get; init; }
            public string[] Selections { get; init; }
            private int properSelection;
            public Question(string information, string[] selections, int properSelection)
            {
                if (selections.Length != 4) throw new ArgumentException();
                QuestionInformation = information;
                Selections = selections;
                this.properSelection = properSelection;
            }
            public Question Shuffle()
            {
                string[] newSelections = new string[4];
                for (int i = 0; i < 4; i++) newSelections[i] = Selections[i];
                int newProperSelection = properSelection;
                for (int i = 0; i < 4; i++)
                {
                    int x = Rand(0, 3), y = Rand(0, 3);
                    if (x == y) continue;
                    if (y == newProperSelection) newProperSelection = x;
                    else if (x == newProperSelection) newProperSelection = y;

                    string temp = newSelections[x];
                    newSelections[x] = newSelections[y];
                    newSelections[y] = temp;
                }
                return new(QuestionInformation, newSelections, newProperSelection);
            }
            public bool CheckAnswer(int index)
            {
                return index == properSelection;
            }
        }
        private static class QuestionLibrary
        {
            private static Question[] questions;
            static QuestionLibrary()
            {
                /* 
                    The Undertale AU I have created
                    //options

                    The header I acquire in T-mas' group
                    Chocolate Chara/La Pluma/Operator/=)

                    My Birthday

                    16/17/18/19th October

                    My avator on Bilibili
                    Chocolate/Chocolate Chara/La Pluma/Ark

                    My last participation of Rhythm Recall tournament
                    2021 New Year Feast/ 2021 Red May/ 2021 Mid Autumn Feast/ 2022 New Year Feast

                */
                questions = new[] {
                    new Question("The Undertale AU I have created",
                        new[] { "UNDERFELL", "UNDERDEF", "UNDERNERF", "UNDERLOOP" },
                        1),
                    new Question("The header I acquire in T-mas' group",
                        new[] { "Chocolate Chara", "La Pluma", "Operator", "=)" },
                        0),
                    new Question("My Birthday",
                        new[] { "16th October", "17th October", "18th October", "19th October" },
                        2),
                    new Question("My avator on Bilibili",
                        new[] { "Chocolate", "Chocolate Chara", "La Pluma", "Ark" },
                        2),
                    new Question("My last participation of championship",
                        new[] { "2021-Spring Celebration", "2021-Red May", "2021-Mid Autumn Feast", "2022-New Year Carnival" },
                        2),
                };
            }
            private const int totalCount = 5;
            private const int count = 3;
            public static Question[] GetRandomQuestion()
            {
                HashSet<int> questionIDs = new();
                int[] items = new int[count];
                for (int i = 0; i < count; i++)
                {
                    int s = Rand(0, totalCount - 1);
                    while (questionIDs.Contains(s)) s = Rand(0, totalCount - 1);
                    questionIDs.Add(s);
                    items[i] = s;
                }
                Question[] result = new Question[count];

                for (int i = 0; i < count; i++)
                {
                    Question cur = questions[items[i]];
                    result[i] = cur.Shuffle();
                }

                return result;
            }
        }
        private class QuestionUI : Entity
        {
            Question[] allQuestions;
            Question currentQuestion;

            int currentSelection = 0;

            int correctCount;
            int currentProgress = 0;
            private const float acceptPercentage = 0.6f;

            public QuestionUI()
            {
                allQuestions = QuestionLibrary.GetRandomQuestion();
                currentQuestion = allQuestions[0];
                GenerateSelection();
                UpdateIn120 = true;
            }
            private void GenerateSelection()
            {
                for (int i = 0; i < 4; i++)
                    AddChild(selections[i] = new Selection(this, i));
                lastSelection = currentSelection = 0;
                selections[0].Selected();
            }
            private void DisposeSelection()
            {
                ChildObjects.ForEach(s => (s as Selection)?.Dispose());
            }
            private void Select()
            {
                if (currentQuestion.CheckAnswer(currentSelection))
                    correctCount++;
                currentProgress++;
                DisposeSelection();
                if (currentProgress == allQuestions.Length)
                {
                    End();
                    return;
                }
                currentQuestion = allQuestions[currentProgress];
                GenerateSelection();
            }
            private void End()
            {
                bool isAccept = correctCount / allQuestions.Length > acceptPercentage;
                if (isAccept)
                {
                    GameStates.ResetScene(new Waves.EndTimeAnomaly());
                }
                else
                {
                    GameStates.EndFight();
                }
            }

            private class Selection : TextSelection
            {
                private static Vector2 FromIndex(int index)
                {
                    float x, y;
                    x = 160 + ((index % 2 == 0) ? 0 : 320);
                    y = (index < 2) ? 340 : 400;
                    return new Vector2(x, y);
                }
                QuestionUI father;
                public Selection(QuestionUI father, int index) : base(father.currentQuestion.Selections[index], FromIndex(index))
                {
                    this.father = father; Size = 0.75f; MaxSize = 1.2f;
                }
                public override void SelectionEvent()
                {
                    father.Select();
                }
            }

            Selection[] selections = new Selection[4];

            public override void Draw()
            {
                Font.NormalFont.CentreDraw(currentQuestion.QuestionInformation, new(320, 264), Color.White, 0.85f, 0.5f);
            }

            int lastSelection = -1;

            public override void Update()
            {
                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    currentSelection += 2;
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                {
                    currentSelection -= 2;
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    currentSelection -= 1;
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.MainRight))
                {
                    currentSelection += 1;
                }
                if (currentSelection < 0) currentSelection = 0;
                if (currentSelection > 3) currentSelection = 3;

                if (lastSelection != currentSelection)
                {
                    selections[lastSelection].DeSelected();
                    selections[currentSelection].Selected();
                    lastSelection = currentSelection;
                    PlaySound(Sounds.changeSelection);
                }

                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                {
                    selections[currentSelection].SelectionEvent();
                }
            }
        }
        public class Charactor : AutoEntity
        {
            public Charactor() : base()
            {
                Image = Loader.Load<Texture2D>("Musics\\EndTime\\sprite_charactor");
            }
            public override void Update()
            {
                Alpha = 1f;
                Centre = new(320, 110);
                Scale = 0.5f;
            }
        }
        public class EventControl : GameObject
        {
            public EventControl()
            {
                ScreenDrawing.MasterAlpha = 0.0f;
            }
            int appearTime = 0;
            GlobalResources.Effects.GrayShader shader;
            public override void Update()
            {
                appearTime++;
                if (appearTime <= 9)
                {
                    ScreenDrawing.MasterAlpha = 0.0f; return;
                }
                if (appearTime == 10)
                {
                    AddInstance(new Charactor());
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Glitching(0.4f) { AverageInterval = 1, AverageDelta = 0.8f });
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader = Shaders.Gray, 0.5f));
                }
                if (ScreenDrawing.MasterAlpha < 1f)
                {
                    ScreenDrawing.MasterAlpha += 0.01f;
                }
                if (Rand(0, 10) == 0)
                {
                    shader.Intensity = Rand(0.65f, 0.8f);
                }
                if (appearTime == 50) AddInstance(new QuestionUI());
            }
        }
        public FacingCharactor()
        {
            InstanceCreate(new EventControl());
        }
    }
}