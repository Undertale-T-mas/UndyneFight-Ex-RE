using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.MathUtil;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rhythm_Recall.Waves
{
    public partial class Traveler_at_Sunset
    {
        public partial class Project  
        { 
            private class Sans : Entity
            {
                private Texture2D head;
                private Texture2D body;
                private Texture2D leg;
                public Sans(ContentManager loader) {
                    loader.RootDirectory = "Content\\Musics\\Travel at Sunset\\Sans";
                    head = loader.Load<Texture2D>("head");
                    body = loader.Load<Texture2D>("body");
                    leg = loader.Load<Texture2D>("leg");
                    GeneratePart();
                }

                ImageEntity compHead, compBody, compLeg;

                private void GeneratePart()
                {
                    compHead = new(head);
                    compBody = new(body);
                    compLeg = new(leg);
                    this.AddChild(compHead);
                    this.AddChild(compBody);
                    this.AddChild(compLeg);
                }

                public override void Draw()
                { 
                }

                public override void Update()
                { 
                }
            }
        }
    }
}