using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    { 
        partial class DifficultyUI
        {
            private class JudgementSelection : Button
            {
                JudgementState _judgeState;
                public JudgementSelection(DifficultyUI father) : base(father, new Vector2(797, 543), "")
                {
                    this._judgeState = father._virtualFather.CurrentJudgementState;

                    this._father = father;
                    NeverEnable = true;

                    ReGenerate(); 

                    this.LeftClick += ChangeJudgement;
                }

                private void ChangeJudgement()
                {
                    this._judgeState = _judgeState switch
                    {
                        JudgementState.Balanced => JudgementState.Lenient,
                        JudgementState.Lenient => JudgementState.Strict,
                        JudgementState.Strict => JudgementState.Balanced,
                        _ => throw new ArgumentException()
                    };
                    ReGenerate();
                }

                private void ReGenerate()
                {
                    this.ChangeText(this._judgeState.ToString());
                    this.ColorNormal = _judgeState switch
                    {
                        JudgementState.Balanced => Color.YellowGreen,
                        JudgementState.Lenient => Color.LimeGreen,
                        JudgementState.Strict => Color.Red,
                        _ => throw new ArgumentException()
                    };

                    _father.ChangeJudge(_judgeState);
                }

                new DifficultyUI _father;

                public override void Draw()
                {
                    base.Draw();
                    FightResources.Font.NormalFont.CentreDraw("Current Judge:", this.Centre - new Vector2(0, 45), Color.White, 1.15f, 0.1f);
                }

                public override void Update()
                {
                    if (GameStates.IsKeyPressed120f(Keys.Space)) this.ConfirmKeyDown();
                    base.Update();
                }
            }
        }
    }
}