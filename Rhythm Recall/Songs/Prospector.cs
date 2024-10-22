﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal class Prospector : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Prospector()
        {
            dif.Add("Div.2", Difficulty.Noob);
            dif.Add("Div.1", Difficulty.Hard);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (128f / 60f)) { }
            public string Music => "Prospector";
            public string FightName => "Prospector";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "";
                public override string BarrageAuthor => "Tlottgodinf";
                public override string AttributeAuthor => "Tlottgodinf";
                public Information() { this.MusicOptimized = true; }
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Noob,0f),
                    new(Difficulty.Hard,0f),


                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                    new(Difficulty.Hard,0f),
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                    new(Difficulty.Hard,0f),
                    }
                    );
            }
            public void Start()
            {

            }
            public void Easy()
            {
                
            }

            public void Extreme()
            {
                
            }

            public void ExtremePlus()
            {
                
            }

            public void Hard()
            {
                
            }

            public void Noob()
            {
                
            }

            public void Normal()
            {
                
            }
        }
    }
}
