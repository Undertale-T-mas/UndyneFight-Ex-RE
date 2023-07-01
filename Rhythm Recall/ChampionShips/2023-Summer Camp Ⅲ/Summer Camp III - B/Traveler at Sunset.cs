using Microsoft.Xna.Framework;
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
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Traveler_at_Sunset : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Traveler_at_Sunset()
        {
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.ExtremePlus);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f/(226f/60f)) { }
            
            
            public string Music => "Traveler at Sunset";

            public string FightName => "Traveler at Sunset";

            public SongImformation Attributes => new Information();
            class Information : SongImformation
            {
                public override string SongAuthor => "SK_kent";
                public override string BarrageAuthor => "zKronO vs Tlottgodinf";
                public override string AttributeAuthor => "ParaDOXXX feat. Woem";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Normal,12.0f),
                    new(Difficulty.ExtremePlus,19.4f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Normal,12.0f),
                    new(Difficulty.ExtremePlus,19.4f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Normal,16.0f),
                    new(Difficulty.ExtremePlus,20.9f)
                    }
                    );
            }
            #region disused
            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }
            #endregion
            private class Rainer : Entity
            {
                public float Intensity { set; private get; } = 0;
                public float Speed { set; private get; } = 3f;

                public Rainer()
                {
                    Rotation = 10f;
                    UpdateIn120 = true;
                }

                private class WindParticle : Entity
                {
                    private readonly float randVal = 0;
                    private readonly Rainer rainer;
                    private readonly float alpha = 0;
                    private readonly float speed = 0;

                    public WindParticle(Rainer rainer)
                    {
                        UpdateIn120 = true;
                        speed = Rand(0f, 1f) + rainer.Speed;
                        Rotation = 0;
                        alpha = Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        Centre = new(320, Rand(-240,240));
                        randVal = Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (rainer.Intensity <= randVal) return;
                    }

                    public override void Update()
                    {
                        Vector2 del = GetVector2(speed / 1.5f, rainer.Rotation + Rotation);
                        Centre += del;
                        if (Centre.X < -499) Dispose();
                    }
                }
                private class RainDrop : Entity
                {
                    private readonly float length;
                    private readonly float randVal = 0;
                    private readonly Rainer rainer;
                    private readonly float alpha = 0;
                    private readonly float speed = 0;

                    public RainDrop(Rainer rainer)
                    {
                        controlLayer = rainer.controlLayer;
                        UpdateIn120 = true;
                        speed = Rand(0f, 1f) + rainer.Speed;
                        Rotation = 0;
                        alpha = Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        Centre = new(320, Rand(-240, 240));
                        length = Rand(6, 11) + rainer.Speed * 1f;
                        randVal = Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (rainer.Intensity <= randVal) return;
                        Vector2 del = GetVector2(length / 2, rainer.Rotation + Rotation + 80);
                        DrawingLab.DrawLine(Centre + del, Centre - del, 2, Color.LightBlue * alpha, 0.99f);
                    }

                    public override void Update()
                    {
                        Vector2 del = GetVector2(speed / 1.5f, rainer.Rotation + Rotation);
                        Centre += del;
                        if (Centre.X < -499) Dispose();
                    }
                }

                public override void Draw()
                {
                }

                public override void Update()
                {
                    var drop = new RainDrop(this);
                    AddChild(drop);
                }
            }
            public void ExtremePlus()
            {
                if(InBeat(0))
                {
                    Rainer r = new();
                    CreateEntity(r);
                    
                    RegisterFunction("FadeOut", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.MasterAlpha = s;
                        },
                        Stable(BeatTime(8),0),
                        EaseIn(BeatTime(24), 1, EaseState.Sine)
                        );
                        RunEase((q) =>
                        {
                            ScreenDrawing.ScreenScale = q;
                        },
                        Stable(BeatTime(8),5f),
                        EaseOut(BeatTime(72), -4f, EaseState.Sine)
                        );
                    });
                    BarrageCreate(0, BeatTime(1), 7, new string[]
                    {
                        "FadeOut"
                    });
                }
            }
            public void Normal()
            {

            }
            public void Start()
            {
                ScreenDrawing.ScreenPositionDetla = new(320, 240);
                GametimeDelta = -1.5f;
                SetSoul(0);
                InstantSetBox(new Vector2(0,0), 84, 84);
                InstantTP(0, 0);
                ScreenDrawing.MasterAlpha = 0f;
                ScreenDrawing.ScreenScale = 2f;
            }
        }
    }
}