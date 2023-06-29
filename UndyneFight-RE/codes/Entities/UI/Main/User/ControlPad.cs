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

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class UserUI : Entity
    {
        private class VirtualFather : GameObject
        {  
            public VirtualFather()
            {
                Login = new LoginUI(); 
                this.AddChild(Login);
                Login.Activate();
                CurrentActivate = Login;
            }

            public bool Activated => true;

            public LoginUI Login { get; init; } 

            public ISelectChunk CurrentActivate { get; set; }

            public IWaveSet SongSelected { get; private set; }
            public HashSet<Difficulty> DifficultyPanel { get; private set; }

            public void SelectSong(object result)
            {
                if(result == null)
                {
                    DifficultyPanel = null;
                    SongSelected = null;
                    return;
                }
                if (result is IWaveSet)
                {
                    DifficultyPanel = new();
                    for (int i = 0; i <= 4; i++) DifficultyPanel.Add((Difficulty)i);
                    this.SongSelected = result as IWaveSet;
                }
                else if (result is IChampionShip)
                {
                    DifficultyPanel = new();
                    IChampionShip championShip;
                    this.SongSelected = (championShip = result as IChampionShip).GameContent;
                    foreach(Difficulty difficulty in championShip.DifficultyPanel.Values) { 
                        DifficultyPanel.Add(difficulty); 
                    }
                }
            }
            public void Select(ISelectChunk module)
            {
                if (CurrentActivate == module) return;
                CurrentActivate.Deactivate();
                CurrentActivate = module;
            }

            public override void Update()
            { 
            }

            public Difficulty CurrentDifficulty { get; private set; } = Difficulty.NotSelected;
            public JudgementState CurrentJudgementState { get; private set; } = JudgementState.Balanced;

            internal void SelectDiff(Difficulty difficulty)
            {
                CurrentDifficulty = difficulty;
            }

            internal void ChangeJudge(JudgementState judgeState)
            {
                CurrentJudgementState = judgeState;
            }
        }

        public UserUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            this.AddChild(new MouseCursor());
            this.AddChild(new LineDistributer());
            this.AddChild(new VirtualFather());
        }

        public override void Draw()
        { 

        }

        public override void Update()
        { 
        }
    }
}
