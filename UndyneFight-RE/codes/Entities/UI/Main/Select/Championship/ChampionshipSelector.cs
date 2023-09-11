using Microsoft.Xna.Framework;
using System;
using System.Net.Sockets;
using System.Text.Json.Serialization;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;

using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using System.Collections.Generic;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class ChampionshipSelector : SmartSelector
    {
        private class ParticleGenerater : GameObject
        {
            public override void Update()
            {
                if(Rand(0, 1f) < 0.765f)
                {
                    float speed = Rand(1.0f, 1.6f);
                    this.AddChild(new Particle(Color.White, new(0, -speed * 1.15f * 4f), Rand(16f, 24f) / MathF.Pow(speed, 1.42f) * 1.315f, new(Rand(0, 960), 730), FightResources.Sprites.square)
                    {
                        DarkingSpeed = 4,
                        AutoRotate = true
                    });
                }
            }
        }
        public ChampionshipSelector() {
            this.KeyEvent = KeyEventFull;
            this.Activate();
        }
        public override void Start()
        {
            ChampionshipInfoShower cis;
            ChampionShip c;
            this.AddChild(cis = new ChampionshipInfoShower());
            c = cis.Championship;
            this.AddChild(new Scoreboard(PlayerManager.CurrentUser?.ChampionshipData?.ChampionshipDivision(_currentChampionship)));
            this.AddChild(new MouseCursor());
            DivisionSelector ds = new DivisionSelector(cis.Info);
            this.AddChild(ds);

            if (c == null) goto A;

            // push song selections
            if(PlayerManager.CurrentUser != null)
            {
                if (PlayerManager.CurrentUser.ChampionshipData.InChampionship(c.Title))
                {
                    Button[] buttons = new Button[c.Fights.Values.Length];
                    for (int i = 0; i < c.Fights.Values.Length; i++)
                    {
                        IChampionShip song = Activator.CreateInstance(c.Fights.Values[i]) as IChampionShip;
                        IWaveSet waveset = song.GameContent;
                        string text = waveset.FightName;
                        bool hidden = waveset.Attributes.Hidden;
                        if (hidden) text = "? ? ? ? ?";


                    }
                } 
            }

            // push division selections
            MakeDivisionButton(c, ds);

            A:
            this.AddChild(new ParticleGenerater());
            base.Start();
        }

        private void MakeDivisionButton(ChampionShip c, DivisionSelector ds)
        {
            string[] divisions = ds.GetDivisions(c);
            List<Button> buttons = new List<Button>();
            for (int i = 0; i < divisions.Length; i++)
            {
                // get the lerp position
                float lerpPos = i * 1.0f / (divisions.Length - 1);
                float posX = MathHelper.Lerp(550f, 770f, lerpPos);

                Button bt;
                this.AddChild(bt = new Button(this, new(posX, 361), divisions[i]));
                buttons.Add(bt);
                string text = divisions[i];
                bt.LeftClick += () =>
                {
                    if (PlayerManager.CurrentUser == null) return;
                    PlayerManager.CurrentUser.ChampionshipData.SignUp(c.Title, text);
                    PlayerManager.Save();
                    buttons.ForEach(s => s.State = SelectState.Disabled);
                    bt.ColorDisabled = col.Orange;
                    all[0].OnFocus();
                };
            }
            if (PlayerManager.CurrentUser != null)
            {
                if (PlayerManager.CurrentUser.ChampionshipData != null)
                {
                    if (PlayerManager.CurrentUser.ChampionshipData.InChampionship(c.Title))
                    {
                        string divName = PlayerManager.CurrentUser.ChampionShipDiv(c.Title);
                        buttons.ForEach(s =>
                        {
                            s.State = SelectState.Disabled;
                            if (s.Text == divName) s.ColorDisabled = col.Orange;
                        });
                    }
                }
            }
        }

        public override void Draw()
        {

        }
        Color bound = Color.Transparent;
        float height = 0;
        public override void Update()
        {
            bound = Color.Lerp(bound, Color.Aqua, 0.035f);
            height = MathHelper.Lerp(height, 200, 0.05f);
            
            DownBoundDistance = height;
            LeftBoundDistance = 0;
            RightBoundDistance = 0;
            UpBoundDistance = 0;
            BoundColor = bound;

            if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
            {
                this.Dispose();
                GameStates.InstanceCreate(new SelectUI());
            }

            base.Update();
        }
        public override void Dispose()
        {
            MasterAlpha = 1f;
            DownBoundDistance = 0f;
            base.Dispose();
        }
    }
}